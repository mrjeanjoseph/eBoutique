--Calculate Values with String Functions
USE TSFP
BEGIN -- String Manipulation
	
	--Taking a look at the code
	SELECT Product_code, Product_Description
	FROM D_Product

	SELECT -- Locate where the space starts
	CHARINDEX(' ', Product_Description)
	FROM D_Product

	SELECT -- Wrap the STUFF function around it
	STUFF(Product_Description, 
	CHARINDEX(' ', Product_Description),1,'&')
	FROM D_Product

	-- Replacing the remaining spaces with hyphen using the replace function
	SELECT -- Wrap the REPLACE function around it
	REPLACE(STUFF(Product_Description,
	CHARINDEX(' ', Product_Description),1,'&'),
	' ', '-')
	FROM D_Product
	
	SELECT --Noticing a problem some of the keywords
	REPLACE(STUFF(Product_Description,
	CHARINDEX(' ', Product_Description),1,'&'),
	' ', '-')
	FROM D_Product
	WHERE Product_Description LIKE '%light%'

	SELECT --Address the color options with the REPLACE statement
	REPLACE(REPLACE(
	REPLACE(STUFF(Product_Description,
	CHARINDEX(' ', Product_Description),1,'&'),
	' ', '-'),'Light-Blue','LightBlue'),'Light-Green','LightGreen')
	FROM D_Product
	--WHERE Product_Description LIKE '%green%'
	--WHERE Product_Description LIKE '%blue%'
	
	CREATE OR ALTER VIEW v_Product_Base
	AS
	SELECT --Move the result to a view
	Product_Code,
	REPLACE(REPLACE(
	REPLACE(STUFF(Product_Description,
	CHARINDEX(' ', Product_Description),1,'&'),
	' ', '-'),'Light-Blue','LightBlue'),
	'Light-Green','LightGreen') [Product_Description]
	FROM D_Product

	SELECT * FROM [dbo].[v_Product_Base]

END GO;

