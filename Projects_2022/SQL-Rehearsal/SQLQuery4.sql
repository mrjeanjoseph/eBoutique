CREATE SCHEMA [Rehearsals]
GO

CREATE TABLE [Rehearsals].[Customer](
	[CustomerID] [int] NOT NULL IDENTITY(101,3) PRIMARY KEY,
	[Name] [varchar](100) NULL,
	[Address] [varchar](300) NULL,
	[Mobileno] [varchar](15) NULL,
	[Birthdate] [datetime] NULL,
	[EmailID] [varchar](300) NULL
)

SELECT * FROM [Rehearsals].[Customer]

ALTER TABLE [Rehearsals].[Customer]
ALTER COLUMN [CustomerID] IDENTITY(61,1) PRIMARY KEY,

INSERT INTO [Rehearsals].[Customer] ([NAME], [Address], [Mobileno], [Birthdate], [EmailID])
VALUES ('Moise Jean-Charles', '2005 Chemen Simo Les', '509-5002-1818', '02/02/1972', 'm.jeancharles@haiti.com')

UPDATE [Rehearsals].[Customer]
SET Mobileno = '509-4053-2202'
WHERE CustomerID = 57




SELECT * INTO [Rehearsals].[Customer_BACKUP2]
FROM [Rehearsals].[Customer]

SELECT * FROM [Rehearsals].[Customer_BACKUP2]

SELECT * FROM [Rehearsals].[Customer]

DROP TABLE [Rehearsals].[Customer]