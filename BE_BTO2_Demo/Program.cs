using Autofac;
using Autofac.Extensions.DependencyInjection;
using BE_BTO2_Demo.Configurations;
using BE_BTO2_Demo.DBContext;
using BE_BTO2_Demo.Middlewares;
using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

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

// Cau hinh AutoFac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(options =>
{
    options.RegisterModule(new AutoFacModule());
});


// Cau hinh tu dong an neu thuoc tinh do là null
builder.Services.Configure<JsonOptions>(options => {
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

//Tat tu dong kiem tra loi ModelState
builder.Services.Configure<ApiBehaviorOptions>(options => {
    options.SuppressModelStateInvalidFilter = true;
});

// Dang ky filter de xu ly loi validation
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFiller>();
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
