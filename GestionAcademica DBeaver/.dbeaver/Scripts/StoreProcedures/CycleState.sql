--SEARCH
CREATE OR ALTER PROCEDURE udpFindCycleState(@Pk_CycleState AS INT = NULL, @StateDescription AS VARCHAR(50) = NULL)
AS
BEGIN
    SELECT  *
    FROM    [dbo].CycleState cs 
    WHERE   (@Pk_CycleState IS NULL OR Pk_CycleState = @Pk_CycleState)
            AND (@StateDescription IS NULL OR StateDescription = @StateDescription);
END