//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Vs.Payroll
//{
//    internal class Class1
//    {
//        private void cmdTinhLuong_Click()
//        {
//            ;/* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo err_loi' at character 57
//   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.DefaultVisit(SyntaxNode node)
//   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitOnErrorGoToStatement(OnErrorGoToStatementSyntax node)
//   at Microsoft.CodeAnalysis.VisualBasic.Syntax.OnErrorGoToStatementSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
//   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
//   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
//   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

//Input: 
//On Error GoTo err_loi

// */
//            ADODB.Recordset RSTL;
//            string BangTam;
//            string BangTamCN;
//            string TuNgay;
//            string DenNgay;
//            string BANGTAM1;
//            string bangtam2;
//            string DON_VI;
//            string MS_PB;
//            string THACHTAM;
//            THACHTAM = "THACHTAM" + IDIP;
//            BangTamCN = "BANGTAMCNMOI" + IDIP;
//            DROP_TABLE(BangTamCN);
//            BangTam = "bangtam" + IDIP;

//            Sql = " SELECT * FROM BANG_LUONG WHERE THANG = '" + Format(ThangTL, "MM/dd/yyyy") + "' ";
//            ;/* Cannot convert EmptyStatementSyntax, CONVERSION ERROR: Conversion for EmptyStatement not implemented, please report this issue in '' at character 575
//   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.DefaultVisit(SyntaxNode node)
//   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitEmptyStatement(EmptyStatementSyntax node)
//   at Microsoft.CodeAnalysis.VisualBasic.Syntax.EmptyStatementSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
//   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
//   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
//   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

//Input: 
//    Set rs = New Recordset

// */
//            rs.Open(Sql, CN, 1, 1);
//            if (!rs.EOF)
//            {
//                if (MsgBoxXP("BÂn muên tÈnh mði (Y) / Hay câp nhât lÂi (N). (Y/N) ?", Constants.vbQuestion + Constants.vbYesNoCancel, this.Caption, null/* Conversion error: Set to default value for this argument */, MyScheme, null/* Conversion error: Set to default value for this argument */, null/* Conversion error: Set to default value for this argument */, true, null/* Conversion error: Set to default value for this argument */, null/* Conversion error: Set to default value for this argument */, mdiMain.OsenXPHookMenu1.Font, null/* Conversion error: Set to default value for this argument */, null/* Conversion error: Set to default value for this argument */, mdiMain.OsenXPHookMenu1.Font) == Constants.vbYes)
//                    goto 1;
//            }
//            else
//            {
//                1:
//        ;
//                Sql = " SELECT * INTO " + BangTamCN + " FROM CONG_NHAN ";
//                CN.Execute(Sql);
//                prb.Visible = true;
//                prb.Max = 100;
//                prb.Min = 1;
//                prb.Value = 1;

//                // xoa du lieu bang luong thang can tinh
//                Sql = " DELETE FROM BANG_LUONG WHERE THANG = '" + Format(ThangTL, "MM/dd/yyyy") + "' ";
//                if (cmbDV.GetKeyValue != "ALL")
//                    Sql = Sql + " AND  MSDV='" + cmbDV.GetKeyValue + "'";
//                if (cmbPB.GetKeyValue != "ALL")
//                    Sql = Sql + " AND MS_PB='" + cmbPB.GetKeyValue + "'";
//                CN.Execute(Sql);
//                prb.Value = 2;
//                DoEvents();

//                // lay ds cong nhan tinh tuong thang tu cham cong thang
//                Sql = " INSERT INTO [BANG_LUONG](THANG, MS_CN, NGAY_VAO_CONG_TY, MA_THE_ATM, SO_TAI_KHOAN, MSDV, MS_PB, TEN_PB, " + " STT_PB, HO, TEN, HO_TEN, TRUC_TIEP, MS_CHUC_VU, PHAN_TRAM, CACH_TINH, NGAY_CONG, CHE_DO_LDN, GIO_LAM_THEM_CDLDN, CHUYEN_CAN, " + " KHONG_PHEP, OM, VIEC_RIENG_KLUONG, VIEC_RIENG_CLUONG, PHEP_NAM, LE_TET, PHEP_THU_BAY, TC_1621, TC_DEM, LAM_DEM, TC_CN, TC_NL, " + " TONG_GIO_CONG, BL_CHINH) " + "SELECT '" + Format(ThangTL, "MM/DD/YYYY") + "' AS THANG, T1.MS_CN, T2.NGAY_VAO_LAM, " + "T2.MA_THE_ATM, T2.SO_TAI_KHOAN,  T1.MSDV, T1.MS_TO, T3.TEN_TO, ISNULL(T3.STT_TO,999), T2.HO, T2.TEN, T2.HO + ' ' + T2.TEN, " + "T2.TRUC_TIEP_SX,  T2.CHUC_VU, T1.PHAN_TRAM, ISNULL(T1.CACH_TINH,''), SUM(ISNULL(T1.NGAY_CONG,0)), " + "SUM(ISNULL(T1.CHE_DO_LDN,0)), SUM(ISNULL(T1.GIO_LAM_THEM_LDN,0)), T1.DIEM , SUM(ISNULL(T1.COT_1,0)) AS KP, " + "SUM(ISNULL(T1.COT_2,0)) AS O, SUM(ISNULL(T1.COT_3,0)) AS KL, SUM(ISNULL(T1.COT_4,0)+ISNULL(T1.COT_7,0)) AS CL, " + "SUM(ISNULL(T1.COT_5,0)) AS PN, SUM(ISNULL(T1.COT_6,0)) AS LT, SUM(ISNULL(T1.PHEP_THU_BAY,0)) AS PL, SUM(ISNULL(T1.TC_1621,0)) AS TCNT, " + "SUM(ISNULL(T1.TC_DEM,0)) AS TCD, SUM(ISNULL(T1.LAM_DEM,0)) AS LDEM, SUM(ISNULL(T1.TC_CN,0)) AS TCCN, SUM(ISNULL(T1.TC_NL,0)) AS TCNL, " + "SUM((IsNull(T1.NGAY_CONG, 0) * 8) - IsNull(T1.CHE_DO_LDN, 0)) As TGC, 1 " + "FROM CHAM_CONG_THANG T1 INNER JOIN " + BangTamCN + " T2 " + "ON T1.MS_CN = T2.MS_CN  INNER JOIN [TO] T3 ON T1.MSDV = T3.MSDV AND T1.MS_TO = T3.MS_TO " + "WHERE T1.THANG = '" + Format(ThangTL, "MM/DD/YYYY") + "' ";
//                if (cmbDV.GetKeyValue != "ALL")
//                    Sql = Sql + " AND T1.MSDV='" + cmbDV.GetKeyValue + "'";
//                if (cmbPB.GetKeyValue != "ALL")
//                    Sql = Sql + " AND T1.MS_TO='" + cmbPB.GetKeyValue + "'";
//                Sql = Sql + " GROUP BY T1.MS_CN, T2.NGAY_VAO_LAM, T2.MA_THE_ATM, T2.SO_TAI_KHOAN,  T1.MSDV, T1.MS_TO, T3.TEN_TO, " + "ISNULL(T3.STT_TO,999), T2.HO, T2.TEN, T2.HO + ' ' + T2.TEN, T2.TRUC_TIEP_SX,  T2.CHUC_VU, T1.PHAN_TRAM, ISNULL(T1.CACH_TINH,''), T1.DIEM";
//                CN.Execute(Sql);
//                prb.Value = 4;
//                DoEvents();

//                // Cap nhat dong tinh chinh cho nhung cong nhan co 2 cach tinh luong
//                Sql = "UPDATE BANG_LUONG SET BL_CHINH = 0 FROM BANG_LUONG TB1 INNER JOIN ( " + "SELECT T1.MS_CN, MIN(ISNULL(T1.NGAY_CONG,0)) AS NC FROM BANG_LUONG T1 INNER JOIN ( " + "SELECT MS_CN, COUNT(CACH_TINH) CountCT FROM BANG_LUONG WHERE THANG = '" + Format(ThangTL, "MM/DD/YYYY") + "' ";
//                if (cmbDV.GetKeyValue != "ALL")
//                    Sql = Sql + " AND MSDV='" + cmbDV.GetKeyValue + "'";
//                if (cmbPB.GetKeyValue != "ALL")
//                    Sql = Sql + " AND MS_TO='" + cmbPB.GetKeyValue + "'";
//                Sql = Sql + " GROUP BY MS_CN " + "HAVING COUNT(CACH_TINH) > 1) T2 ON T1.MS_CN = T2.MS_CN WHERE THANG = '" + Format(ThangTL, "MM/DD/YYYY") + "' " + "GROUP BY T1.MS_CN) TB2 ON TB1.MS_CN = TB2.MS_CN AND TB1.NGAY_CONG = TB2.NC";
//                CN.Execute(Sql);
//                // lay danh sach cong nhan co an luong san pham (co nhap lieu trong phieu cong doan)
//                string danhsachcnanluongsp;
//                danhsachcnanluongsp = "danhsachcnanluongsp" + IDIP;
//                DROP_TABLE(danhsachcnanluongsp);
//                Sql = " select distinct ms_cn into " + danhsachcnanluongsp + " from phieu_cong_doan where ngay between '" + Format(TuNgayTL, "mm/dd/yyyy") + "' and '" + Format(DenNgayTL, "mm/dd/yyyy") + "'";
//                CN.Execute(Sql);
//                Sql = "  INSERT INTO [BANG_LUONG]([THANG], [MS_CN], NGAY_VAO_CONG_TY, MA_THE_ATM, [SO_TAI_KHOAN], [MSDV], [MS_PB], [TEN_PB], " + " [STT_PB], [HO], [TEN] , HO_TEN , TRUC_TIEP, MS_CHUC_VU ,PHAN_TRAM, cach_tinh, BL_CHINH ) " + " SELECT DISTINCT '" + Format(ThangTL, "MM/DD/YYYY") + "' AS THANG, T1.MS_CN, T1.NGAY_VAO_LAM, T1.MA_THE_ATM, " + " T1.SO_TAI_KHOAN,  T1.CHI_NHANH, T1.MS_TO, T2.TEN_TO, ISNULL(T2.STT_TO ,999) ,  T1.HO, T1.TEN, T1.HO + ' ' + T1.TEN AS HO_TEN, " + " T1.TRUC_TIEP_SX, T1.CHUC_VU, 100, '', 1 " + " FROM " + BangTamCN + " T1 INNER JOIN [TO] T2 ON T1.CHI_NHANH = T2.MSDV AND T1.MS_TO = T2.MS_TO INNER JOIN " + danhsachcnanluongsp + " T3 ON T1.MS_CN = T3.MS_CN  WHERE T3.MS_CN NOT IN ( select ms_cn from bang_luong where thang='" + Format(ThangTL, "mm/dd/yyyy") + "' )";
//                CN.Execute(Sql);

//                // 
//                // Sql = "  INSERT INTO [BANG_LUONG]([THANG], [MS_CN], [SO_TAI_KHOAN], [MSDV], [MS_PB], [TEN_PB], " & _
//                // " [STT_PB], [HO], [TEN] , HO_TEN , TRUC_TIEP, MS_CHUC_VU ,PHAN_TRAM, cach_tinh ) " & _
//                // " SELECT DISTINCT '" & Format(ThangTL, "MM/DD/YYYY") & "' AS THANG, " & BangTamCN & ".MS_CN, " & BangTamCN & ".SO_TAI_KHOAN,  CHI_NHANH, " & BangTamCN & ".MS_TO, TEN_TO," & _
//                // " ISNULL([TO].STT_TO ,999) ,  " & BangTamCN & ".HO, " & BangTamCN & ".TEN, " & BangTamCN & ".HO + ' ' + " & BangTamCN & ".TEN AS HO_TEN, " & _
//                // " " & BangTamCN & ".TRUC_TIEP_SX, " & BangTamCN & ".CHUC_VU, 100, ''  " & _
//                // " FROM " & BangTamCN & " INNER JOIN [TO] ON " & BangTamCN & ".CHI_NHANH = [TO].MSDV AND " & BangTamCN & ".MS_TO = [TO].MS_TO INNER JOIN" & _
//                // " DON_VI ON [TO].MSDV = DON_VI.MSDV  where ms_cn not in ( select ms_cn from bang_luong where thang='" & Format(ThangTL, "mm/dd/yyyy") & "' )" & _
//                // " and ms_cn in ( select ms_cn from " & danhsachcnanluongsp & ")"
//                // CN.Execute Sql

//                prb.Value = 6;
//                DoEvents();

//                // cap nhat cach tinh la tinh luong theo san pham neu cach tinh bang rong trong ds tinh luong thang
//                Sql = "UPDATE BANG_LUONG SET CACH_TINH = 'SP' WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "' AND ISNULL(CACH_TINH,'') =''";
//                CN.Execute(Sql);
//                Sql = "UPDATE BANG_LUONG SET CACH_TINH = 'SP' WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "' AND ISNULL(CACH_TINH,'') =''";
//                CN.Execute(Sql);

