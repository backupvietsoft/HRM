ALTER PROCEDURE  [dbo].[spGetListLOAI_MAY]  
	@UName NVARCHAR(100) ='Admin',  
	@NNgu INT =0 
AS 
BEGIN  
	SELECT * FROM dbo.LOAI_MAY
END
