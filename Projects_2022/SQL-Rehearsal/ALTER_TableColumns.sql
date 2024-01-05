PRINT 'Hello T-SQL';

SELECT * FROM dbo.Cars;

ALTER TABLE dbo.Cars
ADD ShippingDate DATE NOT NULL DEFAULT GETDATE(),
	PaymentDate DATE NULL

ALTER TABLE dbo.Cars
DROP COLUMN ShippingDate,
			 DeliveryDate

ALTER TABLE dbo.Cars
ALTER COLUMN ShippingDate DATE

