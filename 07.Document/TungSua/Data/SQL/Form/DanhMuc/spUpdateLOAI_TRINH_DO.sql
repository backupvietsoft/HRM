ALTER PROCEDURE [dbo].[spUpdateLOAI_TRINH_DO]
	@ID_LOAI_TD BIGINT,
    @TEN_LOAI_TD NVARCHAR(250),
    @TEN_LOAI_TD_A NVARCHAR(250),
    @TEN_LOAI_TD_H NVARCHAR(250)
AS
    BEGIN
        IF ( @ID_LOAI_TD = -1 )
            BEGIN
                INSERT INTO dbo.[LOAI_TRINH_DO](TEN_LOAI_TD,TEN_LOAI_TD_A,TEN_LOAI_TD_H)
				VALUES(@TEN_LOAI_TD,@TEN_LOAI_TD_A,@TEN_LOAI_TD_H)
				
				SELECT SCOPE_IDENTITY()
            END	
        ELSE
            BEGIN
                UPDATE  dbo.[LOAI_TRINH_DO]
                SET     TEN_LOAI_TD = @TEN_LOAI_TD ,
						TEN_LOAI_TD_A = @TEN_LOAI_TD_A ,
                        TEN_LOAI_TD_H = @TEN_LOAI_TD_H 
                WHERE   ID_LOAI_TD = @ID_LOAI_TD

				SELECT @ID_LOAI_TD
            END	
    END	


