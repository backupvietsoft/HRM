ALTER PROCEDURE [dbo].[spGetTheoDoiPhepNam]
	@UName NVARCHAR(100) ='admin',
	@NNgu INT =0,
	@DVi INT = -1,
	@XN INT = -1,
	@TO INT = -1,
	@Type INT = 0,
	@TNGAY Date ='2021-01-01',
	@DNGAY Date ='2021-12-31'
AS 
BEGIN
	DECLARE @Chuoi nvarchar(4000)
	DECLARE @Thang Int
	
	SET @Thang = 1
	SET @Chuoi = 'CREATE TABLE ##TBReport ( 
		[ID_CN] Int NULL, [MS_CN] [nvarchar] (20)  NULL, [HO_TEN] [nvarchar] (100)  NULL, [TEN_TO] [nvarchar] (100)  NULL, [TEN_XN] [nvarchar] (100)  NULL , 
		[NGAY_VAO_LAM] [datetime] NULL, [NGAY_NGHI_VIEC] [datetime] NULL, [NGAY_TINH_PHEP] [datetime] NULL, [SO_TK] [nvarchar] (50)  NULL , 
		[ID_TO] [Int] NULL, [ID_XN] [Int] NULL, [LUONG_TP] [float] NULL, [PHEP_NAM] [float] NULL, [PHEP_TN] [float] NULL, [PHEP_KHAC] [float] NULL, 
		[LUONG_THANG] [float] NULL , [NAM_TP] [nvarchar] (4), [STT_XN] [Int] NULL, [STT_TO] [Int] NULL, '
	WHILE @Thang <= 12
		BEGIN
			SET @Chuoi = @Chuoi + ' [T' + CAST(@Thang as varchar) + '] FLOAT  NULL, '
			SET @Thang = @Thang + 1
		END

    SET @Chuoi = @Chuoi + ' [TONG_LD] [float] NULL, [TONG_PHEP] [float] NULL, [PHEP_CON_LAI] [float] NULL)'
    
    EXEC (@Chuoi)
    
    SELECT * INTO #CN FROM dbo.MGetListNhanSuFormToDate(@UName,@NNgu, @DVi, @XN, @TO, @TNGAY, @DNGAY)
    
    INSERT INTO ##TBReport (ID_CN, MS_CN, HO_TEN, TEN_TO, TEN_XN, NGAY_VAO_LAM, NGAY_NGHI_VIEC, SO_TK, PHEP_NAM, ID_TO, ID_XN, STT_XN, STT_TO)
    SELECT ID_CN, MS_CN, HO_TEN, TEN_TO, TEN_XN, NGAY_VAO_CTY, NGAY_NGHI_VIEC, MA_THE_ATM, PHEP_CT, ID_TO, ID_XN, STT_XN, STT_TO
    FROM #CN

    If @Type = 1 
		DELETE FROM ##TBReport WHERE ID_CN IN (SELECT ID_CN FROM #CN WHERE IsNull(NGAY_NGHI_VIEC,'') <> '') 
    
    If @Type = 0
        DELETE FROM ##TBReport WHERE ID_CN IN (SELECT ID_CN FROM CONG_NHAN WHERE IsNull(NGAY_NGHI_VIEC,'') ='')

    UPDATE ##TBReport SET NGAY_NGHI_VIEC = CASE WHEN NGAY_NGHI_VIEC <= @DNGAY THEN NGAY_NGHI_VIEC ELSE @DNGAY END WHERE IsNull(NGAY_NGHI_VIEC,'') <>'' 
    
    UPDATE ##TBReport SET NGAY_TINH_PHEP = NGAY_VAO_LAM
    
    UPDATE ##TBReport SET NGAY_TINH_PHEP = @TNGAY WHERE NGAY_TINH_PHEP <= @TNGAY AND IsNull(NGAY_TINH_PHEP,'') <>''
    
    UPDATE ##TBReport SET NGAY_TINH_PHEP = DATEADD(Day,- DAY(NGAY_TINH_PHEP)+1,NGAY_TINH_PHEP) WHERE DAY(NGAY_TINH_PHEP) < 15
    
    UPDATE ##TBReport SET NGAY_TINH_PHEP = DATEADD(Day,- DAY(NGAY_TINH_PHEP)+1,DATEADD(Month, 1 ,NGAY_TINH_PHEP)), 
    PHEP_KHAC = CASE WHEN DAY(NGAY_TINH_PHEP) < 20 THEN 0.5 ELSE 0 END WHERE DAY(NGAY_TINH_PHEP) >= 15
    
    UPDATE ##TBReport SET NGAY_NGHI_VIEC = DATEADD(DAY,- DAY(NGAY_NGHI_VIEC)+1, NGAY_NGHI_VIEC) WHERE DAY(NGAY_NGHI_VIEC) < 15

    UPDATE ##TBReport SET NGAY_NGHI_VIEC = DATEADD(DAY,- DAY(NGAY_NGHI_VIEC)+1, DATEADD(Month, 1 ,NGAY_NGHI_VIEC)) WHERE DAY(NGAY_NGHI_VIEC) >= 15

    UPDATE ##TBReport SET NGAY_NGHI_VIEC = DATEADD(DAY, 1,@DNGAY) WHERE IsNull(NGAY_NGHI_VIEC,'') =''
    
     
    
    --cap nhat phep 5 nam duoc 1 phep
    UPDATE ##TBReport SET PHEP_TN = (DATEDIFF(D,NGAY_VAO_LAM,NGAY_NGHI_VIEC)/365)/5
    
    --cap nhat so phep duoc huong
    UPDATE ##TBReport SET PHEP_NAM = ISNULL(PHEP_NAM,0) + ISNULL(PHEP_KHAC,0) + DATEDIFF(M,NGAY_TINH_PHEP,NGAY_NGHI_VIEC)
    
    --cap nhat thang nghi phep
    --DECLARE @ThangNP Int
    DECLARE @NamTP Int
    
    SET @NamTP = YEAR(@TNGAY)
    SET @Thang = 1
    WHILE @Thang <= 12
		BEGIN
			SET @Chuoi = 'UPDATE ##TBReport SET T' + CAST(@Thang as varchar) + ' = ISNULL(SNV,0) FROM ##TBReport T1 
			LEFT JOIN (SELECT CCV.ID_CN,  ROUND(SUM(CCV.SG_VANG/CC.SG_LV_QD),1) SNV FROM CHAM_CONG_CHI_TIET_VANG CCV 
			INNER JOIN CHAM_CONG CC ON CCV.NGAY = CC.NGAY AND CC.ID_CN = CCV.ID_CN INNER JOIN LY_DO_VANG LDV ON CCV.ID_LDV = LDV.ID_LDV
			WHERE LDV.PHEP = 1 AND YEAR(CCV.NGAY) = ''' + CAST(@NamTP as varchar)  + ''' AND MONTH(CCV.NGAY) = ''' + CAST(@Thang as varchar) + ''' 
			GROUP BY CCV.ID_CN) T2 ON T1.ID_CN = T2.ID_CN'
			EXEC (@Chuoi)
			
			SET @Chuoi = 'UPDATE ##TBReport SET TONG_PHEP = ISNULL(TONG_PHEP,0) + T'  + CAST(@Thang as varchar) 
			EXEC (@Chuoi)
			SET @Thang = @Thang + 1
		END
    
	--Tinh luong tinh phep
    DECLARE @tringay Datetime
    
    SET @tringay = @DNGAY
    SET @Thang = 1
    WHILE @Thang <= 6
		BEGIN
			UPDATE ##TBReport SET LUONG_TP = ISNULL(TB1.LUONG_TP,0)  + ISNULL(TB2.HS_LUONG,0)
			FROM ##TBReport TB1 LEFT JOIN (SELECT T1.ID_CN, T1.HS_LUONG FROM LUONG_CO_BAN T1 
			INNER JOIN (SELECT ID_CN, MAX(NGAY_HIEU_LUC) NGAYMAX FROM LUONG_CO_BAN 
			WHERE NGAY_HIEU_LUC <= '20211231' GROUP BY ID_CN) T2 ON T1.ID_CN = T2.ID_CN AND T1.NGAY_HIEU_LUC = T2.NGAYMAX) TB2
			ON TB1.ID_CN = TB2.ID_CN
			
			SET @tringay = DATEADD(M,-1,@tringay)
			SET @Thang = @Thang + 1
		END
    
    UPDATE ##TBReport SET LUONG_TP = ROUND(ISNULL(LUONG_TP,0)/6,0), PHEP_CON_LAI = ROUND(ISNULL(PHEP_NAM,0) + ISNULL(PHEP_TN,0) - ISNULL(TONG_PHEP,0),1)
    
    DECLARE @DSCot nvarchar(4000)
    
    SET @DSCot = ''
    SET @Thang = 1
    WHILE @Thang <= 12
		BEGIN
			SET @DSCot = @DSCot + 'T' + CAST(@Thang as varchar) + ', '
			SET @Thang = @Thang + 1
		END
    
    SET @Chuoi = 'SELECT ROW_NUMBER() OVER (ORDER BY STT_XN, STT_TO, MS_CN) AS STT, MS_CN, HO_TEN, SO_TK, TEN_XN, TEN_TO, LUONG_TP,  
    NGAY_NGHI_VIEC, CONVERT(nvarchar(10),NGAY_VAO_LAM,101) NGAY_VL, PHEP_NAM, PHEP_TN, ' + @DSCot +
    ' TONG_PHEP, PHEP_CON_LAI, ROUND(PHEP_CON_LAI * (LUONG_TP/26),0) THANH_TIEN, CONVERT(nvarchar(10),Null) KY_NHAN FROM ##TBReport' 
    EXEC (@Chuoi)
    DROP TABLE ##TBReport
END