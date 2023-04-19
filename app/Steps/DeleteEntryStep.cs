using PasswordManager.app.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.app.Steps
{
    internal class DeleteEntryStep : IStep
    {
        #region Overrides

        public override string GetDisplayOnSelectOption()
        {
            return SelectOptionsDisplay.DELETE_ENTRY;
        }

        protected override string GetDisplayOnActivate()
        {
            return StepTitles.DELETE_ENTRY;
        }

        #endregion
    }

    #region Common

    public partial class StepTitles
    {
        public const string DELETE_ENTRY = "Delete Page";
    }

    public partial class SelectOptionsDisplay
    {
        public const string DELETE_ENTRY = "Delete Entry";
    }

    #endregion
}
