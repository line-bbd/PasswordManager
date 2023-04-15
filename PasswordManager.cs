using PasswordManager.app;
using PasswordManager.app.Common;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManager
{
    internal class Program
    {
        private static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bbdnet2782\Documents\BBD\Grad_program\C_sharp\PasswordManager\database\passwords.mdf;Integrated Security=True;Connect Timeout=30";

        private static bool _isActive = true;

        static void Main(string[] args)
        {
            Aggregator.Instance.Subscribe(nameof(QuitApp), QuitApp);

            StepManager.Instance.Initialize();
            StepManager.Instance.Start();

            string userInput;

            while (_isActive && (userInput = Console.ReadLine()) != "-1")
            {
                StepManager.Instance.Select(int.Parse(userInput));
            }

            //var mp = getMasterPassword();
            //if (mp.Rows.Count == 0)
            //{
            //    //TODO: Add loop for continuous verification prompting if something is wrong
            //    Console.WriteLine("Hi, please create a master password.\n");
            //    string masterPassword = maskInput();
            //    Console.WriteLine("Please verify your master password.\n");
            //    string verifiedPassword = maskInput();

            //    // TODO: Add basic password security check? Length, special characters, etc.
            //    if (masterPassword.Equals(verifiedPassword))
            //    {
            //        Console.WriteLine("Welcome. Password is: " + masterPassword + '\n');
            //        if (addMasterPassword(masterPassword))
            //        {
            //            Console.WriteLine("Password successfully added.\n");
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("Passwords do not match.\n");
            //    }
            //}
            //startPasswordManager();
        }

        private static void QuitApp()
        {
            _isActive = false;
        }

        private static void startPasswordManager()
        {
            Console.WriteLine("To generate and store a new password for a service, press 'g'.\n");
            Console.WriteLine("To store an existing password for a service, press 's'.\n");
            Console.WriteLine("To retrieve a password for a service, press 'r'.\n");

            switch (Console.ReadLine())
            {
                case "g":
                    generateAndStoreJourney();
                    break;
                case "s":
                    storeExistingPasswordJourney();
                    break;
                case "r":
                    retrievePasswordJourney();
                    break;
                default:
                    Console.WriteLine("\nPlease enter a valid option.\n");
                    break;
            }
        }

        private static void storeExistingPasswordJourney()
        {
            Console.WriteLine("\nPlease enter the password you would like to store:\n");
            string password = maskInput();
            Console.WriteLine("Please enter the name of the service this password relates to:\n");
            string? service = Console.ReadLine();
            // TODO: Add error checking for nulls
            addEntry(password, service);
        }

        private static void generateAndStoreJourney()
        {
            Console.WriteLine("\nPlease enter the name of the service you would like to generate a password for:\n");
            string? service = Console.ReadLine();
            // TODO: Add error checking for nulls
            string password = generateSecurePassword(service);
            addEntry(password, service);
        }

        private static void retrievePasswordJourney()
        {
            Console.WriteLine("\nPlease enter the name of the service you would like to retrieve a password for:\n");
            string? service = Console.ReadLine();

            // TODO: Add error checking for nulls
            retrievePassword(service);
        }

        // add user's master password to db
        private static bool addMasterPassword(string masterPass)
        {
            using (SqlConnection cs = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Insert into masterPass (password) values (@password)", cs);
                    cmd.Parameters.AddWithValue("@password", masterPass);
                    cs.Open();
                    cmd.ExecuteNonQuery();
                    cs.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        // retrieve master password
        private static DataTable getMasterPassword()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection cs = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Select * from masterPass", cs);
                    cs.Open();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dataTable);
                    cs.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error when retrieving master password");
                }
            }
            return dataTable;

        }

        // mask user input
        private static string maskInput()
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

        private static void addEntry(string password, string service)
        {
            using (SqlConnection cs = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Insert into passwordManager (password, service) values (@password, @service)", cs);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@service", service);
                    cs.Open();
                    cmd.ExecuteNonQuery();
                    cs.Close();

                    Console.WriteLine("Password stored for: " + service);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void retrievePassword(string service)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection cs = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Select * from passwordManager where service = '" + service + "'", cs);
                    cs.Open();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dataTable);
                    cs.Close();
                    if (dataTable.Rows.Count != 0)
                    {
                        string? password = dataTable.Rows[0]["Password"].ToString();
                        // TODO: Add error checking for nulls
                        Console.WriteLine("Password for " + service + " is: " + password);
                    }
                    else
                    {
                        Console.WriteLine("No password exists for the specified service.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error when retrieving password");
                }
            }
        }

        private static string generateSecurePassword(string service)
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