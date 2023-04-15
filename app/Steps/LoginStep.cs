using PasswordManager.app.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.app.Steps
{
    internal class LoginStep : IStep
    {
        #region Overrides

        public override string GetDisplayOnSelectOption()
        {
            return SelectOptionsDisplay.LOGIN_STEP;
        }

        protected override string GetDisplayOnActivate()
        {
            return StepTitles.LOGIN_STEP;
        }

        #endregion
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
