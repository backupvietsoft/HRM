
ALTER PROCEDURE [dbo].[spGetCongNhanPhepThang]
	@FLAG BIT =1,
	@ID_DV BIGINT =-1,
	@ID_XN BIGINT =-1,
	@ID_TO BIGINT =-1,
	@THANG DATETIME = '2021-03-01 00:00:00.000',
	@UName NVARCHAR(100) ='admin',
	@NNgu INT =0
AS
BEGIN
SELECT  ID_TO,ID_DV,ID_XN,TEN_XN,TEN_TO INTO #TEMPT  FROM dbo.MGetToUser(@UName,@NNgu) WHERE (ID_DV =@ID_DV OR @ID_DV =-1) AND (ID_XN =@ID_XN OR @ID_XN = -1 )

--@FLAG BIT =1, Khi sửa khi lưới chưa có dữ liệu thì lấy dữ liệu right join tự động tính những cột còn lại 
--@FLAG BIT =0, Load dữ truc tiep luoi

IF @FLAG = 1
BEGIN
SELECT A.MS_CN,
       A.HO + ' ' + A.TEN AS HO_TEN,
       A.NGAY_VAO_LAM,
       2.0 AS PHEP_THAM_NIEN,
       0.0 AS PHEP_UNG_TRUOC,
       T_1,
       T_2,
       T_3,
       T_4,
       T_5,
       T_6,
       T_7,
       T_8,
       T_9,
       T_10,
       T_11,
       T_12,
       TT_1,
       TT_2,
       TT_3,
       TT_4,
       TT_5,
       TT_6,
       TT_7,
       TT_8,
       TT_9,
       TT_10,
       TT_11,
       TT_12
	   ,20.5 AS PHEP_DA_NGHI,
	   21.3 AS PHEP_TIEU_CHUAN, 
	   DATEPART(MONTH,@THANG) AS SO_THANG_LV,
	  15.0 AS PHEP_CON_LAI
FROM dbo.CONG_NHAN A
    LEFT JOIN dbo.PHEP_THANG B
        ON B.ID_CN = A.ID_CN
INNER JOIN #TEMPT C ON C.ID_TO = A.ID_TO
WHERE A.ID_TO = @ID_TO OR @ID_TO = -1
END

ELSE
BEGIN
SELECT A.MS_CN,
       A.HO + ' ' + A.TEN AS HO_TEN,
       A.NGAY_VAO_LAM,
       B.PHEP_TN AS PHEP_THAM_NIEN,
       B.PHEP_UNG AS PHEP_UNG_TRUOC,
       T_1,
       T_2,
       T_3,
       T_4,
       T_5,
       T_6,
       T_7,
       T_8,
       T_9,
       T_10,
       T_11,
       T_12,
       TT_1,
       TT_2,
       TT_3,
       TT_4,
       TT_5,
       TT_6,
       TT_7,
       TT_8,
       TT_9,
       TT_10,
       TT_11,
       TT_12,B.PHEP_DA_NGHI,
	   B.PHEP_TIEU_CHUAN, 
	   B.SO_THANG_LV AS SO_THANG_LV,
	  15 AS PHEP_CON_LAI
FROM dbo.CONG_NHAN A
    INNER JOIN dbo.PHEP_THANG B
        ON B.ID_CN = A.ID_CN
INNER JOIN #TEMPT C ON C.ID_TO = A.ID_TO
WHERE A.ID_TO = @ID_TO OR @ID_TO = -1
END

END
