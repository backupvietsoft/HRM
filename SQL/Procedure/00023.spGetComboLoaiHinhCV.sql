IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'spGetComboLoaiHinhCV')
   exec('CREATE PROCEDURE spGetComboLoaiHinhCV AS BEGIN SET NOCOUNT ON; END')
GO
ALTER PROCEDURE [dbo].[spGetComboLoaiHinhCV]
	@Username NVARCHAR(100) ='admin',
	@NNgu INT =0,
	@CoAll BIT=1
AS 
BEGIN
IF @CoAll = 1
BEGIN
SELECT * FROM (
SELECT ID_LHCV,CASE @NNgu WHEN 0 THEN TEN_LHCV WHEN 1 THEN ISNULL(NULLIF(TEN_LHCV_A,''),TEN_LHCV) ELSE ISNULL(NULLIF(TEN_LHCV_H,''),TEN_LHCV) END AS TEN_LHCV FROM dbo.LOAI_HINH_CONG_VIEC
UNION 
SELECT -1,'< All >')T
ORDER BY T.TEN_LHCV
END
ELSE
BEGIN
SELECT ID_LHCV,CASE @NNgu WHEN 0 THEN TEN_LHCV WHEN 1 THEN ISNULL(NULLIF(TEN_LHCV_A,''),TEN_LHCV) ELSE ISNULL(NULLIF(TEN_LHCV_H,''),TEN_LHCV) END AS TEN_LHCV FROM dbo.LOAI_HINH_CONG_VIEC
ORDER BY TEN_LHCV
END	
END	

