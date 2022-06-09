ALTER PROCEDURE [dbo].[spGetDieuChinhBHXH]
	@Thang DATE ='2021-03-01',
	@Dot INT = -1,
	@UName NVARCHAR(100) ='admin',
	@NNgu INT =0
AS
BEGIN
SELECT ID_TO INTO #TMP FROM dbo.MGetToUser(@UName,@NNgu)
SELECT  A.THANG, A.DOT, A.ID_CN,
        B. HO_TEN, A.HS_LUONG_CU,
        A.HS_LUONG_MOI, A.HS_PHU_CAP_CU,
        A.HS_PHU_CAP_MOI, A.TU_THANG,
        A.DEN_THANG, A.SO_THANG,
		A.ID_LOAI_DIEU_CHINH,
        A.PHAN_TRAM_TRICH_NOP,
		A.LY_DO_TRICH_NOP
      
FROM    DIEU_CHINH_BHXH A
        INNER JOIN dbo.MGetListNhanSuToDate (@UName,@NNgu,-1,-1,-1,@Thang) B ON A.ID_CN = B.ID_CN
		INNER JOIN #TMP C  ON C.ID_TO = B.ID_TO
		WHERE CONVERT(NVARCHAR(10),A.THANG,23) = @Thang AND  (A.DOT =@Dot OR @Dot =-1)
END
