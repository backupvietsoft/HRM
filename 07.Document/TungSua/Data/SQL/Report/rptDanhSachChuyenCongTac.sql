ALTER PROCEDURE [dbo].[rptDanhSachChuyenCongTac]
	@UName NVARCHAR(100) ='admin',
	@NNgu INT =0,
	@DVi INT = -1,
	@XN INT = -1,
	@TO INT = -1,
	@TNGAY Date ='20210301',
	@DNGAY Date ='20210331'
AS 
BEGIN
	DECLARE @NgayBD Date
	DECLARE @NgayKT Date
	DECLARE @NgayF Int
	DECLARE @NgayT Int

	SET @NgayBD = @TNGAY
	SET @NgayKT = @DNGAY
	SET @NgayF = 1
	SET @NgayT = 31
		
	SELECT * INTO #CN FROM dbo.MGetListNhanSuFormToDate(@UName,@NNgu, @DVi, @XN, @TO, @TNGAY, @DNGAY)
	
	--Lay danh sach chuyen to trong thang
    SELECT DISTINCT T1.ID_CN, T2.MS_CN, T2.HO_TEN, T4.ID_XN, T4.TEN_XN, T3.ID_TO, T3.TEN_TO, T2.NGAY_VAO_CTY, T2.NGAY_NGHI_VIEC
    INTO ##TBReport
    FROM CHAM_CONG T1 INNER JOIN #CN T2 ON T1.ID_CN = T2.ID_CN
    INNER JOIN [TO] T3 ON T1.ID_TO = T3.ID_TO 
    INNER JOIN [XI_NGHIEP] T4 ON T3.ID_XN = T4.ID_XN
    WHERE T1.NGAY BETWEEN @TNGAY and @DNGAY
    
    --Xoa nhung nguoi khong chuyen to
	DELETE FROM ##TBReport WHERE ID_CN IN ( SELECT ID_CN FROM ##TBReport GROUP BY ID_CN HAVING COUNT(ID_TO) =1)
	
	--lay du lieu cham cong nhưng nguoi chuyen to trong thang
	SELECT T1.*, T2.ID_TO, T2.SG_LV_QD INTO #CCCT FROM CHAM_CONG_CHI_TIET T1 
	INNER JOIN CHAM_CONG T2 ON T1.ID_CN = T2.ID_CN AND T1.NGAY = T2.NGAY
	INNER JOIN ##TBReport T3 ON T2.ID_CN = T3.ID_CN AND T2.ID_TO = T3.ID_TO
	WHERE T1.NGAY BETWEEN @TNGAY AND @DNGAY
	
	--lay du lieu cham cong vang thang
    SELECT T1.NGAY, T1.ID_CN, T4.ID_TO, T1.ID_LDV, T3.MS_LDV, T1.SG_VANG, T4.SG_LV_QD
    INTO #CCVANG
    FROM CHAM_CONG_CHI_TIET_VANG T1 INNER JOIN #CN T2 ON T1.ID_CN= T2.ID_CN 
    INNER JOIN LY_DO_VANG T3 ON T1.ID_LDV = T3.ID_LDV
    INNER JOIN CHAM_CONG T4 ON T1.ID_CN = T4.ID_CN AND T1.NGAY = T4.NGAY
    INNER JOIN ##TBReport T5 ON T2.ID_CN = T5.ID_CN AND T2.ID_TO = T5.ID_TO
    WHERE (T1.NGAY BETWEEN @TNGAY AND @DNGAY)
	
	DECLARE @Chuoi nvarchar(4000)
	DECLARE @DSCot nvarchar(4000)
	SET @Chuoi = ''
	SET @DSCot = ''

	WHILE @NgayF <= @NgayT
		BEGIN
			SET @Chuoi = 'ALTER TABLE ##TBReport ADD [NG' + CAST(@NgayF as varchar) + '] FLOAT  NULL DEFAULT (0)'
			EXEC (@Chuoi)
			SET @Chuoi = 'ALTER TABLE ##TBReport ADD [NG' + CAST(@NgayF as varchar) + 'MS] nvarchar(50)  NULL '
			EXEC (@Chuoi)
			SET @Chuoi = 'ALTER TABLE ##TBReport ADD [NG_CO' + CAST(@NgayF as varchar) + '] FLOAT  NULL DEFAULT (0)'
			EXEC (@Chuoi)
			SET @Chuoi = 'ALTER TABLE ##TBReport ADD [NG_TC' + CAST(@NgayF as varchar) + '] FLOAT  NULL DEFAULT (0)'
			EXEC (@Chuoi)
			SET @Chuoi = 'ALTER TABLE ##TBReport ADD [GIO_TTND' + CAST(@NgayF as varchar) + '] FLOAT  NULL DEFAULT (0)'
			EXEC (@Chuoi)
			
			SET @DSCot = @DSCot + ', [' + CAST(@NgayF as varchar) + ']'
			SET @NgayF = @NgayF + 1
		END

	SET @DSCot = SUBSTRING(@DSCot,2,LEN(@DSCot)-1)
		
	SET @Chuoi = 'SELECT ID_CN AS [MA_SO], ID_TO, ' + @DSCot + ' INTO ##TBNCLV FROM ( 
				SELECT ID_CN, ID_TO, NCLV, NGAY_THANG FROM (SELECT T1.ID_CN, T1.ID_TO, T1.NGAY, DAY(T1.NGAY) NGAY_THANG, 
			ROUND(GLV/ISNULL(SG_LV_QD,8),2,1) NCLV FROM (SELECT CC1.ID_CN, CC1.ID_TO, CC1.NGAY, CC1.SG_LV_QD, 
			SUM(CASE WHEN CC1.TANG_CA = 0 THEN ISNULL(CC1.SG_LV_TT,0) ELSE 0 END) AS GLV 
			FROM #CCCT CC1 GROUP BY CC1.ID_CN, CC1.ID_TO, CC1.NGAY, CC1.SG_LV_QD ) T1) P ) I PIVOT (SUM(NCLV) 
			FOR NGAY_THANG IN (' + @DSCot + ')) AS J'
	EXEC (@Chuoi)
	
	SET @Chuoi = 'UPDATE ##TBReport SET '
	SET @NgayF = 1
	DECLARE @DSCotNG nvarchar(4000)
	SET @DSCotNG = ''
	WHILE @NgayF <= @NgayT
	BEGIN
		SET @DSCotNG = @DSCotNG + ', NG' + CAST(@NgayF as varchar) + ' = ISNULL(T2.[' + CAST(@NgayF as varchar) + '],0)'
		SET @NgayF = @NgayF + 1
	END
	SET @DSCotNG = SUBSTRING(@DSCotNG,2,LEN(@DSCotNG)-1)
	
    SET @Chuoi = @Chuoi + @DSCotNG + ' FROM ##TBReport T1 INNER JOIN ##TBNCLV T2 ON T1.ID_CN = T2.MA_SO AND T1.ID_TO = T2.ID_TO'
	EXEC (@Chuoi)
	
	--cap nhat ngay cong vang
    SET @Chuoi = 'SELECT ID_CN AS [MA_SO], ID_TO, ' + @DSCot + ' INTO ##TBDSV
        FROM ( SELECT ID_CN, ID_TO, LDV, NGAY_THANG FROM ( SELECT ID_CN, ID_TO, DAY(NGAY) NGAY_THANG, 
        MS_LDV LDV FROM #CCVANG) T1 ) I PIVOT (MAX(LDV) FOR NGAY_THANG IN (' + @DSCot + ')) AS J'
    EXEC (@Chuoi)
    
    --SELECT * FROM ##TBDSV
	SET @Chuoi = 'UPDATE ##TBReport SET '
	SET @NgayF = 1
	SET @DSCotNG = ''
	WHILE @NgayF <= @NgayT
	BEGIN
		SET @DSCotNG = @DSCotNG + ', NG' + CAST(@NgayF as varchar) + 'MS = ISNULL(T2.[' + CAST(@NgayF as varchar) + '],''' + '' + ''')'
		SET @NgayF = @NgayF + 1
	END
	SET @DSCotNG = SUBSTRING(@DSCotNG,2,LEN(@DSCotNG)-1)
    
    SET @Chuoi = @Chuoi + @DSCotNG + ' FROM ##TBReport T1 INNER JOIN ##TBDSV T2 ON T1.ID_CN = T2.MA_SO AND T1.ID_TO = T2.ID_TO'
	EXEC (@Chuoi)
	
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
				--cap nhat ky hieu nghi luon, nghi chua nhan viec
				SET @Chuoi = 'UPDATE ##TBReport SET NG' + CAST(@NgayF as varchar) + 'MS = CASE WHEN ISNULL(NG' + CAST(@NgayF as varchar) + ',0) = 0 THEN 
				CASE WHEN NGAY_VAO_CTY > '''+ CONVERT(nvarchar(10),@NgayBD,101) + ''' THEN ''' + 'CNV' + ''' ELSE 
				CASE WHEN NGAY_NGHI_VIEC <= '''+ CONVERT(nvarchar(10),@NgayBD,101) + ''' THEN ''' + 'OSL' + ''' ELSE NG' + CAST(@NgayF as varchar) + 'MS END END 
				ELSE CASE WHEN ISNULL(NG' + CAST(@NgayF as varchar) + 'MS,''' + '' + ''') = ''' + '' + ''' THEN CONVERT(nvarchar(10),NG' + CAST(@NgayF as varchar) + ') 
				ELSE CONVERT(varchar(10),NG' + CAST(@NgayF as varchar) + ') + ''' + '/' + ''' + NG' + CAST(@NgayF as varchar) + 'MS END END'
				EXEC (@Chuoi)
			END
		SET @NgayBD = DATEADD(D,1,@NgayBD)
	END

	SELECT * FROM ##TBReport
	
	DROP TABLE ##TBReport
	DROP TABLE ##TBNCLV
	DROP TABLE ##TBDSV
END

    