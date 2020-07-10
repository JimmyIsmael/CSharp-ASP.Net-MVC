CREATE TABLE [dbo].[Products]
(
	[Id] INT NOT NULL PRIMARY KEY identity, 
    [ProductCode] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(100) NOT NULL, 
    [UnitOfMeasure] VARCHAR(50) NOT NULL, 
    [Category] VARCHAR(50) NOT NULL, 
    [Price] FLOAT NOT NULL, 
    [ImageTitle] VARCHAR(50) NULL, 
    [IsActive] BIT NOT NULL, 
    [ImagePath] VARCHAR(MAX) NOT NULL 
)
