ALTER PROCEDURE [dbo].[spUpdateTo]
	@ID_TO BIGINT,
    @ID_XN	BIGINT,
    @MS_TO NVARCHAR(20),
    @TEN_TO NVARCHAR(250),
    @TEN_TO_A NVARCHAR(250),
    @TEN_TO_H NVARCHAR(250),
    @STT_TO INT,
	@UName NVARCHAR(100)
AS
    BEGIN
        IF ( @ID_TO = -1 )
            BEGIN
                INSERT INTO dbo.[TO](ID_XN,MS_TO,TEN_TO,TEN_TO_A,TEN_TO_H,STT_TO)
				VALUES(@ID_XN,@MS_TO, @TEN_TO,@TEN_TO_A,@TEN_TO_H,@STT_TO)
				
				SET @ID_TO = SCOPE_IDENTITY()

				INSERT INTO	dbo.NHOM_TO
				    (
				        ID_NHOM,
				        ID_TO
				    )
				VALUES
				    ((SELECT TOP 1 ID_NHOM FROM dbo.USERS WHERE [USER_NAME] = @UName),@ID_TO)
				    
            END	
        ELSE
            BEGIN
                UPDATE  dbo.[TO]
                SET     ID_XN = @ID_XN ,
                        MS_TO = @MS_TO ,
						TEN_TO = @TEN_TO ,
						TEN_TO_A = @TEN_TO_A ,
                        TEN_TO_H = @TEN_TO_H ,
                        STT_TO = @STT_TO
                WHERE   ID_TO = @ID_TO

				
            END	

		SELECT @ID_TO
    END	


