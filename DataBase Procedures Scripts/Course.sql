CREATE OR ALTER PROCEDURE udpInsertCourse(@Code AS VARCHAR(50), @Name AS VARCHAR(50), @Credits AS INT, @WeeklyHours AS INT)
AS
BEGIN
    INSERT INTO [dbo].Course VALUES (@Code, @Name, @Credits, @WeeklyHours);
END

CREATE OR ALTER PROCEDURE udpModifyCourse(@Pk_Course AS INT, @Code AS VARCHAR(50), @Name AS VARCHAR(50), @Credits AS INT, @WeeklyHours AS INT)
AS
BEGIN
    UPDATE 
        [dbo].Course 
    SET 
        Code = @Code,
        Name = @Name,
        Credits = @Credits,
        WeeklyHours = @WeeklyHours
    WHERE
        Pk_Course = @Pk_Course;
END

CREATE OR ALTER PROCEDURE udpFindCourse(@Pk_Course AS INT = NULL, @Code AS VARCHAR(50) = NULL, @Name AS VARCHAR(50) = NULL, @Credits AS INT = NULL, @WeeklyHours AS INT = NULL)
AS
BEGIN
	SELECT  *
    FROM    [dbo].Course
    WHERE   (@Pk_Course IS NULL OR Pk_Course = @Pk_Course)
            AND (@Code IS NULL OR Code = @Code)
            AND (@Name IS NULL OR Name = @Name)
            AND (@Credits IS NULL OR Credits = @Credits)
            AND (@WeeklyHours IS NULL OR WeeklyHours = @WeeklyHours)
END

CREATE OR ALTER PROCEDURE udpDeleteCourse(@Pk_Course AS INT)
AS
BEGIN
    DELETE 
        [dbo].Course
    WHERE
        Pk_Course = @Pk_Course;
END