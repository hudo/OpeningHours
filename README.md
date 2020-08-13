# Opening Hours

Web sites have rights. Let them rest with this .NET middleware which will stop all requests outside of opening hours.   
Approved by [Association of Bureaucrats "Uhljeb"](https://www.in-formality.com/wiki/index.php?title=Uhljeb_(Croatia))


```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseOpeningHours(9, 17);

    // or full configuration:

    app.UseOpeningHours(c => 
    {
        c.FromHour = 9;
        c.ToHour = 17;
        c.Message = "This web works from 9 to 16h every day except Saturday and Sunday";
        c.ClosedWeekdays = new[] { DayOfWeek.Saturday, DayOfWeek.Sunday };
        c.LunchBreakAtHour = 12;
        c.LunchBreakDurationMin = 30;
    });

    // rest of the app ...
}
```