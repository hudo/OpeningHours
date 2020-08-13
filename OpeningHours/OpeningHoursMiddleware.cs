using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace OpeningHours
{
    public class OpeningHoursMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Settings _settings;

        public OpeningHoursMiddleware(RequestDelegate next, Settings settings)
        {
            _next = next;
            _settings = settings;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!_settings.ClosedWeekdays.Contains(SystemTime.Now().DayOfWeek) &&
                SystemTime.Now().Hour >= _settings.FromHour 
                && (_settings.FromHour < _settings.ToHour && SystemTime.Now().Hour <= _settings.ToHour) || // from < to, eg 8-16
                   (_settings.FromHour > _settings.ToHour && SystemTime.Now().Hour >= _settings.ToHour ))  // to > from, eg 20-4
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = _settings.StatusCode;
                await context.Response.WriteAsync(_settings.Message);
            }
        }
    }
}
