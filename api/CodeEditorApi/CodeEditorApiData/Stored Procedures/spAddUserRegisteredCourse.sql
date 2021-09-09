CREATE PROCEDURE [dbo].[spAddUserRegisteredCourse]
	@UserId int,
	@CourseId int
AS
	BEGIN
		BEGIN TRY
			IF((SELECT CourseId FROM UserRegisteredCourse WHERE UserId = @UserId) = @CourseId)
				RETURN 0;

			INSERT INTO UserRegisteredCourse VALUES (@UserId, @CourseId);
			Return 1;
		END TRY
		BEGIN CATCH
		END CATCH
	END

