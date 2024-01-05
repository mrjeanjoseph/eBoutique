USE ABCCompany

BEGIN --Basic Template
	-- Adding a new column
	ALTER TABLE [Sales].[SalesPerson]
	ADD [LastSalesDate] date NULL;

	-- We don't have any error handling
	-- Create or alter is new in SQL 2016 SP1
	CREATE OR ALTER PROCEDURE [Sales].[Insert_SalesOrder]
		@SalesPerson int,
		@SalesAmount decimal(36,2),
		@SalesDate date,
		@SalesTerritory int,
		@OrderDescription nvarchar(max)
	AS
	BEGIN
		SET NOCOUNT ON;
			INSERT INTO Sales.SalesOrder (SalesPerson,SalesAmount,SalesDate,SalesTerritory,OrderDescription)
				VALUES (@SalesPerson,@SalesAmount,@SalesDate,@SalesTerritory,@OrderDescription);
			UPDATE Sales.SalesPerson 
			SET LastSalesDate = GETDATE() WHERE Id = @SalesPerson;	
	END

	-- This will fail
	EXECUTE Sales.Insert_SalesOrder @SalesPerson = 1, @SalesAmount = 7500, @SalesDate = '6/1/2019', @SalesTerritory = 88, @OrderDescription = 'First sale of the month. Ship ASAP!';

	-- Did the last sales date get updated
	SELECT * 
	FROM Sales.SalesPerson WHERE Id = 1;

	-- Let's clean up
	UPDATE Sales.SalesPerson
	SET LastSalesDate = NULL WHERE Id = 1;

	-- Here is a basic template with error handling
	CREATE OR ALTER PROCEDURE [Sales].[Insert_SalesOrder]
		@SalesPerson int,
		@SalesAmount decimal(36,2),
		@SalesDate date,
		@SalesTerritory int,
		@OrderDescription nvarchar(max)
	AS
	BEGIN
		BEGIN TRY	
			SET NOCOUNT ON;
			SET XACT_ABORT ON;
			BEGIN TRANSACTION;
				INSERT INTO Sales.SalesOrder (SalesPerson,SalesAmount,SalesDate,SalesTerritory,OrderDescription)
					VALUES (@SalesPerson,@SalesAmount,@SalesDate,@SalesTerritory,@OrderDescription);
				UPDATE Sales.SalesPerson 
				SET LastSalesDate = GETDATE() WHERE Id = @SalesPerson;	
			COMMIT TRANSACTION;
		END TRY
		BEGIN CATCH
			IF (@@TRANCOUNT > 0)
				ROLLBACK TRANSACTION;
				THROW;
		END CATCH
	END

	-- This will fail
	EXECUTE Sales.Insert_SalesOrder @SalesPerson = 1, @SalesAmount = 7500, @SalesDate = '6/1/2019', @SalesTerritory = 88, @OrderDescription = 'First sale of the month. Ship ASAP!';

	-- The last sales date should be null
	SELECT * 
	FROM Sales.SalesPerson WHERE Id = 1;
END

BEGIN --RowCount MissMatch

	-- Adding our business key
	ALTER TABLE [Sales].[SalesPerson] 
	ADD [EmployeeNumber] nvarchar(10) NULL;

	-- Setting the business key
	UPDATE Sales.SalesPerson
	SET EmployeeNumber = 
		CASE
			WHEN Id = 1 THEN '0001'
			WHEN Id = 2 THEN '0002'
			WHEN Id = 3 THEN '0003'
			WHEN Id = 4 THEN '0004'
			WHEN Id = 5 THEN '0005'
			WHEN Id = 6 THEN '0006'
			WHEN Id = 7 THEN '0007'
			WHEN Id = 8 THEN '0008'
			WHEN Id = 9 THEN '0009'
			WHEN Id = 10 THEN '0010'
			WHEN Id = 11 THEN '0010'
			WHEN Id = 12 THEN '0012'
			END;

	-- Procedure for setting an employee to inactive
	CREATE OR ALTER PROCEDURE [Sales].[Update_SalesPerson_Inactive]
		@EmployeeNumber nvarchar(10)
	AS
	BEGIN
		BEGIN TRY	
			SET NOCOUNT ON;
			SET XACT_ABORT ON;
			DECLARE @RowCount int;		
			BEGIN TRANSACTION;
				UPDATE Sales.SalesPerson 
				SET IsActive = 0 WHERE EmployeeNumber = @EmployeeNumber;	
				SET @RowCount = @@ROWCOUNT;		
				IF (@RowCount > 1)
					BEGIN
						;THROW 65002,'Trying to update more than one row',1;
					END		
			COMMIT TRANSACTION;
		END TRY
		BEGIN CATCH
			IF (@@TRANCOUNT > 0)
				ROLLBACK TRANSACTION;
				THROW;
		END CATCH
	END

	-- Let's try setting a sales person to inactive
	EXECUTE Sales.Update_SalesPerson_Inactive @EmployeeNumber = '0010';
