CREATE TRIGGER [dbo].[DateOfCreate] 
   ON  dbStore
   After INSERT
AS
BEGIN
	INSERT dbo.Categories.CreateDate = GETDATE();
END
