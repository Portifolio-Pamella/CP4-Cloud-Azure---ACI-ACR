using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Aegis.Api.Data;
using Aegis.Api.Repositories;
using Aegis.Api.Repositories.Interfaces;
using Aegis.Api.Services;
using Aegis.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuração do Banco de Dados Oracle
var connectionString = builder.Configuration.GetConnectionString("OracleConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(connectionString));

// 2. Registro de Repositórios (Injeção de Dependência)
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<ISateliteRepository, SateliteRepository>();
builder.Services.AddScoped<IDetritoRepository, DetritoRepository>();
builder.Services.AddScoped<IChaserRepository, ChaserRepository>();
builder.Services.AddScoped<IAlertaRepository, AlertaRepository>();
builder.Services.AddScoped<IMissaoRepository, MissaoRepository>();

// 3. Registro de Services (Injeção de Dependência)
builder.Services.AddScoped<IEmpresaService, EmpresaService>();
builder.Services.AddScoped<ISateliteService, SateliteService>();
builder.Services.AddScoped<IDetritoService, DetritoService>();
builder.Services.AddScoped<IChaserService, ChaserService>();
builder.Services.AddScoped<IAlertaService, AlertaService>();
builder.Services.AddScoped<IMissaoService, MissaoService>();

// 4. Configuração de Controllers e JSON
builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// 5. Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 6. Configuração do Pipeline (Middleware)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Bloco de inicialização do banco de dados (Garante que as tabelas sejam criadas)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();