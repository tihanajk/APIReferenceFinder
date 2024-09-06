using System;

namespace APIReferenceFinder
{
    partial class APIReferenceFinderPluginControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(APIReferenceFinderPluginControl));
            this.ComboboxAPIs = new System.Windows.Forms.ComboBox();
            this.ComboboxSolutions = new System.Windows.Forms.ComboBox();
            this.Solution = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CodeText = new System.Windows.Forms.RichTextBox();
            this.FlowsGrid = new System.Windows.Forms.DataGridView();
            this.JSGrid = new System.Windows.Forms.DataGridView();
            this.managedCheck = new System.Windows.Forms.CheckBox();
            this.unmanagedCheck = new System.Windows.Forms.CheckBox();
            this.flowsCheck = new System.Windows.Forms.CheckBox();
            this.wrCheck = new System.Windows.Forms.CheckBox();
            this.refCounter = new System.Windows.Forms.Label();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.LoadButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.prevRefBtn = new System.Windows.Forms.Button();
            this.nextRefBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.allApisCheck = new System.Windows.Forms.CheckBox();
            this.wrFetchInfo = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.wrName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.FlowsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.JSGrid)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ComboboxAPIs
            // 
            this.ComboboxAPIs.FormattingEnabled = true;
            this.ComboboxAPIs.Location = new System.Drawing.Point(445, 43);
            this.ComboboxAPIs.Name = "ComboboxAPIs";
            this.ComboboxAPIs.Size = new System.Drawing.Size(291, 21);
            this.ComboboxAPIs.TabIndex = 1;
            this.ComboboxAPIs.SelectedIndexChanged += new System.EventHandler(this.OnAPISelected);
            // 
            // ComboboxSolutions
            // 
            this.ComboboxSolutions.FormattingEnabled = true;
            this.ComboboxSolutions.Location = new System.Drawing.Point(9, 45);
            this.ComboboxSolutions.Name = "ComboboxSolutions";
            this.ComboboxSolutions.Size = new System.Drawing.Size(291, 21);
            this.ComboboxSolutions.TabIndex = 8;
            this.ComboboxSolutions.SelectedIndexChanged += new System.EventHandler(this.OnSolutionSelected);
            // 
            // Solution
            // 
            this.Solution.AutoSize = true;
            this.Solution.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Solution.ForeColor = System.Drawing.SystemColors.InfoText;
            this.Solution.Location = new System.Drawing.Point(6, 25);
            this.Solution.Name = "Solution";
            this.Solution.Size = new System.Drawing.Size(95, 13);
            this.Solution.TabIndex = 9;
            this.Solution.Text = "Select solution:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label3.Location = new System.Drawing.Point(442, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Select API:";
            // 
            // CodeText
            // 
            this.CodeText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CodeText.Location = new System.Drawing.Point(494, 47);
            this.CodeText.Name = "CodeText";
            this.CodeText.Size = new System.Drawing.Size(660, 842);
            this.CodeText.TabIndex = 13;
            this.CodeText.Text = "";
            // 
            // FlowsGrid
            // 
            this.FlowsGrid.AllowUserToAddRows = false;
            this.FlowsGrid.AllowUserToDeleteRows = false;
            this.FlowsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FlowsGrid.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.FlowsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.InfoText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.FlowsGrid.DefaultCellStyle = dataGridViewCellStyle5;
            this.FlowsGrid.Location = new System.Drawing.Point(6, 19);
            this.FlowsGrid.Name = "FlowsGrid";
            this.FlowsGrid.RowHeadersWidth = 72;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            this.FlowsGrid.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.FlowsGrid.RowTemplate.Height = 31;
            this.FlowsGrid.Size = new System.Drawing.Size(482, 870);
            this.FlowsGrid.TabIndex = 14;
            // 
            // JSGrid
            // 
            this.JSGrid.AllowUserToAddRows = false;
            this.JSGrid.AllowUserToDeleteRows = false;
            this.JSGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.JSGrid.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.JSGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.InfoText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.JSGrid.DefaultCellStyle = dataGridViewCellStyle7;
            this.JSGrid.Location = new System.Drawing.Point(6, 19);
            this.JSGrid.Name = "JSGrid";
            this.JSGrid.RowHeadersWidth = 72;
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            this.JSGrid.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.JSGrid.RowTemplate.Height = 31;
            this.JSGrid.Size = new System.Drawing.Size(482, 870);
            this.JSGrid.TabIndex = 15;
            // 
            // managedCheck
            // 
            this.managedCheck.AutoSize = true;
            this.managedCheck.Location = new System.Drawing.Point(331, 29);
            this.managedCheck.Name = "managedCheck";
            this.managedCheck.Size = new System.Drawing.Size(71, 17);
            this.managedCheck.TabIndex = 16;
            this.managedCheck.Text = "Managed";
            this.managedCheck.UseVisualStyleBackColor = true;
            // 
            // unmanagedCheck
            // 
            this.unmanagedCheck.AutoSize = true;
            this.unmanagedCheck.Checked = true;
            this.unmanagedCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.unmanagedCheck.Location = new System.Drawing.Point(331, 49);
            this.unmanagedCheck.Name = "unmanagedCheck";
            this.unmanagedCheck.Size = new System.Drawing.Size(84, 17);
            this.unmanagedCheck.TabIndex = 17;
            this.unmanagedCheck.Text = "Unmanaged";
            this.unmanagedCheck.UseVisualStyleBackColor = true;
            // 
            // flowsCheck
            // 
            this.flowsCheck.AutoSize = true;
            this.flowsCheck.Checked = true;
            this.flowsCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.flowsCheck.Location = new System.Drawing.Point(767, 29);
            this.flowsCheck.Name = "flowsCheck";
            this.flowsCheck.Size = new System.Drawing.Size(53, 17);
            this.flowsCheck.TabIndex = 18;
            this.flowsCheck.Text = "Flows";
            this.flowsCheck.UseVisualStyleBackColor = true;
            this.flowsCheck.CheckedChanged += new System.EventHandler(this.FlowsCheck_CheckedChanged);
            // 
            // wrCheck
            // 
            this.wrCheck.AutoSize = true;
            this.wrCheck.Location = new System.Drawing.Point(767, 49);
            this.wrCheck.Name = "wrCheck";
            this.wrCheck.Size = new System.Drawing.Size(95, 17);
            this.wrCheck.TabIndex = 19;
            this.wrCheck.Text = "Webresources";
            this.wrCheck.UseVisualStyleBackColor = true;
            this.wrCheck.CheckedChanged += new System.EventHandler(this.WrCheck_CheckedChanged);
            // 
            // refCounter
            // 
            this.refCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refCounter.AutoSize = true;
            this.refCounter.Location = new System.Drawing.Point(998, 22);
            this.refCounter.Name = "refCounter";
            this.refCounter.Size = new System.Drawing.Size(66, 13);
            this.refCounter.TabIndex = 20;
            this.refCounter.Text = "0 references";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(116, 32);
            this.toolStripButton3.Text = "Load solutions";
            this.toolStripButton3.Click += new System.EventHandler(this.LoadSolutionsBtnClick);
            // 
            // LoadButton
            // 
            this.LoadButton.Image = ((System.Drawing.Image)(resources.GetObject("LoadButton.Image")));
            this.LoadButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(91, 32);
            this.LoadButton.Text = "Load APIs";
            this.LoadButton.Visible = false;
            this.LoadButton.Click += new System.EventHandler(this.LoadAPIsBtn_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.LoadButton,
            this.toolStripButton4});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1666, 35);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(114, 32);
            this.toolStripButton4.Text = "Open solution";
            this.toolStripButton4.Click += new System.EventHandler(this.OpenSolutionButton);
            // 
            // prevRefBtn
            // 
            this.prevRefBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.prevRefBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.prevRefBtn.Location = new System.Drawing.Point(1070, 16);
            this.prevRefBtn.Name = "prevRefBtn";
            this.prevRefBtn.Size = new System.Drawing.Size(39, 25);
            this.prevRefBtn.TabIndex = 22;
            this.prevRefBtn.Text = "prev";
            this.prevRefBtn.UseVisualStyleBackColor = true;
            this.prevRefBtn.Click += new System.EventHandler(this.PrevRefBtn_Click);
            // 
            // nextRefBtn
            // 
            this.nextRefBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nextRefBtn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.nextRefBtn.Location = new System.Drawing.Point(1115, 16);
            this.nextRefBtn.Name = "nextRefBtn";
            this.nextRefBtn.Size = new System.Drawing.Size(39, 25);
            this.nextRefBtn.TabIndex = 23;
            this.nextRefBtn.Text = "next";
            this.nextRefBtn.UseVisualStyleBackColor = true;
            this.nextRefBtn.Click += new System.EventHandler(this.NextRefBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.allApisCheck);
            this.groupBox1.Controls.Add(this.wrFetchInfo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.ComboboxAPIs);
            this.groupBox1.Controls.Add(this.ComboboxSolutions);
            this.groupBox1.Controls.Add(this.Solution);
            this.groupBox1.Controls.Add(this.wrCheck);
            this.groupBox1.Controls.Add(this.managedCheck);
            this.groupBox1.Controls.Add(this.flowsCheck);
            this.groupBox1.Controls.Add(this.unmanagedCheck);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.groupBox1.Location = new System.Drawing.Point(3, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1660, 109);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "API info";
            // 
            // allApisCheck
            // 
            this.allApisCheck.AutoSize = true;
            this.allApisCheck.Location = new System.Drawing.Point(445, 70);
            this.allApisCheck.Name = "allApisCheck";
            this.allApisCheck.Size = new System.Drawing.Size(80, 17);
            this.allApisCheck.TabIndex = 21;
            this.allApisCheck.Text = "List all APIs";
            this.allApisCheck.UseVisualStyleBackColor = true;
            this.allApisCheck.CheckedChanged += new System.EventHandler(this.AllApisCheck_CheckedChanged);
            // 
            // wrFetchInfo
            // 
            this.wrFetchInfo.AutoSize = true;
            this.wrFetchInfo.ForeColor = System.Drawing.Color.Red;
            this.wrFetchInfo.Location = new System.Drawing.Point(764, 69);
            this.wrFetchInfo.Name = "wrFetchInfo";
            this.wrFetchInfo.Size = new System.Drawing.Size(381, 13);
            this.wrFetchInfo.TabIndex = 20;
            this.wrFetchInfo.Text = "Not recommended to fetch webresources from all solutions - long running action";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.FlowsGrid);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.groupBox2.Location = new System.Drawing.Point(3, 148);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(494, 895);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Flows:";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.wrName);
            this.groupBox3.Controls.Add(this.prevRefBtn);
            this.groupBox3.Controls.Add(this.JSGrid);
            this.groupBox3.Controls.Add(this.refCounter);
            this.groupBox3.Controls.Add(this.CodeText);
            this.groupBox3.Controls.Add(this.nextRefBtn);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.groupBox3.Location = new System.Drawing.Point(503, 148);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1160, 895);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Web resources:";
            // 
            // wrName
            // 
            this.wrName.AutoSize = true;
            this.wrName.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.wrName.Location = new System.Drawing.Point(504, 22);
            this.wrName.Name = "wrName";
            this.wrName.Size = new System.Drawing.Size(0, 13);
            this.wrName.TabIndex = 15;
            // 
            // APIReferenceFinderPluginControl
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip2);
            this.Name = "APIReferenceFinderPluginControl";
            this.PluginIcon = ((System.Drawing.Icon)(resources.GetObject("$this.PluginIcon")));
            this.Size = new System.Drawing.Size(1666, 1046);
            this.Load += new System.EventHandler(this.MyPluginControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FlowsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.JSGrid)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripButton tsbSample;
        private System.Windows.Forms.ToolStripSeparator tssSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ListView flowsList;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox ComboboxAPIs;
        private System.Windows.Forms.ComboBox ComboboxSolutions;
        private System.Windows.Forms.Label Solution;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox CodeText;
        private System.Windows.Forms.DataGridView FlowsGrid;
        private System.Windows.Forms.DataGridView JSGrid;
        private System.Windows.Forms.CheckBox managedCheck;
        private System.Windows.Forms.CheckBox unmanagedCheck;
        private System.Windows.Forms.CheckBox flowsCheck;
        private System.Windows.Forms.CheckBox wrCheck;
        private System.Windows.Forms.Label refCounter;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton LoadButton;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.Button prevRefBtn;
        private System.Windows.Forms.Button nextRefBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label wrName;
        private System.Windows.Forms.Label wrFetchInfo;
        private System.Windows.Forms.CheckBox allApisCheck;
    }
}
