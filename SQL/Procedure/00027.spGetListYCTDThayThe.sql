IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'spGetListYCTDThayThe')
   exec('CREATE PROCEDURE spGetListYCTDThayThe AS BEGIN SET NOCOUNT ON; END')
GO
ALTER PROCEDURE spGetListYCTDThayThe  
	@ID_YCTD BIGINT = 1,
	@UName NVARCHAR(100) ='Admin',  
	@NNgu INT =0 
AS 
BEGIN  
	SELECT ID_YCTD,ID_VTTD,ID_CN,ID_LCV,NGAY_LV_CUOI,LY_DO_NGHI FROM dbo.YCTD_THAY_THE_CN WHERE ID_YCTD = @ID_YCTD
END
GO
