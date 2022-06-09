ALTER PROCEDURE [dbo].[rptBangCongTCThang]
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
	
	SELECT * INTO #CN FROM dbo.MGetListNhanSuFormToDate(@UName,@NNgu, @DVi, @XN, @TO, @TNGAY, @DNGAY) ORDER BY MS_CN

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
			SET @Chuoi = @Chuoi + ' [NG' + CAST(@NgayF as varchar) + 'MS] nvarchar(50) NULL , '
			SET @NgayF = @NgayF + 1
		END
			
	SET @Chuoi = @Chuoi + ' [TC_NGAY] FLOAT NULL  DEFAULT (0), [TC_DEM] FLOAT NULL  DEFAULT (0), 
		[TC_CN] FLOAT NULL  DEFAULT (0), [TC_CN_DEM] FLOAT NULL  DEFAULT (0), [GIO_CD] FLOAT NULL  DEFAULT (0), [TC_CD] FLOAT NULL  DEFAULT (0), 
		[TONG_TC_NGAY] FLOAT NULL  DEFAULT (0),	[TONG_TC_CN] FLOAT NULL  DEFAULT (0), 
		STT_XN INT  DEFAULT (999), STT_TO INT  DEFAULT (999) , STT_CV INT  DEFAULT (999) )'
	EXEC (@Chuoi)
	
	--tao danh sach cham cong can bao cao
    INSERT INTO ##TBReport(STT_XN, STT_TO, ID_CN, ID_DV, TEN_DV, ID_XN, TEN_XN, ID_TO, TEN_TO, MS_CN, HO_TEN, ID_CVU, 
    CHUC_VU, NGAY_VAO_CTY, NGAY_VAO_LAM, NGAY_NGHI_VIEC, NGAY_SINH) 
    SELECT T1.STT_XN, T1.STT_TO, T1.ID_CN, T1.ID_DV, T1.TEN_DV, T1.ID_XN, T1.TEN_XN, T1.ID_TO, T1.TEN_TO, T1.MS_CN, T1.HO_TEN, T1.ID_CV, T2.TEN_CV, 
    ISNULL(T1.NGAY_THU_VIEC,T1.NGAY_HOC_VIEC), T1.NGAY_VAO_LAM, T1.NGAY_NGHI_VIEC, T1.NGAY_SINH 
    FROM #CN T1 INNER JOIN CHUC_VU T2 ON T1.ID_CV = T2.ID_CV
    
    --tao danh sach cn tang ca thang
    SELECT T1.* INTO ##CCTC
    FROM CHAM_CONG_CHI_TIET T1 INNER JOIN #CN T2 ON T1.ID_CN = T2.ID_CN
    WHERE T1.NGAY BETWEEN @TNGAY AND @DNGAY AND TANG_CA = 1
    
	DECLARE @DSCot nvarchar(4000)
	SET @DSCot = ''
	SET @NgayF = 1
    WHILE @NgayF <= @NgayT
		BEGIN
			SET @DSCot = @DSCot + ', [' + CAST(@NgayF as varchar) + ']'
			SET @NgayF = @NgayF + 1
		END
	SET @DSCot = SUBSTRING(@DSCot,2,LEN(@DSCot)-1)
	PRINT @DSCot
		
    SET @Chuoi = 'SELECT ID_CN AS [MA_SO], ' + @DSCot + ' INTO ##TBSGTC FROM ( 
			SELECT ID_CN, SG_TC, NGAY_THANG FROM (SELECT T1.ID_CN, T1.NGAY, DAY(T1.NGAY) NGAY_THANG, 
			SG_TC FROM (
				SELECT NGAY, ID_CN, SUM(FLOOR(SG_LV_TT) + CASE WHEN (SG_LV_TT - FLOOR(SG_LV_TT))*60 < 15 THEN 0 ELSE 
				CASE WHEN (SG_LV_TT - FLOOR(SG_LV_TT))*60 < 45 THEN 0.5 ELSE 1 END END) SG_TC 
				FROM ##CCTC GROUP BY NGAY, ID_CN
			) T1) P ) I PIVOT (SUM(SG_TC) 
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
	
    SET @Chuoi = @Chuoi + @DSCotNG + ' FROM ##TBReport T1 INNER JOIN ##TBSGTC T2 ON T1.ID_CN = T2.MA_SO'
	EXEC (@Chuoi)
	
	--lay du lieu tang ca ngay thuong cham cong thang
	UPDATE ##TBReport SET TC_NGAY = T2.SG_TC FROM ##TBReport T1 INNER JOIN (SELECT ID_CN, 
	ROUND(SUM(FLOOR(SG_LV_TT) + CASE WHEN (SG_LV_TT - FLOOR(SG_LV_TT))*60 < 15 THEN 0 
	ELSE CASE WHEN (SG_LV_TT - FLOOR(SG_LV_TT))*60 < 45 THEN 0.5 ELSE 1 END END),1) SG_TC 
    FROM ##CCTC T1 WHERE TANG_CA = 1 AND ISNULL(TC_DEM,0) = 0 AND ISNULL(CA_DEM,0) = 0 AND ISNULL(LOAI_GIO,0) = 0
    GROUP BY ID_CN) T2 ON T1.ID_CN = T2.ID_CN 	
 
	--lay du lieu tang ca dem thuong cham cong thang
	UPDATE ##TBReport SET TC_DEM = T2.SG_TC FROM ##TBReport T1 INNER JOIN (SELECT ID_CN, 
	ROUND(SUM(FLOOR(SG_LV_TT) + CASE WHEN (SG_LV_TT - FLOOR(SG_LV_TT))*60 < 15 THEN 0 
	ELSE CASE WHEN (SG_LV_TT - FLOOR(SG_LV_TT))*60 < 45 THEN 0.5 ELSE 1 END END),1) SG_TC 
    FROM ##CCTC T1 WHERE TANG_CA = 1 AND ISNULL(T1.TC_DEM,0) = 1 AND ISNULL(T1.CA_DEM,0) = 0 AND ISNULL(T1.LOAI_GIO,0) = 0
    GROUP BY ID_CN) T2 ON T1.ID_CN = T2.ID_CN 
        
    --lay du lieu tang ca chu nhat thuong cham cong thang
    UPDATE ##TBReport SET TC_CN = T2.SG_TC FROM ##TBReport T1 INNER JOIN (SELECT ID_CN, 
	ROUND(SUM(FLOOR(SG_LV_TT) + CASE WHEN (SG_LV_TT - FLOOR(SG_LV_TT))*60 < 15 THEN 0 
	ELSE CASE WHEN (SG_LV_TT - FLOOR(SG_LV_TT))*60 < 45 THEN 0.5 ELSE 1 END END),1) SG_TC 
    FROM ##CCTC T1 WHERE TANG_CA = 1 AND ISNULL(T1.TC_DEM,0) = 0 AND ISNULL(T1.CA_DEM,0) = 0 AND ISNULL(T1.LOAI_GIO,0) = 1
    GROUP BY ID_CN) T2 ON T1.ID_CN = T2.ID_CN 
    
    --lay du lieu tang ca chu nhat dem thuong cham cong thang
    UPDATE ##TBReport SET TC_CN_DEM = T2.SG_TC FROM ##TBReport T1 INNER JOIN (SELECT ID_CN, 
	ROUND(SUM(FLOOR(SG_LV_TT) + CASE WHEN (SG_LV_TT - FLOOR(SG_LV_TT))*60 < 15 THEN 0 
	ELSE CASE WHEN (SG_LV_TT - FLOOR(SG_LV_TT))*60 < 45 THEN 0.5 ELSE 1 END END),1) SG_TC 
    FROM ##CCTC T1 WHERE TANG_CA = 1 AND ISNULL(T1.TC_DEM,0) = 1 AND ISNULL(T1.CA_DEM,0) = 0 AND ISNULL(T1.LOAI_GIO,0) = 1
    GROUP BY ID_CN) T2 ON T1.ID_CN = T2.ID_CN 

    --lay du lieu tang ca ca dem cham cong thang
    UPDATE ##TBReport SET TC_CD = T2.SG_TC FROM ##TBReport T1 INNER JOIN (SELECT ID_CN, 
	ROUND(SUM(FLOOR(SG_LV_TT) + CASE WHEN (SG_LV_TT - FLOOR(SG_LV_TT))*60 < 15 THEN 0 
	ELSE CASE WHEN (SG_LV_TT - FLOOR(SG_LV_TT))*60 < 45 THEN 0.5 ELSE 1 END END),1) SG_TC 
    FROM ##CCTC T1 WHERE TANG_CA = 1 AND ISNULL(T1.TC_DEM,0) = 1 AND ISNULL(T1.CA_DEM,0) = 1
    GROUP BY ID_CN) T2 ON T1.ID_CN = T2.ID_CN 
    
    --tinh gio lam ca dem
    UPDATE ##TBReport SET GIO_CD = T2.SGCD FROM ##TBReport T1 INNER JOIN (	SELECT T1.ID_CN, SUM(CASE WHEN T1.PHUT_DEN < 1320 THEN T1.PHUT_VE - 1320 
    ELSE T1.PHUT_VE - T1.PHUT_DEN END)/60 SGCD 
	FROM CHAM_CONG_CHI_TIET T1 INNER JOIN #CN T2 ON T1.ID_CN = T2.ID_CN 
	WHERE T1.NGAY BETWEEN  @TNGAY AND @DNGAY AND CA_DEM = 1 And TANG_CA = 0 And PHUT_VE > 1320 GROUP BY T1.ID_CN) T2 ON T1.ID_CN = T2.ID_CN 
	
	UPDATE ##TBReport SET TONG_TC_NGAY = ISNULL(TC_NGAY,0) + ISNULL(TC_DEM,0), TONG_TC_CN = ISNULL(TC_CN,0) + ISNULL(TC_CN_DEM,0)
	
	DECLARE @DSCotNGMS nvarchar(4000)
	SET @DSCotNGMS = ''
	SET @NgayF = 1
	WHILE @NgayF <= @NgayT
	BEGIN
		SET @DSCotNGMS = @DSCotNGMS + ', NG' + CAST(@NgayF as varchar) 
		SET @NgayF = @NgayF + 1
	END
	SET @DSCotNGMS = SUBSTRING(@DSCotNGMS,2,LEN(@DSCotNGMS)-1)
	
	SET @Chuoi = 'SELECT ROW_NUMBER() OVER (ORDER BY STT_XN, STT_TO, MS_CN) AS STT, MS_CN, HO_TEN, CHUC_VU, TEN_XN, TEN_TO, 
		CONVERT(nvarchar(10),NGAY_VAO_CTY,101) NGAY_TV, CONVERT(nvarchar(10),NGAY_VAO_LAM,101) NGAY_VL, ' + @DSCotNGMS + ', 
		TC_NGAY, TC_DEM, TC_CN, TC_CN_DEM, GIO_CD, TC_CD, TONG_TC_NGAY, TONG_TC_CN 
		FROM ##TBReport ORDER BY STT_XN, STT_TO, MS_CN'
	EXEC (@Chuoi) 
	
    DROP TABLE ##TBSGTC
    DROP TABLE ##CCTC
	DROP TABLE ##TBReport
    
END
