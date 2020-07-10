CREATE TABLE [dbo].[OrderProducts]
(
	[Id] INT NOT NULL PRIMARY KEY identity, 
    [OrderId] INT NOT NULL, 
    [ProductId] INT NOT NULL, 
    [ProductCode] VARCHAR(50) NOT NULL, 
    [ProductDescription] VARCHAR(50) NOT NULL, 
    [ProductImagePath] VARCHAR(MAX) NOT NULL, 
    [ProductQty] INT NOT NULL, 
    [ProductPrice] NCHAR(10) NOT NULL
)
