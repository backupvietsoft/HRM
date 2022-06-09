ALTER PROCEDURE [dbo].[spUpdateNoiDungDanhGia]
	@ID_NDDG BIGINT,
	@TEN_NDDG nvarchar(250),
	@TEN_NDDG_A nvarchar(250),
	@TEN_NDDG_H nvarchar(250),
	@Them BIT = 0
AS
    BEGIN
        IF ( @Them = 1 )
---thêm
            BEGIN
INSERT	INTO	dbo.NOI_DUNG_DANH_GIA( TEN_NDDG, TEN_NDDG_A, TEN_NDDG_H )
VALUES  (@TEN_NDDG,@TEN_NDDG_A,@TEN_NDDG_H)
SELECT SCOPE_IDENTITY()
            END
        ELSE
            BEGIN
                UPDATE  dbo.NOI_DUNG_DANH_GIA
                SET    
		  TEN_NDDG =@TEN_NDDG,
		  TEN_NDDG_A =@TEN_NDDG_A,
		  TEN_NDDG_H =@TEN_NDDG_H
                WHERE   ID_NDDG =@ID_NDDG
         SELECT  @ID_NDDG	 

            END
    END	
