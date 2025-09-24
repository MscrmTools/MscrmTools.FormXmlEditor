using MscrmTools.FormXmlEditor.AppCode;
using ScintillaNET;
using ScintillaNET_FindReplaceDialog;
using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;

namespace MscrmTools.FormXmlEditor.Forms
{
    public partial class FormEditorControl : DockContent
    {
        private readonly FindReplace findReplace;
        private FormInfo _form;

        public FormEditorControl()
        {
            InitializeComponent();

            ManageCmdKeys();
            SetEditorStyle();

            findReplace = new FindReplace();
            findReplace.Scintilla = scintilla1;
            findReplace.KeyPressed += MyFindReplace_KeyPressed;
        }

        public event EventHandler ContentChanged;

        public FormInfo Form
        {
            get { return _form; }
            set
            {
                _form = value;
                LoadForm();
            }
        }

        public bool IsDirty => Form.IsDirty;

        public string Beautify(XmlDocument doc)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace
            };
            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                doc.Save(writer);
            }
            return sb.ToString();
        }

        public void LoadForm()
        {
            try
            {
                scintilla1.Text = Form.FormXml.BeautifyXml();
                scintilla1.EmptyUndoBuffer();

                scintilla1.Margins[0].Width = scintilla1.Lines.Count.ToString().Length * 12;

                scintilla1.SetSavePoint();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ParentForm, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void genericScintilla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                findReplace.ShowFind();
                e.SuppressKeyPress = true;
            }
            else if (e.Shift && e.KeyCode == Keys.F3)
            {
                findReplace.Window.FindPrevious();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.F3)
            {
                findReplace.Window.FindNext();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.H)
            {
                findReplace.ShowReplace();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.I)
            {
                findReplace.ShowIncrementalSearch();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.G)
            {
                GoTo myGoTo = new GoTo((Scintilla)sender);
                myGoTo.ShowGoToDialog();
                e.SuppressKeyPress = true;
            }
        }

        private void ManageCmdKeys()
        {
            scintilla1.ClearCmdKey(Keys.Control | Keys.S);
            scintilla1.ClearCmdKey(Keys.Control | Keys.U);
            scintilla1.ClearCmdKey(Keys.Control | Keys.F);
            scintilla1.ClearCmdKey(Keys.Control | Keys.G);
            scintilla1.ClearCmdKey(Keys.Control | Keys.H);
            scintilla1.ClearCmdKey(Keys.Control | Keys.K);
            scintilla1.ClearCmdKey(Keys.Control | Keys.C);
            scintilla1.ClearCmdKey(Keys.Control | Keys.U);
            scintilla1.ClearCmdKey(Keys.Control | Keys.M);
            scintilla1.ClearCmdKey(Keys.Control | Keys.O);
            scintilla1.ClearCmdKey(Keys.Control | Keys.P);
            scintilla1.AssignCmdKey(Keys.Shift | Keys.Delete, Command.LineDelete);
        }

        private void MyFindReplace_KeyPressed(object sender, KeyEventArgs e)
        {
            genericScintilla_KeyDown(sender, e);
        }

        private void scintilla_CharAdded(object sender, CharAddedEventArgs e)
        {
            var currentLine = scintilla1.Lines[scintilla1.CurrentLine];
            var currentPosition = scintilla1.CurrentPosition;
            if (currentLine.Text == "\r\n")
            {
                currentLine.Indentation = scintilla1.Lines[scintilla1.CurrentLine - 1].Indentation;
                scintilla1.CurrentPosition = currentPosition + currentLine.Indentation;
                scintilla1.SelectionStart = scintilla1.CurrentPosition;
            }

            Form.UpdatedFormXml = scintilla1.Text;
            ContentChanged?.Invoke(this, new EventArgs());
        }

        private void scintilla_TextChanged(object sender, EventArgs e)
        {
            Form.UpdatedFormXml = scintilla1.Text;
            scintilla1.Margins[0].Width = scintilla1.Lines.Count.ToString().Length * 12;

            ContentChanged?.Invoke(this, new EventArgs());
        }

        private void SetEditorStyle()
        {
            scintilla1.StyleResetDefault();
            scintilla1.Styles[Style.Default].Font = "Consolas";
            scintilla1.Styles[Style.Default].Size = 10;
            scintilla1.StyleClearAll();
            scintilla1.Styles[Style.Xml.Asp].ForeColor = Color.Black;
            scintilla1.Styles[Style.Xml.Asp].BackColor = Color.Yellow;
            scintilla1.Styles[Style.Xml.AspAt].ForeColor = Color.Black;
            scintilla1.Styles[Style.Xml.AspAt].BackColor = Color.Yellow;
            scintilla1.Styles[Style.Xml.AttributeUnknown].ForeColor = Color.Red;
            scintilla1.Styles[Style.Xml.Attribute].ForeColor = Color.Red;
            scintilla1.Styles[Style.Xml.CData].ForeColor = Color.Blue;
            scintilla1.Styles[Style.Xml.Comment].ForeColor = Color.Green;
            scintilla1.Styles[Style.Xml.Default].ForeColor = Color.Black;
            scintilla1.Styles[Style.Xml.DoubleString].ForeColor = Color.Blue;
            scintilla1.Styles[Style.Xml.Other].ForeColor = Color.FromArgb(128, 0, 0);
            scintilla1.Styles[Style.Xml.Script].ForeColor = Color.FromArgb(128, 0, 0);
            scintilla1.Styles[Style.Xml.SingleString].ForeColor = Color.Blue;
            scintilla1.Styles[Style.Xml.Tag].ForeColor = Color.FromArgb(128, 0, 0);
            scintilla1.Styles[Style.Xml.TagEnd].ForeColor = Color.FromArgb(128, 0, 0);
            scintilla1.Styles[Style.Xml.XcComment].ForeColor = Color.Green;
            scintilla1.Styles[Style.Xml.XmlStart].ForeColor = Color.Blue;
            scintilla1.Styles[Style.Xml.XmlEnd].ForeColor = Color.Blue;

            scintilla1.Lexer = Lexer.Xml;

            // Instruct the lexer to calculate folding
            scintilla1.SetProperty("fold", "1");
            scintilla1.SetProperty("fold.compact", "1");
            scintilla1.SetProperty("fold.html", "1");

            // Configure a margin to display folding symbols
            scintilla1.Margins[2].Type = MarginType.Symbol;
            scintilla1.Margins[2].Mask = Marker.MaskFolders;
            scintilla1.Margins[2].Sensitive = true;
            scintilla1.Margins[2].Width = 20;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                scintilla1.Markers[i].SetForeColor(SystemColors.ControlLightLight);
                scintilla1.Markers[i].SetBackColor(SystemColors.ControlDark);
            }

            // Configure folding markers with respective symbols
            scintilla1.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
            scintilla1.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
            scintilla1.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
            scintilla1.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            scintilla1.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
            scintilla1.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            scintilla1.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;
        }

        #region Comment methods

        public void CommentSelectedLines()
        {
            Comment(true);
        }

        public void UncommentSelectedLines()
        {
            Comment(false);
        }

        private void Comment(bool comment)
        {
            int start = scintilla1.SelectionStart;
            int end = scintilla1.SelectionEnd;

            if (comment)
            {
                var indexOfComment = scintilla1.Text.IndexOf("<!--", start, StringComparison.Ordinal);
                if (indexOfComment >= 0 && indexOfComment < end)
                {
                    MessageBox.Show(this, @"Cannot comment a block that already contains a comment", @"Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            var startLine = scintilla1.Lines.First(l => l.Position <= start && l.EndPosition > start);
            var endLine = start == end ? startLine : scintilla1.Lines.First(l => l.Position < end && l.EndPosition >= end);
            DoCommentWithStartAndEndTags(comment, startLine, endLine, "<!--", "-->");
        }

        private void DoCommentWithStartAndEndTags(bool comment, Line startLine, Line endLine, string startString, string endString)
        {
            if (comment)
            {
                scintilla1.InsertText(startLine.Position, startString);
                scintilla1.InsertText(endLine.EndPosition - 1, endString);
            }
            else
            {
                Line tempLine = startLine;
                int i = tempLine.Text.IndexOf(startString, StringComparison.Ordinal);
                while (i < 0)
                {
                    tempLine = scintilla1.Lines[tempLine.Index - 1];
                    if (tempLine.Index == 0)
                    {
                        break;
                    }

                    i = tempLine.Text.IndexOf(startString, StringComparison.Ordinal);
                }

                if (i < 0)
                {
                    tempLine = startLine;
                    while (i < 0)
                    {
                        tempLine = scintilla1.Lines[tempLine.Index + 1];
                        if (tempLine.Index > endLine.Index)
                        {
                            break;
                        }

                        i = tempLine.Text.IndexOf(startString, StringComparison.Ordinal);
                    }
                }

                if (i < 0)
                {
                    return;
                }

                scintilla1.DeleteRange(tempLine.Position + i, startString.Length);

                tempLine = endLine;
                i = tempLine.Text.IndexOf(endString, StringComparison.Ordinal);
                while (i < 0)
                {
                    tempLine = scintilla1.Lines[tempLine.Index + 1];
                    if (tempLine.Index == scintilla1.Lines.Count - 1)
                    {
                        break;
                    }

                    i = tempLine.Text.IndexOf(endString, StringComparison.Ordinal);
                }

                if (i < 0)
                {
                    tempLine = endLine;
                    i = tempLine.Text.IndexOf(endString, StringComparison.Ordinal);

                    while (i < 0)
                    {
                        tempLine = scintilla1.Lines[tempLine.Index - 1];

                        if (tempLine.Index < startLine.Index)
                        {
                            break;
                        }

                        i = tempLine.Text.IndexOf(endString, StringComparison.Ordinal);
                    }
                }

                if (i < 0)
                {
                    MessageBox.Show(this, @"Unable to find Comment end tag", @"Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                scintilla1.DeleteRange(tempLine.Position + i, endString.Length);
            }
        }

        #endregion Comment methods

        #region Folding methods

        public void ContractFolds()
        {
            foreach (Line ln in scintilla1.Lines)
            {
                if (LineIsFoldPoint(ln.Index))
                {
                    ln.FoldLine(FoldAction.Contract);
                }
            }
        }

        private bool LineIsFoldPoint(int linenum)
        {
            return ((scintilla1.Lines[linenum].FoldLevelFlags & FoldLevelFlags.Header) > 0);
        }

        #endregion Folding methods

        #region Actions methods

        public void Copy()
        {
            scintilla1.Copy();
        }

        public void Find(bool replace)
        {
            if (replace)
                findReplace.ShowReplace();
            else
                findReplace.ShowFind();
        }

        public void GoToLine()
        {
            GoTo goTo = new GoTo(findReplace.Scintilla);
            goTo.ShowGoToDialog();
        }

        #endregion Actions methods
    }
}