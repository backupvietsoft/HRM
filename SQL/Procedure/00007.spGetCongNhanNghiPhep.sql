
ALTER PROCEDURE [dbo].[spGetCongNhanNghiPhep]
	@ID_DV BIGINT =-1,
	@ID_XN BIGINT =-1,
	@ID_TO BIGINT =-1,
	@NAM INT =2022,
	@UName NVARCHAR(100) ='admin',
	@NNgu INT =0
AS
BEGIN

	DECLARE @NgayBD Date
	DECLARE @NgayKT Date
	
	SET @NgayBD = CONVERT(datetime,'01/01/'+ str(@NAM))
	SET @NgayKT = CONVERT(datetime,'12/31/'+ str(@NAM))

	SELECT * INTO #CN FROM dbo.MGetListNhanSuFormToDate(@UName,@NNgu, @ID_DV, @ID_XN, @ID_TO, @NgayBD, @NgayKT)
	
	SELECT DISTINCT CONVERT(BIT,0) AS CHON, CN.ID_CN, CN.MS_CN, CN.HO_TEN, ISNULL(PN.SPCL,0) AS SPCL, 
	CASE WHEN ISNULL(CN.NGAY_NGHI_VIEC,'') = '' THEN 1 ELSE 0 END AS TinhTrang 
	FROM #CN CN LEFT JOIN [dbo].[funGetPhepNamConLai](@NAM) PN ON CN.ID_CN = PN.ID_CN
	
END



GO

