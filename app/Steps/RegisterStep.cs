using PasswordManager.app.Common;
using PasswordManager.app.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordManager.app.Services;

namespace PasswordManager.app.Steps
{
    internal class RegisterStep : IStep
    {
        #region Ctor

        public RegisterStep() : base()
        {
            _canGoBackTo = false;
        }

        #endregion

        #region Overrides

        public override string GetDisplayOnSelectOption()
        {
            return SelectOptionsDisplay.REGISTER_STEP;
        }

        protected override string GetDisplayOnActivate()
        {
            return StepTitles.REGISTER_STEP;
        }

        protected override void HandleInput()
        {
            RegistrationState registrationState = RegistrationState.ERROR;

            while (registrationState == RegistrationState.ERROR)
            {
                Console.Write("Username: ");
                string? username = Console.ReadLine();
                Console.Write("Password: ");
                string? password = Console.ReadLine();
                Console.Write("Retype Password: ");
                string? retypedPassword = Console.ReadLine();

                Console.Write("\n");

                if (username == null || password == null || retypedPassword == null)
                {
                    Console.WriteLine("Invalid username or password. Please try again.");
                    continue;
                }

                registrationState = AttemptRegistration(username, password, retypedPassword);

                if (registrationState == RegistrationState.ERROR)
                {
                    Console.WriteLine("Invalid username or password. Please try again.");
                }
            }
            Console.WriteLine("Registration successful.");

            // Aggregator.Instance.Raise(AggregatorMethodNames.NAVIGATE_TO_OUTCOME, "successfully regustered", true);
        }

        #endregion

        protected RegistrationState AttemptRegistration(string username, string password, string retypedPassword)
        {
            if (password != retypedPassword)
            {
                Console.WriteLine("Passwords do not match. Please try again.");
                return RegistrationState.ERROR;
            }
            Services.AuthServices authServices = new Services.AuthServices(AuthOperation.REGISTER);
            return (authServices.Execute(username, password)) ? RegistrationState.SUCCESS : RegistrationState.ERROR;

        }
        internal enum RegistrationState
        {
            SUCCESS,
            ERROR
        }
        #region Common

        public partial class StepTitles
        {
            public const string REGISTER_STEP = "Register Page";
        }

        public partial class SelectOptionsDisplay
        {
            public const string REGISTER_STEP = "Register";
        }

        public partial class AggregatorMethodNames
        {
            public const string NAVIGATE_TO_OUTCOME = "NavigateToOutcome";
        }

        #endregion
    }
}
