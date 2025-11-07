#!/bin/bash
set -e  # para parar caso dê erro

# ============================
# VARIÁVEIS
# ============================
export RESOURCE_GROUP_NAME="rg-trackzone-net"
export WEBAPP_NAME="trackzone-net-app"
export APP_SERVICE_PLAN="planTrackzoneNet"
export LOCATION="brazilsouth"
export RUNTIME="DOTNETCORE:9.0"
export GITHUB_REPO_NAME="andrealtobelli/challenge3-devops-net"
export BRANCH="main"
export APP_INSIGHTS_NAME="ai-trackzone-net"

# Carregar variáveis do banco, se existir arquivo .env.db gerado pelo create-sql-server.sh
if [ -f ./.env.db ]; then
  # shellcheck disable=SC1091
  source ./.env.db
fi


# ============================
# DEFAULTS DE BANCO (podem ser sobrescritos via env ou .env.db)
# ============================
: "${RG_DB_NAME:=rg-trackzone-net-db}"
: "${DB_LOCATION:=brazilsouth}"
: "${SERVER_NAME:=sqlserver-trackzone-net-$RANDOM$RANDOM}"
: "${DB_USERNAME:=admsql}"
: "${DB_PASSWORD:=Trackzone_321}"
: "${DB_NAME:=SistemaGestaoMotos}"


# ============================
# PROVIDERS E EXTENSÕES
# ============================
az provider register --namespace Microsoft.Web
az provider register --namespace Microsoft.Insights
az provider register --namespace Microsoft.OperationalInsights
az provider register --namespace Microsoft.ServiceLinker
az provider register --namespace Microsoft.Sql

az extension add --name application-insights --allow-preview true || true

# ============================
# PRÉ-CHECKS
# ============================
if ! command -v az >/dev/null 2>&1; then
  echo "ERRO: Azure CLI (az) não encontrado." >&2
  exit 1
fi
# sqlcmd é preferido, mas se não existir e houver pwsh, usaremos Invoke-Sqlcmd como fallback
if ! command -v sqlcmd >/dev/null 2>&1 && ! command -v pwsh >/dev/null 2>&1; then
  echo "ERRO: Nem sqlcmd nem pwsh encontrados. Instale o sqlcmd (ou use PowerShell com módulo SqlServer)." >&2
  exit 1
fi

# ============================
# GRUPOS DE RECURSOS
# ============================
az group create --name $RG_DB_NAME --location "${DB_LOCATION:-eastus2}"
az group create --name $RESOURCE_GROUP_NAME --location "$LOCATION"

# ============================
# BANCO DE DADOS SQL
# ============================
az sql server create \
  --name $SERVER_NAME \
  --resource-group $RG_DB_NAME \
  --location "$DB_LOCATION" \
  --admin-user $DB_USERNAME \
  --admin-password $DB_PASSWORD \
  --enable-public-network true

az sql db create \
  --resource-group $RG_DB_NAME \
  --server $SERVER_NAME \
  --name $DB_NAME \
  --service-objective Basic \
  --backup-storage-redundancy Local \
  --zone-redundant false

# Liberar firewall (apenas testes!)
az sql server firewall-rule create \
  --resource-group $RG_DB_NAME \
  --server $SERVER_NAME \
  --name liberaGeral \
  --start-ip-address 0.0.0.0 \
  --end-ip-address 255.255.255.255

## ============================
## EXECUTAR T-SQL (reset, schema, seeds, triggers)
## ============================
TMP_SQL=$(mktemp)
cat >"$TMP_SQL" <<'SQL'
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
  BEGIN TRAN;

  -- V0: Clean reset (order respects FK dependencies)
  IF OBJECT_ID('dbo.Operacoes','U')     IS NOT NULL DROP TABLE dbo.Operacoes;
  IF OBJECT_ID('dbo.StatusMotos','U')   IS NOT NULL DROP TABLE dbo.StatusMotos;
  IF OBJECT_ID('dbo.Motos','U')         IS NOT NULL DROP TABLE dbo.Motos;
  IF OBJECT_ID('dbo.Usuarios','U')      IS NOT NULL DROP TABLE dbo.Usuarios;
  IF OBJECT_ID('dbo.__EFMigrationsHistory','U') IS NOT NULL DROP TABLE dbo.__EFMigrationsHistory;

  COMMIT TRAN;
END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0 ROLLBACK TRAN;
  THROW;
