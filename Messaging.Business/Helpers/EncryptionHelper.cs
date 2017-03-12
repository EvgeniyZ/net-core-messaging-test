using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace Messaging.Business.Helpers
{
    public class EncryptionHelper : IEncryptionHelper
    {
        /// <summary>
        /// Encrypt Email - AES 256
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string EncryptUserEmail(string email)
        {
            byte[] encrypted;
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(email);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// Encrypt Email In Xml - AES 256
        /// </summary>
        /// <param name="doc"></param>
        public void EncryptEmailInXml(XmlDocument doc)
        {
            XmlElement elementToEncrypt = doc.GetElementsByTagName("Email")[0] as XmlElement;
            if (elementToEncrypt == null)
            {
                throw new XmlException("The specified element was not found");
            }
            EncryptedXml eXml = new EncryptedXml();
            using (RijndaelManaged alg = new RijndaelManaged())
            {
                alg.GenerateKey();
                alg.GenerateIV();
                byte[] encryptedElement = eXml.EncryptData(elementToEncrypt, alg, true);
                EncryptedData edElement = new EncryptedData
                {
                    Type = EncryptedXml.XmlEncElementUrl,
                    EncryptionMethod = new EncryptionMethod(EncryptedXml.XmlEncAES256Url),
                    CipherData = { CipherValue = encryptedElement }
                };
                EncryptedXml.ReplaceElement(elementToEncrypt, edElement, true);
            }
        }
    }
}
