use [dbStore]
go

Insert into [dbo].[Categories] (Name, CreateDate)
VALUES
( 'Запасные части', GETDATE()),
( 'Садово-хзяйственный инвентарь', GETDATE()),
( 'Коммунальная техника',GETDATE());
