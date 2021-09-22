CREATE TABLE [dbo].[Course]
(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[Title] VARCHAR(100) NOT NULL, 
    [Author] INT NOT NULL, 
    [Description] VARCHAR(250) NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [ModifyDate] DATETIME NOT NULL, 
    [IsPublished] BIT NOT NULL, 
    CONSTRAINT [FK_Course_UserId] FOREIGN KEY ([Author]) REFERENCES [User]([Id])
)
