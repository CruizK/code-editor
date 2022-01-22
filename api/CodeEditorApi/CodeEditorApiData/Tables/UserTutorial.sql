CREATE TABLE [dbo].[UserTutorial]
(
	[TutorialId] INT NOT NULL , 
    [UserId] INT NOT NULL, 
    [InProgress] BIT NOT NULL, 
    [IsCompleted] BIT NOT NULL, 
    [ModifyDate] DATETIME NULL, 
    PRIMARY KEY ([UserId], [TutorialId]), 
    CONSTRAINT [FK_UserTutorial_Tutorial] FOREIGN KEY ([TutorialId]) REFERENCES [Tutorial]([Id]), 
    CONSTRAINT [FK_UserTutorial_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id])
)
