namespace MscrmTools.FormXmlEditor
{
    partial class MyPluginControl
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
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.dpMain = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tssbLoadTables = new System.Windows.Forms.ToolStripSplitButton();
            this.tsmiLoadAllTables = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbSaveForm = new System.Windows.Forms.ToolStripButton();
            this.tsbPublishEntity = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveAndPublish = new System.Windows.Forms.ToolStripButton();
            this.tsbReloadForms = new System.Windows.Forms.ToolStripButton();
            this.toolStripMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssbLoadTables,
            this.toolStripSeparator1,
            this.tsbSaveForm,
            this.tsbPublishEntity,
            this.tsbSaveAndPublish,
            this.toolStripSeparator2,
            this.tsbReloadForms});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripMenu.Size = new System.Drawing.Size(1453, 39);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // dpMain
            // 
            this.dpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dpMain.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.dpMain.Location = new System.Drawing.Point(0, 39);
            this.dpMain.Name = "dpMain";
            this.dpMain.Size = new System.Drawing.Size(1453, 903);
            this.dpMain.TabIndex = 5;
            this.dpMain.ActiveDocumentChanged += new System.EventHandler(this.dpMain_ActiveDocumentChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // tssbLoadTables
            // 
            this.tssbLoadTables.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLoadAllTables});
            this.tssbLoadTables.Image = global::MscrmTools.FormXmlEditor.Properties.Resources.Dataverse_32x32;
            this.tssbLoadTables.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tssbLoadTables.Name = "tssbLoadTables";
            this.tssbLoadTables.Size = new System.Drawing.Size(138, 36);
            this.tssbLoadTables.Text = "Load Tables";
            this.tssbLoadTables.ButtonClick += new System.EventHandler(this.tssbLoadTables_ButtonClick);
            // 
            // tsmiLoadAllTables
            // 
            this.tsmiLoadAllTables.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsmiLoadAllTables.Name = "tsmiLoadAllTables";
            this.tsmiLoadAllTables.Size = new System.Drawing.Size(189, 26);
            this.tsmiLoadAllTables.Text = "Load all tables";
            this.tsmiLoadAllTables.Click += new System.EventHandler(this.tsmiLoadAllTables_Click);
            // 
            // tsbSaveForm
            // 
            this.tsbSaveForm.Enabled = false;
            this.tsbSaveForm.Image = global::MscrmTools.FormXmlEditor.Properties.Resources.diskette32;
            this.tsbSaveForm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveForm.Name = "tsbSaveForm";
            this.tsbSaveForm.Size = new System.Drawing.Size(76, 36);
            this.tsbSaveForm.Text = "Save";
            this.tsbSaveForm.Click += new System.EventHandler(this.tsbSaveForm_Click);
            // 
            // tsbPublishEntity
            // 
            this.tsbPublishEntity.Enabled = false;
            this.tsbPublishEntity.Image = global::MscrmTools.FormXmlEditor.Properties.Resources.publishing32;
            this.tsbPublishEntity.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPublishEntity.Name = "tsbPublishEntity";
            this.tsbPublishEntity.Size = new System.Drawing.Size(130, 36);
            this.tsbPublishEntity.Text = "Publish table";
            this.tsbPublishEntity.Click += new System.EventHandler(this.tsbPublishEntity_Click);
            // 
            // tsbSaveAndPublish
            // 
            this.tsbSaveAndPublish.Enabled = false;
            this.tsbSaveAndPublish.Image = global::MscrmTools.FormXmlEditor.Properties.Resources.rocket32;
            this.tsbSaveAndPublish.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveAndPublish.Name = "tsbSaveAndPublish";
            this.tsbSaveAndPublish.Size = new System.Drawing.Size(157, 36);
            this.tsbSaveAndPublish.Text = "Save and publish";
            this.tsbSaveAndPublish.Click += new System.EventHandler(this.tsbSaveAndPublish_Click);
            // 
            // tsbReloadForms
            // 
            this.tsbReloadForms.Enabled = false;
            this.tsbReloadForms.Image = global::MscrmTools.FormXmlEditor.Properties.Resources.Refresh;
            this.tsbReloadForms.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbReloadForms.Name = "tsbReloadForms";
            this.tsbReloadForms.Size = new System.Drawing.Size(136, 36);
            this.tsbReloadForms.Text = "Reload Forms";
            this.tsbReloadForms.ToolTipText = "Reload Forms for current table";
            this.tsbReloadForms.Click += new System.EventHandler(this.tsbReloadForms_Click);
            // 
            // MyPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dpMain);
            this.Controls.Add(this.toolStripMenu);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MyPluginControl";
            this.Size = new System.Drawing.Size(1453, 942);
            this.Load += new System.EventHandler(this.MyPluginControl_Load);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dpMain;
        private System.Windows.Forms.ToolStripSplitButton tssbLoadTables;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoadAllTables;
        private System.Windows.Forms.ToolStripButton tsbSaveAndPublish;
        private System.Windows.Forms.ToolStripButton tsbPublishEntity;
        private System.Windows.Forms.ToolStripButton tsbSaveForm;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbReloadForms;
    }
}
