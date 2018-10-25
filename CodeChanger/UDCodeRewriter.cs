using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChanger
{
    public class UDCodeRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        SyntaxNode _node;
        public UDCodeRewriter(SemanticModel model, SyntaxNode node)
        {
            _semanticModel = model;
            _node = node;
        }

        public override SyntaxNode
            VisitAssignmentExpression(
                AssignmentExpressionSyntax node)
        {


            if (node.Left.GetLastToken().ToString() == "BackColor")
            {
                var memberAccessExpressions = node.DescendantNodes().OfType<MemberAccessExpressionSyntax>().ToList();
                string prevType = string.Empty;
                for (int i = 0; i < memberAccessExpressions.Count; i++)
                {
                    var typeInfo = _semanticModel.GetTypeInfo(memberAccessExpressions[i]).Type;
                    if ((typeInfo != null && (typeInfo.Name == "IntlLabel" || typeInfo.Name == "IntlComboBox"
                    || typeInfo.Name.Contains("IntlCurrencyEditor") || typeInfo.Name.Contains("IntlCheckBox2")
                    || typeInfo.Name.Contains("IntlRadioButton2") || typeInfo.Name.Contains("UltraPictureBox")
                    || typeInfo.Name.Contains("LiveValidationEnforcementSpinner")
                    || typeInfo.Name.Contains("IntlPanel") || typeInfo.Name.Contains("Panel") || typeInfo.Name.Contains("IntlButton")
                    || typeInfo.Name.Contains("TextBox") || typeInfo.Name.Contains("UltraMaskedEdit")
                     ))|| memberAccessExpressions[i].GetLastToken().ToString()== "_pictureBox" || memberAccessExpressions[i].GetLastToken().ToString() == "_idLabel" 
                       || memberAccessExpressions[i].GetLastToken().ToString() == "_nameLabel" 
                       || memberAccessExpressions[i].GetLastToken().ToString() == "_nameTextBox") 
                    {
                        var rightExpression = " = UDThemeStyles.GenericBackColor";
                        if (node.Right.GetText().ToString().Contains("System.Drawing.Color.White"))
                            return node;
                        if (node.Right.GetText().ToString().Contains("System.Drawing.SystemColors.ControlLight"))
                        {
                            var newExpressionNode = SyntaxFactory.ParseExpression(node.GetLeadingTrivia().ToString() + node.Left.ToString() + rightExpression);
                            return node.ReplaceNode(node, newExpressionNode);
                        }

                        if (memberAccessExpressions[i].GetText().ToString().ToUpper().Contains("DIAMOND") || typeInfo.Name.Contains("UltraMaskedEdit"))
                            rightExpression = " = System.Drawing.Color.White";
                        var newExpressionString = node.GetLeadingTrivia().ToString() + node.Left.ToString() + rightExpression;
                        if (typeInfo.Name == "IntlLabel" || typeInfo.Name == "IntlComboBox" 
                           || typeInfo.Name == "IntlButton")
                        {
                            newExpressionString = string.Concat(newExpressionString, ";\n\t\t\t",
                            memberAccessExpressions[i].WithoutLeadingTrivia().GetText().ToString(), ".FlatStyle = FlatStyle.Flat");
                        }
                        if (typeInfo.Name == "IntlComboBox" || typeInfo.Name == "IntlButton")
                        {
                            newExpressionString = string.Concat(newExpressionString, ";\n\t\t\t",
                            memberAccessExpressions[i].WithoutLeadingTrivia().GetText().ToString(), ".ForeColor = System.Drawing.Color.Black");
                        }
                        if (typeInfo.Name.Contains("UltraMaskedEdit"))
                        {
                            newExpressionString = string.Concat(newExpressionString, ";\n\t\t\t",
                            memberAccessExpressions[i].WithoutLeadingTrivia().GetText().ToString(), ".UseFlatMode  =  Infragistics.Win.DefaultableBoolean.True", ";\n\t\t\t",
                            memberAccessExpressions[i].WithoutLeadingTrivia().GetText().ToString(), ".UseOsThemes  =  Infragistics.Win.DefaultableBoolean.False");
                        }
                        if (typeInfo.Name.Contains("IntlCurrencyEditor") || typeInfo.Name.Contains("TextBox") || memberAccessExpressions[i].GetLastToken().ToString() == "_nameTextBox")  
                        {
                            newExpressionString = string.Concat(newExpressionString, ";\n\t\t\t",
                            memberAccessExpressions[i].WithoutLeadingTrivia().GetText().ToString(), ".BorderStyle = BorderStyle.FixedSingle");
                        }
                        if (typeInfo.Name.Contains("IntlCheckBox2"))
                        {
                            newExpressionString = string.Concat(newExpressionString, ";\n\t\t\t",
                            memberAccessExpressions[i].WithoutLeadingTrivia().GetText().ToString(), ".BorderStyle = BorderStyle.None");
                        }
                            if (typeInfo.Name.Contains("IntlRadioButton2") || typeInfo.Name.Contains("Panel") )
                        {
                            newExpressionString = string.Concat(newExpressionString, ";\n\t\t\t",
                            memberAccessExpressions[i].WithoutLeadingTrivia().GetText().ToString(), ".BorderStyle = BorderStyle.None");
                        }
                        var newExpression = SyntaxFactory.ParseExpression(newExpressionString);
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
                        string.Concat(rightExpression, ";\n\t\t\t",
                            memberAccessExpressions[i].WithoutLeadingTrivia().GetText().ToString(), ".UseFlatMode  =  Infragistics.Win.DefaultableBoolean.True");
                        var newExpression = SyntaxFactory.ParseExpression(node.GetLeadingTrivia().ToString() + node.Left.ToString() + rightExpression);
                        return node.ReplaceNode(node, newExpression);
                    }

                }
            }

            if (node.Left.GetLastToken().ToString() == "FlatStyle")
            {
                var memberAccessExpressions = node.DescendantNodes().OfType<MemberAccessExpressionSyntax>().ToList();
                string prevType = string.Empty;
                for (int i = 0; i < memberAccessExpressions.Count; i++)
                {
                    if (memberAccessExpressions[i].GetLastToken().ToString() == "_idLabel"
                     || memberAccessExpressions[i].GetLastToken().ToString() == "_nameLabel")
                    {
                        var rightExpression = ".FlatStyle = FlatStyle.Flat";
                        var newExpression = SyntaxFactory.ParseExpression("\t\t\t"+memberAccessExpressions[i].WithLeadingTrivia().GetText().ToString() + rightExpression);
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
                    if ((typeInfo != null && ((typeInfo.Name == "IntlLabel" && node.Left.GetLastToken().ToString() == "Font") ||
                       (typeInfo.Name.Contains("IntlCheckBox2") && node.Left.GetLastToken().ToString().Contains("LabelFont")) ||
                       (typeInfo.Name.Contains("IntlRadioButton2") && node.Left.GetLastToken().ToString().Contains("LabelFont")) ||
                       (typeInfo.Name.Contains("IntlButton") && node.Left.GetLastToken().ToString() == "Font")
                        )) || memberAccessExpressions[i].GetLastToken().ToString() == "_idLabel"
                       || memberAccessExpressions[i].GetLastToken().ToString() == "_nameLabel")
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

            if (node.Left.GetLastToken().ToString() == "Location")
            {
                var memberAccessExpressions = node.DescendantNodes().OfType<MemberAccessExpressionSyntax>().ToList();
                string prevType = string.Empty;
                for (int i = 0; i < memberAccessExpressions.Count; i++)
                {
                    var typeInfo = _semanticModel.GetTypeInfo(memberAccessExpressions[i]).Type;
                    if ((typeInfo != null && (typeInfo.Name.Contains("CursorAlignedMaskedEdit") || typeInfo.Name.Contains("LiveValidationEnforcementSpinner")))
                        || memberAccessExpressions[i].GetLastToken().ToString() == "_idTextBox")
                    {
                        var cursorAlignedMaskedEditNewExpression = string.Concat(node.GetText().ToString(), ";\n\t\t\t" +
                            memberAccessExpressions[i].WithoutLeadingTrivia().GetText().ToString() + "." + "UseFlatMode = " + "Infragistics.Win.DefaultableBoolean.True");
                        var newExpression = SyntaxFactory.ParseExpression(cursorAlignedMaskedEditNewExpression);
                        return node.ReplaceNode(node, newExpression);
                    }

                }
            }

            if (node.Left.GetLastToken().ToString() == "BorderStyle")
            {
                var memberAccessExpressions = node.DescendantNodes().OfType<MemberAccessExpressionSyntax>().ToList();
                string prevType = string.Empty;
                for (int i = 0; i < memberAccessExpressions.Count; i++)
                {
                    var typeInfo = _semanticModel.GetTypeInfo(memberAccessExpressions[i]).Type;
                    if (typeInfo != null && (typeInfo.Name.Contains("UltraPictureBox")))
                    {
                        var borderStyleRightExpression = "= Infragistics.Win.UIElementBorderStyle.None";
                        var newExpression = SyntaxFactory.ParseExpression(node.GetLeadingTrivia().ToString() + node.Left.ToString() + borderStyleRightExpression);
                        return node.ReplaceNode(node, newExpression);
                    }

                }
            }

            if (node.Left.GetLastToken().ToString() == "BorderShadowColor")
            {
                var memberAccessExpressions = node.DescendantNodes().OfType<MemberAccessExpressionSyntax>().ToList();
                string prevType = string.Empty;
                for (int i = 0; i < memberAccessExpressions.Count; i++)
                {
                    var typeInfo = _semanticModel.GetTypeInfo(memberAccessExpressions[i]).Type;
                    if ((typeInfo != null && (typeInfo.Name.Contains("UltraPictureBox"))) || node.Left.GetText().ToString().Contains("_pictureBox"))
                    {
                        var borderShadowColorRightExpression = "= System.Drawing.Color.Transparent";
                        var newExpression = SyntaxFactory.ParseExpression(node.GetLeadingTrivia().ToString() + node.Left.ToString() + borderShadowColorRightExpression);
                        return node.ReplaceNode(node, newExpression);
                    }

                }
            }
            return node;
        }
        public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
        {

            if (node.GetText().ToString().Contains("ResumeLayout"))
            {
                var expressionsToBeInserted = ";\n\t\t\t this.BackColor = UDThemeStyles.GenericBackColor;\n\t\t\t this.BorderStyle = BorderStyle.None";
                var newExpressionNode = SyntaxFactory.ParseExpression(node.GetLeadingTrivia().ToString() + node.GetText().ToString() + expressionsToBeInserted);
                return node.ReplaceNode(node, newExpressionNode);
            }
            return node;
        }
    }
}
