# API Task Management

This project was generated with:

- [.NET 8](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)
- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

## Features

- Create a new project.
- Add new task from project.
- Remove a task from project.
- Edit a task from project.
- Get tasks from project.
- Report about how tasks is done and how do it.

## Installation

## Run with Docker Compose

## Clone this project:

```sh
clone https://github.com/zWalterli/TaskManagement.git
```

## Execute the Docker Compsoe

```sh
docker-compose up
```

## Enjoy

```sh
http://localhost:5000/swagger/index.html
```

# Run with Docker

## Download the mysql

```sh
docker pull mysql
docker run --name mysql -e MYSQL_ROOT_PASSWORD=root_password_2023 -d -p 3306:3306 mysql
```

## Make a API Image

```sh
docker build -t taskmanagement-api .
```

## Run the Image API

```sh
docker run -p 5000:8080 taskmanagement-api
```

## Enjoy

Para utilizar a API, é necssário enviar via HEADER o identificador do usuário.
No caso, deve adicionar o header:

```sh
"userId" com o valor "1" (Caso usuário com ID 1)
```

# Rota para a API

```sh
http://localhost:5000/
```

## Refinamento

| Título            | Descrição                                                                                                                                                                                                                                                                                                                      |
| ----------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| Front-end         | A existência de um frontend seria muito bom para a utilizaçao do sistema, uma vez que o usuário náo precisaria ficar realizando requests diretas nas rotas da API                                                                                                                                                              |
| Paginação         | Não se sabe o quanto esse sistema vai escalar, A API foi desenvolvida para que seja testada como uma POC, mas posteriormente o ideal para anão oneração do servidor/API seria utilizar um sistema de páginação no back-end.                                                                                                    |
| Sistema de Perfil | Uma melhora na integracao da API que realiza a autenticaçao seria bom, gostaria de receber um token jwt ou um session, assim poderei validar se o usuário está autenticado e válido, além de poder buscar alguns informaões nas "clams" caso for jwt.                                                                          |
| Logs              | Melhora nos sistema de Logs da aplicaçao, deveria colocar logs de utilização, logs nas linhas de códigos das exceptions, assim melhorando uma possível análise de erro                                                                                                                                                         |
| Monitoramento     | Junto do log, poderia haver uma melhoria no monitoramento da API, adicionando por exemplo healthcheck para validar se a API e o banco estão funcionando, além disso, adicionar DataDog (need to pay) ou NewRelic (Free tier) para melhorar ainda mais a visualizão das "vidas" das requests e monitoramento da API em um geral |
| Cache             | Adicionar um cache (Redis) para que sempre que o usuário buscar os dados que se repetem muito, exemplo seria a tabela PROJETO, caso ocorra um GET na rota de projeto ele deve buscar os dados no cache, caso não tenha, deve buscar no banco de dados.                                                                         |

## Final

Conforme descrito na parte de "Refinamento", alguns pontos para melhorar a performance do projeto, observabilidade e até mesmo identificação do usuário já foram falados.
Mas falando sobre o deploy dessa API, seria interessante usar alguma pipeline CI/CD, como por exemplo, a pipeline da Azure DevOps. Nela você tem tudo que precisa para orquestrar o deploy do seu sistema, podendo haver multiplos ambientes (DEV, TST, PRD). Além do mais, seria interessante, utilizar o kubbernets para poder orquestrar as PODS dessa API, uma vez que se ela receber muitas requests podemos escalar ela horizontalmente, isso é, criar uma API clone que tem o intuito de dividir as requisições e processamentos.
