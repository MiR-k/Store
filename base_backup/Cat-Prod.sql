USE [dbStore]
GO
DECLARE
	@shet INT = 1,
	@countes int = 0;
SET @countes = (select count(Id) from [dbo].[Categories])

WHILE (select Id from [dbo].[Categories] Where id = @shet) <= 2*@countes
BEGIN
	UPDATE dbo.Products
	SET Category_Id = @shet WHERE Id = @shet;
	SET @shet = @shet + 1;
END