
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PaymentProcessingSystem.Core.Queries;
using PaymentProcessingSystem.Extensions;
using PaymentProcessingSystem.Infrastructure.Persistence;
using PaymentProcessingSystem.Infrastructure.Services;
using System;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var configurationRoot = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{environment}.json", optional: true)
        .AddEnvironmentVariables();

var builder = WebApplication.CreateBuilder(args);

var configuration = configurationRoot.Build();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPaymentQueries, PaymentQueries>();
builder.RegisterType(typeof(PaymentContext)).As(typeof(IEntitiesContext)).InstancePerLifetimeScope();

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

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new ApplicationModule(configuration));
        builder.RegisterModule(new MediatorModule());
    });

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    #region Db Migration 
    try
    {
        var DbProvider = configuration["ConnectionStrings:PaymentDb"];
        switch (DbProvider)
        {
            case "SqlServer":
            default:
                var context = services.GetRequiredService<PaymentContext>();
                context.Database.Migrate();
                break;
        }

    }
    catch (Exception ex)
    {
        var connectionString = configuration["ConnectionStrings:PaymentDb"];
        var exelog = services.GetRequiredService<ILogger<Program>>();
        exelog.LogError(ex, $"An error occurred creating the DB {connectionString}");
    }
    #endregion



}



app.Run();