//                // tinh so ngay lam viec cua cong nhan vien bang luong thang
//                BANGTAM1 = "BANGTAM1" + IDIP;
//                DROP_TABLE(BANGTAM1);
//                Sql = " SELECT MS_CN, NGAY_VAO_LAM, NGAY_NGHI_VIEC, CONVERT(FLOAT,0) AS SO_NGAY  INTO " + BANGTAM1 + " FROM CONG_NHAN ";
//                CN.Execute(Sql);
//                Sql = " UPDATE " + BANGTAM1 + " SET NGAY_NGHI_VIEC ='" + Format(DenNgayTL, "MM/DD/YYYY") + "'";
//                CN.Execute(Sql);
//                Sql = "UPDATE " + BANGTAM1 + " SET SO_NGAY = DATEDIFF(DAY,NGAY_VAO_LAM, NGAY_NGHI_VIEC)";
//                CN.Execute(Sql);
//                Sql = " UPDATE BANG_LUONG SET SO_NGAY_LV = SN FROM BANG_LUONG INNER JOIN " + "( SELECT MS_CN, SO_NGAY AS SN FROM " + BANGTAM1 + ") A ON A.MS_CN=BANG_LUONG.MS_CN WHERE BANG_LUONG.THANG='" + Format(ThangTL, "MM/DD/YYYY") + "'";
//                if (cmbDV.GetKeyValue != "ALL")
//                    Sql = Sql + " AND  BANG_LUONG.MSDV='" + cmbDV.GetKeyValue + "'";
//                if (cmbPB.GetKeyValue != "ALL")
//                    Sql = Sql + " AND BANG_LUONG.MS_PB='" + cmbPB.GetKeyValue + "'";
//                CN.Execute(Sql);

//                // cap nhat ten chuc vu
//                Sql = "UPDATE BANG_LUONG SET TEN_CHUC_VU=T FROM BANG_LUONG INNER JOIN " + "( SELECT MS_CHUC_VU , TEN_CHUC_VU AS T FROM CHUC_VU ) A ON A.MS_CHUC_VU=BANG_LUONG.MS_CHUC_VU WHERE BANG_LUONG.THANG='" + Format(ThangTL, "MM/DD/YYYY") + "'";
//                if (cmbDV.GetKeyValue != "ALL")
//                    Sql = Sql + " AND  BANG_LUONG.MSDV='" + cmbDV.GetKeyValue + "'";
//                if (cmbPB.GetKeyValue != "ALL")
//                    Sql = Sql + " AND BANG_LUONG.MS_PB='" + cmbPB.GetKeyValue + "'";
//                CN.Execute(Sql);
//                prb.Value = 7;
//                DoEvents();

//                // cap nhat ngay cong chuan CAP_NHAT=1
//                Sql = "UPDATE BANG_LUONG  SET NGAY_CONG_CHUAN=" + Val(txtNgayCongChuan.Text) + ", NC_LV_TT = " + Val(txtNCLV.Text) + " WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "'";
//                if (cmbDV.GetKeyValue != "ALL")
//                    Sql = Sql + " AND MSDV='" + cmbDV.GetKeyValue + "'";
//                if (cmbPB.GetKeyValue != "ALL")
//                    Sql = Sql + " AND MS_PB='" + cmbPB.GetKeyValue + "'";
//                CN.Execute(Sql);

//                // TINH MUC DONG BHXH, YT
//                Sql = "SELECT ISNULL(PT_BHXH,0) , ISNULL(PT_BHYT,0),ISNULL(PT_BHTN,0), ISNULL(PT_CD,0) FROM LUONG_TOI_THIEU WHERE NGAY_QD=( SELECT MAX(NGAY_QD) FROM LUONG_TOI_THIEU WHERE NGAY_QD<='" + Format(ThangTL, "MM/DD/YYYY") + "')";
//                ;/* Cannot convert EmptyStatementSyntax, CONVERSION ERROR: Conversion for EmptyStatement not implemented, please report this issue in '' at character 9747
//   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.DefaultVisit(SyntaxNode node)
//   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitEmptyStatement(EmptyStatementSyntax node)
//   at Microsoft.CodeAnalysis.VisualBasic.Syntax.EmptyStatementSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
//   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
//   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
//   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

//Input: 
//    Set rs = New Recordset

// */
//                rs.Open(Sql, CN, 1, 2);
//                if (rs.EOF == false)
//                {
//                    Sql = "UPDATE BANG_LUONG SET MUC_DONG_BHXH=" + rs(0) + ",MUC_DONG_BHYT=" + rs(1) + " , MUC_DONG_BHTN =" + rs(2) + ", MUC_DONG_CD =" + rs(3) + " WHERE BANG_LUONG.THANG='" + Format(ThangTL, "MM/DD/YYYY") + "'";
//                    if (cmbDV.GetKeyValue != "ALL")
//                        Sql = Sql + " AND MSDV='" + cmbDV.GetKeyValue + "'";
//                    if (cmbPB.GetKeyValue != "ALL")
//                        Sql = Sql + " AND MS_PB='" + cmbPB.GetKeyValue + "'";
//                    CN.Execute(Sql);
//                }

//                prb.Value = 8;
//                DoEvents();

//                // TINH MUC LUONG CO BAN

//                BANGTAM1 = "bangtam1" + IDIP;
//                bangtam2 = "bangtam2" + IDIP;
//                DROP_TABLE(BANGTAM1);
//                DROP_TABLE(bangtam2);
//                if (cmbDV.GetKeyValue != "ALL")
//                    DON_VI = " AND BANG_LUONG.MSDV='" + cmbDV.GetKeyValue + "'";
//                if (cmbPB.GetKeyValue != "ALL")
//                    MS_PB = " AND BANG_LUONG.MS_PB='" + cmbPB.GetKeyValue + "'";

//                Sql = "SELECT MS_CN,MAX(NGAY_HIEU_LUC) AS NGAY INTO " + BANGTAM1 + " FROM LUONG_CO_BAN " + "WHERE NGAY_HIEU_LUC<='" + Format(DenNgayTL, "MM/DD/YYYY") + "' ";
//                Sql = Sql + " GROUP BY MS_CN";
//                CN.Execute(Sql);
//                prb.Value = 9;
//                DoEvents();

//                DROP_TABLE(bangtam2);
//                Sql = "SELECT LUONG_CO_BAN.MS_CN,(isnull(LUONG_CO_BAN.MUC_LUONG_THUC,0)) AS MUC_LUONG_THUC " + " INTO " + bangtam2 + " FROM LUONG_CO_BAN " + " INNER JOIN " + " " + BANGTAM1 + " ON LUONG_CO_BAN.NGAY_HIEU_LUC = " + BANGTAM1 + ".NGAY AND " + " LUONG_CO_BAN.MS_CN = " + BANGTAM1 + ".MS_CN ";
//                CN.Execute(Sql);
//                Sql = "UPDATE BANG_LUONG SET BANG_LUONG.LUONG_HDLD=LCB FROM BANG_LUONG INNER JOIN " + "( SELECT MS_CN, MUC_LUONG_THUC AS LCB  FROM  " + bangtam2 + ") A ON A.MS_CN=BANG_LUONG.MS_CN" + " WHERE BANG_LUONG.THANG='" + Format(ThangTL, "MM/DD/YYYY") + "'" + DON_VI + MS_PB;
//                CN.Execute(Sql);

//                // Cap nhat luong co bang bu luong cho nguoi thu viec
//                Sql = "UPDATE BANG_LUONG SET LUONG_HDLD = T2.MBL_TV FROM BANG_LUONG T1 INNER JOIN " + "(SELECT MSDV, MBL_TV FROM MUC_BU_LUONG_XN WHERE THANG = (SELECT MAX(THANG) FROM MUC_BU_LUONG_XN " + "WHERE THANG <='" + Format(ThangTL, "MM/DD/YYYY") + "')) T2 ON T1.MSDV = T2.MSDV " + "WHERE T1.THANG = '" + Format(ThangTL, "MM/DD/YYYY") + "' AND LEFT(T1.TEN_CHUC_VU,2) IN ('TV')";
//                if (cmbDV.GetKeyValue != "ALL")
//                    Sql = Sql + " AND T1.MSDV='" + cmbDV.GetKeyValue + "'";
//                if (cmbPB.GetKeyValue != "ALL")
//                    Sql = Sql + " AND T1.MS_PB='" + cmbPB.GetKeyValue + "'";
//                CN.Execute(Sql);
//                prb.Value = 10;
//                DoEvents();

//                // Cap nhat luong co bang bu luong cho nguoi hoc viec
//                Sql = "UPDATE BANG_LUONG SET LUONG_HDLD = T2.MBL_HV FROM BANG_LUONG T1 INNER JOIN " + "(SELECT MSDV, MBL_HV FROM MUC_BU_LUONG_XN WHERE THANG = (SELECT MAX(THANG) FROM MUC_BU_LUONG_XN " + "WHERE THANG <='" + Format(ThangTL, "MM/DD/YYYY") + "')) T2 ON T1.MSDV = T2.MSDV " + "WHERE T1.THANG = '" + Format(ThangTL, "MM/DD/YYYY") + "' AND LEFT(T1.TEN_CHUC_VU,2) IN ('HV')";
//                if (cmbDV.GetKeyValue != "ALL")
//                    Sql = Sql + " AND  T1.MSDV='" + cmbDV.GetKeyValue + "'";
//                if (cmbPB.GetKeyValue != "ALL")
//                    Sql = Sql + " AND T1.MS_PB='" + cmbPB.GetKeyValue + "'";
//                CN.Execute(Sql);


//                // tinh cong tg, ngay cong, cong san pham
//                // Sql = " UPDATE BANG_LUONG SET NGAY_CONG=NC " & _
//                // " FROM BANG_LUONG INNER JOIN (" & _
//                // " SELECT MS_CN,MS_TO, MSDV, CONG_TG AS CTG, NGAY_CONG AS NC, CONG_SP AS CSP, CHUC_VU,PHAN_TRAM  " & _
//                // " FROM CHAM_CONG_THANG " & _
//                // " WHERE THANG='" & Format(ThangTL, "MM/DD/YYYY") & "')" & _
//                // " A ON A.MS_CN=BANG_LUONG.MS_CN AND A.MS_TO=BANG_LUONG.MS_PB AND A.MSDV=BANG_LUONG.MSDV " & _
//                // " AND BANG_LUONG.MS_CHUC_VU =A.CHUC_VU  AND A.PHAN_TRAM=BANG_LUONG.PHAN_TRAM  " & _
//                // " WHERE BANG_LUONG.THANG='" & Format(ThangTL, "MM/DD/YYYY") & "'" & DON_VI & MS_PB
//                // CN.Execute Sql

//                prb.Value = 15;
//                DoEvents();

//                string TAM;
//                // TINH LUONG SP
//                DROP_TABLE(BangTam);
//                // lay luong san pham
//                Sql = " SELECT PHIEU_CONG_DOAN.MS_CN, SUM( ISNULL(PHIEU_CONG_DOAN.SO_LUONG,0) )AS TSP, " + " SUM( ISNULL(PHIEU_CONG_DOAN.SO_LUONG,0)* ISNULL(QUI_TRINH_CONG_NGHE_CHI_TIET.DON_GIA_THUC_TE ,0) ) AS T_TIEN, " + " PHIEU_CONG_DOAN.STT_CHUYEN AS CHUYEN_SD , CONVERT(NVARCHAR(10) , NULL) AS TO_CHINH, CONVERT(NVARCHAR(10),NULL) AS MS_TO, " + " CONVERT(FLOAT, NULL) AS PS, PHIEU_CONG_DOAN.MS_CD, LOAI_CUM.LOAI_CUM " + " INTO " + BangTam + " FROM LOAI_CUM INNER JOIN CUM ON LOAI_CUM.LOAI_CUM = CUM.LOAI_CUM INNER JOIN " + " CONG_DOAN ON CUM.MS_CUM = CONG_DOAN.MS_CUM INNER JOIN PHIEU_CONG_DOAN INNER JOIN " + " QUI_TRINH_CONG_NGHE_CHI_TIET ON PHIEU_CONG_DOAN.MS_DDH = QUI_TRINH_CONG_NGHE_CHI_TIET.MS_DDH AND " + " PHIEU_CONG_DOAN.MS_MH = QUI_TRINH_CONG_NGHE_CHI_TIET.MS_MH AND PHIEU_CONG_DOAN.MS_CD = QUI_TRINH_CONG_NGHE_CHI_TIET.MS_CD AND " + " PHIEU_CONG_DOAN.CHUYEN_SD = QUI_TRINH_CONG_NGHE_CHI_TIET.STT_CHUYEN AND PHIEU_CONG_DOAN.[ORDER] = QUI_TRINH_CONG_NGHE_CHI_TIET.[ORDER] ON " + " CONG_DOAN.MS_CD = QUI_TRINH_CONG_NGHE_CHI_TIET.MS_CD " + " WHERE  (PHIEU_CONG_DOAN.NGAY BETWEEN '" + Format(TuNgayTL, "MM/dd/yyyy") + "' AND '" + Format(DenNgayTL, "MM/dd/yyyy") + "' ) ";
//                if (cmbDV.GetKeyValue != "ALL ")
//                    Sql = Sql + " AND PHIEU_CONG_DOAN.MS_CN IN ( SELECT MS_CN FROM " + BangTamCN + " WHERE CHI_NHANH='" + cmbDV.GetKeyValue + "')";
//                if (cmbPB.GetKeyValue != "ALL")
//                    Sql = Sql + " AND PHIEU_CONG_DOAN.MS_CN IN ( SELECT MS_CN FROM " + BangTamCN + " WHERE MS_TO='" + cmbPB.GetKeyValue + "')";
//                Sql = Sql + " GROUP BY PHIEU_CONG_DOAN.MS_CN,PHIEU_CONG_DOAN.STT_CHUYEN ,PHIEU_CONG_DOAN.MS_CD ,LOAI_CUM.LOAI_CUM  ";
//                CN.Execute(Sql);
//                prb.Value = 30;
//                DoEvents();
//                // cap nhat cong doan phat sinh
//                Sql = "UPDATE " + BangTam + " SET PS=S FROM " + BangTam + " INNER JOIN " + "( SELECT ISNULL(CUM.CUM_PS,0) AS S, CONG_DOAN.MS_CD FROM  CUM INNER JOIN CONG_DOAN ON CUM.MS_CUM = CONG_DOAN.MS_CUM ) A " + " ON A.MS_CD=" + BangTam + ".MS_CD";
//                CN.Execute(Sql);

