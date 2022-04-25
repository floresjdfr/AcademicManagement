--INSERT NEW Group
CREATE OR ALTER PROCEDURE udpInsertGroup(@Number AS VARCHAR(50), @Schedule AS VARCHAR(50))
AS
BEGIN
	INSERT INTO [dbo].[Group] ([Number], Schedule) VALUES (@Number, @Schedule);
END

--UPDATE
CREATE OR ALTER PROCEDURE udpModifyGroup(
	@Pk_Group AS INT,
	@Fk_Teacher AS INT = NULL,
	@Fk_Cycle AS INT = NULL,
	@Number AS VARCHAR(50) = NULL,
	@Schedule AS VARCHAR(50) = NULL
)
AS
BEGIN
	UPDATE [DBO].[Group] SET 
		[Number] = @Number, 
		Schedule = @Schedule,
		Fk_Teacher = @Fk_Teacher
	WHERE Pk_Group = @Pk_Group;
END

--SEARCH
CREATE OR ALTER PROCEDURE udpFindGroup(
	@Pk_Group AS INT = NULL,
	@Fk_Teacher AS INT = NULL,
	@Fk_Cycle AS INT = NULL,
	@Number AS VARCHAR(50) = NULL,
	@Schedule AS VARCHAR(50) = NULL
)
AS
BEGIN
	SELECT  *, 
	g.[Number] AS GroupNumber,
	c2.[Number] AS CycleNumber
    FROM    [dbo].[Group] g 
    LEFT JOIN [dbo].Teacher t ON g.Fk_Teacher = t.Pk_Teacher 
    LEFT JOIN [dbo].[Cycle] c2  ON g.Fk_Cycle = c2.Pk_Cycle 
    	LEFT JOIN [DBO].CycleState cs ON c2.Fk_CycleState = cs.Pk_CycleState 
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


