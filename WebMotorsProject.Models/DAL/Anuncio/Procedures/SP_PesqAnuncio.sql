/* Nome da Procedure = SP_PesqAnuncio */

If Object_ID('SP_PesqAnuncio') Is Null
    Exec sp_executesql N'Create Procedure SP_PesqAnuncio As Set Nocount On';
Go

Alter Procedure SP_PesqAnuncio(@Marca Varchar(45), @Modelo Varchar(45), @Versao Varchar(45), @Ano Int)
AS
BEGIN
	
	Declare @SqlCommand Varchar(Max)

	Create Table #Aux (
		Id Int, 
		Marca Varchar(45), 
		Modelo Varchar(45),  
		Versao Varchar(45), 
		Ano Int, 
		Quilometragem Int, 
		Observacao Text
	)

	Set NoCount On

	Set @SqlCommand = '
		Insert Into #Aux
		Select 
			Id, Marca, Modelo, Versao, Ano, Quilometragem, Observacao
		From tb_AnuncioWebmotors
		Where Id > 0'
	
	if (@Marca <> '')
		Begin
			Set @SqlCommand = @SqlCommand + ' And Marca Like ''%' + @Marca + '%'''
		End

	if (@Modelo <> '')
		Begin
			Set @SqlCommand = @SqlCommand + ' And Modelo Like ''%' + @Modelo + '%'''
		End

	if (@Versao <> '')
		Begin
			Set @SqlCommand = @SqlCommand + ' And Versao Like ''%' + @Versao + '%'''
		End

	if (@Ano <> '')
		Begin
			Set @SqlCommand = @SqlCommand + ' And Ano = ' + @Ano
		End

	Set @SqlCommand = @SqlCommand + ' Order By Marca, Modelo, Versao'
	Exec (@SqlCommand)

	Select 
		Id, Marca, Modelo, Versao, Ano, Quilometragem, Observacao
	From #Aux

End