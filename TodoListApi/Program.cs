using Microsoft.EntityFrameworkCore;
using TodolistApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Configura serviços e dependências
// Adiciona o DbContext da aplicação. A connection string é lida de appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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

// Força redirecionamento para HTTPS
app.UseHttpsRedirection();

// Pipeline de autorização (aqui só o middleware padrão; não há políticas definidas)
app.UseAuthorization();

// Mapeia os controllers para as rotas definidas por atributos
app.MapControllers();

// Inicia a aplicação
app.Run();
