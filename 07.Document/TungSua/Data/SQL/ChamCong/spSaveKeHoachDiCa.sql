ALTER PROCEDURE [dbo].[spSaveKeHoachDiCa]
	@Nam Int = 2021,
	@BangTam NVARCHAR(50) = 'grvKeHoachDiCaAdmin'
AS	
BEGIN
CREATE TABLE #BT
(
	[ID_CN] [int] NULL,
	[ID_NHOM] [INT]  NULL,
	[CA] [NVARCHAR](5)  NULL,
	[TU_NGAY] [datetime] NULL,
	[DEN_NGAY] [datetime] NULL,
	[GHI_CHU] [NVARCHAR] (250) NULL
)
--[spXepLoaiKhenThuong]
	DECLARE @sSql  NVARCHAR(400)
	SET @sSql = 'INSERT INTO #BT(ID_CN, ID_NHOM, CA, TU_NGAY, DEN_NGAY, GHI_CHU)
		SELECT ID_CN, ID_NHOM, CA, TU_NGAY, DEN_NGAY, GHI_CHU 
		FROM ' + @BangTam
	EXEC(@sSql)
	SET @sSql = 'DROP TABLE ' + @BangTam
	EXEC(@sSql)
-------------------------------------------------------------------------------

	DELETE FROM KE_HOACH_DI_CA WHERE (YEAR(TU_NGAY) = @Nam) AND ID_CN IN (SELECT DISTINCT ID_CN FROM #BT)  
	   
	INSERT INTO KE_HOACH_DI_CA (ID_CN, ID_NHOM, CA, TU_NGAY, DEN_NGAY, GHI_CHU)  
	SELECT ID_CN, ID_NHOM, CA, TU_NGAY, DEN_NGAY, GHI_CHU
	FROM #BT 
	WHERE ISNULL(ID_NHOM,'')!='' AND ISNULL(CA,'') != '' 
END
