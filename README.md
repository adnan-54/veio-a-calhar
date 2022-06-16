# Veio a Calhar
Trabalho interdiciplinar da Fatec São José do Rio Preto, para o curso de Analise e Desenvolvimento de Sistemas

## Sobre
O projeto trata-se de um software para gerenciamento de uma empresa especializada em calhas e rufos. 

Todos os processos da empresa eram feitos manualmente com papel e caneta, por isso, carinhosamente apelidamos o projeto de Veio a Calhar.

Neste repositorio encontra-se todo código fonte utilizado para criar o software. 
Encontram-se também o script SQL para criação do banco de dados e todos os materiais criados/utilizados durante todo o processo da analise e do desenvolvimento do sistema.

## Tecnologias Utilizadas
- Asp Net Core
- SQL Server

## Dependências
- .NET SDK > 6.0.301
- SQL Server > 2019

## Como Executar
- Execute o script ```veio_a_calhar_db.sql``` do banco de dados no SQLServer
- Atualize a connection string encontrada no arquivo ```.\src\VeioACalhar\appsettings.json```
- Execute os seguintes comandos no diretório principal
```
cd .\src\VeioACalhar\
dotnet restore
dotnet run
```

## Integrantes
- Adnan Silva Pedroso
- Lance Armstrong Ferreira
- Lauro Luis Zarpelao Precioso
- Taylor Rayan de Araujo Fernandes
