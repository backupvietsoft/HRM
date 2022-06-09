--select *from QUA_TRINH_CONG_TAC
CREATE PROCEDURE [dbo].[spkiemtrungBC]
	@ID_BC  BIGINT=null,
	@ID_CN  BIGINT=null,
	@SO_BC  nvarchar(100)=null

AS
BEGIN

		IF @SO_BC <> '' 
		BEGIN
			IF EXISTS (SELECT TOP 1 * FROM dbo.BANG_CAP WHERE  SO_HIEU_BANG = @SO_BC AND ID_BC <> @ID_BC and ID_CN=@ID_CN)
				SELECT 1 AS TT		
			ELSE
				SELECT 0 AS TT	
		END
	
END	


