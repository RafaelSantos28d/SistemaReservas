using Microsoft.EntityFrameworkCore;
using SistemaReserva.API.Middleware;
using SistemaReserva.InfraIoC;
using SistemaReserva.Infrastructure.Context;
using SistemaReserva.Infrastructure.Identity;
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
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BancoContext>();

    await db.Database.MigrateAsync();

    await IdentitySeeder.SeedAsync(scope.ServiceProvider);
}
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
