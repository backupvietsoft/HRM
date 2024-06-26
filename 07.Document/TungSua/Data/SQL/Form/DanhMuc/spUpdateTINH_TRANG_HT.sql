ALTER PROCEDURE [dbo].[spUpdateTINH_TRANG_HT]
	@ID_TT_HT BIGINT,
    @TEN_TT_HT NVARCHAR(250),
    @TEN_TT_HT_A NVARCHAR(250),
    @TEN_TT_HT_H NVARCHAR(250)
AS
    BEGIN
        IF ( @ID_TT_HT = -1 )
            BEGIN
                INSERT INTO dbo.[TINH_TRANG_HT](TEN_TT_HT,TEN_TT_HT_A,TEN_TT_HT_H)
				VALUES(@TEN_TT_HT,@TEN_TT_HT_A,@TEN_TT_HT_H)
				
				SELECT SCOPE_IDENTITY()
            END	
        ELSE
            BEGIN
                UPDATE  dbo.[TINH_TRANG_HT]
                SET     TEN_TT_HT = @TEN_TT_HT ,
						TEN_TT_HT_A = @TEN_TT_HT_A ,
                        TEN_TT_HT_H = @TEN_TT_HT_H 
                WHERE   ID_TT_HT = @ID_TT_HT

				SELECT @ID_TT_HT
            END	
    END	


