using System.Net.NetworkInformation;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using OscarIoT.Api.Data;

var builder = WebApplication.CreateBuilder(args); // This line initializes a new instance of the WebApplicationBuilder class, which is used to configure and build the web application. The args parameter allows for passing command-line arguments to the application, which can be used for configuration purposes.

builder.Services.AddControllers(); // This line adds support for controllers to the application. Controllers are responsible for handling incoming HTTP requests, processing them, and returning appropriate HTTP responses. By calling AddControllers(), we are registering the necessary services and middleware to enable the use of controllers in our application.// This line adds services for API endpoint exploration. It enables the generation of metadata about the API endpoints, which can be used by tools like Swagger to create interactive documentation for the API. By calling AddEndpointsApiExplorer(), we are registering the necessary services to allow for the discovery and documentation of the API endpoints defined in our controllers.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("OscarIoTDb"));

//  builder.Configuration.GetConnectionString("DefaultConnection")) // This line adds the AppDbContext to the dependency injection container and configures it to use SQL Server as the database provider. The connection string is retrieved from the application's configuration settings using the GetConnectionString method, which looks for a connection string named "DefaultConnection". This allows the application to connect to the specified SQL Server database when performing data operations through the AppDbContext.

builder.Services.AddEndpointsApiExplorer(); // This line adds services for API endpoint exploration. It enables the generation of metadata about the API endpoints, which can be used by tools like Swagger to create interactive documentation for the API. By calling AddEndpointsApiExplorer(), we are registering the necessary services to allow for the discovery and documentation of the API endpoints defined in our controllers.
builder.Services.AddSwaggerGen(); //    This line adds Swagger generation services to the application. Swagger is a tool that helps in documenting and testing APIs. By calling AddSwaggerGen(), we are registering the necessary services to generate Swagger documentation for our API endpoints, which can be accessed through a web interface provided by Swagger UI. This allows developers and users to easily understand and interact with the API.

var app = builder.Build(); // This line builds the web application using the configuration defined in the previous steps. The Build() method compiles the application's services and middleware into a runnable application instance, which can then be configured further and eventually run to start listening for incoming HTTP requests.

if (app.Environment.IsDevelopment()) // This conditional statement checks if the application is running in a development environment. If it is, the code inside the block will be executed. This is typically used to enable features that are useful during development, such as detailed error pages or API documentation, while keeping them disabled in production for security and performance reasons.
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run(); // This line starts the web application and begins listening for incoming HTTP requests. The Run() method is a blocking call that keeps the application running until it is shut down. It sets up the necessary infrastructure to handle requests, route them to the appropriate controllers, and send responses back to the clients.


/*var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}*/
