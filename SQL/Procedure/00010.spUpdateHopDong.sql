
ALTER PROCEDURE [dbo].[spUpdateHopDong]
    @ID_HDLD BIGINT ,
    @ID_CN BIGINT ,
    @SO_HDLD NVARCHAR(30) ,
    @ID_LHDLD BIGINT ,
    @NGAY_BAT_DAU_HD DATETIME ,
    @NGAY_HET_HD DATETIME ,
    @NGAY_KY DATETIME ,
    @HD_GIA_HAN BIGINT ,
    @NGAY_BD_THU_VIEC DATETIME ,
    @NGAY_KT_THU_VIEC DATETIME ,
    @LUONG_THU_VIEC FLOAT ,
    @BAC_LUONG BIGINT ,
    @MUC_LUONG_CHINH FLOAT ,
    @CHI_SO_PHU_CAP FLOAT ,
    @MUC_LUONG_THUC_LINH FLOAT ,
    @DIA_DIEM_LAM_VIEC NVARCHAR(200) ,
    @DIA_CHI_NOI_LAM_VIEC NVARCHAR(200) ,
    @CONG_VIEC NVARCHAR(200) ,
    @ID_CV BIGINT ,
    @SO_NGAY_PHEP NVARCHAR(200) ,
    @NGUOI_KY_GIA_HAN INT ,
	@ID_TT INT,
	@TAI_LIEU NVARCHAR(500),
    @Them BIT
AS
    BEGIN
        IF ( @Them = 1 )
---thêm
            BEGIN
			DECLARE @ID_HDLD_TEMP BIGINT
                INSERT  INTO dbo.HOP_DONG_LAO_DONG
                        ( ID_CN ,
                          SO_HDLD ,
                          ID_LHDLD ,
                          NGAY_BAT_DAU_HD ,
                          NGAY_HET_HD ,
                          NGAY_KY ,
                          HD_GIA_HAN ,
                          NGAY_BD_THU_VIEC ,
                          NGAY_KT_THU_VIEC ,
                          LUONG_THU_VIEC ,
                          BAC_LUONG ,
                          MUC_LUONG_CHINH ,
                          CHI_SO_PHU_CAP ,
                          MUC_LUONG_THUC_LINH ,
                          DIA_DIEM_LAM_VIEC ,
                          DIA_CHI_NOI_LAM_VIEC ,
                          CONG_VIEC ,
                          ID_CV ,
                          SO_NGAY_PHEP ,
                          NGUOI_KY_GIA_HAN,ID_TT,TAI_LIEU
                        )
                VALUES  ( @ID_CN ,
                          @SO_HDLD ,
                          @ID_LHDLD ,
                          @NGAY_BAT_DAU_HD ,
                          @NGAY_HET_HD ,
                          @NGAY_KY ,
                          @HD_GIA_HAN ,
                          @NGAY_BD_THU_VIEC ,
                          @NGAY_KT_THU_VIEC ,
                          @LUONG_THU_VIEC ,
                          @BAC_LUONG ,
                          @MUC_LUONG_CHINH ,
                          @CHI_SO_PHU_CAP ,
                          @MUC_LUONG_THUC_LINH ,
                          @DIA_DIEM_LAM_VIEC ,
                          @DIA_CHI_NOI_LAM_VIEC ,
                          @CONG_VIEC ,
                          @ID_CV ,
                          @SO_NGAY_PHEP ,
                          @NGUOI_KY_GIA_HAN ,@ID_TT,@TAI_LIEU
                        )
				
                SET @ID_HDLD_TEMP =  SCOPE_IDENTITY()	
				
				UPDATE dbo.CONG_NHAN
				SET ID_TT_HT = (SELECT LHDLD.ID_TT_HD FROM dbo.HOP_DONG_LAO_DONG HDLD INNER JOIN dbo.LOAI_HDLD LHDLD ON HDLD.ID_LHDLD = LHDLD.ID_LHDLD WHERE HDLD.ID_HDLD = @ID_HDLD_TEMP)
				WHERE CONG_NHAN.ID_CN = @ID_CN
				
				SELECT @ID_HDLD_TEMP
				 
            END	
        ELSE
            BEGIN

                UPDATE  dbo.HOP_DONG_LAO_DONG
                SET     ID_CN = @ID_CN ,
                        SO_HDLD = @SO_HDLD ,
                        ID_LHDLD = @ID_LHDLD ,
                        NGAY_BAT_DAU_HD = @NGAY_BAT_DAU_HD ,
                        NGAY_HET_HD = @NGAY_HET_HD ,
                        NGAY_KY = @NGAY_KY ,
                        HD_GIA_HAN = @HD_GIA_HAN ,
                        NGAY_BD_THU_VIEC = @NGAY_BD_THU_VIEC ,
                        NGAY_KT_THU_VIEC = @NGAY_KT_THU_VIEC ,
                        LUONG_THU_VIEC = @LUONG_THU_VIEC ,
                        BAC_LUONG = @BAC_LUONG ,
                        MUC_LUONG_CHINH = @MUC_LUONG_CHINH ,
                        CHI_SO_PHU_CAP = @CHI_SO_PHU_CAP ,
                        MUC_LUONG_THUC_LINH = @MUC_LUONG_THUC_LINH ,
                        DIA_DIEM_LAM_VIEC = @DIA_DIEM_LAM_VIEC ,
                        DIA_CHI_NOI_LAM_VIEC = @DIA_CHI_NOI_LAM_VIEC ,
                        CONG_VIEC = @CONG_VIEC ,
                        ID_CV = @ID_CV ,
                        SO_NGAY_PHEP = @SO_NGAY_PHEP ,
                        NGUOI_KY_GIA_HAN = @NGUOI_KY_GIA_HAN ,
						ID_TT =@ID_TT,
						TAI_LIEU = @TAI_LIEU
                WHERE   ID_HDLD = @ID_HDLD
                SELECT  @ID_HDLD
            END
            
            -- cập nhật lại tổ vào id loại công việc
			IF(@NGAY_BAT_DAU_HD = (SELECT MAX(NGAY_BAT_DAU_HD) FROM dbo.HOP_DONG_LAO_DONG WHERE ID_CN = @ID_CN ))
			BEGIN
				UPDATE dbo.CONG_NHAN SET ID_LHDLD = @ID_LHDLD,
										ID_TT_HD = (SELECT ID_TT_HD FROM dbo.LOAI_HDLD WHERE ID_LHDLD = @ID_LHDLD)	
				WHERE ID_CN = @ID_CN
			END
    END	
	

GO

