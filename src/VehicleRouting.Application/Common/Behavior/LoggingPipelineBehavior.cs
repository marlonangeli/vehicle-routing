using MediatR;
using Microsoft.Extensions.Logging;

namespace VehicleRouting.Application.Common.Behavior;

public class LoggingPipelineBehavior<TRequest, TResponse>(
        ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).FullName?.Split('.').Last() ?? typeof(TRequest).Name;

        logger.LogInformation("Starting request {@Request} at {@DateTimeUtc}",
            requestName,
            DateTime.UtcNow);

        try
        {
            var result = await next();

            return result;
        }
        catch (Exception e)
        {
            logger.LogError("Error handling request {@Request} at {@DateTimeUtc} with error {@Error}",
                requestName,
                DateTime.UtcNow,
                e.Message);

            throw;
        }
        finally
        {
            logger.LogInformation("Finished request {@Request} at {@DateTimeUtc}",
                requestName,
                DateTime.UtcNow);
        }
    }
}