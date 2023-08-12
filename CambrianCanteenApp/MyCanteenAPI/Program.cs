using Microsoft.EntityFrameworkCore;
using MyCanteenAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<MyCanteenDbContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=H}/{action=Index}/{id?}");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
