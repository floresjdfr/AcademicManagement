
--INSERT NEW CYCLE
CREATE OR ALTER PROCEDURE udpInsertCycle(@Fk_CycleState AS INT, @Year AS INT, @Number AS INT, @StartDate AS DATE, @EndDate AS DATE)
AS
BEGIN
	INSERT INTO [dbo].[Cycle] (Fk_CycleState, [Year], [Number], StartDate, EndDate) VALUES(@Fk_CycleState, @Year, @Number, @StartDate, @EndDate);
END

--UPDATE
CREATE OR ALTER PROCEDURE udpModifyCycle(
	@Pk_Cycle AS INT,
	@Fk_CycleState AS INT,
	@Year AS INT,
	@Number AS INT,
	@StartDate AS DATE,
	@EndDate AS DATE
)
AS
BEGIN
	IF (@Fk_CycleState = 1)
	BEGIN 
		DECLARE @activeCycles AS INT;
		SET @activeCycles = (select COUNT(*) from [Cycle] c where c.Fk_CycleState = 1);
		
		IF(@activeCycles > 0)
		BEGIN
			DECLARE @pkCycle AS INT;
			SET @pkCycle = (select c.Pk_Cycle from [Cycle] c where c.Fk_CycleState = 1);
			UPDATE [Cycle] SET Fk_CycleState = 2 WHERE Pk_Cycle = @pkCycle;
		END
	END
	UPDATE [dbo].[Cycle] 
	SET 
		Fk_CycleState = @Fk_CycleState,
		[Year]  = @Year,
		[Number] = @Number,
		StartDate = @StartDate,
		EndDate = @EndDate 
	WHERE Pk_Cycle  = @Pk_Cycle;
END

--SEARCH
CREATE OR ALTER PROCEDURE udpFindCycle(
@Pk_Cycle AS INT = NULL, 
@Fk_CycleState AS INT = NULL,
@Year AS INT = NULL,
@Number AS INT = NULL,
@StartDate AS DATE = NULL,
@EndDate AS DATE = NULL
)
AS
BEGIN
    SELECT  * 
    FROM    [dbo].[Cycle] c   
    JOIN [dbo].CycleState cs ON c.Fk_CycleState = cs.Pk_CycleState   
    WHERE   (@Pk_Cycle IS NULL OR Pk_Cycle = @Pk_Cycle) 
            AND (@Fk_CycleState IS NULL OR Fk_CycleState = @Fk_CycleState)
            AND (@Year IS NULL OR Year = @Year)
            AND (@Number IS NULL OR Number = @Number)
            AND (@StartDate IS NULL OR StartDate = @StartDate)
            AND (@EndDate IS NULL OR EndDate = @EndDate)
END


--DELETE
CREATE OR ALTER PROCEDURE udpDeleteCycle(@Pk_Cycle AS INT)
AS
BEGIN
    DELETE 
        [dbo].[Cycle]
    WHERE
        Pk_Cycle = @Pk_Cycle;
END


