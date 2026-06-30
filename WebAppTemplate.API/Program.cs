using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebAppTemplate.Application;
using WebAppTemplate.Application.Services.Abstraction;
using WebAppTemplate.Infrastructure;
using WebAppTemplate.Infrastructure.Implementation;
using WebAppTemplate.Infrastructure.Persistance.Data;
using WebAppTemplate.Infrastructure.Seeding;
using WebAppTemplate.Presentation;
using WebAppTemplate.Presentation.Endpoints.Extensions;
using WebAppTemplate.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

var presentationAssembly = typeof(WebAppTemplate.Presentation.DependencyInjection).Assembly;

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
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration, builder.Environment);
builder.Services.AddPresentationLayer();

builder.Services.AddValidatorsFromAssembly(presentationAssembly);

var app = builder.Build();

if (!app.Environment.IsEnvironment("Testing"))
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await context.Database.MigrateAsync();

    var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
    await seeder.SeedAsync();
}

var runningInContainer =
    string.Equals(
        Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"),
        "true",
        StringComparison.OrdinalIgnoreCase);

if (app.Environment.IsDevelopment() || runningInContainer)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGlobalExceptionMiddleware();

if (!runningInContainer && !app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();
app.MapHealthChecks("/health");
app.Run();

public partial class Program;
