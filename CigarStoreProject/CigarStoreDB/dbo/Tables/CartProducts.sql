CREATE TABLE [dbo].[CartProducts]
(
	[Id] INT NOT NULL PRIMARY KEY identity, 
    [CartId] INT NOT NULL, 
    [ProductId] INT NOT NULL, 
    [ProductQty] INT NOT NULL, 
    [AddedDate] DATE NOT NULL, 
    [ProductPrice] FLOAT NOT NULL
)
