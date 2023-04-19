USE master;  
GO  
CREATE DATABASE PasswordManagerDB  
ON   
( NAME = PasswordManager_dat,  
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\PasswordManagerDB.mdf',  
    SIZE = 10,  
    MAXSIZE = 50,  
    FILEGROWTH = 5 )  
LOG ON  
( NAME = PasswordManager_log,  
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\PasswordManagerDB.ldf',  
    SIZE = 5MB,  
    MAXSIZE = 25MB,  
    FILEGROWTH = 5MB ) ;  
GO
USE PasswordManagerDB;
GO
CREATE TABLE Users 
( 
  [userID] INT IDENTITY(1,1) NOT NULL,
  [username] VARCHAR (100) NOT NULL,
  [password] VARCHAR (100) NOT NULL,
  CONSTRAINT PK_Users PRIMARY KEY ([userID] ASC),

) ;  
GO 
CREATE TABLE Entries 
( 
  [entryID] INT IDENTITY(1,1) NOT NULL,
  [userID] INT NOT NULL,
  [service] VARCHAR (100) NOT NULL,
  [username] VARCHAR (100) NOT NULL,
  [password] VARCHAR (100) NOT NULL,
  CONSTRAINT PK_Entries PRIMARY KEY ([entryID] ASC),
  CONSTRAINT FK_Entries_userID FOREIGN KEY (userID) REFERENCES Users ([userID])
) ;  
GO 