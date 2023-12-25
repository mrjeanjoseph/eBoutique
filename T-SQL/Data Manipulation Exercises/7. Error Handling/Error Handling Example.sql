USE ABCCompany

BEGIN --Handling Conversion Errors
	-- Just using CAST
	SELECT CAST('31 Dec 12' AS date) AS 'First Date'
		   ,CAST('Dec 12 1776 12:38AM' AS date) AS 'Second Date'
		   ,CAST('Dec 12 1400 12:38AM' AS datetime) AS 'Third Date'
		   ,CAST('Number 3' AS int) AS 'Number 3'

	-- TRY_CAST 
	SELECT TRY_CAST('30 Dec 06' AS date) AS 'First Date'
		   ,TRY_CAST('Dec 12 1776 12:38AM' AS datetime) AS 'Second Date'
		   ,TRY_CAST('Dec 12 1400 12:38AM' AS datetime) AS 'Third Date'
		   ,TRY_CAST('Number 3' AS int) AS 'Number 3';

	-- CONVERT 
	SELECT CONVERT(date,'30 Dec 06',101)
		   ,CONVERT(int, '00002A');

	-- TRY_CONVERT
	SELECT TRY_CONVERT(date,'30 Dec 06',101)
		   ,TRY_CONVERT(int, '00002A');

	-- Returns an exception
	SELECT TRY_CONVERT(xml,123);

	-- Using CASE
	SELECT CASE WHEN TRY_CONVERT(int, '00002B') IS NULL 
		THEN 99
		END AS Id;
END

BEGIN --Transferring FUNDS

	-- First let's check the balance in our savings and checking account
	SELECT SUM(Amount) AS 'Balance', 'Checking' AS 'Account Type'
	FROM Bank.Checking
	UNION ALL
	SELECT SUM(Amount) AS 'Balance', 'Savings' AS 'Account Type'
	FROM Bank.Savings;
	-- Checking is $350
	-- Savings is $4100

	-- Transfer funds from the savings account to the checking
	INSERT INTO Bank.Savings (Amount, TransactionNotes)
		VALUES (-500,'Sorry mom I really need this game');

	INSERT INTO Bank.Checking (Amount, TransactionNotes)
		VALUES (500,'Adding funds out to buy the sealed original Super Mario Bros on NES');

	-- Let's check the balance in our savings and checking account
	SELECT SUM(Amount) AS 'Balance', 'Checking' AS 'Account Type'
	FROM Bank.Checking
	UNION ALL
	SELECT SUM(Amount) AS 'Balance', 'Savings' AS 'Account Type'
	FROM Bank.Savings;
	
	-- Transfer funds from the savings account to the checking
	INSERT INTO Bank.Savings (Amount, TransactionNotes)
		VALUES (-500,'Sorry mom I really need this game');

	INSERT INTO Bank.Checking (Amount, TransactionNotes)
		VALUES (500,'Adding funds out to buy the sealed original Super Mario Bros on NES');

	-- Remove the failed amount
	DELETE FROM Bank.Savings 
	WHERE Amount = -500 AND TransactionNotes = 'Sorry mom I really need this game';

	-- Let's check the balance in our savings and checking account
	SELECT SUM(Amount) AS 'Balance', 'Checking' AS 'Account Type'
	FROM Bank.Checking
	UNION ALL
	SELECT SUM(Amount) AS 'Balance', 'Savings' AS 'Account Type'
	FROM Bank.Savings;
	-- Checking is $350
	-- Savings is $4100

	BEGIN TRY	
		BEGIN TRANSACTION;
			INSERT INTO Bank.Savings (Amount, TransactionNotes)
				VALUES (-500,'Sorry mom I really need this game');

			INSERT INTO Bank.Checking (Amount, TransactionNotes)
				VALUES (500,'Adding funds out to buy the sealed original Super Mario Bros on NES');

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		IF (@@TRANCOUNT > 0)
			ROLLBACK TRANSACTION;
			THROW;
	END CATCH

	-- Let's check the balance in our savings and checking account
	SELECT SUM(Amount) AS 'Balance', 'Checking' AS 'Account Type'
	FROM Bank.Checking
	UNION ALL
	SELECT SUM(Amount) AS 'Balance', 'Savings' AS 'Account Type'
	FROM Bank.Savings;
	-- Checking is $350
	-- Savings is $4100
END