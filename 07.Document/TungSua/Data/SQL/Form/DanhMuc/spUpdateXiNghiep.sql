ALTER PROCEDURE [dbo].[spUpdateXiNghiep]
    @ID_XN BIGINT ,
	@ID_DV BIGINT,
	@MS_XN NVARCHAR(10),
	@TEN_XN NVARCHAR(50),
	@TEN_XN_A NVARCHAR(50),
	@TEN_XN_H NVARCHAR(50),
	@STT_XN INT
AS
    BEGIN
        IF ( @ID_XN = -1 )
            BEGIN
					INSERT dbo.XI_NGHIEP (ID_DV, MS_XN, TEN_XN, TEN_XN_A, TEN_XN_H, STT_XN)
					VALUES (@ID_DV, @MS_XN, @TEN_XN, @TEN_XN_A, @TEN_XN_H, @STT_XN)
					SELECT SCOPE_IDENTITY()
            END	
        ELSE
            BEGIN
                UPDATE  dbo.XI_NGHIEP SET ID_DV = @ID_DV, MS_XN = @MS_XN, TEN_XN = @TEN_XN, TEN_XN_A = @TEN_XN_A, TEN_XN_H = @TEN_XN_H, STT_XN = @STT_XN WHERE  ID_XN = @ID_XN
				SELECT @ID_XN
            END	
    END	


