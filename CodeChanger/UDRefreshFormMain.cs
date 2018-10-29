using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Build;
using Microsoft.CodeAnalysis;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace CodeChanger
{
    public partial class UDRefreshFormMain : Form
    {

        public Project project { get; private set; }
        public MSBuildWorkspace msbws { get; set; }
        public UDRefreshFormMain()
        {
            InitializeComponent();
        }
        protected void LoadSolutionDocuments()
        {
            string path = this.SolutionPathTextBox.Text;
            //MSBuildLocator.RegisterMSBuildPath(@"C:\Program Files (x86)\MSBuild\15.0");
            MSBuildLocator.RegisterDefaults();
            
            msbws = MSBuildWorkspace.Create();
            this.progressBarLabel.Text = "Initiating File load..";
            this.progressBar1.Value = 1;
            var solution = msbws.OpenSolutionAsync(path).Result;
            project = solution.Projects.Where(n => n.Name.Contains("InfoGenesis.UniversalDesktop.UI.Forms")).Single();
            int counter = 0;
            this.progressBar1.Minimum = 0;
            this.progressBar1.Maximum = project.Documents.Count();
            
            foreach (var document in project.Documents)
            {
                if (document.FilePath.EndsWith(".cs"))
                this.dataGridView1.Rows.Add(document.FilePath, false);
                this.progressBar1.Value = ++counter;
                this.progressBarLabel.Text = $"No:of Files Loaded :{ this.progressBar1.Value}/{project.Documents.Count()}";
                
            }
            this.progressBarLabel.Text = "Files Loaded Successfully";

            this.dataGridView1.AutoResizeColumns();
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;


        }

        protected void InitiateRefreshProcess()
        {
            var checkedRows = from DataGridViewRow r in dataGridView1.Rows
                              where Convert.ToBoolean(r.Cells[1].Value) == true
                              select r;
            var mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
            this.progressBarLabel.Text = "";
            this.progressBarLabel.Text = "Initiating Refresh Process..";
            int counter = 0;
            this.progressBar1.Minimum = 0;
            this.progressBar1.Maximum = checkedRows.Count();
            this.progressBar1.Value = counter;
            
            foreach (var row in checkedRows)
            {
                var document =
                project.Documents.Where(n => n.FilePath == row.Cells[0].Value.ToString()).Single();
                var tree = document.GetSyntaxTreeAsync().Result;
                var compilation = CSharpCompilation.Create("MyCompilation",
                    syntaxTrees: new[] { tree }, references: new[] { mscorlib });
                var root = tree.GetRoot();
                this.progressBar1.Value = ++counter;
                var semanticModel = compilation.GetSemanticModel(tree);
                SyntaxNode rootAfterReplacingNodes = new MethodVisitor(semanticModel, root).Visit(root);
                document = document.WithText(rootAfterReplacingNodes.GetText());
                project = document.Project;
                //var result = msbws.TryApplyChanges(newDocumentAfterInsertingNodes.Project.Solution);
            }
            var result = msbws.TryApplyChanges(project.Solution);
            if (this.progressBar1.Value == checkedRows.Count() && result)
                this.progressBarLabel.Text = "UD Refresh Completed successfully";
            else
                this.progressBarLabel.Text = "UD Refresh Failed";

        }

        private void LoadCsharpFilesButton_Click(object sender, EventArgs e)
        {
            this.LoadSolutionDocuments();
        }

        private void InitiateRefreshButton_Click(object sender, EventArgs e)
        {
            this.InitiateRefreshProcess();
        }
    }
}
