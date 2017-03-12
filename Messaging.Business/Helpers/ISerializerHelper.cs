using System.Xml;

namespace Messaging.Business.Helpers
{
    public interface ISerializerHelper
    {
        XmlDocument SerializeToXml<TEntity>(TEntity entity);
        string XmlToString(XmlDocument document);
        string XmlSerialize<TEntity>(TEntity entity);
        string JsonSerialize<TEntity>(TEntity entity);
    }
}
