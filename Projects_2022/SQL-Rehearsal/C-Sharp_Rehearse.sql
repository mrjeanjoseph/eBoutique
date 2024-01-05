use [SampleDB]
GO

SELECT @@VERSION



CREATE SCHEMA SCHEMA_TESTING1

CREATE TABLE TEST_TABLE (IDNO  SMALLINT NOT NULL,
            SNAME    VARCHAR(40),
            CLASS INTEGER)

GRANT ALL ON TEST_TABLE TO owner
	

CREATE TABLE [SCHEMA_TESTING1].[employee](
	[ID] int IDENTITY(103,7) NOT NULL,
	[Employee_Code] VARCHAR(9) NOT NULL,
	[Employee_FirstName] VARCHAR(30) NOT NULL,
	[Employee_LastName] VARCHAR(30) NOT NULL,
	[LocationID] INT,
	PRIMARY KEY (ID),

)
ALTER TABLE [SCHEMA_TESTING1].[employee] 
ADD FOREIGN KEY (LocationID) REFERENCES [SCHEMA_TESTING1].[Location](ID)

INSERT INTO [SCHEMA_TESTING1].[employee] VALUES ('EMP010','Wilio','Flamant','22370')
SELECT * FROM [SCHEMA_TESTING1].[employee]

CREATE TABLE [SCHEMA_TESTING1].[Location](
	[ID] INT NOT NULL PRIMARY KEY,
	[LocationCode] VARCHAR(5) NOT NULL,
	[LocationDesc] VARCHAR(30) NOT NULL
)

INSERT INTO [SCHEMA_TESTING1].[Location] VALUES ('22370','Loc82','La Gonave, Haiti')
SELECT * FROM [SCHEMA_TESTING1].[Location]


--
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [SCHEMA_TESTING1].[uspEmployeeInfo]
   @employeeID int
   AS
BEGIN
	SELECT e.id,e.employee_code,e.Employee_FirstName,e.Employee_LastName,l.LocationCode,l.LocationDesc 
	FROM [SCHEMA_TESTING1].[employee] e 
	INNER JOIN [SCHEMA_TESTING1].[Location] l on e.locationID=l.id
	WHERE e.id=@employeeID
	END
GO

EXEC [SCHEMA_TESTING1].[uspEmployeeInfo];


--Create Employee Location
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [SCHEMA_TESTING1].[uspUpdateEmployeeLocation]
   @employeeID int,
   @locationID int
AS
BEGIN
   UPDATE employee SET locationID=@locationID WHERE id=@employeeID;
END
GO


--Verify the owner of the db
SELECT suser_sname(owner_sid)
FROM sys.databases
WHERE name = 'SampleDB'

--Parameterless SP
CREATE PROCEDURE GetAllPersonalDetails
	AS
	BEGIN
	SET NOCOUNT ON;

	SELECT * FROM SCHEMA_TESTING1.employee
END;

EXEC GetAllPersonalDetails