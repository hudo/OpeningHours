using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace OpeningHours.Tests
{
    public class OpeningHourMiddlewareTests
    {
        [SetUp]
        public void Setup()
        {
        }

        private OpeningHoursMiddleware Build(Func<DateTime> now, Action<Settings> configure, Action onNext)
        {
            var settings = new Settings { FromHour = 8, ToHour = 16 };

            configure(settings);

            SystemTime.Now = () => now();

            return new OpeningHoursMiddleware(ctx =>
            {
                onNext();
                return Task.CompletedTask;
            }, settings);
        }

        [Test]
        public async Task Open_during_businessHours()
        {
            var isCalled = false;

            var middleware = Build(() => new DateTime(2020, 1, 1, 9, 0, 0), _ => { }, () => isCalled = true);

            await middleware.InvokeAsync(new DefaultHttpContext());

            Assert.IsTrue(isCalled);
        }

        [Test]
        public async Task Closed_outside_businessHours()
        {
            var isCalled = false;

            var middleware = Build(() => new DateTime(2020, 1, 1, 7, 0, 0), _ => { }, () => isCalled = true);

            var ctx = new DefaultHttpContext();
            await middleware.InvokeAsync(ctx);

            Assert.IsFalse(isCalled);
            Assert.AreEqual(412, ctx.Response.StatusCode);
        }
    }
}