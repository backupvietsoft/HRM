IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'spGetComboDanhGiaTayNge')
   exec('CREATE PROCEDURE spGetComboDanhGiaTayNge AS BEGIN SET NOCOUNT ON; END')
GO
ALTER PROCEDURE [dbo].[spGetComboDanhGiaTayNge]
	@Username NVARCHAR(100) ='admin',
	@NNgu INT =0,
	@CoAll BIT=0
AS 
BEGIN
IF @CoAll = 1
BEGIN
SELECT * FROM (
SELECT ID_DGTN,CASE @NNgu WHEN 0 THEN TEN_DGTN WHEN 1 THEN ISNULL(NULLIF(TEN_DGTN_A,''),TEN_DGTN) ELSE ISNULL(NULLIF(TEN_DGTN_H,''),TEN_DGTN) END AS TEN_DGTN FROM dbo.DANH_GIA_TAY_NGHE
UNION 
SELECT -1,'< All >')T
ORDER BY T.TEN_DGTN
END
ELSE
BEGIN
SELECT ID_DGTN,CASE @NNgu WHEN 0 THEN TEN_DGTN WHEN 1 THEN ISNULL(NULLIF(TEN_DGTN_A,''),TEN_DGTN) ELSE ISNULL(NULLIF(TEN_DGTN_H,''),TEN_DGTN) END AS TEN_DGTN FROM dbo.DANH_GIA_TAY_NGHE
ORDER BY TEN_DGTN
END	
END	
GO

