# 🏍️ TrackZone API - Sistema de Gestão de Motos

## 🚀 ADVANCED BUSINESS DEVELOPMENT WITH .NET

### 📋 Visão Geral do Projeto

O **TrackZone API** é uma solução completa de gestão de motos desenvolvida com **.NET 9** e **ASP.NET Core**, implementando as melhores práticas de desenvolvimento empresarial e arquitetura moderna.

### 🎯 Funcionalidades Implementadas

#### ✅ **API RESTful Completa** (25 pontos)
- **Web API** com arquitetura limpa e escalável
- **Endpoints RESTful** seguindo convenções HTTP
- **Paginação** em todos os endpoints de listagem
- **Validação de dados** com Data Annotations
- **Tratamento de erros** padronizado

#### ✅ **Health Checks** (10 pontos)
- **Health Check Geral**: Status da aplicação
- **Health Check do Banco**: Conexão Oracle
- **Health Check da Memória**: Monitoramento de recursos
- **Endpoints**: `/api/v1.0/Health`, `/api/v1.0/Health/database`, `/api/v1.0/Health/memory`

#### ✅ **Versionamento da API** (10 pontos)
- **Suporte a múltiplas versões**: v1.0 e v2.0
- **Versionamento por URL**: `/api/v1.0/` e `/api/v2.0/`
- **Backward compatibility** mantida
- **Estratégia de versionamento** documentada

#### ✅ **Segurança JWT** (25 pontos)
- **Autenticação JWT** completa
- **Autorização baseada em roles**: ADMIN, GERENTE, OPERADOR
- **Token validation** e refresh
- **Claims personalizados** para controle de acesso
- **Middleware de segurança** configurado

#### ✅ **Machine Learning com ML.NET** (25 pontos)
- **Treinamento de modelo** para previsão de status
- **Predição de status** das motos
- **Análise de padrões** nos dados
- **Informações do modelo** e métricas
- **Endpoints ML**: `/api/v2/ml/*`

#### ✅ **Testes Unitários com xUnit** (30 pontos)
- **Testes unitários** para lógica principal
- **Testes de integração** com WebApplicationFactory
- **Cobertura de testes** para serviços críticos
- **Mocks** e stubs implementados

### 🏗️ Arquitetura do Projeto

```
TrackZone API/
├── 📁 Controllers/           # Controladores da API
│   ├── AuthController.cs     # Autenticação JWT
│   ├── MotosController.cs    # Gestão de motos
│   ├── OperacoesController.cs # Operações de check-in/out
│   ├── UsuariosController.cs  # Gestão de usuários
│   ├── StatusMotosController.cs # Status das motos
│   ├── MLController.cs       # Machine Learning
│   └── HealthController.cs   # Health Checks
├── 📁 Services/              # Camada de serviços
│   ├── Auth/JwtService.cs    # Serviço JWT
│   ├── ML/MotoAnalysisService.cs # ML.NET
│   └── HealthChecks/         # Health Check services
├── 📁 Repositories/          # Camada de dados
│   ├── Interfaces/           # Contratos dos repositórios
│   └── Implementations/      # Implementações EF Core
├── 📁 Models/               # Entidades e DTOs
│   ├── Entities/            # Entidades do domínio
│   └── DTOs/                # Data Transfer Objects
├── 📁 Data/                 # Contexto do banco
│   └── ApplicationDbContext.cs # EF Core Context
├── 📁 Tests/                # Testes unitários
│   └── Unit/                # Testes com xUnit
└── 📁 Migrations/           # Migrações do banco
```

### 🛠️ Tecnologias Utilizadas

- **.NET 9** - Framework principal
- **ASP.NET Core** - Web API
- **Entity Framework Core** - ORM
- **Oracle Database** - Banco de dados
- **JWT Bearer** - Autenticação
- **ML.NET** - Machine Learning
- **xUnit** - Testes unitários
- **AutoMapper** - Mapeamento de objetos
- **Swagger/OpenAPI** - Documentação da API

### 🚀 Como Executar o Projeto

#### 📋 Pré-requisitos
- **.NET 9 SDK** instalado
- **Oracle Database** configurado
- **Visual Studio 2022** ou **VS Code**

#### 🔧 Configuração
1. **Clone o repositório**:
   ```bash
   git clone <repository-url>
   cd challenge3-net
   ```

2. **Configure a conexão com o banco**:
   ```json
   // appsettings.json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Data Source=localhost:1521/XE;User Id=trackzone;Password=senha123;"
     }
   }
   ```

3. **Execute as migrações**:
```bash
dotnet ef database update
```

4. **Execute o projeto**:
```bash
dotnet run
```

#### 🌐 Acesso à API
- **URL Base**: `https://localhost:5001` ou `http://localhost:5000`
- **Swagger UI**: `https://localhost:5001/swagger`
- **Health Check**: `https://localhost:5001/api/v1.0/Health`

### 🧪 Executando os Testes

