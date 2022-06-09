CREATE PROCEDURE [dbo].[spDeleteDLChamCongNgay](
	@UName NVARCHAR(100) ='admin',
	@NNgu INT =0,
	@DVi INT = -1,
	@XN INT = -1,
	@TO INT = -1,
	@Ngay Datetime = '08/23/2021'
)  
AS  
BEGIN 

	
	--danh sach cong nhan toi ngay cham cong
	SELECT * INTO #CN FROM dbo.MGetListNhanSuToDate(@UName, @NNgu, @DVi, @XN, @TO, @Ngay)
		
	--xoa du lieu cu cua don vi, phong ban, to da link du lieu
	DELETE FROM CHAM_CONG_CHI_TIET_VANG 
	FROM CHAM_CONG_CHI_TIET_VANG T1 INNER JOIN #CN T2 ON T1.ID_CN = T2.ID_CN 
	WHERE (CONVERT(nvarchar(10),NGAY,101) = CONVERT(nvarchar(10),@Ngay,101))
	
	DELETE FROM CHAM_CONG_CHI_TIET 
	FROM CHAM_CONG_CHI_TIET T1 INNER JOIN #CN T2 ON T1.ID_CN = T2.ID_CN 
	WHERE (CONVERT(nvarchar(10),NGAY,101) = CONVERT(nvarchar(10),@Ngay,101))
	
	DELETE FROM CHAM_CONG
	FROM CHAM_CONG T1 INNER JOIN #CN T2 ON T1.ID_CN = T2.ID_CN 
	WHERE (CONVERT(nvarchar(10),NGAY,101) = CONVERT(nvarchar(10),@Ngay,101))

	DELETE FROM DU_LIEU_QUET_THE
	FROM DU_LIEU_QUET_THE T1 INNER JOIN #CN T2 ON T1.ID_CN = T2.ID_CN 
	WHERE (CONVERT(nvarchar(10),NGAY,101) = CONVERT(nvarchar(10),@Ngay,101))

END  