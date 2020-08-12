using Microsoft.AspNetCore.Http;
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

        public async Task InvokeNext(HttpContext context)
        {
            if (SystemTime.Now().Hour >= _settings.FromHour && SystemTime.Now().Hour <= _settings.ToHour)
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
