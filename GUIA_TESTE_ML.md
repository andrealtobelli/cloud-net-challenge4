# 🧪 Guia de Teste do ML.NET

Este guia mostra como testar todos os endpoints de Machine Learning do projeto.

## 📋 Pré-requisitos

1. Aplicação rodando (`dotnet run`)
2. Banco de dados Oracle configurado
3. Token JWT válido (obtido via login)

## 🔐 Passo 1: Obter Token JWT

Primeiro, faça login para obter o token de autenticação:

### PowerShell
```powershell
$loginBody = @{
    email = "ala@example.com"
    senha = "123456"
} | ConvertTo-Json

$loginResponse = Invoke-RestMethod -Uri "http://localhost:5000/api/v2.0/Auth/login" `
    -Method POST `
    -ContentType "application/json" `
    -Body $loginBody

$token = $loginResponse.token
Write-Host "Token: $token"
```

### cURL
```bash
curl -X POST "http://localhost:5000/api/v2.0/Auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "ala@example.com",
    "senha": "123456"
  }'
```

### Swagger UI
1. Acesse `http://localhost:5000/swagger`
2. Vá para `/api/v2.0/Auth/login`
3. Clique em "Try it out"
4. Preencha os dados e execute
5. Copie o token da resposta

---

## 🤖 Teste 1: Obter Informações do Modelo

### PowerShell
```powershell
$headers = @{
    "Authorization" = "Bearer $token"
}

$modelInfo = Invoke-RestMethod -Uri "http://localhost:5000/api/v2.0/ML/model-info" `
    -Method GET `
    -Headers $headers

$modelInfo | ConvertTo-Json -Depth 10
```

### cURL
```bash
curl -X GET "http://localhost:5000/api/v2.0/ML/model-info" \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

### Resposta Esperada
```json
{
  "modelName": "Moto Status Prediction Model",
  "version": "1.0",
  "algorithm": "SDCA Maximum Entropy",
  "features": [
    "Perfil do Usuário",
    "Tipo de Operação",
    "Dias desde Criação",
    "Total de Operações"
  ],
  "targetVariable": "Status da Moto",
  "possibleStatuses": [
    "PENDENTE",
    "REPARO_SIMPLES",
    "DANOS_ESTRUTURAIS",
    "MOTOR_DEFEITUOSO",
    "MANUTENCAO_AGENDADA",
    "PRONTA",
    "SEM_PLACA",
    "ALUGADA",
    "AGUARDANDO_ALUGUEL"
  ],
  "description": "Modelo de Machine Learning para predição do status de motos...",
  "createdAt": "2024-01-15T10:00:00Z",
  "framework": "ML.NET 4.0.2"
}
```

---

## 🧠 Teste 2: Treinar o Modelo

### PowerShell
```powershell
$headers = @{
    "Authorization" = "Bearer $token"
    "Content-Type" = "application/json"
}

$trainResponse = Invoke-RestMethod -Uri "http://localhost:5000/api/v2.0/ML/train-model" `
    -Method POST `
    -Headers $headers

$trainResponse | ConvertTo-Json -Depth 10
```

### cURL
```bash
curl -X POST "http://localhost:5000/api/v2.0/ML/train-model" \
  -H "Authorization: Bearer YOUR_TOKEN_HERE" \
  -H "Content-Type: application/json"
```

### Resposta de Sucesso
```json
{
  "success": true,
  "message": "Modelo treinado com sucesso",
  "accuracy": 0.85,
  "recordsUsed": 150,
  "metrics": {
    "macroAccuracy": 0.85,
    "microAccuracy": 0.87,
    "logLoss": 0.45,
    "logLossReduction": 0.78
  }
}
```

### Resposta de Erro (Dados Insuficientes)
```json
{
  "success": false,
  "message": "Dados insuficientes para treinamento (mínimo 10 registros)",
  "accuracy": 0,
  "recordsUsed": 5
}
```

---

## 🔮 Teste 3: Predição de Status

### PowerShell
```powershell
$headers = @{
    "Authorization" = "Bearer $token"
    "Content-Type" = "application/json"
}

$predictionBody = @{
    perfil = "ADMIN"
    tipoOperacao = "CHECK_IN"
    diasDesdeCriacao = 30
    totalOperacoes = 5
} | ConvertTo-Json

$predictionResponse = Invoke-RestMethod -Uri "http://localhost:5000/api/v2.0/ML/predict-status" `
    -Method POST `
    -Headers $headers `
    -Body $predictionBody

$predictionResponse | ConvertTo-Json -Depth 10
```

### cURL
```bash
curl -X POST "http://localhost:5000/api/v2.0/ML/predict-status" \
  -H "Authorization: Bearer YOUR_TOKEN_HERE" \
  -H "Content-Type: application/json" \
  -d '{
    "perfil": "ADMIN",
    "tipoOperacao": "CHECK_IN",
    "diasDesdeCriacao": 30,
    "totalOperacoes": 5
  }'
```

