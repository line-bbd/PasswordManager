using PasswordManager.app.Exceptions;
using PasswordManager.app.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.app.Steps
{
    internal class FailStep : IStep
    {
        #region Private

        private string message;

        #endregion

        #region Ctor

        public FailStep(string message) : base()
        {
            _canGoBackTo = false;
            this.message = message;
        }

        #endregion

        #region Private

        #endregion

        #region Override

        public override string GetDisplayOnSelectOption()
        {
            throw new NotSelectableOptionException(this.GetType().Name);
        }

        protected override string GetDisplayOnActivate()
        {
            return StepTitles.OUTCOME_STEP
                + "\n"
                + message;
        }

        #endregion
    }
    #region Common

    public partial class StepTitles
    {
        public const string OUTCOME_STEP = "Outcome Page";
    }

    public partial class SelectOptionsDisplay
    {
        public const string COMPLETE_STEP = "success :)";
    }

    #endregion
}
