# Gerenciador de Tarefas API

Uma API REST de tarefas feita com ASP.NET Core, Entity Framework Core e SQLite. O projeto foi organizado para servir como primeiro projeto de portfólio: a entidade representa os dados, os DTOs definem o que entra e sai da API, o contexto conversa com o banco e o controller contém as operações HTTP.

## Requisitos

- .NET 10 SDK (LTS). Confira a instalação com `dotnet --version`.
- Git, caso queira publicar o projeto no GitHub.

O .NET 10 está em suporte LTS ativo. Consulte a [política oficial de suporte do .NET](https://dotnet.microsoft.com/pt-br/platform/support/policy).

## Executar localmente

No terminal, entre na pasta que contém `GerenciadorTarefas.csproj` e execute:

```bash
dotnet restore
dotnet run
```

Na primeira inicialização, a aplicação cria o arquivo `gerenciador-tarefas.db` na pasta de execução. Esse arquivo contém seus dados e não é enviado ao Git, pois está no `.gitignore`.

O terminal informa os endereços locais (HTTP e HTTPS). Abra o endereço HTTPS seguido de `/swagger`, por exemplo `https://localhost:7000/swagger`, usando a porta que apareceu no seu terminal. A tela do Swagger permite testar cada operação pelo navegador. Se o navegador mostrar um aviso de certificado local, execute `dotnet dev-certs https --trust` uma vez e confirme a solicitação do Windows.

O Swagger fica habilitado no ambiente de desenvolvimento. `dotnet run` usa esse ambiente por padrão.

## Endpoints

| Método | Rota | O que faz | Resposta esperada |
|---|---|---|---|
| GET | `/api/tarefas` | Lista as tarefas | `200 OK` |
| GET | `/api/tarefas/{id}` | Busca uma tarefa pelo ID | `200 OK` ou `404 Not Found` |
| POST | `/api/tarefas` | Cria uma tarefa | `201 Created` |
| PUT | `/api/tarefas/{id}` | Atualiza todos os campos editáveis | `204 No Content` ou `404 Not Found` |
| DELETE | `/api/tarefas/{id}` | Exclui uma tarefa | `204 No Content` ou `404 Not Found` |

### Exemplo de criação

Envie um `POST` para `/api/tarefas` com o corpo JSON:

```json
{
  "titulo": "Estudar C#",
  "descricao": "Praticar controllers e Entity Framework Core"
}
```

Resposta de exemplo (`201 Created`):

```json
{
  "id": 1,
  "titulo": "Estudar C#",
  "descricao": "Praticar controllers e Entity Framework Core",
  "concluida": false,
  "criadaEm": "2026-10-22T12:00:00Z"
}
```

### Exemplo de atualização

Envie um `PUT` para `/api/tarefas/1` com todos os campos editáveis:

```json
{
  "titulo": "Estudar C# e .NET",
  "descricao": "Revisar o CRUD da API",
  "concluida": true
}
```

As validações exigem título entre 3 e 120 caracteres. A descrição é opcional e aceita até 500 caracteres. Quando uma requisição não passa pela validação, a API responde `400 Bad Request` com os detalhes dos campos inválidos.

## Como o projeto está organizado

- `Models/Tarefa.cs`: dados persistidos de uma tarefa.
- `DTOs/`: formatos de entrada e resposta; evita expor a entidade diretamente nos contratos HTTP.
- `Data/TarefasDbContext.cs`: configuração e acesso às tabelas pelo EF Core.
- `Controllers/TarefasController.cs`: rotas e operações CRUD.
- `Program.cs`: registro dos serviços, SQLite, Swagger e inicialização do banco.
- `appsettings.json`: cadeia de conexão do SQLite.

O banco é criado com `EnsureCreated()` para simplificar o primeiro contato. Em projetos que evoluem em produção, o caminho recomendado é usar migrations do EF Core para versionar mudanças no esquema.

## Publicar no GitHub

Crie um repositório vazio no GitHub. Depois, na pasta deste projeto:

```bash
git init
git add .
git commit -m "Criar API de gerenciamento de tarefas"
git branch -M main
git remote add origin https://github.com/SEU-USUARIO/SEU-REPOSITORIO.git
git push -u origin main
```

Substitua `SEU-USUARIO` e `SEU-REPOSITORIO` pelos valores do repositório criado. O banco local, os arquivos de compilação e as configurações específicas da máquina são ignorados pelo Git.
