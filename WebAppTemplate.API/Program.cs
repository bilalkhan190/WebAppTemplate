using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebAppTemplate.Application;
using WebAppTemplate.Application.Services.Abstraction;
using WebAppTemplate.Infrastructure;
using WebAppTemplate.Infrastructure.Authentication.Settings;
using WebAppTemplate.Infrastructure.Implementation;
using WebAppTemplate.Infrastructure.Persistance.Data;
using WebAppTemplate.Infrastructure.Seeding;
using WebAppTemplate.Presentation;
using WebAppTemplate.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var presentationAssembly = typeof(WebAppTemplate.Presentation.DependencyInjection).Assembly;
builder.Services
    .AddControllers()
    .AddApplicationPart(presentationAssembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "WebAppTemplate API",
            Version = "v1"
        });

    options.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter JWT Token"
        });

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
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
                Array.Empty<string>()
            }
        });
});
builder.Services.AddScoped<IPasswordManager, PasswordManager>();
//adding layers dependencies
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddPresentationLayer();

//adding and configuring my presentation layer configuration
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(presentationAssembly);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await context.Database.MigrateAsync();

    var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
    await seeder.SeedAsync();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseGlobalExceptionMiddleware();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}


var runningInContainer =
    string.Equals(
        Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"),
        "true",
        StringComparison.OrdinalIgnoreCase);
if (!runningInContainer && !app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");
app.Run();
