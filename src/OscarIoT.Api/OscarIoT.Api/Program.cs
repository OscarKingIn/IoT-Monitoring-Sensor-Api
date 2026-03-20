using System.IO.Pipelines;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Security.Authentication.ExtendedProtection;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using OscarIoT.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); // This line adds support for controllers to the application. Controllers are responsible for handling incoming HTTP requests, processing them, and returning appropriate HTTP responses. By calling AddControllers(), we are registering the necessary services and middleware to enable the use of controllers in our application.// This line adds services for API endpoint exploration. It enables the generation of metadata about the API endpoints, which can be used by tools like Swagger to create interactive documentation for the API. By calling AddEndpointsApiExplorer(), we are registering the necessary services to allow for the discovery and documentation of the API endpoints defined in our controllers.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("OscarIoTDb"));

//  builder.Configuration.GetConnectionString("DefaultConnection")) // This line adds the AppDbContext to the dependency injection container and configures it to use SQL Server as the database provider. The connection string is retrieved from the application's configuration settings using the GetConnectionString method, which looks for a connection string named "DefaultConnection". This allows the application to connect to the specified SQL Server database when performing data operations through the AppDbContext.

builder.Services.AddEndpointsApiExplorer(); // This line adds services for API endpoint exploration. It enables the generation of metadata about the API endpoints, which can be used by tools like Swagger to create interactive documentation for the API. By calling AddEndpointsApiExplorer(), we are registering the necessary services to allow for the discovery and documentation of the API endpoints defined in our controllers.
builder.Services.AddSwaggerGen(); //    This line adds Swagger generation services to the application. Swagger is a tool that helps in documenting and testing APIs. By calling AddSwaggerGen(), we are registering the necessary services to generate Swagger documentation for our API endpoints, which can be accessed through a web interface provided by Swagger UI. This allows developers and users to easily understand and interact with the API.


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
            policy.SetIsOriginAllowed(_ => true)
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("AllowAll");

// Serve static files (dashboard) before other middleware
app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run(); // This line starts the web application and begins listening for incoming HTTP requests. The Run() method is a blocking call that keeps the application running until it is shut down. It sets up the necessary infrastructure to handle requests, route them to the appropriate controllers, and send responses back to the clients.

