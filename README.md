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
        c.Holidays = new[] { new DateTime(2020, 1, 1), new DateTime(2020, 3, 17) };
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

## To do

- [x] Basic functionality with builder extension
- [x] Weekends
- [ ] Lunch break
- [x] Holidays
- [x] Bribe request headers
- [ ] Refactor to support minutes
- [ ] Better response headers
- [x] Sample website
- [ ] CI/CD pipeline
- [ ] Deploy sample website to Azure free website
- [ ] Unit tests
- [ ] JSON configuration
- [ ] Support for images as response
- [ ] Take a break from this hard and important work

# Package 

Current version: **alpha** 

Nuget: https://www.nuget.org/packages/OpeningHours

# Credits

Big thanks to [Jasen](https://www.instagram.com/jkekanov/) for inspiration!