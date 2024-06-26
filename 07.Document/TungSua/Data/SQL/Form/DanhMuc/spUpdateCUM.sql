ALTER PROCEDURE [dbo].[spUpdateCUM]
	@ID_CUM INT,
    @MS_CUM NVARCHAR(10),
    @TEN_CUM NVARCHAR(100),
    @TEN_CUM_A NVARCHAR(100),
    @TEN_CUM_H NVARCHAR(100),
    @STT SMALLINT,
    @ID_LSP INT,
    @TINH_TG BIT,
    @LOAI_CUM NVARCHAR(10),
    @CUM_PS BIT,
    @CUM_CUOI BIT
AS
    BEGIN
        IF ( @ID_CUM = -1 )
            BEGIN
                INSERT INTO dbo.[CUM](MS_CUM,TEN_CUM, TEN_CUM_A, TEN_CUM_H, STT, ID_NHH, TINH_TG,  LOAI_CUM, CUM_PS, CUM_CUOI)
				VALUES(@MS_CUM,@TEN_CUM, @TEN_CUM_A, @TEN_CUM_H, @STT, @ID_LSP, @TINH_TG, @LOAI_CUM, @CUM_PS, @CUM_CUOI)
				
				SELECT SCOPE_IDENTITY()
            END	
        ELSE  
            BEGIN
                UPDATE  dbo.[CUM]
                SET     MS_CUM = @MS_CUM,
						TEN_CUM= @TEN_CUM,
						TEN_CUM_A= @TEN_CUM_A,
						TEN_CUM_H= @TEN_CUM_H,
						STT= @STT, 
						ID_NHH= @ID_LSP, 
						TINH_TG= @TINH_TG,  
						LOAI_CUM= @LOAI_CUM, 
						CUM_PS= @CUM_PS, 
						CUM_CUOI= @CUM_CUOI
                WHERE   ID_CUM = @ID_CUM

				SELECT @ID_CUM
            END	
    END	


