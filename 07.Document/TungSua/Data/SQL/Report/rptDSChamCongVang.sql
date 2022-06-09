ALTER PROCEDURE [dbo].[rptDSChamCongVang]
	@UName NVARCHAR(100) ='admin',
	@NNgu INT =0,
	@DVi INT = -1,
	@XN INT = -1,
	@TO INT = -1,
	@TNGAY Date = '20210301',
	@DNGAY Date = '20210331',
	@LDV NVARCHAR(100) = '-1'
AS 
BEGIN
	DECLARE @Chuoi nvarchar(4000)
	
	SELECT * INTO #CN FROM dbo.MGetListNhanSuFormToDate(@UName,@NNgu, @DVi, @XN, @TO, @TNGAY, @DNGAY)
	
	SET @Chuoi = 'SELECT T2.MS_CN, T2.HO_TEN, T2.TEN_XN, T2.TEN_TO, T1.NGAY, T3.MS_LDV, T3.TEN_LDV, T1.SG_VANG 
	FROM CHAM_CONG_CHI_TIET_VANG T1 
	INNER JOIN #CN T2 ON T1.ID_CN = T2.ID_CN
	INNER JOIN LY_DO_VANG T3 ON T1.ID_LDV = T3.ID_LDV WHERE T1.NGAY BETWEEN ''' + CONVERT(nvarchar(10),@TNGAY,101) + ''' AND ''' + CONVERT(nvarchar(10),@DNGAY,101) + ''''
	
	IF (@LDV <> '-1') 
		BEGIN
			SET @Chuoi = @Chuoi + ' AND T1.ID_LDV IN (' + @LDV + ')'
		END
	SET @Chuoi = @Chuoi + ' ORDER BY T2.STT_XN, T2.STT_TO, T2.TEN_TO, T2.MS_CN, T1.NGAY'
	EXEC (@Chuoi)

END


