ALTER PROCEDURE [dbo].[spUpdateTaiNanLD]
	@ID_TNLD BIGINT,
	@ID_CN  bigint,
	@ID_NGUYEN_NHAN  bigint,
	@ID_GAY_TAI_NAN  bigint,
	@ID_NGHE_NGHIEP  bigint,
	@NGAY_BI_TAI_NAN  date,
	@GIO_BI_TAI_NAN  TIME,
	@NOI_XAY_RA  nvarchar(250),
	@TINH_TRANG  nvarchar(250),
	@GIAI_QUYET  nvarchar(250),
	@NGAY_CAP_CUU_TAI_CHO  date,
	@GIO_CAP_CUU_TAI_CHO  TIME,
	@PHUONG_PHAP_CAP_CUU  nvarchar(250),
	@SO_NGAY_NGHI  float,
	@KET_QUA_QDINH  nvarchar(250),
	@MUC_DO  tinyint,
	@GTRI_TB_THIET_HAI  float,
	@NGAY_VAO_VIEN  date,
	@NGAY_RA_VIEN  date,
	@DIEN_GIAI  nvarchar(250),
	@CP_Y_TE  float,
	@TRA_LUONG  float,
	@BOI_THUONG_TC  float,
	@Them BIT = 0
AS
BEGIN
-------------------------------------------------------------------------------------------------------------
IF (@Them = 1)
-------------------------------------------------------------------------------------------------------------
BEGIN

INSERT INTO	dbo.TAI_NAN_LD	(ID_CN ,ID_NGUYEN_NHAN ,ID_GAY_TAI_NAN ,ID_NGHE_NGHIEP ,NGAY_BI_TAI_NAN ,GIO_BI_TAI_NAN ,NOI_XAY_RA ,TINH_TRANG ,
GIAI_QUYET ,NGAY_CAP_CUU_TAI_CHO ,GIO_CAP_CUU_TAI_CHO ,PHUONG_PHAP_CAP_CUU ,SO_NGAY_NGHI ,KET_QUA_QDINH ,MUC_DO ,
GTRI_TB_THIET_HAI ,NGAY_VAO_VIEN ,NGAY_RA_VIEN ,DIEN_GIAI ,CP_Y_TE ,TRA_LUONG ,BOI_THUONG_TC)
VALUES(@ID_CN ,@ID_NGUYEN_NHAN ,@ID_GAY_TAI_NAN ,@ID_NGHE_NGHIEP ,@NGAY_BI_TAI_NAN ,@GIO_BI_TAI_NAN ,@NOI_XAY_RA ,
@TINH_TRANG ,@GIAI_QUYET ,@NGAY_CAP_CUU_TAI_CHO ,@GIO_CAP_CUU_TAI_CHO ,@PHUONG_PHAP_CAP_CUU ,@SO_NGAY_NGHI ,
@KET_QUA_QDINH ,@MUC_DO ,@GTRI_TB_THIET_HAI ,@NGAY_VAO_VIEN ,@NGAY_RA_VIEN ,@DIEN_GIAI ,@CP_Y_TE ,@TRA_LUONG ,@BOI_THUONG_TC)
SET @ID_TNLD = (SELECT SCOPE_IDENTITY())
END	
-------------------------------------------------------------------------------------------------------------
ELSE
-------------------------------------------------------------------------------------------------------------
BEGIN
UPDATE dbo.TAI_NAN_LD
SET	
ID_NGUYEN_NHAN =	@ID_NGUYEN_NHAN ,
ID_GAY_TAI_NAN =	@ID_GAY_TAI_NAN ,
ID_NGHE_NGHIEP =	@ID_NGHE_NGHIEP ,
NGAY_BI_TAI_NAN =	@NGAY_BI_TAI_NAN ,
GIO_BI_TAI_NAN =	@GIO_BI_TAI_NAN ,
NOI_XAY_RA =	@NOI_XAY_RA ,
TINH_TRANG =	@TINH_TRANG ,
GIAI_QUYET =	@GIAI_QUYET ,
NGAY_CAP_CUU_TAI_CHO =	@NGAY_CAP_CUU_TAI_CHO ,
GIO_CAP_CUU_TAI_CHO =	@GIO_CAP_CUU_TAI_CHO ,
PHUONG_PHAP_CAP_CUU =	@PHUONG_PHAP_CAP_CUU ,
SO_NGAY_NGHI =	@SO_NGAY_NGHI ,
KET_QUA_QDINH =	@KET_QUA_QDINH ,
MUC_DO =	@MUC_DO ,
GTRI_TB_THIET_HAI =	@GTRI_TB_THIET_HAI ,
NGAY_VAO_VIEN =	@NGAY_VAO_VIEN ,
NGAY_RA_VIEN =	@NGAY_RA_VIEN ,
DIEN_GIAI =	@DIEN_GIAI ,
CP_Y_TE =	@CP_Y_TE ,
TRA_LUONG = 	@TRA_LUONG ,
BOI_THUONG_TC =	@BOI_THUONG_TC
WHERE ID_TNLD  = @ID_TNLD
END	
-------------------------------------------------------------------------------------------------------------
SELECT @ID_TNLD
-------------------------------------------------------------------------------------------------------------
END	