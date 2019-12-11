using System;

namespace PowerChat.Common.Exceptions
{
    public class PowerChatException : Exception
    {
        public string Code { get; }

        protected PowerChatException()
        {
        }

        public PowerChatException(string code)
        {
            Code = code;
        }

        public PowerChatException(string message, params object[] args)
            : this(string.Empty, message, args)
        {
        }

        public PowerChatException(string code, string message, params object[] args)
            : this(null, code, message, args)
        {
        }

        public PowerChatException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public PowerChatException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}
