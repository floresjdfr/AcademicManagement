
--INSERT NEW CYCLE
CREATE OR ALTER PROCEDURE udpInsertCycle(@Pk_CycleState AS INT, @Year AS INT, @Number AS INT, @StartDate AS DATE, @EndDate AS DATE)
AS
BEGIN
	INSERT INTO CYCLE VALUES(@Pk_CycleState, @Year, @Number, @StartDate, @EndDate);
END

