﻿CREATE TABLE [dbo].[User]
(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[Name] VARCHAR(100) NOT NULL, 
	[Email] VARCHAR(255) NOT NULL,
	[Hash] VARCHAR(255),
	[AccessToken] VARCHAR(255),
	[RoleId] INT NOT NULL, 
	CONSTRAINT [FK_User_cfgRoles] FOREIGN KEY ([RoleId]) REFERENCES [cfgRoles]([Id]), 
    CONSTRAINT [AK_Email] UNIQUE ([Email])
)
