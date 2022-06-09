
CREATE PROCEDURE [dbo].[spSaveDuLieuQuetTheTay]
	@Ngay DATE = '2020-02-01',
	@ID_NHOM INT,
	@CA NVARCHAR(4),
	@NGAY_DEN DATETIME,
    @GIO_DEN DATETIME,
    @PHUT_DEN DECIMAL(18,2),
    @NGAY_VE DATETIME,
    @GIO_VE DATETIME,
	@PHUT_VE DECIMAL(18,2),
	@sBT NVARCHAR(50) = 'rptTienThuongXepLoai'
AS	
BEGIN
CREATE TABLE #BT
(
	[CHON] [bit] NULL,
	[ID_CN] [bigint] NULL,
	[MS_CN] [nvarchar] (20)  NULL,
	[HO_TEN] [nvarchar] (71)  NULL,
	[ID_NHOM] [int] NULL
)
--[spXepLoaiKhenThuong]---------------------------------------------------------------------------------------------------
DECLARE @sSql  NVARCHAR(400)
SET @sSql = 'INSERT INTO #BT(ID_CN,MS_CN,HO_TEN,ID_NHOM) SELECT ID_CN,MS_CN,HO_TEN,ID_NHOM FROM '+ @sBT +' WHERE CHON = 1'
EXEC(@sSql)
SET @sSql = 'DROP TABLE ' + @sBT 
EXEC(@sSql)
--------------------------------------------------------------------------------------------------------------------------
DELETE DU_LIEU_QUET_THE WHERE CONVERT(DATE,@Ngay) = CONVERT(DATE,NGAY) AND EXISTS (SELECT * FROM #BT WHERE dbo.DU_LIEU_QUET_THE.ID_CN = #BT.ID_CN)

INSERT INTO dbo.DU_LIEU_QUET_THE
(
    ID_CN,
    NGAY,
    ID_NHOM,
    CA,
    NGAY_DEN,
    GIO_DEN,
    PHUT_DEN,
    NGAY_VE,
    GIO_VE,
    PHUT_VE,
    CHINH_SUA
)
SELECT ID_CN,@Ngay,@ID_NHOM,@CA,@NGAY_DEN,@GIO_DEN,@PHUT_DEN,@NGAY_VE,@GIO_VE,@PHUT_VE,1 FROM #BT
--------------------------------------------------------------------------------------------------------------------------


END	

