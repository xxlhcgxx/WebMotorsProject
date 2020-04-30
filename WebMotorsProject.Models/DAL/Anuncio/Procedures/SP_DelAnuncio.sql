/* Nome da Procedure = SP_DelAnuncio */

If Object_ID('SP_DelAnuncio') Is Null
    Exec sp_executesql N'Create Procedure SP_DelAnuncio As Set Nocount On';
Go

Alter Procedure SP_DelAnuncio
	@Id Int
AS
BEGIN
	DELETE tb_AnuncioWebmotors WHERE Id = @Id
END