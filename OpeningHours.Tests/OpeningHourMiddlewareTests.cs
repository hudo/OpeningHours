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

        [Test]
        public async Task Open_during_businessHours()
        {
            SystemTime.Now = () => new DateTime(2020, 1, 1, 9, 0, 0);

            var settings = new Settings { FromHour = 8, ToHour = 16 };

            var nextCalled = false;

            var middleware = new OpeningHoursMiddleware((ctx) =>
            {
                nextCalled = true;
                return Task.CompletedTask;
            }, settings);


            await middleware.InvokeAsync(new DefaultHttpContext());

            Assert.IsTrue(nextCalled);
        }
    }
}