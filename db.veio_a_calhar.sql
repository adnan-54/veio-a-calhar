USE master

DROP DATABASE IF EXISTS veio_a_calhar_db 

CREATE DATABASE veio_a_calhar_db

USE veio_a_calhar_db

CREATE TABLE Fornecedores(
    Id BIGINT PRIMARY KEY IDENTITY,
    Nome VARCHAR(64) NOT NULL,
    Descricao VARCHAR(MAX),
    Endereco VARCHAR(32) NOT NULL,
    CNPJ VARCHAR(14) NOT NULL,
	PIX VARCHAR(64)
)

CREATE TABlE Fornecedores_Telefones(
    Id BIGINT PRIMARY KEY IDENTITY,
    Id_Fornecedor BIGINT FOREIGN KEY REFERENCES Fornecedores(Id),
    Telefone VARCHAR(14) NOT NULL
)

CREATE TABLE Tipos_Unidades(
    Id BIGINT PRIMARY KEY IDENTITY,
    Nome VARCHAR(32) NOT NULL,
    Abreviacao VARCHAR(8) NOT NULL,
)

INSERT INTO Tipos_Unidades VALUES ('Unitário', 'Un')
INSERT INTO Tipos_Unidades VALUES ('Kilograma', 'Kg')
INSERT INTO Tipos_Unidades VALUES ('Metros', 'm')
INSERT INTO Tipos_Unidades VALUES ('Metros Quadrados', 'm²')

CREATE TABLE Produtos(
    Id BIGINT PRIMARY KEY IDENTITY,
    Id_Fornecedor BIGINT FOREIGN KEY REFERENCES Fornecedores(Id),
    Nome VARCHAR(32) NOT NULL,
    Descricao VARCHAR(MAX) NOT NULL,
    Preco_Custo BIGINT NOT NULL,
    Preco_Venda BIGINT NOT NULL,
    Quantidade DECIMAL(8, 2) NOT NULL,
    Id_Unidade BIGINT FOREIGN KEY REFERENCES Tipos_Unidades(Id)
)

CREATE TABLE Pessoas(
    Id BIGINT PRIMARY KEY IDENTITY,
    Nome VARCHAR(32) NOT NULL,
    CPF VARCHAR(11),
    Endereco VARCHAR(32) NOT NULL,
	PIX VARCHAR(64)
)

CREATE TABlE Pessoas_Telefones(
    Id BIGINT PRIMARY KEY IDENTITY,
    Id_Pessoa BIGINT FOREIGN KEY REFERENCES Pessoas(Id),
    Telefone VARCHAR(14) NOT NULL
)

CREATE TABLE Clientes(
    Id BIGINT PRIMARY KEY REFERENCES Pessoas(Id),
)

CREATE TABLE Cargos(
    Id BIGINT PRIMARY KEY IDENTITY,
	Cargo VARCHAR(32) NOT NULL,
)

INSERT INTO Cargos VALUES ('Dono')
INSERT INTO Cargos VALUES ('Ajudante')

CREATE TABLE Funcionarios(
    Id BIGINT PRIMARY KEY REFERENCES Pessoas(Id),
    Cargo BIGINT FOREIGN KEY REFERENCES Cargos(Id),
	Salario BIGINT NOT NULL
)

CREATE TABLE Formas_Pagamento(
    Id BIGINT PRIMARY KEY IDENTITY,
	Forma VARCHAR(32) NOT NULL,
)

INSERT INTO Formas_Pagamento VALUES('Dinheiro')
INSERT INTO Formas_Pagamento VALUES('Pix')
INSERT INTO Formas_Pagamento VALUES('Cartão Crédito')
INSERT INTO Formas_Pagamento VALUES('Cartão Débito')

CREATE TABLE Pagamentos(
    Id BIGINT PRIMARY KEY IDENTITY,
	Id_Forma BIGINT FOREIGN KEY REFERENCES Formas_Pagamento(Id),
	Valor BIGINT NOT NULL,
	Forma_Pagamento VARCHAR(16) NOT NULL,
	Numero_Parcelas INT NOT NULL
)

