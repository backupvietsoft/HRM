IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'spGetComboNguonTD')
   exec('CREATE PROCEDURE spGetComboNguonTD AS BEGIN SET NOCOUNT ON; END')
GO

ALTER PROCEDURE [dbo].[spGetComboNguonTD]
	@Username NVARCHAR(100) ='admin',
	@NNgu INT =0,
	@CoAll BIT=0
AS 
BEGIN
IF @CoAll = 1
BEGIN
SELECT * FROM (
SELECT ID_NTD,CASE @NNgu WHEN 0 THEN TEN_NTD WHEN 1 THEN ISNULL(NULLIF(TEN_NTD_A,''),TEN_NTD) ELSE ISNULL(NULLIF(TEN_NTD_H,''),TEN_NTD) END AS TEN_NTD FROM dbo.NGUON_TUYEN_DUNG
UNION 
SELECT -1,'< All >')T
ORDER BY T.TEN_NTD
END
ELSE
BEGIN
SELECT ID_NTD,CASE @NNgu WHEN 0 THEN TEN_NTD WHEN 1 THEN ISNULL(NULLIF(TEN_NTD_A,''),TEN_NTD) ELSE ISNULL(NULLIF(TEN_NTD_H,''),TEN_NTD) END AS TEN_NTD FROM dbo.NGUON_TUYEN_DUNG
ORDER BY TEN_NTD
END	
END	

GO

