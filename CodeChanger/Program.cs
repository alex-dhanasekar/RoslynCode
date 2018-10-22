using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build;
using Microsoft.CodeAnalysis;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeChanger
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\IG-POS\451_UDRefresh\70791-UDSkinRefresh\source\UniversalDesktop.sln";
            MSBuildLocator.RegisterDefaults();
            var msbws = MSBuildWorkspace.Create();
            var solution = msbws.OpenSolutionAsync(path).Result;
            var projects = solution.Projects.Where(n => n.Name.Contains("InfoGenesis.UniversalDesktop.UI.Forms"));
            var document = projects.SelectMany(n => n.Documents.Where
            (x => x.FilePath == @"D:\IG-POS\451_UDRefresh\70791-UDSkinRefresh\INFOGENESIS\UniversalDesktop\UI\Forms\Employee\JobCodes\MainPanel.cs")).Single();
            var mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
            var tree = document.GetSyntaxTreeAsync().Result;

            var compilation = CSharpCompilation.Create("MyCompilation",
                syntaxTrees: new[] { tree }, references: new[] { mscorlib });
            var root = tree.GetRoot();
            var semanticModel = compilation.GetSemanticModel(tree);
            SyntaxNode newRoot = new MyConsoleWriteRewriter(semanticModel, root).Visit(root);
            var newDocument = document.WithText(newRoot.GetText());
            var result = msbws.TryApplyChanges(newDocument.Project.Solution);

            Console.Read();

        }
    }

    /// <summary> 
    /// Replaces Console.Write calls with equivalent 
    /// Console.WriteLine calls. 
    /// </summary> 
    class MyConsoleWriteRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        SyntaxNode _node;
        public MyConsoleWriteRewriter(SemanticModel model, SyntaxNode node)
        {
            _semanticModel = model;
            _node = node;
        }

        public override SyntaxNode
            VisitAssignmentExpression(
                AssignmentExpressionSyntax node)
        {
            //    var info = _semanticModel.GetTypeInfo(node.Left).Type;
            //if (info.Name == "Text" &&
            //    info.ContainingType.Name == "Label" && 
            //    info.ContainingNamespace.Name.Contains("Forms")) 
            //if (node.Left.GetLastToken().ToString() == "Text")
            //{
            //    var memberAccessExpressions = _node.DescendantNodes().OfType<MemberAccessExpressionSyntax>().ToList();
            //    string prevType = string.Empty;
            //    for (int i = 0; i < memberAccessExpressions.Count; i++)
            //    {
            //        if (memberAccessExpressions[i].GetText().ToString().Trim() == node.Left.GetText().ToString().Trim())
            //        {
            //            var typeInfo = _semanticModel.GetTypeInfo(memberAccessExpressions[i - 1]).Type;
            //            if (typeInfo != null && typeInfo.Name == "Label"
            //                && typeInfo.ContainingType.Name == "Forms")
            //            {
            //                //Console.WriteLine(memberAccessExpressions[i].GetText());//afafqwfWVWEVWEV
            //                var newExpression = SyntaxFactory.ParseExpression(node.GetLeadingTrivia().ToString() + node.Left.ToString() + " = \"New Label 123\"");
            //                return node.ReplaceNode(node, newExpression);
            //            }
            //        }
            //    }
            //}

            //if (node.Left.GetLastToken().ToString() == "Name")
            //{
            //    var memberAccessExpressions = _node.DescendantNodes().OfType<MemberAccessExpressionSyntax>().ToList();
            //    string prevType = string.Empty;
            //    for (int i = 0; i < memberAccessExpressions.Count; i++)
            //    {
            //        if (memberAccessExpressions[i].GetText().ToString().Trim() == node.Left.GetText().ToString().Trim())
            //        {
            //            var typeInfo = _semanticModel.GetTypeInfo(memberAccessExpressions[i - 1]).Type;
            //            if (typeInfo != null && typeInfo.Name == "TextBox"
            //                && typeInfo.ContainingType.Name == "Forms")
            //            {
            //                //Console.WriteLine(memberAccessExpressions[i].GetText());//afafqwfWVWEVWEV
            //                var newExpression = SyntaxFactory.ParseExpression(node.GetLeadingTrivia().ToString() + node.Left.ToString() + " = \"TextBox 567\"");
            //                return node.ReplaceNode(node, newExpression);
            //            }
            //        }
            //    }
            //}

            if (node.Left.GetLastToken().ToString() == "BackColor")
            {
                var memberAccessExpressions = node.DescendantNodes().OfType<MemberAccessExpressionSyntax>().ToList();
                string prevType = string.Empty;
                for (int i = 0; i < memberAccessExpressions.Count; i++)
                {
                    var typeInfo = _semanticModel.GetTypeInfo(memberAccessExpressions[i]).Type;
                    if (typeInfo != null && (typeInfo.Name == "IntlLabel" || typeInfo.Name == "IntlComboBox"
                    || typeInfo.Name.Contains("IntlCurrencyEditor") || typeInfo.Name.Contains("IntlCheckBox2")
                    || typeInfo.Name.Contains("IntlRadioButton2") || typeInfo.Name.Contains("UltraPictureBox")
                    || typeInfo.Name.Contains("LiveValidationEnforcementSpinner")
                    || typeInfo.Name.Contains("IntlPanel") || typeInfo.Name.Contains("Panel") || typeInfo.Name.Contains("IntlButton")
                    || typeInfo.Name.Contains("TextBox") || typeInfo.Name.Contains("UltraMaskedEdit")))
                    {
                        var rightExpression = " = UDThemeStyles.GenericBackColor";
                        if (memberAccessExpressions[i].GetText().ToString().ToUpper().Contains("DIAMOND") || typeInfo.Name.Contains("UltraMaskedEdit"))
                            rightExpression = "= System.Drawing.Color.White";
                        var newExpression = SyntaxFactory.ParseExpression(node.GetLeadingTrivia().ToString() + node.Left.ToString() + rightExpression);
                        //var flatStyleExpression = SyntaxFactory.ParseExpression(memberAccessExpressions[i].GetText().ToString() + "." + "FlatStyle =" + "FlatStyle.Flat;");
                        //List<AssignmentExpressionSyntax> list = new List<AssignmentExpressionSyntax>();
                        //list.Add(flatStyleExpression as AssignmentExpressionSyntax);
                        //node = node.InsertNodesAfter(node, list);
                        return node.ReplaceNode(node, newExpression);
                    }

                }
            }

            if (node.Left.GetLastToken().ToString() == "BackColorDisabled")
            {
                var memberAccessExpressions = node.DescendantNodes().OfType<MemberAccessExpressionSyntax>().ToList();
                string prevType = string.Empty;
                for (int i = 0; i < memberAccessExpressions.Count; i++)
                {
                    var typeInfo = _semanticModel.GetTypeInfo(memberAccessExpressions[i]).Type;
                    if (typeInfo != null && typeInfo.Name.Contains("UltraMaskedEdit"))
                    {
                        var rightExpression = " = UDThemeStyles.GenericBackColor";
                        var newExpression = SyntaxFactory.ParseExpression(node.GetLeadingTrivia().ToString() + node.Left.ToString() + rightExpression);
                        return node.ReplaceNode(node, newExpression);
                    }

                }
            }

            if (node.Left.GetLastToken().ToString().Contains("Font"))
            {
                var memberAccessExpressions = node.DescendantNodes().OfType<MemberAccessExpressionSyntax>().ToList();
                string prevType = string.Empty;
                for (int i = 0; i < memberAccessExpressions.Count; i++)
                {
                    var typeInfo = _semanticModel.GetTypeInfo(memberAccessExpressions[i]).Type;
                    if (typeInfo != null && ((typeInfo.Name == "IntlLabel" && node.Left.GetLastToken().ToString() == "Font") ||
                       (typeInfo.Name.Contains("IntlCheckBox2") && node.Left.GetLastToken().ToString().Contains("LabelFont")) ||
                       (typeInfo.Name.Contains("IntlRadioButton2") && node.Left.GetLastToken().ToString().Contains("LabelFont")) ||
                       (typeInfo.Name.Contains("IntlButton") && node.Left.GetLastToken().ToString() == "Font")
                        ))
                    {
                        var fontRightExpression = " = new System.Drawing.Font(\"Microsoft Sans Serif\", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0)";
                        if (memberAccessExpressions[i].GetText().ToString().ToUpper().Contains("DIAMOND"))
                            fontRightExpression = "= new System.Drawing.Font(\"Wingdings\", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 2)";
                        var newExpression = SyntaxFactory.ParseExpression(node.GetLeadingTrivia().ToString() + node.Left.ToString() + fontRightExpression);
                        return node.ReplaceNode(node, newExpression);
                    }

                }
            }

            if (node.Left.GetLastToken().ToString() == "ForeColor")
            {
                var memberAccessExpressions = node.DescendantNodes().OfType<MemberAccessExpressionSyntax>().ToList();
                string prevType = string.Empty;
                for (int i = 0; i < memberAccessExpressions.Count; i++)
                {
                    var typeInfo = _semanticModel.GetTypeInfo(memberAccessExpressions[i]).Type;
                    if (typeInfo != null && (typeInfo.Name.Contains("IntlRadioButton2") ||
                       typeInfo.Name.Contains("IntlButton")
                       ))
                    {
                        var foreColorRightExpression = string.Empty;
                        if (memberAccessExpressions[i].GetText().ToString().ToUpper().Contains("DIAMOND"))
                            foreColorRightExpression = "= UDThemeStyles.DiamondForeColor";
                        else if (typeInfo.Name.Contains("IntlRadioButton2"))
                            foreColorRightExpression = "= System.Drawing.Color.Black";
                        var newExpression = SyntaxFactory.ParseExpression(node.GetLeadingTrivia().ToString() + node.Left.ToString() + foreColorRightExpression);
                        return node.ReplaceNode(node, newExpression);
                    }

                }
            }
            return node;
        }
    }
}
