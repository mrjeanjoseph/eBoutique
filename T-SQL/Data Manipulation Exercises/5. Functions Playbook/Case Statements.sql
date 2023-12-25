USE [TSFP]
--Implementing IF THEN Logic

BEGIN --Product Grouping
	SELECT *
	FROM D_Product_Attributes

	SELECT --Taking a look to see what we have
		product_code,
		Season,
		Product,
		Product_Type,
		Colour,
		Size
	FROM D_Product_Attributes

	--We're categorizing the producst
	SELECT
		Product,
		CASE WHEN Product = 'shirt' THEN 'Tops' END AS [Product Category]
	FROM D_Product_Attributes
		
	--Using the IN statement will take care of more options
	SELECT
		Product,
		CASE 
			WHEN Product IN ('shirt','tshirt','blouse','sweater') 
			THEN 'Tops' 
		END AS [Product Category]
	FROM D_Product_Attributes
	ORDER BY 2 DESC

	--Adding a catch-all
	SELECT
		Product,
		CASE 
			WHEN Product IN ('shirt','tshirt','blouse','sweater') 
			THEN 'Tops'
			ELSE 'Other'
		END AS [Product Category]
	FROM D_Product_Attributes
	ORDER BY 2 DESC
		
	--DON'T DO THAT!
	SELECT
		Product,
		CASE WHEN Product IN ('shirt','tshirt','blouse','sweater') THEN 'Tops' ELSE 'Other' END AS [Product Category],
		CASE WHEN Product IN ('trouser','short','skirt') THEN 'Bottoms' ELSE 'Other' END AS [Product Category]
	FROM D_Product_Attributes
	ORDER BY 3 DESC
		
	--Incorporate both sets instead
	SELECT
		Product,
		CASE 
			WHEN Product IN ('shirt','tshirt','blouse','sweater') THEN 'Tops'
			WHEN Product IN ('trouser','short','skirt') THEN 'Bottoms' 
			ELSE 'Other' 
		END AS [Product Category]
	FROM D_Product_Attributes
	ORDER BY 2 DESC
		
	--We can add more case statement to catch other products
	SELECT
		Product,
		CASE 
			WHEN Product IN ('shirt','tshirt','blouse','sweater') THEN 'Tops'
			WHEN Product IN ('trouser','short','skirt') THEN 'Bottoms'
			WHEN Product IN ('coat','jacket','waistcoat') THEN 'Overwear'
			WHEN Product IN ('hat','bandana','scarf','headscarf','tie') THEN 'Accessories'
			ELSE 'Other'
		END AS [Product Category]
	FROM D_Product_Attributes
	WHERE Product IN ('dress', 'waistcoat')
	ORDER BY 2 DESC

	SELECT * FROM D_Product_Attributes
		
	--Move all of these products into a materialize table
	DROP TABLE IF EXISTS D_PRODUCT_CATEGORY
	SELECT
		product_code,
		Product,
		CASE 
			WHEN Product IN ('shirt','tshirt','blouse','sweater') THEN 'Tops'
			WHEN Product IN ('trouser','shorts','skirt') THEN 'Bottoms'
			WHEN Product IN ('coat','jacket','waistcoat') THEN 'Overwear'
			WHEN Product IN ('hat','bandana','scarf','headscarf','tie') THEN 'Accessories'
			ELSE 'Other'
		END AS [Product_Category]
	INTO D_PRODUCT_CATEGORY
	FROM D_Product_Attributes

	SELECT * FROM D_PRODUCT_CATEGORY WHERE Product_Category = 'Other' --only returns dress
END GO;

BEGIN -- Product Grouping Gender
	--Getting Started
	SELECT * FROM D_PRODUCT_CATEGORY
	
	SELECT -- Checking the first logic for dress
		product_code,
		Product,
		[Product_Category],
		CASE WHEN Product = 'dress' THEN 'Female' ELSE 'Unisex' END AS [Product_Gender]
	FROM D_PRODUCT_CATEGORY

	SELECT -- 
		product_code,
		Product,
		[Product_Category],
		CASE 
			WHEN Product = 'dress' THEN 'Female' 
			WHEN [Product_Category] IN ('Accessories','Bottoms','Overwear') AND LEFT(product_code,1) = 1 THEN 'Male'
			WHEN [Product_Category] IN ('Accessories','Bottoms','Overwear') AND LEFT(product_code,1) = 9 THEN 'Female'
			WHEN [Product_Category] = 'Tops' AND LEFT(product_code,1) = 9 OR Product = 'blouse' THEN 'Female'
			WHEN [Product_Category] = 'Tops' THEN 'Male'
			ELSE 'Unisex'
		END AS [Product_Gender]
	FROM D_PRODUCT_CATEGORY
	WHERE Product_Category = 'Tops'

	
	SELECT -- 
		product_code,
		Product,
		[Product_Category],
		CASE 
			WHEN Product = 'dress' THEN 'Female' 
			WHEN [Product_Category] IN ('Accessories','Bottoms','Overwear') AND LEFT(product_code,1) = 1 THEN 'Male'
			WHEN [Product_Category] IN ('Accessories','Bottoms','Overwear') AND LEFT(product_code,1) = 9 THEN 'Female'
			WHEN [Product_Category] = 'Tops' AND LEFT(product_code,1) = 9 OR Product = 'blouse' THEN 'Female'
			WHEN [Product_Category] = 'Tops' THEN 'Male'
			ELSE 'Unisex'
		END AS [Product_Gender]
	FROM D_PRODUCT_CATEGORY
	WHERE CASE WHEN Product = 'dress' THEN 'Female' 
			WHEN [Product_Category] IN ('Accessories','Bottoms','Overwear') AND LEFT(product_code,1) = 1 THEN 'Male'
			WHEN [Product_Category] IN ('Accessories','Bottoms','Overwear') AND LEFT(product_code,1) = 9 THEN 'Female'
			WHEN [Product_Category] = 'Tops' AND LEFT(product_code,1) = 9 OR Product = 'blouse' THEN 'Female'
			WHEN [Product_Category] = 'Tops' THEN 'Male'
			ELSE 'Unisex' END = 'Unisex'

	DROP TABLE IF EXISTS D_PRODUCT_GENDER
	SELECT -- Moving all of that into a materialize table
		product_code,
		Product,
		[Product_Category],
		CASE 
			WHEN Product = 'dress' THEN 'Female' 
			WHEN [Product_Category] IN ('Accessories','Bottoms','Overwear') AND LEFT(product_code,1) = 1 THEN 'Male'
			WHEN [Product_Category] IN ('Accessories','Bottoms','Overwear') AND LEFT(product_code,1) = 9 THEN 'Female'
			WHEN [Product_Category] = 'Tops' AND LEFT(product_code,1) = 9 OR Product = 'blouse' THEN 'Female'
			WHEN [Product_Category] = 'Tops' THEN 'Male'
			ELSE 'Unisex'
		END AS [Product_Gender]
		INTO D_PRODUCT_GENDER
	FROM D_PRODUCT_CATEGORY


	SELECT * FROM D_PRODUCT_GENDER WHERE Product_Gender = 'Unisex'
	SELECT * FROM D_Product_Attributes WHERE Product = 'waistcost'
	UPDATE D_Product_Attributes SET Product = 'waistcoat' WHERE Product = 'waistcost'

