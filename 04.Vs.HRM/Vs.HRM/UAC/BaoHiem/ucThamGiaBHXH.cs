using DevExpress.Utils;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using Vs.Report;

namespace Vs.HRM
{
    public partial class ucThamGiaBHXH : DevExpress.XtraEditors.XtraUserControl
    {
        public static ucThamGiaBHXH _instance;
        public static ucThamGiaBHXH Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucThamGiaBHXH();
                return _instance;
            }
            
        }
        public ucThamGiaBHXH()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, windowsUIButton);
        }
        private void ucThamGiaBHXH_Load(object sender, EventArgs e)
        {

            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDV);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGrdCongNhan();
            LoadThamGiaBHXH();
            grvCongNhan_FocusedRowChanged(null, null);
            radTinHTrang_SelectedIndexChanged(null, null);
            Commons.Modules.sPS = "";
        }

        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGrdCongNhan();
            Commons.Modules.sPS = "";
        }
        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGrdCongNhan();
            Commons.Modules.sPS = "";
        }
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadGrdCongNhan();
            Commons.Modules.sPS = "";
        }

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "In":
                    {
                        InDuLieu();
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
            }
        }

        private void InDuLieu()
        {
            frmViewReport frm = new frmViewReport();
            frm.rpt = new rptThamGiaBHXH(Convert.ToInt64(grvCongNhan.GetFocusedRowCellValue("ID_CN")));
            DataTable dt = new DataTable();
            dt = Commons.Modules.ObjSystems.ConvertDatatable(grvThamGiaBHXH);
            if (dt == null || dt.Rows.Count == 0) return;
            dt.TableName = "DATA";
            frm.AddDataSource(dt);

            frm.ShowDialog();
        }

        #region hàm xử lý dữ liệu
        private void LoadGrdCongNhan()
        {
            try
            {
                Commons.Modules.sPS = "0Load";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCongNhanBHXH", cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdCongNhan, grvCongNhan, dt, false, false, true, true, true, this.Name);
                grvCongNhan.Columns["ID_CN"].Visible = false;
                grvCongNhan.Columns["TINH_TRANG"].Visible = false;
                Commons.Modules.sPS = "";
            }
            catch (Exception)
            {
            }
        }
        private int SoNam(string tu, string den, out int SoThang)
        {
            DateTime TuThang = Convert.ToDateTime("01/" + tu);
            DateTime DenThang = Convert.ToDateTime("01/" + den);

            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spTinhNamThang", TuThang, DenThang));
            int n = Convert.ToInt32(dt.Rows[0]["SN"]);
            SoThang = Convert.ToInt32(dt.Rows[0]["ST"]);
            return n;
        }
        private void LoadThamGiaBHXH()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetThamGiaBHXH", grvCongNhan.GetFocusedRowCellValue("ID_CN")));
                // dòng đầu thì
                dt.Columns["DEN_THANG"].ReadOnly = false;
                dt.Columns["TU_THANG"].ReadOnly = false;
                dt.Columns["NAM_DONG"].ReadOnly = false;
                dt.Columns["THANG_DONG"].ReadOnly = false;
                int resulst;
                for (int i = 0; i < dt.Rows.Count - 1; i++)
                {
                    if (i > 0)
                    {
                        dt.Rows[i]["TU_THANG"] = dt.Rows[i]["DEN_THANG"];
                    }
                    dt.Rows[i]["DEN_THANG"] = Convert.ToDateTime("01/" + dt.Rows[i + 1]["DEN_THANG"]).AddMonths(-1).ToString("MM/yyyy");
                    dt.Rows[i]["NAM_DONG"] = SoNam(dt.Rows[i]["TU_THANG"].ToString(), dt.Rows[i]["DEN_THANG"].ToString(), out resulst);
                    dt.Rows[i]["THANG_DONG"] = resulst;
                }
                if (dt.Rows.Count > 1)
                {
                    dt.Rows[dt.Rows.Count - 1]["TU_THANG"] = dt.Rows[dt.Rows.Count - 1]["DEN_THANG"].ToString() == "" ? dt.Rows[dt.Rows.Count - 1]["TU_THANG"] : dt.Rows[dt.Rows.Count - 1]["DEN_THANG"];
                }
                    dt.Rows[dt.Rows.Count - 1]["DEN_THANG"] = DateTime.Today.AddMonths(-1).ToString("MM/yyyy");
                    dt.Rows[dt.Rows.Count - 1]["NAM_DONG"] = SoNam(dt.Rows[dt.Rows.Count - 1]["TU_THANG"].ToString(), dt.Rows[dt.Rows.Count - 1]["DEN_THANG"].ToString(), out resulst);
                    dt.Rows[dt.Rows.Count - 1]["THANG_DONG"] = resulst;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThamGiaBHXH, grvThamGiaBHXH, dt, false, false, true, true, true, this.Name);
                grvThamGiaBHXH.Columns["ID_CN"].Visible = false;
                grvThamGiaBHXH.Columns["NGAY_HIEU_LUC"].Visible = false;
                grvThamGiaBHXH.Columns["TEN_CV"].Visible = false;
                grvThamGiaBHXH.Columns["MUC_LUONG_DONG"].DisplayFormat.FormatType = FormatType.Numeric;
                grvThamGiaBHXH.Columns["MUC_LUONG_DONG"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
            }
            catch 
            {
                grdThamGiaBHXH.DataSource = null;
            }

        }
        #endregion

        private void radTinHTrang_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtTmp = new DataTable();
            string sdkien = "( 1 = 1 )";
            try
            {
                dtTmp = (DataTable)grdCongNhan.DataSource;

                if (radTinHTrang.SelectedIndex == 1) sdkien = "(TINH_TRANG = 1)";
                if (radTinHTrang.SelectedIndex == 2) sdkien = "(TINH_TRANG = 0)";
                dtTmp.DefaultView.RowFilter = sdkien;
            }
            catch
            {
                try
                {
                    dtTmp.DefaultView.RowFilter = "";
                }
                catch { }
            }
        }

        private void grvCongNhan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            //DataTable dtTmp = new DataTable();
            //String sIDCN;
            try
            {
                LoadThamGiaBHXH();
                //dtTmp = (DataTable)grdThamGiaBHXH.DataSource;

                //string sDK = "";
                //sIDCN = "-1";
                //try { sIDCN = grvCongNhan.GetFocusedRowCellValue("ID_CN").ToString(); } catch { }
                //if (sIDCN != "-1") sDK = " ID_CN = '" + sIDCN + "' ";

                //dtTmp.DefaultView.RowFilter = sDK;
            }
            catch { }
        }


    }
}
