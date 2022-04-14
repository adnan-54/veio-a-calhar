CREATE DATABASE [veio_a_calhar_db]
USE [veio_a_calhar_db]

CREATE TABLE [Products](
    [Id] BIGINT PRIMARY KEY IDENTITY NOT NULL,
    [Name] TEXT NOT NULL,
    [Description] TEXT NOT NULL,
    [Price] INT NOT NULL,
    [Quantity] DECIMAL(6, 2) NOT NULL,
    [Unity] TEXT NOT NULL
)
go

CREATE TABLE [Person_Types](
    [Id] BIGINT PRIMARY KEY IDENTITY NOT NULL,
    [Type] VARCHAR(10) NOT NULL
)
go

CREATE TABLE [Person](
    [Id] BIGINT PRIMARY KEY IDENTITY NOT NULL,
    [Name] TEXT NOT NULL,
    [Document] TEXT,
    [Phone] TEXT,
    [Address] TEXT NOT NULL,
    [Person_Type] BIGINT REFERENCES [Person_Types]
)
go

CREATE TABLE [Customers](
    [Id] BIGINT PRIMARY KEY IDENTITY NOT NULL REFERENCES [Person],
)
go

CREATE TABLE [Providers](
    [Id] BIGINT PRIMARY KEY IDENTITY NOT NULL REFERENCES [Person],
)
go

CREATE TABLE [Sales](
    [Id] BIGINT PRIMARY KEY IDENTITY NOT NULL,
    [Customer] BIGINT NOT NULL REFERENCES [Customers],
    [Date] DATETIME NOT NULL
)
go

CREATE TABLE [Sales_Products](
    [Sales_Id] BIGINT NOT NULL REFERENCES [SALES],
    [Product_Id] BIGINT NOT NULL REFERENCES [PRODUCTS],
    [Quantity] INT NOT NULL,
    [Value] INT NOT NULL,
    CHECK (Quantity > 0),
    PRIMARY KEY (Sales_Id, Product_Id)
)
go