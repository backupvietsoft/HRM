ALTER PROCEDURE [dbo].[spGetListBangDanhGia]
	@ID_CN BIGINT =24
AS
BEGIN
	SELECT ID_DG,NGAY_DANH_GIA,NGUOI_DANH_GIA,NOI_DUNG FROM dbo.BANG_DANH_GIA
	WHERE ID_CN =@ID_CN
END


