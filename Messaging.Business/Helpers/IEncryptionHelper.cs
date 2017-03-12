using System.Xml;

namespace Messaging.Business.Helpers
{
    public interface IEncryptionHelper
    {
        string EncryptUserEmail(string email);
        void EncryptEmailInXml(XmlDocument doc);
    }
}
