📌 Sistema de RH - API

API desenvolvida em .NET 8 para gerenciar funcionários, incluindo cadastro, atualização, exclusão e auditoria de logs de alterações, com persistência em SQL Server e Azure Table Storage.

🛠️ Tecnologias Utilizadas

.NET 8

Entity Framework Core

SQL Server

Azure Table Storage

Swagger / Swashbuckle

⚙️ Funcionalidades

✅ CRUD de Funcionários
✅ Logs de alterações salvos no Azure Table Storage
✅ Rastreio de alterações (antes e depois da atualização)
✅ DTOs para transporte de dados
✅ Uso de Services para regras de negócio

📂 Estrutura do Projeto
src/
├── Controllers/
│   └── FuncionarioController.cs
├── DTOs/
│   └── FuncionarioDTO.cs
├── Models/
│   ├── Funcionario.cs
│   └── FuncionarioLog.cs
├── Repositories/
│   └── FuncionarioRepository.cs
├── Services/
│   ├── FuncionarioService.cs
│   └── FuncionarioLogService.cs
└── Program.cs

▶️ Como Rodar o Projeto

Clone o repositório:

git clone https://github.com/seu-usuario/sistema-rh-api.git
cd sistema-rh-api


Configure a connection string no appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=SistemaRH;User Id=sa;Password=YourPassword;"
},
"AzureStorage": {
  "ConnectionString": "DefaultEndpointsProtocol=https;AccountName=seuStorage;AccountKey=chave;EndpointSuffix=core.windows.net",
  "TableName": "FuncionarioLogs"
}


Rode as migrations (apenas para SQL Server):

dotnet ef database update


Execute a API:

dotnet run


Acesse a documentação no Swagger:

http://localhost:5100/swagger

📖 Exemplo de Log Registrado

Quando um funcionário é atualizado, um log é salvo no Azure Table Storage:

{
  "PartitionKey": "Funcionario",
  "RowKey": "123",
  "Action": "Atualizacao",
  "OldData": {
    "Nome": "João Silva",
    "Salario": 5000.00
  },
  "NewData": {
    "Nome": "João Silva",
    "Salario": 6000.00
  },
  "Timestamp": "2025-08-23T18:00:00Z"
}

🧪 Endpoints Principais

GET /api/funcionarios → Lista todos os funcionários

GET /api/funcionarios/{id} → Busca funcionário por ID

POST /api/funcionarios → Cria novo funcionário

PUT /api/funcionarios/{id} → Atualiza funcionário e registra log

DELETE /api/funcionarios/{id} → Remove funcionário

📌 Melhorias Futuras

Implementar autenticação JWT

Criar paginação nos endpoints de listagem

Criar testes unitários com xUnit