BEGIN --Cutting out product attributes

	SELECT -- Analyzing the data
	Product_code,
	Product_Description
	FROM v_Product_Base

	SELECT --Since we know the length of our first string
	LEFT(Product_Description, 13)
	FROM v_Product_Base

	SELECT --We can do that same with the RIGHT function
	RIGHT(Product_Description, 6)
	FROM v_Product_Base --Not too bright are we?

	SELECT --We also have substring
	SUBSTRING(Product_Description, 15,6)
	FROM v_Product_Base

	SELECT --We instead will use the STRING_SPLIT Function to split the data
	Product_Code,
	value [Attributes]
	FROM v_Product_Base
	CROSS APPLY STRING_SPLIT(Product_Description, '-')
	
	SELECT --Product Code is no longer unique, thereby need new primary key
	Product_Code,
	ROW_NUMBER() OVER(PARTITION BY Product_Code ORDER BY Product_Code),
	value [Attributes]
	FROM v_Product_Base
	CROSS APPLY STRING_SPLIT(Product_Description, '-')

	SELECT --Filtering out each of the attributes
	Product_Code,
	ROW_NUMBER() OVER(PARTITION BY Product_Code ORDER BY Product_Code) [AttributeID],
	CASE ROW_NUMBER() OVER(PARTITION BY Product_Code ORDER BY Product_Code)
		WHEN 1 THEN 'Season'
		WHEN 2 THEN 'Product'
		WHEN 3 THEN 'Type'
		WHEN 4 THEN 'Color'
		WHEN 5 THEN 'Size'
		ELSE 'ERROR' END [Attribute_Name],
	VALUE [Attributes]
	FROM v_Product_Base
	CROSS APPLY STRING_SPLIT(Product_Description, '-')

	
	SELECT 
		Product_Code,
		MAX(CASE WHEN [AttributeID] = 1 THEN [Attributes] ELSE NULL END) [Season],
		MAX(CASE WHEN [AttributeID] = 2 THEN [Attributes] ELSE NULL END) [Product],
		MAX(CASE WHEN [AttributeID] = 3 THEN [Attributes] ELSE NULL END) [Type],
		MAX(CASE WHEN [AttributeID] = 4 THEN [Attributes] ELSE NULL END) [Color],
		MAX(CASE WHEN [AttributeID] = 5 THEN [Attributes] ELSE NULL END) [Size]		
	FROM(
		SELECT --Pivoting the column using CASE Statment
		Product_Code,
		ROW_NUMBER() OVER(PARTITION BY Product_Code ORDER BY Product_Code) [AttributeID],
		CASE ROW_NUMBER() OVER(PARTITION BY Product_Code ORDER BY Product_Code)
			WHEN 1 THEN 'Season'
			WHEN 2 THEN 'Product'
			WHEN 3 THEN 'Type'
			WHEN 4 THEN 'Color'
			WHEN 5 THEN 'Size'
			ELSE 'ERROR' END [Attribute_Name],
		VALUE [Attributes]
		FROM v_Product_Base
		CROSS APPLY STRING_SPLIT(Product_Description, '-')) [Split]
	GROUP BY Product_Code --Rows return is the same as the original query

	--Materializing the result into a new table	
	SELECT 
		Product_Code,
		MAX(CASE WHEN [AttributeID] = 1 THEN [Attributes] ELSE NULL END) [Season],
		MAX(CASE WHEN [AttributeID] = 2 THEN [Attributes] ELSE NULL END) [Product],
		MAX(CASE WHEN [AttributeID] = 3 THEN [Attributes] ELSE NULL END) [Type],
		MAX(CASE WHEN [AttributeID] = 4 THEN [Attributes] ELSE NULL END) [Color],
		MAX(CASE WHEN [AttributeID] = 5 THEN [Attributes] ELSE NULL END) [Size]
	INTO D_Product_Attributes2
	FROM(
		SELECT --Pivoting the column using CASE Statment
		Product_Code,
		ROW_NUMBER() OVER(PARTITION BY Product_Code ORDER BY Product_Code) [AttributeID],
		CASE ROW_NUMBER() OVER(PARTITION BY Product_Code ORDER BY Product_Code)
			WHEN 1 THEN 'Season'
			WHEN 2 THEN 'Product'
			WHEN 3 THEN 'Type'
			WHEN 4 THEN 'Color'
			WHEN 5 THEN 'Size'
			ELSE 'ERROR' END [Attribute_Name],
		VALUE [Attributes]
		FROM v_Product_Base
		CROSS APPLY STRING_SPLIT(Product_Description, '-')) [Split]
	GROUP BY Product_Code --Rows return is the same as the original query

	SELECT --Taking a look at the sales table
		TOP (1000) *
	FROM F_Sales f

	SELECT --Looking the total sales
		SUM(Sales_Qty)
	FROM F_Sales f

	SELECT --Joining in the product attribute table
		SUM(Sales_Qty)
	FROM F_Sales f
	JOIN D_Product_Attributes2 pa ON f.Product_code = pa.Product_code
	--Both tables above should return the same SUM(Sales_Qty) values meaning
	--Product have found a matching value for every product in both tables

	SELECT -- Grouping by product code
		pa.Product_Code,
		SUM(Sales_Qty) [Sales_QTY]
	FROM F_Sales f
	JOIN D_Product_Attributes2 pa ON f.Product_code = pa.Product_code
	GROUP BY pa.Product_code
	ORDER BY 2 DESC
	
	SELECT -- Analyzing dresses
		pa.product,
		SUM(Sales_Qty) [Sales_QTY]
	FROM F_Sales f
	JOIN D_Product_Attributes2 pa ON f.Product_code = pa.Product_code
	WHERE pa.Product = 'dress'
	GROUP BY pa.Product
	ORDER BY 2 DESC

	SELECT -- Analyzing dress by color
		pa.Color,
		SUM(Sales_Qty) [Sales_QTY]
	FROM F_Sales f
	JOIN D_Product_Attributes2 pa ON f.Product_code = pa.Product_code
	WHERE pa.Product = 'dress'
	GROUP BY pa.Color
	ORDER BY 2 DESC
	
	SELECT -- Analyzing by season
		pa.Season,
		pa.Color,
		SUM(Sales_Qty) [Sales_QTY]
	FROM F_Sales f
	JOIN D_Product_Attributes2 pa ON f.Product_code = pa.Product_code
	WHERE pa.Product = 'dress'
	GROUP BY pa.Season, pa.Color
	ORDER BY 1,3 DESC
	
	SELECT -- While we're at it, adding product type
		pa.Season,
		pa.[Type],
		pa.Color,
		SUM(Sales_Qty) [Sales_QTY]
	FROM F_Sales f
	JOIN D_Product_Attributes2 pa ON f.Product_code = pa.Product_code
	WHERE pa.Product = 'dress'
	GROUP BY pa.Season, pa.[Type], pa.Color
	ORDER BY 1,2,4 DESC
    
END