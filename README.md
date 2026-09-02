🚀 Projeto Aegis Aerospace - CP4 Containers em Nuvem (ACR/ACI)
Disciplina: DevOps Tools & Cloud Computing

Professor: João Menk

Instituição: FIAP

👥 Equipe
Felipe Ribeiro Salles de Camargo | RM565224

João Pedro Pereira Camilo | RM562005

Lucas Matsubara Reis | RM565020

Pamella Christiny Chaves Brito | RM565206 (Representante)

📌 Links de Entrega (Checklist do Professor)
🎥 Vídeo de Demonstração (Mín. 720p + Audio): [Insira o Link do YouTube/Drive Aqui]

🗄️ Scripts DDL do Banco (Tabelas, Colunas, PKs): [Link para o arquivo de criação do banco no repositório]

📄 Payloads JSON (Testes GET, POST, PUT, DELETE): [Link para a pasta de testes no repositório]

💻 Código Fonte da API: [Link para a pasta do código fonte no repositório]

📜 Folha de Rosto (PDF): [Link para a Folha de Rosto no repositório]

🌐 Endpoints e Recursos Ativos na Nuvem (ACI)
Os seguintes recursos estão provisionados e ativos no Azure Container Instances (Resource Group: rg-aegis-app):

Banco de Dados Oracle 21c (oracle-dimdim):

IP Público: 130.107.230.198

Porta: 1521

API .NET (api-dotnet):

IP Público: 130.107.173.197

