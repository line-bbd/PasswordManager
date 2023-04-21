using Microsoft.Extensions.Configuration;
using PasswordManager.app.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.app.Services
{
    internal class EncryptionService
    {
        private IConfiguration _configuration;
        public EncryptionService(IConfiguration configuration)
        {
            _configuration = configuration;

            Aggregator.Instance.Subscribe(nameof(Encrypt),Encrypt);
            Aggregator.Instance.Subscribe(nameof(Decrypt), Decrypt);
        }

        public string Encrypt(string input)
        {
            return EncryptString(_configuration["PasswordManager:EncryptionKey"], input);
        }

        public string Decrypt(string input)
        {
            return DecryptString(_configuration["PasswordManager:EncryptionKey"], input);
        }

        private static byte[] Protect(byte[] data)
        {
            return ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
        }

        private static byte[] Unprotect(byte[] data)
        {
        
            return ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
         
        }

        private string BytesToString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var c in bytes)
            {
                sb.Append(c);
            }
            return sb.ToString();
        }

        public static string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

    }
}
