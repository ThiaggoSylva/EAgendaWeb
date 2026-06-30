CREATE TABLE TBTarefa
(
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,

    Titulo NVARCHAR(100) NOT NULL,

    Prioridade INT NOT NULL,

    DataCriacao DATETIME2 NOT NULL,

    DataConclusao DATETIME2 NULL,

    Concluida BIT NOT NULL,

    PercentualConcluido INT NOT NULL
);
GO