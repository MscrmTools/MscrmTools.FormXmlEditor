using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MscrmTools.FormXmlEditor.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MscrmTools.FormXmlEditor.Forms
{
    public partial class FormSelectionControl : DockContent
    {
        private List<Entity> forms = new List<Entity>();
        private List<ListViewItem> items = new List<ListViewItem>();

        public FormSelectionControl()
        {
            InitializeComponent();
        }

        public event EventHandler<FormSelectionEventArgs> FormSelected;

        public void Clear()
        {
            forms = new List<Entity>();
            items = new List<ListViewItem>();
            lvForms.Items.Clear();
        }

        public void LoadForms(IOrganizationService service, string tableName)
        {
            forms = service.RetrieveMultiple(new QueryExpression("systemform")
            {
                ColumnSet = new ColumnSet("name", "ismanaged", "formxml", "description", "type", "versionnumber"),
                Criteria = new FilterExpression
                {
                    Conditions = {
                        new ConditionExpression("objecttypecode", ConditionOperator.Equal, tableName.ToLower()),
                        new ConditionExpression("type", ConditionOperator.In, new int[]{ 2,7,6})
                    }
                }
            }).Entities.ToList();

            items.Clear();

            foreach (var form in forms)
            {
                var fi = new FormInfo(form);

                var item = new ListViewItem
                {
                    Text = form.GetAttributeValue<string>("name"),
                    Tag = fi,
                    SubItems =
                    {
                        new ListViewItem.ListViewSubItem
                        {
                            Text = form.GetAttributeValue<OptionSetValue>("type")?.Value == 2 ? "Main" :
                            form.GetAttributeValue<OptionSetValue>("type")?.Value == 7 ? "Quick Create" :"Quick View"
                        },
                        new ListViewItem.ListViewSubItem
                        {
                            Text = form.GetAttributeValue<bool>("ismanaged") ? "Yes" : "No"
                        },
                        new ListViewItem.ListViewSubItem
                        {
                            Text = form.GetAttributeValue<string>("description")
                        }
                    }
                };

                fi.ListViewItem = item;

                items.Add(item);
            }
        }

        public void ShowForms()
        {
            lvForms.SelectedIndexChanged -= lvForms_SelectedIndexChanged;
            lvForms.Items.Clear();
            lvForms.Items.AddRange(items.ToArray());
            lvForms.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lvForms.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            lvForms.SelectedIndexChanged += lvForms_SelectedIndexChanged;
        }

        private void lvForms_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvForms.Sorting = lvForms.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            lvForms.ListViewItemSorter = new ListViewItemComparer(e.Column, lvForms.Sorting);
        }

        private void lvForms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvForms.SelectedItems.Count == 0) return;

            FormSelected?.Invoke(this, new FormSelectionEventArgs
            {
                SelectedForm = (FormInfo)lvForms.SelectedItems[0].Tag
            });
        }

        private void txtSearchEntity_TextChanged(object sender, EventArgs e)
        {
            var searchTerm = txtSearchEntity.Text;
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                lvForms.BeginUpdate();
                lvForms.Items.Clear();
                lvForms.Items.AddRange(items.ToArray());
                lvForms.EndUpdate();
            }
            else
            {
                lvForms.BeginUpdate();
                lvForms.Items.Clear();

                var filteredItems = items
                    .Where(i => ((FormInfo)i.Tag).FormXml.IndexOf(searchTerm, StringComparison.InvariantCultureIgnoreCase) >= 0
                    || ((FormInfo)i.Tag).Form.GetAttributeValue<string>("name").IndexOf(searchTerm, StringComparison.InvariantCultureIgnoreCase) >= 0
                    || ((FormInfo)i.Tag).Form.GetAttributeValue<string>("description").IndexOf(searchTerm, StringComparison.InvariantCultureIgnoreCase) >= 0)
                    .ToArray();

                lvForms.Items.AddRange(filteredItems);
                lvForms.EndUpdate();
            }
        }
    }
}