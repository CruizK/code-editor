CREATE TABLE [dbo].[Tutorial]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] VARCHAR(100) NOT NULL, 
    [Author] INT NOT NULL, 
    [CourseId] INT NOT NULL, 
    [Prompt] TEXT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [ModifyDate] DATETIME NOT NULL, 
    [IsPublished] BIT NOT NULL, 
    [Index] INT NULL, 
    [LanguageId] INT NULL, 
    [DifficultyId] INT NULL, 
    [Description] NVARCHAR(255) NULL, 
    [Template] TEXT NULL, 
    CONSTRAINT [FK_Tutorial_UserId] FOREIGN KEY ([Author]) REFERENCES [User]([Id]), 
    CONSTRAINT [FK_Tutorial_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Course]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_Tutorial_cfgDifficultyLevel] FOREIGN KEY ([DifficultyId]) REFERENCES [cfgDifficultyLevel]([Id]), 
    CONSTRAINT [FK_Tutorial_cfgProgrammingLanguage] FOREIGN KEY ([LanguageId]) REFERENCES [cfgProgrammingLanguage]([Id])
)
