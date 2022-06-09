ALTER PROCEDURE [dbo].[spUpdateLY_DO_VANG]
	@ID_LDV BIGINT,
	@MS_LDV NVARCHAR(50),
    @TEN_LDV NVARCHAR(250),
    @TEN_LDV_A NVARCHAR(250),
    @TEN_LDV_H NVARCHAR(250),
	@ID_CHE_DO BIGINT,
	@PHEP BIT,
	@PHAN_TRAM_TRO_CAP FLOAT,
	@TINH_BHXH BIT,
	@KY_HIEU NVARCHAR(50),
	@TINH_LUONG BIT,
	@STT_LDV INT
AS
    BEGIN
        IF ( @ID_LDV = -1 )
            BEGIN
                INSERT INTO dbo.[LY_DO_VANG](MS_LDV,TEN_LDV,TEN_LDV_A,TEN_LDV_H,ID_CHE_DO,PHEP,PHAN_TRAM_TRO_CAP,TINH_BHXH,KY_HIEU,TINH_LUONG,STT_LDV)
				VALUES(@MS_LDV,@TEN_LDV,@TEN_LDV_A,@TEN_LDV_H,@ID_CHE_DO,@PHEP,@PHAN_TRAM_TRO_CAP,@TINH_BHXH,@KY_HIEU,@TINH_LUONG,@STT_LDV)
				
				SELECT SCOPE_IDENTITY()
            END	
        ELSE
            BEGIN
                UPDATE  dbo.[LY_DO_VANG]
                SET     MS_LDV = @MS_LDV,
						TEN_LDV = @TEN_LDV ,
						TEN_LDV_A = @TEN_LDV_A ,
                        TEN_LDV_H = @TEN_LDV_H ,
						ID_CHE_DO = @ID_CHE_DO ,
						PHEP = @PHEP ,
						PHAN_TRAM_TRO_CAP = @PHAN_TRAM_TRO_CAP ,
						TINH_BHXH = @TINH_BHXH ,
						KY_HIEU = @KY_HIEU ,
						TINH_LUONG = @TINH_LUONG ,
						STT_LDV = @STT_LDV
                WHERE   ID_LDV = @ID_LDV

				SELECT @ID_LDV
            END	
    END	


