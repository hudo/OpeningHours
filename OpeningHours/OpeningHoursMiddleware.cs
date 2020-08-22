using Microsoft.AspNetCore.Http;
using Structures.Time;
using System.Linq;
using System.Threading.Tasks;

namespace OpeningHours
{
    public class OpeningHoursMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Settings _settings;

        private const string _bribeHeader = "OH-Bribe";
        private const string _currentTimeHeader = "OH-ServerTime";
        private const string _opensAtHeader = "OH-OpensAt";

        public OpeningHoursMiddleware(RequestDelegate next, Settings settings)
        {
            _next = next;
            _settings = settings;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (HasBribe(context.Request) || 
                IsNotWeekend() && IsDuringBusinessHours() && IsNotLunchBreak() && IsNotHoliday())
            {
                await _next(context);
            }
            else
            {
                context.Response.Headers.Add(_currentTimeHeader, SystemTime.Now().TimeOfDay.ToString());
                context.Response.Headers.Add(_opensAtHeader, _settings.FromHour.ToString());

                context.Response.StatusCode = _settings.StatusCode;
                await context.Response.WriteAsync(_settings.Message);
            }
        }

        private bool IsNotWeekend() => !_settings.ClosedWeekdays.Contains(SystemTime.Now().DayOfWeek);

        private bool IsDuringBusinessHours() => (Time)SystemTime.Now() >= _settings.FromHour
                && (_settings.FromHour < _settings.ToHour && SystemTime.Now().Hour <= _settings.ToHour) || // from < to, eg 8-16
                   (_settings.FromHour > _settings.ToHour && SystemTime.Now().Hour >= _settings.ToHour);  // to > from, eg 20-4

        private bool IsNotLunchBreak() => true; // todo

        private bool IsNotHoliday() => _settings.Holidays != null
            && !_settings.Holidays.Any(day => day != SystemTime.Now().Date);

        private bool HasBribe(HttpRequest request) => 
            !string.IsNullOrEmpty(_settings.Bribe) 
            && request.Headers.ContainsKey(_bribeHeader)
            && request.Headers[_bribeHeader].Any(x => x == _settings.Bribe);
    }
}
