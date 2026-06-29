CREATE TABLE TBDespesaCategoria
(
    DespesaId UNIQUEIDENTIFIER NOT NULL,

    CategoriaId UNIQUEIDENTIFIER NOT NULL,

    CONSTRAINT PK_TBDespesaCategoria
    PRIMARY KEY
    (
        DespesaId,
        CategoriaId
    ),

    CONSTRAINT FK_TBDespesaCategoria_Despesa
    FOREIGN KEY (DespesaId)
    REFERENCES TBDespesa(Id),

    CONSTRAINT FK_TBDespesaCategoria_Categoria
    FOREIGN KEY (CategoriaId)
    REFERENCES TBCategoria(Id)
);
GO