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
🎥 Vídeo de Demonstração (Mín. 720p + Audio): [https://youtu.be/q81IKnXv5z0?si=hCX14KOS8Ox-b0Qm]

🗄️ Scripts DDL do Banco (Tabelas, Colunas, PKs): [CP4-Cloud-Azure---ACI-ACR\Banco-ddl\criaçãoBanco.ddl]

📄 Payloads JSON (Testes GET, POST, PUT, DELETE): [CP4-Cloud-Azure---ACI-ACR\Teste\testeApi.json]

💻 Código Fonte da API: [CP4-Cloud-Azure---ACI-ACR\api]

📜 Folha de Rosto (PDF): [CP4-Cloud-Azure---ACI-ACR\LevelUp_container.pdf]

Arquitetura
Azure Container Registry (ACR) (aegisrm565206): Repositório centralizado em nuvem responsável por armazenar as imagens Docker da aplicação. 

Azure Container Instances (ACI) - Banco de Dados (oracle-dimdim): Executa o container do Oracle Database 21c Express ([container-registry.oracle.com/database/express:latest](https://container-registry.oracle.com/database/express:latest)), com IP público e porta 1521.
 
Azure Container Instances (ACI) - Aplicação (api-dotnet): Executa o container da API desenvolvida em .NET na porta 8080, conectando-se dinamicamente ao IP do banco de dados.

Persistência de Dados: Utiliza uma conta de armazenamento (volumeaegisdata565206) e um File Share (oracle-aegis-volume) mapeados no caminho de dados do Oracle para garantir que as informações não sejam perdidas ao reiniciar os containers.

Segurança e Credenciais: Utiliza o Azure Key Vault (keyvault-aegis-565206) para armazenar e injetar de forma segura senhas, strings de conexão e credenciais do ACR, além de configurar o container da API para rodar sob um usuário restrito (non-root USER app).  

How To

* **Passo 1: Autenticação Inicial**

* **Onde executar:** No **Azure Cloud Shell**.
* **Comando:**
```bash
az login

```




* **Passo 2: Criação da Storage Account (Persistência)**

* **Onde executar:** No **Azure Cloud Shell**.
* **Arquivo correspondente:** `criando_storage_account.sh`

* **Comando:**
```bash
./criando_storage_account.sh

```




* **Passo 3: Configuração do Key Vault e Segredos**

* **Onde executar:** No **Azure Cloud Shell**.
* **Arquivo correspondente:** `criando_key_value.sh`

* **Comando:**
```bash
./criando_key_value.sh

```




* **Passo 4: Build e Deploy do Banco de Dados Oracle no ACI**

* **Onde executar:** No **Azure Cloud Shell**.
* **Arquivo correspondente:** `deploy_oracle_aci.sh`

* **Comando:**
```bash
./deploy_oracle_aci.sh

```




* **Passo 5: Build e Push da API (Local para ACR)**

* **Onde executar:** No seu **computador local** (com Docker Desktop rodando e na raiz da API).
* **Comandos:**
```bash
az acr login --name aegisrm565206
docker build -t aegisrm565206.azurecr.io/rm565206-api:v1 .
docker push aegisrm565206.azurecr.io/rm565206-api:v1

```




* **Passo 6: Deploy da API .NET no ACI**

* **Onde executar:** No **Azure Cloud Shell**.
* **Arquivo correspondente:** `deploy_api.sh`

* **Comando:**
```bash
./deploy_api.sh

```