--SEARCH
CREATE OR ALTER PROCEDURE udpFindCoursesByCareer(@Pk_Career AS INT)
AS 
BEGIN 
	SELECT * FROM CareerCourses cc
		JOIN Course c ON  cc.Fk_Course = c.Pk_Course
	WHERE Fk_Career = @Pk_Career;
END

--INSERT COURSE AND THEN ADD IT TO CAREER
CREATE OR ALTER PROCEDURE udpAddCourseToCareer(
	@Code AS VARCHAR(50), 
	@Name AS VARCHAR(50), 
	@Credits AS INT, 
	@WeeklyHours AS INT, 
	@Pk_Career AS INT, 
	@Year AS INT, 
	@Cycle AS INT
)
AS 
BEGIN
	DECLARE @RowsAffected AS INT;
	DECLARE @Pk_CourseAdded AS INT;
	
	INSERT INTO [dbo].Course VALUES(@Code, @Name, @Credits, @WeeklyHours);

	SET @RowsAffected = @@ROWCOUNT;
	IF (@RowsAffected = 0)
		 THROW 51000, 'Course was not added', 1;  
	ELSE
		SET @Pk_CourseAdded = @@IDENTITY
	
	INSERT INTO [dbo].CareerCourses VALUES (@Pk_CourseAdded, @Pk_Career, @Year, @Cycle);
END

--DELETE
CREATE OR ALTER PROCEDURE udpDeleteCourseFromCareer(@Pk_CareerCourses AS INT)
AS 
BEGIN
	DELETE [dbo].CareerCourses WHERE Pk_CareerCourses = @Pk_CareerCourses;
END