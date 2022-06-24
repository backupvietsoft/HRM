if not exists(select * from sys.columns 
           where Name = N'DUONG_DAN_TL' and Object_ID = Object_ID(N'THONG_TIN_CHUNG'))
begin
ALTER TABLE dbo.THONG_TIN_CHUNG ADD DUONG_DAN_TL NVARCHAR(500) END  


if not exists(select * from sys.columns 
           where Name = N'USER_TL' and Object_ID = Object_ID(N'THONG_TIN_CHUNG'))
begin
ALTER TABLE dbo.THONG_TIN_CHUNG ADD USER_TL NVARCHAR(250) END  


if not exists(select * from sys.columns 
           where Name = N'PASS_TL' and Object_ID = Object_ID(N'THONG_TIN_CHUNG'))
begin
ALTER TABLE dbo.THONG_TIN_CHUNG ADD PASS_TL NVARCHAR(250) END  
