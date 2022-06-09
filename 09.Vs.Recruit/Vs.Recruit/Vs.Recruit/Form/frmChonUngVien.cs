using DevExpress.Utils.Menu;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vs.Report;

namespace Vs.Recruit
{
    public partial class frmChonUngVien : DevExpress.XtraEditors.XtraForm
    {
        Int64 iID_TB = -1;
        Int64 iID_UV = -1;
        private DataTable dt_CHON;
        private ucCTQLUV ucUV;

        string strChuyenMon = "";
        string strTrinhDo = "";
        string strKNLV = "";
        string strBangCap = "";

        public AccordionControl accorMenuleft;
        public frmChonUngVien()
        {
            InitializeComponent();
        }

        #region even
        private void frmChonUngVien_Load(object sender, EventArgs e)
        {
            //Vi Tri Tuyen Dung
            DataTable dt_VTTD = new DataTable();
            dt_VTTD.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboViTriTuyenDung",Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_VTTD, dt_VTTD, "ID_VTTD", "TEN_VTTD", "TEN_VTTD");

            //Nguon tuyen dung
            DataTable dt_NTD = new DataTable();
            dt_NTD.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNguonTuyenDung", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_NTD, dt_NTD, "ID_NTD", "TEN_NTD", "TEN_NTD");

            // Trinh do
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_TD, Commons.Modules.ObjSystems.DataTDVH(Convert.ToInt32(-1), true), "ID_TDVH", "TEN_TDVH", "TEN_TDVH");

            // Kinh nghiem lam viec
            DataTable dt_knlv = new DataTable();
            dt_knlv.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboID_KNLV", Commons.Modules.UserName, Commons.Modules.TypeLanguage, true));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_KNLV, dt_knlv, "ID_KNLV", "TEN_KNLV", "TEN_KNLV");

            LoadData();
        }
        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    #region in
                    //case "in":
                    //    {
                    //        try
                    //        {
                    //            dt_CHON = new DataTable();
                    //            DataTable dt_temp = ((DataTable)grdChonUV.DataSource);
                    //            DataTable dt1 = new DataTable();
                    //            try
                    //            {
                    //                if (dt_temp.AsEnumerable().Where(r => r.Field<Boolean>("CHON") == true).Count() > 0)
                    //                {
                    //                    dt_CHON = dt_temp.AsEnumerable().Where(r => r.Field<Boolean>("CHON") == true).CopyToDataTable().Copy();
                    //                    string strSQL = "SELECT SO_TB, TIEU_DE FROM dbo.THONG_BAO_TUYEN_DUNG WHERE ID_TB = " + iID_TB + "";

                    //                    frmViewReport frm = new frmViewReport();
                    //                    frm.rpt = new rptDSUngVien();
                    //                    dt_CHON.TableName = "DA_TA";
                    //                    frm.AddDataSource(dt_CHON);

                    //                    DataTable dt = new DataTable();
                    //                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, strSQL));
                    //                    dt.TableName = "DA_TA1";
                    //                    frm.AddDataSource(dt);
                    //                    frm.ShowDialog();
                    //                }
                    //                else
                    //                {
                    //                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChuaChonUV"));
                    //                    return;
                    //                }
                    //            }
                    //            catch
                    //            {
                    //                //Trong truong hop ma no where khong ra thi no se bi catch, nen cho nay minh dung Clone()
                    //                dt_CHON = dt_temp.Clone();
                    //            }
                    //        }
                    //        catch
                    //        { }
                    //        break;
                    //    }
                    #endregion
                    case "ghi":
                        {
                            break;
                        }
                    case "khongghi":
                        {
                            this.Close();
                            break;
                        }
                }
            }
            catch { }
        }
        #endregion

        #region function
        private void LoadData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListUngVienChon",Commons.Modules.UserName, Commons.Modules.TypeLanguage, string.IsNullOrEmpty(cboID_VTTD.Text) ? -1 : Convert.ToInt64(cboID_VTTD.EditValue), string.IsNullOrEmpty(cboID_NTD.Text) ? -1 : Convert.ToInt64(cboID_NTD.EditValue), string.IsNullOrEmpty(cboID_TD.Text) ? -1 : Convert.ToInt64(cboID_TD.EditValue), string.IsNullOrEmpty(cboID_KNLV.Text) ? -1 : Convert.ToInt64(cboID_KNLV.EditValue)));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdChonUV, grvChonUV, dt, false, true, true, false, true, this.Name);
                grvChonUV.Columns["ID_VTTD_1"].Visible = false;
                grvChonUV.Columns["ID_VTTD_2"].Visible = false;
                grvChonUV.Columns["ID_TDVH"].Visible = false;
                grvChonUV.Columns["ID_KNLV"].Visible = false;
                grvChonUV.Columns["ID_NTD"].Visible = false;
            }
            catch { }
        }
        #endregion

        private void grvChonUV_DoubleClick(object sender, EventArgs e)
        {
            if (grvChonUV.RowCount == 0)
            {
                return;
            }
            this.WindowState = FormWindowState.Maximized;
            ucUV = new ucCTQLUV(Convert.ToInt64(grvChonUV.GetFocusedRowCellValue("ID_UV")));
            Commons.Modules.ObjSystems.ShowWaitForm(this);
            ucUV.Refresh();
            //ns.accorMenuleft = accorMenuleft;
            tablePanel1.Hide();
            this.Controls.Add(ucUV);
            ucUV.Dock = DockStyle.Fill;
            ucUV.backWindowsUIButtonPanel.ButtonClick += BackWindowsUIButtonPanel_ButtonClick;
            //accorMenuleft.Visible = false;
            Commons.Modules.ObjSystems.HideWaitForm();
        }

        public void BackWindowsUIButtonPanel_ButtonClick(object sender, ButtonEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            ucUV.Hide();
            tablePanel1.Show();
            LoadData();

            //DataTable dtmp = new DataTable();
            //dtmp = (DataTable)grdChonUV.DataSource;
            //if (dtmp.Rows.Count == 0) return;
            //string chuoiIDUV_tmp = "";
            //for (int i = 0; i < dtmp.Rows.Count; i++)
            //{
            //    chuoiIDUV_tmp += dtmp.Rows[i]["ID_UV"].ToString() + ",";
            //}
            //string chuoiIDUV = chuoiIDUV_tmp.Remove(chuoiIDUV_tmp.Length - 1);

            //LoadData(true, chuoiIDUV, iIDPV);
            //accorMenuleft.Visible = true;

        }
    }
}
