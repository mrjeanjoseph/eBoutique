USE ABCCompany

BEGIN --SQL Errors

	-- Start a new error log
	-- Please don't run this in production
	EXEC sp_cycle_errorlog;  

	-- Which line number do we get back
	SELECT	'First Script'
			,1/0;

	-- We can check the error number
	SELECT @@ERROR;

	-- Use a variable to hold the error number
	DECLARE @ErrorId int; 
	SELECT 1/0;
	SET @ErrorId = @@ERROR;
	SELECT @ErrorId;

	-- Let's check out the message
	SELECT message_id
		   ,language_id
		   ,severity
		   ,is_event_logged
		   ,[text]	
	FROM [sys].[messages] 
	WHERE message_id = 8134

	-- These are messages which are logged
	SELECT message_id
		   ,language_id
		   ,severity
		   ,is_event_logged
		   ,[text]	
	FROM [sys].[messages] 
	WHERE is_event_logged = 1

	-- Add our own error messages
	EXEC sp_addmessage @msgnum=50011,@severity=16,
		@msgtext='Row count does not match';  

	-- Let's see if our message exists
	SELECT message_id
		   ,language_id
		   ,severity
		   ,is_event_logged
		   ,[text]	
	FROM [sys].[messages] 
	WHERE message_id = 50011

	-- If we want to change the error to be logged
	EXEC sp_altermessage @message_id = 50010, @parameter = 'WITH_LOG',
	@parameter_value = 'TRUE';

	-- Let's drop the message
	-- Might also want to pass in the language
	EXEC sp_dropmessage @msgnum = 50010;
END

BEGIN --Using RAISERROR

		-- Start a new error log
		-- Please do not run this in production
		EXEC sp_cycle_errorlog;  

	-- This will not work
	--RAISEERROR('There is something wrong here',16,1);

	-- Raise a message without the Id
	RAISERROR('The row count does not match',16,1);

	-- Raise where message Id does not exist
	RAISERROR(65000,16,1);

	-- Raise with a low severity
	RAISERROR('This is a lower severity message',1,1);

	-- Will be logged to the error log
	RAISERROR('Sa a se yon message',16,1) WITH LOG;

	-- Using a variable as the message text
	DECLARE @MessageText nvarchar(500);
	SET @MessageText = 'Sa a se yon message 2';

	RAISERROR(@MessageText,16,1);

	-- A way to check the error and log it
	DECLARE @ErrorNumber int;
	SELECT 1/0;
	SET @ErrorNumber = @@ERROR;
	IF (@ErrorNumber = 8134)
		BEGIN
			RAISERROR('Sil te plais, sispann divize zero',0,1) WITH LOG;
		END

	-- Will show the message right away
	RAISERROR('I can not wait 10 seconds',16,1) WITH NOWAIT;
	WAITFOR DELAY '00:00:10';

	EXEC sp_addmessage @msgnum=50010,@severity=16,
		@msgtext='Row count from the %s table does not match',
		@replace = 'replace';  

	-- Raise a message without the Id
	DECLARE @TableName nvarchar(100) = 'SalesOrder++';
	RAISERROR(50010,16,1,@TableName);

	-- If the severity is over 19 our connection will terminate
	RAISERROR('This is a fatal message',20,1) WITH LOG;
END

