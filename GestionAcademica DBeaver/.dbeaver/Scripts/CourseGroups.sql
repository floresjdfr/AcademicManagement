-- --SEARCH CourseGroups 
-- CREATE OR ALTER PROCEDURE udpFindCareerCourse(@Pk_CareerCourse AS INT)
-- AS
-- BEGIN
-- 	SELECT * FROM [dbo].CourseGroups cc
-- 		JOIN [dbo].Career c ON cc.Fk_Career = c.Pk_Career 
-- 		JOIN [dbo].Course c2 ON cc.Fk_Course = c2.Pk_Course
-- 	WHERE Pk_CourseGroups = @Pk_CareerCourse
-- END

-- --SEARCH CourseGroups by Pk_Career and [Cycle]
--  CREATE OR ALTER PROCEDURE udpFindCourseGroupsByCareerAndCicle(@Fk_Career AS INT, @Cycle AS INT)
-- AS
-- BEGIN
-- 	SELECT * FROM [dbo].CourseGroups cc
-- 		LEFT JOIN [dbo].Career c ON cc.Fk_Career = c.Pk_Career 
-- 		LEFT JOIN [dbo].Course c2 ON cc.Fk_Course = c2.Pk_Course
-- 	WHERE cc.Fk_Career = @Fk_Career AND cc.[Cycle] = @Cycle;
-- END


--SEARCH
CREATE OR ALTER PROCEDURE udpFindGroupsByCourse(@Fk_Course AS INT)
AS 
BEGIN 
	SELECT *,
	c2.[Number] AS CycleNumber,
	t.Name AS TeacherName
	FROM CourseGroups cg
		LEFT JOIN Course c ON  cg.Fk_Course  = c.Pk_Course 
		LEFT JOIN [group] g ON cg.Fk_Group = g.Pk_Group 
		LEFT JOIN Teacher t ON g.Fk_Teacher = t.Pk_Teacher 
		LEFT JOIN [Cycle] c2 ON g.Fk_Cycle = c2.Pk_Cycle 
		LEFT JOIN CycleState cs ON c2.Fk_CycleState = cs.Pk_CycleState 
	WHERE Fk_Course = @Fk_Course;
END

--INSERT COURSE AND THEN ADD IT TO CAREER
CREATE OR ALTER PROCEDURE udpAddGroupToCourse(
	@Fk_Course AS INT, 
	@Fk_Teacher AS INT, 
	@Number AS VARCHAR(50), 
	@Schedule AS VARCHAR(50)
)
AS 
BEGIN
	DECLARE @RowsAffected AS INT;
	DECLARE @Pk_GroupAdded AS INT;
	DECLARE @PK_ActiveCycle AS INT;
	
	SET @PK_ActiveCycle = (SELECT TOP 1 Pk_Cycle FROM CYCLE WHERE Fk_CycleState = 1);
	IF(@PK_ActiveCycle IS NULL) 
		THROW 51000, 'There is not an active cycle', 1;  
	
	INSERT INTO [dbo].[Group] VALUES(@Fk_Teacher, @PK_ActiveCycle, @Number, @Schedule);
	SET @RowsAffected = @@ROWCOUNT;
	IF (@RowsAffected = 0)
		 THROW 51000, 'Group was not added', 1;  
	
	SET @Pk_GroupAdded = @@IDENTITY
	INSERT INTO [dbo].CourseGroups VALUES (@Fk_Course, @Pk_GroupAdded);
END


-- --UPDATE COURSE AND CAREER
-- CREATE OR ALTER PROCEDURE udpUpdateCourseAndCareer(
-- 	@Pk_CourseGroups AS INT,
-- 	@Code AS VARCHAR(50),
-- 	@Name AS VARCHAR(50),
-- 	@Credits AS INT,
-- 	@WeeklyHours AS INT,
-- 	@Year AS INT,
-- 	@Cycle AS INT
-- )
-- AS
-- BEGIN 
	
-- 	DECLARE @Pk_Course AS INT;
	
-- 	SET	@Pk_Course = (SELECT Fk_Course FROM  [dbo].CourseGroups cc WHERE cc.Pk_CourseGroups = @Pk_CourseGroups);
	
-- 	UPDATE [dbo].Course SET Code = @Code, Name = @Name, Credits = @Credits, WeeklyHours = @WeeklyHours WHERE Pk_Course = @Pk_Course;
-- 	IF @@ROWCOUNT = 0
-- 		THROW 51000, 'Course was not updated', 1;  
-- 	ELSE
-- 		UPDATE [dbo].CourseGroups SET Year = @Year, Cycle = @Cycle WHERE Pk_CourseGroups = @Pk_CourseGroups;
-- END



-- --DELETE
-- CREATE OR ALTER PROCEDURE udpDeleteCourseFromCareer(@Pk_CourseGroups AS INT)
-- AS 
-- BEGIN
-- 	DELETE [dbo].CourseGroups WHERE Pk_CourseGroups = @Pk_CourseGroups;
-- END