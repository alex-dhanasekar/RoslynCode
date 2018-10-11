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
            var projects = solution.Projects.Where(n=>n.Name.Contains("TestAPP"));
            var document = projects.SelectMany(n => n.Documents.Where(x => x.Name == "Form1.Designer.cs")).Single();
            var mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
            var tree = document.GetSyntaxTreeAsync().Result;
            var compilation = CSharpCompilation.Create("MyCompilation",
                syntaxTrees: new[] { tree },references: new[] { mscorlib});
            var root = tree.GetRoot();
            var initializeComponentMethod = root.DescendantNodes().OfType<MethodDeclarationSyntax>().Where
                (method=>method.Identifier.ToString().Contains("InitializeComponent")).Single();
            var memberAccessExpressions = initializeComponentMethod.DescendantNodes()
                .OfType<MemberAccessExpressionSyntax>().ToList();
            var semanticModel = compilation.GetSemanticModel(tree);
            string prevType = string.Empty;
            for (int i = 0; i < memberAccessExpressions.Count; i++) 
            {
                //var typeInfo = semanticModel.GetTypeInfo(memberAccessExpressions[i]).Type;
                //var symbolInfo = semanticModel.GetSymbolInfo(memberAccessExpressions[i]);
                var lastToken = memberAccessExpressions[i].GetLastToken();
                if(lastToken.Text == "Text")
                {
                    var typeInfo = semanticModel.GetTypeInfo(memberAccessExpressions[i-1]).Type;
                    if (typeInfo != null && typeInfo.Name.Contains("Label"))
                    {
                        Console.WriteLine(memberAccessExpressions[i].GetText());
                        
                    }
                }
            }
            Console.Read();

        }
    }

    //class MemberAccessExpressionRewriter : CSharpSyntaxRewriter
    //{
    //    public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
    //    {
    //        if (!node.IsKind(SyntaxKind.SimpleMemberAccessExpression))
    //        {
    //            return base.VisitMemberAccessExpression(node);
    //        }

    //        var retVal = SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
    //                                                      SyntaxFactory.Literal("NotBaz"));
    //        return retVal;
    //    }
    //}
}
