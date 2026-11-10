using MediatR;
using PSDDDCQRSInPracticeLB.Api.Application.Common.Attributes;

namespace PSDDDCQRSInPracticeLB.Api.Application.Common.Behaviors
{
    public class LoggingDecorator<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ILogger<LoggingDecorator<TRequest, TResponse>> _logger;

        public LoggingDecorator(ILogger<LoggingDecorator<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            bool disabled = Attribute.IsDefined(typeof(TRequest), typeof(DisableLogginAttribute));

            if (disabled)
            {
                return await next();
            }

            var requestName = typeof(TRequest).Name;

            _logger.LogInformation($"Handling {requestName}");

            try
            {
                var response = await next();
                _logger.LogInformation($"Handled {requestName}");
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"Error handling {requestName}");
                throw;
            }
        }
    }
}
