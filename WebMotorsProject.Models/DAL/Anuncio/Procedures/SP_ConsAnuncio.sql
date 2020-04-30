/* Nome da Procedure = SP_ConsAnuncio */

If Object_ID('SP_ConsAnuncio') Is Null
    Exec sp_executesql N'Create Procedure SP_ConsAnuncio As Set Nocount On';
Go

Alter Procedure SP_ConsAnuncio
	@Id Int
AS
BEGIN
	IF(ISNULL(@Id,0) = 0)
		SELECT 
			Id, Marca, Modelo, Versao, Ano, Quilometragem, Observacao
		FROM tb_AnuncioWebmotors WITH(NOLOCK)
	ELSE
		SELECT 
			Id, Marca, Modelo, Versao, Ano, Quilometragem, Observacao
		FROM tb_AnuncioWebmotors WITH(NOLOCK)
		Where Id = @Id
		
END	