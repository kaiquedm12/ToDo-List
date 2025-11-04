using Microsoft.EntityFrameworkCore;
using TodolistApi.Data;using DotNetEnv;
using DotNetEnv;


var builder = WebApplication.CreateBuilder(args);

// Configura serviços e dependências
// Adiciona o DbContext da aplicação. A connection string é lida de appsettings.json
// Carrega as variáveis do .env
Env.Load();

// Lê a string de conexão do .env
var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Adiciona suporte a controladores (API controllers)
builder.Services.AddControllers();

// Adiciona geração de documentação OpenAPI/Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// No ambiente de desenvolvimento habilita a UI do Swagger para testar endpoints
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";



app.UseMiddleware<TodolistApi.Middleware.ErrorHandlingMiddleware>();

// Força redirecionamento para HTTPS
app.UseHttpsRedirection();

// Pipeline de autorização (aqui só o middleware padrão; não há políticas definidas)

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

// Mapeia os controllers para as rotas definidas por atributos
app.MapControllers();

// Inicia a aplicação
app.Run();