END CATCH;
GO

-- V1: Create tables
BEGIN TRAN;

CREATE TABLE dbo.Usuarios (
  Id            BIGINT IDENTITY(1,1) PRIMARY KEY,
  NomeFilial    NVARCHAR(255) NOT NULL,
  Email         NVARCHAR(255) NOT NULL,
  SenhaHash     NVARCHAR(255) NOT NULL,
  Cnpj          NVARCHAR(18)  NOT NULL,
  Endereco      NVARCHAR(500) NULL,
  Telefone      NVARCHAR(20)  NULL,
  Perfil        INT           NOT NULL,
  DataCriacao   DATETIME2     NOT NULL CONSTRAINT DF_Usuarios_DataCriacao DEFAULT SYSUTCDATETIME(),
  DataAtualizacao DATETIME2   NOT NULL CONSTRAINT DF_Usuarios_DataAtualizacao DEFAULT SYSUTCDATETIME(),
  CONSTRAINT CK_Usuarios_Perfil CHECK (Perfil IN (0, 1, 2)),
  CONSTRAINT UQ_Usuarios_Email UNIQUE (Email),
  CONSTRAINT UQ_Usuarios_Cnpj UNIQUE (Cnpj)
);

CREATE TABLE dbo.Motos (
  Id            BIGINT IDENTITY(1,1) PRIMARY KEY,
  Placa         NVARCHAR(10)  NOT NULL,
  Chassi        NVARCHAR(17)  NOT NULL,
  Motor         NVARCHAR(100) NULL,
  UsuarioId     BIGINT        NOT NULL,
  DataCriacao   DATETIME2     NOT NULL CONSTRAINT DF_Motos_DataCriacao DEFAULT SYSUTCDATETIME(),
  DataAtualizacao DATETIME2   NOT NULL CONSTRAINT DF_Motos_DataAtualizacao DEFAULT SYSUTCDATETIME(),
  CONSTRAINT UQ_Motos_Placa UNIQUE (Placa),
  CONSTRAINT UQ_Motos_Chassi UNIQUE (Chassi),
  CONSTRAINT FK_Motos_Usuario FOREIGN KEY (UsuarioId) REFERENCES dbo.Usuarios(Id)
);

CREATE TABLE dbo.StatusMotos (
  Id            BIGINT IDENTITY(1,1) PRIMARY KEY,
  Status        INT           NOT NULL,
  Descricao     NVARCHAR(500) NULL,
  Area          NVARCHAR(50)  NOT NULL,
  DataStatus    DATETIME2     NOT NULL CONSTRAINT DF_StatusMotos_DataStatus DEFAULT SYSUTCDATETIME(),
  MotoId        BIGINT        NOT NULL,
  UsuarioId     BIGINT        NOT NULL,
  CONSTRAINT FK_StatusMotos_Moto     FOREIGN KEY (MotoId)    REFERENCES dbo.Motos(Id),
  CONSTRAINT FK_StatusMotos_Usuario  FOREIGN KEY (UsuarioId) REFERENCES dbo.Usuarios(Id),
  CONSTRAINT CK_StatusMotos_Status CHECK (Status IN (0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12))
);

CREATE TABLE dbo.Operacoes (
  Id            BIGINT IDENTITY(1,1) PRIMARY KEY,
  TipoOperacao  INT           NOT NULL,
  Descricao     NVARCHAR(1000) NULL,
  DataOperacao  DATETIME2     NOT NULL CONSTRAINT DF_Operacoes_DataOperacao DEFAULT SYSUTCDATETIME(),
  MotoId        BIGINT        NOT NULL,
  UsuarioId     BIGINT        NOT NULL,
  CONSTRAINT FK_Operacoes_Moto    FOREIGN KEY (MotoId)    REFERENCES dbo.Motos(Id),
  CONSTRAINT FK_Operacoes_Usuario FOREIGN KEY (UsuarioId) REFERENCES dbo.Usuarios(Id),
  CONSTRAINT CK_Operacoes_TipoOperacao CHECK (TipoOperacao IN (0, 1, 2, 3, 4, 5))
);

-- Índices para melhor performance
CREATE INDEX IX_Motos_UsuarioId ON dbo.Motos(UsuarioId);
CREATE INDEX IX_StatusMotos_MotoId ON dbo.StatusMotos(MotoId);
CREATE INDEX IX_StatusMotos_UsuarioId ON dbo.StatusMotos(UsuarioId);
CREATE INDEX IX_Operacoes_MotoId ON dbo.Operacoes(MotoId);
CREATE INDEX IX_Operacoes_UsuarioId ON dbo.Operacoes(UsuarioId);

