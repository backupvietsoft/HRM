IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'spGetCongNhanTheoTT')
   exec('CREATE PROCEDURE spGetCongNhanTheoTT AS BEGIN SET NOCOUNT ON; END')
GO
ALTER PROCEDURE [dbo].[spGetCongNhanTheoTT]
    @Username NVARCHAR(100) = 'admin',
    @TT INT,
    @NNgu INT = 0,
    @CoAll BIT = 0
AS
BEGIN
    SELECT DISTINCT
           ID_TO
    INTO #TEMP
    FROM dbo.MGetToUser(@Username, @NNgu);
    IF @TT = 1
    BEGIN
        --LẤY CÔNG NHÂN CÒN LÀM
        SELECT T1.ID_CN,
               T1.MS_CN,
               T1.HO + ' ' + T1.TEN AS TEN_CN
        FROM dbo.CONG_NHAN T1
            INNER JOIN #TEMP T2
                ON T1.ID_TO = T2.ID_TO
        WHERE T1.NGAY_NGHI_VIEC IS NULL
              OR T1.NGAY_NGHI_VIEC < GETDATE();
    END;
    ELSE
    BEGIN
        --LẤY CÔNG NHÂN ĐÃ NGHĨ
        SELECT T1.ID_CN,
               T1.MS_CN,
               T1.HO + ' ' + T1.TEN AS TEN_CN
        FROM dbo.CONG_NHAN T1
            INNER JOIN #TEMP T2
                ON T1.ID_TO = T2.ID_TO
        WHERE T1.NGAY_NGHI_VIEC IS NOT NULL
    END;
END;
GO

