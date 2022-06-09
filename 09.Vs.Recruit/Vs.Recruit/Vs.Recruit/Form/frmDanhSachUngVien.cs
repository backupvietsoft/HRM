using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
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

namespace Vs.Recruit
{
    public partial class frmDanhSachUngVien : DevExpress.XtraEditors.XtraForm
    {
        public DataTable dt_CHON;
        public Int64 iID_TB; // -1 xem danh sach ung vien tren ucLyLich, else xem danh sasch ung vien tren ucPhongVan dựa vào cboID_TB
        public Int64 iID_UV;
        public frmDanhSachUngVien(Int64 idtb)
        {
            InitializeComponent();
            iID_TB = idtb;
        }

        #region even
        private void frmDanhSachUngVien_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            if (iID_TB == -1)
            {
                grvUngVien.OptionsSelection.MultiSelect = false;
                grvUngVien.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                btnALL.Buttons[0].Properties.Visible = false;
                LoadCboTTTuyenDung();
            }
            else
            {
                cboTTTuyenDung.EditValue = 0;
                ItemForDA_TUYEN_DUNG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            
            LoadData();
            Commons.Modules.sLoad = "";
            LoadNN();
        }

        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "ghi":
                        {
                            dt_CHON = new DataTable();
                            DataTable dt_temp = ((DataTable)grdUngVien.DataSource);
                            try
                            {
                                if (dt_temp.AsEnumerable().Where(r => r.Field<Boolean>("CHON") == true).Count() > 0)
                                {
                                    dt_CHON = dt_temp.AsEnumerable().Where(r => r.Field<Boolean>("CHON") == true).CopyToDataTable().Copy();
                                }
                                else
                                {
                                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChuaChonUV"));
                                    return;
                                }
                            }
                            catch
                            {
                                //Trong truong hop ma no where khong ra thi no se bi catch, nen cho nay minh dung Clone()
                                dt_CHON = dt_temp.Clone();
                            }
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                            break;
                        }
                    case "thoat":
                        {
                            this.Close();
                            break;
                        }
                    default:
                        break;
                }
            }
            catch { }
        }
        #endregion

        #region function
        private void LoadNN()
        {
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, btnALL);
            Commons.Modules.ObjSystems.MLoadNNXtraGrid(grvUngVien, this.Name);
        }

        private void LoadCboTTTuyenDung()
        {
            try
            {
                //cboTuyenDung
                DataTable dt_td = new DataTable();
                dt_td.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDaTuyenDung", Commons.Modules.TypeLanguage));
                Commons.Modules.ObjSystems.MLoadLookUpEditN(cboTTTuyenDung, dt_td, "ID_TTTD", "TT_TUYEN_DUNG", "TT_TUYEN_DUNG", "");
                cboTTTuyenDung.EditValue = 0;
            }
            catch { }
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = new DataTable();
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spPhongVan", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_TB", SqlDbType.BigInt).Value = iID_TB;
                cmd.Parameters.Add("@DA_TUYEN_DUNG", SqlDbType.Int).Value = Convert.ToInt32(cboTTTuyenDung.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0].Copy();

                try
                {
                    var rows = dt.AsEnumerable().Where(x => !dt_CHON.AsEnumerable().Any(x1 => x["ID_UV"].ToString().Equals(x1["ID_UV"].ToString())));
                    if (rows.Any())
                    {
                        dt = rows.CopyToDataTable();
                    }
                    else
                        dt.Clear();


                    //dt = dt.AsEnumerable().Where(x => !dt_CHON.AsEnumerable().Any(x1 => x["ID_UV"].ToString().Equals(x1["ID_UV"].ToString()))).CopyToDataTable();
                }
                catch { }

                if (grdUngVien.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdUngVien, grvUngVien, dt, false, true, false, false, false, this.Name);
                    grvUngVien.Columns["ID_UV"].Visible = false;
                    grvUngVien.Columns["ID_PVUV"].Visible = false;
                    grvUngVien.Columns["ID_PV"].Visible = false;
                    grvUngVien.Columns["CHON"].Visible = false;
                    grvUngVien.Columns["NOI_DUNG1"].Visible = false;
                    grvUngVien.Columns["DIEM1"].Visible = false;
                    grvUngVien.Columns["NOI_DUNG2"].Visible = false;
                    grvUngVien.Columns["DIEM2"].Visible = false;
                    grvUngVien.Columns["NOI_DUNG3"].Visible = false;
                    grvUngVien.Columns["DIEM3"].Visible = false;
                    grvUngVien.Columns["NOI_DUNG4"].Visible = false;
                    grvUngVien.Columns["DIEM4"].Visible = false;
                    grvUngVien.Columns["NOI_DUNG5"].Visible = false;
                    grvUngVien.Columns["DIEM5"].Visible = false;
                    grvUngVien.Columns["DIEM_TONG_KET"].Visible = false;
                    grvUngVien.Columns["DAT"].Visible = false;
                }
                else
                {
                    grdUngVien.DataSource = dt;
                }
                grvUngVien.OptionsSelection.CheckBoxSelectorField = "CHON";
                grvUngVien.Columns["CHON"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            }
            catch
            {

            }
        }

        #endregion

        private void grvUngVien_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (iID_TB != -1)
                {
                    return;
                }
                iID_UV = Convert.ToInt64(grvUngVien.GetFocusedRowCellValue("ID_UV"));
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch
            { }
        }

        private void grvUngVien_MouseWheel(object sender, MouseEventArgs e)
        {
            //DevExpress.XtraGrid.Views.Grid.GridView view = (sender as DevExpress.XtraGrid.Views.Grid.GridView);
            //view.LeftCoord += e.Delta;
            //(e as DevExpress.Utils.DXMouseEventArgs).Handled = true;
        }

        private void cboTTTuyenDung_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData();
            Commons.Modules.sLoad = "";
        }
    }
}
