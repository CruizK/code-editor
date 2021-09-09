CREATE TABLE [dbo].[UserTutorial]
(
	[TutorialId] INT NOT NULL , 
    [UserId] NCHAR(10) NOT NULL, 
    [InProgress] BIT NOT NULL, 
    [IsCompleted] BIT NOT NULL, 
    [PercentProgress] INT NOT NULL, 
    PRIMARY KEY ([UserId], [TutorialId])
)
