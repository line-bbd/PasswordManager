using PasswordManager.app.interfaces;
using PasswordManager.app.Services;

namespace PasswordManager.app.Steps
{
    internal class LoginStep : IStep
    {
        #region Ctor

        public LoginStep() : base()
        {
            _canGoBackTo = false;
        }

        #endregion

        #region Overrides

        public override string GetDisplayOnSelectOption()
        {
            return SelectOptionsDisplay.LOGIN_STEP;
        }

        protected override string GetDisplayOnActivate()
        {
            return StepTitles.LOGIN_STEP;
        }

        protected override void HandleInput()
        {
            LoginState loginState = LoginState.ERROR;

            while (loginState == LoginState.ERROR)
            {
                Console.Write("Username:\n");
                string? username = Console.ReadLine();

                Console.Write("\nPassword:\n");
                string? password = Utils.Utils.maskInput();

                Console.Write("\n");

                if (username == "" || password == "")
                {
                    Console.WriteLine("Invalid username or password. Please try again.\n");
                    continue;
                }
                loginState = AttemptLogin(username, password);
                if (loginState == LoginState.ERROR)
                {
                    Console.WriteLine("Invalid username or password. Please try again.\n");
                }
            }
            Console.WriteLine("Login successful.\n");
        }

        protected LoginState AttemptLogin(string username, string password)
        {
            Services.AuthServices authServices = new Services.AuthServices(AuthOperation.LOGIN);
            return (authServices.Execute(username, password)) ? LoginState.SUCCESS : LoginState.ERROR;
        }

        #endregion
    }

    internal enum LoginState
    {
        SUCCESS,
        ERROR
    }

    #region Common

    public partial class StepTitles
    {
        public const string LOGIN_STEP = "Login Page";
    }

    public partial class SelectOptionsDisplay
    {
        public const string LOGIN_STEP = "Login";
    }

    #endregion
}
