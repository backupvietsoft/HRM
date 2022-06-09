ALTER PROCEDURE [dbo].[spGetListCongTac]
	@ID_CN BIGINT =24,
	@UName NVARCHAR(100) ='admin',
	@NNgu INT =0
AS
BEGIN
	SELECT 
		A.ID_QTCT,
		A.ID_CN ,
		SO_QUYET_DINH ,
		B.HO +' '+B.TEN AS TEN_CN,
		ID_LQD ,
		ID_NK ,
		A.ID_TO ,
		A.ID_CV ,
		A.ID_LCV ,
		ID_TO_CU ,
		ID_CV_CU ,
		ID_LCV_CU ,
		NGAY_KY ,
		NGAY_HIEU_LUC ,
		 CASE @NNgu WHEN 0 THEN E.TEN_DV WHEN 1 THEN ISNULL(NULLIF(E.TEN_DV_A,''),E.TEN_DV) ELSE ISNULL(NULLIF(E.TEN_DV_H,''),E.TEN_DV) END AS TEN_DV,
	 		 CASE @NNgu WHEN 0 THEN D.TEN_XN WHEN 1 THEN ISNULL(NULLIF(D.TEN_XN_A,''),D.TEN_XN) ELSE ISNULL(NULLIF(D.TEN_XN_H,''),D.TEN_XN) END AS TEN_XN,
		 		 (SELECT  CASE @NNgu WHEN 0 THEN TEN_TO WHEN 1 THEN ISNULL(NULLIF(TEN_TO_A,''),TEN_TO) ELSE ISNULL(NULLIF(TEN_TO_H,''),TEN_TO) END FROM dbo.[TO] WHERE ID_TO =A.ID_TO_CU) AS TEN_TO,
		NOI_CONG_TAC ,
		NHIEM_VU ,
		MUC_LUONG ,
		MUC_LUONG_CU ,
		C.ID_XN,D.ID_DV,
		A.GHI_CHU
	FROM QUA_TRINH_CONG_TAC A
	INNER JOIN dbo.CONG_NHAN B ON B.ID_CN = A.ID_CN
	INNER JOIN dbo.[TO] C ON C.ID_TO = A.ID_TO
	INNER JOIN dbo.XI_NGHIEP D ON D.ID_XN = C.ID_XN
	INNER JOIN dbo.DON_VI E ON E.ID_DV = D.ID_DV
	WHERE A.ID_CN =@ID_CN
	ORDER BY A.NGAY_HIEU_LUC DESC;
END


