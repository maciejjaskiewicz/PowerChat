using System.Collections.Generic;
using FluentValidation.Results;
using PowerChat.Application.Common.Models;
using PowerChat.Services.Common.Exceptions;

namespace PowerChat.Services.Common.Application.Exceptions
{
    public class PowerChatValidationException : PowerChatServiceException
    {
        public IList<ValidationFailureModel> Failures { get; }

        public PowerChatValidationException() 
            : base("validation","One or more validation failures have occured.")
        {
            Failures = new List<ValidationFailureModel>();    
        }

        public PowerChatValidationException(IEnumerable<ValidationFailure> failures)
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