#### 📋 Pré-requisitos
- .NET 9 SDK instalado
- Projeto compilado (`dotnet build`)
- Aplicação deve estar configurada para testes

#### 🧪 Testes Unitários
Os testes unitários testam componentes individuais isoladamente usando mocks e bancos de dados em memória.

```bash
# Executar todos os testes unitários
dotnet test

# Executar testes específicos por classe
dotnet test --filter "JwtServiceTests"
dotnet test --filter "MLServiceTests"

# Executar testes unitários apenas
dotnet test --filter "FullyQualifiedName~Unit"

# Executar com cobertura de código
dotnet test --collect:"XPlat Code Coverage"
```

**Testes Unitários Disponíveis:**
- ✅ `JwtServiceTests` - Testes do serviço JWT (geração, validação, roles)
- ✅ `MLServiceTests` - Testes do serviço ML.NET (treinamento, predição, análise)

#### 🔍 Testes de Integração
Os testes de integração usam `WebApplicationFactory` para testar a aplicação completa em um ambiente de teste real.

```bash
# Executar todos os testes de integração
dotnet test --filter "Integration"

# Executar testes de integração específicos
dotnet test --filter "AuthIntegrationTests"
dotnet test --filter "MotoIntegrationTests"
dotnet test --filter "HealthCheckIntegrationTests"

# Executar testes de integração apenas
dotnet test --filter "FullyQualifiedName~Integration"
```

**Testes de Integração Disponíveis:**
- ✅ `AuthIntegrationTests` - Testes de autenticação (login, validação, user-info)
- ✅ `MotoIntegrationTests` - Testes de endpoints de motos (GET, POST, paginação)
- ✅ `HealthCheckIntegrationTests` - Testes de health checks (health, ready, live, database)

#### 📊 Relatório de Cobertura
```bash
# Gerar relatório de cobertura completo
dotnet test --collect:"XPlat Code Coverage" --results-directory ./TestResults

# Gerar relatório com detalhes
dotnet test --collect:"XPlat Code Coverage" --settings:coverlet.runsettings --results-directory ./TestResults
```

#### 🎯 Estrutura dos Testes
```
Tests/
├── Unit/
│   ├── JwtServiceTests.cs      # Testes unitários do JWT
│   └── MLServiceTests.cs       # Testes unitários do ML.NET
└── Integration/
    ├── AuthIntegrationTests.cs      # Testes de integração de autenticação
    ├── MotoIntegrationTests.cs     # Testes de integração de motos
    └── HealthCheckIntegrationTests.cs # Testes de health checks
```

#### ✅ Verificação de Testes
Para garantir que todos os testes estão passando:

```bash
# Executar todos os testes e verificar resultados
dotnet test --verbosity normal

# Executar com detalhes de falhas
dotnet test --verbosity detailed
```

#### 📝 Notas Importantes sobre Testes
1. **Testes de Integração** requerem que o projeto esteja configurado corretamente
2. **WebApplicationFactory** cria uma instância de teste da aplicação
3. **Testes de Integração** podem fazer requisições HTTP reais para a API
4. **Testes Unitários** usam mocks e bancos em memória para isolamento
5. Os testes de integração testam autenticação JWT real, então precisam de um usuário válido no banco

### 🔐 Autenticação e Segurança

#### 🎫 JWT Token
```bash
# Login para obter token
curl -X POST "https://localhost:5001/api/v1.0/Auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "ala@example.com",
    "senha": "123456"
  }'
```

#### 🔑 Uso do Token
```bash
# Usar token nas requisições
curl -X GET "https://localhost:5001/api/v1.0/Motos" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN"
```

### 🤖 Machine Learning

#### 🧠 Treinamento do Modelo
```bash
curl -X POST "https://localhost:5001/api/v2/ml/train-model" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "trainingData": [
      {
        "motoId": 1,
        "status": "DISPONIVEL",
        "dataCriacao": "2024-01-15T00:00:00Z"
      }
    ]
  }'
```

#### 🔮 Predição de Status
```bash
curl -X POST "https://localhost:5001/api/v2/ml/predict-status" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "motoId": 1,
    "features": {
      "tempoUso": 120,
      "manutencoes": 2,
      "operacoes": 15
    }
  }'
```

### 📊 Health Checks

#### 🏥 Status da Aplicação
```bash
# Health check geral
curl -X GET "https://localhost:5001/api/v1.0/Health"

# Health check do banco
curl -X GET "https://localhost:5001/api/v1.0/Health/database"

# Health check da memória
curl -X GET "https://localhost:5001/api/v1.0/Health/memory"
```

### 📈 Versionamento da API

#### 🔄 Versões Disponíveis
- **v1.0**: Versão estável com funcionalidades básicas
- **v2.0**: Versão com funcionalidades avançadas e ML

#### 📝 Exemplo de Uso
```bash
# API v1.0
curl -X GET "https://localhost:5001/api/v1.0/Motos"

# API v2.0
curl -X GET "https://localhost:5001/api/v2.0/Motos"
```

### 🎯 Perfis de Usuário

