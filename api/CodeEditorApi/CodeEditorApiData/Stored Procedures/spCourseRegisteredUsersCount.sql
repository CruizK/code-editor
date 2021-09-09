CREATE PROCEDURE [dbo].[spCourseRegisteredUsersCount]
	@CourseId int
AS
	BEGIN TRY
		SELECT COUNT(UserId)
		FROM UserRegisteredCourse
		WHERE CourseId = @CourseId;
	RETURN
	END TRY
	BEGIN CATCH
	END CATCH
RETURN 0
