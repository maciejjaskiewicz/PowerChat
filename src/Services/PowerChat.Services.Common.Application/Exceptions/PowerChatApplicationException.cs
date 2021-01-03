using System;
using PowerChat.Services.Common.Exceptions;

namespace PowerChat.Services.Common.Application.Exceptions
{
    public class PowerChatApplicationException : PowerChatServiceException
    {
        public PowerChatApplicationException(string code) : base(code)
        {
        }

        public PowerChatApplicationException(string message, params object[] args)
            : base(string.Empty, message, args)
        {
        }

        public PowerChatApplicationException(string code, string message, params object[] args)
            : base(null, code, message, args)
        {
        }

        public PowerChatApplicationException(Exception innerException, string message, params object[] args)
            : base(innerException, string.Empty, message, args)
        {
        }

        public PowerChatApplicationException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
        }
    }
}
