using System;
using System.Threading.Tasks;
using System.Xml;
using Messaging.Business.Helpers;
using Messaging.Business.Interfaces;
using Messaging.Data.Interfaces;
using Messaging.Model;

namespace Messaging.Business.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository _userRepository;
        private readonly ISerializerHelper _serializerHelper;
        private readonly IEncryptionHelper _encryptionHelper;

        public UserBusiness(IUserRepository userRepository, ISerializerHelper serializerHelper, IEncryptionHelper encryptionHelper)
        {
            _userRepository = userRepository;
            _serializerHelper = serializerHelper;
            _encryptionHelper = encryptionHelper;
        }

        public async Task<string> GetAllUsersAsync(string responseFormat)
        {
            var users = await _userRepository.GetAllAsync();
            if (responseFormat.Equals(ResponseFormats.Xml, StringComparison.OrdinalIgnoreCase))
            {
                var resultDocument = new XmlDocument();
                XmlElement userRoot = resultDocument.CreateElement("ArrayOfUsers");
                resultDocument.AppendChild(userRoot);
                foreach (var user in users)
                {
                    var userXml = _serializerHelper.SerializeToXml(user);
                    _encryptionHelper.EncryptEmailInXml(userXml);
                    if (userXml.DocumentElement != null)
                    {
                        XmlNode importedDocument = resultDocument.ImportNode(userXml.DocumentElement, true);
                        resultDocument.DocumentElement?.AppendChild(importedDocument);
                    }
                }
                return _serializerHelper.XmlToString(resultDocument);
            }
            foreach (var user in users)
            {
                user.Email = _encryptionHelper.EncryptUserEmail(user.Email);
            }
            return _serializerHelper.JsonSerialize(users);
        }

        public async Task<string> GetUserAsync(int id, string responseFormat)
        {
            var user = await _userRepository.GetAsync(id);
            if (responseFormat.Equals(ResponseFormats.Xml, StringComparison.OrdinalIgnoreCase))
            {
                var userXml = _serializerHelper.SerializeToXml(user);
                _encryptionHelper.EncryptEmailInXml(userXml);

                return _serializerHelper.XmlToString(userXml);
            }
            user.Email = _encryptionHelper.EncryptUserEmail(user.Email);
            return _serializerHelper.JsonSerialize(user);
        }
    }
}
