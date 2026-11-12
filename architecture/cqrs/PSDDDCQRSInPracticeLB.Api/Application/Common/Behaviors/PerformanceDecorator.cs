using MediatR;
using PSDDDCQRSInPracticeLB.Api.Application.Common.Interfaces;
using System.Diagnostics;

namespace PSDDDCQRSInPracticeLB.Api.Application.Common.Behaviors
{
    public class PerformanceDecorator<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IQuery<TResponse>
    {
        private readonly ILogger<PerformanceDecorator<TRequest, TResponse>> _logger;
        private readonly Stopwatch _timer;

        public PerformanceDecorator(ILogger<PerformanceDecorator<TRequest, TResponse>> logger)
        {
            _logger = logger;
            _timer = new Stopwatch();
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;

            _timer.Start();

            try
            {
                var response = await next();
                return response;
            }
            finally
            {
                _timer.Stop();

                var elapsedMilliseconds = _timer.ElapsedMilliseconds;

                if (elapsedMilliseconds > 500)
                {
                    _logger.LogWarning($"{requestName} took {elapsedMilliseconds}ms to complete");
                }
                else
                {
                    _logger.LogInformation($"{requestName} completed in {elapsedMilliseconds}ms");
                }
            }
        }
    }
}
