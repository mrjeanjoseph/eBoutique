--Creating a T-SQL Function
CREATE FUNCTION FirstWord(@input VARCHAR(1000))
RETURNS VARCHAR(1000) AS

BEGIN
	DECLARE @output VARCHAR(1000)
	SET @output = SUBSTRING(@input, 0, CASE CHARINDEX(' ', @input)
		WHEN 0 THEN LEN(@input) + 1
		ELSE CHARINDEX(' ', @input)
	END)

	RETURN @output
END
