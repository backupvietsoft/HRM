ALTER PROCEDURE [dbo].[spGetComboQuan]
	@ID_TP BIGINT = -1,
	@Username NVARCHAR(100) ='admin',
	@NNgu INT =0,
	@CoAll BIT=1
AS 
BEGIN
IF @CoAll = 1
BEGIN
-----------------------------------------------------------------------------------------------------------------
	SELECT * FROM (
	SELECT ID_QUAN,CASE @NNgu WHEN 0 THEN TEN_QUAN WHEN 1 THEN ISNULL(NULLIF(TEN_QUAN_A,''),TEN_QUAN) ELSE ISNULL(NULLIF(TEN_QUAN_H,''),TEN_QUAN) END AS TEN_QUAN 
	FROM dbo.QUAN WHERE ID_TP =@ID_TP OR @ID_TP = -1
	UNION 
	SELECT -1,'< All >')T
	ORDER BY T.TEN_QUAN
-----------------------------------------------------------------------------------------------------------------
END
ELSE
BEGIN
-----------------------------------------------------------------------------------------------------------------
	SELECT ID_QUAN,CASE @NNgu WHEN 0 THEN TEN_QUAN WHEN 1 THEN ISNULL(NULLIF(TEN_QUAN_A,''),TEN_QUAN) ELSE ISNULL(NULLIF(TEN_QUAN_H,''),TEN_QUAN) END AS TEN_QUAN 
	FROM dbo.QUAN WHERE ID_TP =@ID_TP OR @ID_TP = -1
	ORDER BY TEN_QUAN
-----------------------------------------------------------------------------------------------------------------
 END	
END	
