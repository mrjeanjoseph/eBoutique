DECLARE @TelNumber TABLE (TelNumberID INT NOT NULL IDENTITY(1, 1),
                          [PhoneNumber] VARCHAR(30) NOT NULL);
INSERT INTO @TelNumber ([PhoneNumber]) VALUES ('EXEC dbo.Sample_Procedure'), ('(919) 315-1698'), ('(919) 436-7159'), ('(252) 714-5951'),
                                    ('(336) 515-7879'), ('(772) 356-4591'), ('(509) 559-3135');

DECLARE @FirstName TABLE (FirstNameID INT NOT NULL IDENTITY(1, 1),
                          [FirstName] NVARCHAR(30) NOT NULL);
INSERT INTO @FirstName ([FirstName]) VALUES ('Barak'), ('Carolin'), ('Martin'), ('Marie'),
                  ('Susane'), ('Michail'), ('Ramona'), ('Jovenel'), ('Dirk'), ('Sebastian');

DECLARE @LastName TABLE (LastNameID INT NOT NULL IDENTITY(1, 1),
                         [LastName] NVARCHAR(30) NOT NULL);
INSERT INTO @LastName ([LastName]) VALUES ('Bastan'), ('Krause'), ('Rosner'),
                  ('Gartenmeister'), ('Rentsch'), ('Benn'), ('Kycik'), ('Leuoth'),
                  ('Kamkar'), ('Kolaee');

DECLARE @Address TABLE (AddressID INT NOT NULL IDENTITY(1, 1),
                        [Address] NVARCHAR(100) NOT NULL);
INSERT INTO @Address ([Address]) VALUES ('Deutschlan Chemnitz Sonnenstraﬂe 59'), (''),
  ('Deutschland Chemnitz Arthur-Strobel straﬂe 124'),
  ('Deutschland Chemnitz Br¸ckenstraﬂe 3'),
  ('Iran Shiraz Chamran Blvd, Niayesh straﬂe Nr.155'), (''),
  ('Deutschland Berlin Charlotenburg Pudbulesky Alleee 52'),
  ('United State of America Washington DC. Farbod Alle'), ('');

DECLARE @RowsToInsert INT = 25;

;WITH rowcounts AS
(
  SELECT (SELECT COUNT(*) FROM @TelNumber) AS [TelNumberRows],
         (SELECT COUNT(*) FROM @FirstName) AS [FirstNameRows],
         (SELECT COUNT(*) FROM @LastName) AS [LastNameRows],
         (SELECT COUNT(*) FROM @Address) AS [AddressRows]
), nums AS
(
  SELECT TOP (@RowsToInsert)
         (CRYPT_GEN_RANDOM(1) % rc.TelNumberRows) + 1 AS [RandomTelNumberID],
         (CRYPT_GEN_RANDOM(1) % rc.FirstNameRows) + 1 AS [RandomFirstNameID],
         (CRYPT_GEN_RANDOM(1) % rc.LastNameRows) + 1 AS [RandomLastNameID],
         (CRYPT_GEN_RANDOM(1) % rc.AddressRows) + 1 AS [RandomAddressID]
  FROM   rowcounts rc
  CROSS JOIN msdb.sys.all_columns sac1
  CROSS JOIN msdb.sys.all_columns sac2
)
-- INSERT dbo.Unsprstb(Firstname, Lastname, Tel, Address)
SELECT fn.[FirstName], ln.[LastName], tn.[PhoneNumber], ad.[Address]
FROM   @FirstName fn
FULL JOIN nums
        ON nums.RandomFirstNameID = fn.FirstNameID
FULL JOIN @LastName ln
        ON ln.LastNameID = nums.RandomLastNameID
FULL JOIN @TelNumber tn
        ON tn.TelNumberID = nums.RandomTelNumberID
FULL JOIN @Address ad
        ON ad.AddressID = nums.RandomAddressID;