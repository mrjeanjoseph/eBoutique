--Aggregating and Summarizing Data with Functions
USE [TSFP]
BEGIN --The Percent_Rank Function
	SELECT 
		Sales_Year,
		SUM(Total_Sales_Qty) [Annual Sales],
		PERCENT_RANK() OVER(ORDER BY SUM(Total_Sales_Qty) ASC) [Percent Rank]
	FROM A_SALES_BY_YEAR
	GROUP BY Sales_Year
	
	--
	SELECT 
		Sales_Year,
		SUM(Total_Sales_Qty) [Annual Sales],
		FORMAT(PERCENT_RANK() OVER(ORDER BY SUM(Total_Sales_Qty) ASC), 'p') [Percent Rank],
		CAST(PERCENT_RANK() OVER(ORDER BY SUM(Total_Sales_Qty) ASC) * 100 AS INT) [Percent Rank	INT]
	FROM A_SALES_BY_YEAR
	GROUP BY Sales_Year

	-- Adding product column
	SELECT 
		Sales_Year,
		Product,
		SUM(Total_Sales_Qty) [Annual Sales],
		FORMAT(PERCENT_RANK() OVER(ORDER BY SUM(Total_Sales_Qty) ASC), 'p') [Percent Rank],
		CAST(PERCENT_RANK() OVER(ORDER BY SUM(Total_Sales_Qty) ASC) * 100 AS INT) [Percent Rank	INT]
	FROM A_SALES_BY_YEAR
	GROUP BY Sales_Year, Product

	
	-- with PARTITION BY clause over the Sales_Year
	SELECT 
		Sales_Year,
		Product,
		SUM(Total_Sales_Qty) [Annual Sales],
		FORMAT(PERCENT_RANK() OVER(PARTITION BY Sales_Year ORDER BY SUM(Total_Sales_Qty) ASC), 'p') [Percent Rank Year],
		FORMAT(PERCENT_RANK() OVER(ORDER BY SUM(Total_Sales_Qty) ASC), 'p') [Percent Rank All]
	FROM A_SALES_BY_YEAR
	GROUP BY Sales_Year, Product
	
	-- Filtering by Year
	SELECT 
		Sales_Year,
		Product,
		SUM(Total_Sales_Qty) [Annual Sales],
		FORMAT(PERCENT_RANK() OVER(PARTITION BY Sales_Year ORDER BY SUM(Total_Sales_Qty) ASC), 'p') [Percent Rank],
		FORMAT(PERCENT_RANK() OVER(ORDER BY SUM(Total_Sales_Qty) ASC), 'p') [Percent Rank All]
	FROM A_SALES_BY_YEAR
	WHERE Sales_Year = 2018
	GROUP BY Sales_Year, Product

	
	-- Adding the Colour column
	-- Percent_Rank must always have an ASC ORDER BY clause 
	SELECT 
		Sales_Year,
		Product,
		Colour,
		SUM(Total_Sales_Qty) [Annual Sales],
		FORMAT(PERCENT_RANK() OVER(ORDER BY SUM(Total_Sales_Qty) ASC), 'p') [Percent Rank All],
		DENSE_RANK() OVER (ORDER BY SUM(Total_Sales_Qty) DESC) [Dense Rank] --This will force dataset to be ranked
	FROM A_SALES_BY_YEAR
	WHERE Sales_Year = 2018
	GROUP BY Sales_Year, Product, Colour
	ORDER BY 4 DESC
END GO;

BEGIN -- First and Last Value
	--FIRST = ROWS UNBOUNDED PRECEDING
	--LAST = ROWS UNBOUNDED FOLLOWING
	
	
	SELECT
		Sales_Year,
		Product,
		SUM(Total_Sales_Qty) AS [Annual Sales]
	FROM A_SALES_BY_YEAR sby
	WHERE Sales_Year = 2023
	GROUP BY Sales_Year, Product


	-- Looking for the first values
	SELECT 
		Sales_Year,
		Product,
		SUM(Total_Sales_Qty) AS [Annual Sales],
		FIRST_VALUE(SUM(Total_Sales_Qty)) OVER(ORDER BY SUM(Total_Sales_Qty)) AS [First Value]
	FROM A_SALES_BY_YEAR sby
	WHERE Sales_Year = 2023
	GROUP BY Sales_Year, Product

	-- Removing the Sales_Year filter to get all the years
	SELECT 
		Sales_Year,
		Product,
		SUM(Total_Sales_Qty) AS [Annual Sales],
		FIRST_VALUE(SUM(Total_Sales_Qty)) OVER(ORDER BY SUM(Total_Sales_Qty)) AS [First Value]
	FROM A_SALES_BY_YEAR sby
	--WHERE Sales_Year = 2023
	GROUP BY Sales_Year, Product

	
	-- Drilling deeper into the data by adding additional attributes
	-- Adding the Product_Type
	SELECT 
		Sales_Year,
		Product,
		Product_Type,
		SUM(Total_Sales_Qty) AS [Annual Sales],
		FIRST_VALUE(SUM(Total_Sales_Qty)) OVER(ORDER BY SUM(Total_Sales_Qty)) AS [First Value]
	FROM A_SALES_BY_YEAR sby
	--WHERE Sales_Year = 2023
	GROUP BY Sales_Year, Product, Product_Type
	

	-- Adding the LAST_VALUE based on the same parameters ** 
	-- We may be forgetting something here **
	SELECT 
		Sales_Year,
		Product,
		Product_Type,
		SUM(Total_Sales_Qty) AS [Annual Sales],
		FIRST_VALUE(SUM(Total_Sales_Qty)) OVER(ORDER BY SUM(Total_Sales_Qty)) AS [First Value],
		LAST_VALUE(SUM(Total_Sales_Qty)) OVER(ORDER BY SUM(Total_Sales_Qty)) AS [Last Value]
	FROM A_SALES_BY_YEAR sby
	--WHERE Sales_Year = 2023
	GROUP BY Sales_Year, Product, Product_Type

	-- Adding the RANGE BETWEEN UNBOUNDED clause
	-- Notice the CURRENT ROW, UNBOUNDED PRECEDING and UNBOUNDED FOLLOWING clauses
	SELECT 
		Sales_Year,
		Product,
		Product_Type,
		SUM(Total_Sales_Qty) AS [Annual Sales],
		FIRST_VALUE(SUM(Total_Sales_Qty)) OVER(ORDER BY SUM(Total_Sales_Qty) RANGE BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS [First Value],
		LAST_VALUE(SUM(Total_Sales_Qty)) OVER(ORDER BY SUM(Total_Sales_Qty)  RANGE BETWEEN CURRENT ROW AND UNBOUNDED FOLLOWING) AS [Last Value]
	FROM A_SALES_BY_YEAR sby
	--WHERE Sales_Year = 2023
	GROUP BY Sales_Year, Product, Product_Type
END