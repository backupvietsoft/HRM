ALTER PROCEDURE [dbo].[spGetComBoNoiDaoTao]
  @NNgu int
AS
BEGIN
	SELECT 1 as ID,N'Công ty' AS Name
	UNION SELECT 2,N'Bên ngoài'
END