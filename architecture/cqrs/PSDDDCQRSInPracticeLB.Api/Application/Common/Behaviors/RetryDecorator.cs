using MediatR;
using PSDDDCQRSInPracticeLB.Api.Application.Common.Interfaces;

namespace PSDDDCQRSInPracticeLB.Api.Application.Common.Behaviors
{
    public class RetryDecorator<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand<TResponse>
    {
        private readonly ILogger<RetryDecorator<TRequest, TResponse>> _logger;

        public RetryDecorator(ILogger<RetryDecorator<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogInformation($"Starting {requestName} with retry logic");

            var maxRetries = 3;
            var baseDelay = TimeSpan.FromMicroseconds(100);

            for(var attempt = 1; attempt <= maxRetries + 1; attempt++)
            {
                try
                {
                    var response = await next();

                    if (attempt > 1)
                    {
                        _logger.LogInformation($"{requestName} succeeded on attempt {attempt}");
                    }

                    return response;
                }
                catch(Exception ex) when (ShouldRetry(ex, attempt, maxRetries))
                {
                    var delay = TimeSpan.FromMicroseconds(baseDelay.TotalMicroseconds * Math.Pow(2, attempt - 1));

                    _logger.LogWarning(ex, $"{requestName} failed on attempt {attempt}/{maxRetries + 1}. Retrying in {delay.TotalMilliseconds}ms");

                    await Task.Delay(delay, cancellationToken);
                }                
            }

            throw new InvalidOperationException($"Exceeded maximum retry attempts for {requestName}");
        }

        private static bool ShouldRetry(Exception ex, int attempt, int maxRetries)
        {
            if (attempt > maxRetries) return false;

            return ex is TimeoutException or 
                         TaskCanceledException or
                         HttpRequestException;
        }
    }
}
