ALTER PROCEDURE [dbo].[spGetComboCaTheoKHDC]
	@ID_nhom BIGINT =1,
	@UName NVARCHAR(100) ='admin',
	@NNgu INT =0,
	@CoAll INT =1
AS
BEGIN

IF @CoAll = 1
BEGIN
	SELECT DISTINCT CA ID_CA, CA FROM CHE_DO_LAM_VIEC WHERE ID_NHOM=@ID_nhom or ID_NHOM=-1
	UNION SELECT '-1', '<All>'
	ORDER BY CA
END	
ELSE
	SELECT DISTINCT CA ID_CA, CA FROM CHE_DO_LAM_VIEC WHERE ID_NHOM=@ID_nhom or ID_NHOM=-1
	ORDER BY CA
END

