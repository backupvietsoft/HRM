IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'spGetListDinhBienLD')
   exec('CREATE PROCEDURE spGetListDinhBienLD AS BEGIN SET NOCOUNT ON; END')
GO
ALTER PROCEDURE spGetListDinhBienLD
    @Nam INT = 2022,
	@ID_DV BIGINT = 2,
	@sBT NVARCHAR(250) ='BCCHITIEC',
	@UserName NVARCHAR(50) = 'admin',
	@NNgu INT  = 0

AS
BEGIN
	IF LEN(@sBT) = 0
	BEGIN
		-- get dữ liệu
	SELECT A.ID_LCV,A.T1,A.T2,A.T3,A.T4,A.T5,A.T6,A.T7,A.T8,A.T9,A.T10,A.T11,A.T12 FROM dbo.DINH_BIEN_LD A
	WHERE A.NAM =@Nam AND A.ID_DV = @ID_DV
	END
	
	IF LEFT(@sBT,5) = 'sBTDB'
    BEGIN
		--update dữ liệu
		 CREATE TABLE #BTDBLD (
		[ID_LCV] [bigint] NOT NULL,
		[T1] [int] NULL,
		[T2] [int] NULL,
		[T3] [int] NULL,
		[T4] [int] NULL,
		[T5] [int] NULL,
		[T6] [int] NULL,
		[T7] [int] NULL,
		[T8] [int] NULL,
		[T9] [int] NULL,
		[T10] [int] NULL,
		[T11] [int] NULL,
		[T12] [int] NULL
	)
	 	
	DECLARE @SQL NVARCHAR(MAX)
	SET @SQL = 'INSERT INTO #BTDBLD(ID_LCV,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12) SELECT ID_LCV,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12 FROM ' + @sBT 
	EXEC(@SQL)
	SET @SQL = 'DROP TABLE ' + @sBT  
	EXEC(@SQL)

	DELETE dbo.DINH_BIEN_LD WHERE NAM = @Nam AND ID_DV = @ID_DV
	INSERT INTO dbo.DINH_BIEN_LD(NAM,ID_DV,ID_LCV,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12)
	SELECT @Nam,@ID_DV,ID_LCV,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12 FROM #BTDBLD A
	END
	
	IF @sBT = 'BCTONG'
	BEGIN
		SELECT ROW_NUMBER() OVER (ORDER BY B.TEN_LCV) AS STT, 
		B.TEN_LCV,
               SUM(A.T1) AS T1,
               SUM(A.T2) AS T2,
               SUM(A.T3) AS T3,
               SUM(A.T4) AS T4,
               SUM(A.T5) AS T5,
               SUM(A.T6) AS T6,
               SUM(A.T7) AS T7,
               SUM(A.T8) AS T8,
               SUM(A.T9) AS T9,
               SUM(A.T10) AS T10,
               SUM(A.T11) AS T11,
               SUM(A.T12) AS T12 FROM dbo.DINH_BIEN_LD A
		INNER JOIN  dbo.LOAI_CONG_VIEC B ON B.ID_LCV = A.ID_LCV
		WHERE A.NAM =@Nam
		GROUP BY B.TEN_LCV
		ORDER BY B.TEN_LCV
	END

	IF @sBT = 'BCCHITIEC'
	
	BEGIN
			SELECT ROW_NUMBER() OVER (ORDER BY B.TEN_LCV) AS STT, 
		CASE @NNgu WHEN 0 THEN B.TEN_LCV WHEN 1 THEN B.TEN_LCV_A ELSE B.TEN_LCV_H END AS TEN_LCV,
               SUM(A.T1) AS T1,
               SUM(A.T2) AS T2,
               SUM(A.T3) AS T3,
               SUM(A.T4) AS T4,
               SUM(A.T5) AS T5,
               SUM(A.T6) AS T6,
               SUM(A.T7) AS T7,
               SUM(A.T8) AS T8,
               SUM(A.T9) AS T9,
               SUM(A.T10) AS T10,
               SUM(A.T11) AS T11,
               SUM(A.T12) AS T12 FROM dbo.DINH_BIEN_LD A
		INNER JOIN  dbo.LOAI_CONG_VIEC B ON B.ID_LCV = A.ID_LCV
		WHERE A.NAM =@Nam AND A.ID_DV = @ID_DV
		GROUP BY B.TEN_LCV,B.TEN_LCV_A,B.TEN_LCV_H
		ORDER BY B.TEN_LCV

		
	END

END

