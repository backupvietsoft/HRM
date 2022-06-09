ALTER PROCEDURE [dbo].[spInSertKeHoachNghiPhep]
    @ID_LDV BIGINT ,
    @ID_CN BIGINT ,
    @TU_NGAY DATETIME ,
    @DEN_NGAY DATETIME ,
    @NGAY_VAO_LAM_LAI DATETIME ,
    @SO_GIO FLOAT ,
    @GHI_CHU NVARCHAR(250)
AS
    BEGIN
        INSERT  INTO dbo.KE_HOACH_NGHI_PHEP
                ( ID_LDV ,
                  ID_CN ,
                  TU_NGAY ,
                  DEN_NGAY ,
                  NGAY_VAO_LAM_LAI ,
                  SO_GIO ,
                  GHI_CHU
                )
        VALUES  ( @ID_LDV , -- ID_LDV - bigint
                  @ID_CN , -- ID_CN - bigint
                  @TU_NGAY , -- TU_NGAY - datetime
                  @DEN_NGAY , -- DEN_NGAY - datetime
                  @NGAY_VAO_LAM_LAI , -- NGAY_VAO_LAM_LAI - datetime
                  @SO_GIO, -- SO_GIO - float
                  @GHI_CHU  -- GHI_CHU - nvarchar(250)
                )
    END
