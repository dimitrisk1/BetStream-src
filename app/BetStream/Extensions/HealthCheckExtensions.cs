using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace BetStream.Extensions
{
    public static class HealthCheckExtensions
    {
        public static void MapBetStreamHealthChecks(this WebApplication app)
        {
            app.MapHealthChecks("/health/live", new HealthCheckOptions
            {
                Predicate = registration => registration.Tags.Contains("live")
            });

            app.MapHealthChecks("/health/ready", new HealthCheckOptions
            {
                Predicate = registration => registration.Tags.Contains("ready")
            });
        }
    }
}
