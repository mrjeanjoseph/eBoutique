USE master;  
GO  
CREATE LOGIN login_test WITH PASSWORD = N'3KHJ6dhx(0xVYsdf' MUST_CHANGE,  
    CHECK_EXPIRATION = ON;  
GO  
GRANT VIEW SERVER STATE TO login_test;  
GO  
--Trigger to limit login attemps
CREATE TRIGGER connection_limit_trigger  
ON ALL SERVER WITH EXECUTE AS N'login_test'  
FOR LOGON  
AS  
BEGIN  
IF ORIGINAL_LOGIN()= N'login_test' AND  
    (SELECT COUNT(*) FROM sys.dm_exec_sessions  
            WHERE is_user_process = 1 AND  
                original_login_name = N'login_test') > 3  
    ROLLBACK;  
END;


--DECLARE @workingWithDates AS VARCHAR(10);
--SET @workingWithDates = '08/05/2022';
--SELECT TRY_PARSE(@workingWithDates AS DATE USING 'FR')


DECLARE @workingWithDates AS VARCHAR(10);
SET @workingWithDates = '08/05/2022';
SELECT TRY_CONVERT(datetime, @workingWithDates, 130)


DECLARE @counter INT = 0
DECLARE @workingWithDates DATETIME = '2022-07-30 00:38:54.840'

CREATE TABLE #dateFormats2 (dateFormatOption int, dateOutput nvarchar(40))

WHILE (@counter <= 150 )
BEGIN
   BEGIN TRY
      INSERT INTO #dateFormats2
      SELECT CONVERT(nvarchar, @counter), CONVERT(nvarchar,GETDATE(), @counter) 
      SET @counter = @counter + 1
   END TRY
   BEGIN CATCH;
      SET @counter = @counter + 1
      IF @counter >= 150
      BEGIN
         BREAK
      END
   END CATCH
END
SELECT * FROM #dateFormats2

SELECT CONVERT(VARCHAR(20), GETDATE(), 100);


SELECT COUNT(*) AS [Total Row Count]
FROM [dbo].[Categories]

SELECT * FROM [dbo].[Employees]

SELECT c.EmployeeID [Customer ID],
		SUM(p.rows) [Total Row Count]
FROM [dbo].[Employees] c
JOIN sys.partitions p
ON c.EmployeeID = p.object_id
AND p.index_id IN (0,1)
GROUP BY c.EmployeeID