ALTER PROCEDURE [dbo].[spUpdateQUAN_HE_GD]
	@ID_QH BIGINT,
    @TEN_QH NVARCHAR(250),
    @TEN_QH_A NVARCHAR(250),
    @TEN_QH_H NVARCHAR(250)
AS
    BEGIN
        IF ( @ID_QH = -1 )
            BEGIN
                INSERT INTO dbo.[QUAN_HE_GD](TEN_QH,TEN_QH_A,TEN_QH_H)
				VALUES(@TEN_QH,@TEN_QH_A,@TEN_QH_H)
				
				SELECT SCOPE_IDENTITY()
            END	
        ELSE
            BEGIN
                UPDATE  dbo.[QUAN_HE_GD]
                SET     TEN_QH = @TEN_QH ,
						TEN_QH_A = @TEN_QH_A ,
                        TEN_QH_H = @TEN_QH_H 
                WHERE   ID_QH = @ID_QH

				SELECT @ID_QH
            END	
    END	