COMMIT TRAN;
GO

-- V2: Seed Data (compatível com Entity Framework)
INSERT INTO dbo.Usuarios (NomeFilial, Email, SenhaHash, Cnpj, Endereco, Telefone, Perfil, DataCriacao, DataAtualizacao)
VALUES
  (N'Admin TrackZone', N'admin@teste.com',   N'$2a$10$N.zmdr9k7uOCQb376NoUnuTJ8iAt6Z5EHsM8lE9lBOsl7iKTVEFDi', N'12.345.678/0001-99', N'Rua A, 123', N'(11) 99999-0001', 0, SYSUTCDATETIME(), SYSUTCDATETIME()),
  (N'Filial Centro',   N'gerente@teste.com', N'$2a$10$N.zmdr9k7uOCQb376NoUnuTJ8iAt6Z5EHsM8lE9lBOsl7iKTVEFDi', N'12.345.678/0002-88', N'Rua B, 456', N'(11) 98888-0002', 1, SYSUTCDATETIME(), SYSUTCDATETIME()),
  (N'Filial Sul',      N'operador@teste.com',N'$2a$10$N.zmdr9k7uOCQb376NoUnuTJ8iAt6Z5EHsM8lE9lBOsl7iKTVEFDi', N'12.345.678/0003-66', N'Rua C, 789', N'(11) 97777-0003', 2, SYSUTCDATETIME(), SYSUTCDATETIME());

INSERT INTO dbo.Motos (Placa, Chassi, Motor, UsuarioId, DataCriacao, DataAtualizacao) VALUES
  (N'ABC1234', N'1HGBH41JXMN109186', N'MOTOR001', 1, SYSUTCDATETIME(), SYSUTCDATETIME()),
  (N'XYZ9876', N'2HGBH41JXMN109187', N'MOTOR002', 2, SYSUTCDATETIME(), SYSUTCDATETIME()),
  (N'DEF5678', N'3HGBH41JXMN109188', N'MOTOR003', 3, SYSUTCDATETIME(), SYSUTCDATETIME());

INSERT INTO dbo.StatusMotos (Status, Descricao, Area, DataStatus, MotoId, UsuarioId) VALUES
  (9, N'Moto pronta para uso', N'Área A', SYSUTCDATETIME(), 1, 1),
  (4, N'Status pendente de verificação', N'Área B', SYSUTCDATETIME(), 2, 2),
  (5, N'Necessário reparo simples', N'Área C', SYSUTCDATETIME(), 3, 3);

INSERT INTO dbo.Operacoes (TipoOperacao, Descricao, DataOperacao, MotoId, UsuarioId) VALUES
  (4, N'Check-in inicial da moto ABC1234', SYSUTCDATETIME(), 1, 1),
  (5, N'Check-out da moto XYZ9876 para manutenção', SYSUTCDATETIME(), 2, 2),
  (4, N'Check-in da moto DEF5678 após reparo', SYSUTCDATETIME(), 3, 3);
GO

-- Triggers para auditoria automática das tabelas principais
GO
CREATE OR ALTER TRIGGER dbo.trg_update_DataAtualizacao_Usuarios
ON dbo.Usuarios
AFTER UPDATE
AS
BEGIN
  SET NOCOUNT ON;
  UPDATE u
  SET DataAtualizacao = SYSUTCDATETIME()
  FROM dbo.Usuarios u
  INNER JOIN inserted i ON u.Id = i.Id;
END
GO

CREATE OR ALTER TRIGGER dbo.trg_update_DataAtualizacao_Motos
ON dbo.Motos
AFTER UPDATE
AS
BEGIN
  SET NOCOUNT ON;
  UPDATE m
  SET DataAtualizacao = SYSUTCDATETIME()
  FROM dbo.Motos m
  INNER JOIN inserted i ON m.Id = i.Id;
END
GO

SELECT N'Banco limpo e pronto para recriação!' AS status;
GO
SQL

