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
using Microsoft.CodeAnalysis.Editing;
using System.Windows.Forms;

namespace CodeChanger
{
    class Program
    {
        static void Main(string[] args)
        {
            UDRefreshFormMain frm = new UDRefreshFormMain();
            Application.Run(frm);
        }
        
    }

    
}