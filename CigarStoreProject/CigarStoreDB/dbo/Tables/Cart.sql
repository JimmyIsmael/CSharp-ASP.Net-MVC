CREATE TABLE [dbo].[Cart]
(
	[Id] INT NOT NULL PRIMARY KEY identity, 
    [UserId] INT NOT NULL, 
    [IsOpen] BIT NOT NULL, 
    [DateCreated] DATETIME NOT NULL
)
