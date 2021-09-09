CREATE PROCEDURE [dbo].[spDeleteUserREgisteredCourse]
	@UserId int,
	@CourseId int
AS
	BEGIN
		BEGIN TRY
			DELETE FROM UserRegisteredCourse 
			WHERE UserId = @UserId 
			AND CourseId = @CourseId;
			RETURN 1;
		END TRY
		BEGIN CATCH
		END CATCH
	END
RETURN 0;