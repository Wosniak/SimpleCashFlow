using Carter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Serilog;
using SimpleCashFlow.Application;
using SimpleCashFlow.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
        c.SwaggerDoc("v1", new OpenApiInfo()
        {
            Title = "Simpple Cash Flow API",
            Description = "Simple API to control a cash flow.",
            Version = "v1",
            License = new OpenApiLicense()
            {
                Name = "MIT",
                Url = new Uri("https://mit-license.org/")
            },
            Contact = new OpenApiContact()
            {
                Name = "Eduardo Farago Wosniak",
                Email = "eduardo.farago@gmail.com",
                Url = new Uri("https://efarago.com.br")
            }
        });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
         {
             {
                   new OpenApiSecurityScheme
                     {
                         Reference = new OpenApiReference
                         {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                         }
                     },
                     new string[] {}
             }
     });
}
);

builder.Services.AddCarter();

builder.Services.AddApplication()
    .AddInfrastructureData(builder.Configuration);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration.GetSection("IdP:Authority").Value;
    options.Audience = builder.Configuration.GetSection("IdP:Audience").Value;
});
builder.Services.AddAuthorization();

builder.Services.AddHealthChecks();


builder.Host.UseSerilog((context, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "ContainerDev")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.MapCarter();

app.MapHealthChecks("/hc");

app.UseAuthentication();
app.UseAuthorization();



app.Run();

