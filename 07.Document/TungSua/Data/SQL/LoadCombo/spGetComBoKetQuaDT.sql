ALTER PROCEDURE [dbo].[spGetComBoKetQuaDT]
@NNgu int
AS
BEGIN
	SELECT 1 as ID_KQ,N'Phù hợp' AS NAME_KQ
	UNION SELECT 2,N'Không phù hợp'
END