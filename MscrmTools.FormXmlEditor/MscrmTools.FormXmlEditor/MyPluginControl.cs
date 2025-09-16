using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using MscrmTools.FormXmlEditor.AppCode;
using MscrmTools.FormXmlEditor.Forms;
using System;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace MscrmTools.FormXmlEditor
{
    public partial class MyPluginControl : PluginControlBase, IShortcutReceiver
    {
        private FormSelectionControl fsForm;
        private bool isCtrlM, isCtrlK;
        private Settings mySettings;
        private TableSelectionControl tsForm;

        public MyPluginControl()
        {
            InitializeComponent();

            SetTheme();

            tsForm = new TableSelectionControl();
            tsForm.TableSelected += TsForm_TableSelected;

            fsForm = new FormSelectionControl();
            fsForm.FormSelected += FsForm_FormSelected;

            tsForm.Show(dpMain, DockState.DockLeft);
            fsForm.Show(dpMain, DockState.DockLeft);
            tsForm.Show(dpMain, DockState.DockLeft);
        }

        #region IShortcutReceiver

        public void ReceiveKeyDownShortcut(KeyEventArgs e)
        {
            var activeContent = dpMain.ActiveContent;

            if (e.Control && e.KeyCode == Keys.S)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                if (activeContent is FormEditorControl bcf)
                {
                    tsbSaveForm_Click(tsbSaveForm, new EventArgs());
                }

                isCtrlM = false;
                isCtrlK = false;
            }
            else if (e.Control && e.KeyCode == Keys.P)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                if (activeContent is FormEditorControl bcf)
                {
                    tsbPublishEntity_Click(tsbPublishEntity, new EventArgs());
                }

                isCtrlM = false;
                isCtrlK = false;
            }
            else if (e.Control && e.KeyCode == Keys.U)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                if (!isCtrlK)
                {
                    tsbSaveAndPublish_Click(tsbSaveAndPublish, new EventArgs());
                }
                else if (activeContent is FormEditorControl cef)
                {
                    cef.UncommentSelectedLines();
                }

                isCtrlM = false;
                isCtrlK = false;
            }
            else if (e.Control && e.KeyCode == Keys.G)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                if (activeContent is FormEditorControl cef)
                {
                    cef.GoToLine();
                }

                isCtrlM = false;
                isCtrlK = false;
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                if (activeContent is FormEditorControl cef)
                {
                    cef.Find(false);
                }
                isCtrlM = false;
                isCtrlK = false;
            }
            else if (e.Control && e.KeyCode == Keys.H)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                if (activeContent is FormEditorControl cef)
                {
                    cef.Find(true);
                }
                isCtrlM = false;
                isCtrlK = false;
            }
            else if (e.Control && e.KeyCode == Keys.M)
            {
                isCtrlM = true;
                isCtrlK = false;
            }
            else if (e.Control && e.KeyCode == Keys.K)
            {
                isCtrlM = false;
                isCtrlK = true;
            }
            else if (e.Control && e.KeyCode == Keys.O)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                if (isCtrlM && activeContent is FormEditorControl cef)
                {
                    cef.ContractFolds();
                }

                isCtrlM = false;
                isCtrlK = false;
            }
            else if (e.Control && e.KeyCode == Keys.C)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                if (activeContent is FormEditorControl cef)
                    if (isCtrlK)
                    {
                        cef.CommentSelectedLines();
                    }
                    else
                    {
                        cef.Copy();
                    }

                isCtrlM = false;
                isCtrlK = false;
            }
            else
            {
                isCtrlM = false;
                isCtrlK = false;
            }
        }

        public void ReceiveKeyPressShortcut(KeyPressEventArgs e)
        {
        }

        public void ReceiveKeyUpShortcut(KeyEventArgs e)
        {
        }

        public void ReceivePreviewKeyDownShortcut(PreviewKeyDownEventArgs e)
        {
        }

        #endregion IShortcutReceiver

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);

            foreach (var doc in dpMain.Documents.OfType<FormEditorControl>())
            {
                doc.Close();
            }

            fsForm.Clear();
            tsForm.Clear();
            tsForm.Show(dpMain, DockState.DockLeft);
        }

        private void disableControls()
        {
            tsbSaveForm.Enabled = false;
            tsbSaveAndPublish.Enabled = false;
            tsbPublishEntity.Enabled = false;
        }

        private void dpMain_ActiveDocumentChanged(object sender, EventArgs e)
        {
            if (!(dpMain.ActiveDocument is FormEditorControl fs))
            {
                tsbSaveForm.Enabled = false;
                tsbSaveAndPublish.Enabled = false;
                tsbPublishEntity.Enabled = false;

                return;
            }
            tsbSaveForm.Enabled = fs.IsDirty;
            tsbSaveAndPublish.Enabled = fs.IsDirty || !fs.Form.IsPublihsed;
            tsbPublishEntity.Enabled = true;
        }

        private void FormCtrl_ContentChanged(object sender, EventArgs e)
        {
            dpMain_ActiveDocumentChanged(dpMain, new EventArgs());
        }

        private void FsForm_FormSelected(object sender, FormSelectionEventArgs e)
        {
            var formCtrl = new FormEditorControl
            {
                Form = e.SelectedForm,
                Text = $"{tsForm.SelectedTable.DisplayName?.UserLocalizedLabel?.Label ?? tsForm.SelectedTable.SchemaName} - {e.SelectedForm.Form.GetAttributeValue<string>("name")}",
            };
            formCtrl.LoadForm();
            formCtrl.ContentChanged += FormCtrl_ContentChanged;

            e.SelectedForm.Document = formCtrl;

            formCtrl.Show(dpMain, DockState.Document);
        }

        private void LoadTables(bool fromSolution = true)
        {
            Guid solutionId = Guid.Empty;
            if (fromSolution)
            {
                using (var dialog = new SolutionPicker(Service))
                {
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                    {
                        solutionId = dialog.SelectedSolution.FirstOrDefault()?.Id ?? Guid.Empty;
                    }
                    else
                    {
                        return;
                    }
                }
            }

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading entities...",
                Work = (bw, e) =>
                {
                    tsForm.LoadTables(Service, solutionId);
                },
                PostWorkCallBack = e =>
                {
                    if (e.Error != null)
                    {
                        string errorMessage = CrmExceptionHelper.GetErrorMessage(e.Error, true);
                        MessageBox.Show(ParentForm, errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    tsForm.ShowTables();
                }
            });
        }

        private void MyPluginControl_Load(object sender, EventArgs e)
        {
        }

        private void SetTheme()
        {
            if (XrmToolBox.Options.Instance.Theme != null)
            {
                switch (XrmToolBox.Options.Instance.Theme)
                {
                    case "Blue theme":
                        {
                            var theme = new VS2015BlueTheme();
                            dpMain.Theme = theme;
                        }
                        break;

                    case "Light theme":
                        {
                            var theme = new VS2015LightTheme();
                            dpMain.Theme = theme;
                        }
                        break;

                    case "Dark theme":
                        {
                            var theme = new VS2015DarkTheme();
                            dpMain.Theme = theme;
                        }
                        break;
                }
            }
        }

        private void tsbPublishEntity_Click(object sender, EventArgs e)
        {
            if (!(dpMain.ActiveDocument is FormEditorControl fs)) return;

            var entity = fs.Form.Form.GetAttributeValue<string>("objecttypecode");
            var emd = tsForm.Tables.FirstOrDefault(t => t.SchemaName.ToLower() == entity);
            disableControls();
            WorkAsync(new WorkAsyncInfo
            {
                Message = $"Publishing table {emd.DisplayName?.UserLocalizedLabel?.Label ?? emd.SchemaName}...",
                Work = (bw, ev) =>
                {
                    fs.Form.Publish(Service);
                },
                PostWorkCallBack = ev =>
                {
                    if (ev.Error != null)
                    {
                        string errorMessage = CrmExceptionHelper.GetErrorMessage(ev.Error, true);
                        MessageBox.Show(ParentForm, errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    fs.Form.SetContainersStyle();
                    dpMain_ActiveDocumentChanged(dpMain, new EventArgs());
                }
            });
        }

        private void tsbSaveAndPublish_Click(object sender, EventArgs e)
        {
            if (!(dpMain.ActiveDocument is FormEditorControl fs)) return;

            var entity = fs.Form.Form.GetAttributeValue<string>("objecttypecode");
            var emd = tsForm.Tables.FirstOrDefault(t => t.SchemaName.ToLower() == entity);

            if (fs.Form.HasChangedSinceLastLoad(Service))
            {
                if (DialogResult.Yes != MessageBox.Show(this, "The form has been updated since it has been loaded. Are you sure you want to continue? This will erase other changes made", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    return;
                }
            }

            disableControls();
            WorkAsync(new WorkAsyncInfo
            {
                Message = $"Saving form {fs.Form.Form.GetAttributeValue<string>("name")}...",
                Work = (bw, ev) =>
                {
                    fs.Form.Save(Service);

                    bw.ReportProgress(0, $"Publishing table {emd.DisplayName?.UserLocalizedLabel?.Label ?? emd.SchemaName}...");

                    fs.Form.Publish(Service);
                },
                ProgressChanged = ev =>
                {
                    SetWorkingMessage(ev.UserState.ToString());
                },
                PostWorkCallBack = ev =>
                {
                    if (ev.Error != null)
                    {
                        string errorMessage = CrmExceptionHelper.GetErrorMessage(ev.Error, true);
                        MessageBox.Show(ParentForm, errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    fs.Form.SetContainersStyle();
                    tsbPublishEntity_Click(this, new EventArgs());
                    dpMain_ActiveDocumentChanged(dpMain, new EventArgs());
                }
            });
        }

        private void tsbSaveForm_Click(object sender, EventArgs e)
        {
            if (!(dpMain.ActiveDocument is FormEditorControl fs)) return;

            var entity = fs.Form.Form.GetAttributeValue<string>("objecttypecode");
            var emd = tsForm.Tables.FirstOrDefault(t => t.SchemaName.ToLower() == entity);

            if (fs.Form.HasChangedSinceLastLoad(Service))
            {
                if (DialogResult.Yes != MessageBox.Show(this, "The form has been updated since it has been loaded. Are you sure you want to continue? This will erase other changes made", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    return;
                }
            }

            disableControls();
            WorkAsync(new WorkAsyncInfo
            {
                Message = $"Saving form {fs.Form.Form.GetAttributeValue<string>("name")}...",
                Work = (bw, ev) =>
                {
                    fs.Form.Save(Service);
                },
                PostWorkCallBack = ev =>
                {
                    if (ev.Error != null)
                    {
                        string errorMessage = CrmExceptionHelper.GetErrorMessage(ev.Error, true);
                        MessageBox.Show(ParentForm, errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    fs.Form.SetContainersStyle();
                    dpMain_ActiveDocumentChanged(dpMain, new EventArgs());
                }
            });
        }

        private void TsForm_TableSelected(object sender, TableSelectionEventArgs e)
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading forms...",
                Work = (bw, ev) =>
                {
                    fsForm.LoadForms(Service, e.SelectedTable.SchemaName);
                },
                PostWorkCallBack = ev =>
                {
                    if (ev.Error != null)
                    {
                        string errorMessage = CrmExceptionHelper.GetErrorMessage(ev.Error, true);
                        MessageBox.Show(ParentForm, errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    fsForm.ShowForms();
                    fsForm.Show(dpMain, DockState.DockLeft);
                }
            });
        }

        private void tsmiLoadAllTables_Click(object sender, EventArgs e)
        {
            ExecuteMethod(LoadTables, false);
        }

        private void tssbLoadTables_ButtonClick(object sender, EventArgs e)
        {
            ExecuteMethod(LoadTables, true);
        }
    }
}