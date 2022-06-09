ALTER PROCEDURE [dbo].[spGetListNS]
	@ID_DV BIGINT =-1,
	@ID_XN BIGINT =-1,
	@ID_TO BIGINT =-1,
	@ID_TT_HT INT =-1,
	@UName NVARCHAR(100) ='admin',
	@NNgu INT =1
AS
BEGIN

	SELECT  ID_TO,ID_DV,ID_XN INTO #TEMPT  FROM dbo.MGetToUser(@UName,@NNgu) WHERE (ID_DV =@ID_DV OR @ID_DV =-1) AND (ID_XN =@ID_XN OR @ID_XN = -1 ) AND (ID_TO =@ID_TO OR @ID_TO =-1)
    
    SELECT T1.ID_CN, MS_CN, HO + ' ' + TEN AS HO_TEN, HINH_CN, MS_THE_CC, DIA_CHI_THUONG_TRU,NGAY_VAO_CTY, T2.TEN_TT_HD, T3.TEN_TT_HT,NGAY_SINH, 
	T1.DT_DI_DONG AS SO_DT,T3.MAU_TT,
	CASE WHEN @NNgu = 0 THEN N'MSCC: ' WHEN @NNgu = 1 THEN 'Time card code: ' ELSE 'Ch...' END AS colMSCC, 
	CASE WHEN @NNgu = 0 THEN N'TT Hợp đồng: ' WHEN @NNgu = 1 THEN 'Contract status: ' ELSE 'Ch...' END AS colTTHD, 
	CASE WHEN @NNgu = 0 THEN N'TT Làm việc: ' WHEN @NNgu = 1 THEN 'Working status: ' ELSE 'Ch...' END AS colTTLV, 
	CASE WHEN @NNgu = 0 THEN N'Ngày sinh: ' WHEN @NNgu = 1 THEN 'Date of birth: ' ELSE 'Ch...' END AS colNS, 
	CASE WHEN @NNgu = 0 THEN N'SĐT: ' WHEN @NNgu = 1 THEN 'Phone: ' ELSE 'Ch...' END AS colSDT, 
	CASE WHEN @NNgu = 0 THEN N'Địa chỉ: ' WHEN @NNgu = 1 THEN 'Address: ' ELSE 'Ch...' END AS colDC
	FROM dbo.CONG_NHAN T1 
	LEFT JOIN dbo.TINH_TRANG_HD T2 ON T1.ID_TT_HD = T2.ID_TT_HD 
	LEFT JOIN dbo.TINH_TRANG_HT T3 ON T1.ID_TT_HT = T3.ID_TT_HT 
	INNER JOIN #TEMPT T4 ON T4.ID_TO = T1.ID_TO
	WHERE T1.ID_TT_HT = @ID_TT_HT OR @ID_TT_HT = -1
	ORDER BY T1.MS_CN
END