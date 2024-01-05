ALTER PROCEDURE [EXE].[GenPhoneNums]
	@mobile_num AS VARCHAR(20) OUTPUT
AS
DECLARE
	@min AS BIGINT,
	@max AS BIGINT	
    BEGIN
		SELECT @min = 1, @max = 99999999
		SELECT @mobile_num = CAST(CAST(((@max + 1) - @min) * Rand() + @min AS BIGINT) AS VARCHAR(15)),
		@mobile_num = '('+ SUBSTRING(@mobile_num, 1, 3) +')' + ' ' + SUBSTRING(@mobile_num, 1, 3) + '-' + RIGHT(@mobile_num,4)
	END
    SELECT @mobile_num
RETURN 0

EXEC [EXE].[GenPhoneNums] 1


