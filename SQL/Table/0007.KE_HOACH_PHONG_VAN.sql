 IF NOT EXISTS(SELECT 1 FROM sys.Tables WHERE  Name = N'KE_HOACH_PHONG_VAN' AND Type = N'U')
	 BEGIN
CREATE TABLE [dbo].[KE_HOACH_PHONG_VAN]
(
[ID_KHPV] [bigint] NOT NULL IDENTITY(1, 1),
[SO_KHPV] [nvarchar] (50)  NOT NULL,
[TIEU_DE] [nvarchar] (150)  NOT NULL,
[NGAY_LAP] [date] NOT NULL,
[ID_YCTD] [bigint] NOT NULL,
[TINH_TRANG] [int] NOT NULL,
[GHI_CHU] [nvarchar] (250)  NULL,
[PV_ON_OF_LINE] [bit] NULL,
[NGUOI_PV_ONLINE_1] [bigint] NULL,
[NGUOI_PV_ONLINE_2] [bigint] NULL,
[NGUOI_PV_OFLINE_1] [bigint] NULL,
[NGUOI_PV_OFLINE_2] [bigint] NULL,
[NGAY_PV_ONLINE_DK] [datetime] NULL,
[NGAY_PV_OFLINE_DK] [datetime] NULL
) ON [PRIMARY]
ALTER TABLE [dbo].[KE_HOACH_PHONG_VAN] ADD CONSTRAINT [PK_KE_HOACH_PHONG_VAN_ID_KHPV] PRIMARY KEY CLUSTERED ([ID_KHPV]) ON [PRIMARY]
ALTER TABLE [dbo].[KE_HOACH_PHONG_VAN] ADD CONSTRAINT [FK_KE_HOACH_PHONG_VAN_YEU_CAU_TUYEN_DUNG] FOREIGN KEY ([ID_YCTD]) REFERENCES [dbo].[YEU_CAU_TUYEN_DUNG] ([ID_YCTD])

EXEC sp_addextendedproperty N'MS_Description', N'Số quyết định', 'SCHEMA', N'dbo', 'TABLE', N'KE_HOACH_PHONG_VAN', 'COLUMN', N'ID_KHPV'

EXEC sp_addextendedproperty N'MS_Description', N'Tiêu đề', 'SCHEMA', N'dbo', 'TABLE', N'KE_HOACH_PHONG_VAN', 'COLUMN', N'TIEU_DE'

EXEC sp_addextendedproperty N'MS_Description', N'Ngày lập', 'SCHEMA', N'dbo', 'TABLE', N'KE_HOACH_PHONG_VAN', 'COLUMN', N'NGAY_LAP'

EXEC sp_addextendedproperty N'MS_Description', N'Yêu cầu tuyển dụng', 'SCHEMA', N'dbo', 'TABLE', N'KE_HOACH_PHONG_VAN', 'COLUMN', N'ID_YCTD'

EXEC sp_addextendedproperty N'MS_Description', N'Tình trạng kế hoạch', 'SCHEMA', N'dbo', 'TABLE', N'KE_HOACH_PHONG_VAN', 'COLUMN', N'TINH_TRANG'

EXEC sp_addextendedproperty N'MS_Description', N'Ghi chú', 'SCHEMA', N'dbo', 'TABLE', N'KE_HOACH_PHONG_VAN', 'COLUMN', N'GHI_CHU'

EXEC sp_addextendedproperty N'MS_Description', N'Phỏng vấn online/offline', 'SCHEMA', N'dbo', 'TABLE', N'KE_HOACH_PHONG_VAN', 'COLUMN', N'PV_ON_OF_LINE'

EXEC sp_addextendedproperty N'MS_Description', N'Người phỏng vấn online 1', 'SCHEMA', N'dbo', 'TABLE', N'KE_HOACH_PHONG_VAN', 'COLUMN', N'NGUOI_PV_ONLINE_1'

EXEC sp_addextendedproperty N'MS_Description', N'Người phỏng vấn online 2', 'SCHEMA', N'dbo', 'TABLE', N'KE_HOACH_PHONG_VAN', 'COLUMN', N'NGUOI_PV_ONLINE_2'

EXEC sp_addextendedproperty N'MS_Description', N'Người phỏng vấn offline 1', 'SCHEMA', N'dbo', 'TABLE', N'KE_HOACH_PHONG_VAN', 'COLUMN', N'NGUOI_PV_OFLINE_1'

EXEC sp_addextendedproperty N'MS_Description', N'Người phỏng vấn offline 2', 'SCHEMA', N'dbo', 'TABLE', N'KE_HOACH_PHONG_VAN', 'COLUMN', N'NGUOI_PV_OFLINE_2'

EXEC sp_addextendedproperty N'MS_Description', N'Ngày phỏng vấn online', 'SCHEMA', N'dbo', 'TABLE', N'KE_HOACH_PHONG_VAN', 'COLUMN', N'NGAY_PV_ONLINE_DK'

EXEC sp_addextendedproperty N'MS_Description', N'Ngày phỏng vấn offline', 'SCHEMA', N'dbo', 'TABLE', N'KE_HOACH_PHONG_VAN', 'COLUMN', N'NGAY_PV_OFLINE_DK'

END

