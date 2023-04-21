using Microsoft.Extensions.Configuration;
using PasswordManager.app;
using PasswordManager.app.Common;
using PasswordManager.app.Services;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
// using PasswordManager.app.Services;

namespace PasswordManager
{
    internal class Program
    {
        private static bool _isActive = true;

        private static IConfigurationRoot config;

        static void Main(string[] args)
        {

            config = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            Aggregator.Instance.Subscribe(nameof(QuitApp), QuitApp);
            Aggregator.Instance.Subscribe(nameof(GetConfig), GetConfig);

            StepManager.Instance.Initialize();
            StepManager.Instance.Start();

            HashService hashService = new HashService(config);
            EncryptionService EncryptionService = new EncryptionService(config);

            string userInput;

            while (_isActive && (userInput = Console.ReadLine()) != "-1")
            {
                if (isInt(userInput))
                {
                    StepManager.Instance.Select(int.Parse(userInput));
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.\n");
                    userInput = "-1";
                }
            }
        }

        private static IConfigurationRoot GetConfig()
        {
            return config;
        }

        private static bool isInt(string input)
        {
            try
            {
                int.Parse(input);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static void QuitApp()
        {
            _isActive = false;
        }
    }
}