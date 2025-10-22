using Menu_Digital.Repositories.Implementations;
using Menu_Digital.Repositories.Interfaces;
using Menu_Digital.Services.Implementation;
using Menu_Digital.Services.Interfaces;
using MenuDigital.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext < MenuDigitalContext > (dbContextOptions => dbContextOptions.UseSqlite(builder.Configuration["ConnectionStrings:MenuDigitalDBConnectionString"]));
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
