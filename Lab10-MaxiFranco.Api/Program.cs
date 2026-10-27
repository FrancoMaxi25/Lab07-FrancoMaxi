using Lab10_MaxiFranco.Api.Configuration;
using Lab10_MaxiFranco.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Registro de controladores
builder.Services.AddControllers();

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();

// Configuración de arquitectura
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

// Configuración de JWT, autorización y Swagger
builder.Services.AddApiSecurity(builder.Configuration);

var app = builder.Build();

// Configuración del entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Mapeo de controladores
app.MapControllers();

app.Run();