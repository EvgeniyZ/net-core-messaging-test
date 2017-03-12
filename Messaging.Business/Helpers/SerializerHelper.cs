using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using Newtonsoft.Json;

namespace Messaging.Business.Helpers
{
    public class SerializerHelper : ISerializerHelper
    {
        public XmlDocument SerializeToXml<TEntity>(TEntity entity)
        {
            XmlDocument doc = new XmlDocument();
            XPathNavigator nav = doc.CreateNavigator();
            using (XmlWriter w = nav.AppendChild())
            {
                XmlSerializer ser = new XmlSerializer(typeof (TEntity));
                ser.Serialize(w, entity);
            }

            return doc;
        }

        public string XmlToString(XmlDocument document)
        {
            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
            {
                document.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();

                return stringWriter.GetStringBuilder().ToString();
            }
        }

        public string XmlSerialize<TEntity>(TEntity entity)
        {
            var xmlserializer = new XmlSerializer(typeof(TEntity));
            using (var stringWriter = new StringWriter())
            {
                using (var writer = XmlWriter.Create(stringWriter))
                {
                    xmlserializer.Serialize(writer, entity);

                    return stringWriter.ToString();
                }
            }
        }

        public string JsonSerialize<TEntity>(TEntity entity)
        {
            return JsonConvert.SerializeObject(entity);
        }
    }
}
