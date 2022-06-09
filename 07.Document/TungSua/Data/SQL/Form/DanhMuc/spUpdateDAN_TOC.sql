ALTER PROCEDURE [dbo].[spUpdateDAN_TOC]
	@ID_DT BIGINT,
    @TEN_DT NVARCHAR(250),
    @TEN_DT_A NVARCHAR(250),
    @TEN_DT_H NVARCHAR(250)
AS
    BEGIN
        IF ( @ID_DT = -1 )
            BEGIN
                INSERT INTO dbo.[DAN_TOC](TEN_DT,TEN_DT_A,TEN_DT_H)
				VALUES(@TEN_DT,@TEN_DT_A,@TEN_DT_H)
				
				SELECT SCOPE_IDENTITY()
            END	
        ELSE
            BEGIN
                UPDATE  dbo.[DAN_TOC]
                SET     TEN_DT = @TEN_DT ,
						TEN_DT_A = @TEN_DT_A ,
                        TEN_DT_H = @TEN_DT_H 
                WHERE   ID_DT = @ID_DT

				SELECT @ID_DT
            END	
    END	


