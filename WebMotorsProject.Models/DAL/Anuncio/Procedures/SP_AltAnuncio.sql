/* Nome da Procedure = SP_AltAnuncio */

If Object_ID('SP_AltAnuncio') Is Null
    Exec sp_executesql N'Create Procedure SP_AltAnuncio As Set Nocount On';
Go

Alter Procedure SP_AltAnuncio
    @Marca			Varchar (45),
    @Modelo			Varchar (45),
	@Versao			Varchar (45),
	@Ano			Varchar (45),
	@Quilometragem	Int,
	@Observacao		Text,
	@Id				Int
AS
BEGIN
	UPDATE tb_AnuncioWebmotors 
	SET 
		Marca = @Marca, 
		Modelo = @Modelo, 
		Versao = @Versao,
		Ano = @Ano,
		Quilometragem = @Quilometragem,
		Observacao = @Observacao
	WHERE Id = @Id
END
