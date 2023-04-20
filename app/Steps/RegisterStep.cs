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
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            Console.Write("Retype Password: ");
            string retypedPassword = Console.ReadLine();

            Console.Write("\n");

            Aggregator.Instance.Raise(AggregatorMethodNames.NAVIGATE_TO_OUTCOME, "successfully regustered", true);
        }

        #endregion
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
