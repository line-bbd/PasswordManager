using PasswordManager.app.Common;
using PasswordManager.app.interfaces;
using PasswordManager.app.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            Console.Write("\n");


        }

        protected loginState AttemptLogin(string username, string password)
        {
            // TODO: attempt login and return either success or error state
            

        }

        #endregion
    }

    internal enum loginState
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
