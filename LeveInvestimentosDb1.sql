-- 1. Criação do Banco de Dados
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'LeveInvestimentosDb')
BEGIN
    CREATE DATABASE LeveInvestimentosDb;
END
GO

USE LeveInvestimentosDb;
GO

-- 2. Criação da Tabela de Usuários
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuarios]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Usuarios] (
        [Id] INT IDENTITY(1,1) NOT NULL,
        [NomeCompleto] NVARCHAR(150) NOT NULL,
        [DataNascimento] DATETIME2(7) NOT NULL,
        [TelefoneFixo] NVARCHAR(20) NULL,
        [TelefoneCelular] NVARCHAR(20) NOT NULL,
        [Email] NVARCHAR(150) NOT NULL,
        [Endereco] NVARCHAR(250) NOT NULL,
        [FotoUrl] NVARCHAR(MAX) NULL,
        [Senha] NVARCHAR(100) NOT NULL,
        [IsGestor] BIT NOT NULL,
        CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
    
    -- Criação de Índice Único para evitar e-mails duplicados (Trava de Regra de Negócio)
    CREATE UNIQUE NONCLUSTERED INDEX [IX_Usuarios_Email] 
        ON [dbo].[Usuarios]([Email] ASC);
END
GO

-- 3. Criação da Tabela de Tarefas
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tarefas]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Tarefas] (
        [Id] INT IDENTITY(1,1) NOT NULL,
        [MensagemDescritiva] NVARCHAR(max) NOT NULL,
        [DataLimite] DATETIME2(7) NOT NULL,
        [Status] NVARCHAR(50) NOT NULL,
        [UsuarioId] INT NOT NULL,
        CONSTRAINT [PK_Tarefas] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [FK_Tarefas_Usuarios_UsuarioId] FOREIGN KEY ([UsuarioId]) 
            REFERENCES [dbo].[Usuarios] ([Id]) ON DELETE CASCADE
    );

    CREATE NONCLUSTERED INDEX [IX_Tarefas_UsuarioId] 
        ON [dbo].[Tarefas]([UsuarioId] ASC);
END
GO

-- 4. População do Usuário Master Padrão (Seed Inicial)
IF NOT EXISTS (SELECT 1 FROM [dbo].[Usuarios] WHERE [Email] = 'ti@leveinvestimentos.com.br')
BEGIN
    INSERT INTO [dbo].[Usuarios] 
        ([NomeCompleto], [DataNascimento], [TelefoneFixo], [TelefoneCelular], [Email], [Endereco], [FotoUrl], [Senha], [IsGestor])
    VALUES 
        (N'Administrador TI Leve', '1990-01-01 00:00:00.0000000', N'(11) 2537-7777', N'(11) 99999-9999', N'ti@leveinvestimentos.com.br', N'Praça Maastricht, nº 200, Bragança Paulista - SP', NULL, N'teste123', 1);
END
GO