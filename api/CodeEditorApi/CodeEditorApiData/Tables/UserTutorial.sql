CREATE TABLE [dbo].[UserTutorial]
(
	[TutorialId] INT NOT NULL , 
    [UserId] INT NOT NULL, 
    [Status] INT NOT NULL DEFAULT 1,
    [ModifyDate] DATETIME NULL, 
    [UserCode] TEXT NULL, 
    PRIMARY KEY ([UserId], [TutorialId]), 
    CONSTRAINT [FK_UserTutorial_Tutorial] FOREIGN KEY ([TutorialId]) REFERENCES [Tutorial]([Id]), 
    CONSTRAINT [FK_UserTutorial_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]),
    CONSTRAINT [FK_UserTutorial_cfgTutorialStatus] FOREIGN KEY ([Status]) REFERENCES [cfgTutorialStatus]([Id])
)
