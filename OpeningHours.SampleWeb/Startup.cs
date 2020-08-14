using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace OpeningHours.SampleWeb
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseOpeningHours(9, 16);

            app.UseOpeningHours(c => 
            {
                c.FromHour = 9;
                c.ToHour = 16;
                c.Message = "This web works from 9 to 16h every day except Saturday and Sunday";
                c.ClosedWeekdays = new[] { DayOfWeek.Saturday, DayOfWeek.Sunday };
                c.LunchBreakAtHour = 12;
                c.LunchBreakDurationMin = 30;
                c.StatusCode = 412;
                c.Bribe = "50$ tip";
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
