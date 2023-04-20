using PasswordManager.app.interfaces;
using PasswordManager.app.Services;

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
            Services.UserServices userServices = new UserServices(CrudOperation.RETRIEVE);
            return userServices.RetrieveAll();
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
                + "Your existing usernames and passwords per service:\n"
                + "______________________________________\n\n"
                + FetchPasswordsForUser();
        }

        protected override string GetBackStep()
        {
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
