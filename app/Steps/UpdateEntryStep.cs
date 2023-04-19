using PasswordManager.app.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.app.Steps
{
    internal class UpdateEntryStep : IStep
    {
        #region Overrides

        public override string GetDisplayOnSelectOption()
        {
            return SelectOptionsDisplay.UPDATE_ENTRY;
        }

        protected override string GetDisplayOnActivate()
        {
            return StepTitles.UPDATE_ENTRY;
        }

        #endregion
    }

    #region Common

    public partial class StepTitles
    {
        public const string UPDATE_ENTRY = "Update Page";
    }

    public partial class SelectOptionsDisplay
    {
        public const string UPDATE_ENTRY = "Update Entry";
    }

    #endregion
}
