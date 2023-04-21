using PasswordManager.app.interfaces;
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
                Console.Write("Username:\n");
                string? username = Console.ReadLine();

                Console.Write("\nPassword:\n");
                string? password = Utils.Utils.maskInput();

                Console.Write("\nConfirm password:\n");
                string? retypedPassword = Utils.Utils.maskInput();

                Console.Write("\n");

                if (password != retypedPassword)
                {
                    Console.WriteLine("Passwords do not match. Please try again.\n");
                    continue;
                }

                if (username == "" || password == "" || retypedPassword == "")
                {
                    Console.WriteLine("Invalid username or password. Please try again.\n");
                    continue;
                }
                registrationState = AttemptRegistration(username, password, retypedPassword);

                if (registrationState == RegistrationState.ERROR)
                {
                    Console.WriteLine("Invalid username or password. Please try again.\n");
                }
            }
            Console.WriteLine("Registration successful.\n");
        }

        #endregion

        protected RegistrationState AttemptRegistration(string username, string password, string retypedPassword)
        {
            if (password != retypedPassword)
            {
                Console.WriteLine("Passwords do not match. Please try again.\n");
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
