
--Query Data with Inline Queries and Scalar Functions
BEGIN -- 
	--Using the Product table to implement LIKE function
	SELECT DISTINCT Product
	FROM D_Product_Attributes
	WHERE Product LIKE '%w%' --Return text with 'w' somewhere in the text

	
	SELECT DISTINCT Product
	FROM D_Product_Attributes
	WHERE Product LIKE 'w%' -- starts with w

	SELECT DISTINCT Product
	FROM D_Product_Attributes
	WHERE Product LIKE '%w' -- ends with w

	SELECT DISTINCT Product
	FROM D_Product_Attributes
	WHERE Product LIKE '%t' -- ends with t

	SELECT DISTINCT Product
	FROM D_Product_Attributes
	WHERE Product LIKE '%r%' -- Get text with r anywhere within the text

	SELECT DISTINCT Product
	FROM D_Product_Attributes
	WHERE Product LIKE '_r%' --Get r at second location

	SELECT DISTINCT Product
	FROM D_Product_Attributes
	WHERE Product LIKE '_a%' -- Get a at second location

	SELECT DISTINCT Product
	FROM D_Product_Attributes
	WHERE Product LIKE '[a-z]a%' -- Get text from a-z
	
	SELECT DISTINCT Product
	FROM D_Product_Attributes
	WHERE Product LIKE '[a-g]a%' -- Get text from a-g
	
	SELECT DISTINCT Product
	FROM D_Product_Attributes
	WHERE Product LIKE '%[^r]' -- do not end in r

	SELECT DISTINCT Product
	FROM D_Product_Attributes
	WHERE Product LIKE '%ou%' -- contains ou somewhere in the text

	SELECT DISTINCT Product
	FROM D_Product_Attributes
	WHERE Product LIKE '%ou%[^r]' -- contains ou somewhere in the text but does not end with an r

END GO;

-- Creating Statements with simple Scalar Functions
BEGIN -- Combining simple functions
	--Tricky spaces
	SELECT DISTINCT
	Product,
	LEN(Product) [Length]
	FROM D_Product_Attributes
	WHERE Product LIKE '% ' -- This will find keywords that have extra spaces in them.
	
	--Testing the LIKE function by finding the letter t
	SELECT DISTINCT
	Product,
	LEN(Product) [Length]
	FROM D_Product_Attributes
	WHERE Product LIKE '%t' -- all products that end with a 't'
	
	--Using the RIGHT function to remove the first letter
	SELECT DISTINCT
	RIGHT(Product, LEN(Product)-1) [Right_], --Notice the -1 for the first char removal
	LEN(Product) [Length] 
	FROM D_Product_Attributes
	WHERE Product LIKE '%t'
	
	--Using the LEFT function to remove the last letter
	SELECT DISTINCT
	LEFT(Product, LEN(Product)-1) [Left_], --Notice the -1 for the last char removal
	LEN(Product) [Length] 
	FROM D_Product_Attributes
	WHERE Product LIKE '%t'

	-- Finding a specific keyword
	SELECT DISTINCT Product
	FROM D_Product_Attributes pa
	WHERE Product LIKE '%shirt%' -- This will find keywords that have extra spaces in them.
	
	--Using the LEFT function to remove the last letter
	SELECT DISTINCT
	LOWER(RIGHT(Product, LEN(Product)-1)) [Left_], --Notice the -1 for the last char removal
	LEN(Product) [Length] 
	FROM D_Product_Attributes
	WHERE Product LIKE '%t'

	-- Getting that specific first letter and make it uppercase
	SELECT DISTINCT 
	UPPER(LEFT(Product,1)) [Product]
	FROM D_Product_Attributes pa
	WHERE Product LIKE '%shirt%'
	
	-- Introducing CONCAT to bring in everything into one word First letter Uppercase
	SELECT DISTINCT 
	CONCAT(
		UPPER(LEFT(Product,1)),
		LOWER(RIGHT(Product, LEN(Product)-1))) [Product]
	FROM D_Product_Attributes pa
	WHERE Product LIKE '%shirt%'
END GO;

