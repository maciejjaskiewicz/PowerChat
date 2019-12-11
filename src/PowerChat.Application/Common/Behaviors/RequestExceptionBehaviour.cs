using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PowerChat.Application.Common.Behaviors
{
    public class RequestExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public RequestExceptionBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception e)
            {
                var name = typeof(TRequest).Name;
                _logger.LogError(e, $"PowerChat Request Error: {name}. {e.Message}");
                throw e;
            }
        }
    }
}
