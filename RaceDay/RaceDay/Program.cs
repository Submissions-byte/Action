using Microsoft.EntityFrameworkCore;
using RaceDay.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

/**
 * Title: Authentication_API Program.cs source code
 * Author: Yusra Adnan
 * Date: 2021
 * Code version: 1.0
 * Availability: https://github.com/YusraAdnan/Authentication_API/blob/master/Authentication_API/Program.cs
 **/

builder.Services.AddDbContext<RaceDayDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
