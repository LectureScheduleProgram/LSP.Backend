using System.Security.Cryptography;
using System.Text;

namespace LSP.Core.Security
{
    public static class ClaimEncryptionHelper
    {
        private static readonly string Key;
        static ClaimEncryptionHelper()
        {
            Key = "OGRsNRXn3spl7tcEQhJ4zfaUSYwrrS8N";
        }

        public static string EncryptedData(string data)
        {
            var iv = new byte[16];
            byte[] encrypted;

            using (var aes = Aes.Create())
            {
                aes.Key = Encoding.ASCII.GetBytes(Key);
                aes.IV = iv;

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (var streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(data);
                        }

                        encrypted = memoryStream.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(encrypted);
        }

        public static string DecryptedData(string data)
        {
            var iv = new byte[16];
            var buffer = Convert.FromBase64String(data);

            using Aes aes = Aes.Create();
            aes.Key = Encoding.ASCII.GetBytes(Key);
            aes.IV = iv;

            using var memoryStream = new MemoryStream(buffer);
            using var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
            using var streamReader = new StreamReader(cryptoStream);

            var decrypted = streamReader.ReadToEnd();

            return decrypted;
        }
    }
}
