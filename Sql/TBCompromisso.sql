CREATE TABLE TBCompromisso
(
    Id UNIQUEIDENTIFIER PRIMARY KEY,

    Assunto NVARCHAR(100) NOT NULL,

    DataOcorrencia DATE NOT NULL,

    HoraInicio TIME NOT NULL,

    HoraTermino TIME NOT NULL,

    Tipo INT NOT NULL,

    Local NVARCHAR(200) NULL,

    Link NVARCHAR(300) NULL,

    ContatoId UNIQUEIDENTIFIER NULL,

    CONSTRAINT FK_TBCompromisso_TBContato
        FOREIGN KEY (ContatoId)
        REFERENCES TBContato(Id)
);