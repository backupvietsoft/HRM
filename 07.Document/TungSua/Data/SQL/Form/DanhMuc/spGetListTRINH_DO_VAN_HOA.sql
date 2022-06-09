ALTER PROCEDURE [dbo].[spGetListTRINH_DO_VAN_HOA]  
@UName NVARCHAR(100) ='Admin',  
@NNgu INT =0 
AS 
BEGIN  
SELECT T1.[ID_TDVH],
CASE @NNgu WHEN 0 THEN T2.TEN_LOAI_TD WHEN 1 THEN ISNULL(NULLIF(T2.TEN_LOAI_TD_A,''),T2.TEN_LOAI_TD) ELSE ISNULL(NULLIF(T2.TEN_LOAI_TD_H,''),T2.TEN_LOAI_TD) END AS TEN_LOAI_TD,
CASE @NNgu WHEN 0 THEN T1.TEN_TDVH WHEN 1 THEN ISNULL(NULLIF(T1.TEN_TDVH_A,''),TEN_TDVH) ELSE ISNULL(NULLIF(T1.TEN_TDVH_H,''),T1.TEN_TDVH) END AS TEN_TDVH,T1.[ID_LOAI_TD] 
FROM TRINH_DO_VAN_HOA T1 INNER JOIN dbo.LOAI_TRINH_DO T2 ON T1.[ID_LOAI_TD] = T2.[ID_LOAI_TD]
ORDER BY 
CASE @NNgu WHEN 0 THEN T2.TEN_LOAI_TD WHEN 1 THEN ISNULL(NULLIF(T2.TEN_LOAI_TD_A,''),T2.TEN_LOAI_TD) ELSE ISNULL(NULLIF(T2.TEN_LOAI_TD_H,''),T2.TEN_LOAI_TD) END ,
CASE @NNgu WHEN 0 THEN T1.TEN_TDVH WHEN 1 THEN ISNULL(NULLIF(T1.TEN_TDVH_A,''),TEN_TDVH) ELSE ISNULL(NULLIF(T1.TEN_TDVH_H,''),T1.TEN_TDVH) END 
END
