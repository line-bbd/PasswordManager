﻿using PasswordManager.app.Exceptions;
using PasswordManager.app.interfaces;

namespace PasswordManager.app.Steps
{
    internal class CompleteStep : IStep
    {
        #region Fields

        private string message;

        #endregion

        #region Ctor

        public CompleteStep(string message) : base()
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

    public partial class SelectOptionsDisplay
    {
        public const string FAIL_STEP = "failed :(";
    }

    #endregion
}
