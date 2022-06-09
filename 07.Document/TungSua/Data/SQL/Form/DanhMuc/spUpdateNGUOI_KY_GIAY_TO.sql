ALTER PROCEDURE [dbo].[spUpdateNGUOI_KY_GIAY_TO]
	@ID_NK BIGINT,
    @HO_TEN NVARCHAR(250),
    @CHUC_VU NVARCHAR(250),
    @CHUC_VU_A NVARCHAR(250),
	@QUOC_TICH NVARCHAR(250),
	@NGAY_SINH DATETIME,
	@SO_CMND NVARCHAR(250),
	@NGAY_CAP DATETIME,
	@NOI_CAP NVARCHAR(250),
	@DIA_CHI NVARCHAR(250)
AS
    BEGIN
        IF ( @ID_NK = -1 )
            BEGIN
                INSERT INTO dbo.[NGUOI_KY_GIAY_TO](HO_TEN,CHUC_VU,CHUC_VU_A,QUOC_TICH,NGAY_SINH,SO_CMND,CAP_NGAY,NOI_CAP,DIA_CHI)
				VALUES(@HO_TEN,@CHUC_VU,@CHUC_VU_A,@QUOC_TICH,@NGAY_SINH,@SO_CMND,@NGAY_CAP,@NOI_CAP,@DIA_CHI)
				
				SELECT SCOPE_IDENTITY()
            END	
        ELSE
            BEGIN
                UPDATE  dbo.[NGUOI_KY_GIAY_TO]
                SET     HO_TEN = @HO_TEN ,
						CHUC_VU = @CHUC_VU ,
                        CHUC_VU_A = @CHUC_VU_A,
						QUOC_TICH = @QUOC_TICH,
						NGAY_SINH = @NGAY_SINH,
						SO_CMND = @SO_CMND,
						CAP_NGAY = @NGAY_CAP,
						NOI_CAP = @NOI_CAP,
						DIA_CHI = @DIA_CHI 
                WHERE   ID_NK = @ID_NK

				SELECT @ID_NK
            END	
    END	


