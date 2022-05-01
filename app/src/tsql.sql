USE [master];
GO
ALTER DATABASE [CodeEditorApi] SET SINGLE_USER WITH ROLLBACK IMMEDIATE 

RESTORE DATABASE [CodeEditorApi] 
FROM DISK = N'.\CodeEditorApi.bak' WITH  FILE = 1, REPLACE, NOUNLOAD, STATS = 5;

ALTER DATABASE [CodeEditorApi] SET MULTI_USER
GO