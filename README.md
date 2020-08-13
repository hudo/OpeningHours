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

    app.UseOpeningHours(9, 18);

    // rest of app ...
}
```