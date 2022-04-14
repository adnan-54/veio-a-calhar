CREATE DATABASE [veio_a_calhar_db]
USE [veio_a_calhar_db]

CREATE TABLE [Produtos](
    [Id] BIGINT PRIMARY KEY IDENTITY NOT NULL,
    [Nome] TEXT NOT NULL,
    [Descricao] TEXT NOT NULL,
    [Valor] BIGINT NOT NULL,
    [Quantidade] DECIMAL(6, 2) NOT NULL,
    [Unidade] TEXT NOT NULL
)
go

CREATE TABLE [Tipos_Pessoa](
    [Id] BIGINT PRIMARY KEY IDENTITY NOT NULL,
    [Tipo] VARCHAR(10) NOT NULL
)
go

CREATE TABLE [Pessoas](
    [Id] BIGINT PRIMARY KEY IDENTITY NOT NULL,
    [Nome] TEXT NOT NULL,
    [Documento] TEXT,
    [Telefone] TEXT,
    [Endereco] TEXT NOT NULL,
    [Tipo_Pessoa] BIGINT REFERENCES [Tipos_Pessoa]
)
go

CREATE TABLE [Clientes](
    [Id] BIGINT PRIMARY KEY IDENTITY NOT NULL REFERENCES [Pessoas],
    [Id_Pessoa] BIGINT FOREIGN KEY REFERENCES [Pessoas],
)
go

CREATE TABLE [Fornecedores](
    [Id] BIGINT PRIMARY KEY IDENTITY NOT NULL REFERENCES [Pessoas],
    [Id_Pessoa] BIGINT FOREIGN KEY REFERENCES [Pessoas],
)
go

CREATE TABLE [Vendas](
    [Id] BIGINT PRIMARY KEY IDENTITY NOT NULL,
    [Cliente] BIGINT NOT NULL REFERENCES [Clientes],
    [Data] DATETIME NOT NULL
)
go

CREATE TABLE [Vendas_Produtos](
    [Id_Venda] BIGINT NOT NULL REFERENCES [Vendas],
    [Id_Produto] BIGINT NOT NULL REFERENCES [Produtos],
    [Quantidade] INT NOT NULL,
    [Valor] BIGINT NOT NULL,
    CHECK (Quantidade > 0),
    PRIMARY KEY (Id_Venda, Id_Produto)
)
go

CREATE TABLE [Pagamentos](
    [Id] BIGINT PRIMARY KEY IDENTITY NOT NULL,
    [Credor] BIGINT NOT NULL REFERENCES [Pessoas],
    [Valor] BIGINT NOT NULL,
)
go