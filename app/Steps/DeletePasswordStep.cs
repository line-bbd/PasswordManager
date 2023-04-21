using PasswordManager.app.interfaces;
using PasswordManager.app.Services;

namespace PasswordManager.app.Steps
{
    internal class DeletePasswordStep : IStep
    {
        #region Ctor

        public DeletePasswordStep() : base()
        {
            _canGoBackTo = false;
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
            return SelectOptionsDisplay.DELETE_PASSWORD_STEP;
        }

        protected override string GetDisplayOnActivate()
        {
            return StepTitles.DELETE_PASSWORD_STEP
                + "\n\n"
                + "Your existing usernames and passwords per service:\n"
                + "______________________________________\n\n"
                + FetchPasswordsForUser();
        }

        protected override string GetBackStep()
        {
            return "Back";
        }

        protected override void HandleInput()
        {
            bool flag = false;
            while (!flag)
            {
                Console.WriteLine("Enter the name of the service you want to delete the entry for:");
                string? service = Console.ReadLine();

                Console.Write("\nConfirm the username:\n");
                string? username = Console.ReadLine();

                Console.Write("\n");

                if (service == "" || username == "")
                {
                    Console.WriteLine("Invalid service or username. Please try again.\n\n");
                    continue;
                }

                Services.UserServices userServices = new Services.UserServices(Services.CrudOperation.DELETE);
                flag = userServices.Execute(username, null, service);
            }
            Console.WriteLine("Entry deleted successfully.\n");
        }

        #endregion
    }
    #region Common

    public partial class StepTitles
    {
        public const string DELETE_PASSWORD_STEP = "Delete Password Page";
    }

    public partial class SelectOptionsDisplay
    {
        public const string DELETE_PASSWORD_STEP = "Delete Password";
    }

    #endregion
}
