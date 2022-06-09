ALTER PROCEDURE [dbo].[spXepLoaiKhenThuong]
	@ThangXL DATE = '2021-09-01',
	@DVi BIGINT = -1,
	@XNghiep BIGINT = -1,
	@To BIGINT = -1,
	@UName NVARCHAR(50) = 'admin',
	@NNgu INT = 1,
	@sBT NVARCHAR(50) = 'XLKTadmin',
	@Loai NVARCHAR(50) = 'LXL'
AS	
--Cbo Load cbo Thang 
--Grd Load luoi khen thuong
--LXL Load Loai xep loai
--Add Load luoi khi them sua

SELECT DISTINCT * INTO #TOUSER FROM dbo.MGetToUser(@UName,@NNgu) WHERE (ID_XN = @XNghiep OR @XNghiep = -1) AND (ID_TO = @To OR @To = -1)  AND (ID_DV = @DVi OR @DVi = -1) 

IF UPPER(@Loai) = UPPER('Cbo')
BEGIN
	SELECT DISTINCT TOP 20	RIGHT(CONVERT(VARCHAR(10),THANG_KTT,103),7) T_NAME,THANG_KTT FROM dbo.KHEN_THUONG_THANG T1 INNER JOIN dbo.CONG_NHAN T2 ON T1.ID_CN = T2.ID_CN INNER JOIN #TOUSER T4 ON T2.ID_TO = T4.ID_TO
	ORDER BY THANG_KTT DESC

END

--Get luoi 
IF UPPER(@Loai) = UPPER('Grd')
BEGIN

DECLARE @sName NVARCHAR(4000) = N'Tổng cộng : '
IF @NNgu = 1 SET  @sName = N'Total'

SELECT     @sName = ISNULL(@sName,'')  + CONVERT(NVARCHAR(10),ISNULL(TS,0)) + ' (' + ISNULL(TEN_LXL,'') + ')' + '; '
FROM(SELECT CASE @NNgu WHEN 0 THEN TEN_LXL WHEN 1 THEN ISNULL(NULLIF(TEN_LXL_A,''),TEN_LXL) ELSE ISNULL(NULLIF(TEN_LXL_H,''),TEN_LXL) END AS TEN_LXL,COUNT(T2.TEN_LXL) TS FROM dbo.KHEN_THUONG_THANG T1 INNER JOIN dbo.LOAI_XEP_LOAI T2 ON T2.ID_LXL = T1.ID_LXL WHERE T1.THANG_KTT = @ThangXL GROUP BY TEN_LXL,T2.TEN_LXL_A,T2.TEN_LXL_H) T



	SELECT T2.ID_CN,T2.MS_CN, T2.HO_TEN,T1.ID_LXL,T1.GHI_CHU,LEFT(@sName,LEN(@sName)-1) AS TS FROM dbo.KHEN_THUONG_THANG T1 INNER JOIN (SELECT DISTINCT ID_CN, MS_CN,HO,TEN,HO_TEN FROM dbo.MGetListNhanSu(@DVi,@XNghiep,@To,-1,@UName,@NNgu)) T2 ON T1.ID_CN = T2.ID_CN  WHERE THANG_KTT = @ThangXL    ORDER BY T2.MS_CN, T2.HO,T2.TEN
END

--LOAI_XEP_LOAI
IF UPPER(@Loai) = UPPER('LXL')
BEGIN
	SELECT T1.ID_LXL,CASE @NNgu WHEN 0 THEN T1.TEN_LXL WHEN 1 THEN ISNULL(NULLIF(T1.TEN_LXL_A,''),T1.TEN_LXL) 
	ELSE ISNULL(NULLIF(T1.TEN_LXL_H,''),T1.TEN_LXL) END AS TEN_LXL FROM dbo.LOAI_XEP_LOAI T1  
	WHERE T1.THANG_LXL = (SELECT MAX(THANG_LXL) FROM dbo.LOAI_XEP_LOAI WHERE THANG_LXL =  @ThangXL ) 
	UNION SELECT	-1 , NULL
	ORDER BY TEN_LXL
END


--Get luoi Them Sua
IF UPPER(@Loai) = UPPER('Add')
BEGIN
	DECLARE @TNgay Date
	DECLARE @DNgay Date
	
	SET @TNgay = @ThangXL
	SET @DNgay = DATEADD(M,1,@TNgay)	 
	SET @DNgay = DATEADD(D,-1,@DNgay)
	
	SELECT T1.ID_CN,T1.MS_CN,HO_TEN, T2.ID_LXL,T2.GHI_CHU,''AS TS  FROM dbo.MGetListNhanSuFormToDate(@UName,@NNgu,@DVi,@XNghiep,@TO,@TNgay,@DNgay) T1 LEFT JOIN (
	SELECT * FROM KHEN_THUONG_THANG WHERE THANG_KTT = (SELECT MAX(THANG_KTT) AS THANG_MAX FROM KHEN_THUONG_THANG  WHERE THANG_KTT = @ThangXL)
	) T2 ON T1.ID_CN= T2.ID_CN ORDER BY T1.MS_CN
END

--Save [spXepLoaiKhenThuong]
IF UPPER(@Loai) = UPPER('Save')
BEGIN
CREATE TABLE #BT
(
	ID_CN BIGINT,
	ID_LXL BIGINT,
	GHI_CHU NVARCHAR(255)
)

--[spXepLoaiKhenThuong]
DECLARE @sSql  NVARCHAR(400)
SET @sSql = 'INSERT	INTO	#BT(ID_CN, ID_LXL,GHI_CHU) SELECT ID_CN,ID_LXL,GHI_CHU FROM ' + @sBT  + ' WHERE ISNULL(ID_LXL,'''') <> '''' '
EXEC(@sSql)

SET @sSql = 'DROP TABLE ' + @sBT 
EXEC(@sSql)

UPDATE dbo.KHEN_THUONG_THANG SET ID_LXL = T2.ID_LXL,GHI_CHU = T2.GHI_CHU FROM KHEN_THUONG_THANG T1 INNER JOIN #BT T2 ON T2.ID_CN = T1.ID_CN WHERE T1.THANG_KTT = @ThangXL AND T2.ID_LXL <> -1	

INSERT INTO	dbo.KHEN_THUONG_THANG(ID_CN, ID_LXL, THANG_KTT, GHI_CHU)
SELECT ID_CN, ID_LXL, @ThangXL AS THANG_KTT, GHI_CHU FROM #BT T2 
WHERE  NOT  EXISTS (SELECT * FROM dbo.KHEN_THUONG_THANG T1 WHERE T2.ID_CN = T1.ID_CN AND T1.THANG_KTT = @ThangXL ) 
AND T2.ID_LXL <> -1	


DELETE T1 FROM dbo.KHEN_THUONG_THANG T1 INNER JOIN #BT T2 ON T1.ID_CN = T2.ID_CN WHERE THANG_KTT = @ThangXL AND T2.ID_LXL = -1	  

END


--delete  @DVi thế id cong nhân
IF UPPER(@Loai) = UPPER('Delete')
BEGIN
	DELETE dbo.KHEN_THUONG_THANG WHERE	(ID_CN = @DVi OR @DVi = -1) AND (THANG_KTT = @ThangXL)
	SELECT * FROM KHEN_THUONG_THANG
	DBCC CHECKIDENT (KHEN_THUONG_THANG,RESEED,0)
	DBCC CHECKIDENT (KHEN_THUONG_THANG,RESEED)
END	