//                // cap nhat to ma cong nhan thuc hien thuc hien cong doan theo chuyen
//                Sql = "UPDATE " + BangTam + " SET MS_TO=MSTO FROM " + BangTam + " INNER JOIN " + "( SELECT MS_TO AS MSTO,STT_CHUYEN FROM [TO] ) A ON A.STT_CHUYEN=" + BangTam + ".CHUYEN_SD";
//                CN.Execute(Sql);
//                // cap nhat to chinh cho cong nhan thuc hien cong doan
//                Sql = "UPDATE " + BangTam + " SET TO_CHINH=T FROM " + BangTam + " INNER JOIN " + "( SELECT MS_CN,MS_PB AS T FROM BANG_LUONG  WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "')" + " A ON A.MS_CN=" + BangTam + ".MS_CN";
//                CN.Execute(Sql);
//                prb.Value = 31;
//                DoEvents();

//                Sql = " UPDATE BANG_LUONG SET TONG_SP=round(TSL,0) FROM  BANG_LUONG INNER JOIN " + "( SELECT MS_CN, SUM(TSP) AS TSL FROM " + BangTam + " GROUP BY MS_CN ) A ON A.MS_CN=BANG_LUONG.MS_CN WHERE BANG_LUONG.THANG='" + Format(ThangTL, "MM/DD/YYYY") + "' ";
//                CN.Execute(Sql);
//                Sql = " UPDATE BANG_LUONG  SET LUONG_SP =ROUND(LSP,0) FROM BANG_LUONG INNER JOIN " + "( SELECT MS_CN,SUM(T_TIEN)AS LSP  FROM " + BangTam + " WHERE ISNULL(MS_TO,'')=ISNULL(TO_CHINH,'') AND ISNULL(PS,0) =0 GROUP BY MS_CN ) A ON A.MS_CN=BANG_LUONG.MS_CN " + " WHERE BANG_LUONG.THANG='" + Format(ThangTL, "MM/DD/YYYY") + "'";
//                CN.Execute(Sql);
//                prb.Value = 32;
//                DoEvents();

//                // TINH LUONG SP LAM BP KHAC
//                Sql = " UPDATE BANG_LUONG  SET LUONG_PB_KHAC=ROUND(LSP,0) FROM BANG_LUONG INNER JOIN " + "( SELECT MS_CN,SUM(T_TIEN)  AS LSP FROM " + BangTam + " WHERE ISNULL(MS_TO,'') <> ISNULL(TO_CHINH,'')  AND ISNULL(PS,0) =0 GROUP BY MS_CN) A ON A.MS_CN=BANG_LUONG.MS_CN " + " WHERE BANG_LUONG.THANG='" + Format(ThangTL, "MM/DD/YYYY") + "' ";
//                CN.Execute(Sql);

//                // TINH LUONG CONG DOAN PHAT SINH
//                Sql = " UPDATE BANG_LUONG  SET TIEN_CDPS =ROUND(LSP,0) FROM BANG_LUONG INNER JOIN " + "( SELECT MS_CN,SUM(T_TIEN)AS LSP  FROM " + BangTam + " WHERE  ISNULL(PS,0) <> 0  GROUP BY MS_CN ) A ON A.MS_CN=BANG_LUONG.MS_CN " + " WHERE BANG_LUONG.THANG='" + Format(ThangTL, "MM/DD/YYYY") + "'";  // DON_VI & MS_PB 'AND CACH_TINH='SP'
//                CN.Execute(Sql);
//                prb.Value = 35;
//                DoEvents();
//                // het tinh luong san pham

//                // tinh luong che do lao dong nu
//                Sql = " UPDATE BANG_LUONG SET LUONG_CD_LDN=ISNULL(LUONG_HDLD,0) * ISNULL(CHE_DO_LDN,0) /( NGAY_CONG_CHUAN*8) " + "  WHERE BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'" + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                prb.Value = 40;
//                prb.Refresh();

//                // tinh luong lam them che do lao dong nu
//                Sql = " UPDATE BANG_LUONG SET LUONG_LAM_THEM_CDLDN=ISNULL(LUONG_HDLD,0) * ISNULL(GIO_LAM_THEM_CDLDN,0) * 0.5 /(NGAY_CONG_CHUAN*8) " + " WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "'" + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                prb.Value = 45;
//                DoEvents();

//                // set phong ban tinh tang ca neu co
//                Sql = " UPDATE BANG_LUONG SET TINH_TC=1 WHERE MS_NHOM IN " + " ( SELECT MS_PB FROM PHONG_BAN_TINH_TC WHERE THANG=" + "( SELECT MAX(THANG) FROM PHONG_BAN_TINH_TC WHERE THANG <='" + Format(ThangTL, "MM/DD/YYYY") + "')) AND THANG='" + Format(ThangTL, "MM/DD/YYYY") + "' AND CACH_TINH IN ('DT','TT')";
//                CN.Execute(Sql);
//                Sql = " UPDATE BANG_LUONG SET TC_1621=0, TC_DEM=0, TC_CN=0, TC_NL=0 WHERE CACH_TINH IN ('DT','TT','LK','LCT','LQC','LKTC','LKTX','LN') AND ISNULL(TINH_TC,0)=0 " + " AND THANG = '" + Format(ThangTL, "mm/dd/yyyy") + "'" + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                prb.Value = 50;
//                DoEvents();

//                // tinh tien chuyen can
//                Sql = "UPDATE BANG_LUONG SET TIEN_CHUYEN_CAN=round(T,0) FROM BANG_LUONG INNER JOIN (" + "  SELECT dbo.DIEM_TIEN.DIEM, dbo.DIEM_TIEN.DON_VI, dbo.DIEM_TIEN.TIEN AS T " + "  FROM   dbo.DIEM_TIEN INNER JOIN  ( SELECT  MAX(THANG) AS THANG, DON_VI" + "    From dbo.DIEM_TIEN   WHERE     (THANG <= '" + Format(ThangTL, "mm/dd/yyyy") + "') " + "  GROUP BY DON_VI   )  v3 ON dbo.DIEM_TIEN.THANG = v3.THANG AND dbo.DIEM_TIEN.DON_VI = v3.DON_VI) A ON A.DIEM =BANG_LUONG.CHUYEN_CAN AND A.DON_VI = BANG_LUONG.MSDV " + " WHERE BANG_lUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "' AND BL_CHINH = 1" + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                prb.Value = 55;
//                DoEvents();

//                // luong thoi gian
//                // Dim danhsachbu As String
//                // danhsachbu = "danhsachbu" & IDIP
//                // DROP_TABLE danhsachbu

//                Sql = "UPDATE BANG_LUONG SET LUONG_HDLD1 = ISNULL(LUONG_HDLD,0) WHERE THANG = '" + Format(ThangTL, "mm/dd/yyyy") + "'" + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                Sql = "UPDATE BANG_LUONG SET LUONG_HDLD1 = LK FROM BANG_LUONG T1 INNER JOIN (SELECT MS_CN, LUONG_KHOAN AS LK, MS_CHUC_VU FROM LUONG_KHOAN " + "WHERE THANG = '" + Format(ThangTL, "mm/dd/yyyy") + "') T2 ON T1.MS_CN = T2.MS_CN AND T1.MS_CHUC_VU = T2.MS_CHUC_VU " + "WHERE T1.THANG = '" + Format(ThangTL, "mm/dd/yyyy") + "'";
//                if (cmbDV.GetKeyValue != "ALL")
//                    Sql = Sql + " AND  T1.MSDV='" + cmbDV.GetKeyValue + "'";
//                if (cmbPB.GetKeyValue != "ALL")
//                    Sql = Sql + " AND T1.MS_PB='" + cmbPB.GetKeyValue + "'";
//                CN.Execute(Sql);

//                // tinh luong theo tung cach tinh luong
//                // sp
//                Sql = "UPDATE BANG_LUONG SET TT_TIENPHEP = CASE WHEN ISNULL(PHEP_NAM,0)+ISNULL(PHEP_THU_BAY,0) > 0 THEN ISNULL(LUONG_HDLD,0)/NGAY_CONG_CHUAN*(ISNULL(PHEP_NAM,0)+ISNULL(PHEP_THU_BAY,0)) ELSE 0 END";
//                Sql = Sql + ", TT_VIECRIENG = CASE WHEN ISNULL(VIEC_RIENG_CLUONG,0) > 0 THEN ISNULL(LUONG_HDLD,0)/NGAY_CONG_CHUAN*ISNULL(VIEC_RIENG_CLUONG,0) ELSE 0 END";
//                Sql = Sql + ", TT_LETET = CASE WHEN ISNULL(LE_TET,0) > 0 THEN ISNULL(LUONG_HDLD,0)/NGAY_CONG_CHUAN*ISNULL(LE_TET,0) ELSE 0 END";
//                Sql = Sql + ", TT_CDLDN = CASE WHEN ISNULL(CHE_DO_LDN,0) > 0 THEN ISNULL(LUONG_HDLD,0)/NGAY_CONG_CHUAN*ISNULL(CHE_DO_LDN,0) ELSE 0 END";
//                Sql = Sql + ", LUONG_TC_1621 = CASE WHEN ISNULL(TONG_GIO_CONG,0) > 0 THEN (((ISNULL(LUONG_SP,0)+ISNULL(LUONG_PB_KHAC,0)+ISNULL(TIEN_CDPS,0))/(ISNULL(TONG_GIO_CONG,0)+ISNULL(TC_1621,0)" + "+ISNULL(TC_CN,0)+ISNULL(TC_NL,0)+ISNULL(LAM_DEM,0)+ISNULL(TC_DEM,0)))*50/100)*ISNULL(TC_1621,0) ELSE 0 END";
//                Sql = Sql + ", LUONG_TC_DEM = CASE WHEN ISNULL(TONG_GIO_CONG,0) > 0 THEN ((ISNULL(LUONG_SP,0)+ISNULL(LUONG_PB_KHAC,0)+ISNULL(TIEN_CDPS,0))/(ISNULL(TONG_GIO_CONG,0)+ISNULL(TC_1621,0)" + "+ISNULL(TC_CN,0)+ISNULL(TC_NL,0)+ISNULL(LAM_DEM,0)+ISNULL(TC_DEM,0)))*ISNULL(TC_DEM,0) ELSE 0 END";
//                Sql = Sql + ", LUONG_TC_CN = CASE WHEN ISNULL(TONG_GIO_CONG,0) > 0 THEN ((ISNULL(LUONG_SP,0)+ISNULL(LUONG_PB_KHAC,0)+ISNULL(TIEN_CDPS,0))/(ISNULL(TONG_GIO_CONG,0)+ISNULL(TC_1621,0)" + "+ISNULL(TC_CN,0)+ISNULL(TC_NL,0)+ISNULL(LAM_DEM,0)+ISNULL(TC_DEM,0)))*ISNULL(TC_CN,0) ELSE 0 END";
//                Sql = Sql + " WHERE BANG_LUONG.CACH_TINH IN ('SP') AND BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'" + DON_VI + MS_PB;
//                CN.Execute(Sql);

//                // tinh luong tang ca


//                Sql = "UPDATE BANG_LUONG SET LUONG_TG=round(ISNULL(LUONG_HDLD1,0) * ISNULL(NGAY_CONG,0) / NGAY_CONG_CHUAN,0) " + "WHERE BANG_LUONG.CACH_TINH IN ('TG','LTT') AND BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "' " + DON_VI + MS_PB;
//                CN.Execute(Sql);

//                // luong net
//                Sql = "UPDATE BANG_LUONG SET LUONG_TG=ISNULL(LUONG_HDLD1,0) " + "  WHERE BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "' AND BANG_LUONG.CACH_TINH = 'LN' " + DON_VI + MS_PB;
//                CN.Execute(Sql);

