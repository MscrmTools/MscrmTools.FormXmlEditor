using Microsoft.Xrm.Sdk;
using MscrmTools.FormXmlEditor.Forms;
using System.Drawing;
using System.Web.Services.Description;
using System.Windows.Forms;

namespace MscrmTools.FormXmlEditor.AppCode
{
    public class FormInfo
    {
        private Entity _formEntity;
        private string _updatedFormXml;

        public FormInfo(Entity form)
        {
            _formEntity = form;
        }

        public FormEditorControl Document { get; internal set; }
        public Entity Form => _formEntity;
        public string FormXml => _formEntity.GetAttributeValue<string>("formxml");
        public bool IsDirty => !string.IsNullOrEmpty(UpdatedFormXml) && UpdatedFormXml.BeautifyXml() != _formEntity.GetAttributeValue<string>("formxml").BeautifyXml();
        public bool IsPublihsed { get; set; } = true;
        public ListViewItem ListViewItem { get; internal set; }

        public string UpdatedFormXml
        {
            get { return _updatedFormXml; }
            set
            {
                _updatedFormXml = value;

                SetContainersStyle();
            }
        }

        public bool HasChangedSinceLastLoad(IOrganizationService service)
        {
            var onlineVersion = service.Retrieve("systemform", _formEntity.Id, new Microsoft.Xrm.Sdk.Query.ColumnSet("versionnumber")).GetAttributeValue<long>("versionnumber");
            return onlineVersion > _formEntity.GetAttributeValue<long>("versionnumber");
        }

        public void Publish(IOrganizationService service)
        {
            service.Execute(new Microsoft.Crm.Sdk.Messages.PublishXmlRequest
            {
                ParameterXml = $"<importexportxml><entities><entity>{_formEntity.GetAttributeValue<string>("objecttypecode")}</entity></entities><nodes><node>forms</node></nodes></importexportxml>"
            });

            IsPublihsed = true;
        }

        public void Save(IOrganizationService service)
        {
            if (IsDirty)
            {
                _formEntity["formxml"] = UpdatedFormXml;
                service.Update(_formEntity);

                _formEntity["versionnumber"] = service.Retrieve("systemform", _formEntity.Id, new Microsoft.Xrm.Sdk.Query.ColumnSet("versionnumber"))["versionnumber"];
            }

            IsPublihsed = false;
        }

        public void SetContainersStyle()
        {
            if (ListViewItem != null)
            {
                ListViewItem.ForeColor = IsDirty ? Color.Red : !IsPublihsed ? Color.Blue : Color.Black;
            }

            if (Document != null)
            {
                var text = Document.Text.Replace(" *", "").Replace(" !", "");

                if (IsDirty)
                {
                    Document.Text = text + " *";
                }
                else if (!IsPublihsed)
                {
                    Document.Text = text + " !";
                }
                else
                {
                    Document.Text = text;
                }
            }
        }
    }
}