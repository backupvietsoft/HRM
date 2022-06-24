
ALTER PROCEDURE [dbo].[spUpdateTienLuong]
    @ID_LCB BIGINT ,
    @ID_CN BIGINT ,
    @ID_TO BIGINT ,
    @ID_CV BIGINT ,
    @ID_NK BIGINT ,
    @NGAY_KY DATETIME ,
    @SO_QUYET_DINH NVARCHAR(20) ,
    @NGAY_HIEU_LUC DATETIME ,
    @ID_NL BIGINT ,
    @ID_BL BIGINT ,
    @GHI_CHU NVARCHAR(100) ,
    @HS_LUONG FLOAT ,
    @LUONG_CO_BAN FLOAT ,
    @MUC_LUONG_THUC FLOAT ,
    @THUONG_CHUYEN_CAN FLOAT ,
    @PC_DOC_HAI FLOAT ,
    @THUONG_HT_CV FLOAT ,
    @PC_KY_NANG FLOAT ,
    @PC_SINH_HOAT FLOAT,
	@PC_CON_NHO FLOAT,
	@ID_TT INT,
	@TAI_LIEU NVARCHAR(500),
    @Them BIT
AS
    BEGIN
        IF ( @Them = 1 )
---thêm
            BEGIN
                INSERT  INTO dbo.LUONG_CO_BAN
                        ( ID_CN ,
                          ID_TO ,
                          ID_CV ,
                          ID_NK ,
                          NGAY_KY ,
                          SO_QUYET_DINH ,
                          NGAY_HIEU_LUC ,
                          ID_NL ,
                          ID_BL ,
                          GHI_CHU ,
                          HS_LUONG ,
                          LUONG_CO_BAN ,
                          MUC_LUONG_THUC ,
                          THUONG_CHUYEN_CAN ,
                          PC_DOC_HAI ,
                          THUONG_HT_CV ,
                          PC_KY_NANG ,
                          PC_SINH_HOAT,
						  PC_CON_NHO,
						  ID_TT,
						  TAI_LIEU
                        )
                VALUES  ( @ID_CN ,
                          @ID_TO ,
                          @ID_CV ,
                          @ID_NK ,
                          @NGAY_KY ,
                          @SO_QUYET_DINH ,
                          @NGAY_HIEU_LUC ,
                          @ID_NL ,
                          @ID_BL ,
                          @GHI_CHU ,
                          @HS_LUONG ,
                          @LUONG_CO_BAN ,
                          @MUC_LUONG_THUC ,
                          @THUONG_CHUYEN_CAN ,
                          @PC_DOC_HAI ,
                          @THUONG_HT_CV ,
                          @PC_KY_NANG ,
                          @PC_SINH_HOAT,
						  @PC_CON_NHO,
						  @ID_TT,
						  @TAI_LIEU

                        )
                SELECT  SCOPE_IDENTITY()	 
            END	
        ELSE
            BEGIN
                UPDATE  dbo.LUONG_CO_BAN
                SET     ID_TO = @ID_TO ,
                        ID_CV = @ID_CV ,
                        ID_NK = @ID_NK ,
                        NGAY_KY = @NGAY_KY ,
                        SO_QUYET_DINH = @SO_QUYET_DINH ,
                        NGAY_HIEU_LUC = @NGAY_HIEU_LUC ,
                        ID_NL = @ID_NL ,
                        ID_BL = @ID_BL ,
                        GHI_CHU = @GHI_CHU ,
                        HS_LUONG = @HS_LUONG ,
                        LUONG_CO_BAN = @LUONG_CO_BAN ,
                        MUC_LUONG_THUC = @MUC_LUONG_THUC ,
                        THUONG_CHUYEN_CAN = @THUONG_CHUYEN_CAN ,
                        PC_DOC_HAI = @PC_DOC_HAI ,
                        THUONG_HT_CV = @THUONG_HT_CV ,
                        PC_KY_NANG = @PC_KY_NANG ,
                        PC_SINH_HOAT = @PC_SINH_HOAT,
						PC_CON_NHO =@PC_CON_NHO,
						ID_TT = @ID_TT,
						TAI_LIEU =@TAI_LIEU
                WHERE   ID_LCB = @ID_LCB

				SELECT @ID_LCB
            END	

    END
GO

