-- How many rows are affected in each query using @@ROWCOUNT
SELECT * FROM dbo.Users WHERE DisplayName LIKE '%jeanjoseph%'

DECLARE @rowsAffected INT
UPDATE [dbo].[Users]
	SET Views = 1001
	WHERE DisplayName LIKE '%jeanjoseph%';

SET @rowsAffected = @@ROWCOUNT

IF @rowsAffected > 0 
	PRINT 'Multiple rows have been affected: ' + CAST(@rowsAffected AS VARCHAR(10));



-- 
DECLARE @YearsActive INT=0
UPDATE [dbo].[Users]
	SET @YearsActive = YEAR(GETDATE()) - YEAR([dbo].[Users].[CreationDate]),
	Reputation += @YearsActive
GO


SELECT Id, DisplayName, Reputation, CreationDate
FROM [dbo].[Users]
ORDER BY Reputation DESC
GO
