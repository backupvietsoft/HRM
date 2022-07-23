 IF NOT EXISTS(SELECT 1 FROM sys.Tables WHERE  Name = N'TINH_TRANG_TD' AND Type = N'U')
	 BEGIN
	CREATE TABLE [dbo].[TINH_TRANG_TD]
	(
	[ID_TTTD] [int] NOT NULL,
	[Ten_TTTD] [nvarchar] (50)  NULL,
	[Ten_TTTD_A] [nvarchar] (50)  NULL,
	[Ten_TTTD_H] [nvarchar] (50)  NULL
	) ON [PRIMARY]

	ALTER TABLE [dbo].[TINH_TRANG_TD] ADD CONSTRAINT [TINH_TRANG_TD_ID_TTTD] PRIMARY KEY CLUSTERED (ID_TTTD) ON [PRIMARY]

	INSERT INTO dbo.TINH_TRANG_TD(ID_TTTD,Ten_TTTD,Ten_TTTD_A,Ten_TTTD_H)
	VALUES(   1,N'Chưa tuyển dụng', N'Not recruiting yet',N'Not recruiting yet')
	INSERT INTO dbo.TINH_TRANG_TD(ID_TTTD,Ten_TTTD,Ten_TTTD_A,Ten_TTTD_H)
	VALUES(   2,N'Đã tuyển dụng', N'Recruited',N'Recruited')
	INSERT INTO dbo.TINH_TRANG_TD(ID_TTTD,Ten_TTTD,Ten_TTTD_A,Ten_TTTD_H)
	VALUES(   3,N'Chưa nộp hồ sơ', N'Not send file',N'Not send file')
 END

 