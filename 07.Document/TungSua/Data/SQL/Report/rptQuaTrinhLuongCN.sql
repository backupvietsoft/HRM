ALTER PROCEDURE [dbo].[rptQuaTrinhLuongCN]
	@UName NVARCHAR(100) ='admin',
	@NNgu INT =0,
	@ID_CN BIGINT = 313
AS 
BEGIN
	
	SELECT CN.MS_CN, CN.HO + ' ' + TEN HO_TEN, SO_QUYET_DINH, LUONG.NGAY_HIEU_LUC, LUONG.NGAY_KY, NL.TEN_NL, BL.TEN_BL,
	LUONG.HS_LUONG, LUONG.PC_DOC_HAI, LUONG.PC_SINH_HOAT, LUONG.THUONG_CHUYEN_CAN, LUONG.THUONG_HT_CV, LUONG.PC_KY_NANG, NK.HO_TEN NGUOI_KY
	FROM LUONG_CO_BAN LUONG INNER JOIN CONG_NHAN CN ON LUONG.ID_CN = CN.ID_CN
	INNER JOIN NGACH_LUONG NL ON LUONG.ID_NL = NL.ID_NL
	INNER JOIN BAC_LUONG BL ON LUONG.ID_BL = BL.ID_BL
	LEFT JOIN NGUOI_KY_GIAY_TO NK ON LUONG.ID_NK = NK.ID_NK
	WHERE LUONG.ID_CN = @ID_CN
END


