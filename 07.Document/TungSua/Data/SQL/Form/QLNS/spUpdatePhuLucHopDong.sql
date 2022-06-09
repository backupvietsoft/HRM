ALTER PROCEDURE [dbo].[spUpdatePhuLucHopDong]
	@ID_HDLD  BIGINT = 5,
	@SO_PLHD_OLD  nvarchar(30) ='a',
	@SO_PLHD_NEW  nvarchar(30) ='abbb',
	@NOI_DUNG_THAY_DOI  nvarchar(500)='d',
	@THOI_GIAN_THUC_HIEN  nvarchar(500)='d',
	@NGAY_KY  DATETIME = '04/04/2020',
	@NGUOI_KY  BIGINT =1,
	@GHI_CHU  nvarchar(500)='f',
	@Them BIT = 0
AS
    BEGIN
        IF ( @Them = 1 )
---thêm
            BEGIN

INSERT INTO dbo.PHU_LUC_HDLD	
        ( ID_HDLD ,
          SO_PLHD ,
          NOI_DUNG_THAY_DOI ,
          THOI_GIAN_THUC_HIEN ,
          NGAY_KY ,
          NGUOI_KY ,
          GHI_CHU
        )
VALUES  ( @ID_HDLD ,
          @SO_PLHD_NEW ,
          @NOI_DUNG_THAY_DOI ,
          @THOI_GIAN_THUC_HIEN ,
          @NGAY_KY ,
          @NGUOI_KY ,
          @GHI_CHU
        )

            END	
        ELSE
            BEGIN

                UPDATE  dbo.PHU_LUC_HDLD
                SET    
		  SO_PLHD =@SO_PLHD_NEW,
          NOI_DUNG_THAY_DOI =@NOI_DUNG_THAY_DOI,
          THOI_GIAN_THUC_HIEN=@THOI_GIAN_THUC_HIEN ,
          NGAY_KY=@NGAY_KY ,
          NGUOI_KY =@NGUOI_KY ,
          GHI_CHU =@GHI_CHU
                WHERE   SO_PLHD = @SO_PLHD_OLD AND ID_HDLD =@ID_HDLD
            END
         SELECT  @SO_PLHD_NEW	 

    END	


	