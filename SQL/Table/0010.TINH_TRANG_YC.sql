﻿ IF NOT EXISTS(SELECT 1 FROM sys.Tables WHERE  Name = N'TINH_TRANG_YC' AND Type = N'U')
	 BEGIN
	CREATE TABLE [dbo].[TINH_TRANG_YC]
	(
	[ID_TTYC] [int] NOT NULL,
	[Ten_TTYC] [nvarchar] (50)  NULL,
	[Ten_TTYC_A] [nvarchar] (50)  NULL,
	[Ten_TTYC_H] [nvarchar] (50)  NULL
	) ON [PRIMARY]
	ALTER TABLE [dbo].[TINH_TRANG_YC] ADD CONSTRAINT [TINH_TRANG_YC_ID_TTYC] PRIMARY KEY CLUSTERED (ID_TTYC) ON [PRIMARY]
	INSERT INTO dbo.TINH_TRANG_YC(ID_TTYC,Ten_TTYC,Ten_TTYC_A,Ten_TTYC_H)
	VALUES(   1,N'Đang thực hiện', N'Processing',N'Processing')
	INSERT INTO dbo.TINH_TRANG_YC(ID_TTYC,Ten_TTYC,Ten_TTYC_A,Ten_TTYC_H)
	VALUES(   2,N'Đã khóa', N'Closed',N'Closed')
 END

 