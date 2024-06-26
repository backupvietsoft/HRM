ALTER PROCEDURE [dbo].[spGetNhomCC]
    @DNgay Date = '20210401',
    @UName NVARCHAR(100) ='admin',
	@NNgu INT =0
AS
BEGIN
	SELECT DISTINCT T1.ID_NHOM, TEN_NHOM 
	FROM CHE_DO_LAM_VIEC T1 
	INNER JOIN NHOM_CHAM_CONG T2 ON T1.ID_NHOM = T2.ID_NHOM
	WHERE T1.NGAY = (SELECT MAX(NGAY) FROM CHE_DO_LAM_VIEC WHERE NGAY <= @DNgay)
	UNION 
	SELECT -1 AS ID_NHOM, '' TEN_NHOM
	ORDER BY TEN_NHOM
END