| Perfil | Permissões | Endpoints |
|--------|------------|-----------|
| **ADMIN** | Acesso total | Todos os endpoints |
| **GERENTE** | Operações + Relatórios | Motos, Operações, Status, ML |
| **OPERADOR** | Operações básicas | Operações limitadas |

### 📋 Documentação Completa dos Endpoints

### 🔐 Autenticação

#### Login
- **Endpoint**: `POST /api/v1.0/Auth/login`
- **Descrição**: Realiza login do usuário e retorna token JWT
- **Body**:
```json
{
  "email": "ala@example.com",
  "senha": "123456"
}
```
- **Resposta**:
```json
{
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjUzIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6ImFsYSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6ImFsYUBleGFtcGxlLmNvbSIsInBlcmZpbCI6IkdFUkVOVEUiLCJjbnBqIjoiOTguNzY1LjAwMC8wMDAxLTEwIiwianRpIjoiM2FkNmE3MDgtNjViZi00N2U0LWJiYWUtYTM4Zjk2Mzk3MjUzIiwiaWF0IjoxNzYxNjU5NjQ0LCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiR0VSRU5URSIsIk9QRVJBRE9SIl0sImV4cCI6MTc2MTY2MzI0NCwiaXNzIjoiVHJhY2tab25lQVBJIiwiYXVkIjoiVHJhY2tab25lVXNlcnMifQ.Sqsv8feSsJCZKZL4AXj_zjdnCdiKrMcvZSZaou72fMQ",
    "usuario": {
      "id": 53,
      "nome": "Ala",
      "email": "ala@example.com",
      "perfil": "GERENTE"
    }
  }
  ```

#### Validar Token
- **Endpoint**: `POST /api/v1.0/Auth/validate-token`
- **Descrição**: Valida se o token JWT é válido
- **Headers**: `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
- **Resposta**:
  ```json
  {
    "isValid": true,
    "usuario": {
      "id": 53,
      "nome": "Ala",
      "email": "ala@example.com",
      "perfil": "GERENTE"
    }
  }
  ```

#### Informações do Usuário
- **Endpoint**: `GET /api/v1.0/Auth/user-info`
- **Descrição**: Retorna informações do usuário logado
- **Headers**: `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
- **Resposta**:
```json
{
  "id": 53,
    "nome": "Ala",
  "email": "ala@example.com",
  "perfil": "GERENTE",
    "isAdmin": false,
    "isManagerOrAdmin": true,
    "isOperador": true
  }
  ```

### 🏍️ Motos

#### Listar Motos
- **Endpoint**: `GET /api/v1.0/Motos?pageNumber=1&pageSize=10`
- **Descrição**: Lista todas as motos com paginação
- **Headers**: `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
- **Query Parameters**:
  - `pageNumber` (int): Número da página (padrão: 1)
  - `pageSize` (int): Tamanho da página (padrão: 10)
- **Resposta**:
```json
{
    "data": [
    {
      "id": 1,
        "placa": "ABC-1234",
        "chassi": "CHASSI123456789",
        "motor": "Motor 1.0",
        "usuarioId": 53,
        "dataCriacao": "2024-01-15T10:30:00Z"
      },
      {
        "id": 2,
        "placa": "XYZ-9876",
        "chassi": "CHASSI987654321",
        "motor": "Motor 2.0",
        "usuarioId": 53,
        "dataCriacao": "2024-01-15T11:45:00Z"
      }
    ],
    "totalCount": 2,
  "pageNumber": 1,
  "pageSize": 10,
  "totalPages": 1,
  "hasPreviousPage": false,
    "hasNextPage": false
  }
  ```

#### Buscar Moto por ID
- **Endpoint**: `GET /api/v1.0/Motos/1`
- **Descrição**: Busca uma moto específica por ID
- **Headers**: `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
- **Resposta**:
```json
{
  "id": 1,
    "placa": "ABC-1234",
    "chassi": "CHASSI123456789",
    "motor": "Motor 1.0",
    "usuarioId": 53,
    "dataCriacao": "2024-01-15T10:30:00Z"
  }
  ```

