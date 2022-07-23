
ALTER PROCEDURE spGetPhongVan_UV
	@ID_UV BIGINT = 0,
	@UName NVARCHAR(50) = 'admin',
	@NNgu INT = 0 
AS
BEGIN
	SELECT A.ID_PV, MA_SO, NGAY_PV, BUOC_PV, NGUOI_PV_1, NGUOI_PV_2, TG_BD, TG_KT, ISNULL(PV_ON_OF_LINE,0) PV_ON_OF_LINE, B.DIEM_TONG_KET, ISNULL(B.DAT,0) DAT,
	CASE @NNgu WHEN 0 THEN TEN_TT_KHPV WHEN 1 THEN ISNULL(NULLIF(C.TEN_TT_KHPV_A,''),TEN_TT_KHPV) ELSE ISNULL(NULLIF(C.TEN_TT_KHPV_H,''),C.TEN_TT_KHPV) END AS TEN_TT_PV
	FROM PHONG_VAN A
	LEFT JOIN PHONG_VAN_UNG_VIEN B ON B.ID_PV = A.ID_PV
	LEFT JOIN dbo.TINH_TRANG_KHPV C ON A.TINH_TRANG = C.ID_TT_KHPV
	WHERE B.ID_UV =@ID_UV
	ORDER BY A.MA_SO,A.NGAY_PV
END
GO

