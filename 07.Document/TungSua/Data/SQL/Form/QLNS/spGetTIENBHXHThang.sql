ALTER PROCEDURE [dbo].[spGetTIENBHXHThang]
	@DVi BIGINT = -1,
	@XNghiep BIGINT = -1,
	@To BIGINT = -1,
	@UName NVARCHAR(50) = 'admin',
	@NNgu INT = 0 ,
	@Thang datetime = '2021-03-01',
	@Dot int = 1
AS	

BEGIN
	SELECT DISTINCT 
	
t1.THANG,
t1.DOT,
t1.TU_NGAY,
t1.DEN_NGAY,
t1.TONG_SO_LAO_DONG,
t1.LD_TANG,
t1.LD_GIAM,
t1.TONG_QL_DK,
t1.TONG_QL_TANG,
t1.TONG_QL_GIAM,
t1.TONG_QL_CK,
t1.TONG_NOP,
t1.TIEN_BHXH_T,
t1.TIEN_BHYT_T,
t1.TIEN_BHTN_T,
t1.TIEN_BHXH_G,
t1.TIEN_BHYT_G,
t1.TIEN_BHTN_G,
t1.DC_TANG_BHXH,
t1.DC_TANG_BHYT,
t1.DC_TANG_BHTN,
t1.DC_GIAM_BHXH,
t1.DC_GIAM_BHYT,
t1.DC_GIAM_BHTN,
t1.SO_PHAI_NOP_BHXH,
t1.SO_PHAI_NOP_BHYT,
t1.SO_PHAI_NOP_BHTN
	FROM dbo.TIEN_BHXH_THANG  t1 

	WHERE t1.THANG = @Thang AND t1.DOT = @Dot
END