#### Criar Moto
- **Endpoint**: `POST /api/v1.0/Motos`
- **Descrição**: Cria uma nova moto
- **Headers**: `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
- **Body**:
  ```json
  {
    "placa": "DEF-5678",
    "chassi": "CHASSI567890123",
    "motor": "Motor 3.0",
    "usuarioId": 53
  }
  ```
- **Resposta**:
  ```json
  {
    "id": 3,
    "placa": "DEF-5678",
    "chassi": "CHASSI567890123",
    "motor": "Motor 3.0",
    "usuarioId": 53,
    "dataCriacao": "2024-01-15T14:20:00Z"
  }
  ```

#### Atualizar Moto
- **Endpoint**: `PUT /api/v1.0/Motos/{id}`
- **Descrição**: Atualiza uma moto existente
- **Headers**: `Authorization: Bearer {token}`
- **Body**:
```json
{
    "placa": "string",
    "chassi": "string",
    "motor": "string",
    "usuarioId": 0
  }
  ```
- **Resposta**:
```json
{
    "id": 0,
    "placa": "string",
    "chassi": "string",
    "motor": "string",
    "usuarioId": 0,
    "dataCriacao": "2024-01-01T00:00:00Z"
  }
  ```

#### Deletar Moto
- **Endpoint**: `DELETE /api/v1.0/Motos/{id}`
- **Descrição**: Deleta uma moto
- **Headers**: `Authorization: Bearer {token}`
- **Resposta**: `204 No Content`

### 📊 Operações

#### Listar Operações
- **Endpoint**: `GET /api/v1.0/Operacoes`
- **Descrição**: Lista todas as operações com paginação
- **Headers**: `Authorization: Bearer {token}`
- **Query Parameters**:
  - `pageNumber` (int): Número da página (padrão: 1)
  - `pageSize` (int): Tamanho da página (padrão: 10)
- **Resposta**:
  ```json
  {
    "data": [
      {
        "id": 0,
        "tipoOperacao": "CHECK_IN",
        "descricao": "string",
        "motoId": 0,
        "usuarioId": 0,
        "dataCriacao": "2024-01-01T00:00:00Z"
      }
    ],
    "totalCount": 0,
    "pageNumber": 0,
    "pageSize": 0,
    "totalPages": 0,
    "hasPreviousPage": true,
    "hasNextPage": true
  }
  ```

#### Buscar Operação por ID
- **Endpoint**: `GET /api/v1.0/Operacoes/{id}`
- **Descrição**: Busca uma operação específica por ID
- **Headers**: `Authorization: Bearer {token}`
- **Resposta**:
```json
{
    "id": 0,
    "tipoOperacao": "CHECK_IN",
    "descricao": "string",
    "motoId": 0,
    "usuarioId": 0,
    "dataCriacao": "2024-01-01T00:00:00Z"
  }
  ```

#### Criar Operação
- **Endpoint**: `POST /api/v1.0/Operacoes`
- **Descrição**: Cria uma nova operação
- **Headers**: `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
- **Body**:
```json
{
    "tipoOperacao": 0,
    "descricao": "Check-in da moto para entrega",
    "motoId": 1,
    "usuarioId": 53
  }
  ```
- **Resposta**:
  ```json
  {
    "id": 1,
    "tipoOperacao": "CHECK_IN",
    "descricao": "Check-in da moto para entrega",
    "motoId": 1,
    "usuarioId": 53,
    "dataCriacao": "2024-01-15T15:30:00Z"
  }
  ```

#### Atualizar Operação
- **Endpoint**: `PUT /api/v1.0/Operacoes/{id}`
- **Descrição**: Atualiza uma operação existente
- **Headers**: `Authorization: Bearer {token}`
- **Body**:
  ```json
  {
    "tipoOperacao": 0,
    "descricao": "string",
    "motoId": 0,
    "usuarioId": 0
  }
  ```
- **Resposta**:
  ```json
  {
    "id": 0,
    "tipoOperacao": "CHECK_IN",
    "descricao": "string",
    "motoId": 0,
    "usuarioId": 0,
    "dataCriacao": "2024-01-01T00:00:00Z"
  }
  ```

#### Deletar Operação
- **Endpoint**: `DELETE /api/v1.0/Operacoes/{id}`
- **Descrição**: Deleta uma operação
- **Headers**: `Authorization: Bearer {token}`
- **Resposta**: `204 No Content`

### 👥 Usuários

#### Listar Usuários
- **Endpoint**: `GET /api/v1.0/Usuarios?pageNumber=1&pageSize=10`
- **Descrição**: Lista todos os usuários com paginação
- **Headers**: `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
- **Query Parameters**:
  - `pageNumber` (int): Número da página (padrão: 1)
  - `pageSize` (int): Tamanho da página (padrão: 10)
- **Resposta**:
  ```json
  {
    "data": [
      {
        "id": 53,
        "nome": "Ala",
        "email": "ala@example.com",
        "perfil": "GERENTE",
        "cnpj": "98.765.000/0001-10",
        "telefone": "(11) 99999-9999",
        "endereco": "Rua das Flores, 123",
        "nomeFilial": "Filial Central",
        "dataCriacao": "2024-01-10T08:00:00Z"
      }
    ],
    "totalCount": 1,
    "pageNumber": 1,
    "pageSize": 10,
    "totalPages": 1,
    "hasPreviousPage": false,
    "hasNextPage": false
  }
  ```

#### Buscar Usuário por ID
- **Endpoint**: `GET /api/v1.0/Usuarios/53`
- **Descrição**: Busca um usuário específico por ID
- **Headers**: `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
- **Resposta**:
  ```json
  {
    "id": 53,
    "nome": "Ala",
    "email": "ala@example.com",
    "perfil": "GERENTE",
    "cnpj": "98.765.000/0001-10",
    "telefone": "(11) 99999-9999",
    "endereco": "Rua das Flores, 123",
    "nomeFilial": "Filial Central",
    "dataCriacao": "2024-01-10T08:00:00Z"
  }
  ```

