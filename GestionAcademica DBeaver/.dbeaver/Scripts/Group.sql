--INSERT NEW Group
CREATE OR ALTER PROCEDURE udpInsertGroup(@Number AS VARCHAR(50), @Schedule AS VARCHAR(50))
AS
BEGIN
	INSERT INTO [dbo].[Group] ([Number], Schedule) VALUES (@Number, @Schedule);
END

--UPDATE
--MODIFYTYPE => 1 = Number and Schedule, 2 = Teacher, 3 = Cycle
CREATE OR ALTER PROCEDURE udpModifyGroup(
	@Pk_Group AS INT,
	@Fk_Teacher AS INT = NULL,
	@Fk_Cycle AS INT = NULL,
	@Number AS VARCHAR(50) = NULL,
	@Schedule AS VARCHAR(50) = NULL,
	@ModifyType AS INT
)
AS
BEGIN
	IF (@ModifyType = 1)
	BEGIN 
		UPDATE [DBO].[Group] SET [Number] = @Number, Schedule = @Schedule WHERE Pk_Group = @Pk_Group;
	END
	IF(@ModifyType = 2)
	BEGIN
		UPDATE [dbo].[Group] 
		SET 
			Fk_Teacher = @Fk_Teacher
		WHERE Pk_Group  = @Pk_Group;
	END
	IF(@ModifyType = 3)
	BEGIN
		UPDATE [dbo].[Group] 
		SET 
			Fk_Cycle = @Fk_Cycle
		WHERE Pk_Group  = @Pk_Group;
	END
END

--SEARCH
CREATE OR ALTER PROCEDURE udpFindGroup(
	@Pk_Group AS INT,
	@Fk_Teacher AS INT = NULL,
	@Fk_Cycle AS INT = NULL,
	@Number AS VARCHAR(50) = NULL,
	@Schedule AS VARCHAR(50) = NULL
)
AS
BEGIN
    SELECT  * 
    FROM    [dbo].[Group] g 
    JOIN [dbo].Teacher t ON g.Fk_Teacher = t.Pk_Teacher 
    JOIN [dbo].[Cycle] c2  ON g.Fk_Cycle = c2.Pk_Cycle 
    WHERE   (@Pk_Group IS NULL OR g.Pk_Group = @Pk_Group) 
            AND (@Fk_Teacher IS NULL OR g.Fk_Teacher = @Fk_Teacher) 
            AND (@Fk_Cycle IS NULL OR g.Fk_Cycle = @Fk_Cycle) 
            AND (@Number IS NULL OR g.[Number] = @Number) 
            AND (@Schedule IS NULL OR g.Schedule = @Schedule);
END

--DELETE
CREATE OR ALTER PROCEDURE udpDeleteGroup(@Pk_Group AS INT)
AS
BEGIN
    DELETE 
        [dbo].[Group]
    WHERE
        Pk_Group = @Pk_Group;
END


