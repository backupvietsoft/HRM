ALTER PROCEDURE [dbo].[spUpdateChuc_Vu]
	@ID_CV BIGINT,
    @MS_CV NVARCHAR(20),
    @TEN_CV NVARCHAR(250),
    @TEN_CV_A NVARCHAR(250),
    @TEN_CV_H NVARCHAR(250),
	@ID_LOAI_CV BIGINT,
    @STT_IN_CV INT
AS
    BEGIN
        IF ( @ID_CV = -1 )
            BEGIN
                INSERT INTO dbo.[CHUC_VU](MS_CV,TEN_CV,TEN_CV_A,TEN_CV_H,ID_LOAI_CV,STT_IN_CV)
				VALUES(@MS_CV, @TEN_CV,@TEN_CV_A,@TEN_CV_H,@ID_LOAI_CV,@STT_IN_CV)
				
				SELECT SCOPE_IDENTITY()
            END	
        ELSE
            BEGIN
                UPDATE  dbo.[CHUC_VU]
                SET     MS_CV = @MS_CV ,
						TEN_CV = @TEN_CV ,
						TEN_CV_A = @TEN_CV_A ,
                        TEN_CV_H = @TEN_CV_H ,
						ID_LOAI_CV = @ID_LOAI_CV,
                        STT_IN_CV = @STT_IN_CV
                WHERE   ID_CV = @ID_CV

				SELECT @ID_CV
            END	
    END	