//                // tinh luong viec rieng
//                Sql = "UPDATE BANG_LUONG SET LUONG_VIEC_RIENG=round(ISNULL(LUONG_HDLD1,0) * ISNULL(VIEC_RIENG_CLUONG,0) /( 26),0) " + "  WHERE BANG_LUONG.CACH_TINH IN ('LK') AND BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'" + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                Sql = "UPDATE BANG_LUONG SET LUONG_VIEC_RIENG=round(ISNULL(LUONG_HDLD,0) * ISNULL(VIEC_RIENG_CLUONG,0) /( 26),0) " + "  WHERE BANG_LUONG.CACH_TINH NOT IN ('LK') AND BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'" + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                prb.Value = 56;
//                prb.Refresh();
//                this.Refresh();
//                // tinh luong phep
//                // luong khoan
//                Sql = "UPDATE BANG_LUONG SET LUONG_NGAY_PHEP=round(ISNULL(LUONG_HDLD1,0) * ISNULL(PHEP_NAM,0) / 26,0) " + "  WHERE BANG_LUONG.CACH_TINH IN ('LK') AND BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'" + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                // luong thoi gian, sp
//                Sql = "UPDATE BANG_LUONG SET LUONG_NGAY_PHEP=round(ISNULL(LUONG_HDLD,0) * ISNULL(PHEP_NAM,0) / 26,0) " + "  WHERE BANG_LUONG.CACH_TINH NOT IN ('LK','LN') AND BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'" + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                prb.Value = 57;
//                prb.Refresh();
//                this.Refresh();
//                // TINH LUONG PHEP THU 7
//                Sql = "UPDATE BANG_LUONG SET TIEN_PHEP_THU_BAY=round(ISNULL(LUONG_HDLD1,0) * ISNULL(PHEP_THU_BAY,0) / 26,0) " + "  WHERE BANG_LUONG.CACH_TINH IN ('LK') AND BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'" + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                Sql = "UPDATE BANG_LUONG SET TIEN_PHEP_THU_BAY=round(ISNULL(LUONG_HDLD,0) * ISNULL(PHEP_THU_BAY,0) / 26,0) " + "  WHERE BANG_LUONG.CACH_TINH NOT IN ('LK','LN') AND BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'" + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                prb.Value = 57;
//                prb.Refresh();
//                this.Refresh();
//                // tinh luong le , tet
//                Sql = " UPDATE BANG_LUONG SET LUONG_LE_TET=round(ISNULL(LUONG_HDLD1,0) * ISNULL(LE_TET,0) / 26,0) " + " WHERE BANG_LUONG.CACH_TINH NOT IN ('LN') AND BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'" + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                prb.Value = 58;
//                prb.Refresh();
//                this.Refresh();

//                // tinh tong luong sp
//                Sql = " update bang_Luong set TONG_CONG_LSP = isnull(luong_sp,0) + isnull(luong_pb_khac,0) + isnull(tien_cdps,0) " + " where thang='" + Format(ThangTL, "mm/dd/yyyy") + "'" + DON_VI + MS_PB;
//                CN.Execute(Sql);

//                // tinh luong cbql (LTC, LKTC, LKTX)
//                Sql = "UPDATE BANG_LUONG SET LUONG_CD = CASE WHEN LEFT(T1.TEN_CHUC_VU,2) = 'TV' THEN T2.MUC_LUONG_TV ELSE T2.MUC_LUONG END " + ", PC_DT = T2.PHU_CAP FROM BANG_LUONG T1 " + "INNER JOIN (SELECT MSDV, CACH_TINH, MUC_LUONG, MUC_LUONG_TV, PHU_CAP " + "FROM QUI_DINH_LUONG_CBQL_CHUYEN WHERE THANG = (SELECT MAX(THANG) FROM QUI_DINH_LUONG_CBQL_CHUYEN WHERE THANG <= '" + Format(ThangTL, "MM/DD/YYYY") + "')) T2 " + "ON T1.MSDV = T2.MSDV AND T1.CACH_TINH = T2.CACH_TINH " + "WHERE T1.THANG = '" + Format(ThangTL, "MM/DD/YYYY") + "'";
//                if (cmbDV.GetKeyValue != "ALL")
//                    Sql = Sql + " AND  T1.MSDV='" + cmbDV.GetKeyValue + "'";
//                if (cmbPB.GetKeyValue != "ALL")
//                    Sql = Sql + " AND T1.MS_PB='" + cmbPB.GetKeyValue + "'";
//                CN.Execute(Sql);

//                // Cap nhat muc hoanh thanh doanh thu thang
//                Sql = "UPDATE BANG_LUONG SET MUC_HT_DT = T2.MUC_CL FROM BANG_LUONG T1 " + "INNER JOIN (SELECT MS_TO, MUC_CL FROM MUC_CL_THANG WHERE THANG = (SELECT MAX(THANG) " + "FROM MUC_CL_THANG WHERE THANG <='" + Format(ThangTL, "MM/DD/YYYY") + "')) T2 ON T1.MS_PB = T2.MS_TO " + "WHERE T1.THANG = '" + Format(ThangTL, "MM/DD/YYYY") + "'";
//                if (cmbDV.GetKeyValue != "ALL")
//                    Sql = Sql + " AND  T1.MSDV='" + cmbDV.GetKeyValue + "'";
//                if (cmbPB.GetKeyValue != "ALL")
//                    Sql = Sql + " AND T1.MS_PB='" + cmbPB.GetKeyValue + "'";
//                CN.Execute(Sql);

//                // tinh bu luong thap
//                string dsb;
//                dsb = "danhsachbu" + IDIP;
//                DROP_TABLE(dsb);
//                // Khong phai la cong nhan moi, khong vi pham nghi khong phep
//                Sql = "select ms_cn,cach_tinh, (isnull(NGAY_CONG_LSP,0) + isnull(NGAY_CONG,0)) * 8 + isnull(GIO_TC_TG,0) + round(isnull(TC_1621,0) * 1.5 ,1) + round (isnull(TC_DEM,0)*1.95,1) + round(isnull(LAM_DEM,0) * 1.3,1) + round(isnull(TC_CN,0)*2,1) + round(isnull(TC_NL,0)*3,1)" + "  +isnull(VIEC_RIENG_CLUONG,0) * 8 + isnull(PHEP_NAM,5) * 8  + isnull(PHEP_THU_BAY,0) * 8 + isnull(LE_TET,0) * 8 " + "  + isnull(CD_LD_NU,0) + isnull(GIO_LAM_THEM_CDLDN,0) as Gio_cong, " + "  isnull(TONG_CONG_LSP,0) + isnull(LUONG_TG,0) + isnull(LUONG_TC_TG,0) + isnull(LUONG_CD_LDN,0) + isnull(LUONG_VIEC_RIENG,0) + isnull(LUONG_NGAY_PHEP,0) + isnull(TIEN_PHEP_THU_BAY,0) + isnull(LUONG_LE_TET,0) + isnull(LUONG_LAM_THEM_CDLDN,0) " + "  + isnull(LUONG_TC_1621,0) + isnull(LUONG_TC_DEM,0) + isnull(LUONG_LAM_DEM,0) + isnull(LUONG_TC_CN,0) + isnull(LUONG_TC_NL,0) as TienLuong, ISNULL(CONG_TG,0) + ISNULL(CONG_SP,0) +isnull(viec_rieng_cluong,0) + isnull(phep_nam,0) + isnull(le_tet,0) as cong_tinh_com,tien_chuyen_can ,ms_chuc_vu ,khong_phep ,msdv" + " into " + dsb + "  From bang_luong " + "  where thang='" + Format(ThangTL, "MM/dd/yyyy") + " ' and cach_tinh in ('tg','LTT','sp') " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                ;/* Cannot convert EmptyStatementSyntax, CONVERSION ERROR: Conversion for EmptyStatement not implemented, please report this issue in '' at character 29327
//   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.DefaultVisit(SyntaxNode node)
//   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitEmptyStatement(EmptyStatementSyntax node)
//   at Microsoft.CodeAnalysis.VisualBasic.Syntax.EmptyStatementSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
//   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
//   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
//   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

//Input: 
          
//          ' and ms_chuc_vu not in ( select ms_chuc_vu from chuc_vu where right(ten_chuc_vu,2) in ('HV','TV')) " & _
//              "  and isnull(khong_phep,0) =0
              
//    '
//        Dim BuQC As Double
//        BuQC = CDbl(txtMucBu.Text)
//        Dim BuKhacQC As Double
//        BuKhacQC = CDbl(txtBuKhac.Text)
//        ' khong phai la cong nhan HV,TV va khong vi pham nghi khong phep
//        'Dim TAM As String
//        TAM = "BULUONG" & IDIP  'tung them
//        DROP_TABLE TAM
//        Sql = "select ms_cn,gio_cong,tienluong, cong_tinh_com, tien_chuyen_can,ms_chuc_vu,msdv into " & TAM & " from " & dsb & " where  ms_chuc_vu not in ( select ms_chuc_vu from chuc_vu where right(ten_chuc_vu,2) in ('HV','TV'))  and isnull(khong_phep,0) =0"

// */
//                CN.Execute(Sql);
//                Sql = "alter table " + TAM + " add tien_bu float null";
//                CN.Execute(Sql);
//                // Sql = "update " & TAM & " set tien_bu=" & BuQC & " where left(ms_cn,2)='qc' or ms_chuc_vu in (select ms_chuc_vu from chuc_vu where right(ten_chuc_vu,3)='kcs')"
//                // CN.Execute Sql
//                // Sql = "update " & TAM & " set tien_bu=" & BuKhacQC & " where left(ms_cn,2)<>'qc' and  ms_chuc_vu not in (select ms_chuc_vu from chuc_vu where right(ten_chuc_vu,3)='kcs')"
//                // CN.Execute Sql
//                Sql = "update " + TAM + " set tien_bu=a.muc_bu from  " + TAM + " B inner join " + "(select MS_CHUC_VU , DON_VI,muc_bu from BU_THEO_CHUC_VU  where thang='" + Format(ThangTL, "mm/dd/yyyy") + "') a on a.ms_chuc_vu=b.ms_chuc_vu and a.don_vi=b.msdv ";
//                CN.Execute(Sql);
//                Sql = "delete from " + TAM + " where isnull(gio_cong,0) =0";
//                CN.Execute(Sql);
//                Sql = "alter table " + TAM + " add tien_so_sanh float null";
//                CN.Execute(Sql);
//                Sql = "update " + TAM + " set tien_so_sanh=isnull(tien_bu,0) * isnull(gio_cong,0) /208";
//                CN.Execute(Sql);
//                Sql = "alter table " + TAM + " add tien_can_bu float ";
//                CN.Execute(Sql);
//                Sql = "update " + TAM + " set tien_can_bu=round(isnull(tien_so_sanh,0),0) - round(isnull(tienluong,0),0) ";
//                CN.Execute(Sql);
//                Sql = "delete from " + TAM + " where isnull(tien_can_bu,0) <=0";
//                CN.Execute(Sql);
//                Sql = "update bang_luong set BU_LUONG_THAP=a.tien_can_bu from bang_luong b inner join ( select ms_cn, tien_can_bu from " + TAM + ") a on a.ms_cn=b.ms_cn where thang='" + Format(ThangTL, "MM/dd/yyyy") + "'";
//                CN.Execute(Sql);
//                DROP_TABLE(TAM);
//                // tinh tien tham nien
//                Sql = "SELECT MS_CN, MSDV, CACH_TINH, NGAY_CONG_CHUAN,  ISNULL(NGAY_CONG,0) AS NGAY_CONG into " + TAM + " from bang_luong where thang='" + Format(ThangTL, "MM/dd/yyyy") + "' ";
//                CN.Execute(Sql);
//                Sql = "alter table " + TAM + " add ngay_vao_lam datetime ";
//                CN.Execute(Sql);
//                Sql = "alter table " + TAM + " add ngay_nghi_viec datetime ";
//                CN.Execute(Sql);
//                Sql = "update " + TAM + " set ngay_vao_lam =a.ngay_vao_lam, ngay_nghi_viec =a.ngay_nghi_viec from " + TAM + " B inner join " + "( select ms_cn, ngay_vao_lam, ngay_nghi_viec from cong_nhan) a on a.ms_cn =b.ms_cn";
//                CN.Execute(Sql);
//                Sql = "update " + TAM + " set ngay_vao_lam ='" + Format(ThangTL, "mm/dd/yyyy") + "' where ngay_vao_lam is null";
//                CN.Execute(Sql);
//                Sql = "update " + TAM + " set ngay_nghi_viec ='" + Format(DenNgayTL, "mm/dd/yyyy") + "' where ngay_nghi_viec is null";
//                CN.Execute(Sql);
//                Sql = "update " + TAM + " set ngay_nghi_viec ='" + Format(DenNgayTL, "mm/dd/yyyy") + "' where ngay_nghi_viec > '" + Format(DenNgayTL, "mm/dd/yyyy") + "'";
//                CN.Execute(Sql);
//                Sql = "exec TinhThamNien '" + TAM + "','ngay_vao_lam','Ngay_nghi_viec'";
//                CN.Execute(Sql);
//                Sql = "alter table " + TAM + " add tien_tham_nien float ";
//                CN.Execute(Sql);
//                Sql = "update " + TAM + " set tien_tham_nien=a.tien from " + TAM + " b inner join " + "( SELECT THAM_NIEN.THANG, THAM_NIEN.THAM_NIEN, THAM_NIEN.TIEN, THAM_NIEN.DON_VI" + "  FROM   THAM_NIEN INNER JOIN(SELECT MAX(THANG) AS THANG, DON_VI  From THAM_NIEN " + "  where THANG<='" + Format(ThangTL, "mm/dd/yyyy") + "' GROUP BY DON_VI) v3 ON THAM_NIEN.THANG = v3.THANG AND THAM_NIEN.DON_VI = v3.DON_VI)" + " a on a.don_vi=b.msdv and a.tham_nien =b.sonam ";
//                CN.Execute(Sql);
//                Recordset rstamm = new Recordset();
//                Sql = "SELECT THAM_NIEN.THAM_NIEN, THAM_NIEN.DON_VI, THAM_NIEN.TIEN " + " FROM   THAM_NIEN INNER JOIN " + "(  SELECT MAX(THANG) AS THANG, DON_VI, MAX(THAM_NIEN) AS THAM_NIEN FROM   THAM_NIEN " + "   where THANG<='" + Format(ThangTL, "mm/dd/yyyy") + "' GROUP BY DON_VI) v3 ON THAM_NIEN.THANG = v3.THANG AND THAM_NIEN.DON_VI = v3.DON_VI AND " + "   THAM_NIEN.THAM_NIEN = v3.THAM_NIEN";
//                rstamm.Open(Sql, CN, 1, 2);
//                while (!rstamm.EOF)
//                {
//                    Sql = "update " + TAM + " set tien_tham_nien =" + rstamm.Fields("tien") + " where msdv='" + rstamm.Fields("don_vi").Value + "' and sonam >=" + rstamm.Fields("tham_nien").Value;
//                    CN.Execute(Sql);
//                    rstamm.MoveNext();
//                }
//                rstamm.Close();
//                Sql = "UPDATE " + TAM + " SET tien_tham_nien =ROUND(ISNULL(tien_tham_nien,0) * ISNULL(NGAY_CONG,0) / ISNULL(NGAY_CONG_CHUAN,1),0)";
//                CN.Execute(Sql);
//                Sql = "DELETE FROM " + TAM + " WHERE MS_CN IN (SELECT MS_CN FROM DS_CN_KHONG_TINH_PC " + "WHERE THANG = (SELECT MAX(THANG) FROM DS_CN_KHONG_TINH_PC WHERE THANG <= '" + Format(ThangTL, "mm/dd/yyyy") + "') AND THAM_NIEN = 1)";
//                CN.Execute(Sql);
//                Sql = "update bang_luong set tham_nien =a.tien_tham_nien from bang_luong b inner join " + "( select ms_cn, cach_tinh, isnull(tien_tham_nien,0) as tien_tham_nien from " + TAM + ") a " + "on a.ms_cn=b.ms_cn and a.cach_tinh = b.cach_tinh where thang='" + Format(ThangTL, "mm/dd/yyyy") + "'";
//                CN.Execute(Sql);

