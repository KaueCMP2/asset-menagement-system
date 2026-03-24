using Assets_menagement_system.Contexts;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Carregando o .env
Env.Load();

// Acessando a connection string do .env   
string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

// Conecção com o banco de dados usando a connection string
builder.Services.AddDbContext<AssetMenagementDbContext>(options =>
    options.UseSqlServer(connectionString)
);


// Add services to the container.

builder.Services.AddControllers();
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
