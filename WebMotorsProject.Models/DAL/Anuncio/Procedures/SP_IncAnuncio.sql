/* Nome da Procedure = SP_IncAnuncio */

If Object_ID('SP_IncAnuncio') Is Null
    Exec sp_executesql N'Create Procedure SP_IncAnuncio As Set Nocount On';
Go

Alter Procedure SP_IncAnuncio
    @Marca	        Varchar (45),
    @Modelo	        Varchar (45),
    @Versao	        Varchar (45),
    @Ano	        Int,
    @Quilometragem  Int,
    @Observacao	    Text
AS
BEGIN
	INSERT INTO tb_AnuncioWebmotors (Marca, Modelo, Versao, Ano, Quilometragem, Observacao) 
	VALUES (@Marca, @Modelo, @Versao, @Ano, @Quilometragem, @Observacao)

	SELECT SCOPE_IDENTITY()
END


