using PasswordManager.app.interfaces;

namespace PasswordManager.app.Steps
{
    internal class CreatePasswordStep : IStep
    {
        #region Ctor

        public CreatePasswordStep() : base()
        {
            _canGoBackTo = false;
        }

        #endregion

        #region Override

        public override string GetDisplayOnSelectOption()
        {
            return SelectOptionsDisplay.CREATE_PASSWORD_STEP;
        }

        protected override string GetDisplayOnActivate()
        {
            return StepTitles.CREATE_PASSWORD_STEP;
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
                Console.WriteLine("Enter the name of the service you want to create a password for:");
                string? service = Console.ReadLine();

                Console.Write("\nUsername:\n");
                string? username = Console.ReadLine();

                Console.Write("\nPassword:\n");
                string? password = Utils.Utils.maskInput();

                Console.Write("\nConfirm password:\n");
                string? retypedPassword = Utils.Utils.maskInput();

                Console.Write("\n");

                if (service == "" || username == "" || password == "" || retypedPassword == "")
                {
                    Console.WriteLine("Invalid input. Please try again.\n\n");
                    continue;
                }

                Services.UserServices userServices = new Services.UserServices(Services.CrudOperation.ADD);
                flag = userServices.Execute(username, password, service);
            }
            Console.WriteLine("Password created successfully.\n");
        }
        #endregion
    }
    #region Common


    public partial class StepTitles
    {
        public const string CREATE_PASSWORD_STEP = "Create Password Page";
    }

    public partial class SelectOptionsDisplay
    {
        public const string CREATE_PASSWORD_STEP = "Create Password";
    }

    #endregion
}
