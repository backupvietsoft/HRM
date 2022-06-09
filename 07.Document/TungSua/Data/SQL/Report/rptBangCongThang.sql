ALTER PROCEDURE [dbo].[rptBangCongThang]
	@UName NVARCHAR(100) ='admin',
	@NNgu INT =0,
	@DVi INT = -1,
	@XN INT = -1,
	@TO INT = -1,
	@TNGAY Date ='2021-03-01',
	@DNGAY Date ='2021-03-31'
AS 
BEGIN
	DECLARE @NgayBD Date
	DECLARE @NgayKT Date
	
	SET @NgayBD = @TNGAY
	SET @NgayKT = @DNGAY
	
	SELECT * INTO #CN FROM dbo.MGetListNhanSuFormToDate(@UName,@NNgu, @DVi, @XN, @TO, @TNGAY, @DNGAY)

	DECLARE @Chuoi nvarchar(MAX)
	DECLARE @NgayF Int
	DECLARE @NgayT Int
	SET @NgayF = DAY(@NgayBD)
	SET @NgayT = DAY(@NgayKT)
	
	SET @Chuoi = 'CREATE TABLE ##TBReport (
			[ID_CN] Int NULL, [MS_CN] nvarchar(20) NOT NULL, [HO_TEN] nvarchar(100) NULL, [CHUC_VU] nvarchar(100), [TEN_TO] nvarchar(100) NULL , 
			[TEN_XN] nvarchar(100)  NULL , [TEN_DV] nvarchar(100)  NULL , [DIA_CHI_DV] nvarchar(100)  NULL , [ID_DV] INT NULL , [ID_XN] INT NULL , 
			[ID_TO] INT NULL, [ID_CVU] INT NULL, NGAY_VAO_CTY datetime NULL, [NGAY_VAO_LAM] datetime NULL, [NGAY_NGHI_VIEC] datetime NULL, 
			[NGAY_SINH] datetime NULL, [GOP_PB] [bit] NULL, ' 
	
	WHILE @NgayF <= @NgayT
		BEGIN
			SET @Chuoi = @Chuoi + ' [NG' + CAST(@NgayF as varchar) + '] FLOAT  NULL  DEFAULT (0) , '
			SET @Chuoi = @Chuoi + ' [LV_CD' + CAST(@NgayF as varchar) + '] Int  NULL  DEFAULT (0) , '
			SET @Chuoi = @Chuoi + ' [NG' + CAST(@NgayF as varchar) + 'MS] nvarchar(50) NULL , '
			SET @NgayF = @NgayF + 1
		END
			
		SET @Chuoi = @Chuoi + ' [NGAY_CONG] FLOAT NULL  DEFAULT (0), [NGAY_CONG_CN] FLOAT NULL  DEFAULT (0), 
		[NGHI_PN] FLOAT NULL  DEFAULT (0), [NGHI_BU] FLOAT NULL  DEFAULT (0), [NGHI_LE] FLOAT NULL  DEFAULT (0), [NGHI_BHXH] FLOAT NULL  DEFAULT (0), 
		[NGHI_CHUA_NV] FLOAT NULL  DEFAULT (0),	[NGHI_HL] FLOAT NULL  DEFAULT (0), [NGHI_KL] FLOAT NULL  DEFAULT (0), 
		[NGHI_VIEC] FLOAT NULL  DEFAULT (0), [CONG_DT_VS_RN] FLOAT NULL  DEFAULT (0), [SO_LAN_DT] FLOAT NULL  DEFAULT (0), [SO_GIO_DT] FLOAT NULL  DEFAULT (0),
		[SO_LAN_VS] FLOAT NULL  DEFAULT (0), [SO_GIO_VS] FLOAT NULL  DEFAULT (0),
		[SO_LAN_RN] FLOAT NULL  DEFAULT (0), [SO_GIO_RN] FLOAT NULL  DEFAULT (0),
		[TONG_NGAY_CONG] FLOAT NULL  DEFAULT (0), STT_XN INT  DEFAULT (999), STT_TO INT  DEFAULT (999) , STT_CV INT  DEFAULT (999) )'
	EXEC (@Chuoi)
	
	--tao danh sach cham cong can bao cao
    INSERT INTO ##TBReport(STT_XN, STT_TO, ID_CN, ID_DV, TEN_DV, ID_XN, TEN_XN, ID_TO, TEN_TO, MS_CN, HO_TEN, ID_CVU, CHUC_VU, NGAY_VAO_CTY, 
    NGAY_VAO_LAM, NGAY_NGHI_VIEC, NGAY_SINH) 
    SELECT T1.STT_XN, T1.STT_TO, T1.ID_CN, T1.ID_DV, T1.TEN_DV, T1.ID_XN, T1.TEN_XN, T1.ID_TO, T1.TEN_TO, T1.MS_CN, T1.HO_TEN, T1.ID_CV, T2.TEN_CV, 
    ISNULL(T1.NGAY_THU_VIEC,T1.NGAY_HOC_VIEC), T1.NGAY_VAO_LAM, T1.NGAY_NGHI_VIEC, T1.NGAY_SINH 
    FROM #CN T1 INNER JOIN CHUC_VU T2 ON T1.ID_CV = T2.ID_CV
    --lay du lieu cham cong thang
    SELECT T1.*, T3.SG_LV_QD INTO #CCCT FROM CHAM_CONG_CHI_TIET T1 INNER JOIN #CN T2 ON T1.ID_CN = T2.ID_CN
    INNER JOIN CHAM_CONG T3 ON T1.ID_CN = T3.ID_CN AND T1.NGAY = T3.NGAY
    WHERE T1.NGAY BETWEEN  @TNGAY AND @DNGAY
	--lay du lieu cham cong vang thang
    SELECT T1.NGAY, T1.ID_CN, T1.ID_LDV, T3.MS_LDV, T1.SG_VANG, T4.SG_LV_QD
    INTO #CCVANG
    FROM CHAM_CONG_CHI_TIET_VANG T1 INNER JOIN #CN T2 ON T1.ID_CN= T2.ID_CN 
    INNER JOIN LY_DO_VANG T3 ON T1.ID_LDV = T3.ID_LDV
    INNER JOIN CHAM_CONG T4 ON T1.ID_CN = T4.ID_CN AND T1.NGAY = T4.NGAY
    WHERE (T1.NGAY BETWEEN @TNGAY AND @DNGAY)
    
	--Cap nhat ngay cong lam viec
	SET @NgayF = DAY(@NgayBD)
	SET @NgayT = DAY(@NgayKT)
	
	DECLARE @DSCot nvarchar(4000)
	SET @DSCot = ''
    WHILE @NgayF <= @NgayT
		BEGIN
			SET @DSCot = @DSCot + ', [' + CAST(@NgayF as varchar) + ']'
			SET @NgayF = @NgayF + 1
		END
	SET @DSCot = SUBSTRING(@DSCot,2,LEN(@DSCot)-1)
	
    SET @Chuoi = 'SELECT ID_CN AS [MA_SO], ' + @DSCot + ' INTO ##TBNCLV FROM ( 
				SELECT ID_CN, NCLV, NGAY_THANG FROM (SELECT T1.ID_CN, T1.NGAY, DAY(T1.NGAY) NGAY_THANG, 
			ROUND(GLV/ISNULL(SG_LV_QD,8),2,1) NCLV FROM (SELECT CC1.ID_CN, CC1.NGAY, CC1.SG_LV_QD, 
			SUM(CASE WHEN CC1.TANG_CA = 0 THEN ISNULL(CC1.SG_LV_TT,0) ELSE 0 END) AS GLV 
			FROM #CCCT CC1 GROUP BY CC1.ID_CN, CC1.NGAY, CC1.SG_LV_QD ) T1) P ) I PIVOT (SUM(NCLV) 
			FOR NGAY_THANG IN (' + @DSCot + ')) AS J'
	EXEC (@Chuoi)
	
	SET @Chuoi = 'UPDATE ##TBReport SET '
	SET @NgayF = DAY(@NgayBD)
	DECLARE @DSCotNG nvarchar(4000)
	SET @DSCotNG = ''
	WHILE @NgayF <= @NgayT
	BEGIN
		SET @DSCotNG = @DSCotNG + ', NG' + CAST(@NgayF as varchar) + ' = ISNULL(T2.[' + CAST(@NgayF as varchar) + '],0)'
		SET @NgayF = @NgayF + 1
	END
	SET @DSCotNG = SUBSTRING(@DSCotNG,2,LEN(@DSCotNG)-1)
	
    SET @Chuoi = @Chuoi + @DSCotNG + ' FROM ##TBReport T1 INNER JOIN ##TBNCLV T2 ON T1.ID_CN = T2.MA_SO'
	EXEC (@Chuoi)
	
	--cap nhat ngay cong vang
    SET @Chuoi = 'SELECT ID_CN AS [MA_SO], ' + @DSCot + ' INTO ##TBDSV
        FROM ( SELECT ID_CN, LDV, NGAY_THANG FROM ( SELECT ID_CN, DAY(NGAY) NGAY_THANG, 
        MS_LDV LDV FROM #CCVANG) T1 ) I PIVOT (MAX(LDV) FOR NGAY_THANG IN (' + @DSCot + ')) AS J'
    EXEC (@Chuoi)
    
    --SELECT * FROM ##TBDSV
	SET @Chuoi = 'UPDATE ##TBReport SET '
	SET @NgayF = DAY(@NgayBD)
	SET @DSCotNG = ''
	WHILE @NgayF <= @NgayT
	BEGIN
		SET @DSCotNG = @DSCotNG + ', NG' + CAST(@NgayF as varchar) + 'MS = ISNULL(T2.[' + CAST(@NgayF as varchar) + '],''' + '' + ''')'
		SET @NgayF = @NgayF + 1
	END
	SET @DSCotNG = SUBSTRING(@DSCotNG,2,LEN(@DSCotNG)-1)
    
    SET @Chuoi = @Chuoi + @DSCotNG + ' FROM ##TBReport T1 INNER JOIN ##TBDSV T2 ON T1.ID_CN = T2.MA_SO'
	EXEC (@Chuoi)
	
	SET @Chuoi = 'UPDATE ##TBReport SET NGAY_CONG = ISNULL(TB2.TCLV,0), NGAY_CONG_CN = ISNULL(TB2.TCLCN,0) FROM ##TBReport TB1 
		LEFT JOIN (SELECT T1.ID_CN, SUM(CASE WHEN DATENAME(DW,T1.NGAY) = ''' + 'Sunday' + ''' THEN 0 ELSE ROUND(GLV/ISNULL(SG_LV_QD,8) ,2)END) TCLV, 
		SUM(CASE WHEN DATENAME(DW,T1.NGAY) = ''' + 'Sunday' + ''' THEN ROUND(GLV/ISNULL(SG_LV_QD,8),2) ELSE 0 END) TCLCN 
		FROM (SELECT ID_CN, NGAY, SG_LV_QD, CA_DEM, SUM(SG_LV_TT) AS GLV FROM #CCCT 
		WHERE TANG_CA = 0 GROUP BY ID_CN, NGAY, SG_LV_QD, CA_DEM) T1 GROUP BY T1.ID_CN) TB2 ON TB1.ID_CN = TB2.ID_CN'
	EXEC (@Chuoi)

	--Cap nhat so ngay nghi
	--nghi phep chinh thuc
    UPDATE ##TBReport SET NGHI_PN =  ISNULL(SNP_CT,0) 
    FROM ##TBReport T1 LEFT JOIN (SELECT ID_CN, SUM(ROUND(SG_VANG/SG_LV_QD,2)) SNP_CT 
    FROM #CCVANG WHERE MS_LDV IN ('P') GROUP BY ID_CN) T2 ON T1.ID_CN = T2.ID_CN 
  
	--nghi bu chinh thuc
    UPDATE ##TBReport SET NGHI_BU =  ISNULL(SNP_CT,0) 
    FROM ##TBReport T1 LEFT JOIN (SELECT ID_CN, SUM(ROUND(SG_VANG/SG_LV_QD,2)) SNP_CT 
    FROM #CCVANG WHERE MS_LDV IN ('BU') GROUP BY ID_CN) T2 ON T1.ID_CN = T2.ID_CN 

    --nghi le chinh thuc
    UPDATE ##TBReport SET NGHI_LE =  ISNULL(SNP_CT,0) 
    FROM ##TBReport T1 LEFT JOIN (SELECT ID_CN, SUM(ROUND(SG_VANG/SG_LV_QD,2)) SNP_CT 
    FROM #CCVANG WHERE MS_LDV IN ('L') GROUP BY ID_CN) T2 ON T1.ID_CN = T2.ID_CN 

	--nghi huong bhxh
    UPDATE ##TBReport SET NGHI_BHXH = ISNULL(SN_BHXH,0) 
    FROM ##TBReport T1 LEFT JOIN (SELECT CCV.ID_CN, SUM(ROUND(CCV.SG_VANG/CCV.SG_LV_QD,2)) SN_BHXH 
    FROM #CCVANG CCV INNER JOIN LY_DO_VANG LDV ON CCV.ID_LDV = LDV.ID_LDV 
    WHERE LDV.TINH_BHXH = 1 GROUP BY CCV.ID_CN) T2 ON T1.ID_CN = T2.ID_CN 

    --so ngay chua nhan viec
	UPDATE ##TBReport SET NGHI_CHUA_NV = DateDiff(D,@TNGAY,NGAY_VAO_CTY) WHERE NGAY_VAO_CTY > @TNGAY

    --nghi huong luong chinh thuc
    UPDATE ##TBReport SET NGHI_HL =  ISNULL(SNHL_CT,0) 
    FROM ##TBReport T1 LEFT JOIN (SELECT CCV.ID_CN, SUM(ROUND(CCV.SG_VANG/CCV.SG_LV_QD,2)) SNHL_CT
    FROM #CCVANG CCV INNER JOIN LY_DO_VANG LDV ON CCV.ID_LDV = LDV.ID_LDV 
    WHERE LDV.TINH_LUONG = 1 GROUP BY CCV.ID_CN) T2 ON T1.ID_CN = T2.ID_CN 
    
    --nghi khong luong, tu do
    UPDATE ##TBReport SET NGHI_KL =  ISNULL(SN_KL,0) 
    FROM ##TBReport T1 LEFT JOIN (SELECT ID_CN, SUM(ROUND(SG_VANG/SG_LV_QD,2)) SN_KL 
    FROM #CCVANG WHERE MS_LDV IN ('Ro','O') GROUP BY ID_CN) T2 ON T1.ID_CN = T2.ID_CN 
    
	--cap nhat ra ngoai
	--Ra ngoai
	UPDATE ##TBReport SET SO_GIO_RN = T2.SG, SO_LAN_RN = T2.SL FROM ##TBReport T1 
	INNER JOIN (SELECT ID_CN, SG_LV_QD, SUM(SG_VANG) AS SG, COUNT(NGAY) AS SL FROM #CCVANG
	WHERE MS_LDV IN ('RN') GROUP BY ID_CN, SG_LV_QD) T2 ON T1.ID_CN = T2.ID_CN 

	SET @Chuoi = ''
	SET @DSCotNG = ''
	WHILE @NgayBD <= @NgayKT
	BEGIN
	
		SET @NgayF = DAY(@NgayBD)
		SET @DSCotNG = @DSCotNG + ', NG' + CAST(@NgayF as varchar) + 'MS'
		
		IF DATENAME(dw,@NgayBD) = 'Sunday'
			BEGIN
				SET @Chuoi = 'UPDATE ##TBReport SET NG' + CAST(@NgayF as varchar) + 'MS = ''' + 'CN' + ''''
				EXEC (@Chuoi)
				SET @Chuoi = 'UPDATE ##TBReport SET NG' + CAST(@NgayF as varchar) + 'MS = NG' + CAST(@NgayF as varchar) + ' WHERE ISNULL(NG' + CAST(@NgayF as varchar) + ',0) > 0'
				EXEC (@Chuoi)
			END
		ELSE
			BEGIN
				--Cap nhat di tre
				SET @Chuoi = 'UPDATE ##TBReport SET NG' + CAST(@NgayF as varchar) + 'MS = CASE WHEN (1-T1.NG' + CAST(@NgayF as varchar) + ')*480 <= 30 
					THEN ''' + '' + ''' ELSE NG' + CAST(@NgayF as varchar) + 'MS + ''' + '/' + ''' END + ''' + 'TI' + ''', 
					SO_GIO_DT = ISNULL(SO_GIO_DT,0) + ROUND(CONVERT(Float,SPDT)/60,2), SO_LAN_DT = ISNULL(SO_LAN_DT,0) + 1 
					FROM ##TBReport T1 INNER JOIN (SELECT T1.ID_CN, T1.PBD_CC - T2.PBD_ND SPDT FROM (
					SELECT ID_CN, ID_NHOM, CA, MIN(PHUT_DEN) PBD_CC FROM #CCCT WHERE NGAY = '''+ CONVERT(nvarchar(10),@NgayBD,101) + ''' AND TANG_CA = 0 GROUP BY ID_CN, ID_NHOM, CA 
					) T1 INNER JOIN (
					SELECT ID_NHOM, CA, MIN(PHUT_BD+TRU_DAU_GIO) PBD_ND FROM CHE_DO_LAM_VIEC 
					WHERE NGAY = (SELECT MAX(NGAY) FROM CHE_DO_LAM_VIEC WHERE NGAY <= '''+ CONVERT(nvarchar(10),@NgayBD,101) +''') AND TANG_CA = 0 GROUP BY ID_NHOM, CA 
					) T2 ON T1.ID_NHOM = T2.ID_NHOM AND T1.CA = T2.CA WHERE (T1.PBD_CC - T2.PBD_ND BETWEEN 1 AND 30)) T2 ON T1.ID_CN = T2.ID_CN'
				EXEC (@Chuoi)
				--Cap nhat ve som
				SET @Chuoi = 'UPDATE ##TBReport SET NG' + CAST(@NgayF as varchar) + 'MS = CASE WHEN (1-T1.NG' + CAST(@NgayF as varchar) + ')*480 <= 30 
					THEN ''' + '' + ''' ELSE NG' + CAST(@NgayF as varchar) + 'MS + ''' + '/' + ''' END + ''' + 'EO' + ''', 
					SO_GIO_VS = ISNULL(SO_GIO_VS,0) + ROUND(CONVERT(Float,SPVS)/60,2), SO_LAN_VS = ISNULL(SO_LAN_VS,0) + 1 
					FROM ##TBReport T1 INNER JOIN (SELECT T1.ID_CN, T2.PKT_ND - T1.PKT_CC SPVS FROM (SELECT ID_CN, ID_NHOM, CA, MAX(PHUT_VE) PKT_CC 
					FROM #CCCT WHERE NGAY = '''+ CONVERT(nvarchar(10),@NgayBD,101) + ''' AND TANG_CA = 0 GROUP BY ID_CN, ID_NHOM, CA ) T1 
					INNER JOIN (SELECT ID_NHOM, CA, MAX(PHUT_KT-TRU_CUOI_GIO) PKT_ND FROM CHE_DO_LAM_VIEC WHERE NGAY = (SELECT MAX(NGAY) FROM CHE_DO_LAM_VIEC 
					WHERE NGAY <= '''+ CONVERT(nvarchar(10),@NgayBD,101) + ''') AND TANG_CA = 0 GROUP BY ID_NHOM, CA ) T2 
					ON T1.ID_NHOM = T2.ID_NHOM AND T1.CA = T2.CA WHERE (T2.PKT_ND - T1.PKT_CC BETWEEN 1 AND 30)) T2 ON T1.ID_CN = T2.ID_CN'
				EXEC (@Chuoi)
				--cap nhat ky hieu nghi luon, nghi chua nhan viec
				SET @Chuoi = 'UPDATE ##TBReport SET NG' + CAST(@NgayF as varchar) + 'MS = CASE WHEN ISNULL(NG' + CAST(@NgayF as varchar) + ',0) = 0 THEN 
				CASE WHEN NGAY_VAO_CTY > '''+ CONVERT(nvarchar(10),@NgayBD,101) + ''' THEN ''' + 'CNV' + ''' ELSE 
				CASE WHEN NGAY_NGHI_VIEC <= '''+ CONVERT(nvarchar(10),@NgayBD,101) + ''' THEN ''' + 'OSL' + ''' ELSE NG' + CAST(@NgayF as varchar) + 'MS END END 
				ELSE CASE WHEN ISNULL(NG' + CAST(@NgayF as varchar) + 'MS,''' + '' + ''') = ''' + '' + ''' THEN CONVERT(nvarchar(10),NG' + CAST(@NgayF as varchar) + ') 
				ELSE CONVERT(varchar(10),NG' + CAST(@NgayF as varchar) + ') + ''' + '/' + ''' + NG' + CAST(@NgayF as varchar) + 'MS END END, 
				NGHI_CHUA_NV = ISNULL(NGHI_CHUA_NV,0) + CASE WHEN NGAY_VAO_CTY > ''' + CONVERT(nvarchar(10),@NgayBD,101) + ''' THEN 1 ELSE 0 END, 
				NGHI_VIEC = ISNULL(NGHI_VIEC,0) + CASE WHEN NGAY_NGHI_VIEC <= ''' + CONVERT(nvarchar(10),@NgayBD,101) + ''' THEN 1 ELSE 0 END'
				EXEC (@Chuoi)
			END
		SET @NgayBD = DATEADD(D,1,@NgayBD)
	END
	SET @DSCotNG = SUBSTRING(@DSCotNG,2,LEN(@DSCotNG)-1)
	--print @DSCotNG
	
	UPDATE ##TBReport SET CONG_DT_VS_RN = ROUND((ISNULL(SO_GIO_DT,0) + ISNULL(SO_GIO_VS,0) + ISNULL(SO_GIO_RN,0))/8,2)
    
    UPDATE ##TBReport SET TONG_NGAY_CONG = ROUND(ISNULL(NGAY_CONG,0) + ISNULL(NGHI_PN,0) + ISNULL(NGHI_BU,0) + ISNULL(NGHI_LE,0) 
    + ISNULL(NGHI_BHXH,0) + ISNULL(NGHI_CHUA_NV,0) + ISNULL(NGHI_HL,0) + ISNULL(NGHI_KL,0) + ISNULL(NGHI_VIEC,0) + ISNULL(CONG_DT_VS_RN,0),0)

	SET @Chuoi = 'SELECT ROW_NUMBER() OVER (ORDER BY STT_XN, STT_TO, MS_CN) AS STT, MS_CN, HO_TEN, CHUC_VU, TEN_XN, TEN_TO, 
	CONVERT(nvarchar(10),NGAY_VAO_CTY,101) NGAY_TV, CONVERT(nvarchar(10),NGAY_VAO_LAM,101) NGAY_VL, ' + @DSCotNG + ', NGAY_CONG, NGAY_CONG_CN, NGHI_PN, 
	NGHI_BU, NGHI_LE, NGHI_BHXH, NGHI_HL, NGHI_KL, NGHI_VIEC, CONG_DT_VS_RN, SO_LAN_DT, SO_GIO_DT, SO_LAN_VS, SO_GIO_VS, SO_LAN_RN, SO_GIO_RN, TONG_NGAY_CONG 
	FROM ##TBReport'
	EXEC (@Chuoi)
	
	DROP TABLE ##TBReport
	DROP TABLE ##TBNCLV
	DROP TABLE ##TBDSV
END

