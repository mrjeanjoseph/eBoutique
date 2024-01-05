--Combining Queries with COALESCE
USE [TSFP]
BEGIN -- 
	SELECT --Looking at the Promo code
		Customer_code,
		Sales_date,
		Promo_Code
	FROM Promo
	
	
	SELECT -- Getting the discount value from promo code
		Customer_code,
		Sales_date,
		Promo_Code,
		CAST(RIGHT(Promo_Code,2) AS MONEY) [Discount]
	FROM Promo
	
	SELECT -- Getting the sales value
		Customer_code,
		SUM(s.Sales_Qty * p.Unit_Price) [Sales_Value],
		Promo_Code
	FROM F_Sales s 
	JOIN Unit_Prices p ON s.Product_code = p.Product_Code
	GROUP BY Customer_Code
	
	SELECT -- Then join in the Promo table using inner join
		s.Customer_Code,
		SUM(s.Sales_Qty * p.Unit_Price) [Sales_Value]
	FROM F_Sales s 
	JOIN Unit_Prices p ON s.Product_code = p.Product_Code
	JOIN Promo pr ON s.Customer_Code = pr.Customer_code
	GROUP BY s.Customer_Code
	
	SELECT -- Join in the discount column
		s.Customer_Code,
		CAST(RIGHT(pr.Promo_Code,2) AS MONEY) [Discount],
		SUM(s.Sales_Qty * p.Unit_Price) [Sales_Value]
	FROM F_Sales s 
	JOIN Unit_Prices p ON s.Product_code = p.Product_Code
	JOIN Promo pr ON s.Customer_Code = pr.Customer_code
	GROUP BY s.Customer_Code, CAST(RIGHT(pr.Promo_Code,2) AS MONEY)
	
	SELECT -- Join in the discount column
		s.Customer_Code,
		CAST(RIGHT(pr.Promo_Code,2) AS MONEY) [Discount],
		SUM(s.Sales_Qty * p.Unit_Price) [Sales_Value]
	FROM F_Sales s 
	JOIN Unit_Prices p ON s.Product_code = p.Product_Code
	LEFT OUTER JOIN Promo pr ON s.Customer_Code = pr.Customer_code
	WHERE s.Customer_Code IS NOT NULL
	GROUP BY s.Customer_Code, CAST(RIGHT(pr.Promo_Code,2) AS MONEY) 

	--Joining both Login into one
	SELECT yes.Customer_Code [Customer],
		yes.Discount_Sales_Value [Discount_Sales_Value],
		[no].Sales_Value [Sales_Value]
	FROM (
		SELECT s.Customer_Code,
			CAST(RIGHT(pr.Promo_Code,2) AS MONEY) [Discount],
			SUM(s.Sales_Qty * p.Unit_Price) - CAST(RIGHT(pr.Promo_Code,2) AS MONEY) [Discount_Sales_Value]
		FROM F_Sales s 
		JOIN Unit_Prices p ON s.Product_code = p.Product_Code
		JOIN Promo pr ON s.Customer_Code = pr.Customer_code
		GROUP BY s.Customer_Code, CAST(RIGHT(pr.Promo_Code,2) AS MONEY)) [yes]
	FULL OUTER JOIN (
		SELECT s.Customer_Code,
			CAST(RIGHT(pr.Promo_Code,2) AS MONEY) [Discount],
			SUM(s.Sales_Qty * p.Unit_Price) [Sales_Value]
		FROM F_Sales s 
		JOIN Unit_Prices p ON s.Product_code = p.Product_Code
		LEFT OUTER JOIN Promo pr ON s.Customer_Code = pr.Customer_code
		WHERE s.Customer_Code IS NOT NULL
		GROUP BY s.Customer_Code, CAST(RIGHT(pr.Promo_Code,2) AS MONEY)) [no]
	ON yes.Customer_Code = no.Customer_Code
END GO;

