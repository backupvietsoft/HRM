--select *from QUA_TRINH_CONG_TAC
CREATE PROCEDURE [dbo].[spkiemtrungHDLD]
	@ID_HD  BIGINT=null,
	@ID_CN  BIGINT=null,
	@SO_HD  nvarchar(100)=null

AS
BEGIN

		IF @SO_HD <> '' 
		BEGIN
			IF EXISTS (SELECT TOP 1 * FROM dbo.HOP_DONG_LAO_DONG WHERE  SO_HDLD = @SO_HD AND ID_HDLD <> @ID_HD and ID_CN=@ID_CN)
				SELECT 1 AS TT		
			ELSE
				SELECT 0 AS TT	
		END
	
END	


