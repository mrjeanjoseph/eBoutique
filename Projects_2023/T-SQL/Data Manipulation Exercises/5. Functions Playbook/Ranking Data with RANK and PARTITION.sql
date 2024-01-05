--Ranking Data with RANK and PARTITION--
USE [TSFP]

BEGIN --RANK and PARTITION--
	SELECT COUNT(*) FROM [DBO].[A_SALES_BY_YEAR]
	SELECT * FROM [DBO].[A_SALES_BY_YEAR] ORDER BY Sales_Year

	--Custom implementation
	INSERT INTO [DBO].[A_SALES_BY_YEAR]
	SELECT * FROM [DBO].[A_SALES_BY_YEAR]


	--Creating a query using the row number function
	SELECT 
		Sales_Year,
		Total_Sales_Qty,
		ROW_NUMBER() OVER (ORDER BY Total_Sales_Qty) AS [Row Number]
	FROM A_SALES_BY_YEAR sby
	ORDER BY 3,1

	--Amending that query to PARTITION BY Sales_Year
	SELECT 
		Sales_Year,
		Total_Sales_Qty,
		ROW_NUMBER() OVER (PARTITION BY Sales_Year ORDER BY Total_Sales_Qty) AS [Row Number]
	FROM A_SALES_BY_YEAR sby
	ORDER BY 1,3 --Switching the order by column changes the data to order by the given column

	--Moving some of the data to new years
	SELECT TOP 2513 * FROM [DBO].[A_SALES_BY_YEAR] WHERE Sales_Year = 2018
	SELECT COUNT(*) FROM [DBO].[A_SALES_BY_YEAR] WHERE Sales_Year = 2018
	UPDATE TOP (2513) [DBO].[A_SALES_BY_YEAR] SET Sales_Year = 2023 WHERE Sales_Year = 2018
END
GO;

BEGIN -----The Rank Function-------

	--The query to Rank by Total_Sals_Qty
	SELECT 
		Sales_Year,
		Total_Sales_Qty,
		RANK() OVER (PARTITION BY Sales_Year ORDER BY Total_Sales_Qty) AS [Row Number]
	FROM A_SALES_BY_YEAR sby
	ORDER BY 1,3 


	--The query to Rank by Total_Sals_Qty
	SELECT 
		Sales_Year,
		Total_Sales_Qty,
		RANK() OVER (PARTITION BY Sales_Year ORDER BY Total_Sales_Qty) AS [Row Number]
	FROM A_SALES_BY_YEAR sby
	WHERE Sales_Year = 2019
	ORDER BY 1,3

	--Still using RANK
	SELECT 
		Sales_Year,
		Product,
		Colour,
		Total_Sales_Qty,
		RANK() OVER (PARTITION BY Sales_Year ORDER BY Total_Sales_Qty) AS [Row Number]
	FROM A_SALES_BY_YEAR sby
	WHERE Sales_Year = 2019
	ORDER BY 4,3,2,1

	--Now using ROW_NUMBER()
	SELECT 
		Sales_Year,
		Product,
		Colour,
		Total_Sales_Qty,
		ROW_NUMBER() OVER (PARTITION BY Sales_Year ORDER BY Total_Sales_Qty) AS [Row Number]
	FROM A_SALES_BY_YEAR sby
	WHERE Sales_Year = 2019
	ORDER BY 4,3,2,1
END
GO;

BEGIN -----DENSE RANK Function-------

	--using DENSE_RANK()
	SELECT 
		Sales_Year,
		Product,
		Colour,
		Total_Sales_Qty,
		DENSE_RANK() OVER (PARTITION BY Sales_Year ORDER BY Total_Sales_Qty) AS [Row Number]
	FROM A_SALES_BY_YEAR sby
	WHERE Sales_Year = 2023
	ORDER BY 4,3,2,1

	--Aggragating the Total_Sales_Qty column
	SELECT 
		Sales_Year,
		--Product,
		--Colour,
		SUM(Total_Sales_Qty) [Annual Sales],
		DENSE_RANK() OVER (PARTITION BY Sales_Year ORDER BY SUM(Total_Sales_Qty)) AS [Row Number]
	FROM A_SALES_BY_YEAR sby
	--WHERE Sales_Year = 2023
	--ORDER BY 4,3,2,1
	GROUP BY Sales_Year

	--Removing the PARTITION BY Clause - each year returns a row number
	SELECT 
		Sales_Year,
		--Product,
		--Colour,
		SUM(Total_Sales_Qty) [Annual Sales],
		DENSE_RANK() OVER (ORDER BY SUM(Total_Sales_Qty)) AS [Row Number]
	FROM A_SALES_BY_YEAR sby
	--WHERE Sales_Year = 2023
	--ORDER BY 4,3,2,1
	GROUP BY Sales_Year


	--PARTITION BY product column
	SELECT 
		Sales_Year,
		Product, --Added that back
		--Colour,
		SUM(Total_Sales_Qty) [Annual Sales],
		DENSE_RANK() OVER (PARTITION BY Product ORDER BY SUM(Total_Sales_Qty)) AS [Row Number]
	FROM A_SALES_BY_YEAR sby
	--WHERE Sales_Year = 2023
	--ORDER BY 4,3,2,1
	GROUP BY Sales_Year, Product --Product added to the group by clause


	--PARTITION BY Sales_Year column
	SELECT 
		Sales_Year,
		Product, --Added that back
		--Colour,
		SUM(Total_Sales_Qty) [Annual Sales],
		DENSE_RANK() OVER (PARTITION BY Sales_Year ORDER BY SUM(Total_Sales_Qty)DESC) AS [Row Number]
	FROM A_SALES_BY_YEAR sby
	--WHERE Sales_Year = 2023
	--ORDER BY 4,3,2,1
	GROUP BY Sales_Year, Product --Product added to the group by clause
END
GO;


BEGIN -----NTILE Function-------

		--NTILE BY Sales_Year column
	SELECT 
		Sales_Year,
		Product,
		SUM(Total_Sales_Qty) [Annual Sales],
		NTILE(4) OVER (PARTITION BY Sales_Year ORDER BY SUM(Total_Sales_Qty)DESC) AS [NTile Row_Num]
	FROM A_SALES_BY_YEAR sby
	GROUP BY Sales_Year, Product
	--To be tiled in sequence of 4 groups
END GO;