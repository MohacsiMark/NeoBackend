using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using NeotonFamiliaAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<NeotonFamiliaContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("AlapKapcsolat")!));

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("Frontend");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();
app.Run();
