# Sistema de Reserva de Salas e Recursos
 
API REST para gerenciamento de recursos (salas, equipamentos, espaços) e das reservas feitos sobre eles. O sistema impede que um mesmo recurso seja reservado em horários sobrepostos e controla o acesso por perfil de usuário (Admin e usuário comum) usando autenticação JWT.
 

## Sumário
 
- [Demonstração online](#demonstração-online)
- [Funcionalidades](#funcionalidades)
- [Tecnologias](#tecnologias)
- [Arquitetura](#arquitetura)
- [Regras de negócio](#regras-de-negócio)
- [Endpoints](#endpoints)
- [Como executar](#como-executar)
- [Configuração](#configuração)
- [Testes](#testes)
- [Estrutura de pastas](#estrutura-de-pastas)
- [Melhorias futuras](#melhorias-futuras)

## Demonstração online
 
A API está publicada na plataforma Render e pode ser testada diretamente pelo Swagger, sem instalar nada:
 
**[Acessar o Swagger UI](https://sistemareservas-1pm8.onrender.com/swagger/index.html)**
 
Para testar as rotas restritas ao Admin, use a conta de teste:
 
| Campo | Valor |
| --- | --- |
| E-mail | `admin@sistema.com` |
| Senha | `Admin@123456` |
 
Roteiro sugerido:
 
1. Em `POST /api/Auth/login`, envie as credenciais acima e copie o `accessToken` retornado.
2. Clique em Authorize no topo da página e cole o token.
3. Crie um recurso em `POST /api/Recurso`.
4. Crie uma reserva em `POST /api/Reserva` e teste o conflito de horário repetindo a chamada com um intervalo sobreposto.
5. Para testar o perfil de usuário comum, registre uma conta em `POST /api/Auth/Register` e faça login com ela.
Observação: como a instância é de demonstração, a primeira requisição pode demorar alguns segundos caso o serviço esteja inativo. Os dados podem ser alterados por qualquer pessoa que use a conta de teste.
 
## Funcionalidades
 
- Cadastro e login de usuários com Identity e tokens JWT
- Perfil Admin criado automaticamente na inicialização da aplicação
- CRUD de recursos, restrito ao Admin para escrita
- Criação de reservas com validação de conflito de horário
- Cancelamento de reservas pelo dono ou por um Admin
- Consulta das reservas do usuário autenticado
- Listagem geral de reservas com filtros por recurso e por usuário (Admin)
- Paginação nas listagens
- Tratamento centralizado de exceções com respostas JSON padronizadas
- Documentação interativa com Swagger e suporte a Bearer token
- Migrations aplicadas automaticamente ao iniciar a API
- Execução containerizada com Docker Compose (API + MySQL)
## Tecnologias
 
- C# e .NET 10
- ASP.NET Core Web API
- Entity Framework Core 9 com Pomelo (provedor MySQL)
- MySQL 8.4
- ASP.NET Core Identity
- Autenticação JWT Bearer
- AutoMapper
- Swagger (Swashbuckle)
- xUnit, Moq e FluentAssertions
- Docker e Docker Compose
## Arquitetura
 
O projeto segue os princípios da Clean Architecture, dividido em camadas com dependências apontando para o domínio:
 
| Projeto | Responsabilidade |
| --- | --- |
| `SistemaReserva.Domain` | Entidades, enums, exceções de domínio, interfaces (repositórios, Unit of Work, serviço de token) e paginação |
| `SistemaReserva.Application` | Casos de uso organizados por funcionalidade (um serviço por operação), requests, responses e perfil do AutoMapper |
| `SistemaReserva.Infrastructure` | DbContext, configurações de entidades, repositórios, Unit of Work, migrations, Identity, seed e geração de tokens |
| `SistemaReserva.InfraIoC` | Registro de dependências, autenticação JWT e configuração do Swagger |
| `SistemaReserva.API` | Controllers, middleware de exceções e ponto de entrada da aplicação |
| `SistemaReserva.Tests` | Testes unitários de serviços e controllers |
 
Padrões utilizados: Repository Pattern, Unit of Work, DTOs (requests e responses), injeção de dependência e validações no próprio domínio.
 
## Regras de negócio
 
### Recursos
 
- O nome do recurso é obrigatório e limitado a 250 caracteres.
- Apenas usuários com perfil Admin podem criar, atualizar e excluir recursos.
- Qualquer usuário autenticado pode listar e consultar recursos.
- Um recurso que possui reservas vinculadas não pode ser excluído.
- Apenas recursos ativos podem ser reservados.
### Reservas
 
- A data de início deve ser futura.
- A data de início deve ser anterior à data de fim.
- Não é permitido criar uma reserva que sobreponha o horário de outra reserva confirmada para o mesmo recurso. A verificação considera o intervalo `inicio < fim_existente` e `fim > inicio_existente`, então reservas que apenas se encostam (uma termina exatamente quando a outra começa) são permitidas.
- Reservas canceladas não bloqueiam o horário.
- Toda reserva é criada com o status `Confirmada`.
- O cancelamento é permitido ao dono da reserva ou a um Admin.
- Não é possível cancelar uma reserva já cancelada nem uma reserva que já ocorreu.
- Datas são tratadas com `DateTimeOffset`.
### Paginação
 
- A página deve ser maior que 0.
- O tamanho da página deve estar entre 1 e 50.
## Endpoints
 
Todas as rotas usam o prefixo `/api`. A coluna Acesso indica a autorização exigida.
 
### Autenticação
 
| Método | Rota | Acesso | Descrição |
| --- | --- | --- | --- |
| POST | `/api/Auth/Register` | Público | Registra um novo usuário |
| POST | `/api/Auth/login` | Público | Autentica e retorna `accessToken` e `refreshToken` |
 
### Recursos
 
| Método | Rota | Acesso | Descrição |
| --- | --- | --- | --- |
| POST | `/api/Recurso` | Admin | Cria um recurso |
| GET | `/api/Recurso?page=1&pageSize=10` | Autenticado | Lista recursos paginados |
| GET | `/api/Recurso/{id}` | Autenticado | Consulta um recurso por id |
| PUT | `/api/Recurso/{id}` | Admin | Atualiza um recurso |
| DELETE | `/api/Recurso/{id}` | Admin | Exclui um recurso sem reservas |
 
### Reservas
 
| Método | Rota | Acesso | Descrição |
| --- | --- | --- | --- |
| POST | `/api/Reserva` | Autenticado | Cria uma reserva |
| GET | `/api/Reserva/minhas-reservas?pageNumber=1&pageSize=10` | Autenticado | Lista as reservas do usuário logado |
| PUT | `/api/Reserva/{id}/Cancelar` | Dono ou Admin | Cancela uma reserva |
| GET | `/api/Reserva?recursoId=&userId=&page=1&pageSize=10` | Admin | Lista todas as reservas com filtros opcionais |
 
### Exemplos de requisição
 
Registro:
 
```json
POST /api/Auth/Register
{
  "email": "usuario@exemplo.com",
  "password": "Senha@123",
  "phoneNumber": "13999999999"
}
```
 
Login:
 
```json
POST /api/Auth/login
{
  "email": "usuario@exemplo.com",
  "password": "Senha@123"
}
```
 
Criação de recurso (Admin):
 
```json
POST /api/Recurso
{
  "nome": "Sala de Reunião 1",
  "descricao": "Capacidade para 10 pessoas, com projetor"
}
```
 
Criação de reserva:
 
```json
POST /api/Reserva
{
  "recursoId": 1,
  "descricao": "Reunião de planejamento",
  "inicio": "2026-10-01T14:00:00-03:00",
  "fim": "2026-10-01T15:30:00-03:00"
}
```
 
Nas rotas protegidas, envie o header `Authorization: Bearer {accessToken}`.
 
### Formato de erro
 
Erros tratados pelo middleware retornam JSON no formato abaixo. Em ambiente de desenvolvimento o campo de detalhes traz o stack trace; nos demais, a mensagem genérica `Internal server error`.
 
| Situação | Status HTTP |
| --- | --- |
| Validação de domínio ou requisição inválida | 400 |
| Credenciais inválidas ou token ausente | 401 |
| Sem permissão para a operação | 403 |
| Recurso ou reserva não encontrado | 404 |
| Operação inválida (`InvalidOperationException`) | 409 |
| Erro não tratado | 500 |
 
## Como executar
 
### Pré-requisitos
 
- [.NET SDK 10](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) e Docker Compose
- Git
### Opção 1: tudo com Docker Compose
 
1. Clone o repositório:
```bash
   git clone <url-do-repositorio>
   cd <pasta-do-repositorio>
```
 
2. Crie um arquivo `.env` na raiz (veja o modelo na seção [Configuração](#configuração)).
3. Suba os serviços:
```bash
   docker compose up --build
```
 
4. Acesse o Swagger em `http://localhost:8080/swagger`.
O MySQL fica exposto na porta `3307` do host e os dados são persistidos no volume `mysql_data`. A API só inicia depois que o healthcheck do banco passar.
 
### Opção 2: API local com banco no Docker
 
1. Suba somente o banco:
```bash
   docker compose up mysql
```
 
2. Crie o arquivo `SistemaReserva.API/appsettings.Development.json` (ele é ignorado pelo Git):
```json
   {
     "Jwt": {
       "Key": "<chave-com-32-caracteres-ou-mais>",
       "Issuer": "ReservaSalas.API",
       "Audience": "ReservaSalas.Clients"
     },
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Port=3307;Database=SistemaReserva;User=root;Password=<senha-do-banco>"
     }
   }
```
 
3. Execute a API:
```bash
   dotnet run --project SistemaReserva.API
```
 
4. Acesse o Swagger em `http://localhost:5276/swagger`.
### Usuário Admin padrão
 
Na inicialização, a aplicação aplica as migrations e cria o perfil `Admin` e um usuário administrador de desenvolvimento:
 
| Campo | Valor |
| --- | --- |
| E-mail | `admin@sistema.com` |
| Senha | `Admin@123456` |
 
Essas credenciais estão definidas no código para facilitar o desenvolvimento. Altere-as antes de qualquer uso em produção.
 
### Autenticando no Swagger
 
1. Faça login em `POST /api/Auth/login` e copie o `accessToken`.
2. Clique em Authorize no topo do Swagger.
3. Cole o token (sem o prefixo `Bearer`) e confirme.
## Configuração
 
A aplicação lê as seguintes chaves de configuração:
 
| Chave | Descrição |
| --- | --- |
| `ConnectionStrings:DefaultConnection` | String de conexão com o MySQL |
| `Jwt:Key` | Chave de assinatura do token (mínimo de 32 caracteres) |
| `Jwt:Issuer` | Emissor do token |
| `Jwt:Audience` | Audiência do token |
 
Em variáveis de ambiente, o separador `:` é substituído por `__` (por exemplo, `Jwt__Key`).
 
Modelo de `.env` para o Docker Compose:
 
```env
DB_PASSWORD=<senha-do-banco>
JWT_Issuer=ReservaSalas.API
JWT_Audience=ReservaSalas.Clients
JWT_Key=<chave-com-32-caracteres-ou-mais>
```
 
Não versione o `.env` nem o `appsettings.Development.json`. Ambos já estão no `.gitignore`.
 
O access token expira em 1 hora.
 
## Testes
 
Os testes usam xUnit, Moq e FluentAssertions e cobrem serviços e controllers.
 
```bash
dotnet test
```
 
## Estrutura de pastas
 
```
.
├── SistemaReserva.API
│   ├── Controllers
│   ├── Errors
│   ├── Middleware
│   └── Program.cs
├── SistemaReserva.Application
│   ├── Auth
│   ├── Common
│   ├── Recursos
│   └── Reservas
├── SistemaReserva.Domain
│   ├── Constants
│   ├── Entities
│   ├── Enums
│   ├── Exceptions
│   ├── Interfaces
│   └── Pagination
├── SistemaReserva.Infrastructure
│   ├── Context
│   ├── EntitiesConfiguration
│   ├── Helpers
│   ├── Identity
│   ├── Migrations
│   └── Repositories
├── SistemaReserva.InfraIoC
├── SistemaReserva.Tests
├── Dockerfile
└── docker-compose.yml
```
