using PasswordManager.app.Exceptions;
using PasswordManager.app.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.app.Steps
{
    internal class StartupStep : IStep
    {
        #region Overrides
        public override string GetDisplayOnSelectOption()
        {
            throw new NotSelectableOptionException(this.GetType().Name);
        }

        protected override string GetDisplayOnActivate()
        {
            return StepTitles.STARTUP_STEP;
        }

        protected override string GetBackStep()
        {
            return "Quit";
        }

        #endregion
    }

    public partial class StepTitles
    {
        public const string STARTUP_STEP = "Welcome to Password Manager!";
    }
}
