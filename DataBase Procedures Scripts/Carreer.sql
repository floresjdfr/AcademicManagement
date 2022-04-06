CREATE OR ALTER PROCEDURE udpInsertCareer(@Code AS VARCHAR(50), @CareerName as VARCHAR(50), @DegreeName as VARCHAR(50))
AS
BEGIN
    INSERT INTO [dbo].Career VALUES (@Code, @CareerName, @DegreeName);
END
GO

CREATE OR ALTER PROCEDURE udpModifyCareer(@Pk_Career AS INT, @Code AS VARCHAR(50), @CareerName as VARCHAR(50), @DegreeName as VARCHAR(50))
AS
BEGIN
    UPDATE 
        [dbo].Career 
    SET 
        Code = @Code, 
        CareerName = @CareerName, 
        DegreeName = @DegreeName
    WHERE
        Pk_Career = @Pk_Career;
END
GO

CREATE OR ALTER PROCEDURE udpFindCareer(@Pk_Career AS INT = NULL, @Code AS VARCHAR(50) = NULL, @CareerName as VARCHAR(50) = NULL, @DegreeName as VARCHAR(50) = NULL)
AS
BEGIN
	IF (@Pk_Career IS NOT NULL)
		SELECT * FROM [dbo].Career WHERE Pk_Career = @Pk_Career;
	ELSE
		IF (@Code IS NOT NULL)
			SELECT * FROM [dbo].Career WHERE Code = @Code
		ELSE
			IF (@CareerName IS NOT NULL)
				SELECT * FROM [dbo].Career WHERE CareerName = @CareerName;
			ELSE
				IF (@DegreeName IS NOT NULL)
					SELECT * FROM [dbo].Career WHERE DegreeName = @DegreeName;
				ELSE
					SELECT * FROM [dbo].Career;
END
GO

CREATE OR ALTER PROCEDURE udpDeleteCareer(@Pk_Career AS INT)
AS
BEGIN
    DELETE 
        [dbo].Career
    WHERE
        Pk_Career = @Pk_Career;
END
GO