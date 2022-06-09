--select * from [MGetListNhanSuFormToDate]('Admin',0,-1,-1,-1,'20210101','20211231')
ALTER FUNCTION [dbo].[MGetListNhanSuFormToDate]
(
	@UName NVARCHAR(100) ='admin',
	@NNgu INT =0,
	@ID_DV BIGINT =-1,
	@ID_XN BIGINT =-1,
	@ID_TO BIGINT =-1,
	@TNgay Date ='2021-03-01',
	@DNgay Date ='2021-03-31'
)

RETURNS @NhanSu TABLE (
	ID_CN BIGINT NULL,
	[MS_CN] [NVARCHAR](20) NULL,
	[MS_THE_CC] [NVARCHAR](50) NULL,
	[HO] [NVARCHAR](50) NULL,
	[TEN] [NVARCHAR](20) NULL,
	[HO_TEN] [NVARCHAR](500) NULL,
	[NGAY_SINH] [DATETIME] NULL,
	[NAM_SINH] [INT] NULL,
	[PHAI] [BIT] NULL,
	[NGAY_VAO_CTY] [DATETIME] NULL,
	[NGAY_THU_VIEC] [DATETIME] NULL,
	[NGAY_HOC_VIEC] [DATETIME] NULL,
	[NGAY_VAO_LAM] [DATETIME] NULL,
	[NGAY_NGHI_VIEC] [DATETIME] NULL,
	[PHEP_CT] [Float] NULL,
	[ID_TT_HD] [Int] NULL,
	[ID_LD_TV] [Int] NULL,
	[ID_LCV] [Int] NULL,
	[MA_THE_ATM] [NVARCHAR](20) NULL,
	[ID_DV] [BIGINT] NULL,
	[MS_DV] [NVARCHAR](10) NULL,
	[TEN_DV] [NVARCHAR](250) NULL,
	[ID_XN] [BIGINT] NULL,
	[MS_XN] [NVARCHAR](10) NULL,
	[TEN_XN] [NVARCHAR](250) NULL,
	[STT_XN] [Int] NULL,
	[ID_CV] [Int] NULL,
	[ID_TO] [BIGINT] NULL,
	[MS_TO] [NVARCHAR](20) NULL,
	[TEN_TO] [NVARCHAR](250) NULL,
	[STT_TO] [Int] NULL
)
AS	
BEGIN
    
	DECLARE @UserTo TABLE(
		[ID_DV] [BIGINT] NULL,
		[MS_DV] [NVARCHAR](10) NULL,
		[TEN_DV] [NVARCHAR](250) NULL,
		[ID_XN] [BIGINT] NULL,
		[MS_XN] [NVARCHAR](10) NULL,
		[TEN_XN] [NVARCHAR](250) NULL,
		[STT_XN] [Int] NULL,
		[ID_TO] [BIGINT] NOT NULL,
		[MS_TO] [NVARCHAR](20) NULL,
		[TEN_TO] [NVARCHAR](250) NULL,
		[STT_TO] [Int] NULL
	) 

	INSERT INTO @UserTo(ID_DV, MS_DV, TEN_DV, ID_XN, MS_XN, TEN_XN, STT_XN, ID_TO, MS_TO, TEN_TO, STT_TO)
	SELECT  DISTINCT ID_DV,MSDV,TEN_DV,ID_XN,MS_XN, TEN_XN,STT_XN,ID_TO,MS_TO,TEN_TO,STT_TO FROM dbo.MGetToUser(@UName,@NNgu) 
	WHERE (ID_DV =@ID_DV OR @ID_DV =-1) AND (ID_XN =@ID_XN OR @ID_XN = -1 ) AND (ID_TO =@ID_TO OR @ID_TO =-1)

	INSERT INTO @NhanSu	(ID_CN, MS_CN, MS_THE_CC, HO, TEN, HO_TEN, NGAY_SINH, NAM_SINH, PHAI, NGAY_VAO_CTY, NGAY_THU_VIEC, NGAY_HOC_VIEC, 
	NGAY_VAO_LAM, NGAY_NGHI_VIEC, PHEP_CT, ID_TT_HD, ID_CV, ID_LCV, ID_DV, TEN_DV, ID_XN, MS_XN, TEN_XN, STT_XN, ID_TO, MS_TO, TEN_TO, STT_TO, ID_LD_TV, MA_THE_ATM)
	SELECT CN.ID_CN, CN.MS_CN, CN.MS_THE_CC, CN.HO, CN.TEN, CN.HT, CN.NGAY_SINH, CN.NAM_SINH, CN.PHAI, CN.NGAY_VAO_CTY, CN.NGAY_THU_VIEC, 
	CN.NGAY_HOC_VIEC, CN.NGAY_VAO_LAM, CN.NGAY_NGHI_VIEC, CN.PHEP_CT, CN.ID_TT_HD, CN.ID_CV, CN.ID_LCV, TOPQ.ID_DV, TOPQ.TEN_DV, 
	TOPQ.ID_XN, TOPQ.MS_XN, TOPQ.TEN_XN, TOPQ.STT_XN, CN.ID_TO, TOPQ.MS_TO, TOPQ.TEN_TO, TOPQ.STT_TO, CN.ID_LD_TV, CN.MA_THE_ATM 
	FROM (SELECT T1.ID_CN, T1.MS_CN, T1.MS_THE_CC, HO, T1.TEN, HO + ' ' + TEN HT, T1.NGAY_SINH, T1.NAM_SINH, T1.PHAI, 
	T1.NGAY_VAO_CTY, T1.NGAY_THU_VIEC, T1.NGAY_HOC_VIEC, T1.NGAY_VAO_LAM, T1.NGAY_NGHI_VIEC, T1.PHEP_CT, T1.ID_TT_HD, T1.ID_CV, T1.ID_LCV,
	ISNULL(T2.ID_TO, T1.ID_TO) ID_TO, T1.ID_LD_TV, T1.MA_THE_ATM
	FROM CONG_NHAN T1 LEFT JOIN (SELECT QTCT.ID_CN, QTCT.ID_TO FROM QUA_TRINH_CONG_TAC QTCT INNER JOIN (
		SELECT ID_CN, MAX(NGAY_HIEU_LUC) NGAYMAX FROM QUA_TRINH_CONG_TAC WHERE NGAY_HIEU_LUC <= @DNgay GROUP BY ID_CN
	) QTCNM ON QTCT.ID_CN = QTCNM.ID_CN AND QTCT.NGAY_HIEU_LUC = QTCNM.NGAYMAX) T2 ON T1.ID_CN = T2.ID_CN
	WHERE (T1.NGAY_VAO_CTY <= @DNgay) AND ((T1.NGAY_NGHI_VIEC IS NULL) OR (T1.NGAY_NGHI_VIEC > @TNgay))) CN 
	INNER JOIN @UserTo TOPQ on CN.ID_TO = TOPQ.ID_TO

RETURN
END