CREATE TABLE Parcelas(
    Id BIGINT PRIMARY KEY IDENTITY,
	Id_Pagamento BIGINT FOREIGN KEY REFERENCES Pagamentos(Id),
	Numero_Parcela INT NOT NULL,
	Valor_Parcela BIGINT NOT NULL,
	Valor_Pago BIGINT NOT NULL,
	Data_Vencimento DATE NOT NULL,
	Data_Pagamento DATE NOT NULL
)

CREATE TABLE Status_Venda(
    Id BIGINT PRIMARY KEY IDENTITY,
	[Status] VARCHAR(32) NOT NULL,
)

INSERT INTO Status_Venda VALUES ('Orçamento')
INSERT INTO Status_Venda VALUES ('Em aberto')
INSERT INTO Status_Venda VALUES ('Concluído')
INSERT INTO Status_Venda VALUES ('Cancelado')

CREATE TABLE Vendas(
    Id BIGINT PRIMARY KEY IDENTITY,
	Id_Pagamento BIGINT FOREIGN KEY REFERENCES Pagamentos(Id),
	Id_Status BIGINT FOREIGN KEY REFERENCES Status_Venda(Id),
	Data_Criacao DATETIME NOT NULL,
	Data_Fechamento DATETIME,
	Previsao_Inicio DATETIME NOT NULL,
	Previsao_Entrega DATETIME NOT NULL,
	Valor_Total BIGINT NOT NULL,
	Valor_Desconto BIGINT NOT NULL,
	Observacoes VARCHAR(MAX),
)

CREATE TABLE Venda_Clientes(
    Id BIGINT PRIMARY KEY IDENTITY,
	Id_Venda BIGINT FOREIGN KEY REFERENCES Vendas(Id),
	Id_Cliente BIGINT FOREIGN KEY REFERENCES Clientes(Id),
)

CREATE TABLE Venda_Funcionarios(
    Id BIGINT PRIMARY KEY IDENTITY,
	Id_Venda BIGINT FOREIGN KEY REFERENCES Vendas(Id),
	Id_Funcionario BIGINT FOREIGN KEY REFERENCES Funcionarios(Id),
)

CREATE TABLE Vendas_Produtos(
    Id_Venda BIGINT REFERENCES Vendas(Id),
    Id_Produto BIGINT REFERENCES Produtos(Id),
    Quantidade INT NOT NULL,
    Valor_Unitario BIGINT NOT NULL,
	Valor_Total BIGINT NOT NULL,
	Desconto_Unitario INT NOT NULL,
    CHECK (Quantidade > 0),
    PRIMARY KEY (Id_Venda, Id_Produto)
)

CREATE TABLE Contas(
	Id BIGINT PRIMARY KEY IDENTITY,
	Id_Fornecedor BIGINT FOREIGN KEY REFERENCES Fornecedores(Id),
	Id_Pessoa BIGINT FOREIGN KEY REFERENCES Pessoas(Id),
	Id_Pagamento BIGINT FOREIGN KEY REFERENCES Pagamentos(Id),
	Data_Vencimento DATETIME NOT NULL,
	Observacoes VARCHAR(MAX),
	CHECK(Id_Fornecedor IS NULL AND Id_Pessoa IS NOT NULL 
        OR Id_Fornecedor IS NOT NULL AND Id_Pessoa IS NULL)
)

CREATE TABLE Contas_Pagar(
    Id BIGINT PRIMARY KEY IDENTITY,
    Id_Conta BIGINT FOREIGN KEY REFERENCES Contas(Id),
)

CREATE TABLE Contas_Receber(
    Id BIGINT PRIMARY KEY IDENTITY,
    Id_Conta BIGINT FOREIGN KEY REFERENCES Contas(Id),
)