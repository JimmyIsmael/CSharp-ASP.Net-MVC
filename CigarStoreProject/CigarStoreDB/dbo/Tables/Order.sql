CREATE TABLE [dbo].[Order]
(
	[Id] INT NOT NULL PRIMARY KEY identity, 
    [UserId] INT NOT NULL, 
    [Name] VARCHAR(250) NOT NULL, 
    [CompanyName] VARCHAR(50) NOT NULL, 
    [Address] VARCHAR(150) NOT NULL, 
    [City] VARCHAR(50) NOT NULL, 
    [State] VARCHAR(50) NOT NULL, 
    [Zip] VARCHAR(50) NOT NULL, 
    [DateCreated] DATETIME NOT NULL, 
    [Status] VARCHAR(50) NOT NULL DEFAULT 'Received', 
    [TrackingNumber] VARCHAR(MAX) NULL DEFAULT 'Not Assigned Yet'
)
