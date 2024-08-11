using ApiRepracionCelular.Entidades;
using ApiRepracionCelular.Middleware;
using ApiRepracionCelular.Servicios.Autenticacion;
using ApiRepracionCelular.Servicios.Autenticacion.Interfaces;
using ApiRepracionCelular.Servicios.Cliente;
using ApiRepracionCelular.Servicios.Cliente.Interfaces;
using ApiRepracionCelular.Servicios.Reparacion;
using ApiRepracionCelular.Servicios.Reparacion.Interfaces;
using ApiRepracionCelular.Servicios.Rol;
using ApiRepracionCelular.Servicios.Rol.Interfaces;
using ApiRepracionCelular.Servicios.Usuario;
using ApiRepracionCelular.Servicios.Usuario.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("*",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});
// Añadir servicios de autenticación
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SECRETO"]!))
    };
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Reparaciones", Version = "v1" });

    // Definir el esquema de seguridad
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor ingrese el token JWT con el prefijo 'Bearer' (Ejemplo: 'Bearer 12345abcdef')",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // Requerir el token en cada operación
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext<ContextoDB>(options =>
{
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
    options.UseSqlServer(builder.Configuration.GetConnectionString("UriSqlServer"));
});
    
builder.Services.AddScoped<IAutenticacionService, AutenticacionService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IReparacionService, ReparacionService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExcepcionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("*");
app.MapControllers();

app.Run();
