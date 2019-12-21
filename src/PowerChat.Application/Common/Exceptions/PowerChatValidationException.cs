using System.Collections.Generic;
using FluentValidation.Results;
using PowerChat.Application.Common.Models;
using PowerChat.Common.Exceptions;

namespace PowerChat.Application.Common.Exceptions
{
    public class PowerChatValidationException : PowerChatException
    {
        public IList<ValidationFailureModel> Failures { get; }

        public PowerChatValidationException() 
            : base("validation","One or more validation failures have occured.")
        {
            Failures = new List<ValidationFailureModel>();    
        }

        public PowerChatValidationException(IReadOnlyCollection<ValidationFailure> failures)
            : this()
        {
            foreach (var failure in failures)
            {
                Failures.Add(new ValidationFailureModel
                {
                    Property = failure.PropertyName,
                    Code = failure.ErrorCode,
                    Message = failure.ErrorMessage
                });
            }
        }
    }
}
