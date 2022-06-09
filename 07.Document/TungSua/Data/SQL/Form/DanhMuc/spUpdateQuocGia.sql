ALTER PROCEDURE [dbo].[spUpdateQuocGia]
	@ID_QG BIGINT,
	@MA_QG NVARCHAR(20),
    @TEN_QG NVARCHAR(200),
	@TEN_QG_A NVARCHAR(200),
	@TEN_QG_H NVARCHAR(200)	
AS


    BEGIN
        IF (@ID_QG = -1 )
            BEGIN
				INSERT  INTO dbo.QUOC_GIA (MA_QG, TEN_QG, TEN_QG_A, TEN_QG_H)
				VALUES (@MA_QG, @TEN_QG,@TEN_QG_A,@TEN_QG_H)
                
				SELECT SCOPE_IDENTITY()
            END	
        ELSE
            BEGIN
                UPDATE  dbo.QUOC_GIA
                SET     MA_QG = @MA_QG, TEN_QG = @TEN_QG, TEN_QG_A = @TEN_QG_A, TEN_QG_H = @TEN_QG_H 
				WHERE   ID_QG = @ID_QG

				SELECT @ID_QG
            END	
    END	