URL Swagger: [http://130.107.173.197:8080/swagger/index.html](http://130.107.173.197:8080/swagger/index.html)

FQDN / Domínio: api-aegis-rm565206.canadacentral.azurecontainer.io:8080/swagger

🏗️ Arquitetura do Projeto
O projeto consiste em uma API RESTful desenvolvida em .NET integrada a um banco de dados Oracle 21c. Toda a infraestrutura foi provisionada na Azure utilizando estritamente Azure CLI e scripts automatizados em Bash.

Container Registry (ACR): aegisrm565206

Banco de Dados (ACI): oracle-dimdim (Imagem: [container-registry.oracle.com/database/express:latest](https://container-registry.oracle.com/database/express:latest) / gvenzl/oracle-xe:21-slim)  
SH

Aplicação (ACI): api-dotnet (Imagem: aegisrm565206.azurecr.io/rm565206-api:v1)  
SH

Persistência de Dados: Azure Storage Account (volumeaegisdata565206) com File Share (oracle-aegis-volume) mapeado no Oracle.  
SH
+ 1

Segurança (Key Vault & Non-Root): Credenciais e senhas sensíveis gerenciadas via Azure Key Vault. O container da API .NET foi configurado para executar com o usuário app (não privilegiado), respeitando as diretrizes de segurança (ver Dockerfile).  
SH
+ 2
🚀 Tutorial Detalhado de Execução (How-To) - Aegis Aerospace🌐 Informações de Acesso e Endpoints Ativos na Nuvem (ACI)Resource Group: rg-aegis-app  Banco de Dados Oracle (oracle-dimdim):IP Público: 130.107.230.198Porta: 1521  API .NET (api-dotnet):IP Público: 130.107.173.197URL do Swagger: http://130.107.173.197:8080/swagger/index.htmlFQDN: api-aegis-rm565206.canadacentral.azurecontainer.io:8080🛠️ Passo a Passo Detalhado da ExecuçãoPré-requisitosAcesse o Azure Portal.Abra o Azure Cloud Shell (clicando no ícone de terminal >_ na barra superior do portal) e selecione o ambiente Bash.Certifique-se de estar autenticado com o comando:Bashaz login
Passo 1: Configuração do Storage Account (Persistência)Onde executar: No Azure Cloud Shell.Objetivo: Cria a conta de armazenamento e o File Share para garantir que os dados do banco Oracle não sejam perdidos ao reiniciar o contêiner.Script correspondente:[cite: 2]Bash# Crie e execute o script de storage no Cloud Shell
cat << 'EOF' > setup_storage.sh
#!/bin/bash
rm="565206"
RESOURCE_GROUP="rg-aegis-app"
LOCATION="canadacentral"
STORAGE_ACCOUNT="volumeaegisdata$rm"
FILE_SHARE="oracle-aegis-volume"

az provider register --namespace Microsoft.Storage

if ! az storage account show --name "$STORAGE_ACCOUNT" --resource-group "$RESOURCE_GROUP" &>/dev/null; then
  az group create --name "$RESOURCE_GROUP" --location "$LOCATION"
  az storage account create --resource-group "$RESOURCE_GROUP" \
    --name "$STORAGE_ACCOUNT" \
    --location "$LOCATION" \
    --sku Standard_LRS
else
  echo "A conta de armazenamento '$STORAGE_ACCOUNT' já existe"
fi

connection_string=$(az storage account show-connection-string --name $STORAGE_ACCOUNT --resource-group $RESOURCE_GROUP --query connectionString --output tsv)

if ! az storage share exists --name "$FILE_SHARE" --account-name "$STORAGE_ACCOUNT" --connection-string "$connection_string" | grep true; then
  az storage share create --name "$FILE_SHARE" --account-name "$STORAGE_ACCOUNT" --connection-string "$connection_string"
else
  echo "O compartilhamento de arquivos '$FILE_SHARE' já existe"
fi
EOF

chmod +x setup_storage.sh
./setup_storage.sh
Passo 2: Configuração e Salvamento de Segredos no Azure Key VaultOnde executar: No Azure Cloud Shell.Objetivo: Criar o Key Vault de segurança e armazenar centralmente a senha do banco, string de conexão e credenciais do ACR.Script correspondente:[cite: 1]Bashcat << 'EOF' > setup_keyvault.sh
#!/bin/bash
rm="565206"
resourceGroup="rg-aegis-app"
location="canadacentral"

ORACLE_PASSWORD="200806"
CONNECTIONSTRINGS='Data Source=oracle-dimdim:1521/XE;User Id=rm565206;Password=200806;'

acrName="aegisrm$rm"
keyVaultName="keyvault-aegis-$rm"

az provider register --namespace Microsoft.KeyVault
if ! az keyvault show --name "$keyVaultName" --resource-group "$resourceGroup" &> /dev/null; then
  az keyvault create --name "$keyVaultName" --resource-group "$resourceGroup" --location "$location"
else
  echo "Key Vault '$keyVaultName' já existe."
fi

az role assignment create \
  --assignee $(az account show --query user.name -o tsv) \
  --role "Key Vault Administrator" \
  --scope /subscriptions/$(az account show --query id -o tsv)/resourceGroups/$resourceGroup/providers/Microsoft.KeyVault/vaults/$keyVaultName

sleep 15

ACRUSERNAME=$(az acr credential show --name $acrName --resource-group $resourceGroup --query username --output tsv)
ACRPASSWORD=$(az acr credential show --name $acrName --resource-group $resourceGroup --query passwords[0].value --output tsv)

az keyvault secret set --vault-name $keyVaultName --name oracle-password --value "$ORACLE_PASSWORD"
az keyvault secret set --vault-name $keyVaultName --name connection-strings --value "$CONNECTIONSTRINGS"
az keyvault secret set --vault-name $keyVaultName --name acr-username --value "$ACRUSERNAME"
az keyvault secret set --vault-name $keyVaultName --name acr-password --value "$ACRPASSWORD"
EOF

chmod +x setup_keyvault.sh
./setup_keyvault.sh
Passo 3: Build e Push da Imagem da API (Local para ACR)Onde executar: No seu computador local (com Docker Desktop rodando e a pasta do projeto .NET aberta no terminal).Objetivo: Compilar a imagem do app localmente e enviá-la para o Azure Container Registry (aegisrm565206)[cite: 1, 4].Bash# 1. Efetuar login no ACR da equipe
az acr login --name aegisrm565206

# 2. Realizar o build da imagem Docker da API a partir do Dockerfile local
docker build -t aegisrm565206.azurecr.io/rm565206-api:v1 .

# 3. Realizar o Push da imagem construída para a nuvem
docker push aegisrm565206.azurecr.io/rm565206-api:v1
Passo 4: Deploy do Banco de Dados Oracle no ACIOnde executar: No Azure Cloud Shell.Objetivo: Subir o container do banco Oracle 21c (oracle-dimdim) utilizando a imagem oficial, associando-o ao IP público 130.107.230.198 e ao volume de persistência criado no Passo 1[cite: 2].  Script correspondente:  Bashcat << 'EOF' > deploy_oracle.sh
#!/bin/bash
rm="565206"
resourceGroup="rg-aegis-app"
acrName="aegisrm$rm"
aciName="oracle-dimdim"
storageAccountName="volumeaegisdata$rm"
file_share_name="oracle-aegis-volume"
storage_key=$(az storage account keys list --resource-group $resourceGroup --account-name $storageAccountName --query "[0].value" --output tsv)
keyVaultName="keyvault-aegis-$rm"

az provider register --namespace Microsoft.ContainerInstance

az container create \
  --resource-group $resourceGroup \
  --name $aciName \
  --image container-registry.oracle.com/database/express:latest \
  --ip-address Public \
  --cpu 1 \
  --memory 2 \
  --os-type Linux \
  --dns-name-label oracle-container-$rm \
  --ports 1521 \
  --azure-file-volume-account-name $storageAccountName \
  --azure-file-volume-account-key $storage_key \
  --azure-file-volume-share-name $file_share_name \
  --azure-file-volume-mount-path /opt/oracle/oradata \
  --environment-variables \
    ORACLE_PWD=$(az keyvault secret show --vault-name $keyVaultName --name oracle-password --query value -o tsv) \
  --restart-policy Always
EOF

chmod +x deploy_oracle.sh
./deploy_oracle.sh
Passo 5: Deploy da Aplicação .NET no ACIOnde executar: No Azure Cloud Shell.Objetivo: Implantar o container da API de detritos espaciais (api-dotnet), buscando as credenciais de forma segura no Key Vault[cite: 4] e apontando a string de conexão para o IP do banco Oracle na nuvem (130.107.230.198).Script correspondente:[cite: 4]Bashcat << 'EOF' > deploy_api.sh
#!/bin/bash
rm="565206"
resourceGroup="rg-aegis-app"
acrName="aegisrm$rm"
aciName="api-dotnet"
aciNameOracle="oracle-dimdim"
imageName="rm565206-api"
tag="v1"
keyVaultName="keyvault-aegis-$rm"

oraclePublicIP=$(az container show --resource-group $resourceGroup --name $aciNameOracle --query ipAddress.ip --output tsv)

az provider register --namespace Microsoft.ContainerInstance

az container create \
  --resource-group $resourceGroup \
  --name $aciName \
  --image $acrName.azurecr.io/$imageName:$tag \
  --cpu 1 \
  --memory 1.5 \
  --os-type Linux \
  --dns-name-label api-dotnet-container-$rm \
  --ports 8080 \
  --registry-login-server $acrName.azurecr.io \
  --registry-username $(az keyvault secret show --vault-name $keyVaultName --name acr-username --query value -o tsv) \
  --registry-password $(az keyvault secret show --vault-name $keyVaultName --name acr-password --query value -o tsv) \
  --environment-variables \
    ConnectionStrings__DefaultConnection=$(az keyvault secret show --name connection-strings --vault-name $keyVaultName --query value -o tsv | sed "s/oracle-dimdim/$oraclePublicIP/") \
    ASPNETCORE_ENVIRONMENT="Development" \
    ASPNETCORE_URLS="http://+:8080" \
  --restart-policy Always
EOF

chmod +x deploy_api.sh
./deploy_api.sh
Passo 6: Como Acessar o Banco de Dados para Evidências (SELECT)Onde executar: No Azure Cloud Shell.Objetivo: Validar o funcionamento das operações do CRUD diretamente no banco relacional por meio de comandos SQL.Bash# Conectar no container do banco via utilitário sqlplus
az container exec --resource-group rg-aegis-app --name oracle-dimdim --exec-command "sqlplus sys/200806 as sysdba"
Uma vez dentro do prompt SQL>, execute os comandos de consulta para evidência no vídeo:SQL-- Verificar as tabelas geradas automaticamente pelo Entity Framework
SELECT TABLE_NAME FROM USER_TABLES;

-- Realizar o SELECT evidenciando o CRUD na tabela de Empresas
SET LINESIZE 200;
SELECT * FROM TB_EMPRESA_AEROESPACIAL;
🔍 Validação Geral dos ContêineresPara listar rapidamente o status, os IPs públicos e as URLs ativas na nuvem dos dois serviços criados, execute a qualquer momento no Cloud Shell:Bashaz container list --resource-group rg-aegis-app --query "[].{Container:name, IP_Publico:ipAddress.ip, URL:ipAddress.fqdn}" -o table