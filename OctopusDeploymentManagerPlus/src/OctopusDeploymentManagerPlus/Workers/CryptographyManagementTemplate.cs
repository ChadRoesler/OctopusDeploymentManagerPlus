using System;
using System.IO;
using System.Security.Cryptography;
using OctopusDeploymentManagerPlus.Models.Interfaces;

namespace OctopusDeploymentManagerPlus.Workers
{
    public class CryptographyManagementTemplate : ICryptographyManagement
    {
        public string Decryption(string value)
        {
            var valueAsBytes = Convert.FromBase64String(value);
            var decryptedString = string.Empty;
            using (var aesEncryption = new AesManaged())
            {
                aesEncryption.Key = Convert.FromBase64String("{0}");
                aesEncryption.IV = Convert.FromBase64String("{1}");
                
                var decryptor = aesEncryption.CreateDecryptor(aesEncryption.Key, aesEncryption.IV);

                using (var decryptionMemoryStream = new MemoryStream(valueAsBytes))
                {
                    using (var decryptionCryptoStream = new CryptoStream(decryptionMemoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (var decryptionStreamReader = new StreamReader(decryptionCryptoStream))
                        {
                            decryptedString = decryptionStreamReader.ReadToEnd();
                        }
                    }
                }
            }
            return decryptedString;
        }
    }
}