using Microsoft.EntityFrameworkCore;
using RegistroEstudiantes.Context;
using RegistroEstudiantes.Services;
using Scalar.AspNetCore; 

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("SqlConStr");

builder.Services.AddDbContextFactory<Contexto>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<EstudiantesService>();
builder.Services.AddScoped<LibrosServices>();
builder.Services.AddScoped<PrestamosServices>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();