BEGIN -- Using CatchAll
	-- Let's check and see how many rows we have
	SELECT *
	FROM Sales.SalesPerson;

	-- This insert should succeed 
	-- We should not enter the catch
	BEGIN TRY	
		SET IDENTITY_INSERT Sales.Salesperson ON;	
		INSERT INTO Sales.SalesPerson (Id,FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
			VALUES	(13,'Bruce','Wayne',125,1,1,'Bruce.Wayne@ABCCorp.com','7/1/2019');
		INSERT INTO Sales.SalesPerson (Id,FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
			VALUES	(14,'Drake','Mallard',300,1,1,'Drake.Mallard@ABCCorp.com','7/1/2019');
		INSERT INTO Sales.SalesPerson (Id,FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
			VALUES	(15,'Clark','Kent',300,1,2,'Clark.Kent@ABCCorp.com','7/1/2019');
		SET IDENTITY_INSERT Sales.Salesperson OFF;
	END TRY
	BEGIN CATCH
		PRINT 'Does this execute?';
	END CATCH

	-- Let's remove so we can try again
	DELETE FROM Sales.SalesPerson WHERE Id IN (13,14,15);

	-- The second insert will not work
	-- Will the third row be inserted
	BEGIN TRY	
		SET IDENTITY_INSERT Sales.Salesperson ON;	
		INSERT INTO Sales.SalesPerson (Id,FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
			VALUES	(13,'Bruce','Wayne',125,1,1,'Bruce.Wayne@ABCCorp.com','7/1/2019');
		INSERT INTO Sales.SalesPerson (Id,FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
			VALUES	(14,'Drake','Mallard',300,1,99,'Drake.Mallard@ABCCorp.com','7/1/2019');
		INSERT INTO Sales.SalesPerson (Id,FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
			VALUES	(15,'Clark','Kent',300,1,2,'Clark.Kent@ABCCorp.com','7/1/2019');
		SET IDENTITY_INSERT Sales.Salesperson OFF;
	END TRY
	BEGIN CATCH
		PRINT 'Start Catch';
	END CATCH

	-- Did either row get inserted
	SELECT * 
	FROM Sales.SalesPerson;

	-- Let's clean up
	DELETE FROM Sales.SalesPerson WHERE Id IN (13,14,15);

	-- The second insert will not work
	-- Is this the error we want to see
	BEGIN TRY
		SET IDENTITY_INSERT Sales.Salesperson ON;	
		INSERT INTO Sales.SalesPerson (Id,FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
			VALUES	(13,'Bruce','Wayne',125,1,1,'Bruce.Wayne@ABCCorp.com','7/1/2019');
		INSERT INTO Sales.SalesPerson (Id,FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
			VALUES	(14,'Drake','Mallard',300,1,99,'Drake.Mallard@ABCCorp.com','7/1/2019');
		INSERT INTO Sales.SalesPerson (Id,FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
			VALUES	(15,'Clark','Kent',300,1,2,'Clark.Kent@ABCCorp.com','7/1/2019');
		SET IDENTITY_INSERT Sales.Salesperson OFF;
	END TRY
	BEGIN CATCH
		RAISERROR('Something went really wrong',16,1);
	END CATCH

	-- Let's clean up
	DELETE FROM Sales.SalesPerson WHERE Id IN (13,14,15);

	-- This is more like it
	BEGIN TRY	
		SET IDENTITY_INSERT Sales.Salesperson ON;	
		INSERT INTO Sales.SalesPerson (Id,FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
			VALUES	(13,'Bruce','Wayne',125,1,1,'Bruce.Wayne@ABCCorp.com','7/1/2019');
		INSERT INTO Sales.SalesPerson (Id,FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
			VALUES	(14,'Drake','Mallard',300,1,99,'Drake.Mallard@ABCCorp.com','7/1/2019');
		INSERT INTO Sales.SalesPerson (Id,FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
			VALUES	(15,'Clark','Kent',300,1,2,'Clark.Kent@ABCCorp.com','7/1/2019');
		SET IDENTITY_INSERT Sales.Salesperson OFF;	
	END TRY
	BEGIN CATCH
		DECLARE @ErrorMessage nvarchar(250);
		DECLARE @ErrorSeverity int;
		DECLARE @ErrorState int;
		DECLARE @ErrorLine int;	
		SELECT	 @ErrorMessage = ERROR_MESSAGE()
				,@ErrorSeverity = ERROR_SEVERITY()
				,@ErrorState = ERROR_STATE()
				,@ErrorLine = ERROR_LINE();
		RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState,@ErrorLine);
	END CATCH
END

BEGIN -- USING ABORT_XACT_STATE
	-- Let's remove these
	DELETE FROM Sales.SalesPerson WHERE Id IN (13,14,15);

	-- Let's check and see how many rows we have
	SELECT *
	FROM Sales.SalesPerson;

	-- Using XACT_ABORT ON without an explicit transaction
	-- Are any of the rows inserted
	SET XACT_ABORT ON;	
		SET IDENTITY_INSERT Sales.Salesperson ON;	
		INSERT INTO Sales.SalesPerson (Id,FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
			VALUES	(13,'Bruce','Wayne',125,1,1,'Bruce.Wayne@ABCCorp.com','7/1/2019');
		INSERT INTO Sales.SalesPerson (Id,FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
			VALUES	(14,'Drake','Mallard',300,1,99,'Drake.Mallard@ABCCorp.com','7/1/2019');
		INSERT INTO Sales.SalesPerson (Id,FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
			VALUES	(15,'Clark','Kent',300,1,2,'Clark.Kent@ABCCorp.com','7/1/2019');
		SET IDENTITY_INSERT Sales.Salesperson OFF;

	-- Which rows were inserted
	SELECT * 
	FROM Sales.SalesPerson;

	-- Let's remove so we can try again
	DELETE FROM Sales.SalesPerson WHERE Id IN (13,14,15);

	-- Using XACT_ABORT ON with an explicit transaction
	-- Will any rows be inserted
	SET XACT_ABORT ON;
	BEGIN TRANSACTION;	
		SET IDENTITY_INSERT Sales.Salesperson ON;	
		INSERT INTO Sales.SalesPerson (Id,FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
			VALUES	(13,'Bruce','Wayne',125,1,1,'Bruce.Wayne@ABCCorp.com','7/1/2019');
		INSERT INTO Sales.SalesPerson (Id,FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
			VALUES	(14,'Drake','Mallard',300,1,99,'Drake.Mallard@ABCCorp.com','7/1/2019');
		INSERT INTO Sales.SalesPerson (Id,FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
			VALUES	(15,'Clark','Kent',300,1,2,'Clark.Kent@ABCCorp.com','7/1/2019');
		SET IDENTITY_INSERT Sales.Salesperson OFF;
	COMMIT TRANSACTION;

	-- Did either row get inserted
	SELECT * 
	FROM Sales.SalesPerson;

	-- Let's clean up
	DELETE FROM Sales.SalesPerson WHERE Id IN (13,14,15);

	-- XACT_ABORT OFF & XACT_STATE
	SET XACT_ABORT OFF;
	BEGIN TRY
	BEGIN TRANSACTION;	
		SELECT XACT_STATE(); -- Should be 1
		SET IDENTITY_INSERT Sales.Salesperson ON;	
		INSERT INTO Sales.SalesPerson (Id,FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
			VALUES	(13,'Bruce','Wayne',125,1,1,'Bruce.Wayne@ABCCorp.com','7/1/2019');
		INSERT INTO Sales.SalesPerson (Id,FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
			VALUES	(14,'Drake','Mallard',300,1,99,'Drake.Mallard@ABCCorp.com','7/1/2019');
		INSERT INTO Sales.SalesPerson (Id,FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
			VALUES	(15,'Clark','Kent',300,1,2,'Clark.Kent@ABCCorp.com','7/1/2019');
		SET IDENTITY_INSERT Sales.Salesperson OFF;
	COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		SELECT XACT_STATE();
		IF (XACT_STATE() = -1)
			BEGIN
				PRINT 'Things are not looking good';
				ROLLBACK TRANSACTION;
			END
		IF (XACT_STATE() = 1)
			BEGIN
				PRINT 'At least something works';
				COMMIT TRANSACTION;
			END
	END CATCH

	-- Did either row get inserted
	SELECT * 
	FROM Sales.SalesPerson;

	-- Let's clean up
	DELETE FROM Sales.SalesPerson WHERE Id IN (13,14,15);

	-- XACT_ABORT ON & XACT_STATE
	SET XACT_ABORT ON;
	BEGIN TRY
	BEGIN TRANSACTION;	
		SELECT XACT_STATE(); -- Should be 1
		SET IDENTITY_INSERT Sales.Salesperson ON;	
		INSERT INTO Sales.SalesPerson (Id,FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
			VALUES	(13,'Bruce','Wayne',125,1,1,'Bruce.Wayne@ABCCorp.com','7/1/2019');
		INSERT INTO Sales.SalesPerson (Id,FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
			VALUES	(14,'Drake','Mallard',300,1,99,'Drake.Mallard@ABCCorp.com','7/1/2019');
		INSERT INTO Sales.SalesPerson (Id,FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
			VALUES	(15,'Clark','Kent',300,1,2,'Clark.Kent@ABCCorp.com','7/1/2019');
		SET IDENTITY_INSERT Sales.Salesperson OFF;
	COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH	
		SELECT XACT_STATE();	
		IF (XACT_STATE() = -1)
			BEGIN
				PRINT 'Things are not looking good';
				ROLLBACK TRANSACTION;
			END
		IF (XACT_STATE() = 1)
			BEGIN
				PRINT 'At least something works';
				COMMIT TRANSACTION;
			END
	END CATCH

	-- Did either row get inserted
	SELECT * 
	FROM Sales.SalesPerson;
END

BEGIN --USING THROW **RECOMMENDED**
	-- This will not work outside a catch
	--THROW;

	-- Raise a message without the Id
	-- Notice we don't specify the severity
	--THROW 50010,'This is a great message',1;

-- The syntax for throw is simple
-- Notice what line number is returned
	BEGIN TRY	
		SELECT 1/0;
	END TRY
	BEGIN CATCH
		THROW;
		PRINT 'Does this print?';
	END CATCH

	-- Using a variable as the message text and number
	DECLARE @MessageText2 nvarchar(500);
	DECLARE @ErrorNumber2 int;
	SET @MessageText2 = 'This is a custom error message';
	SET @ErrorNumber2 = 65000;

	THROW @ErrorNumber2,@MessageText2,1;
END