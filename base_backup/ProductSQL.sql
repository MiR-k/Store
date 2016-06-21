use [dbStore]
go

Insert into [dbo].[Products] (Name, Description, DescriptionSecond, Price, CreateDate)
VALUES
( 'Штуцер','Маз Штуцер 1.0','Дополнительное поле Коленвал', 1000.1, GETDATE()),
( 'Коленвал','Маз Коленвал 2.0','Дополнительное поле Коленвал', 2000.3, GETDATE()),
( 'Ступица','Маз Ступица 3.0','Дополнительное поле Ступица',3000.3, GETDATE());
