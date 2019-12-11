using PowerChat.Common.Exceptions;

namespace PowerChat.Common.Results
{
    public class Result
    {
        public bool Succeeded { get; }
        public PowerChatError Error { get; }

        protected Result(bool succeeded, PowerChatError error)
        {
            Succeeded = succeeded;
            Error = error;
        }

        public static Result Ok()
        {
            return new Result(true, null);
        }

        public static Result Fail(PowerChatError error)
        {
            return new Result(false, error);
        }

        public virtual void ThrowIfFailed()
        {
            if (Succeeded == false)
            {
                throw new PowerChatException(Error.Code, Error.Description);
            }
        }
    }


    public class Result<TResult> : Result
    {
        public TResult Value { get; }

        protected Result(bool succeeded, PowerChatError error, TResult value)
            : base(succeeded, error)
        {
            Value = value;
        }

        public static Result<TResult> Ok(TResult resultItem)
        {
            return new Result<TResult>(true, null, resultItem);
        }

        public new static Result<TResult> Fail(PowerChatError error)
        {
            return new Result<TResult>(false, error, default);
        }

        public virtual TResult GetValueOrThrow()
        {
            ThrowIfFailed();
            return Value;
        }
    }
}
