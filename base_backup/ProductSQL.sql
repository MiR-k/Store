use [dbStore]
go

Insert into [dbo].[Products] (Name, Description, DescriptionSecond, Price, CreateDate)
VALUES
( '������','��� ������ 1.0','�������������� ���� ��������', 1000.1, GETDATE()),
( '��������','��� �������� 2.0','�������������� ���� ��������', 2000.3, GETDATE()),
( '�������','��� ������� 3.0','�������������� ���� �������',3000.3, GETDATE());
