ALTER PROCEDURE [dbo].[spGetUpdateQuyetDinhThoiViec]
	@ID_QDTV INT = 2,
	@ID_CN  BIGINT = 24,
	@SO_QD  nvarchar(20) ='QD02',
	@NGAY_NHAN_DON  DATE =  '1/1/2019',
	@NGAY_THOI_VIEC  DATE ='1/1/2019',
	@HS_LUONG  FLOAT =0,
	@LUONG_TOI_THIEU  FLOAT =0,
	@TIEN_TRO_CAP  FLOAT=0,
	@TIEN_PHEP  FLOAT =0,
	@TRO_CAP_KHAC  FLOAT =0,
	@TONG_CONG  FLOAT =0,
	@NGAY_KY  DATE = '1/1/2019',
	@ID_LD_TV  BIGINT =1,
	@NGAY_VAO_CTY  DATE ='1/1/2019',
	@NGAY_PHEP  FLOAT = 0,
	@NGUYEN_NHAN  nvarchar(250) ='sa',
	@GHI_CHU  nvarchar(250) = 'ghichu',
	@ID_NK  BIGINT = 1,
	@LUONG_TINH_PHEP  FLOAT =0
AS
BEGIN
IF @ID_QDTV = -1
BEGIN
INSERT INTO	dbo.QUYET_DINH_THOI_VIEC
--------------------------------------------------------------------------------------------------------------------------------------------
(ID_CN ,SO_QD ,NGAY_NHAN_DON ,NGAY_THOI_VIEC ,HS_LUONG ,LUONG_TOI_THIEU ,TIEN_TRO_CAP ,TIEN_PHEP ,TRO_CAP_KHAC ,TONG_CONG ,NGAY_KY ,ID_LD_TV ,
NGAY_VAO_CTY ,NGAY_PHEP ,NGUYEN_NHAN ,GHI_CHU ,ID_NK ,LUONG_TINH_PHEP)
VALUES  (@ID_CN ,@SO_QD ,@NGAY_NHAN_DON ,@NGAY_THOI_VIEC ,@HS_LUONG ,@LUONG_TOI_THIEU ,@TIEN_TRO_CAP ,@TIEN_PHEP ,@TRO_CAP_KHAC ,@TONG_CONG ,
@NGAY_KY ,@ID_LD_TV ,@NGAY_VAO_CTY ,@NGAY_PHEP ,@NGUYEN_NHAN ,@GHI_CHU ,@ID_NK ,@LUONG_TINH_PHEP)
--------------------------------------------------------------------------------------------------------------------------------------------
END
ELSE
BEGIN
--------------------------------------------------------------------------------------------------------------------------------------------
UPDATE dbo.QUYET_DINH_THOI_VIEC
SET
ID_CN = @ID_CN ,
SO_QD =	@SO_QD ,
NGAY_NHAN_DON =	@NGAY_NHAN_DON ,
NGAY_THOI_VIEC =	@NGAY_THOI_VIEC ,
HS_LUONG =	@HS_LUONG ,
LUONG_TOI_THIEU =	@LUONG_TOI_THIEU ,
TIEN_TRO_CAP =	@TIEN_TRO_CAP ,
TIEN_PHEP =	@TIEN_PHEP ,
TRO_CAP_KHAC =	@TRO_CAP_KHAC ,
TONG_CONG =	@TONG_CONG ,
NGAY_KY =	@NGAY_KY ,
ID_LD_TV =	@ID_LD_TV ,
NGAY_VAO_CTY =	@NGAY_VAO_CTY ,
NGAY_PHEP =	@NGAY_PHEP ,
NGUYEN_NHAN =	@NGUYEN_NHAN ,
GHI_CHU =	@GHI_CHU ,
ID_NK =	@ID_NK ,
LUONG_TINH_PHEP= @LUONG_TINH_PHEP
WHERE ID_QDTV =@ID_QDTV
--------------------------------------------------------------------------------------------------------------------------------------------
END
-----------------------------Cập nhật lại table công nhân ngày ngày và lý do----------------------------------------------------------------
UPDATE dbo.CONG_NHAN SET
 NGAY_NGHI_VIEC =@NGAY_THOI_VIEC
 ,ID_LD_TV =@ID_LD_TV,ID_TT_HT = 2
 WHERE ID_CN =@ID_CN
--------------------------------------------------------------------------------------------------------------------------------------------
SELECT @ID_CN

END


