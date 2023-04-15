using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.app.Exceptions
{
    internal class NotSelectableOptionException : Exception
    {
        public NotSelectableOptionException(String stepName) 
            : base(String.Format(ExceptionMessages.NOT_SELECTABLE_OPTION_EXCEPTION + ": {0}", stepName))
        {

        }
    }

    public partial class ExceptionMessages
    {
        public const string NOT_SELECTABLE_OPTION_EXCEPTION = "Invalid step for selection";
    }
}
