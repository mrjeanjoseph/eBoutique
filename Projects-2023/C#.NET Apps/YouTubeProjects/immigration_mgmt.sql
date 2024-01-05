
CREATE DATABASE ImmigrationManagementDB
GO
USE ImmigrationManagementDB
GO
CREATE TABLE Country
(
	countryId INT IDENTITY PRIMARY KEY,
	countryName NVARCHAR(30) NOT NULL
)
GO

CREATE TABLE Visa
(
	visaId INT IDENTITY PRIMARY KEY,
	visaType NVARCHAR(40) NOT NULL,
	visaNumber NVARCHAR(40) UNIQUE NOT NULL,
	issueDate DATETIME NOT NULL,
	expireDate DATETIME NOT NULL
)
GO

CREATE TABLE Passport
(
	passportID INT IDENTITY PRIMARY KEY,
	passportNumber NVARCHAR (40) UNIQUE NOT NULL,
	issueDate DATETIME NOT NULL,
	expireDate DATETIME NOT NULL,
	visaId INT REFERENCES Visa(visaId)

)
DROP TABLE IF EXISTS Immigrant
GO
CREATE TABLE Immigrant
(
	immigrantId INT IDENTITY PRIMARY KEY,
	immigrantName NVARCHAR(40) NOT NULL,
	dateOfBirth DATETIME NOT NULL,
	gender NVARCHAR(15) NOT NULL,
	passportId INT REFERENCES Passport(passportID),
	nidNumber NVARCHAR(15) UNIQUE NOT NULL,
	countryId INT REFERENCES Country(countryId),
	email NVARCHAR(30) NOT NULL,
	phone NVARCHAR (15) NOT NULL,
	maritalStatus BIT NULL,
	image VARBINARY(max) NULL,
	imagePath NVARCHAR(300) NULL
)
GO
SELECT * FROM Immigrant 


DROP TABLE Immigrant
GO

CREATE TABLE Transports
(
	transportId INT IDENTITY PRIMARY KEY,
	transportType NVARCHAR(30) NOT NULL,
	
)
GO
INSERT INTO  Transports VALUES('Airplanes');
INSERT INTO  Transports VALUES('Trains');
INSERT INTO  Transports VALUES('Bus');
INSERT INTO  Transports VALUES('Ships');
Go
CREATE TABLE TransportsCompany
(
	companyId INT IDENTITY PRIMARY KEY,
	companyName NVARCHAR(30) NOT NULL,
	
)
GO
CREATE TABLE TransportsDetails
(
	transportId INT REFERENCES Transports(transportId),
	companyId INT REFERENCES TransportsCompany(companyId),
	TransportNumber NVARCHAR(30) NOT NULL,
	PRIMARY KEY (transportId,companyId)
)

DROP TABLE TransportsDetails
GO
select * from TransportsCompany
go



CREATE TABLE ImmigrationOfficer
(
	immigrationOfficerId INT IDENTITY PRIMARY KEY,
	officerName NVARCHAR(40) NOT NULL,
	dateOfBirth DATETIME NOT NULL,
	gender NVARCHAR(15) NOT NULL,
	nidNumber NVARCHAR(15) UNIQUE NOT NULL,
	email NVARCHAR(30) NOT NULL,
	phone NVARCHAR (15) NOT NULL,
	address NVARCHAR (40) NOT NULL,
	maritalStatus BIT NULL,
	joinDate DATETIME NOT NULL,
	Designation NVARCHAR(40) NOT NULL,
	image VARBINARY(max) NULL,
	imagePath NVARCHAR(300) NULL
)
GO
ALTER TABLE ImmigrationOfficer 
ADD officerName NVARCHAR(40)
GO
ALTER TABLE ImmigrationOfficer 
ADD Designation NVARCHAR(40)
GO
select * from ImmigrationOfficer
go

drop table ImmigrationOfficer
go

CREATE TABLE ImmigrationOffice
(
	immigrationOfficeId INT IDENTITY PRIMARY KEY,
	officeName  NVARCHAR(40) NOT NULL,
	immigrationOfficerId INT REFERENCES ImmigrationOfficer(immigrationOfficerId)
)
GO

drop table ImmigrationOffice
go

CREATE TABLE ImmigrationDetails
(
	immigrationSerialId INT IDENTITY PRIMARY KEY,
	immigrantId INT REFERENCES Immigrant(immigrantId),
	passportId INT REFERENCES Passport(passportID),
	visaId INT REFERENCES Visa(visaId),
	transportID INT REFERENCES Transports (transportId),
	transportNumID INT NOT NULL,
	immigrationOfficerId INT REFERENCES ImmigrationOfficer(immigrationOfficerId),
	immigrationOffice NVARCHAR (50),
	departureDate DATETIME NOT NULL,
	arrivalDate DATETIME NOT NULL,
	departurePlace NVARCHAR (40) NOT NULL,
	arrivalplace NVARCHAR (40) NOT NULL
)
GO
select * from ImmigrationDetails
go
DROP TABLE ImmigrationDetails
GO


--//if needed to add foriegn key of passport table and visa Table to show passport number and visa type
CREATE TABLE Softuser
(
	id	INT IDENTITY PRIMARY KEY,
	userName NVARCHAR(15) NOT NULL,
	password INT NOT NULL,
	immigrationOfficerId INT REFERENCES ImmigrationOfficer(immigrationOfficerId)
)
GO
DROP TABLE Softuser
GO

 SELECT "Country"."countryName", "Immigrant"."immigrantName", "Immigrant"."gender", "Immigrant"."phone", "ImmigrationDetails"."departureDate", "ImmigrationDetails"."arrivalDate", "Passport"."passportNumber", "Immigrant"."image"
 FROM   (("ImmigrationManagementDB"."dbo"."ImmigrationDetails" "ImmigrationDetails" INNER JOIN "ImmigarationManagementDB"."dbo"."Immigrant" "Immigrant" ON "ImmigrationDetails"."immigrantId"="Immigrant"."immigrantId") INNER JOIN "ImmigarationManagementDB"."dbo"."Passport" "Passport" ON ("ImmigrationDetails"."passportId"="Passport"."passportID") AND ("Immigrant"."passportId"="Passport"."passportID")) INNER JOIN "ImmigarationManagementDB"."dbo"."Country" "Country" ON "Immigrant"."countryId"="Country"."countryId"
 ORDER BY "Country"."countryName"