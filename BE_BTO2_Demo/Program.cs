using BE_BTO2_Demo.DBContext;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

Env.Load();
var Host = Env.GetString("Host");
var Port = Env.GetString("Port");
var Database = Env.GetString("Database");
var Username = Env.GetString("Username");
var Password = Env.GetString("Password");

var databaseUrl = $"Host={Host};Port={Port};Database={Database};Username={Username};Password={Password}";
builder.Services.AddDbContext<MyDBContext>(options =>
{
    options.UseNpgsql(databaseUrl);
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
