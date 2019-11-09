using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace PowerChat.Application.Common.Exceptions
{
    public class PowerChatValidationException : Exception
    {
        public IDictionary<string, string[]> Failures { get; }

        public PowerChatValidationException() 
            : base("One or more validation failures have occured.")
        {
            Failures = new Dictionary<string, string[]>();    
        }

        public PowerChatValidationException(IReadOnlyCollection<ValidationFailure> failures)
            : this()
        {
            var propertyNames = failures
                .Select(x => x.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(x => x.PropertyName == propertyName)
                    .Select(x => x.ErrorMessage)
                    .ToArray();

                Failures.Add(propertyName, propertyFailures);
            }
        }
    }
}
