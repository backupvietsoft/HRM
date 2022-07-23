
ALTER PROCEDURE [dbo].[spGetListBangCapUV]
	@ID_UV BIGINT,
	@UName NVARCHAR(100) ='Admin',  
	@NNgu INT =0 
AS 
BEGIN
	SELECT ID_BC, ID_UV, TEN_BANG, TEN_TRUONG, TU_NAM, DEN_NAM, T1.ID_XL
	FROM UNG_VIEN_BANG_CAP T1 WHERE T1.ID_UV =@ID_UV
END
GO