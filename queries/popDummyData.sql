USE PasswordManagerDB;

INSERT INTO [Users] (
	[username],
	[password]
)
VALUES ('admin', 'password'),
		('test', '123Test')
GO

INSERT INTO [Entries] (
	[userID],
	[service],
	[username],
	[password]
)
VALUES (1, 'YouTube', 'admin@gmail.com', 'password123'),
		(1, 'GitHub', 'adminGithub@gmail.com', 'gitHubPassword123'),
		(2, 'YouTube', 'test@gmail.com', '123TestYT')