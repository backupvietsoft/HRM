ALTER PROCEDURE [dbo].[spSaveCheDoChamCongNV]
	@BangTam NVARCHAR(50) = 'CDCC_NV_TMPadmin'
AS	
BEGIN
	CREATE TABLE #BT
	(
		[ID_CN] [int] NULL,
		[NGAY_AD] [NVARCHAR](50) NULL,
		[ID_NHOM] [INT]  NULL,
		[CA] [NVARCHAR](5)  NULL
	)
	--[spXepLoaiKhenThuong]
	DECLARE @sSql  NVARCHAR(400)
	SET @sSql = 'INSERT INTO #BT(ID_CN, NGAY_AD, ID_NHOM, CA) SELECT ID_CN, NGAY_AD, ID_NHOM, CA FROM '+@BangTam
	EXEC(@sSql)
	SET @sSql = 'DROP TABLE ' + @BangTam 
	EXEC(@sSql)

-------------------------------------------------------------------------------
	DELETE A FROM CHE_DO_CHAM_CONG_NHAN_VIEN A INNER JOIN #BT B ON A.ID_CN=B.ID_CN WHERE A.NGAY_AD=convert(date,B.NGAY_AD,103) 
	INSERT INTO CHE_DO_CHAM_CONG_NHAN_VIEN (ID_CN, NGAY_AD, ID_NHOM, CA, ID_CDLV)  
	SELECT T1.ID_CN, convert(date,T1.NGAY_AD,103), T1.ID_NHOM, T2.CA, T1.CA 
	FROM (SELECT*FROM #BT WHERE ISNULL(NGAY_AD,'')!='') T1 INNER JOIN CHE_DO_LAM_VIEC T2 ON T1.CA = T2.ID_CDLV

END
