using System;
using System.Text;
using System.Xml;

namespace MscrmTools.FormXmlEditor.AppCode
{
    public static class Extensions
    {
        public static string BeautifyXml(this string s)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(s);
            }
            catch (Exception error)
            {
                return s;
            }

            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace,
                OmitXmlDeclaration = true
            };
            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                doc.Save(writer);
            }
            return sb.ToString();
        }
    }
}