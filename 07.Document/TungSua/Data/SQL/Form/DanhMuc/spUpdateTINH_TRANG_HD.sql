ALTER PROCEDURE [dbo].[spUpdateTINH_TRANG_HD]
	@ID_TT_HD BIGINT,
    @TEN_TT_HD NVARCHAR(250),
    @TEN_TT_HD_A NVARCHAR(250),
    @TEN_TT_HD_H NVARCHAR(250)
AS
    BEGIN
        IF ( @ID_TT_HD = -1 )
            BEGIN
                INSERT INTO dbo.[TINH_TRANG_HD](TEN_TT_HD,TEN_TT_HD_A,TEN_TT_HD_H)
				VALUES(@TEN_TT_HD,@TEN_TT_HD_A,@TEN_TT_HD_H)
				
				SELECT SCOPE_IDENTITY()
            END	
        ELSE
            BEGIN
                UPDATE  dbo.[TINH_TRANG_HD]
                SET     TEN_TT_HD = @TEN_TT_HD ,
						TEN_TT_HD_A = @TEN_TT_HD_A ,
                        TEN_TT_HD_H = @TEN_TT_HD_H 
                WHERE   ID_TT_HD = @ID_TT_HD

				SELECT @ID_TT_HD
            END	
    END	


