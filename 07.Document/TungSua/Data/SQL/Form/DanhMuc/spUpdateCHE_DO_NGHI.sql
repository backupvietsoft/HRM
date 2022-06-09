ALTER PROCEDURE [dbo].[spUpdateCHE_DO_NGHI]
	@ID_CHE_DO BIGINT,
    @TEN_CHE_DO NVARCHAR(250),
    @TEN_CHE_DO_A NVARCHAR(250),
    @TEN_CHE_DO_H NVARCHAR(250)
AS
    BEGIN
        IF ( @ID_CHE_DO = -1 )
            BEGIN
                INSERT INTO dbo.[CHE_DO_NGHI](TEN_CHE_DO,TEN_CHE_DO_A,TEN_CHE_DO_H)
				VALUES(@TEN_CHE_DO,@TEN_CHE_DO_A,@TEN_CHE_DO_H)
				
				SELECT SCOPE_IDENTITY()
            END	
        ELSE
            BEGIN
                UPDATE  dbo.[CHE_DO_NGHI]
                SET     TEN_CHE_DO = @TEN_CHE_DO ,
						TEN_CHE_DO_A = @TEN_CHE_DO_A ,
                        TEN_CHE_DO_H = @TEN_CHE_DO_H 
                WHERE   ID_CHE_DO = @ID_CHE_DO

				SELECT @ID_CHE_DO
            END	
    END	


