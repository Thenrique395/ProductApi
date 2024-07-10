"# ProductApi" 

# Product Management API

## Descrição

Esta é a API de gerenciamento de produtos desenvolvida em .NET 8.0, utilizando Entity Framework Core, Docker, e Kafka. A API permite operações de CRUD em produtos e utiliza o Kafka para publicar eventos de criação de produtos.

## Pré-requisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)

## Configuração do Projeto

### 1. Clonar o Repositório

Clone o repositório para a sua máquina local:

git clone https://github.com/seu-usuario/product-management-api.git
cd product-management-api

### 2. Configurar o Docker

Certifique-se de que o Docker está instalado e em execução. Em seguida, execute o comando abaixo para construir e iniciar os containers:

docker-compose up --build

### 3. Adicionar a Primeira Migração
Depois de configurar o Docker, adicione a primeira migração do Entity Framework:

dotnet ef migrations add InitialCreate

###4. Atualizar o Banco de Dados
Atualize o banco de dados para aplicar as migrações:

dotnet ef database update


###5. Executar a Aplicação
Execute a aplicação utilizando o .NET CLI:

dotnet run


### Observações
Alguns pontos que não foram melhor implementado por falta de tempo. Utilizei 2 horas para fazer esse projeto. 
Qualquer Duvida estou a disposição






