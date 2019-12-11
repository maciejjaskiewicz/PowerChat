using System.Collections.Generic;
using PowerChat.Common.Results;

namespace PowerChat.Application.Common.Results
{
    public class ValidationResult : Result
    {
        public IList<string> ValidationFailures { get; }

        protected ValidationResult(bool succeeded, PowerChatError error, IList<string> validationFailures) 
            : base(succeeded, error)
        {
            ValidationFailures = validationFailures;
        }

        public new static ValidationResult Ok()
        {
            return new ValidationResult(true, null, new List<string>());
        }

        public static ValidationResult Fail(IList<string> validationFailures)
        {
            var error = PowerChatError.Create("validation", "One or more validation failures have occured.");

            return new ValidationResult(false, error, validationFailures);
        }
    }
}
