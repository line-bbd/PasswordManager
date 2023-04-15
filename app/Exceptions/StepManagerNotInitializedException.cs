using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.app.Exceptions
{
    internal class StepManagerNotInitializedException : Exception
    {
        public StepManagerNotInitializedException()
            : base(ExceptionMessages.STEP_MANAGER_NOT_INITIALIZED_EXCEPTION)
        {

        }
    }

    public partial class ExceptionMessages
    {
        public const string STEP_MANAGER_NOT_INITIALIZED_EXCEPTION = "Step manager has not been initialized prior to starting the step.";
    }
}
