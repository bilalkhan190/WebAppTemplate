using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using WebAppTemplate.Application;
using WebAppTemplate.Application.Services.Abstraction;
using WebAppTemplate.Infrastructure;
using WebAppTemplate.Infrastructure.Authentication.Settings;
using WebAppTemplate.Infrastructure.Implementation;
using WebAppTemplate.Presentation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var presentationAssembly = typeof(WebAppTemplate.Presentation.DependencyInjection).Assembly;
builder.Services
    .AddControllers()
    .AddApplicationPart(presentationAssembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPasswordManager, PasswordManager>();
//adding layers dependencies
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddPresentationLayer();

//adding and configuring my presentation layer configuration
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(presentationAssembly);

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
