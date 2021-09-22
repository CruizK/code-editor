CREATE PROCEDURE [dbo].[spUserRegisteredCourse]
	@UserId int
AS
	BEGIN
		BEGIN TRY
			SELECT UserId
			FROM UserRegisteredCourse
			WHERE UserId = @UserId
		END TRY
		BEGIN CATCH
		END CATCH
	END
RETURN 0
