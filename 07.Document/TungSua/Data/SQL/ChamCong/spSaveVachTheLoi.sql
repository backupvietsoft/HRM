ALTER PROCEDURE [dbo].[spSaveVachTheLoi]
	
	@Ngay DATE = '2020-02-01',
	@sBT NVARCHAR(50) = 'rptTienThuongXepLoai'
AS	
BEGIN
	CREATE TABLE #BT
	(
		[ID_CN] [bigint] NULL,
		[MS_CN] [nvarchar] (20) ,
		[HO_TEN] [nvarchar] (150) ,
		[NGAY_DEN] [datetime] NULL,
		[NGAY_VE] [datetime] NULL,
		[MS_THE_CC] [nvarchar] (50) ,
		[CHINH_SUA] [bit] NULL,
		[GIO_DEN_LUU] [datetime] NULL,
		[GIO_VE_LUU] [datetime] NULL
	)

	--[spXepLoaiKhenThuong]
	DECLARE @sSql  NVARCHAR(400)
	SET @sSql = 'INSERT INTO #BT(ID_CN,MS_CN,HO_TEN,NGAY_DEN,NGAY_VE,MS_THE_CC,CHINH_SUA,GIO_DEN_LUU,GIO_VE_LUU) 
	SELECT ID_CN,MS_CN,HO_TEN,NGAY_DEN,NGAY_VE,MS_THE_CC,CHINH_SUA,GIO_DEN_LUU,GIO_VE_LUU FROM '+@sBT+' WHERE CHINH_SUA = 1'
	EXEC(@sSql)
	SET @sSql = 'DROP TABLE ' + @sBT 
	EXEC(@sSql)
-------------------------------------------------------------------------------

	UPDATE  A
	SET A.NGAY_DEN = B.NGAY_DEN,
	A.NGAY_VE =B.NGAY_VE,
	A.GIO_DEN = B.GIO_DEN_LUU,
	A.PHUT_DEN = (DATEPART(HH,B.GIO_DEN_LUU)*60) + DATEPART(MI,B.GIO_DEN_LUU),
	A.GIO_VE =B.GIO_VE_LUU,
	A.PHUT_VE = (DATEPART(HH,B.GIO_VE_LUU)*60) + DATEPART(MI,B.GIO_VE_LUU),
	A.CHINH_SUA = 1
	FROM dbo.DU_LIEU_QUET_THE A
	INNER JOIN #BT B ON B.ID_CN = A.ID_CN
	WHERE CONVERT(DATE,NGAY) = @Ngay
END	

