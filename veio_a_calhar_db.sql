USE master
DROP DATABASE IF EXISTS veio_a_calhar_db 
CREATE DATABASE veio_a_calhar_db
USE veio_a_calhar_db

CREATE TABLE Pessoas(
    Id INT PRIMARY KEY IDENTITY,
    Nome VARCHAR(32) NOT NULL,
    Observacoes VARCHAR(MAX),
	PIX VARCHAR(64),
	Email VARCHAR(64),
)

CREATE TABLE Pessoas_Enderecos(
    Id INT PRIMARY KEY IDENTITY,
    Id_Pessoa INT FOREIGN KEY REFERENCES Pessoas(Id),
    Logradouro VARCHAR(32) NOT NULL,
    Numero INT NOT NULL,
    Bairro VARCHAR(32) NOT NULL,
    Cidade VARCHAR(16) NOT NULL,
    CEP VARCHAR(9),
    Observacao VARCHAR(1024)
)

CREATE TABlE Pessoas_Telefones(
    Id INT PRIMARY KEY IDENTITY,
    Id_Pessoa INT FOREIGN KEY REFERENCES Pessoas(Id),
    Numero VARCHAR(14) NOT NULL,
    Observacao VARCHAR(1024)
)

CREATE TABLE Pessoas_Juridicas(
    Id INT PRIMARY KEY REFERENCES Pessoas(Id),
    Nome_Fantasia VARCHAR(64),
    IE VARCHAR(14),
    CNPJ VARCHAR(14)
)

CREATE TABLE Pessoas_Fisicas(
    Id INT PRIMARY KEY REFERENCES Pessoas(Id),
    CPF VARCHAR(11),
    RG VARCHAR(12)
)

CREATE TABLE Clientes(
    Id INT PRIMARY KEY REFERENCES Pessoas(Id),
)

CREATE TABLE Cargos(
    Id INT PRIMARY KEY IDENTITY,
	Nome VARCHAR(32) NOT NULL,
)

INSERT INTO Cargos VALUES ('Dono')
INSERT INTO Cargos VALUES ('Ajudante')

CREATE TABLE Usuarios(
    Id INT PRIMARY KEY IDENTITY,
    Nome VARCHAR(50) NOT NULL,
	Senha VARCHAR(50) NOT NULL,
	Data_Cadastro DATETIME NOT NULL,
	Ativo BIT NOT NULL
)

CREATE TABLE Funcionarios(
    Id INT PRIMARY KEY REFERENCES Pessoas_Fisicas(Id),
    Id_Cargo INT FOREIGN KEY REFERENCES Cargos(Id),
	Id_Usuario INT FOREIGN KEY REFERENCES Usuarios(Id),
	Salario INT NOT NULL
)

CREATE TABLE Fornecedores(
    Id INT PRIMARY KEY REFERENCES Pessoas_Juridicas(Id),
)

CREATE TABLE Unidades(
    Id INT PRIMARY KEY IDENTITY,
    Nome VARCHAR(32) NOT NULL,
    Sigla VARCHAR(8) NOT NULL,
)

INSERT INTO Unidades VALUES ('Unitário', 'Un')
INSERT INTO Unidades VALUES ('Kilograma', 'Kg')
INSERT INTO Unidades VALUES ('Metros', 'm')
INSERT INTO Unidades VALUES ('Metros Quadrados', 'm²')

CREATE TABLE Produtos(
    Id INT PRIMARY KEY IDENTITY,
    Id_Fornecedor INT FOREIGN KEY REFERENCES Fornecedores(Id),
    Nome VARCHAR(32) NOT NULL,
    Descricao VARCHAR(MAX) NOT NULL,
    Preco_Custo INT NOT NULL,
    Preco_Venda INT NOT NULL,
    Quantidade DECIMAL(8, 2) NOT NULL,
    Id_Unidade INT FOREIGN KEY REFERENCES Unidades(Id)
)

