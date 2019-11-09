using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using PowerChat.Application.Common.Exceptions;

namespace PowerChat.Application.Common.Behaviors
{
    public class RequestValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public RequestValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, 
            RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext(request);

            var failures = _validators
                .Select(async v => await v.ValidateAsync(context, cancellationToken))
                .SelectMany(r => r.Result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
            {
                throw new PowerChatValidationException(failures);
            }

            return await next();
        }
    }
}
