IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'spGetListPhieuYeuCau')
   exec('CREATE PROCEDURE spGetListPhieuYeuCau AS BEGIN SET NOCOUNT ON; END')
GO
ALTER PROCEDURE spGetListPhieuYeuCau
    @TNgay DATETIME ='2022-07-10 09:48:43.783',
	@DNgay DATETIME ='2022-07-19 09:48:43.783',
	@UserName NVARCHAR(500) ='admin',
	@NNgu INT = 0
AS
BEGIN
	--SELECT ID_CN,MS_CN INTO #CN FROM dbo.MGetListNhanSu(-1,-1,-1,-1,@UserName,@NNgu);
	SELECT A.ID_YCTD,
           A.MA_YCTD,
           A.ID_TO,
		 CASE @NNgu WHEN 0 THEN B.TEN_TO WHEN 1 THEN ISNULL(NULLIF(B.TEN_TO_A,''),B.TEN_TO) ELSE ISNULL(NULLIF(B.TEN_TO_H,''),B.TEN_TO) END AS TEN_TO , 
           A.ID_CN,
		   C.HO + ' ' + C.TEN AS HO_TEN, 
           A.NGAY_YEU_CAU,
           A.NGAY_NHAN_DON,
           A.GHI_CHU,ISNULL(A.ID_TT,1) ID_TT  FROM dbo.YEU_CAU_TUYEN_DUNG A INNER JOIN dbo.[TO] B ON B.ID_TO = A.ID_TO
		   INNER JOIN dbo.CONG_NHAN C ON C.ID_CN = A.ID_CN
		   WHERE A.NGAY_YEU_CAU BETWEEN @TNgay AND @DNgay
		   ORDER BY A.MA_YCTD
END


