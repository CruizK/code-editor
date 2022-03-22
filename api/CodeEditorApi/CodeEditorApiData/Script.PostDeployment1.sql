/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

/* Migration 1 */

BEGIN
    BEGIN TRY
        INSERT INTO cfgRoles (Id, Role) VALUES (1, 'Admin'), (2, 'Teacher'), (3, 'Student');
        INSERT INTO cfgDifficultyLevel (Id, Difficulty) VALUES (1, 'Easy'), (2, 'Medium'), (3, 'Hard');
        INSERT INTO cfgProgrammingLanguage(Id, Language) VALUES (1, 'CSS'), (2, 'C#'), (3, 'HTML'), (4, 'Java'), (5, 'JavaScript'), (6, 'Python');
        INSERT INTO cfgTutorialStatus(Id, Status) VALUES (1, 'Not Started'), (2, 'In Progress'), (3, 'Completed'), (4, 'Restarted');
    END TRY
    BEGIN CATCH
    END CATCH
END;
GO

/* Migration 2 */

BEGIN
    BEGIN TRY
        INSERT INTO [dbo].[User](Name, Email, Hash, RoleId, IsConfirmed) VALUES (
            'Admin', 
            'admin@admin.com', 
            'Pbkdf2-10000-OWVkYzBmNzA3YWZiNzgyYzFiNDZiN2ZlYzlhNWQ3NTA4ODQ5MTJiNTUzZjhiODI0N2RkMTAxNGNlMDRlMGQ5Yw==-O94HOZo1o75Ee35YiBGYfRjad5MdrKr36ClPKtRN+R4=',
            1,
            1
        );
    END TRY
    BEGIN CATCH
    END CATCH
END;
GO

BEGIN
    BEGIN TRY
        INSERT INTO [dbo].[User](Name, Email, Hash, RoleId, IsConfirmed) VALUES (
            'Teacher', 
            'devteacher@teacher.com', 
            'Pbkdf2-10000-hOUYyxfF4fkhRjwDOn8McQ==-nN7GLmMf7964lucYG7ZOwKpEtHhzuxnIksxZQ27YWgA=',
            2,
            1
        );
        INSERT INTO [dbo].[User](Name, Email, Hash, RoleId, IsConfirmed) VALUES (
            'Student', 
            'devstudent@student.com', 
            'Pbkdf2-10000-peqYKfUKbJXx+9FnBL+92Q==-VNDl+0Wwdwb6skyOHPJjggp5tXvwabfDEaN5cULY7ZU=',
            3,
            1
        );
    END TRY
    BEGIN CATCH
    END CATCH
END;
GO

/* Migration 3 */

BEGIN
    BEGIN TRY
        INSERT INTO cfgTutorialStatus(Id, Status) VALUES (1, 'NotStarted'), (2, 'InProgress'), (3, 'Completed'), (4, 'Restarted');
    END TRY
    BEGIN CATCH
    END CATCH
END;
GO