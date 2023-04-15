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
        public override string GetDisplayOnSelectOption()
        {
            return SelectOptionsDisplay.REGISTER_STEP;
        }

        protected override string GetDisplayOnActivate()
        {
            return StepTitles.REGISTER_STEP;
        }
    }

    public partial class StepTitles
    {
        public const string REGISTER_STEP = "Register Page";
    }

    public partial class SelectOptionsDisplay
    {
        public const string REGISTER_STEP = "Register";
    }
}
