CREATE TABLE [dbo].[UserRegisteredCourse]
(
	[CourseId] INT NOT NULL , 
    [UserId] NCHAR(10) NOT NULL, 
    CONSTRAINT [PK_UserRegisteredCourse] PRIMARY KEY ([CourseId], [UserId]) 
)