//                // tinh tien phu cap di lai (tung sua)
//                DROP_TABLE(TAM);
//                Sql = "SELECT T1.DON_VI, T1.SO_TIEN INTO " + TAM + " FROM QD_TC_DI_LAI T1 INNER JOIN " + "(SELECT DON_VI, MAX(NGAY_QD) NGAY_MAX FROM QD_TC_DI_LAI WHERE NGAY_QD <= '" + Format(ThangTL, "mm/dd/yyyy") + "' GROUP BY DON_VI) T2 " + "ON T1.NGAY_QD = T2.NGAY_MAX AND T1.DON_VI = T2.DON_VI";
//                CN.Execute(Sql);
//                // Cap nhat tien di lai
//                Sql = "UPDATE BANG_LUONG SET DI_LAI = ROUND(T2.SO_TIEN * (ISNULL(T1.NGAY_CONG,0)+ISNULL(T1.VIEC_RIENG_CLUONG,0))/" + "ISNULL(NGAY_CONG_CHUAN,1),0)FROM BANG_LUONG T1 INNER JOIN " + TAM + " T2 ON T1.MSDV = T2.DON_VI " + "WHERE T1.THANG = '" + Format(ThangTL, "mm/dd/yyyy") + "' AND ISNULL(T1.KHONG_PHEP,0) <= 0 AND MS_CN NOT IN (SELECT MS_CN FROM DS_CN_KHONG_TINH_PC " + "WHERE THANG = (SELECT MAX(THANG) FROM DS_CN_KHONG_TINH_PC WHERE THANG <= '" + Format(ThangTL, "mm/dd/yyyy") + "') AND ISNULL(DI_LAI,0)=1)";
//                if (cmbDV.GetKeyValue != "ALL")
//                    Sql = Sql + " AND  T1.MSDV='" + cmbDV.GetKeyValue + "'";
//                if (cmbPB.GetKeyValue != "ALL")
//                    Sql = Sql + " AND T1.MS_PB='" + cmbPB.GetKeyValue + "'";

//                CN.Execute(Sql);

//                // tinh tien phu cap con nho
//                DROP_TABLE(TAM);
//                Sql = "SELECT V1.MS_CN, V1.SN*V2.SO_TIEN AS TroCapConNho INTO " + TAM + " FROM (SELECT CONG_NHAN.CHI_NHANH AS MSDV, GIA_DINH.MS_CN, COUNT(GIA_DINH.MS_CN) SN " + "FROM GIA_DINH INNER JOIN CONG_NHAN ON GIA_DINH.MS_CN = CONG_NHAN.MS_CN " + "WHERE MS_QH = 4 AND DATEDIFF(MONTH,GIA_DINH.NGAY_SINH,'" + Format(ThangTL, "mm/dd/yyyy") + "') < 72 " + "AND NOT (GIA_DINH.NGAY_SINH  IS NULL) AND (CONG_NHAN.NGAY_NGHI_VIEC IS NULL OR NGAY_NGHI_VIEC > '" + Format(ThangTL, "mm/dd/yyyy") + "') " + "GROUP BY CONG_NHAN.CHI_NHANH, GIA_DINH.MS_CN) V1 " + "INNER JOIN (SELECT T1.DON_VI, T1.SO_TIEN FROM QD_TC_CON_NHO T1 INNER JOIN " + "(SELECT DON_VI, MAX(NGAY_QD) NGAY_MAX FROM QD_TC_CON_NHO WHERE NGAY_QD <= '" + Format(ThangTL, "mm/dd/yyyy") + "' GROUP BY DON_VI) T2 " + "ON T1.NGAY_QD = T2.NGAY_MAX AND T1.DON_VI = T2.DON_VI) V2 ON V1.MSDV = V2.DON_VI";
//                CN.Execute(Sql);
//                Sql = "DELETE FROM " + TAM + " WHERE MS_CN IN (SELECT MS_CN FROM DS_CN_KHONG_TINH_PC " + "WHERE THANG = (SELECT MAX(THANG) FROM DS_CN_KHONG_TINH_PC WHERE THANG <= '" + Format(ThangTL, "mm/dd/yyyy") + "') AND CON_NHO = 1)";
//                CN.Execute(Sql);
//                Sql = "UPDATE BANG_LUONG SET CON_NHO = CASE WHEN (ISNULL(T3.TC,0))/ISNULL(NGAY_CONG_CHUAN,1)>=0.5 THEN T2.TroCapConNho ELSE 0 END " + "FROM BANG_LUONG T1 INNER JOIN " + TAM + " T2 ON T1.MS_CN = T2.MS_CN " + "INNER JOIN (SELECT MS_CN, SUM(NGAY_CONG) AS TC FROM BANG_LUONG WHERE THANG = '" + Format(ThangTL, "mm/dd/yyyy") + "' GROUP BY MS_CN) T3 " + "ON T1.MS_CN = T3.MS_CN " + "WHERE T1.THANG = '" + Format(ThangTL, "mm/dd/yyyy") + "' AND T1.BL_CHINH = 1";
//                if (cmbDV.GetKeyValue != "ALL")
//                    Sql = Sql + " AND  T1.MSDV='" + cmbDV.GetKeyValue + "'";
//                if (cmbPB.GetKeyValue != "ALL")
//                    Sql = Sql + " AND T1.MS_PB='" + cmbPB.GetKeyValue + "'";
//                CN.Execute(Sql);

//                // tinh tien phu cap nguyet san
//                DROP_TABLE(TAM);
//                Sql = "SELECT MS_CN INTO " + TAM + " FROM CONG_NHAN WHERE PHAI = 0 AND " + "CASE WHEN NGAY_SINH IS NULL THEN YEAR('" + Format(ThangTL, "mm/dd/yyyy") + "') - NAM_SINH " + "ELSE YEAR('" + Format(ThangTL, "mm/dd/yyyy") + "') - YEAR(NGAY_SINH) END <= 50 " + "AND MS_CN NOT IN (SELECT MS_CN FROM DS_CN_KHONG_TINH_PC " + "WHERE THANG = (SELECT MAX(THANG) FROM DS_CN_KHONG_TINH_PC WHERE THANG <= '" + Format(ThangTL, "mm/dd/yyyy") + "') AND NGUYET_SAN = 1) ";
//                CN.Execute(Sql);
//                Sql = "UPDATE BANG_LUONG SET NGUYET_SAN = CASE WHEN ISNULL(T1.NGAY_CONG,0)/ISNULL(NC_LV_TT,1) >= 0.5 " + "THEN ROUND(1.5 * (ISNULL(LUONG_HDLD,0)/(ISNULL(NGAY_CONG_CHUAN,1)*8)),0) ELSE 0 END " + "FROM BANG_LUONG T1 INNER JOIN " + TAM + " T2 ON T1.MS_CN = T2.MS_CN " + "INNER JOIN (SELECT MS_CN, SUM(NGAY_CONG) AS TC FROM BANG_LUONG WHERE THANG = '" + Format(ThangTL, "mm/dd/yyyy") + "' GROUP BY MS_CN) T3 " + "ON T1.MS_CN = T3.MS_CN " + "WHERE T1.THANG = '" + Format(ThangTL, "MM/dd/yyyy") + "' AND T1.BL_CHINH = 1";
//                if (cmbDV.GetKeyValue != "ALL")
//                    Sql = Sql + " AND  T1.MSDV='" + cmbDV.GetKeyValue + "'";
//                if (cmbPB.GetKeyValue != "ALL")
//                    Sql = Sql + " AND T1.MS_PB='" + cmbPB.GetKeyValue + "'";
//                CN.Execute(Sql);

//                // hoc viec khong tinh pc di lai, chuyen can, cong nho, nguyet san
//                Sql = "UPDATE BANG_LUONG SET CHUYEN_CAN = 0, TIEN_CHUYEN_CAN = 0, DI_LAI = 0, CON_NHO = 0, NGUYET_SAN = 0 " + "WHERE THANG = '" + Format(ThangTL, "mm/dd/yyyy") + "' AND LEFT(TEN_CHUC_VU,2) IN ('HV')" + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                // thu viec khong tinh pc di lai
//                Sql = "UPDATE BANG_LUONG SET DI_LAI = 0, CON_NHO = 0, NGUYET_SAN = 0 WHERE THANG = '" + Format(ThangTL, "mm/dd/yyyy") + "' AND LEFT(TEN_CHUC_VU,2) IN ('TV')" + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                // ----------------------------------
//                DROP_TABLE(TAM);
//                Sql = " SELECT DISTINCT MS_CN INTO " + TAM + " FROM CHAM_CONG_CHI_TIET_VANG_KHACH WHERE NGAY BETWEEN '" + Format(TuNgayTL, "MM/DD/YYYY") + "' AND '" + Format(DenNgayTL, "MM/DD/YYYY") + "'" + " AND  MS_LY_DO_VANG IN ('TS','GS','T3','DH')";
//                CN.Execute(Sql);
//                Sql = " update bang_luong set BU_LUONG_THAP =0 where  thang='" + Format(ThangTL, "mm/dd/yyyy") + "'" + " and MS_CN IN ( SELECT MS_CN FROM " + TAM + ")";
//                CN.Execute(Sql);

