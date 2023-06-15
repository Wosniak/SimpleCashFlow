using Carter;
using Serilog;
using SimpleCashFlow.Application;
using SimpleCashFlow.Infrastructure.Auth;
using SimpleCashFlow.Infrastructure.Data;
using SimpleCashFlow.Presentation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCarter();

builder.Services.AddApplication()
    .AddInfrastructureData()
    .AddInfrastructureAuth();

builder.Services.AddHealthChecks();


builder.Host.UseSerilog((context, configuration) => 
        configuration.ReadFrom.Configuration(context.Configuration));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.MapCarter();

app.MapHealthChecks("/hc");

app.Run();