echo "==> Aplicando schema/seed no Azure SQL: $SERVER_NAME/$DB_NAME"
if command -v sqlcmd >/dev/null 2>&1; then
  sqlcmd \
    -S "${SERVER_NAME}.database.windows.net" \
    -d "$DB_NAME" \
    -U "$DB_USERNAME" \
    -P "$DB_PASSWORD" \
    -l 60 \
    -b \
    -i "$TMP_SQL"
else
  echo "sqlcmd não encontrado, usando pwsh + Invoke-Sqlcmd como fallback..."
  pwsh -NoLogo -NoProfile -Command "
    if (-not (Get-Module -ListAvailable -Name SqlServer)) {
      try { Install-Module -Name SqlServer -Scope CurrentUser -Force -AllowClobber -ErrorAction Stop } catch { Write-Error 'Falha ao instalar módulo SqlServer'; exit 1 }
    }
    try {
      Invoke-Sqlcmd -ServerInstance '${SERVER_NAME}.database.windows.net' -Database '${DB_NAME}' -Username '${DB_USERNAME}' -Password '${DB_PASSWORD}' -InputFile '${TMP_SQL}' -ConnectionTimeout 60 -QueryTimeout 300
    } catch {
      Write-Error $_.Exception.Message; exit 1
    }
  "
fi

rm -f "$TMP_SQL"

# ============================
# APPLICATION INSIGHTS
# ============================
az monitor app-insights component create \
  --app $APP_INSIGHTS_NAME \
  --location "$LOCATION" \
  --resource-group $RESOURCE_GROUP_NAME \
  --application-type web

CONNECTION_STRING=$(az monitor app-insights component show \
  --app $APP_INSIGHTS_NAME \
  --resource-group $RESOURCE_GROUP_NAME \
  --query connectionString \
  --output tsv)

# ============================
# APP SERVICE PLAN + WEBAPP
# ============================
az appservice plan create \
  --name $APP_SERVICE_PLAN \
  --resource-group $RESOURCE_GROUP_NAME \
  --location "$LOCATION" \
  --sku F1 \
  --is-linux

az webapp create \
  --name $WEBAPP_NAME \
  --resource-group $RESOURCE_GROUP_NAME \
  --plan $APP_SERVICE_PLAN \
  --runtime "$RUNTIME"

# Habilitar autenticação SCM
az resource update \
  --resource-group $RESOURCE_GROUP_NAME \
  --namespace Microsoft.Web \
  --resource-type basicPublishingCredentialsPolicies \
  --name scm \
  --parent sites/$WEBAPP_NAME \
  --set properties.allow=true

# ============================
# CONFIGURAR VARIÁVEIS DO APP
# ============================
DOTNET_CONNECTION_STRING="Server=$SERVER_NAME.database.windows.net;Database=$DB_NAME;User Id=$DB_USERNAME;Password=$DB_PASSWORD;Encrypt=true;TrustServerCertificate=false;Connection Timeout=30;"

az webapp config appsettings set \
  --name "$WEBAPP_NAME" \
  --resource-group "$RESOURCE_GROUP_NAME" \
  --settings \
    APPLICATIONINSIGHTS_CONNECTION_STRING="$CONNECTION_STRING" \
    ApplicationInsightsAgent_EXTENSION_VERSION="~3" \
    XDT_MicrosoftApplicationInsights_Mode="Recommended" \
    XDT_MicrosoftApplicationInsights_PreemptSdk="1" \
    ConnectionStrings__DefaultConnection="$DOTNET_CONNECTION_STRING" \
    DB_SERVER="$SERVER_NAME.database.windows.net" \
    DB_DATABASE="$DB_NAME" \
    DB_USERNAME="$DB_USERNAME" \
    DB_PASSWORD="$DB_PASSWORD" \
    ASPNETCORE_ENVIRONMENT="Production"

# Reiniciar o Web App
az webapp restart --name $WEBAPP_NAME --resource-group $RESOURCE_GROUP_NAME

# Conectar App ao Application Insights
az monitor app-insights component connect-webapp \
    --app $APP_INSIGHTS_NAME \
    --web-app $WEBAPP_NAME \
    --resource-group $RESOURCE_GROUP_NAME

# ============================
# DEPLOY VIA GITHUB ACTIONS
# ============================
az webapp deployment github-actions add \
  --name $WEBAPP_NAME \
  --resource-group $RESOURCE_GROUP_NAME \
  --repo $GITHUB_REPO_NAME \
  --branch $BRANCH \
  --login-with-github

echo "✅ Deploy configurado com sucesso!"