//                // tinh tien bo sung
//                Sql = "UPDATE BANG_LUONG SET TIEN_BO_SUNG =round(T,0) FROM BANG_LUONG INNER JOIN ( SELECT MS_CN , TIEN_BO_SUNG  AS T " + "  FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "mm/dd/yyyy") + "') A ON " + " A.MS_CN=BANG_LUONG.MS_CN WHERE BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'  AND ISNULL(BL_CHINH,0) <>0 " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                prb.Value = 63;
//                prb.Refresh();
//                this.Refresh();
//                // tinh tien dieu chinh
//                Sql = "UPDATE BANG_LUONG SET TIEN_LUONG_DC =round(T,0) FROM BANG_LUONG INNER JOIN ( SELECT MS_CN , TIEN_DIEU_CHINH  AS T " + " FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "mm/dd/yyyy") + "') A ON " + " A.MS_CN=BANG_LUONG.MS_CN WHERE BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'  AND ISNULL(BL_CHINH,0) <>0  " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                prb.Value = 64;
//                prb.Refresh();
//                this.Refresh();
//                // TINH TIEN BS
//                Sql = " UPDATE BANG_LUONG SET BS_CDLDN =round(A.BS_CDLDN,0) FROM BANG_LUONG INNER JOIN " + "( SELECT MS_CN, BS_CDLDN FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "') A " + " ON A.MS_CN=BANG_LUONG.MS_CN WHERE   BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'  AND ISNULL(BL_CHINH,0) <>0  " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                Sql = " UPDATE BANG_LUONG SET BS_VIECRIENG =round(A.BS_VIECRIENG,0) FROM BANG_LUONG INNER JOIN " + "( SELECT MS_CN, BS_VIECRIENG FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "') A " + " ON A.MS_CN=BANG_LUONG.MS_CN WHERE   BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'  AND ISNULL(BL_CHINH,0) <>0  " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                Sql = " UPDATE BANG_LUONG SET BS_TIENPHEP =round(A.BS_TIENPHEP,0) FROM BANG_LUONG INNER JOIN " + "( SELECT MS_CN, BS_TIENPHEP FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "') A " + " ON A.MS_CN=BANG_LUONG.MS_CN WHERE   BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'  AND ISNULL(BL_CHINH,0) <>0  " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                Sql = " UPDATE BANG_LUONG SET BS_LETET =round(A.BS_LETET,0) FROM BANG_LUONG INNER JOIN " + "( SELECT MS_CN, BS_LETET FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "') A " + " ON A.MS_CN=BANG_LUONG.MS_CN WHERE   BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'  AND ISNULL(BL_CHINH,0) <>0  " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                Sql = " UPDATE BANG_LUONG SET BS_TANGCA =round(A.BS_TANGCA,0) FROM BANG_LUONG INNER JOIN " + "( SELECT MS_CN, BS_TANGCA FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "') A " + " ON A.MS_CN=BANG_LUONG.MS_CN WHERE   BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'  AND ISNULL(BL_CHINH,0) <>0  " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                Sql = " UPDATE BANG_LUONG SET BS_CHUYENCAN =round(A.BS_CHUYENCAN,0) FROM BANG_LUONG INNER JOIN " + "( SELECT MS_CN, BS_CHUYENCAN FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "') A " + " ON A.MS_CN=BANG_LUONG.MS_CN WHERE   BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'  AND ISNULL(BL_CHINH,0) <>0  " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                Sql = " UPDATE BANG_LUONG SET BS_LUONG =round(A.BS_LUONG,0) FROM BANG_LUONG INNER JOIN " + "( SELECT MS_CN, BS_LUONG FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "') A " + " ON A.MS_CN=BANG_LUONG.MS_CN WHERE   BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'  AND ISNULL(BL_CHINH,0) <>0  " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                Sql = " UPDATE BANG_LUONG SET BS_MAYMAU =round(A.BS_MAYMAU,0) FROM BANG_LUONG INNER JOIN " + "( SELECT MS_CN, BS_MAYMAU FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "') A " + " ON A.MS_CN=BANG_LUONG.MS_CN WHERE   BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'  AND ISNULL(BL_CHINH,0) <>0  " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                Sql = " UPDATE BANG_LUONG SET BS_UIMAU =round(A.BS_UIMAU,0) FROM BANG_LUONG INNER JOIN " + "( SELECT MS_CN, BS_UIMAU FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "') A " + " ON A.MS_CN=BANG_LUONG.MS_CN WHERE   BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'  AND ISNULL(BL_CHINH,0) <>0  " + DON_VI + MS_PB;
//                CN.Execute(Sql);

//                // truy thu
//                Sql = " UPDATE BANG_LUONG SET TT_CDLDN =round(A.TT_CDLDN,0) FROM BANG_LUONG INNER JOIN " + "( SELECT MS_CN, TT_CDLDN FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "') A " + " ON A.MS_CN=BANG_LUONG.MS_CN WHERE   BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'  AND ISNULL(BL_CHINH,0) <>0  " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                Sql = " UPDATE BANG_LUONG SET TT_VIECRIENG =round(A.TT_VIECRIENG,0) FROM BANG_LUONG INNER JOIN " + "( SELECT MS_CN, TT_VIECRIENG FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "') A " + " ON A.MS_CN=BANG_LUONG.MS_CN WHERE   BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'  AND ISNULL(BL_CHINH,0) <>0  " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                Sql = " UPDATE BANG_LUONG SET TT_TIENPHEP =round(A.TT_TIENPHEP,0) FROM BANG_LUONG INNER JOIN " + "( SELECT MS_CN, TT_TIENPHEP FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "') A " + " ON A.MS_CN=BANG_LUONG.MS_CN WHERE   BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'  AND ISNULL(BL_CHINH,0) <>0  " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                Sql = " UPDATE BANG_LUONG SET TT_LETET =round(A.TT_LETET,0) FROM BANG_LUONG INNER JOIN " + "( SELECT MS_CN, TT_LETET FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "') A " + " ON A.MS_CN=BANG_LUONG.MS_CN WHERE   BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'  AND ISNULL(BL_CHINH,0) <>0  " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                Sql = " UPDATE BANG_LUONG SET TT_TANGCA =round(A.TT_TANGCA,0) FROM BANG_LUONG INNER JOIN " + "( SELECT MS_CN, TT_TANGCA FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "') A " + " ON A.MS_CN=BANG_LUONG.MS_CN WHERE   BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'  AND ISNULL(BL_CHINH,0) <>0  " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                Sql = " UPDATE BANG_LUONG SET TT_MAYMAU =round(A.TT_MAYMAU,0) FROM BANG_LUONG INNER JOIN " + "( SELECT MS_CN, TT_MAYMAU FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "') A " + " ON A.MS_CN=BANG_LUONG.MS_CN WHERE   BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'  AND ISNULL(BL_CHINH,0) <>0  " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                Sql = " UPDATE BANG_LUONG SET TT_UIMAU =round(A.TT_UIMAU,0) FROM BANG_LUONG INNER JOIN " + "( SELECT MS_CN, TT_UIMAU FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "') A " + " ON A.MS_CN=BANG_LUONG.MS_CN WHERE   BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'  AND ISNULL(BL_CHINH,0) <>0  " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                // tinh thoi thu BHXH
//                Sql = "UPDATE BANG_LUONG SET THOI_THU_BHXH =round(T,0) FROM BANG_LUONG INNER JOIN ( SELECT MS_CN , THOI_THU_BHXH AS T " + "  FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "mm/dd/yyyy") + "') A ON " + " A.MS_CN=BANG_LUONG.MS_CN WHERE BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'  AND ISNULL(BL_CHINH,0) <>0 " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                prb.Value = 65;
//                prb.Refresh();
//                this.Refresh();
//                // THOI THU BHTN
//                Sql = "UPDATE BANG_LUONG SET THOI_THU_BHTN =round(T,0) FROM BANG_LUONG INNER JOIN ( SELECT MS_CN , THOI_THU_BHTN AS T " + "  FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "mm/dd/yyyy") + "') A ON " + " A.MS_CN=BANG_LUONG.MS_CN WHERE BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'  AND ISNULL(BL_CHINH,0) <>0 " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                prb.Value = 65;
//                prb.Refresh();
//                this.Refresh();
//                // THOI THU THUE TNCN
//                Sql = "UPDATE BANG_LUONG SET THOI_THU_TNCN =round(T,0) FROM BANG_LUONG INNER JOIN ( SELECT MS_CN , THOI_THU_TNCN AS T " + "  FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "mm/dd/yyyy") + "') A ON " + " A.MS_CN=BANG_LUONG.MS_CN WHERE BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'  AND ISNULL(BL_CHINH,0) <>0 " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                prb.Value = 65;
//                prb.Refresh();
//                this.Refresh();
//                // tinh thoi thu BHYT
//                Sql = "UPDATE BANG_LUONG SET THOI_THU_BHYT =round(T,0) FROM BANG_LUONG INNER JOIN ( SELECT MS_CN , THOI_THU_BHYT  AS T " + "  FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "mm/dd/yyyy") + "') A ON A.MS_CN=BANG_LUONG.MS_CN " + " WHERE BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "' AND ISNULL(BL_CHINH,0) <>0  " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                prb.Value = 66;
//                prb.Refresh();
//                this.Refresh();
//                // tinh tien khac
//                Sql = " UPDATE BANG_LUONG SET KHAC =round(T,0), CONG_KHAC_TM = TM FROM BANG_LUONG INNER JOIN ( SELECT MS_CN , CONG_KHAC AS T, ISNULL(CONG_KHAC_TM,0) AS TM " + " FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "mm/dd/yyyy") + "') A ON A.MS_CN=BANG_LUONG.MS_CN " + " WHERE BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "' AND ISNULL(BL_CHINH,0) <>0 " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                prb.Value = 67;
//                prb.Refresh();
//                this.Refresh();
//                // tinh tong thu nhap
//                Sql = " UPDATE BANG_LUONG SET TONG_THU_NHAP =isnull(tham_nien,0) + ISNULL(LUONG_TG,0)+ ISNULL(LUONG_CD_LDN,0) + ISNULL(TIEN_CHUYEN_CAN,0) + ISNULL(LUONG_VIEC_RIENG,0) + ISNULL(LUONG_NGAY_PHEP,0)" + " + ISNULL(LUONG_LE_TET,0) + ISNULL(LUONG_KHAC,0) + ISNULL(LUONG_TC_TG,0) + ISNULL(LUONG_SP,0) + ISNULL(LUONG_CHUYEN_MON,0) " + " + ISNULL(LUONG_TRACH_NHIEM,0) + ISNULL(LUONG_PB_KHAC,0) + ISNULL(TIEN_MAY_MAU,0) + ISNULL(TIEN_UI_MAU,0) + ISNULL(TIEN_CDPS,0) " + " + ISNULL(PHAN_BO_LUONG,0) + ISNULL(TIEN_LUONG_DC,0) + ISNULL(THOI_THU_BHXH,0) + ISNULL(THOI_THU_BHYT,0)+ ISNULL(THOI_THU_BHTN,0) + ISNULL(KHAC,0) + ISNULL(THOI_THU_TNCN,0)" + " + ISNULL(LUONG_TC_1621,0) + ISNULL(LUONG_TC_DEM,0) + ISNULL(LUONG_LAM_DEM,0) + ISNULL(LUONG_TC_CN,0) + ISNULL(LUONG_TC_NL,0)+ ISNULL(BU_LUONG_THAP,0) " + " + ISNULL(BS_CDLDN,0) + ISNULL(BS_VIECRIENG,0) + ISNULL(BS_TIENPHEP,0) + ISNULL(BS_LETET,0) + ISNULL(BS_TANGCA,0) + ISNULL(BS_CHUYENCAN,0) + ISNULL(BS_LUONG,0)+ ISNULL(BS_MAYMAU,0) + ISNULL(BS_UIMAU,0) " + " + ISNULL(LUONG_LAM_THEM_CDLDN,0) " + " WHERE BANG_lUONG.THANG='" + Format(ThangTL, "MM/DD/YYYY") + "'" + DON_VI + MS_PB;
//                CN.Execute(Sql);

//                // tinh thuong luong
//                Sql = "UPDATE BANG_LUONG SET THUONG_LUONG=round(TL,0) FROM BANG_LUONG INNER JOIN " + "( SELECT MS_CN, THUONG_LUONG AS TL FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "' ) A " + " ON A.MS_CN=BANG_LUONG.MS_CN WHERE BANG_LUONG.THANG='" + Format(ThangTL, "MM/DD/YYYY") + "' AND ISNULL(BL_CHINH,0) <>0  " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                // tinh bu luong thap
//                Sql = "UPDATE BANG_LUONG SET TIEN_BU_LUONG FROM BANG_LUONG CASE WHEN LEFT(TEN_CHUC_VU,2) IN ('TV') THEN T2.MBL_TV ELSE CASE WHEN LEFT(TEN_CHUC_VU,2) IN ('HV') THEN T2.MBL_HV ELSE T2.MBL_CT END END AS MUC_BU_LSELECT MSDV, MBL_CT, MBL_TV, MBL_HV FROM MUC_BU_LUONG_XN WHERE THANG = (SELECT MAX(THANG) FROM MUC_BU_LUONG_XN " + "WHERE THANG <='" + Format(ThangTL, "MM/DD/YYYY") + "')" + DON_VI + MS_PB;
//                // tinh  trich nop bhxh
//                Sql = " UPDATE BANG_LUONG SET TRICH_NOP_BHXH =round( ISNULL(LUONG_HDLD,0) * ISNULL(MUC_DONG_BHXH,0) /100 ,0) " + " WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "' AND ISNULL(BL_CHINH,0) <>0 " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                prb.Value = 68;
//                prb.Refresh();
//                this.Refresh();
//                // tinh trich nop bhyt
//                Sql = " UPDATE BANG_LUONG SET TRICH_NOP_BHYT = round(ISNULL(LUONG_HDLD,0) * ISNULL(MUC_DONG_BHYT,0) /100 ,0) " + " WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "' AND  ISNULL(BL_CHINH,0) <>0 " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                // tinh trich nop bhtn
//                Sql = " UPDATE BANG_LUONG SET BHTN = round(ISNULL(LUONG_HDLD,0) * ISNULL(MUC_DONG_BHTN,0) /100,0)  " + " WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "' AND  ISNULL(BL_CHINH,0) <>0 " + DON_VI + MS_PB;
//                CN.Execute(Sql);