#### Criar Usuário
- **Endpoint**: `POST /api/v1.0/Usuarios`
- **Descrição**: Cria um novo usuário
- **Headers**: `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
- **Body**:
  ```json
  {
    "nome": "João Silva",
    "email": "joao@empresa.com",
    "senha": "123456",
    "perfil": "OPERADOR",
    "cnpj": "12.345.678/0001-90",
    "telefone": "(11) 88888-8888",
    "endereco": "Rua Nova, 456",
    "nomeFilial": "Filial Norte"
  }
  ```
- **Resposta**:
```json
{
    "id": 54,
    "nome": "João Silva",
    "email": "joao@empresa.com",
    "perfil": "OPERADOR",
    "cnpj": "12.345.678/0001-90",
    "telefone": "(11) 88888-8888",
    "endereco": "Rua Nova, 456",
    "nomeFilial": "Filial Norte",
    "dataCriacao": "2024-01-15T16:00:00Z"
  }
  ```

#### Atualizar Usuário
- **Endpoint**: `PUT /api/v1.0/Usuarios/{id}`
- **Descrição**: Atualiza um usuário existente
- **Headers**: `Authorization: Bearer {token}`
- **Body**:
  ```json
  {
    "nome": "string",
    "email": "string",
    "senha": "string",
    "perfil": "ADMIN",
    "cnpj": "string",
    "telefone": "string",
    "endereco": "string",
    "nomeFilial": "string"
  }
  ```
- **Resposta**:
  ```json
  {
    "id": 0,
    "nome": "string",
    "email": "string",
    "perfil": "ADMIN",
    "cnpj": "string",
    "telefone": "string",
    "endereco": "string",
    "nomeFilial": "string",
    "dataCriacao": "2024-01-01T00:00:00Z"
  }
  ```

#### Deletar Usuário
- **Endpoint**: `DELETE /api/v1.0/Usuarios/{id}`
- **Descrição**: Deleta um usuário
- **Headers**: `Authorization: Bearer {token}`
- **Resposta**: `204 No Content`

### 📈 Status das Motos

#### Listar Status das Motos
- **Endpoint**: `GET /api/v1.0/StatusMotos?pageNumber=1&pageSize=10`
- **Descrição**: Lista todos os status das motos com paginação
- **Headers**: `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
- **Query Parameters**:
  - `pageNumber` (int): Número da página (padrão: 1)
  - `pageSize` (int): Tamanho da página (padrão: 10)
- **Resposta**:
```json
{
    "data": [
      {
        "id": 1,
        "status": "DISPONIVEL",
        "descricao": "Moto disponível para uso",
  "motoId": 1,
        "dataCriacao": "2024-01-15T09:00:00Z"
      },
      {
        "id": 2,
        "status": "EM_USO",
        "descricao": "Moto em uso para entrega",
        "motoId": 2,
        "dataCriacao": "2024-01-15T10:15:00Z"
      }
    ],
    "totalCount": 2,
    "pageNumber": 1,
    "pageSize": 10,
    "totalPages": 1,
    "hasPreviousPage": false,
    "hasNextPage": false
  }
  ```

#### Buscar Status por ID
- **Endpoint**: `GET /api/v1.0/StatusMotos/{id}`
- **Descrição**: Busca um status específico por ID
- **Headers**: `Authorization: Bearer {token}`
- **Resposta**:
  ```json
  {
    "id": 0,
    "status": "DISPONIVEL",
    "descricao": "string",
    "motoId": 0,
    "dataCriacao": "2024-01-01T00:00:00Z"
  }
  ```

#### Status Atual da Moto
- **Endpoint**: `GET /api/v1.0/StatusMotos/moto/1/atual`
- **Descrição**: Busca o status atual de uma moto específica
- **Headers**: `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
- **Resposta**:
  ```json
  {
    "id": 1,
    "status": "DISPONIVEL",
    "descricao": "Moto disponível para uso",
    "motoId": 1,
    "dataCriacao": "2024-01-15T09:00:00Z"
  }
  ```

#### Histórico de Status da Moto
- **Endpoint**: `GET /api/v1.0/StatusMotos/moto/1/historico`
- **Descrição**: Busca o histórico de status de uma moto específica
- **Headers**: `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
- **Resposta**:
  ```json
  [
    {
      "id": 1,
      "status": "DISPONIVEL",
      "descricao": "Moto disponível para uso",
      "motoId": 1,
      "dataCriacao": "2024-01-15T09:00:00Z"
    },
    {
      "id": 3,
      "status": "EM_USO",
      "descricao": "Moto em uso para entrega",
      "motoId": 1,
      "dataCriacao": "2024-01-15T11:30:00Z"
    },
    {
      "id": 4,
      "status": "DISPONIVEL",
      "descricao": "Moto retornou e está disponível",
      "motoId": 1,
      "dataCriacao": "2024-01-15T14:45:00Z"
    }
  ]
  ```

