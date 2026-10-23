# Gerenciador de Tarefas API 

Uma API RESTful desenvolvida em **ASP.NET Core**, utilizando **Entity Framework Core** e **SQLite**. 

Criei este projeto para consolidar meus conhecimentos em desenvolvimento backend e estruturar uma aplicação limpa e escalável. A arquitetura foi pensada para separar bem as responsabilidades: as entidades cuidam dos dados, os DTOs gerenciam os contratos de entrada e saída, o contexto faz a ponte com o banco e os controllers coordenam os fluxos HTTP.

---

## Tecnologias e Requisitos

- **.NET 10 SDK (LTS)** — Você pode verificar se já tem instalado executando `dotnet --version` no terminal. *(O .NET 10 está em suporte LTS ativo; veja a [política oficial de suporte](https://dotnet.microsoft.com/pt-br/platform/support/policy)).*
- **Git** — Caso queira versionar e publicar o projeto.

---

## Como rodar o projeto localmente

1. IMPORTANTE!!! Abra o terminal na pasta raiz do projeto (onde está o arquivo `GerenciadorTarefas.csproj`).
2. Execute os comandos abaixo:

```bash
dotnet restore
dotnet run
```

Na primeira inicialização, a aplicação criará automaticamente o arquivo `gerenciador-tarefas.db` com o banco SQLite local. Fique tranquilo: ele já está configurado no `.gitignore`, então não será enviado para o seu repositório.

O terminal exibirá as URLs locais (HTTP e HTTPS). Abra a versão **HTTPS** seguida de `/swagger` no seu navegador (por exemplo: `https://localhost:7000/swagger`) para acessar a interface interativa e testar todas as rotas facilmente.

> **Dica para o certificado:** Se o navegador exibir um aviso de segurança sobre o certificado SSL local, basta rodar `dotnet dev-certs https --trust` uma única vez no terminal para resolver.

---

##  Endpoints da API

| Método | Rota | Descrição | Resposta Padrão |
| :--- | :--- | :--- | :--- |
| **GET** | `/api/tarefas` | Lista todas as tarefas cadastradas | `200 OK` |
| **GET** | `/api/tarefas/{id}` | Busca uma tarefa específica pelo ID | `200 OK` ou `404 Not Found` |
| **POST** | `/api/tarefas` | Cria uma nova tarefa | `201 Created` |
| **PUT** | `/api/tarefas/{id}` | Atualiza os dados de uma tarefa | `204 No Content` ou `404 Not Found` |
| **DELETE** | `/api/tarefas/{id}` | Remove uma tarefa do sistema | `204 No Content` ou `404 Not Found` |

###  Exemplos de Uso

#### Criando uma tarefa (`POST`)
Envie uma requisição para `/api/tarefas` com o corpo em JSON:

```json
{
  "titulo": "Estudar C#",
  "descricao": "Praticar controllers e Entity Framework Core"
}
```

Resposta esperada (`201 Created`):
```json
{
  "id": 1,
  "titulo": "Estudar C#",
  "descricao": "Praticar controllers e Entity Framework Core",
  "concluida": false,
  "criadaEm": "2026-10-22T12:00:00Z"
}
```

#### Atualizando uma tarefa (`PUT`)
Envie uma requisição para `/api/tarefas/1`:

```json
{
  "titulo": "Estudar C# e .NET",
  "descricao": "Revisar o CRUD da API",
  "concluida": true
}
```
