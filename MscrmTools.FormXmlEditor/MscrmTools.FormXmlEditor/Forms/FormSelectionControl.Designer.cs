namespace MscrmTools.FormXmlEditor.Forms
{
    partial class FormSelectionControl
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
            this.lvForms = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pnlTop = new System.Windows.Forms.Panel();
            this.txtSearchEntity = new System.Windows.Forms.TextBox();
            this.lblSearchEntity = new System.Windows.Forms.Label();
            this.pnlSearchInfo = new System.Windows.Forms.Panel();
            this.lblSearchInfo = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.pnlSearchInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvForms
            // 
            this.lvForms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4,
            this.columnHeader3});
            this.lvForms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvForms.FullRowSelect = true;
            this.lvForms.HideSelection = false;
            this.lvForms.Location = new System.Drawing.Point(0, 31);
            this.lvForms.Margin = new System.Windows.Forms.Padding(4);
            this.lvForms.Name = "lvForms";
            this.lvForms.Size = new System.Drawing.Size(800, 377);
            this.lvForms.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvForms.TabIndex = 88;
            this.lvForms.UseCompatibleStateImageBehavior = false;
            this.lvForms.View = System.Windows.Forms.View.Details;
            this.lvForms.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvForms_ColumnClick);
            this.lvForms.SelectedIndexChanged += new System.EventHandler(this.lvForms_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 140;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Type";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Is managed";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Description";
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.txtSearchEntity);
            this.pnlTop.Controls.Add(this.lblSearchEntity);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.pnlTop.Size = new System.Drawing.Size(800, 31);
            this.pnlTop.TabIndex = 87;
            // 
            // txtSearchEntity
            // 
            this.txtSearchEntity.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSearchEntity.Location = new System.Drawing.Point(83, 4);
            this.txtSearchEntity.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearchEntity.Name = "txtSearchEntity";
            this.txtSearchEntity.Size = new System.Drawing.Size(717, 22);
            this.txtSearchEntity.TabIndex = 86;
            this.txtSearchEntity.TextChanged += new System.EventHandler(this.txtSearchEntity_TextChanged);
            // 
            // lblSearchEntity
            // 
            this.lblSearchEntity.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSearchEntity.Location = new System.Drawing.Point(0, 4);
            this.lblSearchEntity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearchEntity.Name = "lblSearchEntity";
            this.lblSearchEntity.Size = new System.Drawing.Size(83, 27);
            this.lblSearchEntity.TabIndex = 85;
            this.lblSearchEntity.Text = "Search:";
            this.lblSearchEntity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlSearchInfo
            // 
            this.pnlSearchInfo.BackColor = System.Drawing.SystemColors.Info;
            this.pnlSearchInfo.Controls.Add(this.lblSearchInfo);
            this.pnlSearchInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSearchInfo.Location = new System.Drawing.Point(0, 408);
            this.pnlSearchInfo.Name = "pnlSearchInfo";
            this.pnlSearchInfo.Size = new System.Drawing.Size(800, 42);
            this.pnlSearchInfo.TabIndex = 89;
            // 
            // lblSearchInfo
            // 
            this.lblSearchInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSearchInfo.ForeColor = System.Drawing.SystemColors.InfoText;
            this.lblSearchInfo.Location = new System.Drawing.Point(0, 0);
            this.lblSearchInfo.Name = "lblSearchInfo";
            this.lblSearchInfo.Size = new System.Drawing.Size(800, 42);
            this.lblSearchInfo.TabIndex = 0;
            this.lblSearchInfo.Text = "Search is performed against name, description and formXml of the form.";
            this.lblSearchInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormSelectionControl
            // 
            this.AllowEndUserDocking = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.lvForms);
            this.Controls.Add(this.pnlSearchInfo);
            this.Controls.Add(this.pnlTop);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)));
            this.Name = "FormSelectionControl";
            this.Text = "Forms";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlSearchInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvForms;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.TextBox txtSearchEntity;
        private System.Windows.Forms.Label lblSearchEntity;
        private System.Windows.Forms.Panel pnlSearchInfo;
        private System.Windows.Forms.Label lblSearchInfo;
    }
}