ALTER PROCEDURE [dbo].[spGetComBoTheoYC]
  @NNgu int
AS
BEGIN
	SELECT 0 as ID,N'Công ty' AS Name
	UNION SELECT 1,N'Cá nhân'
END

