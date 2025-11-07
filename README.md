# ☁️ TrackZone Cloud API – Deploy e CI/CD no Azure

## 🚀 Sprint 4 – Cloud Computing & DevOps

### 👨‍🏫 Professor - João Carlos Menk
Projeto para entrega da **Sprint 4** da disciplina **DevOps Tools & Cloud Computing**.

**Integrantes do Grupo:**  
- RM554764 – André Rogério Vieira Pavanela Altobelli Antunes – 2TDSPH 
- RM555241 – Letícia Cristina dos Santos Passos – 2TDSPH
- RM558604 – Enrico Figueiredo Del Guerra – 2TDSPH  

**Repositório do Azure DevOps:**  
🔗 [https://dev.azure.com/RM554764/Sprint%204%20-%20Azure%20DevOps](https://dev.azure.com/RM554764/Sprint%204%20-%20Azure%20DevOps)

---

## 1️⃣ Descrição da Solução

O **TrackZone Cloud API** é uma aplicação **.NET 9 + ASP.NET Core Web API** projetada para o **gerenciamento completo de motos, usuários, operações e status**, agora **evoluída para um ambiente totalmente automatizado em nuvem**, com **integração contínua (CI)** e **entrega contínua (CD)** via **Azure DevOps**.

A aplicação combina **boas práticas de desenvolvimento backend** com **princípios de DevOps e Cloud Computing**, garantindo **alta disponibilidade, escalabilidade e automação completa do ciclo de entrega** — do código até o deploy no **Azure App Service**.

---

### 🚀 Funcionalidades Principais

- **Gestão de Usuários:** Controle de acesso com perfis diferenciados (Admin, Gerente, Operador)  
- **Gestão de Motos:** Cadastro, atualização e controle detalhado de veículos  
- **Gestão de Operações:** Registro completo de ações sobre as motos (Venda, Aluguel, Manutenção, Devolução)  
- **Gestão de Status:** Acompanhamento do estado atual de cada moto (Disponível, Alugada, Manutenção, Vendida)

---

### ☁️ Integração Cloud + DevOps

- **CI/CD Automatizado:** Pipeline em **Azure DevOps** que executa build, testes e publicação automática de artefatos  
- **Deploy Contínuo no Azure App Service:** A aplicação é disponibilizada automaticamente em ambiente de nuvem após cada push na branch principal  
- **Ambiente Gerenciado:** Uso de **Azure App Service** para hospedagem e **Azure Resource Group** para centralizar recursos e monitoramento  
- **Escalabilidade e Confiabilidade:** Configuração voltada à execução estável em produção, com base em práticas de **infraestrutura como código** e **integração contínua**

---


---

## 2️⃣ Arquitetura da Solução



---

## 3️⃣ Estrutura do Pipeline

### 📦 Fase 1 – CI (Integração Contínua)
- Executa build do projeto com `dotnet build`
- Roda testes automatizados (`dotnet test`)
- Publica artefatos no Azure DevOps

### ☁️ Fase 2 – CD (Entrega Contínua)
- Baixa os artefatos publicados
- Realiza o deploy no Azure App Service
- Atualiza a versão em produção automaticamente

---

## 4️⃣ Deploy na Nuvem (CI / CD)

O pipeline YAML realiza o deploy automático no **Azure App Service** após o merge na branch `main`.  
As credenciais e permissões são configuradas via **Service Connection** no Azure DevOps.

```bash
# ==========================================
# Pipeline CI + CD - Projeto TrackZone (.NET)
# ==========================================
# Este pipeline:
#  1️⃣ Executa build e testes (CI)
#  2️⃣ Publica artefatos (.zip)
#  3️⃣ Faz deploy automático no Azure App Service (CD)
# ==========================================

trigger:
  branches:
    include:
      - main     

pool:
  vmImage: 'ubuntu-latest'

variables:
  buildConfiguration: 'Release'
  artifactName: 'drop'
  webAppName: 'trackzone-net-app'         
  resourceGroupName: 'rg-trackzone-net'   
  runtimeStack: 'DOTNETCORE|9.0'          
  packagePath: '$(Build.ArtifactStagingDirectory)/publish_output'

stages:
# ================================
# 🧱 STAGE 1: Build, Test & Publish
# ================================
- stage: Build
  displayName: 'Build, Test e Publicação do TrackZone'
  jobs:
    - job: BuildJob
      displayName: 'Compilar, testar e publicar o projeto .NET'
      steps:
        - checkout: self

        - task: UseDotNet@2
          displayName: 'Instalar SDK .NET 9.0'
          inputs:
            packageType: 'sdk'
            version: '9.0.x'

        - script: |
            echo "🔧 Restaurando dependências..."
            dotnet restore

            echo "🏗️ Compilando projeto..."
            dotnet build --configuration $(buildConfiguration)

            echo "🧪 Executando testes..."
            dotnet test --configuration $(buildConfiguration) --no-build --logger "trx;LogFileName=test_results.trx"

            echo "📦 Publicando artefatos..."
            dotnet publish --configuration $(buildConfiguration) -o $(packagePath)
          displayName: 'Build, Test e Publish'

        - task: PublishTestResults@2
          displayName: 'Publicar resultados dos testes'
          inputs:
            testResultsFormat: 'VSTest'
            testResultsFiles: '**/test_results.trx'
            mergeTestResults: true

        - task: ArchiveFiles@2
          displayName: 'Compactar artefatos em .zip'
          inputs:
            rootFolderOrFile: '$(packagePath)'
            includeRootFolder: false
            archiveType: 'zip'
            archiveFile: '$(Build.ArtifactStagingDirectory)/$(artifactName).zip'
            replaceExistingArchive: true

        - task: PublishBuildArtifacts@1
          displayName: 'Publicar artefato para o pipeline'
          inputs:
            pathToPublish: '$(Build.ArtifactStagingDirectory)'
            artifactName: '$(artifactName)'

# ================================
# 🚀 STAGE 2: Deploy no Azure
# ================================
- stage: Deploy
  displayName: 'Deploy automático no Azure App Service'
  dependsOn: Build
  condition: succeeded()

  jobs:
    - deployment: DeployJob
      displayName: 'Publicar TrackZone no App Service'
      environment: 'production'
      strategy:
        runOnce:
          deploy:
            steps:
              - task: DownloadBuildArtifacts@0
                displayName: 'Baixar artefatos do build'
                inputs:
                  buildType: 'current'
                  downloadType: 'single'
                  artifactName: '$(artifactName)'
                  downloadPath: '$(System.ArtifactsDirectory)'

              - task: AzureWebApp@1
                displayName: 'Deploy no App Service (trackzone-net-app)'
                inputs:
                  azureSubscription: 'trackzone-azure-conn'
                  appName: '$(webAppName)'
                  package: '$(System.ArtifactsDirectory)/$(artifactName)/$(artifactName).zip'

              - script: |
                  echo "✅ Deploy concluído com sucesso no App Service: $(webAppName)"
                displayName: 'Confirmação do deploy'

```

---

## 6️⃣ Resultados e Benefícios

✅ Pipeline 100% automatizado (Build, Test e Deploy)  
✅ Entregas contínuas e seguras via Azure DevOps  
✅ Monitoramento de logs e métricas pelo Azure Portal  
✅ Redução de tempo de deploy e falhas humanas  
✅ Escalabilidade e alta disponibilidade na nuvem Azure  

---

## 🧠 Conclusão

A implementação do **TrackZone Cloud API** demonstra o domínio dos conceitos de **DevOps e Cloud Computing**, integrando uma aplicação real a pipelines CI/CD automatizados com **Azure DevOps** e **Azure App Service**, promovendo um ciclo de entrega ágil, seguro e sustentável.

---

## 📄 Licença
Este projeto é de uso acadêmico para fins de demonstração na **FIAP – Sprint 4**.

