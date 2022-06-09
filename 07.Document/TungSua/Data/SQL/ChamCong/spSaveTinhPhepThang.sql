ALTER PROCEDURE [dbo].[spSaveTinhPhepThang]

	@BangTam NVARCHAR(1000) = 'sBTPhepThangadmin',
	@Thang datetime = '20210501'
AS	
BEGIN
CREATE TABLE #BT(
	[THANG]	[datetime] NULL,
	[ID_CN]	[int]	NOT NULL,
	[PHEP_TN]	[float]	NULL,
	[PHEP_UNG]	[float]	NULL,
	[PHEP_DA_NGHI]	[float]	NULL,
	[PHEP_TIEU_CHUAN]	[float]	NULL,
	[PHEP_TON]	[float]	NULL,
	[SO_THANG_LV]	[float]	NULL,
	[T_1]	[float]	NULL,
	[T_2]	[float]	NULL,
	[T_3]	[float]	NULL,
	[T_4]	[float]	NULL,
	[T_5]	[float]	NULL,
	[T_6]	[float]	NULL,
	[T_7]	[float]	NULL,
	[T_8]	[float]	NULL,
	[T_9]	[float]	NULL,
	[T_10]	[float]	NULL,
	[T_11]	[float]	NULL,
	[T_12]	[float]	NULL,
	[TT_1]	[float]	NULL,
	[TT_2]	[float]	NULL,
	[TT_3]	[float]	NULL,
	[TT_4]	[float]	NULL,
	[TT_5]	[float]	NULL,
	[TT_6]	[float]	NULL,
	[TT_7]	[float]	NULL,
	[TT_8]	[float]	NULL,
	[TT_9]	[float]	NULL,
	[TT_10]	[float]	NULL,
	[TT_11]	[float]	NULL,
	[TT_12]	[float]	NULL
	)

--[spXepLoaiKhenThuong]
DECLARE @sSql  NVARCHAR(2000)
SET @sSql = 'INSERT INTO #BT([ID_CN], [PHEP_TN], [PHEP_UNG], [PHEP_DA_NGHI],[PHEP_TIEU_CHUAN],[PHEP_TON],[SO_THANG_LV],[T_1],[T_2],[T_3],[T_4],[T_5],[T_6],
	[T_7], [T_8], [T_9], [T_10], [T_11], [T_12], [TT_1],[TT_2],[TT_3],[TT_4],[TT_5],[TT_6], [TT_7], [TT_8], [TT_9], [TT_10], [TT_11], [TT_12] ) 
	SELECT ID_CN, PHEP_THAM_NIEN, PHEP_UNG_TRUOC, PHEP_DA_NGHI, PHEP_TIEU_CHUAN, PHEP_CON_LAI, SO_THANG_LV, T_1, T_2, T_3, T_4, T_5, T_6, T_7, T_8, T_9, T_10, T_11, T_12, 
	TT_1, TT_2, TT_3, TT_4, TT_5, TT_6, TT_7, TT_8, TT_9, TT_10, TT_11, TT_12 FROM '+@BangTam
	

EXEC(@sSql)
SELECT * FROM #BT
SET @sSql = 'DROP TABLE ' + @BangTam 
EXEC(@sSql)
-------------------------------------------------------------------------------
	DELETE A FROM PHEP_THANG A INNER JOIN #BT B ON A.ID_CN=B.ID_CN
	WHERE A.ID_CN=B.ID_CN AND A.THANG=@Thang
	
	INSERT INTO PHEP_THANG([THANG], [ID_CN], [PHEP_TN], [PHEP_UNG], [PHEP_DA_NGHI],[PHEP_TIEU_CHUAN],[PHEP_TON],[SO_THANG_LV],[T_1],[T_2],[T_3],[T_4],[T_5],[T_6],
	[T_7], [T_8], [T_9], [T_10], [T_11], [T_12], [TT_1],[TT_2],[TT_3],[TT_4],[TT_5],[TT_6], [TT_7], [TT_8], [TT_9], [TT_10], [TT_11], [TT_12]) 
	SELECT ISNULL(THANG,@Thang) THANG, ID_CN, PHEP_TN, PHEP_UNG, PHEP_DA_NGHI, PHEP_TIEU_CHUAN, PHEP_TON, SO_THANG_LV, T_1, T_2, T_3, T_4, T_5, T_6, T_7, T_8, T_9, T_10, T_11, T_12, 
	TT_1, TT_2, TT_3, TT_4, TT_5, TT_6, TT_7, TT_8, TT_9, TT_10, TT_11, TT_12
	FROM #BT where ISNULL(PHEP_TN,'')!='' OR ISNULL(PHEP_UNG,'')!='' OR  ISNULL(PHEP_DA_NGHI,'')!='' OR ISNULL(PHEP_TIEU_CHUAN,'')!='' OR
	 ISNULL(PHEP_TON,'')!='' OR ISNULL(SO_THANG_LV,'')!=''
	
END

--select*from PHEP_THANG WHERE THANG='20210501'