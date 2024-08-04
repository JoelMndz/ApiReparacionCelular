using ApiRepracionCelular.Entidades;
using ApiRepracionCelular.Middleware;
using ApiRepracionCelular.Servicios.Autenticacion;
using ApiRepracionCelular.Servicios.Autenticacion.Interfaces;
using ApiRepracionCelular.Servicios.Cliente;
using ApiRepracionCelular.Servicios.Cliente.Interfaces;
using ApiRepracionCelular.Servicios.Rol;
using ApiRepracionCelular.Servicios.Rol.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ContextoDB>(options =>
{
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
    options.UseSqlServer(builder.Configuration.GetConnectionString("UriSqlServer"));
});
    
builder.Services.AddScoped<IAutenticacionService, AutenticacionService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IRolService, RolService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExcepcionMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
