using PasswordManager.app.Common;
using PasswordManager.app.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                string username = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();
                Console.Write("Retype Password: ");
                string retypedPassword = Console.ReadLine();

                Console.Write("\n");

                registrationState = AttemptRegistration(username, password, retypedPassword);
            }
            Console.WriteLine("Registration successful.");

            Aggregator.Instance.Raise(AggregatorMethodNames.NAVIGATE_TO_OUTCOME, "successfully regustered", true);
        }

        protected RegistrationState AttemptRegistration(string username, string password, string retypedPassword)
        {
            if (password != retypedPassword)
            {
                Console.WriteLine("Passwords do not match. Please try again.");
                return RegistrationState.ERROR;
            }
            // add proper validation to return another error if something else is wrong
            return RegistrationState.SUCCESS;

            #endregion
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
