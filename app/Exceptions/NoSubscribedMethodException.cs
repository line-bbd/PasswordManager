using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.app.Exceptions
{
    internal class NoSubscribedMethodException : Exception
    {
        public NoSubscribedMethodException(String methodName)
            : base(String.Format(ExceptionMessages.NO_SUBSCRIBED_METHOD_EXCEPTION + ": {0}", methodName))
        {

        }

        public partial class ExceptionMessages
        {
            public const string NO_SUBSCRIBED_METHOD_EXCEPTION = "No method has been subscribed for this action";
        }
    }
}
