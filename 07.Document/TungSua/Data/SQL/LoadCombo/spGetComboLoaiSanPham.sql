ALTER PROCEDURE [dbo].[spGetComboLoaiSanPham]
	@Username NVARCHAR(100) ='admin',
	@NNgu INT =0,
	@CoAll BIT=1
AS 
BEGIN
IF @CoAll = 1
BEGIN
-----------------------------------------------------------------------------------------------------------------
SELECT * FROM (
SELECT ID_NHH,TEN_NHH FROM dbo.NHOM_HANG_HOA
UNION 
SELECT -1,'< All >')T
ORDER BY TEN_NHH
-----------------------------------------------------------------------------------------------------------------
END
ELSE
BEGIN
-----------------------------------------------------------------------------------------------------------------
SELECT ID_NHH,TEN_NHH FROM dbo.NHOM_HANG_HOA
ORDER BY TEN_NHH
-----------------------------------------------------------------------------------------------------------------
 END	
END	
