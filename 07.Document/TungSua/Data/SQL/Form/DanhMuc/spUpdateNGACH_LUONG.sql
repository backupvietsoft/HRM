ALTER PROCEDURE [dbo].[spUpdateNGACH_LUONG]
	@ID_NL BIGINT,
    @MS_NL NVARCHAR(20),
    @TEN_NL NVARCHAR(250),
    @TEN_NL_A NVARCHAR(250),
    @TEN_NL_H NVARCHAR(250)
AS
    BEGIN
        IF ( @ID_NL = -1 )
            BEGIN
                INSERT INTO dbo.[NGACH_LUONG](MS_NL,TEN_NL,TEN_NL_A,TEN_NL_H)
				VALUES(@MS_NL, @TEN_NL,@TEN_NL_A,@TEN_NL_H)
				
				SELECT SCOPE_IDENTITY()
            END	
        ELSE
            BEGIN
                UPDATE  dbo.[NGACH_LUONG]
                SET     MS_NL = @MS_NL ,
						TEN_NL = @TEN_NL ,
						TEN_NL_A = @TEN_NL_A ,
                        TEN_NL_H = @TEN_NL_H 
                WHERE   ID_NL = @ID_NL

				SELECT @ID_NL
            END	
    END	


