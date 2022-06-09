ALTER PROCEDURE [dbo].[spUpdateQuan]
	@ID_QUAN BIGINT,
	@MS_QUAN NVARCHAR(50),
	@TEN_QUAN NVARCHAR(250),
	@TEN_QUAN_A NVARCHAR(250),
	@TEN_QUAN_H NVARCHAR(250),
	@ID_TP BIGINT
AS

    

    BEGIN
        IF ( @ID_QUAN = -1 )
            BEGIN
                INSERT INTO dbo.QUAN (MS_QUAN, TEN_QUAN, TEN_QUAN_A, TEN_QUAN_H, ID_TP)
				VALUES(@MS_QUAN, @TEN_QUAN, @TEN_QUAN_A, @TEN_QUAN_H, @ID_TP)
				SELECT SCOPE_IDENTITY()
            END	
        ELSE
            BEGIN
                UPDATE  dbo.QUAN 
				SET MS_QUAN = @MS_QUAN,
					TEN_QUAN = @TEN_QUAN,
					TEN_QUAN_A = @TEN_QUAN_A,
					TEN_QUAN_H = @TEN_QUAN_H,
					ID_TP = @ID_TP
                WHERE   ID_QUAN = @ID_QUAN

				SELECT @ID_QUAN
            END	
    END	