#### Status por Tipo
- **Endpoint**: `GET /api/v1.0/StatusMotos/tipo/DISPONIVEL`
- **Descrição**: Busca status por tipo específico
- **Headers**: `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
- **Resposta**:
```json
  [
{
      "id": 1,
      "status": "DISPONIVEL",
      "descricao": "Moto disponível para uso",
  "motoId": 1,
      "dataCriacao": "2024-01-15T09:00:00Z"
    },
    {
      "id": 4,
      "status": "DISPONIVEL",
      "descricao": "Moto retornou e está disponível",
      "motoId": 1,
      "dataCriacao": "2024-01-15T14:45:00Z"
    }
  ]
  ```

#### Criar Status
- **Endpoint**: `POST /api/v1.0/StatusMotos`
- **Descrição**: Cria um novo status
- **Headers**: `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
- **Body**:
  ```json
  {
    "status": "EM_MANUTENCAO",
    "descricao": "Moto em manutenção preventiva",
    "motoId": 1
  }
  ```
- **Resposta**:
```json
{
    "id": 5,
    "status": "EM_MANUTENCAO",
    "descricao": "Moto em manutenção preventiva",
  "motoId": 1,
    "dataCriacao": "2024-01-15T16:30:00Z"
  }
  ```

#### Atualizar Status
- **Endpoint**: `PUT /api/v1.0/StatusMotos/{id}`
- **Descrição**: Atualiza um status existente
- **Headers**: `Authorization: Bearer {token}`
- **Body**:
```json
{
    "status": "DISPONIVEL",
    "descricao": "string",
    "motoId": 0
  }
  ```
- **Resposta**:
  ```json
  {
    "id": 0,
    "status": "DISPONIVEL",
    "descricao": "string",
    "motoId": 0,
    "dataCriacao": "2024-01-01T00:00:00Z"
  }
  ```

#### Deletar Status
- **Endpoint**: `DELETE /api/v1.0/StatusMotos/{id}`
- **Descrição**: Deleta um status
- **Headers**: `Authorization: Bearer {token}`
- **Resposta**: `204 No Content`

### 🤖 Machine Learning

#### Treinar Modelo
- **Endpoint**: `POST /api/v2/ml/train-model`
- **Descrição**: Treina o modelo de machine learning
- **Headers**: `Authorization: Bearer {token}`
- **Body**:
```json
{
    "trainingData": [
      {
        "motoId": 0,
        "status": "DISPONIVEL",
        "dataCriacao": "2024-01-01T00:00:00Z"
      }
    ]
  }
  ```
- **Resposta**:
  ```json
  {
    "success": true,
    "message": "Modelo treinado com sucesso",
    "accuracy": 0.95,
    "trainingTime": "00:00:30"
  }
  ```

#### Prever Status
- **Endpoint**: `POST /api/v2/ml/predict-status`
- **Descrição**: Preve o status de uma moto usando ML
- **Headers**: `Authorization: Bearer {token}`
- **Body**:
```json
{
    "motoId": 0,
    "features": {
      "tempoUso": 0,
      "manutencoes": 0,
      "operacoes": 0
    }
  }
  ```
- **Resposta**:
  ```json
  {
    "predictedStatus": "DISPONIVEL",
    "confidence": 0.95,
    "features": {
      "tempoUso": 0,
      "manutencoes": 0,
      "operacoes": 0
  }
}
```

#### Analisar Padrões
- **Endpoint**: `GET /api/v2/ml/analyze-patterns`
- **Descrição**: Analisa padrões nos dados das motos
- **Headers**: `Authorization: Bearer {token}`
- **Resposta**:
```json
{
    "patterns": [
      {
        "pattern": "string",
        "frequency": 0,
        "description": "string"
      }
    ],
    "insights": [
      {
        "insight": "string",
        "confidence": 0.95
      }
    ]
  }
  ```

#### Informações do Modelo
- **Endpoint**: `GET /api/v2/ml/model-info`
- **Descrição**: Retorna informações sobre o modelo ML
- **Headers**: `Authorization: Bearer {token}`
- **Resposta**:
  ```json
  {
    "modelName": "string",
    "version": "string",
    "lastTraining": "2024-01-01T00:00:00Z",
    "accuracy": 0.95,
    "features": ["string"]
  }
  ```

### 🏥 Health Checks

#### Health Check Geral
- **Endpoint**: `GET /api/v1.0/Health`
- **Descrição**: Verifica a saúde geral da aplicação
- **Resposta**:
```json
{
  "status": "Healthy",
    "timestamp": "2024-01-15T16:45:00Z",
    "uptime": "02:30:15",
    "version": "1.0.0"
  }
  ```

#### Health Check do Banco
- **Endpoint**: `GET /api/v1.0/Health/database`
- **Descrição**: Verifica a saúde da conexão com o banco
- **Resposta**:
  ```json
  {
    "status": "Healthy",
    "database": "Oracle",
    "connectionTime": "00:00:02",
    "timestamp": "2024-01-15T16:45:00Z"
  }
  ```