END GO;

BEGIN -- Intelligent Measures
	SELECT * FROM D_PRODUCT_CATEGORY
	SELECT * FROM Unit_Prices 

	--Create a measure that will calculate the total sales value for product category
	SELECT 
		s.Product_code,
		SUM(s.Sales_Qty * p.Unit_Price) [Sales_Value]
	FROM F_Sales s 
	JOIN Unit_Prices p ON s.Product_code = p.Product_Code
	GROUP BY s.Product_code

	SELECT -- Join in Product_Category Table
		c.Product_Category,
		SUM(s.Sales_Qty * p.Unit_Price) [Sales_Value]
	FROM F_Sales s 
	JOIN Unit_Prices p ON s.Product_code = p.Product_Code
	JOIN D_PRODUCT_CATEGORY c ON s.Product_code = c.product_code
	GROUP BY s.Product_code, c.Product_Category
	--There's an error - No dataset returned
	
	SELECT -- Case the sales value
		c.Product_Category,
		SUM(CASE WHEN c.Product_Category = 'Accessories' THEN s.Sales_Qty * p.Unit_Price ELSE 0 END) [Accessories_Value]
	FROM F_Sales s 
	JOIN Unit_Prices p ON s.Product_code = p.Product_Code
	JOIN D_PRODUCT_CATEGORY c ON s.Product_code = c.product_code
	GROUP BY c.Product_Category
	--Error persists - No dataset returned

	
	SELECT -- Case the remaining categories
		c.Product_Category,
		SUM(CASE WHEN c.Product_Category = 'Accessories' THEN s.Sales_Qty * p.Unit_Price ELSE 0 END) [Accessories_Value],
		SUM(CASE WHEN c.Product_Category = 'Bottoms' THEN s.Sales_Qty * p.Unit_Price ELSE 0 END) [Bottoms_Value],
		SUM(CASE WHEN c.Product_Category = 'Other' THEN s.Sales_Qty * p.Unit_Price ELSE 0 END) [Bottoms_Value],
		SUM(CASE WHEN c.Product_Category = 'Tops' THEN s.Sales_Qty * p.Unit_Price ELSE 0 END) [Bottoms_Value],
		SUM(CASE WHEN c.Product_Category = 'Overwear' THEN s.Sales_Qty * p.Unit_Price ELSE 0 END) [Bottoms_Value]
	FROM F_Sales s 
	JOIN Unit_Prices p ON s.Product_code = p.Product_Code
	JOIN D_PRODUCT_CATEGORY c ON s.Product_code = c.product_code
	GROUP BY c.Product_Category
	--No dataset returned
	
	SELECT -- Final output
		b.brand,
		SUM(CASE WHEN c.Product_Category = 'Accessories' THEN s.Sales_Qty * p.Unit_Price ELSE 0 END) [Accessories_Value],
		SUM(CASE WHEN c.Product_Category = 'Bottoms' THEN s.Sales_Qty * p.Unit_Price ELSE 0 END) [Bottoms_Value],
		SUM(CASE WHEN c.Product_Category = 'Other' THEN s.Sales_Qty * p.Unit_Price ELSE 0 END) [Bottoms_Value],
		SUM(CASE WHEN c.Product_Category = 'Tops' THEN s.Sales_Qty * p.Unit_Price ELSE 0 END) [Bottoms_Value],
		SUM(CASE WHEN c.Product_Category = 'Overwear' THEN s.Sales_Qty * p.Unit_Price ELSE 0 END) [Bottoms_Value]
	FROM F_Sales s 
	JOIN Unit_Prices p ON s.Product_code = p.Product_Code
	JOIN D_PRODUCT_CATEGORY c ON s.Product_code = c.product_Code
	JOIN D_BRAND b ON s.Brand_Code = b.Brand_Code
	GROUP BY b.brand
	--No dataset returned
END