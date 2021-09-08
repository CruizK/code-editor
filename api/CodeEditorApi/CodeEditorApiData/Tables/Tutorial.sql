CREATE TABLE [dbo].[Tutorial]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] VARCHAR(100) NULL, 
    [Author] INT NOT NULL, 
    [CourseId] INT NULL, 
    [Prompt] TEXT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [ModifyDate] NCHAR(10) NOT NULL, 
    [IsPublished] BIT NOT NULL, 
    [StudentCount] INT NOT NULL, 
    CONSTRAINT [FK_Tutorial_UserId] FOREIGN KEY ([Author]) REFERENCES [User]([Id]), 
    CONSTRAINT [FK_Tutorial_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Course]([Id])
)
