ALTER PROCEDURE [dbo].[spUpdateCHUYEN]
	@ID_CHUYEN INT,
    @STT_CHUYEN NVARCHAR(7),
    @TEN_CHUYEN NVARCHAR(50),
    @ID_TO BIGINT
AS
    BEGIN
        IF ( @ID_CHUYEN = -1 )
            BEGIN
                INSERT INTO dbo.[CHUYEN](MS_CHUYEN, TEN_CHUYEN, ID_TO)
				VALUES(@STT_CHUYEN, @TEN_CHUYEN, @ID_TO)
				
				SELECT SCOPE_IDENTITY()
            END	
        ELSE
            BEGIN
                UPDATE  dbo.[CHUYEN]
                SET     MS_CHUYEN = @STT_CHUYEN,
						TEN_CHUYEN= @TEN_CHUYEN,
						ID_TO= @ID_TO
						
                WHERE   ID_CHUYEN = @ID_CHUYEN

				SELECT @ID_CHUYEN
            END	
    END	


