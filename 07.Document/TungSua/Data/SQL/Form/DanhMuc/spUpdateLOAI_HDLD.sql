ALTER PROCEDURE [dbo].[spUpdateLOAI_HDLD]
	@ID_LHDLD BIGINT,
    @TEN_LHDLD NVARCHAR(250),
    @TEN_LHDLD_A NVARCHAR(250),
    @TEN_LHDLD_H NVARCHAR(250),
    @SO_THANG INT
AS
    BEGIN
        IF ( @ID_LHDLD = -1 )
            BEGIN
                INSERT INTO dbo.[LOAI_HDLD](TEN_LHDLD,TEN_LHDLD_A,TEN_LHDLD_H,SO_THANG)
				VALUES(@TEN_LHDLD,@TEN_LHDLD_A,@TEN_LHDLD_H,@SO_THANG)
				
				SELECT SCOPE_IDENTITY()
            END	
        ELSE
            BEGIN
                UPDATE  dbo.[LOAI_HDLD]
                SET     TEN_LHDLD = @TEN_LHDLD ,
						TEN_LHDLD_A = @TEN_LHDLD_A ,
                        TEN_LHDLD_H = @TEN_LHDLD_H ,
						SO_THANG = @SO_THANG
                WHERE   ID_LHDLD = @ID_LHDLD

				SELECT @ID_LHDLD
            END	
    END	


