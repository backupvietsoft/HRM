IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'spSaveKeHoachTuyenDung')
   exec('CREATE PROCEDURE spSaveKeHoachTuyenDung AS BEGIN SET NOCOUNT ON; END')
GO
ALTER PROCEDURE spSaveKeHoachTuyenDung
    @Thang DATETIME ='2022-07-16 09:11:39.240',
	@sBTKHT NVARCHAR(250) ='sBTKHTadmin',
	@sBTNT NVARCHAR(250) ='sBTNTadmin'
AS
BEGIN
  	CREATE TABLE #TEMPT_KHT(
	[ID_YCTD] [bigint] NULL,
	[ID_VTTD] [bigint] NULL,
	[THANG] [datetime] NULL,
	[TUAN] [int] NULL,
	[TEN_TUAN] [NVARCHAR](250)  NULL,
	[TNgay] [datetime] NULL,
	[DNgay] [datetime] NULL,
	[SL_KH] [int] NULL,
	[Note] [NVARCHAR](500)  NULL
	) ON [PRIMARY] 
	DECLARE @sSql NVARCHAR(1000)

	set @sSql = 'INSERT #TEMPT_KHT(ID_YCTD,ID_VTTD,THANG,TUAN,TEN_TUAN,TNgay,DNgay,SL_KH,Note)
	SELECT ID_YCTD,ID_VTTD,THANG,TUAN,TEN_TUAN,TNgay,DNgay,SL_KH,Note FROM ' + @sBTKHT
	EXEC (@sSql)
	set @sSql = 'DROP TABLE ' + @sBTKHT
	EXEC (@sSql)

	DELETE dbo.KHTD_TUAN WHERE RIGHT(CONVERT(NVARCHAR(12),THANG,103),7) =RIGHT(CONVERT(NVARCHAR(12),@Thang,103),7)
	
	INSERT INTO dbo.KHTD_TUAN(ID_YCTD,ID_VTTD,THANG,TUAN,TU_NGAY,DEN_NGAY,SL_KH,Note)
	SELECT ID_YCTD,ID_VTTD,THANG,TUAN,TNgay,DNgay,SL_KH,Note FROM #TEMPT_KHT WHERE RIGHT(CONVERT(NVARCHAR(12),THANG,103),7) =RIGHT(CONVERT(NVARCHAR(12),@Thang,103),7)

  	CREATE TABLE #TEMPT_NT(
	[ID_YCTD] [bigint] NULL,
	[ID_VTTD] [bigint] NULL,
	[ID_NTD] [bigint] NULL,
	[Ghi_Chu] [nvarchar] (500) NULL
	) ON [PRIMARY] 

	set @sSql = 'INSERT INTO #TEMPT_NT(ID_YCTD,ID_VTTD,ID_NTD,Ghi_Chu)
	SELECT ID_YCTD,ID_VTTD,ID_NTD,Ghi_Chu FROM ' + @sBTNT +' WHERE  ISNULL(ID_NTD,'''') !='''' '
	EXEC (@sSql)
	set @sSql = 'DROP TABLE ' + @sBTNT
	EXEC (@sSql)

	SELECT * FROM dbo.BAC_LUONG 
	
	DELETE A
	FROM dbo.KHTD_NTD A
	INNER JOIN (SELECT DISTINCT ID_YCTD,ID_VTTD FROM #TEMPT_NT) B ON A.ID_YCTD = B.ID_YCTD AND A.ID_VTTD = B.ID_VTTD

	INSERT INTO dbo.KHTD_NTD(ID_YCTD,ID_VTTD,ID_NTD,Ghi_Chu)
	SELECT ID_YCTD,ID_VTTD,ID_NTD,Ghi_Chu FROM #TEMPT_NT

END
GO
