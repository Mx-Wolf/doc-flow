using DocFlow.Api;
using DocFlow.Application;
using DocFlow.Infrastructure;

using Microsoft.AspNetCore.Mvc.ApplicationParts;

using AssemblyReference = DocFlow.Api.AssemblyReference;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers()
    .PartManager.ApplicationParts.Add( 
        new AssemblyPart(AssemblyReference.Api));

builder.Services.AddApplicationServices();
builder.Services.AddApiServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
