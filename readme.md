# PlantCare API

Backend desenvolvido em .NET Core (C#) para o gerenciamento e monitoramento de plantas, utilizando banco de dados Oracle.

## Integrantes

Este projeto foi desenvolvido por:

| Nome Completo | RM |
|---------------|-----|
| João Victor Alves da Silva | 559726 |
| Vinicius Kenzo Tocuyosi | 559982 |
| Juan Pablo Rebelo Coelho | 560445 |


## Features

### 1. Monitoramento e Observabilidade
* **Health Checks:** Adicionado endpoint dedicado para verificar a saúde da conexão com o banco de dados Oracle.
* **Tracing e Métricas:** Integração com **Application Insights** via Auto-Instrumentation para rastrear requisições HTTP, tempo de resposta e consultas SQL.
* **Logging Estruturado:** Configuração do **Serilog** substituindo o logger padrão. Injeção de logs estratégicos (Information, Warning, Error) nas Controllers, formatados para facilitar a indexação e análise no console.

### 2. Testes Automatizados (Padrão AAA)
A solução foi modularizada para suportar uma cultura de testes automatizados:
* **Testes Unitários (`PlantCare-api.Tests.Unit`):** Utilizando `xUnit` e `Moq` para validar as regras de negócio da camada de *Service* (ex: tratamento de strings e validações) isolando o acesso ao banco de dados.
* **Testes de Integração (`PlantCare-api.Tests.Integration`):** Utilizando `WebApplicationFactory` para levantar a API em memória e testar o fluxo HTTP de ponta a ponta nas *Controllers*, aplicando injeção de dependência customizada para isolamento da infraestrutura externa durante os testes.


---

## Endpoints da API

A documentação interativa completa pode ser acessada via Swagger navegando para `/swagger` quando a aplicação estiver em execução. Abaixo estão as rotas principais:

### Monitoramento
* `GET /health` : Retorna o status de saúde da aplicação e sua comunicação com o banco Oracle (Retornos esperados: `Healthy` ou `Unhealthy`).

### Usuários
* `POST /api/usuario` : Cadastra um novo usuário no sistema.

### Plantas
* `GET /api/planta` : Retorna a lista de todas as plantas cadastradas.
* `GET /api/planta/{id}` : Retorna os detalhes de uma planta específica pelo seu ID.
* `POST /api/planta` : Cadastra uma nova planta vinculada a um usuário existente.
* `PUT /api/planta/{id}` : Atualiza os dados completos de uma planta.
* `DELETE /api/planta/{id}` : Remove uma planta do sistema.

---

## Como executar o projeto

### 1. Rodando a API Localmente
Certifique-se de configurar a sua *Connection String* do Oracle no arquivo `appsettings.json`.

```bash
# Entre na pasta do projeto principal
cd PlantCare-api

# Execute a aplicação
dotnet run
```

#### Rodando testes

```bash
dotnet test
```