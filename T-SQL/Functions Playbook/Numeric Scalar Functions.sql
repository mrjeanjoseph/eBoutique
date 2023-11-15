--Calculating Values Using Numeric Scalar Functions
USE TSFP;

BEGIN -- Rounding values

	SELECT --Getting started
		Sales_Year,
		SUM(Sales_Value) AS [SUM_Sales_Value]
	FROM Sales_Star ss
	WHERE ss.Product LIKE '%hat%'
	GROUP BY Sales_Year
	
	SELECT --Pre-Rounding the values
		Sales_Year,
		SUM(Sales_Value) AS [SUM_Sales_Value],
		SUM(CAST(Sales_Value AS INT)) AS [PRE_Rounded Values]
	FROM Sales_Star ss
	WHERE ss.Product LIKE '%hat%'
	GROUP BY Sales_Year
	
	SELECT --Convert into INT
		Sales_Year,
		SUM(Sales_Value) AS [SUM_Sales_Value],
		SUM(CAST(Sales_Value AS INT)) AS [PRE_Rounded Values],
		CAST(SUM(Sales_Value) AS INT) AS [INT_Sales_Value]
	FROM Sales_Star ss
	WHERE ss.Product LIKE '%hat%'
	GROUP BY Sales_Year
	
	SELECT --Introducing Ceiling function
		Sales_Year,
		SUM(Sales_Value) AS [SUM_Sales_Value],
		SUM(CAST(Sales_Value AS INT)) AS [PRE_Rounded Values],
		CAST(SUM(Sales_Value) AS INT) AS [INT_Sales_Value],
		CEILING(SUM(Sales_Value)) AS [CEILING_Sales_Value]
	FROM Sales_Star ss
	WHERE ss.Product LIKE '%hat%'
	GROUP BY Sales_Year
	
	SELECT --Introducing floor function
		Sales_Year,
		SUM(Sales_Value) AS [SUM_Sales_Value],
		SUM(CAST(Sales_Value AS INT)) AS [PRE_Rounded Values],
		CAST(SUM(Sales_Value) AS INT) AS [INT_Sales_Value],
		CEILING(SUM(Sales_Value)) AS [CEILING_Sales_Value],
		FLOOR(SUM(Sales_Value)) AS [FLOOR_Sales_Value]
	FROM Sales_Star ss
	WHERE ss.Product LIKE '%hat%'
	GROUP BY Sales_Year
	
	SELECT --Finally The Round function
		Sales_Year,
		SUM(Sales_Value) AS [SUM_Sales_Value],
		SUM(CAST(Sales_Value AS INT)) AS [PRE_Rounded Values],
		CAST(SUM(Sales_Value) AS INT) AS [INT_Sales_Value],
		CEILING(SUM(Sales_Value)) AS [CEILING_Sales_Value],
		FLOOR(SUM(Sales_Value)) AS [FLOOR_Sales_Value],
		ROUND(SUM(Sales_Value),0,1) AS [ROUND_Sales_Value],
		ROUND(SUM(Sales_Value),1,0) AS [ROUND_Sales_Value]
	FROM Sales_Star ss
	WHERE ss.Product LIKE '%shirt%'
	GROUP BY Sales_Year
	
	SELECT --Finally The Round function
		Sales_Year,
		SUM(Sales_Value) AS [SUM_Sales_Value],
		SUM(CAST(Sales_Value AS INT)) AS [PRE_Rounded Values],
		CAST(SUM(Sales_Value) AS INT) AS [INT_Sales_Value],
		CEILING(SUM(Sales_Value)) AS [CEILING_Sales_Value],
		FLOOR(SUM(Sales_Value)) AS [FLOOR_Sales_Value],
		ROUND(SUM(Sales_Value),0,1) AS [ROUND_Sales_Value],
		ROUND(SUM(Sales_Value),-1,0) AS [ROUND UP]
	FROM Sales_Star ss
	WHERE ss.Product LIKE '%shirt%'
	GROUP BY Sales_Year
END GO;

BEGIN --The AB test (Grouping) using Modulus

	SELECT * --Testing the data		
	FROM D_Customer
	
	SELECT
		Customer_Code,
		CASE WHEN Customer_Code % 2 = 0 THEN 'A' ELSE 'B' END [AB_SEGMENT]
	FROM D_Customer
	WHERE Customer_code BETWEEN 2000 AND 600000
	ORDER BY Customer_code

	SELECT --Creating a control group 
		Customer_Code,
		CASE WHEN Customer_Code % 2 = 0 THEN 'A' ELSE 'B' END [AB_SEGMENT],
		RIGHT(Customer_Code, 1) [Control_Group]
	FROM D_Customer
	WHERE Customer_code BETWEEN 2000 AND 600000
	ORDER BY Customer_code
	
	SELECT --Added day grouping
		Customer_Code,
		DATEPART(DY, fs.Sales_Date) [DayInYear],
		CASE WHEN Customer_Code % 2 = 0 THEN 'A' ELSE 'B' END [AB_Segment],
		CASE WHEN DATEPART(DY, fs.Sales_Date) % 2 = 0 THEN 'A' ELSE 'B' END [Day_Segment]
		--RIGHT(Customer_Code, 1) [Control_Group]
	FROM F_SALES fs
	
	SELECT --Added week grouping
		Customer_Code,
		DATEPART(DY, fs.Sales_Date) [DayInYear],
		DATEPART(WK, fs.Sales_Date) [WeekInYear],
		CASE WHEN Customer_Code % 2 = 0 THEN 'A' ELSE 'B' END [AB_Segment],
		CASE WHEN DATEPART(DY, fs.Sales_Date) % 2 = 0 THEN 'A' ELSE 'B' END [Day_Segment],
		CASE WHEN DATEPART(WK, fs.Sales_Date) % 2 = 0 THEN 'A' ELSE 'B' END [Week_Segment]
		--RIGHT(Customer_Code, 1) [Control_Group]
	FROM F_SALES fs
	
	SELECT --Alternate AB Tags
		Customer_Code,
		DATEPART(DY, fs.Sales_Date) [DayInYear],
		DATEPART(WK, fs.Sales_Date) [WeekInYear],
		CASE WHEN Customer_Code % 2 = 0 THEN 'A' ELSE 'B' END [AB_Segment],
		CASE WHEN DATEPART(DY, fs.Sales_Date) % 2 = 0 THEN 'A' ELSE 'B' END [Day_Segment],
		CASE WHEN DATEPART(WK, fs.Sales_Date) % 2 = 0 THEN 'A' ELSE 'B' END [Week_Segment],
		CASE WHEN 
		(CASE WHEN Customer_Code % 2 = 0 THEN 'A' ELSE 'B' END) = 
		(CASE WHEN DATEPART(DY, fs.Sales_Date) % 2 = 0 THEN 'A' ELSE 'B' END)
			THEN 'A' ELSE 'B' END [Day_Rotate_AB],
		CASE WHEN 
		(CASE WHEN Customer_Code % 2 = 0 THEN 'A' ELSE 'B' END) = 
		(CASE WHEN DATEPART(WK, fs.Sales_Date) % 2 = 0 THEN 'A' ELSE 'B' END)
			THEN 'A' ELSE 'B' END [Week_Rotate_AB]
	FROM F_SALES fs
END