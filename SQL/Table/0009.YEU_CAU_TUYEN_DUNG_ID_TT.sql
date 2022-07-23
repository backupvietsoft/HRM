-- add chức vụ cho loại công việc
if not exists(select * from sys.columns 
           where Name = N'ID_TT' and Object_ID = Object_ID(N'YEU_CAU_TUYEN_DUNG'))
BEGIN
 ALTER TABLE dbo.YEU_CAU_TUYEN_DUNG ADD ID_TT INT 
END  
