using PasswordManager.app.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.app.Services
{
    internal class HashService
    {
        private SHA256 hashAlgorithm;

        public HashService()
        {
            Aggregator.Instance.Subscribe(nameof(GetHash), GetHash);

            hashAlgorithm = SHA256.Create();
        }

        private string GetHash(string input)
        {

            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
