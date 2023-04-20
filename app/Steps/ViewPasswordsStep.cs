using PasswordManager.app.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordManager.app.Common;

namespace PasswordManager.app.Steps
{
    internal class ViewPasswordsStep : IStep
    {
        #region Ctor

        public ViewPasswordsStep() : base()
        {
            _canGoBackTo = true;
        }

        #endregion

        #region Private

        private string FetchPasswordsForUser()
        {
            return "no stored passwords :(";
        }

        #endregion

        #region Override

        public override string GetDisplayOnSelectOption()
        {
            return SelectOptionsDisplay.VIEW_PASSWORDS_STEP;
        }

        protected override string GetDisplayOnActivate()
        {
            return StepTitles.VIEW_PASSWORDS_STEP
                + "\n\n"
                + FetchPasswordsForUser();
        }

        protected override string GetBackStep()
        {
            Aggregator.Instance.Raise("logout");
            return "Logout";
        }

        #endregion
    }
    #region Common

    public partial class StepTitles
    {
        public const string VIEW_PASSWORDS_STEP = "Passwords Page";
    }

    public partial class SelectOptionsDisplay
    {
        public const string VIEW_PASSWORDS_STEP = "Continue";
    }

    #endregion
}
