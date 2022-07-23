 IF NOT EXISTS(SELECT 1 FROM sys.Tables WHERE  Name = N'TINH_TRANG_PV' AND Type = N'U')
	 BEGIN
	CREATE TABLE [dbo].[TINH_TRANG_PV]
	(
	[ID_TTPV] [int] NOT NULL,
	[Ten_TTPV] [nvarchar] (50)  NULL,
	[Ten_TTPV_A] [nvarchar] (50)  NULL,
	[Ten_TTPV_H] [nvarchar] (50)  NULL
	) ON [PRIMARY]
	ALTER TABLE [dbo].[TINH_TRANG_PV] ADD CONSTRAINT [TINH_TRANG_PV_ID_TTPV] PRIMARY KEY CLUSTERED (ID_TTPV) ON [PRIMARY]
	INSERT INTO dbo.TINH_TRANG_PV(ID_TTPV,Ten_TTPV,Ten_TTPV_A,Ten_TTPV_H)
	VALUES(   1,N'Đang soạn', N'Composing',N'Composing')
	INSERT INTO dbo.TINH_TRANG_PV(ID_TTPV,Ten_TTPV,Ten_TTPV_A,Ten_TTPV_H)
	VALUES(   2,N'Đang thực hiện', N'Processing',N'Processing')
	INSERT INTO dbo.TINH_TRANG_PV(ID_TTPV,Ten_TTPV,Ten_TTPV_A,Ten_TTPV_H)
	VALUES(   3,N'Đã đóng', N'Closed',N'Closed')
 END

 