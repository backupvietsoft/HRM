IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'spGetComboViTriTheoYeuCau')
   exec('CREATE PROCEDURE spGetComboViTriTheoYeuCau AS BEGIN SET NOCOUNT ON; END')
GO
ALTER PROCEDURE spGetComboViTriTheoYeuCau
	@ID_YCTD BIGINT = 0,
	@UName NVARCHAR(100) ='Admin',  
	@NNgu INT =0 
AS	
BEGIN
--
SELECT DISTINCT A.ID_VTTD,CASE @NNgu WHEN 0 THEN TEN_LCV WHEN 1 THEN ISNULL(NULLIF(TEN_LCV_A,''),TEN_LCV) ELSE ISNULL(NULLIF(TEN_LCV_H,''),TEN_LCV) END AS TEN_VTTD FROM dbo.YCTD_VI_TRI_TUYEN A
INNER JOIN dbo.LOAI_CONG_VIEC B ON B.ID_LCV = A.ID_VTTD WHERE A.ID_YCTD =@ID_YCTD

END
GO



