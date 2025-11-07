# 🧪 Guia Completo de Testes - TrackZone API

## 📋 Índice
1. [Pré-requisitos](#pré-requisitos)
2. [Iniciando a API](#iniciando-a-api)
3. [PASSO 1: Login e Autenticação](#passo-1-login-e-autenticação)
4. [PASSO 2: Testando Endpoints v1.0](#passo-2-testando-endpoints-v10)
5. [PASSO 3: Testando Endpoints v2.0](#passo-3-testando-endpoints-v20)
6. [PASSO 4: Testando Health Checks](#passo-4-testando-health-checks)
7. [PASSO 5: Testando Machine Learning (v2.0)](#passo-5-testando-machine-learning-v20)

---

## 🔧 Pré-requisitos

1. **.NET 9 SDK** instalado
2. **Postman**, **Insomnia** ou **curl** para fazer requisições
3. **API rodando** (execute `dotnet run` na pasta do projeto)
4. **URL Base**: `https://localhost:5001` ou `http://localhost:5000`

---

## 🚀 Iniciando a API

### 1. Abra o terminal na pasta do projeto:
```powershell
cd C:\Users\crist\Desktop\TUDO\ablablabla\challenge3-net
```

### 2. Execute a API:
```powershell
dotnet run
```

### 3. Aguarde a mensagem:
```
Now listening on: https://localhost:5001
Now listening on: http://localhost:5000
```

### 4. Acesse o Swagger (opcional):
```
https://localhost:5001/swagger
```

---

## 🔐 PASSO 1: Login e Autenticação

### ⚠️ IMPORTANTE: O AuthController está na v2.0!

### 1.1 - Fazer Login

**Endpoint:** `POST /api/v2.0/Auth/login`

**URL Completa:** `https://localhost:5001/api/v2.0/Auth/login`

**Headers:**
```
Content-Type: application/json
```

**Body (JSON):**
```json
{
  "email": "ala@example.com",
  "senha": "123456"
}
```

**Exemplo com curl (PowerShell):**
```powershell
$body = @{
    email = "ala@example.com"
    senha = "123456"
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://localhost:5001/api/v2.0/Auth/login" `
    -Method POST `
    -ContentType "application/json" `
    -Body $body `
    -SkipCertificateCheck
```

**Resposta Esperada (200 OK):**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "tokenType": "Bearer",
  "expiresIn": 3600,
  "message": "Login realizado com sucesso"
}
```

**⚠️ GUARDE O TOKEN!** Você vai precisar dele para os próximos passos.

---

### 1.2 - Validar Token

**Endpoint:** `POST /api/v2.0/Auth/validate-token`

**URL Completa:** `https://localhost:5001/api/v2.0/Auth/validate-token`

**Headers:**
```
Authorization: Bearer SEU_TOKEN_AQUI
Content-Type: application/json
```

**Exemplo com curl (PowerShell):**
```powershell
$token = "SEU_TOKEN_AQUI"

Invoke-RestMethod -Uri "https://localhost:5001/api/v2.0/Auth/validate-token" `
    -Method POST `
    -Headers @{
        "Authorization" = "Bearer $token"
        "Content-Type" = "application/json"
    } `
    -SkipCertificateCheck
```

**Resposta Esperada (200 OK):**
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

---

### 1.3 - Obter Informações do Usuário

**Endpoint:** `GET /api/v2.0/Auth/user-info`

**URL Completa:** `https://localhost:5001/api/v2.0/Auth/user-info`

**Headers:**
```
Authorization: Bearer SEU_TOKEN_AQUI
```

**Exemplo com curl (PowerShell):**
```powershell
$token = "SEU_TOKEN_AQUI"

Invoke-RestMethod -Uri "https://localhost:5001/api/v2.0/Auth/user-info" `
    -Method GET `
    -Headers @{
        "Authorization" = "Bearer $token"
    } `
    -SkipCertificateCheck
```

**Resposta Esperada (200 OK):**
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

---

## 📦 PASSO 2: Testando Endpoints v1.0

### ⚠️ IMPORTANTE: Todos os endpoints abaixo precisam do token JWT no header!

**Header obrigatório para todos:**
```
Authorization: Bearer SEU_TOKEN_AQUI
```

---

### 2.1 - Health Checks (v1.0)

#### 2.1.1 - Health Check Geral
**Endpoint:** `GET /api/v1.0/Health`

**URL Completa:** `https://localhost:5001/api/v1.0/Health`

**Exemplo:**
```powershell
Invoke-RestMethod -Uri "https://localhost:5001/api/v1.0/Health" `
    -Method GET `
    -SkipCertificateCheck
```

**Resposta Esperada:**
```json
{
  "status": "Healthy",
  "timestamp": "2024-01-15T16:45:00Z",
  "uptime": "02:30:15",
  "version": "1.0.0"
}
```

#### 2.1.2 - Health Check do Banco
**Endpoint:** `GET /api/v1.0/Health/database`

**URL Completa:** `https://localhost:5001/api/v1.0/Health/database`

**Exemplo:**
```powershell
Invoke-RestMethod -Uri "https://localhost:5001/api/v1.0/Health/database" `
    -Method GET `
    -SkipCertificateCheck
```

#### 2.1.3 - Health Check da Memória
**Endpoint:** `GET /api/v1.0/Health/memory`

**URL Completa:** `https://localhost:5001/api/v1.0/Health/memory`

**Exemplo:**
```powershell
Invoke-RestMethod -Uri "https://localhost:5001/api/v1.0/Health/memory" `
    -Method GET `
    -SkipCertificateCheck
```

---

### 2.2 - Motos (v1.0)

#### 2.2.1 - Listar Motos
**Endpoint:** `GET /api/v1.0/Motos?pageNumber=1&pageSize=10`

**URL Completa:** `https://localhost:5001/api/v1.0/Motos?pageNumber=1&pageSize=10`

**Headers:**
```
Authorization: Bearer SEU_TOKEN_AQUI
```

**Exemplo:**
```powershell
$token = "SEU_TOKEN_AQUI"

Invoke-RestMethod -Uri "https://localhost:5001/api/v1.0/Motos?pageNumber=1&pageSize=10" `
    -Method GET `
    -Headers @{
        "Authorization" = "Bearer $token"
    } `
    -SkipCertificateCheck
```

**Resposta Esperada:**
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

#### 2.2.2 - Buscar Moto por ID
**Endpoint:** `GET /api/v1.0/Motos/{id}`

**URL Completa:** `https://localhost:5001/api/v1.0/Motos/1`

**Exemplo:**
```powershell
$token = "SEU_TOKEN_AQUI"

Invoke-RestMethod -Uri "https://localhost:5001/api/v1.0/Motos/1" `
    -Method GET `
    -Headers @{
        "Authorization" = "Bearer $token"
    } `
    -SkipCertificateCheck
```

#### 2.2.3 - Criar Moto
**Endpoint:** `POST /api/v1.0/Motos`

**URL Completa:** `https://localhost:5001/api/v1.0/Motos`

**Headers:**
```
Authorization: Bearer SEU_TOKEN_AQUI
Content-Type: application/json
```

**Body (JSON):**
```json
{
  "placa": "DEF-5678",
  "chassi": "CHASSI567890123",
  "motor": "Motor 3.0",
  "usuarioId": 53
}
```

**Exemplo:**
```powershell
$token = "SEU_TOKEN_AQUI"
$body = @{
    placa = "DEF-5678"
    chassi = "CHASSI567890123"
    motor = "Motor 3.0"
    usuarioId = 53
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://localhost:5001/api/v1.0/Motos" `
    -Method POST `
    -Headers @{
        "Authorization" = "Bearer $token"
        "Content-Type" = "application/json"
    } `
    -Body $body `
    -SkipCertificateCheck
```

#### 2.2.4 - Atualizar Moto
**Endpoint:** `PUT /api/v1.0/Motos/{id}`

**URL Completa:** `https://localhost:5001/api/v1.0/Motos/1`

**Body (JSON):**
```json
{
  "placa": "XYZ-9999",
  "chassi": "CHASSI999999999",
  "motor": "Motor Atualizado",
  "usuarioId": 53
}
```

#### 2.2.5 - Deletar Moto
**Endpoint:** `DELETE /api/v1.0/Motos/{id}`

**URL Completa:** `https://localhost:5001/api/v1.0/Motos/1`

---

### 2.3 - Usuários (v1.0)

#### 2.3.1 - Listar Usuários
**Endpoint:** `GET /api/v1.0/Usuarios?pageNumber=1&pageSize=10`

**URL Completa:** `https://localhost:5001/api/v1.0/Usuarios?pageNumber=1&pageSize=10`

**Exemplo:**
```powershell
$token = "SEU_TOKEN_AQUI"

Invoke-RestMethod -Uri "https://localhost:5001/api/v1.0/Usuarios?pageNumber=1&pageSize=10" `
    -Method GET `
    -Headers @{
        "Authorization" = "Bearer $token"
    } `
    -SkipCertificateCheck
```

#### 2.3.2 - Buscar Usuário por ID
**Endpoint:** `GET /api/v1.0/Usuarios/{id}`

**URL Completa:** `https://localhost:5001/api/v1.0/Usuarios/53`

#### 2.3.3 - Criar Usuário
**Endpoint:** `POST /api/v1.0/Usuarios`

**Body (JSON):**
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

#### 2.3.4 - Atualizar Usuário
**Endpoint:** `PUT /api/v1.0/Usuarios/{id}`

#### 2.3.5 - Deletar Usuário
**Endpoint:** `DELETE /api/v1.0/Usuarios/{id}`

---

### 2.4 - Operações (v1.0)

#### 2.4.1 - Listar Operações
**Endpoint:** `GET /api/v1.0/Operacoes?pageNumber=1&pageSize=10`

**URL Completa:** `https://localhost:5001/api/v1.0/Operacoes?pageNumber=1&pageSize=10`

#### 2.4.2 - Buscar Operação por ID
**Endpoint:** `GET /api/v1.0/Operacoes/{id}`

#### 2.4.3 - Criar Operação
**Endpoint:** `POST /api/v1.0/Operacoes`

**Body (JSON):**
```json
{
  "tipoOperacao": 0,
  "descricao": "Check-in da moto para entrega",
  "motoId": 1,
  "usuarioId": 53
}
```

**⚠️ Valores para tipoOperacao:**
- `0` = CHECK_IN
- `1` = CHECK_OUT

#### 2.4.4 - Atualizar Operação
**Endpoint:** `PUT /api/v1.0/Operacoes/{id}`

#### 2.4.5 - Deletar Operação
**Endpoint:** `DELETE /api/v1.0/Operacoes/{id}`

---

### 2.5 - Status das Motos (v1.0)

#### 2.5.1 - Listar Status
**Endpoint:** `GET /api/v1.0/StatusMotos?pageNumber=1&pageSize=10`

**URL Completa:** `https://localhost:5001/api/v1.0/StatusMotos?pageNumber=1&pageSize=10`

#### 2.5.2 - Buscar Status por ID
**Endpoint:** `GET /api/v1.0/StatusMotos/{id}`

#### 2.5.3 - Status Atual da Moto
**Endpoint:** `GET /api/v1.0/StatusMotos/moto/{motoId}/atual`

**URL Completa:** `https://localhost:5001/api/v1.0/StatusMotos/moto/1/atual`

#### 2.5.4 - Histórico de Status da Moto
**Endpoint:** `GET /api/v1.0/StatusMotos/moto/{motoId}/historico`

**URL Completa:** `https://localhost:5001/api/v1.0/StatusMotos/moto/1/historico`

#### 2.5.5 - Status por Tipo
**Endpoint:** `GET /api/v1.0/StatusMotos/tipo/{tipo}`

**URL Completa:** `https://localhost:5001/api/v1.0/StatusMotos/tipo/DISPONIVEL`

**Valores possíveis para tipo:**
- `DISPONIVEL`
- `EM_USO`
- `EM_MANUTENCAO`
- `INDISPONIVEL`

#### 2.5.6 - Criar Status
**Endpoint:** `POST /api/v1.0/StatusMotos`

**Body (JSON):**
```json
{
  "status": "EM_MANUTENCAO",
  "descricao": "Moto em manutenção preventiva",
  "motoId": 1
}
```

#### 2.5.7 - Atualizar Status
**Endpoint:** `PUT /api/v1.0/StatusMotos/{id}`

#### 2.5.8 - Deletar Status
**Endpoint:** `DELETE /api/v1.0/StatusMotos/{id}`

---

## 🚀 PASSO 3: Testando Endpoints v2.0

### ⚠️ IMPORTANTE: Todos os endpoints abaixo precisam do token JWT no header!

**Header obrigatório para todos:**
```
Authorization: Bearer SEU_TOKEN_AQUI
```

---

### 3.1 - Health Checks (v2.0)

Os mesmos endpoints da v1.0, mas usando `/api/v2.0/Health`:

- `GET /api/v2.0/Health`
- `GET /api/v2.0/Health/database`
- `GET /api/v2.0/Health/memory`

---

### 3.2 - Motos (v2.0)

Todos os endpoints de motos da v1.0, mas usando `/api/v2.0/Motos`:

- `GET /api/v2.0/Motos?pageNumber=1&pageSize=10`
- `GET /api/v2.0/Motos/{id}`
- `POST /api/v2.0/Motos`
- `PUT /api/v2.0/Motos/{id}`
- `DELETE /api/v2.0/Motos/{id}`

**Exemplo:**
```powershell
$token = "SEU_TOKEN_AQUI"

Invoke-RestMethod -Uri "https://localhost:5001/api/v2.0/Motos?pageNumber=1&pageSize=10" `
    -Method GET `
    -Headers @{
        "Authorization" = "Bearer $token"
    } `
    -SkipCertificateCheck
```

---

### 3.3 - Usuários (v2.0)

Todos os endpoints de usuários da v1.0, mas usando `/api/v2.0/Usuarios`:

- `GET /api/v2.0/Usuarios?pageNumber=1&pageSize=10`
- `GET /api/v2.0/Usuarios/{id}`
- `POST /api/v2.0/Usuarios`
- `PUT /api/v2.0/Usuarios/{id}`
- `DELETE /api/v2.0/Usuarios/{id}`

---

### 3.4 - Operações (v2.0)

Todos os endpoints de operações da v1.0, mas usando `/api/v2.0/Operacoes`:

- `GET /api/v2.0/Operacoes?pageNumber=1&pageSize=10`
- `GET /api/v2.0/Operacoes/{id}`
- `POST /api/v2.0/Operacoes`
- `PUT /api/v2.0/Operacoes/{id}`
- `DELETE /api/v2.0/Operacoes/{id}`

---

### 3.5 - Status das Motos (v2.0)

Todos os endpoints de status da v1.0, mas usando `/api/v2.0/StatusMotos`:

- `GET /api/v2.0/StatusMotos?pageNumber=1&pageSize=10`
- `GET /api/v2.0/StatusMotos/{id}`
- `GET /api/v2.0/StatusMotos/moto/{motoId}/atual`
- `GET /api/v2.0/StatusMotos/moto/{motoId}/historico`
- `GET /api/v2.0/StatusMotos/tipo/{tipo}`
- `POST /api/v2.0/StatusMotos`
- `PUT /api/v2.0/StatusMotos/{id}`
- `DELETE /api/v2.0/StatusMotos/{id}`

---

## 🤖 PASSO 4: Testando Machine Learning (v2.0)

### ⚠️ IMPORTANTE: Todos os endpoints ML precisam do token JWT no header!

**Header obrigatório para todos:**
```
Authorization: Bearer SEU_TOKEN_AQUI
```

---

### 4.1 - Treinar Modelo

**Endpoint:** `POST /api/v2.0/ml/train-model`

**URL Completa:** `https://localhost:5001/api/v2.0/ml/train-model`

**Headers:**
```
Authorization: Bearer SEU_TOKEN_AQUI
Content-Type: application/json
```

**Body (JSON):**
```json
{
  "trainingData": [
    {
      "motoId": 1,
      "status": "DISPONIVEL",
      "dataCriacao": "2024-01-15T00:00:00Z"
    },
    {
      "motoId": 2,
      "status": "EM_USO",
      "dataCriacao": "2024-01-15T01:00:00Z"
    }
  ]
}
```

**Exemplo:**
```powershell
$token = "SEU_TOKEN_AQUI"
$body = @{
    trainingData = @(
        @{
            motoId = 1
            status = "DISPONIVEL"
            dataCriacao = "2024-01-15T00:00:00Z"
        },
        @{
            motoId = 2
            status = "EM_USO"
            dataCriacao = "2024-01-15T01:00:00Z"
        }
    )
} | ConvertTo-Json -Depth 10

Invoke-RestMethod -Uri "https://localhost:5001/api/v2.0/ml/train-model" `
    -Method POST `
    -Headers @{
        "Authorization" = "Bearer $token"
        "Content-Type" = "application/json"
    } `
    -Body $body `
    -SkipCertificateCheck
```

**Resposta Esperada:**
```json
{
  "success": true,
  "message": "Modelo treinado com sucesso",
  "accuracy": 0.95,
  "trainingTime": "00:00:30"
}
```

---

### 4.2 - Prever Status

**Endpoint:** `POST /api/v2.0/ml/predict-status`

**URL Completa:** `https://localhost:5001/api/v2.0/ml/predict-status`

**Headers:**
```
Authorization: Bearer SEU_TOKEN_AQUI
Content-Type: application/json
```

**Body (JSON):**
```json
{
  "motoId": 1,
  "features": {
    "tempoUso": 120,
    "manutencoes": 2,
    "operacoes": 15
  }
}
```

**Exemplo:**
```powershell
$token = "SEU_TOKEN_AQUI"
$body = @{
    motoId = 1
    features = @{
        tempoUso = 120
        manutencoes = 2
        operacoes = 15
    }
} | ConvertTo-Json -Depth 10

Invoke-RestMethod -Uri "https://localhost:5001/api/v2.0/ml/predict-status" `
    -Method POST `
    -Headers @{
        "Authorization" = "Bearer $token"
        "Content-Type" = "application/json"
    } `
    -Body $body `
    -SkipCertificateCheck
```

**Resposta Esperada:**
```json
{
  "predictedStatus": "DISPONIVEL",
  "confidence": 0.95,
  "features": {
    "tempoUso": 120,
    "manutencoes": 2,
    "operacoes": 15
  }
}
```

---

### 4.3 - Analisar Padrões

**Endpoint:** `GET /api/v2.0/ml/analyze-patterns`

**URL Completa:** `https://localhost:5001/api/v2.0/ml/analyze-patterns`

**Headers:**
```
Authorization: Bearer SEU_TOKEN_AQUI
```

**Exemplo:**
```powershell
$token = "SEU_TOKEN_AQUI"

Invoke-RestMethod -Uri "https://localhost:5001/api/v2.0/ml/analyze-patterns" `
    -Method GET `
    -Headers @{
        "Authorization" = "Bearer $token"
    } `
    -SkipCertificateCheck
```

**Resposta Esperada:**
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

---

### 4.4 - Informações do Modelo

**Endpoint:** `GET /api/v2.0/ml/model-info`

**URL Completa:** `https://localhost:5001/api/v2.0/ml/model-info`

**Headers:**
```
Authorization: Bearer SEU_TOKEN_AQUI
```

**Exemplo:**
```powershell
$token = "SEU_TOKEN_AQUI"

Invoke-RestMethod -Uri "https://localhost:5001/api/v2.0/ml/model-info" `
    -Method GET `
    -Headers @{
        "Authorization" = "Bearer $token"
    } `
    -SkipCertificateCheck
```

**Resposta Esperada:**
```json
{
  "modelName": "string",
  "version": "string",
  "lastTraining": "2024-01-01T00:00:00Z",
  "accuracy": 0.95,
  "features": ["string"]
}
```

---

## 🏥 PASSO 5: Testando Health Checks

### 5.1 - Health Check Geral (sem autenticação)

**Endpoint:** `GET /health`

**URL Completa:** `https://localhost:5001/health`

**Exemplo:**
```powershell
Invoke-RestMethod -Uri "https://localhost:5001/health" `
    -Method GET `
    -SkipCertificateCheck
```

---

### 5.2 - Health Check do Banco (sem autenticação)

**Endpoint:** `GET /health/database`

**URL Completa:** `https://localhost:5001/health/database`

**Exemplo:**
```powershell
Invoke-RestMethod -Uri "https://localhost:5001/health/database" `
    -Method GET `
    -SkipCertificateCheck
```

---

### 5.3 - Health Check Ready (sem autenticação)

**Endpoint:** `GET /health/ready`

**URL Completa:** `https://localhost:5001/health/ready`

**Exemplo:**
```powershell
Invoke-RestMethod -Uri "https://localhost:5001/health/ready" `
    -Method GET `
    -SkipCertificateCheck
```

---

### 5.4 - Health Check Live (sem autenticação)

**Endpoint:** `GET /health/live`

**URL Completa:** `https://localhost:5001/health/live`

**Exemplo:**
```powershell
Invoke-RestMethod -Uri "https://localhost:5001/health/live" `
    -Method GET `
    -SkipCertificateCheck
```

---

## 📝 Resumo dos Endpoints

### 🔐 Autenticação (v2.0 apenas)
- `POST /api/v2.0/Auth/login` - Fazer login
- `POST /api/v2.0/Auth/validate-token` - Validar token
- `GET /api/v2.0/Auth/user-info` - Informações do usuário

### 🏥 Health Checks
- `GET /api/v1.0/Health` - Health check geral (v1.0)
- `GET /api/v1.0/Health/database` - Health check do banco (v1.0)
- `GET /api/v1.0/Health/memory` - Health check da memória (v1.0)
- `GET /api/v2.0/Health` - Health check geral (v2.0)
- `GET /api/v2.0/Health/database` - Health check do banco (v2.0)
- `GET /api/v2.0/Health/memory` - Health check da memória (v2.0)
- `GET /health` - Health check geral (sem versão)
- `GET /health/database` - Health check do banco (sem versão)
- `GET /health/ready` - Health check ready
- `GET /health/live` - Health check live

### 🏍️ Motos (v1.0 e v2.0)
- `GET /api/v{version}/Motos` - Listar motos
- `GET /api/v{version}/Motos/{id}` - Buscar moto por ID
- `POST /api/v{version}/Motos` - Criar moto
- `PUT /api/v{version}/Motos/{id}` - Atualizar moto
- `DELETE /api/v{version}/Motos/{id}` - Deletar moto

### 👥 Usuários (v1.0 e v2.0)
- `GET /api/v{version}/Usuarios` - Listar usuários
- `GET /api/v{version}/Usuarios/{id}` - Buscar usuário por ID
- `POST /api/v{version}/Usuarios` - Criar usuário
- `PUT /api/v{version}/Usuarios/{id}` - Atualizar usuário
- `DELETE /api/v{version}/Usuarios/{id}` - Deletar usuário

### 📊 Operações (v1.0 e v2.0)
- `GET /api/v{version}/Operacoes` - Listar operações
- `GET /api/v{version}/Operacoes/{id}` - Buscar operação por ID
- `POST /api/v{version}/Operacoes` - Criar operação
- `PUT /api/v{version}/Operacoes/{id}` - Atualizar operação
- `DELETE /api/v{version}/Operacoes/{id}` - Deletar operação

### 📈 Status das Motos (v1.0 e v2.0)
- `GET /api/v{version}/StatusMotos` - Listar status
- `GET /api/v{version}/StatusMotos/{id}` - Buscar status por ID
- `GET /api/v{version}/StatusMotos/moto/{motoId}/atual` - Status atual da moto
- `GET /api/v{version}/StatusMotos/moto/{motoId}/historico` - Histórico de status
- `GET /api/v{version}/StatusMotos/tipo/{tipo}` - Status por tipo
- `POST /api/v{version}/StatusMotos` - Criar status
- `PUT /api/v{version}/StatusMotos/{id}` - Atualizar status
- `DELETE /api/v{version}/StatusMotos/{id}` - Deletar status

### 🤖 Machine Learning (v2.0 apenas)
- `POST /api/v2.0/ml/train-model` - Treinar modelo
- `POST /api/v2.0/ml/predict-status` - Prever status
- `GET /api/v2.0/ml/analyze-patterns` - Analisar padrões
- `GET /api/v2.0/ml/model-info` - Informações do modelo

---

## ⚠️ Dicas Importantes

1. **Token JWT**: O token expira em 1 hora (3600 segundos). Se receber erro 401, faça login novamente.

2. **HTTPS**: Se estiver usando `https://localhost:5001`, pode precisar usar `-SkipCertificateCheck` no PowerShell ou aceitar o certificado auto-assinado.

3. **Swagger**: Acesse `https://localhost:5001/swagger` para testar os endpoints diretamente no navegador.

4. **Credenciais de Teste**:
   - Email: `ala@example.com`
   - Senha: `123456`
   - Perfil: `GERENTE`

5. **Códigos de Status HTTP**:
   - `200` - Sucesso
   - `201` - Criado com sucesso
   - `204` - Sem conteúdo (deletado)
   - `400` - Requisição inválida
   - `401` - Não autorizado (token inválido ou ausente)
   - `403` - Proibido (sem permissão)
   - `404` - Não encontrado
   - `500` - Erro interno do servidor

---

## 🎯 Ordem Recomendada de Testes

1. ✅ **Iniciar a API** (`dotnet run`)
2. ✅ **Fazer Login** (`POST /api/v2.0/Auth/login`)
3. ✅ **Guardar o Token**
4. ✅ **Testar Health Checks** (sem token)
5. ✅ **Testar Endpoints v1.0** (com token)
6. ✅ **Testar Endpoints v2.0** (com token)
7. ✅ **Testar Machine Learning** (com token)

---

## 📞 Suporte

Se encontrar algum problema:

1. Verifique se a API está rodando
2. Verifique se o token está válido
3. Verifique os logs da aplicação
4. Verifique a conexão com o banco de dados
5. Acesse o Swagger para testar diretamente: `https://localhost:5001/swagger`

---

**Boa sorte nos testes! 🚀**

