CREATE TABLE [dbo].[CourseDifficultyTags]
(
	[CourseId] INT NOT NULL , 
    [DifficultyId] INT NOT NULL, 
    PRIMARY KEY ([CourseId], [DifficultyId]) 
)
