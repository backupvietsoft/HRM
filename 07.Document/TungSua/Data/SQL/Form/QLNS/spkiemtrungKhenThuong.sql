--select *from KHEN_THUONG
ALTER PROCEDURE [dbo].[spkiemtrungKhenThuong]
	@ID_HD  BIGINT=null,
	@ID_CN  BIGINT=null,
	@SO_HD  nvarchar(100)=null

AS
BEGIN

		IF @SO_HD <> '' 
		BEGIN
			IF EXISTS (SELECT TOP 1 * FROM dbo.KHEN_THUONG WHERE  SO_QUYET_DINH= @SO_HD AND	ID_KTHUONG <> @ID_HD and ID_CN=@ID_CN)
				SELECT 1 AS TT		
			ELSE
				SELECT 0 AS TT	
		END
	
END	


