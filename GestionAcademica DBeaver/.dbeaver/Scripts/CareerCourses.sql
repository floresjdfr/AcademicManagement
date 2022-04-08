--SEARCH
CREATE OR ALTER PROCEDURE udpGetCoursesByCareer(@Pk_Career AS INT)
AS 
BEGIN 
	SELECT * FROM CareerCourses cc
		JOIN Course c ON  cc.Fk_Course = c.Pk_Course
	WHERE Fk_Career = @Pk_Career;
END

--INSERT
CREATE OR ALTER PROCEDURE udpAddCourseToCareer(@Pk_Course AS INT, @Pk_Career AS INT, @Year AS INT, @Cycle AS INT)
AS 
BEGIN
	INSERT INTO [dbo].CareerCourses VALUES (@Pk_Course, @Pk_Career, @Year, @Cycle);
END

--DELETE
CREATE OR ALTER PROCEDURE udpDeleteCourseFromCareer(@Pk_CareerCourses AS INT)
AS 
BEGIN
	DELETE [dbo].CareerCourses WHERE Pk_CareerCourses = @Pk_CareerCourses;
END