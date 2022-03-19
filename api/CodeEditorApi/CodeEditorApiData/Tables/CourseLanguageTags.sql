CREATE TABLE [dbo].[CourseLanguageTags]
(
	[CourseId] INT NOT NULL , 
    [LanguageId] INT NOT NULL, 
    PRIMARY KEY ([CourseId], [LanguageId])
)
