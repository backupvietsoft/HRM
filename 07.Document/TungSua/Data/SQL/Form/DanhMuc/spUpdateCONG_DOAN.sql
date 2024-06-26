ALTER PROCEDURE [dbo].[spUpdateCONG_DOAN]
	@ID_CD INT,
    @ID_CUM INT,
    @MS_CD NVARCHAR(8),
    @TEN_CD NVARCHAR(1000),
    @TEN_CD_A NVARCHAR(1000),
    @TEN_CD_H NVARCHAR(1000),
    @ID_BT INT,
    @ID_LM INT,
    @TGTK SMALLINT,
    @CU_GA_LAP NVARCHAR(30),
    @YEU_CAU_KT NVARCHAR(200),
    @NGAY_LAP DATETIME
AS
    BEGIN
        IF ( @ID_CD = -1 )
            BEGIN
                INSERT INTO dbo.[CONG_DOAN](ID_CUM,MS_CD, TEN_CD, TEN_CD_A, TEN_CD_H,ID_BT,ID_LM,TGTK,CU_GA_LAP,YEU_CAU_KT,NGAY_LAP)
				VALUES(@ID_CUM,@MS_CD, @TEN_CD, @TEN_CD_A, @TEN_CD_H, @ID_BT,@ID_LM,@TGTK,@CU_GA_LAP,@YEU_CAU_KT,@NGAY_LAP)
				
				SELECT SCOPE_IDENTITY()
            END	
        ELSE
            BEGIN
                UPDATE  dbo.[CONG_DOAN]
                SET     ID_CUM = @ID_CUM,
						MS_CD= @MS_CD, 
						TEN_CD= @TEN_CD,
						TEN_CD_A= @TEN_CD_A,
						TEN_CD_H= @TEN_CD_H,
						ID_BT= @ID_BT,
						ID_LM= @ID_LM,
						TGTK= @TGTK,
						CU_GA_LAP= @CU_GA_LAP,
						YEU_CAU_KT= @YEU_CAU_KT,
						NGAY_LAP= @NGAY_LAP
                WHERE   ID_CD = @ID_CD

				SELECT @ID_CD
            END	
    END	


