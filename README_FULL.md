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


## Estrutura de arquivos (resumo)

- `TodoListApi/Program.cs` - configuração geral da aplicação e do pipeline HTTP
- `TodoListApi/Controllers/TodoController.cs` - controlador com ações CRUD
- `TodoListApi/Data/AppDbContext.cs` - DbContext do EF Core
- `TodoListApi/Models/TodoItem.cs` - entidade TodoItem
- `appsettings.json` / `appsettings.Development.json` - configuração, connection string


## Migrações

O repositório já contém a pasta `Migrations` com migração inicial. Para aplicar essas migrações localmente, use `dotnet ef database update` (veja seção acima).


## Notas finais

- Mantive a lógica inalterada e apenas acrescentei comentários e inicializadores mínimos para evitar warnings de compilação.
- Se preferir que eu não adicione os inicializadores `= null!;` e lide apenas com comentários, posso reverter essa pequena mudança; entretanto, pode ser necessário ajustar o projeto (nullable) ou inicializar propriedades para compilar sem avisos.

Se quiser, eu posso:
- Reverter ou alterar a forma de lidar com propriedades não-nulas (usar `string?` ou `required`),
- Adicionar exemplos de requests/responses no README,
- Incluir scripts de Docker para PostgreSQL e para a API.

Obrigado — diga qual complemento prefere e eu sigo com isso.
