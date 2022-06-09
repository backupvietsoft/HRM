ALTER PROCEDURE [dbo].[spUpdateLOAI_KHEN_THUONG]
	@ID_LOAI_KT BIGINT,
    @TEN_LOAI_KT NVARCHAR(250),
    @TEN_LOAI_KT_A NVARCHAR(250),
    @TEN_LOAI_KT_H NVARCHAR(250)
AS
    BEGIN
        IF ( @ID_LOAI_KT = -1 )
            BEGIN
                INSERT INTO dbo.[LOAI_KHEN_THUONG](TEN_LOAI_KT,TEN_LOAI_KT_A,TEN_LOAI_KT_H)
				VALUES(@TEN_LOAI_KT,@TEN_LOAI_KT_A,@TEN_LOAI_KT_H)
				
				SELECT SCOPE_IDENTITY()
            END	
        ELSE
            BEGIN
                UPDATE  dbo.[LOAI_KHEN_THUONG]
                SET     TEN_LOAI_KT = @TEN_LOAI_KT ,
						TEN_LOAI_KT_A = @TEN_LOAI_KT_A ,
                        TEN_LOAI_KT_H = @TEN_LOAI_KT_H 
                WHERE   ID_LOAI_KT = @ID_LOAI_KT

				SELECT @ID_LOAI_KT
            END	
    END	
