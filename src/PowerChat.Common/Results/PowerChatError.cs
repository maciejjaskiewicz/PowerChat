namespace PowerChat.Common.Results
{
    public class PowerChatError
    {
        public string Code { get; }
        public string Description { get; }

        protected PowerChatError(string code, string description)
        {
            Code = code;
            Description = description;
        }

        public static PowerChatError Create(string code, string description)
        {
            return new PowerChatError(code, description);
        }
    }
}
