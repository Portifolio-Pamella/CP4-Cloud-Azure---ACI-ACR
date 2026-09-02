# 🚀 Projeto Aegis Aerospace - CP4 Containers em Nuvem (ACR/ACI)

**Disciplina:** DevOps Tools & Cloud Computing  
**Professor:** João Menk  
**Instituição:** FIAP  

## 👥 Equipe
* Felipe Ribeiro Salles de Camargo | RM565224
* João Pedro Pereira Camilo | RM562005
* Lucas Matsubara Reis | RM565020
* Pamella Christiny Chaves Brito | RM565206 (Representante)

---

## 📌 Links de Entrega (Checklist do Professor)

- 🎥 **Vídeo de Demonstração (Mín. 720p + Áudio):** [Insira o Link do YouTube/Drive Aqui]
- 🗄️ **Scripts DDL do Banco (Tabelas, Colunas, PKs):** [Link para a pasta/arquivo DDL no repositório]
- 📄 **Payloads JSON (Testes GET, POST, PUT, DELETE):** [Link para a pasta com os JSONs no repositório]
- 💻 **Código Fonte da API:** [Link para a pasta do código C# no repositório]
- 📜 **Folha de Rosto (PDF):** [Link para o PDF da Folha de Rosto no repositório]

---

## 🏗️ Arquitetura do Projeto

O projeto consiste em uma API RESTful desenvolvida em **.NET** integrada a um banco de dados **Oracle 21c**. Toda a infraestrutura foi provisionada na Azure utilizando estritamente **Azure CLI**.

* **Container Registry (ACR):** `aegisrm565206`
* **Banco de Dados (ACI):** `oracle-dimdim` (Imagem: `gvenzl/oracle-xe:21-slim`)
* **Aplicação (ACI):** `api-dotnet` (Imagem: `rm565206-api:v1`)
* **Persistência de Dados:** Azure Storage Account (File Share mapeado no Oracle).
* **Segurança (Non-Root):** O container da API .NET foi configurado para executar com o usuário `app` (não privilegiado), respeitando as diretrizes de segurança (ver `Dockerfile`). Os dados sensíveis (Connection Strings e senhas) foram injetados via variáveis de ambiente e **não** estão expostos no código.

---

## 🛠️ HOW TO - Tutorial de Execução e Deploy

Abaixo estão todos os comandos utilizados para criar os recursos, realizar o build das imagens, publicá-las no ACR e executar o deploy no ACI (Azure Container Instances). Os scripts `.sh` completos estão na pasta `/scripts` deste repositório.

### Pré-requisitos
* Azure CLI instalada e autenticada (`az login`).
* Docker Desktop rodando localmente.

### Passo 1: Criação da Infraestrutura Base e Persistência
```bash
# 1. Criar o Grupo de Recursos
az group create --name rg-aegis-app --location canadacentral

# 2. Criar o Azure Container Registry (ACR)
az acr create --resource-group rg-aegis-app --name aegisrm565206 --sku Basic --admin-enabled true

# 3. Criar a Storage Account para persistência do Banco de Dados
az storage account create --resource-group rg-aegis-app --name volumeaegisdata565206 --location canadacentral --sku Standard_LRS

# 4. Criar o File Share (Volume)
az storage share create --account-name volumeaegisdata565206 --name oracle-aegis-volume

```

### Passo 2: Docker Build e Push (Local para ACR)

*No terminal, navegue até a pasta onde está o `Dockerfile` da API.*

```bash
# 1. Login no ACR
az acr login --name aegisrm565206

# 2. Build da Imagem Local (API)
docker build -t aegisrm565206.azurecr.io/rm565206-api:v1 .

# 3. Teste Local (Opcional antes do Push)
docker run -d -p 8080:8080 aegisrm565206.azurecr.io/rm565206-api:v1

# 4. Push da Imagem para o Azure Container Registry
docker push aegisrm565206.azurecr.io/rm565206-api:v1

```

### Passo 3: Deploy do Banco de Dados no ACI (Com Persistência)

```bash
# Obter a chave de acesso do Storage
STORAGE_KEY=$(az storage account keys list --resource-group rg-aegis-app --account-name volumeaegisdata565206 --query "[0].value" --output tsv)

# Deploy do Container Oracle 21c
az container create \
  --resource-group rg-aegis-app \
  --name oracle-dimdim \
  --image gvenzl/oracle-xe:21-slim \
  --ip-address Public \
  --cpu 2 --memory 4 --os-type Linux --ports 1521 \
  --environment-variables ORACLE_PASSWORD=SuaSenhaForteAqui \
  --azure-file-volume-account-name volumeaegisdata565206 \
  --azure-file-volume-account-key $STORAGE_KEY \
  --azure-file-volume-share-name oracle-aegis-volume \
  --azure-file-volume-mount-path /opt/oracle/oradata

```

*Nota: Após o banco subir, os scripts de DDL / Criação de usuário (`RM565206`) foram aplicados acessando o container via `az container exec`.*

### Passo 4: Deploy da Aplicação (API) no ACI

```bash
# Resgatar senhas do ACR para autenticação do ACI
ACR_USER=$(az acr credential show --name aegisrm565206 --query username -o tsv)
ACR_PASS=$(az acr credential show --name aegisrm565206 --query passwords[0].value -o tsv)

# String de Conexão com o IP gerado no Passo 3
CONN_STRING="Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=IP_DO_BANCO)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=RM565206;Password=SuaSenhaForteAqui;"

# Criação do Container da API
az container create \
  --resource-group rg-aegis-app \
  --name api-dotnet \
  --image aegisrm565206.azurecr.io/rm565206-api:v1 \
  --cpu 1 --memory 1.5 --os-type Linux --ports 8080 \
  --dns-name-label api-aegis-rm565206 \
  --registry-login-server aegisrm565206.azurecr.io \
  --registry-username $ACR_USER \
  --registry-password $ACR_PASS \
  --environment-variables ConnectionStrings__OracleConnection="$CONN_STRING" ASPNETCORE_ENVIRONMENT="Development" ASPNETCORE_URLS="http://+:8080"

```

## 🔒 Regra: Container App rodando como Non-Root

O projeto atende ao requisito de não executar a aplicação com privilégios administrativos. O `Dockerfile` da API foi configurado utilizando a diretiva padrão da Microsoft para contêineres OCI seguros (`USER app`), rodando a aplicação com um UID não privilegiado (UID 1654), isolando o processo do root do Linux.

---

*Este repositório foi criado para fins acadêmicos como parte do CP4 da FIAP.*

```
