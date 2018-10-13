using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeChanger
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\Alexander\source\repos\TestAPP\TestAPP.sln";
            MSBuildLocator.RegisterDefaults();
            var msbws = MSBuildWorkspace.Create();
            var solution = msbws.OpenSolutionAsync(path).Result;
            var projects = solution.Projects.Where(n => n.Name.Contains("TestAPP"));
            var document = projects.SelectMany(n => n.Documents.Where(x => x.Name == "Form1.Designer.cs")).Single();
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
            if (node.Left.GetLastToken().ToString() == "Text")
            {
                var memberAccessExpressions = _node.DescendantNodes().OfType<MemberAccessExpressionSyntax>().ToList();
                string prevType = string.Empty;
                for (int i = 0; i < memberAccessExpressions.Count; i++)
                {
                    if (memberAccessExpressions[i].GetText().ToString().Trim() == node.Left.GetText().ToString().Trim())
                    {
                        var typeInfo = _semanticModel.GetTypeInfo(memberAccessExpressions[i - 1]).Type;
                        if (typeInfo != null && typeInfo.Name == "Label"
                            && typeInfo.ContainingType.Name == "Forms")
                        {
                            //Console.WriteLine(memberAccessExpressions[i].GetText());//afafqwfWVWEVWEV
                            var newExpression = SyntaxFactory.ParseExpression(node.GetLeadingTrivia().ToString() + node.Left.ToString() + " = \"New Label 123\"");
                            return node.ReplaceNode(node, newExpression);
                        }
                    }
                }
            }

            if (node.Left.GetLastToken().ToString() == "Name")
            {
                var memberAccessExpressions = _node.DescendantNodes().OfType<MemberAccessExpressionSyntax>().ToList();
                string prevType = string.Empty;
                for (int i = 0; i < memberAccessExpressions.Count; i++)
                {
                    if (memberAccessExpressions[i].GetText().ToString().Trim() == node.Left.GetText().ToString().Trim())
                    {
                        var typeInfo = _semanticModel.GetTypeInfo(memberAccessExpressions[i - 1]).Type;
                        if (typeInfo != null && typeInfo.Name == "TextBox"
                            && typeInfo.ContainingType.Name == "Forms")
                        {
                            //Console.WriteLine(memberAccessExpressions[i].GetText());//afafqwfWVWEVWEV
                            var newExpression = SyntaxFactory.ParseExpression(node.GetLeadingTrivia().ToString() + node.Left.ToString() + " = \"TextBox 567\"");
                            return node.ReplaceNode(node, newExpression);
                        }
                    }
                }
            }
            return node;
        }
    }
}
