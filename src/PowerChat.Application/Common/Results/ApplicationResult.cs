using PowerChat.Application.Common.Exceptions;
using PowerChat.Common.Results;

namespace PowerChat.Application.Common.Results
{
    public class ApplicationResult : Result
    {
        protected ApplicationResult(bool succeeded, PowerChatError error) 
            : base(succeeded, error)
        { }

        public new static ApplicationResult Ok()
        {
            return new ApplicationResult(true, null);
        }

        public new static ApplicationResult Fail(PowerChatError error)
        {
            return new ApplicationResult(false, error);
        }

        public override void ThrowIfFailed()
        {
            if (Succeeded == false)
            {
                throw new PowerChatApplicationException(Error.Code, Error.Description);
            }
        }
    }

    public class ApplicationResult<TResult> : ApplicationResult
    {
        public TResult Value { get; }

        protected ApplicationResult(bool succeeded, PowerChatError error, TResult value)
            : base(succeeded, error)
        {
            Value = value;
        }

        public static ApplicationResult<TResult> Ok(TResult resultItem)
        {
            return new ApplicationResult<TResult>(true, null, resultItem);
        }

        public new static ApplicationResult<TResult> Fail(PowerChatError error)
        {
            return new ApplicationResult<TResult>(false, error, default);
        }

        public virtual TResult GetValueOrThrow()
        {
            ThrowIfFailed();
            return Value;
        }
    }
}
