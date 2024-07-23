﻿using System;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(APIReferenceFinderPluginControl));
            this.ComboboxAPIs = new System.Windows.Forms.ComboBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.LoadButton = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ComboboxSolutions = new System.Windows.Forms.ComboBox();
            this.Solution = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CodeText = new System.Windows.Forms.RichTextBox();
            this.FlowsGrid = new System.Windows.Forms.DataGridView();
            this.JSGrid = new System.Windows.Forms.DataGridView();
            this.managedCheck = new System.Windows.Forms.CheckBox();
            this.unmanagedCheck = new System.Windows.Forms.CheckBox();
            this.flowsCheck = new System.Windows.Forms.CheckBox();
            this.wrCheck = new System.Windows.Forms.CheckBox();
            this.refCounter = new System.Windows.Forms.Label();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FlowsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.JSGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // ComboboxAPIs
            // 
            this.ComboboxAPIs.FormattingEnabled = true;
            this.ComboboxAPIs.Location = new System.Drawing.Point(458, 70);
            this.ComboboxAPIs.Name = "ComboboxAPIs";
            this.ComboboxAPIs.Size = new System.Drawing.Size(291, 32);
            this.ComboboxAPIs.TabIndex = 1;
            this.ComboboxAPIs.SelectedIndexChanged += new System.EventHandler(this.OnAPISelected);
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.LoadButton});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1666, 44);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(181, 38);
            this.toolStripButton3.Text = "Load Solutions";
            this.toolStripButton3.Click += new System.EventHandler(this.LoadSolutionsBtnClick);
            // 
            // LoadButton
            // 
            this.LoadButton.Image = ((System.Drawing.Image)(resources.GetObject("LoadButton.Image")));
            this.LoadButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(137, 38);
            this.LoadButton.Text = "Load APIs";
            this.LoadButton.Click += new System.EventHandler(this.LoadAPIsBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(19, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Flows:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(431, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Webresources:";
            // 
            // ComboboxSolutions
            // 
            this.ComboboxSolutions.FormattingEnabled = true;
            this.ComboboxSolutions.Location = new System.Drawing.Point(22, 72);
            this.ComboboxSolutions.Name = "ComboboxSolutions";
            this.ComboboxSolutions.Size = new System.Drawing.Size(291, 32);
            this.ComboboxSolutions.TabIndex = 8;
            this.ComboboxSolutions.SelectedIndexChanged += new System.EventHandler(this.OnSolutionSelected);
            // 
            // Solution
            // 
            this.Solution.AutoSize = true;
            this.Solution.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Solution.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Solution.Location = new System.Drawing.Point(19, 52);
            this.Solution.Name = "Solution";
            this.Solution.Size = new System.Drawing.Size(98, 25);
            this.Solution.TabIndex = 9;
            this.Solution.Text = "Solution:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(455, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 25);
            this.label3.TabIndex = 10;
            this.label3.Text = "API:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(879, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 25);
            this.label4.TabIndex = 12;
            this.label4.Text = "Code:";
            // 
            // CodeText
            // 
            this.CodeText.Location = new System.Drawing.Point(884, 136);
            this.CodeText.Name = "CodeText";
            this.CodeText.Size = new System.Drawing.Size(536, 578);
            this.CodeText.TabIndex = 13;
            this.CodeText.Text = "";
            // 
            // FlowsGrid
            // 
            this.FlowsGrid.AllowUserToAddRows = false;
            this.FlowsGrid.AllowUserToDeleteRows = false;
            this.FlowsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FlowsGrid.Location = new System.Drawing.Point(24, 136);
            this.FlowsGrid.Name = "FlowsGrid";
            this.FlowsGrid.RowHeadersWidth = 72;
            this.FlowsGrid.RowTemplate.Height = 31;
            this.FlowsGrid.Size = new System.Drawing.Size(406, 578);
            this.FlowsGrid.TabIndex = 14;
            // 
            // JSGrid
            // 
            this.JSGrid.AllowUserToAddRows = false;
            this.JSGrid.AllowUserToDeleteRows = false;
            this.JSGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.JSGrid.Location = new System.Drawing.Point(436, 136);
            this.JSGrid.Name = "JSGrid";
            this.JSGrid.RowHeadersWidth = 72;
            this.JSGrid.RowTemplate.Height = 31;
            this.JSGrid.Size = new System.Drawing.Size(433, 578);
            this.JSGrid.TabIndex = 15;
            // 
            // managedCheck
            // 
            this.managedCheck.AutoSize = true;
            this.managedCheck.Location = new System.Drawing.Point(344, 56);
            this.managedCheck.Name = "managedCheck";
            this.managedCheck.Size = new System.Drawing.Size(121, 29);
            this.managedCheck.TabIndex = 16;
            this.managedCheck.Text = "Managed";
            this.managedCheck.UseVisualStyleBackColor = true;
            // 
            // unmanagedCheck
            // 
            this.unmanagedCheck.AutoSize = true;
            this.unmanagedCheck.Checked = true;
            this.unmanagedCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.unmanagedCheck.Location = new System.Drawing.Point(344, 79);
            this.unmanagedCheck.Name = "unmanagedCheck";
            this.unmanagedCheck.Size = new System.Drawing.Size(145, 29);
            this.unmanagedCheck.TabIndex = 17;
            this.unmanagedCheck.Text = "Unmanaged";
            this.unmanagedCheck.UseVisualStyleBackColor = true;
            // 
            // flowsCheck
            // 
            this.flowsCheck.AutoSize = true;
            this.flowsCheck.Checked = true;
            this.flowsCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.flowsCheck.Location = new System.Drawing.Point(780, 56);
            this.flowsCheck.Name = "flowsCheck";
            this.flowsCheck.Size = new System.Drawing.Size(89, 29);
            this.flowsCheck.TabIndex = 18;
            this.flowsCheck.Text = "Flows";
            this.flowsCheck.UseVisualStyleBackColor = true;
            this.flowsCheck.CheckedChanged += new System.EventHandler(this.FlowsCheck_CheckedChanged);
            // 
            // wrCheck
            // 
            this.wrCheck.AutoSize = true;
            this.wrCheck.Location = new System.Drawing.Point(780, 76);
            this.wrCheck.Name = "wrCheck";
            this.wrCheck.Size = new System.Drawing.Size(166, 29);
            this.wrCheck.TabIndex = 19;
            this.wrCheck.Text = "Webresources";
            this.wrCheck.UseVisualStyleBackColor = true;
            this.wrCheck.CheckedChanged += new System.EventHandler(this.WrCheck_CheckedChanged);
            // 
            // refCounter
            // 
            this.refCounter.AutoSize = true;
            this.refCounter.Location = new System.Drawing.Point(1254, 108);
            this.refCounter.Name = "refCounter";
            this.refCounter.Size = new System.Drawing.Size(120, 25);
            this.refCounter.TabIndex = 20;
            this.refCounter.Text = "0 references";
            this.refCounter.Click += new System.EventHandler(this.refCounter_Click);
            // 
            // MyPluginControl
            // 
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Controls.Add(this.refCounter);
            this.Controls.Add(this.wrCheck);
            this.Controls.Add(this.flowsCheck);
            this.Controls.Add(this.unmanagedCheck);
            this.Controls.Add(this.managedCheck);
            this.Controls.Add(this.JSGrid);
            this.Controls.Add(this.FlowsGrid);
            this.Controls.Add(this.CodeText);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Solution);
            this.Controls.Add(this.ComboboxSolutions);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.ComboboxAPIs);
            this.Name = "MyPluginControl";
            this.Size = new System.Drawing.Size(1666, 1046);
            this.Load += new System.EventHandler(this.MyPluginControl_Load);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FlowsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.JSGrid)).EndInit();
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
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton LoadButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ComboboxSolutions;
        private System.Windows.Forms.Label Solution;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox CodeText;
        private System.Windows.Forms.DataGridView FlowsGrid;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.DataGridView JSGrid;
        private System.Windows.Forms.CheckBox managedCheck;
        private System.Windows.Forms.CheckBox unmanagedCheck;
        private System.Windows.Forms.CheckBox flowsCheck;
        private System.Windows.Forms.CheckBox wrCheck;
        private System.Windows.Forms.Label refCounter;
    }
}