### Resposta Esperada
```json
{
  "success": true,
  "message": "Predição realizada com sucesso",
  "predictedStatus": "PRONTA",
  "confidence": 0.92,
  "allScores": [0.05, 0.02, 0.01, 0.92, 0.00]
}
```

### Exemplos de Entrada

#### Exemplo 1: Moto Nova com Poucas Operações
```json
{
  "perfil": "OPERADOR",
  "tipoOperacao": "CHECK_IN",
  "diasDesdeCriacao": 5,
  "totalOperacoes": 1
}
```

#### Exemplo 2: Moto Antiga com Muitas Operações
```json
{
  "perfil": "GERENTE",
  "tipoOperacao": "CHECK_OUT",
  "diasDesdeCriacao": 180,
  "totalOperacoes": 50
}
```

---

## 📊 Teste 4: Análise de Padrões

### PowerShell
```powershell
$headers = @{
    "Authorization" = "Bearer $token"
}

$patternsResponse = Invoke-RestMethod -Uri "http://localhost:5000/api/v2.0/ML/analyze-patterns" `
    -Method GET `
    -Headers $headers

$patternsResponse | ConvertTo-Json -Depth 10
```

### cURL
```bash
curl -X GET "http://localhost:5000/api/v2.0/ML/analyze-patterns" \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

### Resposta Esperada
```json
{
  "success": true,
  "message": "Análise de padrões concluída com sucesso",
  "totalOperacoes": 150,
  "operacaoFrequencia": [
    { "tipo": "CHECK_IN", "count": 80 },
    { "tipo": "CHECK_OUT", "count": 70 }
  ],
  "operacaoPorHora": [
    { "hora": 9, "count": 25 },
    { "hora": 14, "count": 30 },
    { "hora": 18, "count": 20 }
  ],
  "operacaoPorUsuario": [
    { "usuarioId": 1, "count": 45 },
    { "usuarioId": 2, "count": 30 }
  ],
  "motosMaisAtivas": [
    { "motoId": 1, "count": 35 },
    { "motoId": 2, "count": 28 }
  ],
  "periodoAnalise": {
    "inicio": "2024-01-01T00:00:00Z",
    "fim": "2024-01-15T23:59:59Z"
  }
}
```

---

## 🚀 Teste Automatizado (Script PowerShell)

Execute o script completo:

```powershell
.\teste_ml.ps1
```

Este script:
1. ✅ Faz login automaticamente
2. ✅ Obtém informações do modelo
3. ✅ Treina o modelo
4. ✅ Faz predição de status
5. ✅ Analisa padrões

---

## 🧪 Testes Automatizados (xUnit)

### Executar Testes Unitários do ML
```bash
dotnet test --filter "MLServiceTests"
```

### Executar Todos os Testes
```bash
dotnet test
```

### Testes Disponíveis
- ✅ `TrainStatusPredictionModel_WithInsufficientData_ShouldReturnFailure`
- ✅ `AnalyzeOperationPatterns_WithData_ShouldReturnSuccess`
- ✅ `AnalyzeOperationPatterns_WithoutData_ShouldReturnFailure`
- ✅ `PredictMotoStatus_WithoutTrainedModel_ShouldAutoTrain`

---

## 📝 Swagger UI (Interface Visual)

1. **Acesse**: `http://localhost:5000/swagger`
2. **Selecione**: `v2.0` no dropdown
3. **Autorize**: Clique no botão "Authorize" e cole o token
4. **Teste os endpoints**:
   - `/api/v2.0/ML/model-info` (GET)
   - `/api/v2.0/ML/train-model` (POST)
   - `/api/v2.0/ML/predict-status` (POST)
   - `/api/v2.0/ML/analyze-patterns` (GET)

---

## ⚠️ Troubleshooting

### Erro 401 (Unauthorized)
- **Causa**: Token inválido ou expirado
- **Solução**: Faça login novamente para obter um novo token

### Erro 400 (Bad Request) no Treinamento
- **Causa**: Dados insuficientes no banco (mínimo 10 registros)
- **Solução**: Adicione mais dados ao banco de dados

### Erro 500 (Internal Server Error)
- **Causa**: Erro no servidor ou modelo não treinado
- **Solução**: Verifique os logs da aplicação

### Modelo não treinado ao fazer predição
- **Causa**: O modelo não foi treinado ainda
- **Solução**: O sistema treina automaticamente, mas pode falhar se houver poucos dados

---

## 📚 Recursos Adicionais

- **Documentação ML.NET**: https://docs.microsoft.com/en-us/dotnet/machine-learning/
- **Swagger UI**: `http://localhost:5000/swagger`
- **Health Check**: `http://localhost:5000/health`


