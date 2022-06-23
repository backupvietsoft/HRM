
--add columns hợp đồng lao động HOP_DONG_LAO_DONG
IF not exists(select * from sys.columns 
           where Name = N'TAI_LIEU' and Object_ID = Object_ID(N'HOP_DONG_LAO_DONG'))
begin
ALTER TABLE dbo.HOP_DONG_LAO_DONG ADD TAI_LIEU NVARCHAR(500) END  

if not exists(select * from sys.columns 
           where Name = N'ID_TT' and Object_ID = Object_ID(N'HOP_DONG_LAO_DONG'))
begin
ALTER TABLE dbo.HOP_DONG_LAO_DONG ADD ID_TT INT END  



--add columns hợp đồng lao động QUA_TRINH_CONG_TAC
IF not exists(select * from sys.columns 
           where Name = N'TAI_LIEU' and Object_ID = Object_ID(N'QUA_TRINH_CONG_TAC'))
begin
ALTER TABLE dbo.QUA_TRINH_CONG_TAC ADD TAI_LIEU NVARCHAR(500) END  

if not exists(select * from sys.columns 
           where Name = N'ID_TT' and Object_ID = Object_ID(N'QUA_TRINH_CONG_TAC'))
begin
ALTER TABLE dbo.QUA_TRINH_CONG_TAC ADD ID_TT INT END  


--add columns hợp đồng lao động LUONG_CO_BAN
IF not exists(select * from sys.columns 
           where Name = N'TAI_LIEU' and Object_ID = Object_ID(N'LUONG_CO_BAN'))
begin
ALTER TABLE dbo.LUONG_CO_BAN ADD TAI_LIEU NVARCHAR(500) END  

if not exists(select * from sys.columns 
           where Name = N'ID_TT' and Object_ID = Object_ID(N'LUONG_CO_BAN'))
begin
ALTER TABLE dbo.LUONG_CO_BAN ADD ID_TT INT END  

--add columns hợp đồng lao động KHEN_THUONG
IF not exists(select * from sys.columns 
           where Name = N'TAI_LIEU' and Object_ID = Object_ID(N'KHEN_THUONG'))
begin
ALTER TABLE dbo.KHEN_THUONG ADD TAI_LIEU NVARCHAR(500) END  

if not exists(select * from sys.columns 
           where Name = N'ID_TT' and Object_ID = Object_ID(N'KHEN_THUONG'))
begin
ALTER TABLE dbo.KHEN_THUONG ADD ID_TT INT END  

