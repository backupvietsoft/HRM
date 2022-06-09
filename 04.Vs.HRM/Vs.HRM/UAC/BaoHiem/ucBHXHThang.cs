using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Vs.HRM
{
    public partial class ucBHXHThang : DevExpress.XtraEditors.XtraUserControl
    {
        private static bool isAdd = false;
        DataTable dtThang = null;
        int iIDCN = 0;
        public ucBHXHThang()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, windowsUIButton);
        }
    
        private void ucBHXHThang_Load(object sender, EventArgs e)
        {
            Commons.Modules.sPS = "0Load";
            DateTime t = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            formatText();
          
            LoadNullSum();
            LoadThang(t);
            LoadGrdBHXHThang();
            LoadGrdBHXHThangDieuChinh();

            RepositoryItemLookUpEdit cboLDC = new RepositoryItemLookUpEdit();
            cboLDC.NullText = "";
            cboLDC.ValueMember = "ID_LOAI_DIEU_CHINH";
            cboLDC.DisplayMember = "TEN_LOAI_DIEU_CHINH";
            cboLDC.DataSource = Commons.Modules.ObjSystems.DataLoaiDieuChinh(false);
            cboLDC.Columns.Clear();
            cboLDC.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_LOAI_DIEU_CHINH", "Loại điều chỉnh"));
            cboLDC.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            cboLDC.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            grvDieuChinh.Columns["ID_LOAI_DIEU_CHINH"].ColumnEdit = cboLDC;

            LoadThangTienBHXHThang();
            Commons.Modules.sPS = "";
            enableButon(true);
        }


        private void formatText()
        {
            Commons.OSystems.SetDateEditFormat(dtTuNgay);
            Commons.OSystems.SetDateEditFormat(dtDenNgay);

            txtTONG_LDau.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            txtTONG_LDTang.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            txtTONG_LDGiam.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            txtTONG_LDCuoi.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";

            txtQL_Dau.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            txtQL_Tang.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            txtQL_Giam.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            txtQL_Cuoi.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            txtTongCong.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";

            txtBHXHTang.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            txtBHXHGiam.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            txtBHXHDCTang.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            txtBHXHDCGiam.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            txtBHXH_SPN.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";

            txtBHYTTang.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            txtBHYTGiam.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            txtBHYTDCTang.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            txtBHYTDCGiam.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            txtBHYT_SPN.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";

            txtBHTNTang.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            txtBHTNGiam.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            txtBHTNDCTang.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            txtBHTNDCGiam.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            txtBHTN_SPN.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";

            txtTONG_LDau.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            txtTONG_LDTang.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            txtTONG_LDGiam.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            txtTONG_LDCuoi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

            txtQL_Dau.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            txtQL_Tang.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            txtQL_Giam.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            txtQL_Cuoi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            txtTongCong.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

            txtBHXHTang.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            txtBHXHGiam.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            txtBHXHDCTang.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            txtBHXHDCGiam.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            txtBHXH_SPN.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

            txtBHYTTang.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            txtBHYTGiam.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            txtBHYTDCTang.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            txtBHYTDCGiam.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            txtBHYT_SPN.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

            txtBHTNTang.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            txtBHTNGiam.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            txtBHTNDCTang.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            txtBHTNDCGiam.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            txtBHTN_SPN.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;


            txtBHXHTang.ReadOnly = true;
            txtBHYTTang.ReadOnly = true;
            txtBHTNTang.ReadOnly = true;

            txtBHXHGiam.ReadOnly = true;
            txtBHYTGiam.ReadOnly = true;
            txtBHTNGiam.ReadOnly = true;

            txtBHXHDCTang.ReadOnly = true;
            txtBHYTDCTang.ReadOnly = true;
            txtBHTNDCTang.ReadOnly = true;

            txtBHXHDCGiam.ReadOnly = true;
            txtBHYTDCGiam.ReadOnly = true;
            txtBHTNDCGiam.ReadOnly = true;

            txtQL_Dau.ReadOnly = true;
            txtQL_Cuoi.ReadOnly = true;
            txtTONG_LDau.ReadOnly = true;
            txtTONG_LDCuoi.ReadOnly = true;

            txtBHXH_SPN.ReadOnly = true;
            txtBHYT_SPN.ReadOnly = true;
            txtBHTN_SPN.ReadOnly = true;
            txtTongCong.ReadOnly = true;
        }

        private void LoadNullSum()
        {
            try
            {
                txtTONG_LDau.EditValue = 0;
                txtTONG_LDTang.EditValue = 0;
                txtTONG_LDGiam.EditValue = 0;
                txtTONG_LDCuoi.EditValue = 0;

                txtQL_Dau.EditValue = 0;
                txtQL_Tang.EditValue = 0;
                txtQL_Giam.EditValue = 0;
                txtQL_Cuoi.EditValue = 0;
                txtTongCong.EditValue = 0;

                txtBHXHTang.EditValue = 0;
                txtBHXHGiam.EditValue = 0;
                txtBHXHDCTang.EditValue = 0;
                txtBHXHDCGiam.EditValue = 0;
                txtBHXH_SPN.EditValue = 0;

                txtBHYTTang.EditValue = 0;
                txtBHYTGiam.EditValue = 0;

                txtBHYTDCTang.EditValue = 0;
                txtBHYTDCGiam.EditValue = 0;
                txtBHYT_SPN.EditValue = 0;

                txtBHTNTang.EditValue = 0;
                txtBHTNGiam.EditValue = 0;
                txtBHTNDCTang.EditValue = 0;
                txtBHTNDCGiam.EditValue = 0;
                txtBHTN_SPN.EditValue = 0;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void LoadTuNgayDenNgay(DateTime dt)
        {
            try
            {
                DateTime tuNgay = new DateTime();
                if (dt.Month == 1)
                {
                    tuNgay = new DateTime(dt.Year - 1, 12, 16);
                }
                else
                {
                    tuNgay = new DateTime(dt.Year, dt.Month - 1, 16);
                }
                DateTime denNgay = new DateTime(dt.Year, dt.Month, 15);

                dtTuNgay.EditValue = tuNgay;
                dtDenNgay.EditValue = denNgay;
            }
            catch 
            {

            }

        }

        public void LoadDot(DateTime thang)
        {
            try
            {
                DataRow[] dr;
                // tất cả các dòng cùng tháng
                dr = dtThang.Select(" THANG " + "='" + thang + "'  ", "THANG", DataViewRowState.CurrentRows);
                // chọn lại  cá đợt duy nhất của tháng
                Commons.Modules.ObjSystems.MLoadComboboxEdit(cbDot, dr, "DOT");
                cbDot.SelectedIndex = 0;
                if (dr.Count() >= 1)
                {
                    cboThang.Text = Convert.ToDateTime(dr[0]["THANG"].ToString()).ToString("MM/yyyy");
                    cbDot.EditValue = dr[0]["Dot"].ToString();
                    dtTuNgay.EditValue = dr[0]["TU_NGAY"];
                    dtDenNgay.EditValue = dr[0]["DEN_NGAY"];
                }
                else
                {
                    cbDot.EditValue = 1;
                    LoadTuNgayDenNgay(thang);
                }
            }
            catch (Exception ex)
            {
                LoadNull();
            }
            
        }

        private void LoadThang(DateTime thang)
        {
            try
            {
                DataTable dtthang = new DataTable();
                dtThang = new DataTable();

                //dtThang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetThangBHXHThang", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                string sSql = "SELECT disTINCT SUBSTRING(CONVERT(VARCHAR(10),THANG,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),THANG,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM dbo.BHXH_THANG ORDER BY Y DESC , M DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, false, true, true, true, this.Name);
                grvThang.Columns["M"].Visible = false;
                grvThang.Columns["Y"].Visible = false;
                cboThang.Text = grvThang.GetFocusedRowCellValue("THANG").ToString();
                LoadDot(Convert.ToDateTime(cboThang.EditValue));
            }
            catch (Exception ex)
            {
                LoadNull();
            }
        }

        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            LoadTuNgayDenNgay(Convert.ToDateTime(cboThang.EditValue));
            if (Commons.Modules.sPS == "0Load") return;

            LoadDot(Convert.ToDateTime(cboThang.EditValue));
            LoadTuNgayDenNgay(Convert.ToDateTime(cboThang.EditValue));
            DateTime thang = Convert.ToDateTime("01/01/1900");
            try
            {
                thang = Convert.ToDateTime(cboThang.Text.ToString());
                thang = new DateTime(thang.Year, thang.Month, 1);
            }
            catch
            {

            }
            LoadGrdBHXHThang();
            LoadGrdBHXHThangDieuChinh();
            LoadThangTienBHXHThang();
           Commons.Modules.sPS = "";
        }

        private void cboDot_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            DateTime thang = Convert.ToDateTime("01/01/1900");
            try
            {
                thang = Convert.ToDateTime(cboThang.Text.ToString());
                thang = new DateTime(thang.Year, thang.Month, 1);
            }
            catch
            {

            }
            LoadGrdBHXHThang();
            LoadGrdBHXHThangDieuChinh();
            LoadThangTienBHXHThang();
            Commons.Modules.sPS = "";
        }

        private void calThang_DateTimeCommit(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            try
            {
                cboThang.Text = calThang.DateTime.ToString("MM/yyyy");
                DateTime thang = Convert.ToDateTime("01/01/1900");
                try
                {
                    thang = Convert.ToDateTime(cboThang.Text.ToString());
                    thang = new DateTime(thang.Year, thang.Month, 1);
                }
                catch { }
                LoadThangTienBHXHThang();

            }
            catch (Exception ex)
            {
                cboThang.Text = calThang.DateTime.ToString("MM/yyyy");
            }
            cboThang.ClosePopup();

            Commons.Modules.sPS = "";
        }

        //==========Tung sua 23/09/2021

        private void LoadGrdBHXHThang()
        {
            DataTable dt = new DataTable();
            try
            {

                if (isAdd)
                {
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetEditBHXH", Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd"),
                            cbDot.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, dtTuNgay.EditValue, dtDenNgay.EditValue));
                }
                else
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListBHXHThang", Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd"), cbDot.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));

                Commons.Modules.ObjSystems.MLoadXtraGrid(grdTrongThang, grvTrongThang, dt, false, false, true, false, true, this.Name);
                    grvTrongThang.Columns["ID_CN"].Visible = false;
                    grvTrongThang.Columns["DOT"].Visible = false;
                    grvTrongThang.Columns["THANG"].Visible = false;
                    grvTrongThang.Columns["ID_LDC"].Visible = false;
                    grvTrongThang.Columns["HS_LUONG_CU"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    grvTrongThang.Columns["HS_LUONG_CU"].DisplayFormat.FormatString = Commons.Modules.sSoLeSL;
                    grvTrongThang.Columns["HS_LUONG"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    grvTrongThang.Columns["HS_LUONG"].DisplayFormat.FormatString = Commons.Modules.sSoLeSL;
                    grvTrongThang.Columns["PHU_CAP_CU"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    grvTrongThang.Columns["PHU_CAP_CU"].DisplayFormat.FormatString = Commons.Modules.sSoLeSL;
                    grvTrongThang.Columns["PHU_CAP"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    grvTrongThang.Columns["PHU_CAP"].DisplayFormat.FormatString = Commons.Modules.sSoLeSL;
            }
            catch (Exception ex) { }
        }

        private void LoadThangTienBHXHThang()
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr
                    , "spGetTIENBHXHThang"
                    , Commons.Modules.UserName
                    , Commons.Modules.TypeLanguage
                    , Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd") 
                   , int.Parse(cbDot.EditValue.ToString()),"","",0
                    ));


                if (dt == null || dt.Rows.Count <= 0)
                    LoadNullSum();
                else
                {

                    txtTONG_LDau.EditValue = dt.Rows[0]["TONG_SO_LAO_DONG"];
                    txtTONG_LDTang.EditValue = dt.Rows[0]["LD_TANG"];
                    txtTONG_LDGiam.EditValue = dt.Rows[0]["LD_GIAM"];
                    try
                    {
                        txtTONG_LDCuoi.EditValue = int.Parse(txtTONG_LDau.EditValue.ToString()) + int.Parse(txtTONG_LDTang.EditValue.ToString()) - int.Parse(txtTONG_LDGiam.EditValue.ToString());
                    }
                    catch
                    {
                        txtTONG_LDCuoi.EditValue = null;
                    }

                    txtQL_Dau.EditValue = dt.Rows[0]["TONG_QL_DK"];
                    txtQL_Tang.EditValue = dt.Rows[0]["TONG_QL_TANG"];
                    txtQL_Giam.EditValue = dt.Rows[0]["TONG_QL_GIAM"];
                    txtQL_Cuoi.EditValue = dt.Rows[0]["TONG_QL_CK"];
                    txtTongCong.EditValue = dt.Rows[0]["TONG_NOP"];

                    txtBHXHTang.EditValue = dt.Rows[0]["TIEN_BHXH_T"];
                    txtBHXHGiam.EditValue = dt.Rows[0]["TIEN_BHXH_G"];
                    txtBHXHDCTang.EditValue = dt.Rows[0]["DC_TANG_BHXH"];
                    txtBHXHDCGiam.EditValue = dt.Rows[0]["DC_GIAM_BHXH"];
                    txtBHXH_SPN.EditValue = dt.Rows[0]["SO_PHAI_NOP_BHXH"];

                    txtBHYTTang.EditValue = dt.Rows[0]["TIEN_BHYT_T"];
                    txtBHYTGiam.EditValue = dt.Rows[0]["TIEN_BHYT_G"];
                    txtBHYTDCTang.EditValue = dt.Rows[0]["DC_TANG_BHYT"];
                    txtBHYTDCGiam.EditValue = dt.Rows[0]["DC_GIAM_BHYT"];
                    txtBHYT_SPN.EditValue = dt.Rows[0]["SO_PHAI_NOP_BHYT"];

                    txtBHTNTang.EditValue = dt.Rows[0]["TIEN_BHTN_T"];
                    txtBHTNGiam.EditValue = dt.Rows[0]["TIEN_BHTN_G"];
                    txtBHTNDCTang.EditValue = dt.Rows[0]["DC_TANG_BHTN"];
                    txtBHTNDCGiam.EditValue = dt.Rows[0]["DC_GIAM_BHTN"];
                    txtBHTN_SPN.EditValue = dt.Rows[0]["SO_PHAI_NOP_BHTN"];


                }

            }
            catch
            {
                cbDot.Text = "1";
                LoadNullSum();
            }
        }
        
        //private void LoadGrdNewBHXHThang()
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        DateTime dThang = Convert.ToDateTime("01/01/1900");
        //        try
        //        {
        //            dThang = Convert.ToDateTime(cboThang.Text.ToString());
        //            dThang = new DateTime(dThang.Year, dThang.Month, 1);
        //        }
        //        catch
        //        {

        //        }
        //        dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spNewBHXHThang", dThang.ToString("yyyy-MM-dd"), (cbDot.EditValue == null || cbDot.EditValue.ToString() == "") ? -1 : cbDot.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
        //        Commons.Modules.ObjSystems.MLoadXtraGrid(grdTrongThang, grvTrongThang, dt, false, false, false, true, true, this.Name);
        //    }
        //    catch (Exception ex) { }
        //}

        private void LoadGrdBHXHThangDieuChinh()
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetDieuChinhBHXH", Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd"), cbDot.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdDieuChinh, grvDieuChinh, dt, false, true, true, true, true, this.Name);
                grvDieuChinh.Columns["DOT"].Visible = false;
                grvDieuChinh.Columns["THANG"].Visible = false;
                grvDieuChinh.Columns["TU_THANG"].Visible = false;
                grvDieuChinh.Columns["DEN_THANG"].Visible = false;
                grvDieuChinh.Columns["LY_DO_TRICH_NOP"].Visible = false;
                grvDieuChinh.Columns["HS_LUONG_CU"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                grvDieuChinh.Columns["HS_LUONG_CU"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
                grvDieuChinh.Columns["HS_LUONG_MOI"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                grvDieuChinh.Columns["HS_LUONG_MOI"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
                grvDieuChinh.Columns["HS_PHU_CAP_CU"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                grvDieuChinh.Columns["HS_PHU_CAP_CU"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
                grvDieuChinh.Columns["HS_PHU_CAP_MOI"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                grvDieuChinh.Columns["HS_PHU_CAP_MOI"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
                grvDieuChinh.Columns["PHAN_TRAM_TRICH_NOP"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                grvDieuChinh.Columns["PHAN_TRAM_TRICH_NOP"].DisplayFormat.FormatString = Commons.Modules.sSoLeSL;

                Commons.Modules.ObjSystems.AddCombXtra("ID_CN", "MS_CN", grvDieuChinh, Commons.Modules.ObjSystems.DataCongNhan(false), "ID_CN", "CONG_NHAN");
                //Commons.Modules.ObjSystems.AddCombXtra("MS_CN", "MS_CN", grvDieuChinh, "spGetCongNhan");
                Commons.Modules.ObjSystems.AddCombXtra("ID_LOAI_DIEU_CHINH", "TEN_LOAI_DIEU_CHINH", grvDieuChinh, Commons.Modules.ObjSystems.DataLoaiDieuChinh(false));
            }
            catch (Exception ex) { }
        }

        private void AddnewRow(GridView view, bool add)
        {
            view.OptionsBehavior.Editable = add;
            if (add == true)
            {
                view.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
                view.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            }
            else
            {
                view.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            }
        }
        
        private void LoadNull()
        {
            try
            {
                if (cboThang.Text == "") cboThang.Text = DateTime.Now.ToString("MM/yyyy");
                cbDot.EditValue = 1;
                //dtTuNgay.EditValue = null;
                //dtDenNgay.EditValue = null;
            }
            catch (Exception ex)
            {
                cboThang.Text = "";
                cbDot.EditValue = null;
                dtTuNgay.EditValue = null;
                dtDenNgay.EditValue = null;
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        
       

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "themsua":
                    {
                        if (string.IsNullOrEmpty(cboThang.Text) || cbDot.EditValue == null || cbDot.EditValue.ToString() == "-1")
                        {
                            Commons.Modules.ObjSystems.msgChung("msgThangkhongduocdetrong");
                            return;
                        }
                        isAdd = true;
                        LoadGrdBHXHThang();
                                                
                        Commons.Modules.ObjSystems.AddnewRow(grvDieuChinh, true);
                        enableButon(false);
                        break;
                    }
                case "xoa":
                    {
                        if (grvDieuChinh.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
                        if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
                        //xóa bhxh thang
                        try
                        {
                            string sSql = "DELETE dbo.BHXH_THANG WHERE THANG = '" + Convert.ToDateTime(cboThang.EditValue).ToString("yyyyMMdd") + "' AND DOT = " + cbDot.EditValue;
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                            sSql = "DELETE dbo.DIEU_CHINH_BHXH WHERE THANG = '" + Convert.ToDateTime(cboThang.EditValue).ToString("yyyyMMdd") + "' AND DOT = " + cbDot.EditValue;
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                            sSql = "DELETE dbo.TIEN_BHXH_THANG WHERE THANG = '" + Convert.ToDateTime(cboThang.EditValue).ToString("yyyyMMdd") + "' AND DOT = " + cbDot.EditValue;
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                        }
                        catch
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
                        }
                        LoadGrdBHXHThang();
                        LoadGrdBHXHThangDieuChinh();
                        LoadThangTienBHXHThang();
                        break;
                    }
                case "In":
                    {
                       
                        frmInBHXH InHopDongCN = new frmInBHXH(Convert.ToDateTime(cboThang.EditValue), Convert.ToInt32(cbDot.EditValue));
                        InHopDongCN.ShowDialog();
                        break;
                    }
                case "luu":
                    {
                        isAdd = false;
                        Validate();
                        if (grvTrongThang.HasColumnErrors) return;
                        if (grvTrongThang.HasColumnErrors) return;
                        ThaoTac(1);
                        enableButon(true);
                        LoadThang(Convert.ToDateTime(cboThang.EditValue));
                        LoadGrdBHXHThang();
                        LoadGrdBHXHThangDieuChinh();
                        LoadThangTienBHXHThang();
                        break;
                    }
                case "tinhtong":
                    {
                        Validate();
                        if (grvTrongThang.HasColumnErrors) return;
                        if (grvTrongThang.HasColumnErrors) return;
                        ThaoTac(2);
                        enableButon(false);
                        break;
                    }

                case "khongluu":
                    {
                        isAdd = false;
                        Commons.Modules.ObjSystems.DeleteAddRow(grvDieuChinh);
                        LoadGrdBHXHThang();
                        LoadGrdBHXHThangDieuChinh();
                        LoadThangTienBHXHThang();

                        enableButon(true);
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
                default:
                    break;
            }
        }


        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = !visible;
            windowsUIButton.Buttons[6].Properties.Visible = !visible;
            windowsUIButton.Buttons[7].Properties.Visible = !visible;
            windowsUIButton.Buttons[8].Properties.Visible = visible;
            cboThang.ReadOnly = !visible;
            cbDot.ReadOnly = !visible;
            AddnewRow(grvDieuChinh, !visible);

        }
     
        private void grvThang_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboThang.Text = grvThang.GetFocusedRowCellValue("THANG").ToString();
            }
            catch { }
            cboThang.ClosePopup();
        }

        private void grvDieuChinh_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            string sSql = "";
            GridView view = sender as GridView;
            if (view == null) return;
            DateTime dtThang = Convert.ToDateTime("01/01/1900");
            int dot = 1;
            try
            {
                dtThang = Convert.ToDateTime("01/" + cboThang.Text.ToString());
                dot = int.Parse(cbDot.Text);
            }
            catch
            {
            }
            if (e.Column.Name == "colID_CN")
            {
                if (view.GetRowCellValue(e.RowHandle, view.Columns["ID_CN"]).ToString() == "")
                {
                    return;
                }
                iIDCN = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, view.Columns["ID_CN"]));
                sSql = "SELECT  HO +' '+ TEN HO_TEN FROM dbo.CONG_NHAN WHERE ID_CN = " + iIDCN;
                string s = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql).ToString();
                view.SetRowCellValue(e.RowHandle, view.Columns["HO_TEN"], s);
                view.SetRowCellValue(e.RowHandle, view.Columns["THANG"], dtThang);
                view.SetRowCellValue(e.RowHandle, view.Columns["DOT"], dot);
            }
            
            if (e.Column.Name == "colID_LOAI_DIEU_CHINH")
            {
                var va1 = "";
                object va2 = null;
                if (view.GetRowCellValue(e.RowHandle, view.Columns["ID_LOAI_DIEU_CHINH"]) != DBNull.Value)
                {
                    sSql = "SELECT TEN_LOAI_DIEU_CHINH, PHAN_TRAM_DONG FROM dbo.LOAI_DIEU_CHINH  WHERE ID_LOAI_DIEU_CHINH = " + view.GetRowCellValue(e.RowHandle, view.Columns["ID_LOAI_DIEU_CHINH"]) + "";
                    DataTable dtTmp = new DataTable();
                    dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                    va1 = dtTmp.Rows[0]["TEN_LOAI_DIEU_CHINH"].ToString();
                    va2 = dtTmp.Rows[0]["PHAN_TRAM_DONG"];

                }
                view.SetRowCellValue(e.RowHandle, view.Columns["LY_DO_TRICH_NOP"], va1);
                view.SetRowCellValue(e.RowHandle, view.Columns["PHAN_TRAM_TRICH_NOP"], va2);
                view.SetRowCellValue(e.RowHandle, view.Columns["THANG"], dtThang);
                view.SetRowCellValue(e.RowHandle, view.Columns["DOT"], dot);
                return;
            }
            return;
        }

      
        private bool ThaoTac(int Save = 1)
        {
            try
            {
                string sBT1 = "BHXH_THANG" + Commons.Modules.UserName;
                string sBT2 = "DIEU_CHINH_BHXH" + Commons.Modules.UserName;
                DateTime thang = Convert.ToDateTime("01/01/1900");
                try
                {
                    thang = Convert.ToDateTime("01/" + cboThang.Text.ToString());
                }
                catch { }
                if (string.IsNullOrEmpty(dtTuNgay.Text) || dtTuNgay.EditValue == null || dtTuNgay.EditValue.ToString() == "")
                {

                    Commons.Modules.ObjSystems.msgChung("msgTuNgayKhongDeTrong");
                    dtTuNgay.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(dtDenNgay.Text) || dtDenNgay.EditValue == null || dtDenNgay.EditValue.ToString() == "")
                {
                    Commons.Modules.ObjSystems.msgChung("msgDenNgayKhongDeTrong");
                    dtDenNgay.Focus();
                    return false;
                }
                DataTable tb1 = Commons.Modules.ObjSystems.ConvertDatatable(grvTrongThang);
                if (tb1 != null && tb1.Rows.Count > 0)
                {
                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT1, tb1, "");
                }
                else
                {
                    sBT1 = "";
                }
                DataTable tb2 = Commons.Modules.ObjSystems.ConvertDatatable(grvDieuChinh);
                if (tb2 != null && tb2.Rows.Count > 0)
                {
                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT2, tb2, "");
                }
                else
                {
                    sBT2 = "";
                }
                
                // lưu = 1 Tính tổng =2
                if (Save == 1)
                {
                    SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateTienBHXHThang",
                        thang, int.Parse(cbDot.Text), dtTuNgay.DateTime, dtDenNgay.DateTime, txtTONG_LDau.EditValue, txtTONG_LDTang.EditValue, txtTONG_LDGiam.EditValue
                        , txtQL_Dau.EditValue, txtQL_Tang.EditValue, txtQL_Giam.EditValue, txtQL_Cuoi.EditValue, txtTongCong.EditValue, txtBHXHTang.EditValue, txtBHYTTang.EditValue
                        , txtBHTNTang.EditValue, txtBHXHGiam.EditValue, txtBHYTGiam.EditValue, txtBHTNGiam.EditValue, txtBHXHDCTang.EditValue, txtBHYTDCTang.EditValue, txtBHTNDCTang.EditValue
                        , txtBHXHDCGiam.EditValue, txtBHYTDCGiam.EditValue, txtBHTNDCGiam.EditValue, txtBHXH_SPN.EditValue, txtBHYT_SPN.EditValue, txtBHTN_SPN.EditValue, sBT1, sBT2
                        , Save.ToString()
                        );
                }
                if (Save == 2)
                {
                    DataTable tbTienBHXH = new DataTable();
                    tbTienBHXH.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetTIENBHXHThang", Commons.Modules.UserName, Commons.Modules.TypeLanguage, Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd"), cbDot.EditValue, sBT1, sBT2, 1));

                    txtTONG_LDau.EditValue = tbTienBHXH.Rows[0]["LDDK"];
                    txtTONG_LDTang.EditValue = tbTienBHXH.Rows[0]["LDT"];
                    txtTONG_LDGiam.EditValue = tbTienBHXH.Rows[0]["LDG"];
                    txtTONG_LDCuoi.EditValue = tbTienBHXH.Rows[0]["LDCK"];

                    txtQL_Dau.EditValue = tbTienBHXH.Rows[0]["QLDK"];
                    txtQL_Tang.EditValue = tbTienBHXH.Rows[0]["QLT"];
                    txtQL_Giam.EditValue = tbTienBHXH.Rows[0]["QLG"];
                    txtQL_Cuoi.EditValue = tbTienBHXH.Rows[0]["QLCK"];

                    txtBHXHTang.EditValue = tbTienBHXH.Rows[0]["TANGBHXH"];
                    txtBHYTTang.EditValue = tbTienBHXH.Rows[0]["TANGBHYT"];
                    txtBHTNTang.EditValue = tbTienBHXH.Rows[0]["TANGBHTN"];

                    txtBHXHGiam.EditValue = tbTienBHXH.Rows[0]["GIAMBHXH"];
                    txtBHYTGiam.EditValue = tbTienBHXH.Rows[0]["GIAMBHYT"];
                    txtBHTNGiam.EditValue = tbTienBHXH.Rows[0]["GIAMBHTN"];

                    txtBHXHDCTang.EditValue = tbTienBHXH.Rows[0]["DCTANGBHXH"];
                    txtBHYTDCTang.EditValue = tbTienBHXH.Rows[0]["DCTANGBHYT"];
                    txtBHTNDCTang.EditValue = tbTienBHXH.Rows[0]["DCTANGBHTN"];

                    txtBHXHDCGiam.EditValue = tbTienBHXH.Rows[0]["DCGIAMBHXH"];
                    txtBHYTDCGiam.EditValue = tbTienBHXH.Rows[0]["DCGIAMBHYT"];
                    txtBHTNDCGiam.EditValue = tbTienBHXH.Rows[0]["DCGIAMBHTN"];

                    txtBHXH_SPN.EditValue = Convert.ToInt64(txtBHXHTang.EditValue) + Convert.ToInt64(txtBHXHDCTang.EditValue) - Convert.ToInt64(txtBHXHGiam.EditValue) - Convert.ToInt64(txtBHXHDCGiam.EditValue);
                    txtBHYT_SPN.EditValue = Convert.ToInt64(txtBHYTTang.EditValue) + Convert.ToInt64(txtBHYTDCTang.EditValue) - Convert.ToInt64(txtBHYTGiam.EditValue) - Convert.ToInt64(txtBHYTDCGiam.EditValue);
                    txtBHTN_SPN.EditValue = Convert.ToInt64(txtBHTNTang.EditValue) + Convert.ToInt64(txtBHTNDCTang.EditValue) - Convert.ToInt64(txtBHTNGiam.EditValue) - Convert.ToInt64(txtBHTNDCGiam.EditValue); ;


                    txtTongCong.EditValue = Convert.ToInt64(txtBHXH_SPN.EditValue) + Convert.ToInt64(txtBHYT_SPN.EditValue) + Convert.ToInt64(txtBHTN_SPN.EditValue);
                }
                Commons.Modules.ObjSystems.XoaTable(sBT1);
                Commons.Modules.ObjSystems.XoaTable(sBT2);
                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                return false;
            }
        }

        private void txtTONG_LDTang_EditValueChanged(object sender, EventArgs e)
        {
            txtTONG_LDCuoi.EditValue = Convert.ToInt64(txtTONG_LDau.EditValue) + Convert.ToInt64(txtTONG_LDTang.EditValue) - Convert.ToInt64(txtTONG_LDGiam.EditValue);
        }

        private void txtTONG_LDGiam_EditValueChanged(object sender, EventArgs e)
        {
            txtTONG_LDCuoi.EditValue = Convert.ToInt64(txtTONG_LDau.EditValue) + Convert.ToInt64(txtTONG_LDTang.EditValue) - Convert.ToInt64(txtTONG_LDGiam.EditValue);
        }

        private void txtQL_Tang_EditValueChanged(object sender, EventArgs e)
        {
            txtBHXHTang.EditValue = Convert.ToInt64(txtQL_Tang.EditValue) * 26 / 100;
            txtBHYTTang.EditValue = Convert.ToInt64(txtQL_Tang.EditValue) * 4.5 / 100;
            txtBHTNTang.EditValue = Convert.ToInt64(txtQL_Tang.EditValue) * 2 / 100;

            txtBHXH_SPN.EditValue = Convert.ToInt64(txtBHXHTang.EditValue) + Convert.ToInt64(txtBHXHDCTang.EditValue) - Convert.ToInt64(txtBHXHGiam.EditValue) - Convert.ToInt64(txtBHXHDCGiam.EditValue);
            txtBHYT_SPN.EditValue = Convert.ToInt64(txtBHYTTang.EditValue) + Convert.ToInt64(txtBHYTDCTang.EditValue) - Convert.ToInt64(txtBHYTGiam.EditValue) - Convert.ToInt64(txtBHYTDCGiam.EditValue);
            txtBHTN_SPN.EditValue = Convert.ToInt64(txtBHTNTang.EditValue) + Convert.ToInt64(txtBHTNDCTang.EditValue) - Convert.ToInt64(txtBHTNGiam.EditValue) - Convert.ToInt64(txtBHTNDCGiam.EditValue); ;

            txtQL_Cuoi.EditValue = Convert.ToInt64(txtQL_Dau.EditValue) + Convert.ToInt64(txtQL_Tang.EditValue) - Convert.ToInt64(txtQL_Giam.EditValue);
        }

        private void txtQL_Giam_EditValueChanged(object sender, EventArgs e)
        {
            txtBHXHGiam.EditValue = Convert.ToInt64(txtQL_Giam.EditValue) * 26 / 100;
            txtBHYTGiam.EditValue = Convert.ToInt64(txtQL_Giam.EditValue) * 4.5 / 100;
            txtBHTNGiam.EditValue = Convert.ToInt64(txtQL_Giam.EditValue) * 2 / 100;

            txtBHXH_SPN.EditValue = Convert.ToInt64(txtBHXHTang.EditValue) + Convert.ToInt64(txtBHXHDCTang.EditValue) - Convert.ToInt64(txtBHXHGiam.EditValue) - Convert.ToInt64(txtBHXHDCGiam.EditValue);
            txtBHYT_SPN.EditValue = Convert.ToInt64(txtBHYTTang.EditValue) + Convert.ToInt64(txtBHYTDCTang.EditValue) - Convert.ToInt64(txtBHYTGiam.EditValue) - Convert.ToInt64(txtBHYTDCGiam.EditValue);
            txtBHTN_SPN.EditValue = Convert.ToInt64(txtBHTNTang.EditValue) + Convert.ToInt64(txtBHTNDCTang.EditValue) - Convert.ToInt64(txtBHTNGiam.EditValue) - Convert.ToInt64(txtBHTNDCGiam.EditValue); ;

            txtQL_Cuoi.EditValue = Convert.ToInt64(txtQL_Dau.EditValue) + Convert.ToInt64(txtQL_Tang.EditValue) - Convert.ToInt64(txtQL_Giam.EditValue);
        }

        private void grdDieuChinh_ProcessGridKey(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                XoaDieuChinhBHXH();
            }
        }

        private void XoaDieuChinhBHXH()
        {
            //xóa
            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDeleteDieuChinhBHXH"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE FROM dbo.DIEU_CHINH_BHXH WHERE THANG = '" + Convert.ToDateTime(grvDieuChinh.GetFocusedRowCellValue("THANG")).ToString("yyyy-MM-dd") + "' AND DOT = " + grvDieuChinh.GetFocusedRowCellValue("DOT") + " AND ID_CN = " + grvDieuChinh.GetFocusedRowCellValue("ID_CN") + "");
                grvDieuChinh.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
    }
}