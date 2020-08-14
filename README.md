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

    app.UseStaticFiles();

    // before all other app components but after UseStaticFiles

    app.UseOpeningHours(9, 17);

    // or full configuration:

    app.UseOpeningHours(c => 
    {
        c.FromHour = 9;
        c.ToHour = 17;
        c.Message = "This web works from 9 to 17h every day except Saturday and Sunday";
        c.ClosedWeekdays = new[] { DayOfWeek.Saturday, DayOfWeek.Sunday };
        c.LunchBreakAtHour = 12;
        c.LunchBreakDurationMin = 30;
        c.StatusCode = 412;
        c.Bribe = "50$ tip";
    });

    // rest of the app configuration...
}
```

Outside of opening hours all requests will be responded with **status code 412 and message** (all configurable).  

To serve request outside of opening hours provide request header `OH-Bribe` with configured value (if any).   

Each blocked request will return headers:  
- `OH-ServerTime` - current server time  
- `OH-OpensAt` - when will web open its service