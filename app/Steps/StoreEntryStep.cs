using PasswordManager.app.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.app.Steps
{
    internal class StoreEntryStep : IStep
    {
        #region Overrides

        public override string GetDisplayOnSelectOption()
        {
            return SelectOptionsDisplay.STORE_STEP;
        }

        protected override string GetDisplayOnActivate()
        {
            return StepTitles.STORE_STEP;
        }

        #endregion
    }

    #region Common

    public partial class StepTitles
    {
        public const string STORE_STEP = "Store Page";
    }

    public partial class SelectOptionsDisplay
    {
        public const string STORE_STEP = "Store Entry";
    }

    #endregion
}
