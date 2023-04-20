using PasswordManager.app;
using PasswordManager.app.Common;

namespace PasswordManager
{
    internal class Program
    {
        private static bool _isActive = true;

        static void Main(string[] args)
        {
            Aggregator.Instance.Subscribe(nameof(QuitApp), QuitApp);

            StepManager.Instance.Initialize();
            StepManager.Instance.Start();

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