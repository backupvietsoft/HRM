ALTER PROCEDURE [dbo].[spUpdateDangKyKeHoachDiKa]
    @ID_NHOM INT =1,
    @CA NVARCHAR(4) = 1,
    @TU_NGAY DATETIME =getdate,
    @DEN_NGAY DATETIME =getdate,
    @GHI_CHU NVARCHAR(500) ='',
    @BT NVARCHAR(100) = 'sBTAdmin'
AS
BEGIN TRY
	DECLARE  @resulst BIT;
    BEGIN TRANSACTION updatestaff;
    CREATE TABLE #TEMPT
    (
        [ID_CN] [INT] NOT NULL,
    ) ON [PRIMARY];
    DECLARE @sSql NVARCHAR(1000);
    SET @sSql = N'INSERT INTO #TEMPT SELECT ID_CN FROM ' + @BT;
    EXEC (@sSql);
    SET @sSql = N'DROP TABLE ' + @BT;
    EXEC (@sSql);
    INSERT INTO dbo.KE_HOACH_DI_CA
    (
        ID_CN,
        TU_NGAY,
        DEN_NGAY,
        ID_NHOM,
        CA,
        GHI_CHU
    )
    SELECT ID_CN,
           @TU_NGAY,
           @DEN_NGAY,
           @ID_NHOM,
           @CA,
           @GHI_CHU
    FROM #TEMPT;
    COMMIT TRANSACTION updatestaff;
	SET @resulst = 1
END TRY
BEGIN CATCH
    PRINT 'rollback ne';
    ROLLBACK TRAN updatestaff;
	SET @resulst = 0
END CATCH;
SELECT @resulst
