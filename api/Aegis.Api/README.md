# AEGIS - Plataforma de Gestão de Lixo Espacial

Projeto desenvolvido para otimizar a segurança orbital através da monitorização e gestão de detritos espaciais.

## 👥 Integrantes
* **João Pedro Pereira Camilo** | RM562005
* **Lucas Matsubara Reis** | RM565020
* **Pamella Christiny Chaves Brito** | RM565206

---

## 1. Problema Abordado
O crescimento exponencial de detritos espaciais em órbita terrestre baixa (LEO) — fenômeno conhecido como Síndrome de Kessler — cria uma ameaça crítica à integridade de satélites ativos, infraestruturas de telecomunicações e tripulações. Atualmente, a gestão dessas ameaças é fragmentada, com a ausência de um sistema centralizado que correlacione, em tempo real, a trajetória de detritos perigosos com a localização de ativos espaciais.

## 2. Objetivos da Solução
A Plataforma AEGIS visa transformar a segurança orbital através de:
* **Monitoramento Centralizado:** Consolidar dados de ativos e detritos em uma base íntegra.
* **Automação de Resposta:** Reduzir a latência operacional através de missões de interceptação automatizadas.
* **Visibilidade de Risco:** Permitir o cálculo imediato de risco de colisão.
* **Gestão de Ciclo de Vida:** Acompanhar desde a identificação do risco até a execução da missão.

---

## 3. Requisitos Técnicos
* **Arquitetura:** API REST com separação de camadas (Controllers, Services, Repositories).
* **Persistência:** Banco de Dados Oracle.
* **ORM:** Entity Framework Core.
* **Relacionamentos:** Mapeamento de 1:N e N:N conforme as regras de negócio.
* **Gestão de Banco:** Uso de Migrations para versionamento do esquema.

---

## 4. Como Executar

### Pré-requisitos
1. Ter o **.NET SDK** instalado.
2. Configurar a *Connection String* no `appsettings.json`.

### Instalação de Dependências
No terminal, dentro da pasta do projeto, execute:
```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Oracle.EntityFrameworkCore
dotnet add package Microsoft.AspNetCore.Mvc

Banco de Dados
Para atualizar o banco de dados (Migrations):

Bash
dotnet ef database update
Links
Repositório GitHub: https://github.com/Portifolio-Pamella/sistema-gestao-lixo-espacial-dotnet.git

Documentação API (Swagger): http://localhost:5274/swagger/index.html

5. Exemplos de Teste (Ordem de Execução)
IMPORTANTE: Devido às chaves estrangeiras (Foreign Keys) do banco relacional, os registros devem ser criados seguindo esta ordem rigorosa.

Passo 1: Criar Empresa
JSON
{
  "nome": "SpaceX Solutions",
  "cnpj": "12345678000199",
  "paisOrigem": "EUA",
  "status": "ATIVO"
}
Passo 2: Criar Detrito
JSON
{
  "nome": "Detrito-001",
  "massaKg": 150.5,
  "tamanhoMetros": 2.5,
  "coordenadaX": 10.5,
  "coordenadaY": 20.0,
  "coordenadaZ": 30.5
}
Passo 3: Criar Satélite (Use o ID da empresa criada)
JSON
{
  "numeroSatelite": "STAR-001",
  "altitudeKm": 550.5,
  "empresaId": 1
}
Passo 4: Criar Alerta (Use o ID do Satélite e Detrito criados)
JSON
{
  "sateliteId": 1,
  "detritoId": 1,
  "statusGravidade": "ALTA"
}
Passo 5: Criar Chaser
JSON
{
  "nome": "Chaser-Alfa-01",
  "bateria": 95,
  "coordenadaX": 10.5,
  "coordenadaY": 20.0,
  "coordenadaZ": 5.0
}
Passo 6: Criar Missão de Interceptação (Use o ID do Alerta e Chaser criados)
JSON
{
  "alertaId": 1,
  "chaserId": 1,
  "dataExecucao": "2026-06-05T14:26:54.896",
  "status": "PENDENTE"
}