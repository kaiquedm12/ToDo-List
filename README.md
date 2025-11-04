# ToDo-List (API)

Este repositório contém uma API simples de lista de tarefas (ToDo) construída com ASP.NET Core e Entity Framework Core.

## Visão geral

- Projeto principal: `TodoListApi` (API Web)
- Objetivo: CRUD para tarefas (todo items) com persistência via EF Core.
- Banco: PostgreSQL (configurado via `Npgsql` no `Program.cs`, string em `appsettings.json`).


## O que foi feito nesta revisão

- Adicionei comentários explicativos (em português) aos arquivos principais do projeto para documentar propósito e funcionamento:
  - `TodoListApi/Models/TodoItem.cs` - documento da entidade TodoItem e suas propriedades.
  - `TodoListApi/Data/AppDbContext.cs` - explica o DbContext e o DbSet.
  - `TodoListApi/Controllers/TodoController.cs` - documenta cada rota e comportamento esperado.
  - `TodoListApi/Program.cs` - descreve a configuração do app, DbContext, Swagger e pipeline.

- Criei este `README_FULL.md` com instruções de execução e detalhes úteis para desenvolvedores.

> Observação técnica: Para evitar warnings/erros de compilação com propriedades não nulas do C# (quando o projeto tem nullable enabled), foi adicionado o inicializador `= null!;` em duas propriedades que são populadas pelo EF em runtime (`TodoItem.Title` e `AppDbContext.TodoItems`). Isso é uma alteração mínima e não altera a lógica da aplicação — apenas elimina um aviso de compilação.


## Requisitos

- .NET 7/8/9 SDK (o projeto foi visto em .NET 9 no diretório `bin`)
- PostgreSQL para executar localmente (ou adapte a connection string para outro provider)
- (Opcional) `dotnet-ef` se desejar executar migrações via CLI


## Executando localmente (PowerShell)

Abra um terminal na pasta do projeto `TodoListApi` e rode:

```powershell
# Na raiz do projeto (ex: c:\Users\...\ToDo-List\TodoListApi)
# Restaura pacotes
dotnet restore

# (Opcional) Aplicar migrações - requer dotnet-ef
# dotnet tool install --global dotnet-ef
# dotnet ef database update

# Build
dotnet build

# Executar a API
dotnet run
```

A aplicação, por padrão, expõe os endpoints configurados em `TodoController`. Se estiver em ambiente de desenvolvimento, o Swagger UI estará disponível para inspeção e testes.


## Endpoints principais

Base route: `api/todo`

- GET `api/todo` - lista todas as tarefas
- GET `api/todo/{id}` - retorna a tarefa pelo id (404 se não existir)
- POST `api/todo` - cria uma nova tarefa (envie JSON com `Title` e `IsCompleted` opcional)
- PUT `api/todo/{id}` - atualiza a tarefa (id da rota deve bater com `item.Id` do payload)
- DELETE `api/todo/{id}` - remove a tarefa


## Exemplos de uso

Abaixo há exemplos de requests e responses para os endpoints principais. Use `curl` no Linux/macOS ou PowerShell no Windows.

1) Criar uma tarefa (POST)

Request (curl):

```bash
curl -X POST "https://localhost:5001/api/todo" -H "Content-Type: application/json" -d '{"title":"Comprar leite","isCompleted":false}'
```

Request (PowerShell):

```powershell
Invoke-RestMethod -Uri "https://localhost:5001/api/todo" -Method Post -Body (@{ title = 'Comprar leite'; isCompleted = $false } | ConvertTo-Json) -ContentType "application/json"
```

Exemplo de body JSON (POST):

```json
{
  "title": "Comprar leite",
  "isCompleted": false
}
```

Resposta esperada (201 Created) — Location aponta para `GET api/todo/{id}`:

```json
{
  "id": 1,
  "title": "Comprar leite",
  "isCompleted": false
}
```

2) Listar todas as tarefas (GET)

Request (curl):

```bash
curl "https://localhost:5001/api/todo"
```

Resposta (200 OK):

```json
[
  {
    "id": 1,
    "title": "Comprar leite",
    "isCompleted": false
  }
]
```

3) Atualizar uma tarefa (PUT)

Request (curl):

```bash
curl -X PUT "https://localhost:5001/api/todo/1" -H "Content-Type: application/json" -d '{"id":1,"title":"Comprar leite e pão","isCompleted":true}'
```

Exemplo de body JSON (PUT):

```json
{
  "id": 1,
  "title": "Comprar leite e pão",
  "isCompleted": true
}
```

Resposta esperada: `204 No Content` (sem body)

4) Deletar uma tarefa (DELETE)

Request (curl):

```bash
curl -X DELETE "https://localhost:5001/api/todo/1"
```

Resposta esperada: `204 No Content`

Obs: Se estiver usando HTTPS local padrão do ASP.NET Core, ajuste a porta conforme exibida ao rodar `dotnet run` (por exemplo 5001/5000). Se usar HTTP, troque `https://` por `http://`.


## Estrutura de arquivos (resumo)

- `TodoListApi/Program.cs` - configuração geral da aplicação e do pipeline HTTP
- `TodoListApi/Controllers/TodoController.cs` - controlador com ações CRUD
- `TodoListApi/Data/AppDbContext.cs` - DbContext do EF Core
- `TodoListApi/Models/TodoItem.cs` - entidade TodoItem
- `appsettings.json` / `appsettings.Development.json` - configuração, connection string