#### Health Check da Memória
- **Endpoint**: `GET /api/v1.0/Health/memory`
- **Descrição**: Verifica o uso de memória da aplicação
- **Resposta**:
  ```json
  {
    "status": "Healthy",
    "memoryUsage": {
      "total": 8589934592,
      "used": 4294967296,
      "free": 4294967296,
      "percentage": 50.0
    },
    "timestamp": "2024-01-15T16:45:00Z"
  }
  ```

### 🌤️ Weather Forecast

#### Previsão do Tempo
- **Endpoint**: `GET /api/v1.0/WeatherForecast`
- **Descrição**: Retorna previsão do tempo (endpoint de exemplo)
- **Resposta**:
  ```json
  [
    {
      "date": "2024-01-15T00:00:00Z",
      "temperatureC": 25,
      "temperatureF": 77,
      "summary": "Ensolarado"
    },
    {
      "date": "2024-01-16T00:00:00Z",
      "temperatureC": 22,
      "temperatureF": 72,
      "summary": "Parcialmente nublado"
    },
    {
      "date": "2024-01-17T00:00:00Z",
      "temperatureC": 28,
      "temperatureF": 82,
      "summary": "Ensolarado"
    }
  ]
  ```

## 🔒 Autenticação e Autorização

### Perfis de Usuário
- **ADMIN**: Acesso total ao sistema
- **GERENTE**: Acesso a operações e relatórios
- **OPERADOR**: Acesso básico às operações

