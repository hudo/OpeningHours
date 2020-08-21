using Microsoft.AspNetCore.Builder;
using System;

namespace OpeningHours
{
    public static class MiddlewareExtensions
    {
        public static void UseOpeningHours(this IApplicationBuilder builder, int fromHour, int toHour)
        {
            SetupMiddleware(builder, new Settings { FromHour = fromHour, ToHour = toHour });
        }

        public static void UseOpeningHours(this IApplicationBuilder builder, Action<Settings> configure)
        {
            var settings = new Settings();

            configure(settings);

            SetupMiddleware(builder, settings);
        }

        private static void SetupMiddleware(IApplicationBuilder builder, Settings settings)
        {
            builder.UseMiddleware<OpeningHoursMiddleware>(settings);
        }
    }
}
