-- 1. Criação do Banco de Dados
CREATE DATABASE LeveInvestimentosDB;
GO

-- 2. Seleciona o novo banco para criar as tabelas dentro dele
USE LeveInvestimentosDB;
GO

-- 3. Criação da Tabela de Usuários
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NomeCompleto VARCHAR(150) NOT NULL,
    DataNascimento DATE NOT NULL,
    TelefoneFixo VARCHAR(20) NULL,
    TelefoneCelular VARCHAR(20) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Senha VARCHAR(255) NOT NULL,
    Endereco VARCHAR(255) NOT NULL,
    FotoCaminho VARCHAR(255) NULL,
    IsGestor BIT NOT NULL DEFAULT 0
);
GO

-- 4. Criação da Tabela de Tarefas
CREATE TABLE Tarefas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    MensagemDescritiva VARCHAR(MAX) NOT NULL,
    DataLimite DATETIME NOT NULL,
    StatusTarefa VARCHAR(20) NOT NULL DEFAULT 'Pendente',
    GestorId INT NOT NULL,
    SubordinadoId INT NOT NULL,
    CONSTRAINT FK_Tarefas_Gestor FOREIGN KEY (GestorId) REFERENCES Usuarios(Id),
    CONSTRAINT FK_Tarefas_Subordinado FOREIGN KEY (SubordinadoId) REFERENCES Usuarios(Id)
);
GO

-- 5. Carga Inicial do Usuário TI
INSERT INTO Usuarios (NomeCompleto, DataNascimento, TelefoneFixo, TelefoneCelular, Email, Senha, Endereco, IsGestor)
VALUES (
    'Administrador TI', 
    '1990-01-01', 
    '(11) 2537-7777', 
    '(11) 99999-9999', 
    'ti@leveinvestimentos.com.br', 
    'teste123', 
    'Praça Maastricht, nº 200, Bragança Paulista - SP', 
    1
);
GO