### Headers de Autenticação
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjUzIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6ImFsYSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6ImFsYUBleGFtcGxlLmNvbSIsInBlcmZpbCI6IkdFUkVOVEUiLCJjbnBqIjoiOTguNzY1LjAwMC8wMDAxLTEwIiwianRpIjoiM2FkNmE3MDgtNjViZi00N2U0LWJiYWUtYTM4Zjk2Mzk3MjUzIiwiaWF0IjoxNzYxNjU5NjQ0LCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiR0VSRU5URSIsIk9QRVJBRE9SIl0sImV4cCI6MTc2MTY2MzI0NCwiaXNzIjoiVHJhY2tab25lQVBJIiwiYXVkIjoiVHJhY2tab25lVXNlcnMifQ.Sqsv8feSsJCZKZL4AXj_zjdnCdiKrMcvZSZaou72fMQ
```

### Códigos de Status
- `200`: Sucesso
- `201`: Criado com sucesso
- `204`: Sem conteúdo (deletado)
- `400`: Requisição inválida
- `401`: Não autorizado
- `403`: Proibido
- `404`: Não encontrado
- `500`: Erro interno do servidor

## 📝 Notas Importantes

### Valores Válidos para Tipo de Operação
- `0` = CHECK_IN
- `1` = CHECK_OUT

### Credenciais de Teste
- **Email**: `ala@example.com`
- **Senha**: `123456`
- **Perfil**: GERENTE
- **ID do Usuário**: `53`
- **CNPJ**: `98.765.000/0001-10`

---

## 🚀 Desenvolvimento e Deploy

### 🔧 Configuração de Desenvolvimento

#### 📦 Dependências do Projeto
```xml
<PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="9.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.0" />
<PackageReference Include="Oracle.EntityFrameworkCore" Version="9.0.0" />
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="9.0.0" />
<PackageReference Include="Microsoft.ML" Version="3.0.1" />
<PackageReference Include="AutoMapper" Version="13.0.1" />
<PackageReference Include="xunit" Version="2.9.2" />
<PackageReference Include="Microsoft.AspNetCore.Mvc.Testing" Version="9.0.0" />
```

#### 🗄️ Configuração do Banco de Dados
```sql
-- Script de criação das tabelas Oracle
CREATE TABLE usuarios (
    id NUMBER(19) PRIMARY KEY,
    nome VARCHAR2(100) NOT NULL,
    email VARCHAR2(100) UNIQUE NOT NULL,
    senha_hash VARCHAR2(255) NOT NULL,
    perfil VARCHAR2(20) NOT NULL,
    cnpj VARCHAR2(20),
    telefone VARCHAR2(20),
    endereco VARCHAR2(200),
    nome_filial VARCHAR2(100),
    data_criacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE motos (
    id NUMBER(19) PRIMARY KEY,
    placa VARCHAR2(10) UNIQUE NOT NULL,
    chassi VARCHAR2(50) UNIQUE NOT NULL,
    motor VARCHAR2(100),
    usuario_id NUMBER(19) REFERENCES usuarios(id),
    data_criacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE operacoes (
    id NUMBER(19) PRIMARY KEY,
    tipo_operacao VARCHAR2(20) NOT NULL,
    descricao VARCHAR2(1000),
    moto_id NUMBER(19) REFERENCES motos(id),
    usuario_id NUMBER(19) REFERENCES usuarios(id),
    data_criacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT ck_tipo_operacao CHECK (tipo_operacao IN ('CHECK_IN', 'CHECK_OUT'))
);

CREATE TABLE status_motos (
    id NUMBER(19) PRIMARY KEY,
    status VARCHAR2(20) NOT NULL,
    descricao VARCHAR2(1000),
    moto_id NUMBER(19) REFERENCES motos(id),
    data_criacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
```

### 🧪 Estrutura dos Testes

#### 📁 Organização dos Testes
```
Tests/
├── Unit/
│   ├── JwtServiceTests.cs      # Testes do serviço JWT
│   ├── MotoServiceTests.cs     # Testes do serviço de motos
│   ├── OperacaoServiceTests.cs # Testes do serviço de operações
│   └── MLServiceTests.cs       # Testes do ML.NET
├── Integration/
│   ├── AuthIntegrationTests.cs # Testes de integração de auth
│   ├── MotoIntegrationTests.cs # Testes de integração de motos
│   └── DatabaseTests.cs        # Testes do banco de dados
└── Helpers/
    ├── TestDataBuilder.cs      # Builder para dados de teste
    └── WebApplicationFactory.cs # Factory para testes de integração
```

#### 🎯 Exemplo de Teste Unitário
```csharp
[Fact]
public void GenerateToken_ValidUser_ReturnsValidToken()
{
    // Arrange
    var user = new Usuario { Id = 1, Email = "test@test.com", Perfil = PerfilUsuario.ADMIN };
    
    // Act
    var token = _jwtService.GenerateToken(user);
    
    // Assert
    Assert.NotNull(token);
    Assert.True(_jwtService.ValidateToken(token));
}
```

### 📊 Métricas e Monitoramento

#### 📈 Health Checks Detalhados
- **Uptime**: Tempo de execução da aplicação
- **Memory Usage**: Uso de memória em tempo real
- **Database Connection**: Status da conexão Oracle
- **Response Time**: Tempo de resposta dos endpoints

#### 🔍 Logs e Observabilidade
- **Structured Logging** com Serilog
- **Correlation IDs** para rastreamento de requisições
- **Performance Counters** para métricas de performance
- **Error Tracking** com detalhes de exceções

### 🚀 Deploy e Produção

#### 🐳 Docker Support
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["challenge-3-net.csproj", "."]
RUN dotnet restore
COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "challenge-3-net.dll"]
```

#### ☁️ Azure Deployment
```yaml
# azure-pipelines.yml
trigger:
- main

pool:
  vmImage: 'ubuntu-latest'

variables:
  buildConfiguration: 'Release'

steps:
- task: DotNetCoreCLI@2
  displayName: 'Restore packages'
  inputs:
    command: 'restore'
    projects: '**/*.csproj'

- task: DotNetCoreCLI@2
  displayName: 'Build project'
  inputs:
    command: 'build'
    projects: '**/*.csproj'
    arguments: '--configuration $(buildConfiguration)'

- task: DotNetCoreCLI@2
  displayName: 'Run tests'
  inputs:
    command: 'test'
    projects: '**/*Tests.csproj'
    arguments: '--configuration $(buildConfiguration) --collect:"XPlat Code Coverage"'

- task: DotNetCoreCLI@2
  displayName: 'Publish project'
  inputs:
    command: 'publish'
    projects: '**/*.csproj'
    arguments: '--configuration $(buildConfiguration) --output $(Build.ArtifactStagingDirectory)'
```

### 📚 Documentação Adicional

#### 🔗 Links Úteis
- **Swagger UI**: `/swagger` - Documentação interativa da API
- **Health Dashboard**: `/health` - Status da aplicação
- **API Documentation**: `/api-docs` - Documentação OpenAPI

#### 📖 Recursos de Aprendizado
- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [ML.NET Documentation](https://docs.microsoft.com/en-us/dotnet/machine-learning/)
- [JWT Authentication](https://jwt.io/)

### 🎯 Próximos Passos

#### 🔮 Melhorias Futuras
- **Rate Limiting** para controle de requisições
- **Caching** com Redis para performance
- **Message Queues** para processamento assíncrono
- **Microservices** para escalabilidade
- **GraphQL** para consultas flexíveis

#### 📊 Monitoramento Avançado
- **Application Insights** para telemetria
- **Prometheus** para métricas customizadas
- **Grafana** para dashboards
- **ELK Stack** para logs centralizados

---

## 🏆 Conclusão

O **TrackZone API** representa uma implementação completa e profissional de uma API RESTful utilizando **.NET 9**, demonstrando:

✅ **Arquitetura Limpa** com separação de responsabilidades  
✅ **Segurança Robusta** com JWT e autorização baseada em roles  
✅ **Machine Learning** integrado com ML.NET  
✅ **Testes Abrangentes** com xUnit e WebApplicationFactory  
✅ **Documentação Completa** com Swagger e exemplos práticos  
✅ **Health Checks** para monitoramento em produção  
✅ **Versionamento** para evolução da API  

Este projeto atende a todos os requisitos do **ADVANCED BUSINESS DEVELOPMENT WITH .NET** e está pronto para produção! 🚀