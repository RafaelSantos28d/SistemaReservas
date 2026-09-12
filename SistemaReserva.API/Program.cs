using Microsoft.AspNetCore.Identity;
using SistemaReserva.API.Middleware;
using SistemaReserva.Domain.Constants;
using SistemaReserva.Domain.Entities;
using SistemaReserva.InfraIoC;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDataProtection();
builder.Services.AddInfrastructureSwagger();
builder.Services.AddControllers()
.AddJsonOptions(options =>
{
 options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reservas v1");
});


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
