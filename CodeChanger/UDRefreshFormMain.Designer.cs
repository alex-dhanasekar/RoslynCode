namespace CodeChanger
{
    partial class UDRefreshFormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SolutionPathTextBox = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.FilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.InitiateRefreshButton = new System.Windows.Forms.Button();
            this.LoadCsharpFilesButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressBarLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(314, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(301, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "UD Refresh Automation";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(-2, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Solution Path";
            // 
            // SolutionPathTextBox
            // 
            this.SolutionPathTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SolutionPathTextBox.Location = new System.Drawing.Point(100, 111);
            this.SolutionPathTextBox.Name = "SolutionPathTextBox";
            this.SolutionPathTextBox.Size = new System.Drawing.Size(753, 26);
            this.SolutionPathTextBox.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FilePath,
            this.Select});
            this.dataGridView1.Location = new System.Drawing.Point(12, 243);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(938, 512);
            this.dataGridView1.TabIndex = 3;
            // 
            // FilePath
            // 
            this.FilePath.HeaderText = "FilePath";
            this.FilePath.Name = "FilePath";
            // 
            // Select
            // 
            this.Select.HeaderText = "Select";
            this.Select.Name = "Select";
            // 
            // InitiateRefreshButton
            // 
            this.InitiateRefreshButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InitiateRefreshButton.Location = new System.Drawing.Point(12, 774);
            this.InitiateRefreshButton.Name = "InitiateRefreshButton";
            this.InitiateRefreshButton.Size = new System.Drawing.Size(938, 32);
            this.InitiateRefreshButton.TabIndex = 4;
            this.InitiateRefreshButton.Text = "Initiate Refresh Process";
            this.InitiateRefreshButton.UseVisualStyleBackColor = true;
            this.InitiateRefreshButton.Click += new System.EventHandler(this.InitiateRefreshButton_Click);
            // 
            // LoadCsharpFilesButton
            // 
            this.LoadCsharpFilesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadCsharpFilesButton.Location = new System.Drawing.Point(859, 111);
            this.LoadCsharpFilesButton.Name = "LoadCsharpFilesButton";
            this.LoadCsharpFilesButton.Size = new System.Drawing.Size(100, 26);
            this.LoadCsharpFilesButton.TabIndex = 6;
            this.LoadCsharpFilesButton.Text = "Load C# Files";
            this.LoadCsharpFilesButton.UseVisualStyleBackColor = true;
            this.LoadCsharpFilesButton.Click += new System.EventHandler(this.LoadCsharpFilesButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 197);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(938, 23);
            this.progressBar1.TabIndex = 7;
            // 
            // progressBarLabel
            // 
            this.progressBarLabel.AutoSize = true;
            this.progressBarLabel.Location = new System.Drawing.Point(13, 178);
            this.progressBarLabel.Name = "progressBarLabel";
            this.progressBarLabel.Size = new System.Drawing.Size(0, 13);
            this.progressBarLabel.TabIndex = 8;
            // 
            // UDRefreshFormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 835);
            this.Controls.Add(this.progressBarLabel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.LoadCsharpFilesButton);
            this.Controls.Add(this.InitiateRefreshButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.SolutionPathTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UDRefreshFormMain";
            this.ShowIcon = false;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SolutionPathTextBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilePath;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.Button InitiateRefreshButton;
        private System.Windows.Forms.Button LoadCsharpFilesButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label progressBarLabel;
    }
}