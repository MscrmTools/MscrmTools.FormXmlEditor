namespace MscrmTools.FormXmlEditor.Forms
{
    partial class FormEditorControl
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
            this.scintilla1 = new ScintillaNET.Scintilla();
            this.SuspendLayout();
            // 
            // scintilla1
            // 
            this.scintilla1.AutomaticFold = ((ScintillaNET.AutomaticFold)((ScintillaNET.AutomaticFold.Show | ScintillaNET.AutomaticFold.Click)));
            this.scintilla1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintilla1.Location = new System.Drawing.Point(0, 0);
            this.scintilla1.Margin = new System.Windows.Forms.Padding(2);
            this.scintilla1.Name = "scintilla1";
            this.scintilla1.Size = new System.Drawing.Size(536, 285);
            this.scintilla1.TabIndex = 2;
            this.scintilla1.Text = "scintilla1";
            this.scintilla1.CharAdded += new System.EventHandler<ScintillaNET.CharAddedEventArgs>(this.scintilla_CharAdded);
            this.scintilla1.TextChanged += this.scintilla_TextChanged;
            // 
            // FormEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1322, 956);
            this.Controls.Add(this.scintilla1);
            this.Name = "FormEditorControl";
            this.Text = "FormEditorControl";
            this.ResumeLayout(false);

        }

        #endregion

        private ScintillaNET.Scintilla scintilla1;
    }
}