//                // ko tinh bhxh, bhtn khi ngay cong < 15
//                // Sql = " UPDATE BANG_LUONG SET  TRICH_NOP_BHXH =0, TRICH_NOP_BHYT = round(ISNULL(LUONG_HDLD,0) * 4.5/100 ,0), " & _
//                // "BHTN=0 FROM BANG_LUONG T1 INNER JOIN CONG_NHAN T2 ON T1.MS_CN = T2.MS_CN " & _
//                // "INNER JOIN (SELECT MS_CN, SUM(ISNULL(NGAY_CONG,0)+ISNULL(PHEP_NAM,0)+ISNULL(LE_TET,0)) AS TC FROM BANG_LUONG WHERE THANG = '" & Format(ThangTL, "mm/dd/yyyy") & "' GROUP BY MS_CN) T3 " & _
//                // "ON T1.MS_CN = T3.MS_CN " & _
//                // "WHERE  (ISNULL(T3.TC,0) < ROUND(ISNULL(NC_LV_TT,0)/2,1)) " & _
//                // " AND T1.THANG='" & Format(ThangTL, "MM/DD/YYYY") & "' "
//                // If cmbDV.GetKeyValue <> "ALL" Then
//                // Sql = Sql & " AND  T1.MSDV='" & cmbDV.GetKeyValue & "'"
//                // End If
//                // If cmbPB.GetKeyValue <> "ALL" Then
//                // Sql = Sql & " AND T1.MS_PB='" & cmbPB.GetKeyValue & "'"
//                // End If
//                // 
//                // CN.Execute Sql

//                // Sql = " UPDATE BANG_LUONG SET TRICH_NOP_BHYT = 0 WHERE RIGHT(TEN,3) = 'NTS' AND THANG = '" & Format(ThangTL, "MM/DD/YYYY") & "'"
//                // CN.Execute Sql

//                // Sql = " UPDATE BANG_LUONG SET TRICH_NOP_BHXH =0, BHTN=0 " & _
//                // "FROM BANG_LUONG T1 INNER JOIN CONG_NHAN T2 ON T1.MS_CN = T2.MS_CN " & _
//                // "WHERE  (ISNULL(NGAY_CONG,0) + ISNULL(CONG_TG,0) + ISNULL(CONG_SP,0) + ISNULL(PHEP_NAM,0) + ISNULL(LE_TET,0) < ROUND(ISNULL(NC_LV_TT,0)/2,1)) " & _
//                // " AND T1.THANG='" & Format(ThangTL, "MM/DD/YYYY") & "' AND NOT T2.NGAY_NGHI_VIEC IS NULL"
//                // If cmbDV.GetKeyValue <> "ALL" Then
//                // Sql = Sql & " AND  T1.MSDV='" & cmbDV.GetKeyValue & "'"
//                // End If
//                // If cmbPB.GetKeyValue <> "ALL" Then
//                // Sql = Sql & " AND T1.MS_PB='" & cmbPB.GetKeyValue & "'"
//                // End If
//                // CN.Execute Sql

//                Sql = " UPDATE BANG_LUONG SET  TRICH_NOP_BHXH =0 " + "WHERE MS_CN NOT IN ( SELECT MS_CN FROM BHXH_CD WHERE THANG=( SELECT MAX(THANG) FROM BHXH_CD " + "WHERE THANG<='" + Format(ThangTL, "MM/DD/YYYY") + "') AND ISNULL(BHXH,0) <>0 ) AND THANG='" + Format(ThangTL, "MM/DD/YYYY") + "'" + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                Sql = " UPDATE BANG_LUONG SET  TRICH_NOP_BHYT =0 " + "WHERE MS_CN NOT IN ( SELECT MS_CN FROM BHXH_CD WHERE THANG=( SELECT MAX(THANG) FROM BHXH_CD " + "WHERE THANG<='" + Format(ThangTL, "MM/DD/YYYY") + "') AND ISNULL(BHYT,0) <>0 ) AND THANG='" + Format(ThangTL, "MM/DD/YYYY") + "'" + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                Sql = " UPDATE BANG_LUONG SET  BHTN =0 " + "WHERE MS_CN NOT IN ( SELECT MS_CN FROM BHXH_CD WHERE THANG=( SELECT MAX(THANG) FROM BHXH_CD " + "WHERE THANG<='" + Format(ThangTL, "MM/DD/YYYY") + "') AND ISNULL(BHTN,0) <>0 ) AND THANG='" + Format(ThangTL, "MM/DD/YYYY") + "'" + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                Sql = " UPDATE BANG_LUONG SET CD_PHI = CASE ISNULL(TIEN_CD,0) WHEN 0 THEN round(ISNULL(LUONG_HDLD,0) * ISNULL(MUC_DONG_CD,0) /100,0) ELSE TIEN_CD END " + " FROM BANG_LUONG INNER JOIN (SELECT MS_CN, TIEN_CD FROM BHXH_CD WHERE THANG=( SELECT MAX(THANG) FROM BHXH_CD WHERE THANG<='" + Format(ThangTL, "MM/DD/YYYY") + "') AND ISNULL(CONG_DOAN,0) <>0) T2 " + " ON BANG_LUONG.MS_CN = T2.MS_CN " + " WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "' AND  ISNULL(BL_CHINH,0) <>0 " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                // Sql = " UPDATE BANG_lUONG SET  CD_PHI =0 WHERE MS_CN NOT IN ( SELECT MS_CN FROM BHXH_CD WHERE THANG=( SELECT MAX(THANG) FROM BHXH_CD WHERE THANG<='" & Format(ThangTL, "MM/DD/YYYY") & "') AND ISNULL(CONG_DOAN,0) <>0 )" & _
//                // " AND THANG='" & Format(ThangTL, "MM/DD/YYYY") & "'"
//                // CN.Execute Sql
//                prb.Value = 69;
//                prb.Refresh();
//                this.Refresh();
//                // tinh truy thu bhxh
//                Sql = " UPDATE BANG_LUONG SET TRUY_THU_BHXH =round(T,0) FROM BANG_LUONG INNER JOIN ( SELECT MS_CN , TRUY_THU_BHXH  AS T " + " FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "mm/dd/yyyy") + "') A ON " + " A.MS_CN=BANG_LUONG.MS_CN WHERE BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "' AND ISNULL(BL_CHINH,0) <>0 " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                prb.Value = 70;
//                prb.Refresh();
//                this.Refresh();
//                // truy thu luong
//                Sql = " UPDATE BANG_LUONG SET TRUY_THU_LUONG =round(T,0) FROM BANG_LUONG INNER JOIN ( SELECT MS_CN , TRUY_THU_LUONG  AS T " + " FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "mm/dd/yyyy") + "') A ON " + " A.MS_CN=BANG_LUONG.MS_CN WHERE BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "' AND ISNULL(BL_CHINH,0) <>0 " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                prb.Value = 70;
//                prb.Refresh();
//                this.Refresh();
//                // TRUY THU TIEN CC
//                Sql = " UPDATE BANG_LUONG SET TRUY_THU_TIEN_CC =round(T,0) FROM BANG_LUONG INNER JOIN ( SELECT MS_CN , TRUY_THU_TIEN_CC  AS T " + " FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "mm/dd/yyyy") + "') A ON " + " A.MS_CN=BANG_LUONG.MS_CN WHERE BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "' AND ISNULL(BL_CHINH,0) <>0 " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                // TRUY THU TIEN TC
//                Sql = " UPDATE BANG_LUONG SET TRUY_THU_TIEN_TC =round(T,0) FROM BANG_LUONG INNER JOIN ( SELECT MS_CN , TRUY_THU_TIEN_TC  AS T " + " FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "mm/dd/yyyy") + "') A ON " + " A.MS_CN=BANG_LUONG.MS_CN WHERE BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "' AND ISNULL(BL_CHINH,0) <>0 " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                // TRUY THU THUE TNCN
//                Sql = " UPDATE BANG_LUONG SET  TRUY_THU_TNCN=round(T,0) FROM BANG_LUONG INNER JOIN ( SELECT MS_CN , TRUY_THU_TNCN  AS T " + " FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "mm/dd/yyyy") + "') A ON " + " A.MS_CN=BANG_LUONG.MS_CN WHERE BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "' AND ISNULL(BL_CHINH,0) <>0 " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                prb.Value = 70;
//                prb.Refresh();
//                this.Refresh();
//                // TRUY THU BHTN
//                Sql = " UPDATE BANG_LUONG SET  TRUY_THU_BHTN=round(T,0) FROM BANG_LUONG INNER JOIN ( SELECT MS_CN , TRUY_THU_BHTN  AS T " + " FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "mm/dd/yyyy") + "') A ON " + " A.MS_CN=BANG_LUONG.MS_CN WHERE BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "' AND ISNULL(BL_CHINH,0) <>0 " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                prb.Value = 70;
//                prb.Refresh();
//                this.Refresh();
//                // tinh truy thu BHYT
//                Sql = "UPDATE BANG_LUONG SET TRUY_THU_BHYT =round(T,0) FROM BANG_LUONG INNER JOIN ( SELECT MS_CN , TRUY_THU_BHYT  AS T " + "  FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "mm/dd/yyyy") + "') A ON " + "  A.MS_CN=BANG_LUONG.MS_CN WHERE BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "' AND ISNULL(BL_CHINH,0) <>0" + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                prb.Value = 71;
//                prb.Refresh();
//                this.Refresh();

//                // tinh tam ung
//                Sql = "UPDATE BANG_LUONG SET TAM_UNG =round(T,0) FROM BANG_LUONG INNER JOIN ( SELECT MS_CN, SUM(SO_TIEN) AS T " + "  FROM TAM_UNG WHERE THANG='" + Format(ThangTL, "mm/dd/yyyy") + "'  GROUP BY MS_CN ) A ON " + " A.MS_CN=BANG_LUONG.MS_CN  WHERE BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "' AND ISNULL(BL_CHINH,0) <>0 " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                prb.Value = 72;
//                prb.Refresh();
//                this.Refresh();
//                // tinh tru khac
//                Sql = " UPDATE BANG_LUONG SET TRU_KHAC =round(T,0) FROM BANG_LUONG INNER JOIN ( SELECT MS_CN , TRU_KHAC AS T " + "  FROM TIEN_CONG_TRU WHERE THANG='" + Format(ThangTL, "mm/dd/yyyy") + "') A ON " + " A.MS_CN=BANG_LUONG.MS_CN WHERE BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "' AND ISNULL(BL_CHINH,0) <>0 " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                prb.Value = 73;
//                prb.Refresh();
//                this.Refresh();

//                // Tinh thue thu nhap
//                TinhThueTN();

//                // Cap nhat du lieu nhung nguoi tinh luong Net
//                Sql = "UPDATE BANG_LUONG SET PHEP_NAM = 0, PHEP_THU_BAY = 0, LE_TET = 0, VIEC_RIENG_CLUONG = 0, " + "TC_1621 = 0, TC_CN = 0, CHUYEN_CAN = 0, TIEN_CHUYEN_CAN = 0, THAM_NIEN =0, DI_LAI = 0, CON_NHO = 0, " + "NGUYET_SAN = 0, TRICH_NOP_BHXH = 0, TRICH_NOP_BHYT = 0, BHTN = 0, THUE_THU_NHAP = 0, CD_PHI = 0 " + "WHERE BANG_LUONG.THANG='" + Format(ThangTL, "mm/dd/yyyy") + "' AND BANG_LUONG.CACH_TINH IN ('LN')" + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                Sql = " UPDATE BANG_LUONG SET TONG_KHAU_TRU =ISNULL(TRICH_NOP_BHXH,0) + ISNULL(TRICH_NOP_BHYT,0) + ISNULL(BHTN,0) + ISNULL(TRUY_THU_BHXH,0) + ISNULL(TRUY_THU_LUONG,0)" + "  + ISNULL(TRUY_THU_TIEN_CC,0) + ISNULL(TRUY_THU_TIEN_TC,0) +ISNULL(TRUY_THU_TNCN,0) + ISNULL(TRUY_THU_BHTN,0) + ISNULL(TRUY_THU_BHYT,0)+ isnull(TT_CDLDN,0) + isnull(TT_VIECRIENG,0) +isnull(TT_TIENPHEP,0) + isnull(TT_LETET,0)+ isnull(TT_MAYMAU,0) + isnull(TT_UIMAU,0)  " + "  + ISNULL(TAM_UNG,0) +ISNULL(TRU_KHAC,0) + ISNULL(THUE_THU_NHAP,0) WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "'" + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                // tinh ngay phep ton
//                Sql = "UPDATE BANG_LUONG SET NP_TT = ISNULL(T2.TT_THANG,0), LP_TT = CASE WHEN ISNULL(T2.TT_THANG,0) = 0 THEN 0 ELSE T1.LUONG_HDLD/26*T2.TT_THANG END " + "FROM BANG_LUONG T1 LEFT JOIN (SELECT MS_CN, TT_THANG " + "FROM PHEP_THANG WHERE THANG ='" + Format(ThangTL, "MM/DD/YYYY") + "') T2 ON T1.MS_CN = T2.MS_CN " + "WHERE T1.THANG ='" + Format(ThangTL, "MM/DD/YYYY") + "' AND T1.BL_CHINH = 1";
//                if (cmbDV.GetKeyValue != "ALL")
//                    Sql = Sql + " AND  MSDV='" + cmbDV.GetKeyValue + "'";
//                if (cmbPB.GetKeyValue != "ALL")
//                    Sql = Sql + " AND MS_PB='" + cmbPB.GetKeyValue + "'";
//                CN.Execute(Sql);

