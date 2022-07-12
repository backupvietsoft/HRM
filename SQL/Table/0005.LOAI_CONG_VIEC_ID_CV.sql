-- add chức vụ cho loại công việc
if not exists(select * from sys.columns 
           where Name = N'ID_CV' and Object_ID = Object_ID(N'LOAI_CONG_VIEC'))
BEGIN
 ALTER TABLE dbo.LOAI_CONG_VIEC ADD ID_CV BIGINT 
END  

