CREATE TABLE [dbo].[UserRegisteredCourse]
(
	[CourseId] INT NOT NULL , 
    [UserId] INT NOT NULL, 
    CONSTRAINT [PK_UserRegisteredCourse] PRIMARY KEY ([CourseId], [UserId]), 
    CONSTRAINT [FK_UserRegisteredCourse_Course] FOREIGN KEY ([CourseId]) REFERENCES [Course]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRegisteredCourse_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]) ON DELETE CASCADE
)
