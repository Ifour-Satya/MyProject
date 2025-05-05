using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configure the host to use Serilog as the logging provider before building the host.
// This setup reads all the Serilog settings from appsettings.json.

// context: An instance of HostBuilderContext that provides access to the application's configuration and environment, such as environment variables or appsettings.json values.
// services:  The application's IServiceProvider, used for dependency injection.
// configuration: An instance of LoggerConfiguration used to configure Serilog.

//Without Asynchronous

//builder.Host.UseSerilog((context, services, configuration) =>
//{
//    // Reads configuration settings for Serilog from the appsettings.json file or any other configuration source
//    // This enables setting options such as log levels, sinks, and output formats directly from configuration files.
//    configuration.ReadFrom.Configuration(context.Configuration);
//    // Integrate with the dependency injection container, enabling sinks to use other registered services.
//    // This is useful if any of the logging sinks require dependencies such as database or HTTP context.
//    configuration.ReadFrom.Services(services);
//});



//With Asynchronous

// Configure the host to use Serilog as the logging provider.
builder.Host.UseSerilog((context, services, configuration) =>
{
    // Read Serilog settings from the appsettings.json file.
    configuration.ReadFrom.Configuration(context.Configuration);
    // Integrate with dependency injection for any sinks that require additional services.
    configuration.ReadFrom.Services(services);
    // Optionally, enable asynchronous logging for additional sinks defined in code.
    configuration.WriteTo.Async(a =>
    {
        a.Console();  // Wrap Console sink asynchronously.
        a.File("Logs/log-.txt",
               rollingInterval: RollingInterval.Day,
               retainedFileCountLimit: 30);  // Wrap File sink asynchronously.
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
