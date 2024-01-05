SELECT * FROM dbo.Countries;

CREATE INDEX ix_countries ON Countries (CountryID)

SELECT * FROM dbo.Countries WHERE CountryID = 123

DROP INDEX ix_countries ON Countries;
ALTER INDEX ix_countries ON Countries DISABLE;
ALTER INDEX ix_countries ON Countries REBUILD;

--Clustered, Unique and Sorted Indexes

SELECT * FROM dbo.Cars;
CREATE CLUSTERED INDEX ix_cl_cars ON dbo.Cars(CarsID,CustomerID)
--Cannot create more than one clustered index

SELECT * FROM dbo.Customers;
CREATE CLUSTERED INDEX ix_cl_Cust ON dbo.Customers(CustomerID, PhoneNumber)
--Cannot create more than one clustered index

CREATE UNIQUE INDEX up_cust_ix_email ON dbo.Customers(EmailAddress)
--Works

CREATE INDEX ix_eid_desc ON dbo.Customers (CustomerID Desc)

ALTER INDEX up_cust_ix_email ON dbo.Customers REBUILD;