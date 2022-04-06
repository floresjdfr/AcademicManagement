CREATE OR ALTER PROCEDURE udpInsertCycle(@Fk_CycleState AS INT, @Year AS INT, @Number AS INT, @StartDate AS DATE, @EndDate AS DATE)
AS
BEGIN
    INSERT INTO [dbo].Cycle VALUES (@Fk_CycleState, @Year, @Number, @StartDate, @EndDate);
END

CREATE OR ALTER PROCEDURE udpModifyCycle(@Pk_Cycle AS INT, @Fk_CycleState AS INT, @Year AS INT, @Number AS INT, @StartDate AS DATE, @EndDate AS DATE)
AS
BEGIN
    UPDATE 
        [dbo].Cycle 
    SET 
        Fk_CycleState = @Fk_CycleState,
        Year = @Year,
        Number = @Number,
        StartDate = @StartDate,
        EndDate = @EndDate
    WHERE
        Pk_Cycle = @Pk_Cycle;
END

CREATE OR ALTER PROCEDURE udpFindCycle(@Pk_Cycle AS INT = NULL, @Fk_CycleState AS INT = NULL, @Year AS INT = NULL, @Number AS INT = NULL, @StartDate AS DATE = NULL, @EndDate AS DATE = NULL)
AS
BEGIN
	select  *
    from    [dbo].Cycle
    where   (@Pk_Cycle is null or Pk_Cycle = @Pk_Cycle)
            and (@Fk_CycleState is null or Fk_CycleState = @Fk_CycleState)
            and (@Year is null or Year = @Year)
            and (@Number is null or Number = @Number)
            and (@StartDate is null or StartDate = @StartDate)
            and (@EndDate is null or EndDate = @EndDate)
END

CREATE OR ALTER PROCEDURE udpDeleteCycle(@Pk_Cycle AS INT)
AS
BEGIN
    DELETE 
        [dbo].Cycle
    WHERE
        Pk_Cycle = @Pk_Cycle;
END