CREATE TABLE Formas_Pagamento(
    Id INT PRIMARY KEY IDENTITY,
	Nome VARCHAR(32) NOT NULL,
	Maximo_Parcelas INT NOT NULL,
)

INSERT INTO Formas_Pagamento VALUES('Dinheiro', 1)
INSERT INTO Formas_Pagamento VALUES('Boleto', 1)
INSERT INTO Formas_Pagamento VALUES('Pix', 1)
INSERT INTO Formas_Pagamento VALUES('Cartão Crédito', 12)
INSERT INTO Formas_Pagamento VALUES('Cartão Débito', 1)

CREATE TABLE Pagamentos(
    Id INT PRIMARY KEY IDENTITY,
	Id_Pagador INT FOREIGN KEY REFERENCES Pessoas(Id),
	Id_Favorecido INT FOREIGN KEY REFERENCES Pessoas(Id),
	Id_Forma_Pagamento INT FOREIGN KEY REFERENCES Formas_Pagamento(Id),
)

CREATE TABLE Parcelas(
    Id INT PRIMARY KEY IDENTITY,
	Id_Pagamento INT FOREIGN KEY REFERENCES Pagamentos(Id),
	Numero INT NOT NULL,
	Valor INT NOT NULL,
	Porcentagem_Desconto DECIMAL(3, 2) NOT NULL,
	Valor_Desconto INT NOT NULL,
	Valor_Pago INT,
	Data_Vencimento DATE NOT NULL,
	Data_Pagamento DATE
)

CREATE TABLE Status_Transacaos(
    Id INT PRIMARY KEY IDENTITY,
	[Status] VARCHAR(32) NOT NULL,
)

INSERT INTO Status_Transacaos VALUES ('Orçamento')
INSERT INTO Status_Transacaos VALUES ('Em aberto')
INSERT INTO Status_Transacaos VALUES ('Concluído')
INSERT INTO Status_Transacaos VALUES ('Cancelado')

CREATE TABLE Transacoes(
    Id INT PRIMARY KEY IDENTITY,
	Id_Pagamento INT FOREIGN KEY REFERENCES Pagamentos(Id),
	Id_Status INT FOREIGN KEY REFERENCES Status_Transacaos(Id),
    Data_Criacao DATETIME NOT NULL,
	Data_Fechamento DATETIME,
	Observacoes VARCHAR(MAX)
)

CREATE TABLE Transacoes_Produtos(
    Id INT PRIMARY KEY IDENTITY,
    Id_Transacoes INT REFERENCES Transacoes(Id),
    Id_Produto INT REFERENCES Produtos(Id),
    Quantidade INT NOT NULL,
    Valor_Unitario INT NOT NULL,
	Valor_Total INT NOT NULL,
	Desconto_Unitario INT NOT NULL
)

CREATE TABLE Vendas(
    Id INT PRIMARY KEY REFERENCES Transacoes(Id),
	Previsao_Inicio DATETIME NOT NULL,
	Previsao_Entrega DATETIME NOT NULL
)

CREATE TABLE Venda_Clientes(
    Id INT PRIMARY KEY IDENTITY,
	Id_Venda INT FOREIGN KEY REFERENCES Vendas(Id),
	Id_Cliente INT FOREIGN KEY REFERENCES Clientes(Id)
)

CREATE TABLE Venda_Funcionarios(
    Id INT PRIMARY KEY IDENTITY,
	Id_Venda INT FOREIGN KEY REFERENCES Vendas(Id),
	Id_Funcionario INT FOREIGN KEY REFERENCES Funcionarios(Id)
)

CREATE TABLE Compras(
    Id INT PRIMARY KEY REFERENCES Transacoes(Id),
    Id_Fornecedor INT FOREIGN KEY REFERENCES Fornecedores(Id),
	Data_Compra DATETIME NOT NULL,
	Data_Entrega DATETIME NOT NULL,
)