//                // tinh tien mat
//                Sql = " UPDATE BANG_LUONG SET TIEN_MAT=ROUND(ISNULL(TONG_THU_NHAP,0) - ISNULL(TONG_KHAU_TRU,0),0) " + "  -ISNULL(TAM_UNG,0) - ISNULL(TRU_KHAC,0) WHERE BANG_LUONG.THANG='" + Format(ThangTL, "MM/DD/YYYY") + "' AND ISNULL(MA_THE_ATM,'') ='' " + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                Sql = " UPDATE BANG_LUONG SET ATM= ROUND(ISNULL(TONG_THU_NHAP,0) - ISNULL(TONG_KHAU_TRU,0),0)" + " WHERE BANG_LUONG.THANG='" + Format(ThangTL, "MM/DD/YYYY") + "' AND ISNULL(MA_THE_ATM,'') <>''" + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                Sql = "UPDATE BANG_LUONG SET TIEN_MAT=ISNULL(ATM,0) WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "'" + " AND ISNULL(MA_THE_ATM,'') <>'' AND MS_CN IN ( SELECT MS_CN FROM CONG_NHAN WHERE NGAY_NGHI_VIEC <='" + Format(DenNgayTL, "MM/DD/YYYY") + "')" + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                Sql = "UPDATE BANG_LUONG SET ATM=0 WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "'" + " AND ISNULL(MA_THE_ATM,'') <>'' AND MS_CN IN ( SELECT MS_CN FROM CONG_NHAN WHERE NGAY_NGHI_VIEC <='" + Format(DenNgayTL, "MM/DD/YYYY") + "')" + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                Sql = "UPDATE BANG_LUONG SET STT_IN_CV =A.STT FROM BANG_LUONG INNER JOIN " + " (  SELECT ms_chuc_vu, isnull(stt_in,200) as stt   from chuc_vu ) a on a.ms_chuc_vu=bang_luong.ms_chuc_vu where bang_luong.thang='" + Format(ThangTL, "mm/dd/yyyy") + "'" + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                i = 1;
//                Sql = " SELECT MS_CN,STT_PB, STT_IN_CV, STT  FROM BANG_LUONG WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "' AND MSDV='" + cmbDV.GetKeyValue + "' " + " and ms_pb+msdv  not in ( select ms_to+msdv from [to] where isnull(khong_xuat,0) <>0) " + " ORDER BY STT_PB ASC , STT_IN_CV ASC, HO_TEN ASC ";
//                ;/* Cannot convert EmptyStatementSyntax, CONVERSION ERROR: Conversion for EmptyStatement not implemented, please report this issue in '' at character 66224
//   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.DefaultVisit(SyntaxNode node)
//   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitEmptyStatement(EmptyStatementSyntax node)
//   at Microsoft.CodeAnalysis.VisualBasic.Syntax.EmptyStatementSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
//   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
//   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
//   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

//Input: 
//       Set rs = New Recordset

// */
//                rs.Open(Sql, CN, 1, 2);
//                while (!rs.EOF)
//                {
//                    rs.Fields("STT").Value = i;
//                    i = i + 1;
//                    rs.MoveNext();
//                }
//                DROP_TABLE(THACHTAM);
//                Sql = " SELECT MS_CN, SUM(ISNULL(TIEN_MAT,0) + ISNULL(ATM,0)) AS SO_TIEN INTO " + THACHTAM + " FROM BANG_LUONG WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "' GROUP BY MS_CN";
//                CN.Execute(Sql);
//                Sql = " DELETE FROM " + THACHTAM + " WHERE ISNULL(SO_TIEN,0) >=" + System.Convert.ToString(txtSoTien.Text);
//                CN.Execute(Sql);
//                // Sql = " UPDATE BANG_LUONG SET MA_THE_ATM=NULL, TIEN_MAT=ISNULL(TIEN_MAT,0) + ISNULL(ATM,0) WHERE THANG='" & Format(ThangTL, "MM/DD/YYYY") & "' AND MS_CN IN ( SELECT MS_CN FROM " & THACHTAM & ")"
//                // CN.Execute Sql
//                Sql = " UPDATE BANG_LUONG SET ATM=0 WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "' AND MS_CN IN ( SELECT MS_CN FROM " + THACHTAM + ")";
//                CN.Execute(Sql);
//                Sql = "UPDATE BANG_LUONG SET TIEN_BU_LUONG ='" + System.Convert.ToString(txtMucBu.Text) + "',TIEN_BU_LUONG_KHAC='" + System.Convert.ToString(txtBuKhac.Text) + "',TIEN_COM_BU='" + System.Convert.ToString(txtComBu.Text) + "' WHERE THANG='" + Format(ThangTL, "MM/DD/YYYY") + "'" + DON_VI + MS_PB;
//                CN.Execute(Sql);

//                // ============================ TINH TIEN CAC COT BANG LUONG
//                Sql = "UPDATE BANG_LUONG SET TT_TIENPHEP = CASE ISNULL(PHEP_NAM,0)+ISNULL(PHEP_THU_BAY,0) WHEN 0 THEN 0 " + "ELSE ISNULL(LUONG_HDLD,0)/NGAY_CONG_CHUAN*(ISNULL(PHEP_NAM,0)+ISNULL(PHEP_THU_BAY,0)) END, " + "LUONG_TC_1621 = ROUND(CASE WHEN (ISNULL(TONG_GIO_CONG,0) + ISNULL(TC_1621,0) + ISNULL(TC_DEM,0) + ISNULL(TC_CN,0)) > 0 THEN " + "((ISNULL(LUONG_SP,0)+ISNULL(LUONG_PB_KHAC,0)+ISNULL(TIEN_CDPS,0))/(ISNULL(TONG_GIO_CONG,0) + ISNULL(TC_1621,0) + " + "ISNULL(TC_DEM,0) + ISNULL(TC_CN,0)))*0.5*ISNULL(TC_1621,0) ELSE 0 END ,0), TT_LETET = CASE ISNULL(LE_TET,0) WHEN 0 THEN 0 " + "ELSE ISNULL(LUONG_HDLD,0)/NGAY_CONG_CHUAN*ISNULL(LE_TET,0) END " + "FROM BANG_LUONG WHERE THANG = '" + Format(ThangTL, "mm/dd/yyyy") + "' AND CACH_TINH = 'SP'" + DON_VI + MS_PB;
//                CN.Execute(Sql);
//                // ============================Nhung ai co bu luong thi ko co chuyen can di lai
//                // Xi nghiep my tho
//                Sql = "UPDATE BANG_LUONG SET TIEN_CHUYEN_CAN = 0, DI_LAI = 0 " + "FROM BANG_LUONG T1 INNER JOIN " + "(SELECT MSDV, MBL_CT, MBL_TV, MBL_HV FROM MUC_BU_LUONG_XN WHERE THANG = (SELECT MAX(THANG) FROM MUC_BU_LUONG_XN " + "WHERE THANG <='" + Format(ThangTL, "mm/dd/yyyy") + "')) T2 ON T1.MSDV = T2.MSDV " + "INNER JOIN (SELECT MS_CN, SUM(ISNULL(TONG_GIO_CONG,0)+((ISNULL(PHEP_NAM,0)+ISNULL(PHEP_THU_BAY,0))*8)+(LE_TET*8)+(TC_1621*1.5)) AS TC " + "FROM BANG_LUONG WHERE THANG = '" + Format(ThangTL, "mm/dd/yyyy") + "' GROUP BY MS_CN) T3 " + "ON T1.MS_CN = T3.MS_CN " + "WHERE THANG = '" + Format(ThangTL, "mm/dd/yyyy") + "' AND CACH_TINH = 'SP' " + "AND (CASE WHEN LEFT(TEN_CHUC_VU,2) IN ('TV') THEN T2.MBL_TV ELSE CASE WHEN LEFT(TEN_CHUC_VU,2) IN ('HV') " + "THEN T2.MBL_HV ELSE T2.MBL_CT END END/(NGAY_CONG_CHUAN*8))*(ISNULL(T1.TONG_GIO_CONG,0)+((ISNULL(PHEP_NAM,0)+ISNULL(PHEP_THU_BAY,0))*8)+(LE_TET*8)+(TC_1621*1.5)) - " + "((ISNULL(LUONG_SP,0)+ISNULL(LUONG_PB_KHAC,0)+ISNULL(TIEN_CDPS,0))+ ISNULL(TT_TIENPHEP,0) + ISNULL(LUONG_TC_1621,0) " + "+ ISNULL(TT_LETET,0)) > 0";
//                if (cmbDV.GetKeyValue != "ALL")
//                    Sql = Sql + " AND  T1.MSDV='" + cmbDV.GetKeyValue + "'";
//                if (cmbPB.GetKeyValue != "ALL")
//                    Sql = Sql + " AND T1.MS_PB='" + cmbPB.GetKeyValue + "'";
//                CN.Execute(Sql);


//                // Xi nghiep tra vinh
//                // Sql = "UPDATE BANG_LUONG SET TIEN_CHUYEN_CAN = 0, DI_LAI = 0 " & _
//                // "FROM BANG_LUONG T1 INNER JOIN " & _
//                // "(SELECT MSDV, MBL_CT, MBL_TV, MBL_HV FROM MUC_BU_LUONG_XN WHERE THANG = (SELECT MAX(THANG) FROM MUC_BU_LUONG_XN " & _
//                // "WHERE THANG <='" & Format(ThangTL, "mm/dd/yyyy") & "')) T2 ON T1.MSDV = T2.MSDV " & _
//                // "WHERE THANG = '" & Format(ThangTL, "mm/dd/yyyy") & "' AND CACH_TINH = 'SP' AND T1.MSDV = '02' " & _
//                // "AND (CASE WHEN LEFT(TEN_CHUC_VU,2) IN ('TV') THEN T2.MBL_TV ELSE CASE WHEN LEFT(TEN_CHUC_VU,2) IN ('HV') " & _
//                // "THEN T2.MBL_HV ELSE T2.MBL_CT END END/(NGAY_CONG_CHUAN))*isnull(T1.NGAY_CONG,0) - " & _
//                // "(ISNULL(LUONG_SP,0)+ISNULL(LUONG_PB_KHAC,0)+ISNULL(TIEN_CDPS,0)) > 0"
//                // CN.Execute Sql

//                // CAP NHAP LAI CAC CONG NHAN CO TREN 1 CHUC VU VA PHONG BAN
//                // Sql = "EXEC spTinhLuong '" & Format(ThangTL, "MM/DD/YYYY") & "', '" & Format(DenNgayTL, "MM/DD/YYYY") & "' "
//                // CN.Execute Sql
//                2:
//        ;

//                // XOA DU LIEU NHUNG NGUOI KO co ngay cong, luong san pham, phep, le
//                Sql = "DELETE FROM BANG_LUONG WHERE THANG = '" + Format(ThangTL, "mm/dd/yyyy") + "' AND (ISNULL(NGAY_CONG,0) + ISNULL(TONG_CONG_LSP,0) + ISNULL(LUONG_LE_TET,0) + ISNULL(LUONG_NGAY_PHEP,0)) <= 0";
//                CN.Execute(Sql);
//                RsBLuong.Requery();
//                prb.Visible = false;
//                MsgBoxXP("TÈnh lõïng ho¿n tÞt !", Constants.vbExclamation + Constants.vbOKOnly, this.Caption, null/* Conversion error: Set to default value for this argument */, MyScheme, null/* Conversion error: Set to default value for this argument */, null/* Conversion error: Set to default value for this argument */, true, null/* Conversion error: Set to default value for this argument */, null/* Conversion error: Set to default value for this argument */, mdiMain.OsenXPHookMenu1.Font, null/* Conversion error: Set to default value for this argument */, null/* Conversion error: Set to default value for this argument */, mdiMain.OsenXPHookMenu1.Font);
//            }
//            return;
//            err_loi:
//            ;
//            prb.Visible = false;
//            MsgBoxXP("TÈnh lõïng khéng th¿nh céng !", Constants.vbExclamation + Constants.vbOKOnly, this.Caption, null/* Conversion error: Set to default value for this argument */, MyScheme, null/* Conversion error: Set to default value for this argument */, null/* Conversion error: Set to default value for this argument */, true, null/* Conversion error: Set to default value for this argument */, null/* Conversion error: Set to default value for this argument */, mdiMain.OsenXPHookMenu1.Font, null/* Conversion error: Set to default value for this argument */, null/* Conversion error: Set to default value for this argument */, mdiMain.OsenXPHookMenu1.Font);
//        }

//    }
//}
