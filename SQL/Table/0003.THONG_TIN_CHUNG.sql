if not exists(select * from sys.columns 
           where Name = N'DUONG_DAN_TL' and Object_ID = Object_ID(N'THONG_TIN_CHUNG'))
begin
ALTER TABLE dbo.THONG_TIN_CHUNG ADD DUONG_DAN_TL NVARCHAR(500) END  
