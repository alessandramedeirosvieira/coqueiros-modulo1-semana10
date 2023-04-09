using ExerSemana10.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = "Server= DESKTOP-GV5QH64;Database=Semana;Trusted_Connection=True;TrustServerCertificate=True;";
//Inje��o de Depencencia do Context
builder.Services.AddDbContext<LocacaoContext>(o => o.UseSqlServer(connectionString));

builder.Services.AddDbContext<LocacaoContext>();

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