BEGIN -- Adding the Coalesce Function

	--Complication switching between the yes/no alias Customer_Code
	SELECT yes.Customer_Code [Customer],
		yes.Discount_Sales_Value [Discount_Sales_Value],
		[no].Sales_Value [Sales_Value]
	FROM (
		SELECT s.Customer_Code,
			CAST(RIGHT(pr.Promo_Code,2) AS MONEY) [Discount],
			SUM(s.Sales_Qty * p.Unit_Price) - CAST(RIGHT(pr.Promo_Code,2) AS MONEY) [Discount_Sales_Value]
		FROM F_Sales s 
		JOIN Unit_Prices p ON s.Product_code = p.Product_Code
		JOIN Promo pr ON s.Customer_Code = pr.Customer_code
		GROUP BY s.Customer_Code, CAST(RIGHT(pr.Promo_Code,2) AS MONEY)) [yes]
	FULL OUTER JOIN (
		SELECT s.Customer_Code,
			CAST(RIGHT(pr.Promo_Code,2) AS MONEY) [Discount],
			SUM(s.Sales_Qty * p.Unit_Price) [Sales_Value]
		FROM F_Sales s 
		JOIN Unit_Prices p ON s.Product_code = p.Product_Code
		LEFT OUTER JOIN Promo pr ON s.Customer_Code = pr.Customer_code
		WHERE s.Customer_Code IS NOT NULL
		GROUP BY s.Customer_Code, CAST(RIGHT(pr.Promo_Code,2) AS MONEY)) [no]
	ON yes.Customer_Code = no.Customer_Code

	--Pass in a CASE Statement is one option
	SELECT CASE WHEN [no].Customer_Code IS NULL THEN [yes].Customer_Code ELSE [no].Customer_Code END [Customer],
		yes.Discount_Sales_Value [Discount_Sales_Value],
		[no].Sales_Value [Sales_Value]
	FROM (
		SELECT s.Customer_Code,
			CAST(RIGHT(pr.Promo_Code,2) AS MONEY) [Discount],
			SUM(s.Sales_Qty * p.Unit_Price) - CAST(RIGHT(pr.Promo_Code,2) AS MONEY) [Discount_Sales_Value]
		FROM F_Sales s 
		JOIN Unit_Prices p ON s.Product_code = p.Product_Code
		JOIN Promo pr ON s.Customer_Code = pr.Customer_code
		GROUP BY s.Customer_Code, CAST(RIGHT(pr.Promo_Code,2) AS MONEY)) [yes]
	FULL OUTER JOIN (
		SELECT s.Customer_Code,
			CAST(RIGHT(pr.Promo_Code,2) AS MONEY) [Discount],
			SUM(s.Sales_Qty * p.Unit_Price) [Sales_Value]
		FROM F_Sales s 
		JOIN Unit_Prices p ON s.Product_code = p.Product_Code
		LEFT OUTER JOIN Promo pr ON s.Customer_Code = pr.Customer_code
		WHERE s.Customer_Code IS NOT NULL
		GROUP BY s.Customer_Code, CAST(RIGHT(pr.Promo_Code,2) AS MONEY)) [no]
	ON yes.Customer_Code = no.Customer_Code
	--This option will get complex fast when there's more than one table involved.

	--Adding the Coalesce funtion to the query / Evaluation Function
	SELECT COALESCE([yes].Customer_Code, [no].Customer_Code) [Customer],
		yes.Discount_Sales_Value [Discount_Sales_Value],
		[no].Sales_Value [Sales_Value]
	FROM (
		SELECT s.Customer_Code,
			CAST(RIGHT(pr.Promo_Code,2) AS MONEY) [Discount],
			SUM(s.Sales_Qty * p.Unit_Price) - CAST(RIGHT(pr.Promo_Code,2) AS MONEY) [Discount_Sales_Value]
		FROM F_Sales s 
		JOIN Unit_Prices p ON s.Product_code = p.Product_Code
		JOIN Promo pr ON s.Customer_Code = pr.Customer_code
		GROUP BY s.Customer_Code, CAST(RIGHT(pr.Promo_Code,2) AS MONEY)) [yes]
	FULL OUTER JOIN (
		SELECT s.Customer_Code,
			CAST(RIGHT(pr.Promo_Code,2) AS MONEY) [Discount],
			SUM(s.Sales_Qty * p.Unit_Price) [Sales_Value]
		FROM F_Sales s 
		JOIN Unit_Prices p ON s.Product_code = p.Product_Code
		LEFT OUTER JOIN Promo pr ON s.Customer_Code = pr.Customer_code
		WHERE s.Customer_Code IS NOT NULL
		GROUP BY s.Customer_Code, CAST(RIGHT(pr.Promo_Code,2) AS MONEY)) [no]
	ON yes.Customer_Code = no.Customer_Code	

	--Clean up the output to include 0 instead of NULLS
	SELECT COALESCE([yes].Customer_Code, [no].Customer_Code) [Customer],
		--USE CASE Statements to capture Nulls and make them 0 
		CASE WHEN yes.Discount_Sales_Value IS NULL THEN 0 ELSE [yes].Discount_Sales_Value END [Discount_Sales_Value],
		CASE WHEN [no].Sales_Value IS NULL THEN 0 ELSE [no].Sales_Value END [Sales_Value]
	FROM (
		SELECT s.Customer_Code,
			CAST(RIGHT(pr.Promo_Code,2) AS MONEY) [Discount],
			SUM(s.Sales_Qty * p.Unit_Price) - CAST(RIGHT(pr.Promo_Code,2) AS MONEY) [Discount_Sales_Value]
		FROM F_Sales s 
		JOIN Unit_Prices p ON s.Product_code = p.Product_Code
		JOIN Promo pr ON s.Customer_Code = pr.Customer_code
		GROUP BY s.Customer_Code, CAST(RIGHT(pr.Promo_Code,2) AS MONEY)) [yes]
	FULL OUTER JOIN (
		SELECT s.Customer_Code,
			CAST(RIGHT(pr.Promo_Code,2) AS MONEY) [Discount],
			SUM(s.Sales_Qty * p.Unit_Price) [Sales_Value]
		FROM F_Sales s 
		JOIN Unit_Prices p ON s.Product_code = p.Product_Code
		LEFT OUTER JOIN Promo pr ON s.Customer_Code = pr.Customer_code
		WHERE s.Customer_Code IS NOT NULL
		GROUP BY s.Customer_Code, CAST(RIGHT(pr.Promo_Code,2) AS MONEY)) [no]
	ON yes.Customer_Code = no.Customer_Code

	--Clean up the output to include 0 instead of NULLS option 2
	SELECT COALESCE([yes].Customer_Code, [no].Customer_Code) [Customer],
		--USE ISNULL as well to capture nulls and make them 0 
		ISNULL(yes.Discount_Sales_Value, 0) [Discount_Sales_Value],
		ISNULL([no].Sales_Value, 0) [Sales_Value]
	FROM (
		SELECT s.Customer_Code,
			CAST(RIGHT(pr.Promo_Code,2) AS MONEY) [Discount],
			SUM(s.Sales_Qty * p.Unit_Price) - CAST(RIGHT(pr.Promo_Code,2) AS MONEY) [Discount_Sales_Value]
		FROM F_Sales s 
		JOIN Unit_Prices p ON s.Product_code = p.Product_Code
		JOIN Promo pr ON s.Customer_Code = pr.Customer_code
		GROUP BY s.Customer_Code, CAST(RIGHT(pr.Promo_Code,2) AS MONEY)) [yes]
	FULL OUTER JOIN (
		SELECT s.Customer_Code,
			CAST(RIGHT(pr.Promo_Code,2) AS MONEY) [Discount],
			SUM(s.Sales_Qty * p.Unit_Price) [Sales_Value]
		FROM F_Sales s 
		JOIN Unit_Prices p ON s.Product_code = p.Product_Code
		LEFT OUTER JOIN Promo pr ON s.Customer_Code = pr.Customer_code
		WHERE s.Customer_Code IS NOT NULL
		GROUP BY s.Customer_Code, CAST(RIGHT(pr.Promo_Code,2) AS MONEY)) [no]
	ON yes.Customer_Code = no.Customer_Code	
END GO;

BEGIN -- Merging multiple tables into one using COALESCE
	
	SELECT -- We don't have access to these tables
		Brand,
		AWSales,
		SSSales
	FROM A_BRAND_PRODUCT_SALES;
	
	SELECT -- We don't have access to these tables
		Brand,
		Sales_2017,
		Sales_2018
	FROM A_BRAND_YEAR_SALES;
	
	SELECT -- We don't have access to these tables
		Brand,
		Mobile_Sales,
		Web_Sales
	FROM A_BRAND_CHANNEL_SALES;

	--Using the COALESCE Function to merge these tables into one
	SELECT
		COALESCE(p.Brand, y.Brand, c.Brand) [Brand],
		p.AWSales,
		p.SSSales,
		y.Sales_2017,
		y.Sales_2018,
		c.Mobile_Sales,
		c.Web_Sales
	FROM A_BRAND_PRODUCT_SALES p
	FULL OUTER JOIN A_BRAND_YEAR_SALES y ON p.Brand = y.Brand
	FULL OUTER JOIN A_BRAND_CHANNEL_SALES c ON p.Brand = c.Brand
END