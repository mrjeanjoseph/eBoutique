-- Module 7
-- Query Data with static value function

BEGIN --Static Value Function

	SELECT CURRENT_USER;
	SELECT SYSTEM_USER;

	SELECT SUSER_NAME(1)
	SELECT SUSER_NAME(2)
	SELECT SUSER_NAME(3)
	SELECT SUSER_NAME(4)
	SELECT SUSER_NAME(5) -- none

	SELECT SYSDATETIME();

	SELECT CONVERT(DATE, SYSDATETIME());

	SELECT
		SYSDATETIME(),
		SYSDATETIMEOFFSET(),
		SYSUTCDATETIME(),
		CURRENT_TIMESTAMP,
		GETDATE(),
		GETUTCDATE()


	SELECT
		CONVERT(DATE, SYSDATETIME()),
		CONVERT(DATE, SYSDATETIMEOFFSET()),
		CONVERT(DATE, SYSUTCDATETIME()),
		CONVERT(DATE, CURRENT_TIMESTAMP),
		CONVERT(DATE, GETDATE()),
		CONVERT(DATE, GETUTCDATE())
END GO;

BEGIN --Monthly Rolling Sales with GETDATE
	
	SELECT EOMONTH(GETDATE(), -12) [End of Month]

	SELECT * FROM Sales_Star --Altered the view and brought the year columns forward by 5 years.

	SELECT -- Starting 12 month back
		FORMAT(s.Sales_Date, 'yyyy-MM') [Sales Date],
		SUM(s.Sales_Value) [Sales Value]
	FROM Sales_Star s
	WHERE s.Sales_Date = EOMONTH(GETDATE(), -12)
	GROUP BY FORMAT(s.Sales_Date, 'yyyy-MM')
	
	SELECT -- then extend an additional 12 month back some more.
		FORMAT(s.Sales_Date, 'yyyy-MM') [Sales Date],
		SUM(s.Sales_Value) [Sales Value]
	FROM Sales_Star s
	WHERE s.Sales_Date 
		BETWEEN EOMONTH(GETDATE(), -24) 
		AND EOMONTH(GETDATE(), -12)
	GROUP BY FORMAT(s.Sales_Date, 'yyyy-MM')
	ORDER BY 1

	
	SELECT -- Getting additional dates from the beginning of the starting month.
		FORMAT(s.Sales_Date, 'MM-yyyy') [Sales Date],
		SUM(s.Sales_Value) [Sales Value]
	FROM Sales_Star s
	WHERE s.Sales_Date 
		BETWEEN DATEADD( DAY, 1, EOMONTH(GETDATE(), -24))
		AND EOMONTH(GETDATE(), -12)
	GROUP BY FORMAT(s.Sales_Date, 'MM-yyyy')
	ORDER BY 1
END GO;