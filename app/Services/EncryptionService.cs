using Microsoft.AspNetCore.DataProtection;
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
        public EncryptionService() { }

        public string Encrypt(string input)
        {
            var bArr = Protect(Encoding.ASCII.GetBytes(input));

            return BytesToString(bArr);
        }

        public string Dencrypt(string input)
        {
            var bArr = Unprotect(Encoding.ASCII.GetBytes(input));

            return BytesToString(bArr);
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

    }
}
