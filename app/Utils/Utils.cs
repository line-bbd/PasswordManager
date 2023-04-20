using System.Security.Cryptography;
using System.Text;

namespace PasswordManager.app.Utils
{
    internal class Utils
    {
        // method to get true file path of db
        public static string getDBPath()
        {
            string systemPath = Path.GetFullPath("database\\");
            systemPath = systemPath.Substring(0, systemPath.IndexOf("bin")) + "database\\PasswordManagerDB.mdf";
            return systemPath;
        }

        public static string maskInput()
        {
            int cursorPos = Console.CursorLeft;

            string password = "";
            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key != ConsoleKey.Enter)
                {
                    password += keyInfo.KeyChar;
                    Console.SetCursorPosition(cursorPos, Console.CursorTop);
                    Console.Write(new string('*', password.Length));
                }
            } while (keyInfo.Key != ConsoleKey.Enter);

            Console.WriteLine('\n');
            return password;
        }

        public static string generateSecurePassword(string service)
        {
            string password = "";
            using (SHA256 hash = SHA256.Create())
            {
                password = GetHash(hash, service).Substring(0, 15);
            }
            return password;
        }

        // Microsoft's GetHash algorithm from https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.hashalgorithm.computehash?view=netcore-3.1
        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}