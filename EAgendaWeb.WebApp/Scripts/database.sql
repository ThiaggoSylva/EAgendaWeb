-- ==========================================
-- EAgenda Database
-- ==========================================

-- CONTATOS
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TBContato' AND xtype='U')
BEGIN
    CREATE TABLE TBContato
    (
        Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,

        Nome VARCHAR(100) NOT NULL,
        Email VARCHAR(150) NOT NULL,
        Telefone VARCHAR(20) NOT NULL,

        Cargo VARCHAR(100) NULL,
        Empresa VARCHAR(100) NULL
    );

    CREATE UNIQUE INDEX IX_TBContato_Email
        ON TBContato(Email);

    CREATE UNIQUE INDEX IX_TBContato_Telefone
        ON TBContato(Telefone);
END
GO

-- COMPROMISSOS
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TBCompromisso' AND xtype='U')
BEGIN
    CREATE TABLE TBCompromisso
    (
        Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,

        Assunto VARCHAR(100) NOT NULL,

        DataOcorrencia DATE NOT NULL,

        HoraInicio TIME NOT NULL,
        HoraTermino TIME NOT NULL,

        Tipo INT NOT NULL,

        Local VARCHAR(200) NULL,
        Link VARCHAR(500) NULL,

        ContatoId UNIQUEIDENTIFIER NULL,

        CONSTRAINT FK_TBCompromisso_TBContato
            FOREIGN KEY (ContatoId)
            REFERENCES TBContato(Id)
    );
END
GO

-- CATEGORIAS
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TBCategoria' AND xtype='U')
BEGIN
    CREATE TABLE TBCategoria
    (
        Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,

        Titulo VARCHAR(100) NOT NULL
    );

    CREATE UNIQUE INDEX IX_TBCategoria_Titulo
        ON TBCategoria(Titulo);
END
GO

-- DESPESAS
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TBDespesa' AND xtype='U')
BEGIN
    CREATE TABLE TBDespesa
    (
        Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,

        Descricao VARCHAR(100) NOT NULL,

        DataOcorrencia DATE NOT NULL,

        Valor DECIMAL(18,2) NOT NULL,

        FormaPagamento INT NOT NULL
    );
END
GO

-- RELACIONAMENTO N:N
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TBDespesaCategoria' AND xtype='U')
BEGIN
    CREATE TABLE TBDespesaCategoria
    (
        DespesaId UNIQUEIDENTIFIER NOT NULL,
        CategoriaId UNIQUEIDENTIFIER NOT NULL,

        CONSTRAINT PK_TBDespesaCategoria
            PRIMARY KEY (DespesaId, CategoriaId),

        CONSTRAINT FK_TBDespesaCategoria_TBDespesa
            FOREIGN KEY (DespesaId)
            REFERENCES TBDespesa(Id),

        CONSTRAINT FK_TBDespesaCategoria_TBCategoria
            FOREIGN KEY (CategoriaId)
            REFERENCES TBCategoria(Id)
    );
END
GO

-- TAREFAS
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TBTarefa' AND xtype='U')
BEGIN
    CREATE TABLE TBTarefa
    (
        Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,

        Titulo VARCHAR(100) NOT NULL,

        Prioridade INT NOT NULL,

        DataCriacao DATETIME NOT NULL,

        DataConclusao DATETIME NULL,

        PercentualConcluido INT NOT NULL,

        Concluida BIT NOT NULL
    );
END
GO

-- ITENS DE TAREFA
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TBItemTarefa' AND xtype='U')
BEGIN
    CREATE TABLE TBItemTarefa
    (
        Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,

        Titulo VARCHAR(100) NOT NULL,

        Concluido BIT NOT NULL,

        TarefaId UNIQUEIDENTIFIER NOT NULL,

        CONSTRAINT FK_TBItemTarefa_TBTarefa
            FOREIGN KEY (TarefaId)
            REFERENCES TBTarefa(Id)
            ON DELETE CASCADE
    );
END
GO