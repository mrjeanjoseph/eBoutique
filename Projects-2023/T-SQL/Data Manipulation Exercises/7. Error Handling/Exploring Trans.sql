USE [ABCCompany];
GO

BEGIN --Transcation Modes
	-- Examine SalesPerson table 10 rows
	SELECT * 
	FROM Sales.SalesPerson;

	-- Autocommit - default mode of SQL Server
	INSERT INTO Sales.SalesPerson (FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
		VALUES	('Susan','Jobes',300,1,2,'Susan.Jobes@ABCCorp.com','6/5/2019');

	INSERT INTO Sales.SalesPerson (FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
		VALUES	('Harry','Martin',300,1,2,'Harry.Martin@ABCCorp.com','6/5/2019');

	INSERT INTO Sales.SalesPerson (FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
		VALUES	('Karen','Wright',300,1,4,'Karen.Wright@ABCCorp.com','6/5/2019');

	-- Will any value be inserted
	INSERT INTO Sales.SalesPerson (FirstName,LastName,SalaryHr,ManagerId,LevelId,Email,StartDate) 
		VALUES	('Susan','Jobes',300,1,2,'Susan.Jobes@ABCCorp.com','6/5/2019')
				,('Harry','Martin',300,1,2,'Harry.Martin@ABCCorp.com','6/5/2019')
				,('Karen','Wright',300,1,4,'Karen.Wright@ABCCorp.com','6/5/2019');

	-- Our original count was 10
	SELECT COUNT(1) 
	FROM Sales.SalesPerson;

	-- Implicit transaction
	SET IMPLICIT_TRANSACTIONS ON;
		INSERT INTO Sales.SalesPersonLevel (LevelName)
			VALUES	('Sr Staff');
		INSERT INTO Sales.SalesPersonLevel (LevelName)
			VALUES	('Sr Director');

	-- Check our open transactions
	-- Method 1
	DBCC OPENTRAN;

	-- Method 2
	SELECT	s.session_id
			,s.open_transaction_count
	FROM [sys].[dm_exec_sessions] s
	ORDER BY last_request_start_time DESC;

	-- Session options 2 indicates implicit transactions
	-- https://docs.microsoft.com/en-us/previous-versions/sql/sql-server-2005/ms176031(v=sql.90)
	SELECT @@OPTIONS & 2;

	ROLLBACK TRANSACTION;
	SET IMPLICIT_TRANSACTIONS OFF;

	-- Explicit transaction
	BEGIN TRANSACTION;
		INSERT INTO Sales.SalesPersonLevel (LevelName)
			VALUES	('Sr Staff');
		INSERT INTO Sales.SalesPersonLevel (LevelName)
			VALUES	('Sr Director');
	COMMIT TRANSACTION;

	-- Can we rollback DDL statements
	BEGIN TRANSACTION;
		ALTER TABLE Sales.SalesPersonLevel ADD isActive bit NOT NULL DEFAULT 1;
		TRUNCATE TABLE Sales.SalesOrder;
	ROLLBACK TRANSACTION;

	-- let's check!
	SELECT * 
	FROM Sales.SalesPersonLevel;

	SELECT *
	FROM Sales.SalesOrder;
END



BEGIN --Isolation Level Reads
	-- Can we perform a select
	SELECT * 
	FROM Sales.SalesOrder;

	-- How about on the record we are updating
	SELECT *
	FROM Sales.SalesOrder WHERE Id = 1;

	-- What about a different row
	SELECT *
	FROM Sales.SalesOrder WHERE Id = 2;

	-- Using NOLOCK
	SELECT *
	FROM Sales.SalesOrder WITH (NOLOCK) WHERE Id = 1;

	-- Change isolation level read uncommitted
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	SELECT *
	FROM Sales.SalesOrder WHERE Id = 1;

	-- Let's set this back
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
END

BEGIN --Isolation Update
	-- Check which isolation level we are using
	DBCC USEROPTIONS;

	SELECT	s.session_id AS 'SPID'
			,s.nt_user_name AS 'User'
			,CASE s.transaction_isolation_level 
			WHEN 0 THEN 'Unspecified' 
			WHEN 1 THEN 'Read Uncomitted' 
			WHEN 2 THEN 'Read Comitted' 
			WHEN 3 THEN 'Repeatable' 
			WHEN 4 THEN 'Serializable' 
			WHEN 5 THEN 'Snapshot' 
					  END as 'Isolation Level'
	FROM [sys].[dm_exec_sessions] s
	WHERE s.session_id = @@SPID;

	-- Perform some updates on our salesorder table
	BEGIN TRANSACTION;
		UPDATE Sales.SalesOrder SET OrderDescription = NULL;
	ROLLBACK TRANSACTION;

	-- Let's make sure we have no open transactions
	DBCC OPENTRAN;

	-- Only try and update one row
	BEGIN TRANSACTION;
		UPDATE Sales.SalesOrder SET OrderDescription = NULL WHERE Id = 1;
	ROLLBACK TRANSACTION;
END

BEGIN --Nested Transaction
	-- Check our current transaction count
	SELECT @@TRANCOUNT;

	-- Currently 7 rows
	SELECT * 
	FROM Sales.SalesPersonLevel;

	-- Let's check out how @@TRANCOUNT works
	BEGIN TRANSACTION;
		UPDATE Sales.SalesOrder SET OrderDescription = NULL;
	ROLLBACK TRANSACTION;

	-- Now let's nest a transaction
	BEGIN TRANSACTION Level_1;
		INSERT INTO Sales.SalesPersonLevel (LevelName)
			VALUES	('Vice President');
	BEGIN TRANSACTION Level_2;
		INSERT INTO Sales.SalesPersonLevel (LevelName)
			VALUES ('CIO');
	BEGIN TRANSACTION Level_3;
		INSERT INTO sales.SalesPersonLevel (LevelName)
			VALUES ('Intern');

	-- Check our current transaction count
	SELECT @@TRANCOUNT;

	-- Will this work
	ROLLBACK TRANSACTION Level_3;
	-- I only want to commit the level 2
	COMMIT TRANSACTION Level_2;
	-- Do I have the intern
	SELECT * 
	FROM Sales.SalesPersonLevel;
	-- A rollback must be applied to the outermost
	ROLLBACK TRANSACTION Level_1;

	-- Microsoft article on nested transactions
	-- https://docs.microsoft.com/en-us/previous-versions/sql/sql-server-2008-r2/ms189336(v=sql.105)
	-- Paul Randal
	-- https://www.sqlskills.com/blogs/paul/a-sql-server-dba-myth-a-day-2630-nested-transactions-are-real/
END

BEGIN --Save points

	-- Using save points
	BEGIN TRANSACTION;
		SAVE TRANSACTION Level_1;
			INSERT INTO Sales.SalesPersonLevel (LevelName)
				VALUES	('Vice President');
		SAVE TRANSACTION Level_2;
			INSERT INTO Sales.SalesPersonLevel (LevelName)
				VALUES ('CIO');
		SAVE TRANSACTION Level_3;
			INSERT INTO sales.SalesPersonLevel (LevelName)
				VALUES ('Intern');
		SAVE TRANSACTION Level_4;

	-- Let's check our transaction count
	SELECT @@TRANCOUNT;

	-- now we can remove the intern
	ROLLBACK TRANSACTION Level_3;

	-- Only commit up to save point 3
	COMMIT TRANSACTION Level_3;

	-- Let's check the transaction count now
	SELECT @@TRANCOUNT;

	-- Check out level names
	SELECT * 
	FROM Sales.SalesPersonLevel;

	-- Microsoft article on save points
	-- https://docs.microsoft.com/en-us/sql/t-sql/language-elements/save-transaction-transact-sql?view=sql-server-2017
END