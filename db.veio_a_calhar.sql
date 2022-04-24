CREATE DATABASE veio_a_calhar_db
USE veio_a_calhar_db

CREATE TABLE Fornecedores(
    Id BIGINT PRIMARY KEY IDENTITY NOT NULL,
    Nome VARCHAR(64) NOT NULL,
    Descricao VARCHAR(MAX),
    Endereco VARCHAR(32) NOT NULL,
    CNPJ VARCHAR(14) NOT NULL,
)
go

CREATE TABlE Fornecedores_Telefones(
    Id BIGINT PRIMARY KEY IDENTITY NOT NULL,
    Id_Fornecedor BIGINT FOREIGN KEY REFERENCES Fornecedores,
    Telefone VARCHAR(14) NOT NULL
)
go

CREATE TABLE Tipos_Unidades(
    Id BIGINT PRIMARY KEY IDENTITY NOT NULL,
    Nome VARCHAR(16) NOT NULL,
    Abreviacao VARCHAR(8) NOT NULL,
)
go

CREATE TABLE Produtos(
    Id BIGINT PRIMARY KEY IDENTITY NOT NULL,
    Id_Fornecedor BIGINT FOREIGN KEY REFERENCES Fornecedores,
    Nome VARCHAR(32) NOT NULL,
    Descricao VARCHAR(MAX) NOT NULL,
    Custo BIGINT NOT NULL,
    Valor BIGINT NOT NULL,
    Quantidade DECIMAL(8, 2) NOT NULL,
    Tipo_Unidade BIGINT FOREIGN KEY REFERENCES Tipos_Unidades
)
go

CREATE TABLE Tipos_Pessoa(
    Id BIGINT PRIMARY KEY IDENTITY NOT NULL,
    Tipo VARCHAR(8) NOT NULL
)
go

CREATE TABLE Pessoas(
    Id BIGINT PRIMARY KEY IDENTITY NOT NULL,
    Nome VARCHAR(32) NOT NULL,
    CPF VARCHAR(11),
    Endereco VARCHAR(32) NOT NULL,
    Tipo_Pessoa BIGINT REFERENCES Tipos_Pessoa
)
go

CREATE TABlE Pessoas_Telefones(
    Id BIGINT PRIMARY KEY IDENTITY NOT NULL,
    Id_Pessoa BIGINT FOREIGN KEY REFERENCES Pessoas,
    Telefone VARCHAR(14) NOT NULL
)
go

CREATE TABLE Clientes(
    Id_Pessoa BIGINT PRIMARY KEY REFERENCES Pessoas(Id),
)
go

CREATE TABLE Funcionarios(
    Id_Pessoa BIGINT PRIMARY KEY REFERENCES Pessoas(Id),
    Cargo VARCHAR(16) NOT NULL
)
go

CREATE TABLE Vendas(
    Id BIGINT PRIMARY KEY IDENTITY NOT NULL,
    Cliente BIGINT NOT NULL REFERENCES Clientes,
    Data DATETIME NOT NULL
)
go

CREATE TABLE Vendas_Produtos(
    Id_Venda BIGINT NOT NULL REFERENCES Vendas,
    Id_Produto BIGINT NOT NULL REFERENCES Produtos,
    Quantidade INT NOT NULL,
    Valor BIGINT NOT NULL,
    CHECK (Quantidade > 0),
    PRIMARY KEY (Id_Venda, Id_Produto)
)
go