END

BEGIN --Actionable Message
	SELECT * FROM
	Sales.SalesPerson;

	-- Add a check constraint on sales person start date
	ALTER TABLE Sales.SalesPerson WITH CHECK
		ADD CONSTRAINT CK_SalesPerson_StartDate CHECK(StartDate <= GETDATE());

	-- Not a clear error message
	BEGIN TRY
		BEGIN TRANSACTION;
			SET IDENTITY_INSERT Sales.Salesperson ON;	
			INSERT INTO Sales.SalesPerson (Id,FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
				VALUES	(13,'Elijah','Jean Joseph',750,1,2,'Tony.Stark@ABCCorp.com','1/1/2024');
			SET IDENTITY_INSERT Sales.Salesperson OFF;
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH	
		IF (@@TRANCOUNT > 0)
			ROLLBACK TRANSACTION;	
			THROW;
	END CATCH

	-- This message will make more sense to the end user
	BEGIN TRY	
		DECLARE @CurrentDate date = GETDATE();
		BEGIN TRANSACTION;
			SET IDENTITY_INSERT Sales.Salesperson ON;	
			INSERT INTO Sales.SalesPerson (Id,FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
				VALUES	(14,'Elijah','Jean Joseph',750,1,2,'Tony.Stark@ABCCorp.com','1/1/2024');
			SET IDENTITY_INSERT Sales.Salesperson OFF;
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		-- Can't we just use the error number
		IF (ERROR_MESSAGE() = 'The INSERT statement conflicted with the CHECK constraint "CK_SalesPerson_StartDate". The conflict occurred in database "ABCCompany", table "Sales.SalesPerson", column ''StartDate''.' AND @@TRANCOUNT > 0)
			BEGIN		
				DECLARE @Message nvarchar(500);
				SET @Message = CONCAT('Please enter a start date before ',@CurrentDate);			
				ROLLBACK TRANSACTION;			
				THROW 65000,@Message,1;
			END
		IF (@@TRANCOUNT > 0)
			ROLLBACK TRANSACTION;	
			THROW;
	END CATCH

	-- Using XACT_ABORT to rollback
	BEGIN TRY	
		SET XACT_ABORT ON;	
		DECLARE @CurrentDate date = GETDATE();
		BEGIN TRANSACTION;
			SET IDENTITY_INSERT Sales.Salesperson ON;	
			INSERT INTO Sales.SalesPerson (Id,FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
				VALUES	(14,'Elijah','Jean Joseph',750,1,2,'Tony.Stark@ABCCorp.com','1/1/2024');
			SET IDENTITY_INSERT Sales.Salesperson OFF;
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		IF (ERROR_MESSAGE() = 'The INSERT statement conflicted with the CHECK constraint "CK_SalesPerson_StartDate". The conflict occurred in database "ABCCompany", table "Sales.SalesPerson", column ''StartDate''.' AND @@TRANCOUNT > 0)
			BEGIN		
				DECLARE @Message nvarchar(500);
				SET @Message = CONCAT('Please enter a start date before ',@CurrentDate);
				THROW 65000,@Message,1;
			END
		IF (@@TRANCOUNT > 0)
			ROLLBACK TRANSACTION;	
			THROW;
	END CATCH

	--Delete id 14 if we happen to have added a new record
	DELETE FROM Sales.SalesPerson WHERE Id = 13

	-- Did our new sales person get inserted
	SELECT * FROM
	Sales.SalesPerson;
END

BEGIN --Enforce Business Logic

	-- Add a new column and populate
	ALTER TABLE [Sales].[SalesPerson] 
	ADD [IsActive] bit NOT NULL
	DEFAULT 1
	WITH VALUES;

	-- Let's set one of our sales people to inactive
	UPDATE Sales.SalesPerson SET [IsActive] = 0 
	WHERE Id = 2;

	-- We don't want sales orders added with inative sales people
	CREATE OR ALTER PROCEDURE [Sales].[Insert_SalesOrder]
		@SalesPerson int,
		@SalesAmount decimal(36,2),
		@SalesDate date,
		@SalesTerritory int,
		@OrderDescription nvarchar(max)
	AS
	BEGIN TRY	
		SET NOCOUNT ON;
		SET XACT_ABORT ON;
		BEGIN TRANSACTION;
			IF EXISTS (SELECT 1 FROM Sales.SalesPerson WHERE IsActive = 0 and Id = @SalesPerson)
				BEGIN
					;THROW 65001, 'Please select an active sales person',1;
				END
				ELSE
			INSERT INTO Sales.SalesOrder (SalesPerson,SalesAmount,SalesDate,SalesTerritory,OrderDescription)
				VALUES (@SalesPerson,@SalesAmount,@SalesDate,@SalesTerritory,@OrderDescription);	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		IF (@@TRANCOUNT > 0)
			ROLLBACK TRANSACTION;
			THROW;
	END CATCH

	-- We are using an inactive sales person here
	EXECUTE Sales.Insert_SalesOrder @SalesPerson = 2, @SalesAmount = 7500, @SalesDate = '6/1/2019', @SalesTerritory = 2, @OrderDescription = 'An older order Sally made';

	-- Did that sales order get added
	SELECT * 
	FROM Sales.SalesOrder;
END

BEGIN --Error Tables

	-- Drop if the error log table already exists
	DROP TABLE IF EXISTS [dbo].[ErrorLog];

	-- This will create our error log table
	CREATE TABLE [dbo].[ErrorLog] (
		[Id] int identity(1,1) NOT NULL,
		[MessageId] int NOT NULL,
		[MessageText] nvarchar(2047) NULL,
		[SeverityLevel] int NOT NULL,
		[State] int NOT NULL,
		[LineNumber] int NOT NULL,
		[ProcedureName] nvarchar(2500) NULL,
		[CreateDate] datetime NOT NULL DEFAULT GETDATE(),
		CONSTRAINT [PK_ErrorLogId] PRIMARY KEY CLUSTERED ([Id]));

	-- Logging a message to the error log table
	BEGIN TRY
		SET XACT_ABORT ON;	
		BEGIN TRANSACTION;
			SET IDENTITY_INSERT Sales.Salesperson ON;	
			INSERT INTO Sales.SalesPerson (Id,FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
				VALUES	(14,'Denzel','Paniague',300,1,99,'denzel.paniague@ABCCorp.com','7/1/2020');
			SET IDENTITY_INSERT Sales.Salesperson OFF;
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH	
		IF (@@TRANCOUNT > 0)
			ROLLBACK TRANSACTION;	
			INSERT INTO dbo.ErrorLog (MessageId,MessageText,SeverityLevel,[State],LineNumber,
								 ProcedureName)
			VALUES (ERROR_NUMBER(),ERROR_MESSAGE(),ERROR_SEVERITY(),ERROR_STATE(),ERROR_LINE(),
					ERROR_PROCEDURE());		
			THROW;
	END CATCH

	-- Let's check the error log table now
	SELECT * 
	FROM dbo.ErrorLog;

	-- Procedure to log the error message
	CREATE OR ALTER PROCEDURE [dbo].[Log_Error_Message] 
	AS
	BEGIN TRY
		SET NOCOUNT ON;
		SET XACT_ABORT ON;
		BEGIN TRANSACTION;
			INSERT INTO dbo.ErrorLog (MessageId,MessageText,SeverityLevel,[State],LineNumber,
									 ProcedureName)
				VALUES (ERROR_NUMBER(),ERROR_MESSAGE(),ERROR_SEVERITY(),ERROR_STATE(),ERROR_LINE(),
						ERROR_PROCEDURE());
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		IF (@@TRANCOUNT > 0)
			ROLLBACK TRANSACTION;		
			THROW;
	END CATCH

	-- Using the error log procedure
	BEGIN TRY
		SET XACT_ABORT ON;	
		BEGIN TRANSACTION;
			SET IDENTITY_INSERT Sales.Salesperson ON;	
			INSERT INTO Sales.SalesPerson (Id,FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
				VALUES	(14,'Denzel','Paniague',300,1,99,'denzel.paniague@ABCCorp.com','7/1/2020');
			SET IDENTITY_INSERT Sales.Salesperson OFF;
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		IF (@@TRANCOUNT > 0)		
			ROLLBACK TRANSACTION;
			EXECUTE dbo.Log_Error_Message;
			THROW;
	END CATCH

	-- Let's check the error log table again
	SELECT * 
	FROM dbo.ErrorLog;

	CREATE OR ALTER PROCEDURE [Sales].[Insert_SalesOrder]
		@SalesPerson int,
		@SalesAmount decimal(36,2),
		@SalesDate date,
		@SalesTerritory int,
		@OrderDescription nvarchar(max)
	AS
	BEGIN TRY	
		SET NOCOUNT ON;
		SET XACT_ABORT ON;
		BEGIN TRANSACTION;
			IF EXISTS (SELECT 1 FROM Sales.SalesPerson WHERE IsActive = 0 and Id = @SalesPerson)
				BEGIN
					;THROW 65001, 'Please select an active sales person',1;
				END
				ELSE
			INSERT INTO Sales.SalesOrder (SalesPerson,SalesAmount,SalesDate,SalesTerritory,OrderDescription)
				VALUES (@SalesPerson,@SalesAmount,@SalesDate,@SalesTerritory,@OrderDescription);	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		IF (@@TRANCOUNT > 0)
			ROLLBACK TRANSACTION;
			EXECUTE dbo.Log_Error_Message;
			THROW;
	END CATCH

	-- We are using an inactive sales person here
	EXECUTE Sales.Insert_SalesOrder @SalesPerson = 2, @SalesAmount = 7500, @SalesDate = '6/1/2019', @SalesTerritory = 2, @OrderDescription = 'An older order Sally made';

	-- Let's check the error log table again
	SELECT * 
	FROM dbo.ErrorLog;
END