CREATE OR ALTER PROCEDURE udpInsertCareer(@Code AS VARCHAR(50), @CareerName as VARCHAR(50), @DegreeName as VARCHAR(50))
AS
BEGIN
    INSERT INTO [dbo].Career VALUES (@Code, @CareerName, @DegreeName);
    SELECT SCOPE_IDENTITY();
END

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

CREATE OR ALTER PROCEDURE udpFindCareer(@Pk_Career AS INT = NULL, @Code AS VARCHAR(50) = NULL, @CareerName as VARCHAR(50) = NULL, @DegreeName as VARCHAR(50) = NULL)
AS
BEGIN
    SELECT  *
    FROM    [dbo].Career
    WHERE   (@Pk_Career IS NULL OR Pk_Career = @Pk_Career)
            AND (@Code IS NULL OR Code = @Code)
            AND (@CareerName IS NULL OR CareerName = @CareerName)
            AND (@DegreeName IS NULL OR DegreeName = @DegreeName)
END

CREATE OR ALTER PROCEDURE udpDeleteCareer(@Pk_Career AS INT)
AS
BEGIN
    DELETE 
        [dbo].Career
    WHERE
        Pk_Career = @Pk_Career;
END