--SEARCH CareerCourses 
CREATE OR ALTER PROCEDURE udpFindCareerCourse(@Pk_CareerCourse AS INT)
AS
BEGIN
	SELECT * FROM [dbo].CareerCourses cc
		JOIN [dbo].Career c ON cc.Fk_Career = c.Pk_Career 
		JOIN [dbo].Course c2 ON cc.Fk_Course = c2.Pk_Course
	WHERE Pk_CareerCourses = @Pk_CareerCourse
END


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


--UPDATE COURSE AND CAREER
CREATE OR ALTER PROCEDURE udpUpdateCourseAndCareer(
	@Pk_CareerCourses AS INT,
	@Code AS VARCHAR(50),
	@Name AS VARCHAR(50),
	@Credits AS INT,
	@WeeklyHours AS INT,
	@Year AS INT,
	@Cycle AS INT
)
AS
BEGIN 
	
	DECLARE @Pk_Course AS INT;
	
	SET	@Pk_Course = (SELECT Fk_Course FROM  [dbo].CareerCourses cc WHERE cc.Pk_CareerCourses = @Pk_CareerCourses);
	
	UPDATE [dbo].Course SET Code = @Code, Name = @Name, Credits = @Credits, WeeklyHours = @WeeklyHours WHERE Pk_Course = @Pk_Course;
	IF @@ROWCOUNT = 0
		THROW 51000, 'Course was not updated', 1;  
	ELSE
		UPDATE [dbo].CareerCourses SET Year = @Year, Cycle = @Cycle WHERE Pk_CareerCourses = @Pk_CareerCourses;
END



--DELETE
CREATE OR ALTER PROCEDURE udpDeleteCourseFromCareer(@Pk_CareerCourses AS INT)
AS 
BEGIN
	DELETE [dbo].CareerCourses WHERE Pk_CareerCourses = @Pk_CareerCourses;
END