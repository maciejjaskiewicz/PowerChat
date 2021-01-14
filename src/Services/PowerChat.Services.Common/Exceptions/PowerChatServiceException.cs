using System;
using PowerChat.Common.Exceptions;

namespace PowerChat.Services.Common.Exceptions
{
    public class PowerChatServiceException : PowerChatException
    {
        public PowerChatServiceException(string code) : base(code)
        {
        }

        public PowerChatServiceException(string message, params object[] args)
            : base(string.Empty, message, args)
        {
        }

        public PowerChatServiceException(string code, string message, params object[] args)
            : base(null, code, message, args)
        {
        }

        public PowerChatServiceException(Exception innerException, string message, params object[] args)
            : base(innerException, string.Empty, message, args)
        {
        }

        public PowerChatServiceException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
        }
    }
}
