using PasswordManager.app.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.app.Steps
{
    internal class CreatePasswordStep : IStep
    {
        #region Ctor

        public CreatePasswordStep() : base()
        {
            _canGoBackTo = false;
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
            return SelectOptionsDisplay.CREATE_PASSWORD_STEP;
        }

        protected override string GetDisplayOnActivate()
        {
            return StepTitles.CREATE_PASSWORD_STEP
                + "\n\n"
                + FetchPasswordsForUser();
        }

        protected override string GetBackStep()
        {
            return "Back";
        }

        #endregion
    }
    #region Common

    public partial class StepTitles
    {
        public const string CREATE_PASSWORD_STEP = "Create Password Page";
    }

    public partial class SelectOptionsDisplay
    {
        public const string CREATE_PASSWORD_STEP = "Create Password";
    }

    #endregion
}