BEGIN --Formatting Functions
	
	--To format dates
	SELECT
		Sales_Date,
		FORMAT(Sales_Date, 'yyyy') [Year String], --Will display Just the year string
		FORMAT(Sales_Date, 'yyyyMM') [MonthYear String],--Will display year and month string
		FORMAT(Sales_Date, 'MMddyyyy') [Date Only]--Will display Just the year string
	FROM D_Sales_Calendar

	--To format percentage
	SELECT 
		FORMAT(.71, 'p'), 
		FORMAT(.71, 'P0'),
		FORMAT(GETDATE(), 'hh:mm:ss')

	SELECT FORMAT(Sales_Value, '###.##')
	FROM Sales_Star s

	SELECT CAST(Sales_Value AS INT) FROM Sales_Star;
END GO;

BEGIN --Inline View Queries and Subqueries
	--Buiding a inline view queries and subqueries
	SELECT 
		ss.Product, 
		ss.Sales_Date [Last Sales Date], 
		SUM(Sales_Value) [Sum SalesValue]
	FROM Sales_Star ss
	WHERE ss.Sales_Date = 
		(SELECT MAX(ss2.Sales_Date) 
		 FROM Sales_Star ss2
		 WHERE ss.Product = ss2.Product)
	GROUP BY ss.Product, SS.Sales_Date
	ORDER BY ss.Sales_Date DESC

	SELECT 
		s.Product,
		DATEADD(DAY, -365, iv.[Last Sales Date]) [Start_Date],
		iv.[Last Sales Date],
		SUM(Sales_Value) [Sum SalesValue]
	FROM Sales_Star s,
		(SELECT 
			ss.Product, 
			ss.Sales_Date [Last Sales Date], 
			SUM(Sales_Value) [Sum SalesValue]
		FROM Sales_Star ss
		WHERE ss.Sales_Date = (
			SELECT MAX(ss2.Sales_Date) 
			FROM Sales_Star ss2
			WHERE ss.Product = ss2.Product)
		GROUP BY ss.Product, SS.Sales_Date) AS iv
	WHERE s.Product = iv.Product 
	AND s.Sales_Date BETWEEN DATEADD(DAY, -365, iv.[Last Sales Date]) AND iv.[Last Sales Date]
	GROUP BY s.Product, iv.[Last Sales Date], DATEADD(DAY, -365, iv.[Last Sales Date])
	ORDER BY 4 DESC
	--ORDER BY ss.Sales_Date DESC
END GO;

BEGIN -- Additional Challenges - Subquieries
	--Look at sale from 2018
	SELECT 
		s.Product, 
		s.Sales_Year,
		FORMAT(SUM(s.Sales_Value) / ss.Sales, 'P2') [Sales_PCT],
		SUM(s.Sales_Value) [sales_value],
		ss.Sales
	FROM Sales_Star s,
		(SELECT s2.Sales_Year, SUM(s2.Sales_Value) [Sales]
		FROM Sales_Star s2
		GROUP BY s2.Sales_Year) ss
	GROUP BY s.Product, s.Sales_Year, ss.Sales
	ORDER BY 2,3 DESC

	--Getting a list of Customers in the top percentile
	SELECT COUNT(DISTINCT Customer_Code) FROM Sales_Star;-- Getting a list of distinct customers

	SELECT
		s.Customer_Code,
		CAST(PERCENT_RANK() OVER (ORDER BY s.Sales_Value) AS DECIMAL(4,2)) [Percentile]	
	FROM Sales_Star s
	HAVING CAST(PERCENT_RANK() OVER (ORDER BY SUM(s.Sales_Value)) AS DECIMAL(4,2)) = 1
	--Msg 4108, Level 15, State 1, Line 194
	--Windowed functions can only appear in the SELECT or ORDER BY clauses.

	--We will instead use an inner quiery
	SELECT sq.Customer_Code 
	FROM (SELECT
			s.Customer_Code,
			CAST(PERCENT_RANK() OVER (ORDER BY SUM(s.Sales_Value)) AS DECIMAL(4,2)) [Percentile]
		FROM Sales_Star s
		GROUP BY s.Customer_Code) sq
	WHERE sq.Percentile = 1 ;
END GO;