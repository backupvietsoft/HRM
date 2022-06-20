
if not exists(select * from sys.columns 
           where Name = N'ID_CTL' and Object_ID = Object_ID(N'QUA_TRINH_CONG_TAC'))
begin
ALTER TABLE dbo.QUA_TRINH_CONG_TAC ADD ID_CTL INT END  


if not exists(select * from sys.columns 
           where Name = N'ID_CTL_CU' and Object_ID = Object_ID(N'QUA_TRINH_CONG_TAC'))
begin
ALTER TABLE dbo.QUA_TRINH_CONG_TAC ADD ID_CTL_CU INT END  




