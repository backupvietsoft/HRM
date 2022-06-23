
 IF NOT EXISTS(SELECT 1 FROM sys.Tables WHERE  Name = N'TINH_TRANG' AND Type = N'U')
	 BEGIN
	CREATE TABLE [dbo].[TINH_TRANG]
	(
	[ID_TT] [int] NOT NULL,
	[TenTT] [nvarchar] (50)  NULL,
	[TenTT_A] [nvarchar] (50)  NULL,
	[TenTT_H] [nvarchar] (50)  NULL
	) ON [PRIMARY]

	ALTER TABLE [dbo].[TINH_TRANG] ADD CONSTRAINT [PK_TINH_TRANG] PRIMARY KEY CLUSTERED ([ID_TT]) ON [PRIMARY]

	INSERT INTO dbo.TINH_TRANG(ID_TT,TenTT,TenTT_A,TenTT_H)
	VALUES(   1,N'Đang soạn', N'Composing',N'Composing')
	INSERT INTO dbo.TINH_TRANG(ID_TT,TenTT,TenTT_A,TenTT_H)
	VALUES(   2,N'Đã ký', N'Signed',N'Signed')
	INSERT INTO dbo.TINH_TRANG(ID_TT,TenTT,TenTT_A,TenTT_H)
	VALUES(   3,N'Đã hủy', N'cancelled',N'cancelled')
 END


 