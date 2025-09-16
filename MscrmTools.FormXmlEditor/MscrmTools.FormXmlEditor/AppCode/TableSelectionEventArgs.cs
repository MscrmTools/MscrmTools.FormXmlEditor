using Microsoft.Xrm.Sdk.Metadata;
using System;

namespace MscrmTools.FormXmlEditor.AppCode
{
    public class TableSelectionEventArgs : EventArgs
    {
        public EntityMetadata SelectedTable { get; set; }
    }
}