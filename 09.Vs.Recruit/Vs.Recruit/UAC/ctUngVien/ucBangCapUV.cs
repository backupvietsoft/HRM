using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraLayout;
using System.Drawing;
using Vs.Report;

namespace Vs.Recruit
{
    public partial class ucBangCapUV : DevExpress.XtraEditors.XtraUserControl
    {
        static Int64 iduv = 0;
        Int64 id_BC = -1;
        bool cothem = false;
        public ucBangCapUV(Int64 id)
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root, layoutControlGroup1 }, windowsUIButton);
            iduv = id;
        }

        #region function form Load
        private void LoadgrdBangCap(int id)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListUngVienBangCap", iduv, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_BC"] };
            if (grdBangCapUV.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdBangCapUV, grvBangCapUV, dt, false, true, true, true, true, this.Name);
                grvBangCapUV.Columns["TEN_BANG"].Visible = false;
                grvBangCapUV.Columns["XEP_LOAI"].Visible = false;
                grvBangCapUV.Columns["NGUOI_KY"].Visible = false;
                grvBangCapUV.Columns["NOI_CAP"].Visible = false;
                grvBangCapUV.Columns["GHI_CHU"].Visible = false;
                grvBangCapUV.Columns["NGAY_KY"].Visible = false;
                grvBangCapUV.Columns["ID_BC"].Visible = false;
                grvBangCapUV.Columns["ID_LOAI_TD"].Visible = false;
            }
            else
            {
                grdBangCapUV.DataSource = dt;
            }
            if (id != -1)
            {
                int index = dt.Rows.IndexOf(dt.Rows.Find(id));
                grvBangCapUV.FocusedRowHandle = grvBangCapUV.GetRowHandle(index);
            }
        }

        #endregion

        #region function dung chung
        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = visible;
            windowsUIButton.Buttons[5].Properties.Visible = visible;
            windowsUIButton.Buttons[6].Properties.Visible = !visible;
            windowsUIButton.Buttons[7].Properties.Visible = !visible;
            windowsUIButton.Buttons[8].Properties.Visible = visible;

            grdBangCapUV.Enabled = visible;

            SO_HIEU_BANGTextEdit.Properties.ReadOnly = visible;
            TEN_BANGTextEdit.Properties.ReadOnly = visible;
            NOI_HOCTextEdit.Properties.ReadOnly = visible;
            NGAY_BDDateEdit.Enabled = !visible;
            NGAY_KTDateEdit.Enabled = !visible;
            NGANH_DAO_TAOTextEdit.Properties.ReadOnly = visible;
            HT_DAO_TAOTextEdit.Properties.ReadOnly = visible;
            XEP_LOAITextEdit.Properties.ReadOnly = visible;
            TRINH_DOLookUpEdit.Properties.ReadOnly = visible;
            NGAY_KYDateEdit.Enabled = !visible;
            NGUOI_KYTextEdit.Properties.ReadOnly = visible;
            NOI_CAPTextEdit.Properties.ReadOnly = visible;
            GHI_CHUMemoEdit.Properties.ReadOnly = visible;
        }
        private void Bindingdata(bool bthem)
        {
            if (bthem == true)
            {
                SO_HIEU_BANGTextEdit.EditValue = "";
                TEN_BANGTextEdit.EditValue = "";
                NOI_HOCTextEdit.EditValue = "";
                NGAY_BDDateEdit.EditValue = DateTime.Today;
                NGAY_KTDateEdit.EditValue = DateTime.Today;
                NGANH_DAO_TAOTextEdit.EditValue = "";
                HT_DAO_TAOTextEdit.EditValue = "";
                XEP_LOAITextEdit.EditValue = "";
                TRINH_DOLookUpEdit.EditValue = 1;
                NGAY_KYDateEdit.EditValue = DateTime.Today;
                NGUOI_KYTextEdit.EditValue = "";
                NOI_CAPTextEdit.EditValue = "";
                GHI_CHUMemoEdit.EditValue = "";
            }
            else
            {
                SO_HIEU_BANGTextEdit.EditValue = grvBangCapUV.GetFocusedRowCellValue("SO_HIEU_BANG");
                TEN_BANGTextEdit.EditValue = grvBangCapUV.GetFocusedRowCellValue("TEN_BANG");
                NOI_HOCTextEdit.EditValue = grvBangCapUV.GetFocusedRowCellValue("NOI_HOC");
                NGAY_BDDateEdit.EditValue = Convert.ToDateTime(grvBangCapUV.GetFocusedRowCellValue("NGAY_BD")).Date;
                NGAY_KTDateEdit.EditValue = Convert.ToDateTime(grvBangCapUV.GetFocusedRowCellValue("NGAY_KT")).Date;
                NGANH_DAO_TAOTextEdit.EditValue = grvBangCapUV.GetFocusedRowCellValue("NGANH_DAO_TAO");
                HT_DAO_TAOTextEdit.EditValue = grvBangCapUV.GetFocusedRowCellValue("HT_DAO_TAO");
                XEP_LOAITextEdit.EditValue = grvBangCapUV.GetFocusedRowCellValue("XEP_LOAI");
                TRINH_DOLookUpEdit.EditValue = Convert.ToInt64(grvBangCapUV.GetFocusedRowCellValue("ID_LOAI_TD"));
                NGAY_KYDateEdit.EditValue = Convert.ToDateTime(grvBangCapUV.GetFocusedRowCellValue("NGAY_KY")).Date;
                NGUOI_KYTextEdit.EditValue = grvBangCapUV.GetFocusedRowCellValue("NGUOI_KY");
                NOI_CAPTextEdit.EditValue = grvBangCapUV.GetFocusedRowCellValue("NOI_CAP");
                GHI_CHUMemoEdit.EditValue = grvBangCapUV.GetFocusedRowCellValue("GHI_CHU");
            }
        }
        private void SaveData()
        {
            try
            {

                int n = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateBangCapUngVien",
                        grvBangCapUV.GetFocusedRowCellValue("ID_BC"),
                        iduv,
                        NGUOI_KYTextEdit.EditValue,
                        TRINH_DOLookUpEdit.EditValue,
                        SO_HIEU_BANGTextEdit.EditValue,
                        NGAY_BDDateEdit.EditValue,
                        NGAY_KTDateEdit.EditValue,
                        NOI_HOCTextEdit.EditValue,
                        HT_DAO_TAOTextEdit.EditValue,
                        NGANH_DAO_TAOTextEdit.EditValue,
                        TEN_BANGTextEdit.Text.ToString() == "" ? DBNull.Value : TEN_BANGTextEdit.EditValue,
                        XEP_LOAITextEdit.EditValue,
                        NGAY_KYDateEdit.EditValue,
                        NOI_CAPTextEdit.EditValue,
                        GHI_CHUMemoEdit.EditValue, cothem));
                LoadgrdBangCap(n);
            }
            catch 
            { }
        }
        private void DeleteData()
        {
            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDeleteKhoaDaoTao"), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTieuDeXoa"), MessageBoxButtons.YesNo) == DialogResult.No) return;
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE	dbo.UNG_VIEN_BANG_CAP WHERE ID_BC = " + grvBangCapUV.GetFocusedRowCellValue("ID_BC") + "");
                grvBangCapUV.DeleteSelectedRows();
                ((DataTable)grdBangCapUV.DataSource).AcceptChanges();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDelDangSuDung") + "\n" + ex.Message.ToString(), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region sự kiện form
        private void ucBangCapUV_Load(object sender, EventArgs e)
        {
            Commons.OSystems.SetDateEditFormat(NGAY_BDDateEdit);
            Commons.OSystems.SetDateEditFormat(NGAY_KTDateEdit);
            Commons.OSystems.SetDateEditFormat(NGAY_KYDateEdit);

            LoadgrdBangCap(-1);
            Commons.Modules.ObjSystems.MLoadLookUpEdit(TRINH_DOLookUpEdit, Commons.Modules.ObjSystems.DataLoaiTrinhDo(false), "ID_LOAI_TD", "TEN_LOAI_TD", "TEN_LOAI_TD");
            Commons.Modules.ObjSystems.MAutoCompleteTextEdit(HT_DAO_TAOTextEdit, Commons.Modules.ObjSystems.ConvertDatatable(grdBangCapUV), "HT_DAO_TAO");
            Commons.Modules.ObjSystems.MAutoCompleteTextEdit(NGANH_DAO_TAOTextEdit, Commons.Modules.ObjSystems.ConvertDatatable(grdBangCapUV), "NGANH_DAO_TAO");
            enableButon(true);
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
        }

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "them":
                    {
                        Bindingdata(true);
                        cothem = true;
                        enableButon(false);
                        break;
                    }
                case "sua":
                    {
                        if (grvBangCapUV.RowCount == 0)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        cothem = false;
                        enableButon(false);
                        break;
                    }
                case "xoa":
                    {
                        if (grvBangCapUV.RowCount == 0)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        DeleteData();
                        if(grvBangCapUV.RowCount == 0)
                        {
                            Bindingdata(true);
                        }
                        break;
                    }
                case "In":
                    {
                        try
                        {
                            if (grvBangCapUV.RowCount == 0)
                            {
                                return;
                            }
                            System.Data.SqlClient.SqlConnection conn;
                            DataTable dt = new DataTable();
                            frmViewReport frm = new frmViewReport();
                            frm.rpt = new rptBCBangCapUV();

                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();

                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCBangCapUV", conn);
                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd.Parameters.Add("@ID_UV", SqlDbType.Int).Value = iduv;
                            cmd.CommandType = CommandType.StoredProcedure;

                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            dt = new DataTable();
                            dt = ds.Tables[0].Copy();
                           
                            dt.TableName = "DA_TA";
                            frm.AddDataSource(dt);

                            frm.ShowDialog();
                        }
                        catch
                        { }

                        break;
                    }
                case "luu":
                    {
                        if (!dxValidationProvider1.Validate()) return;
                        //kiem trung 
                        System.Data.SqlClient.SqlConnection conn;
                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                        conn.Open();
                        if (cothem == true)
                        {
                            id_BC = -1;
                        }
                        else
                        {
                            id_BC = Convert.ToInt64(grvBangCapUV.GetFocusedRowCellValue("ID_BC"));
                        }

                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spkiemtrungBCUV", conn);
                        cmd.Parameters.Add("@ID_BC", SqlDbType.BigInt).Value = id_BC;
                        cmd.Parameters.Add("@ID_UV", SqlDbType.BigInt).Value = iduv;
                        cmd.Parameters.Add("@SO_BC", SqlDbType.NVarChar).Value = SO_HIEU_BANGTextEdit.Text;
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (Convert.ToInt16(cmd.ExecuteScalar()) == 1)
                        {
                            XtraMessageBox.Show(ItemForSO_HIEU_BANG.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgBCap_NayDaTonTai"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SO_HIEU_BANGTextEdit.Focus();
                            return;
                        }
                        SaveData();
                        enableButon(true);
                        break;
                    }
                case "khongluu":
                    {
                        enableButon(true);
                        dxValidationProvider1.ValidateHiddenControls = false;
                        dxValidationProvider1.RemoveControlError(SO_HIEU_BANGTextEdit);
                        if (grvBangCapUV.RowCount > 0)
                        {
                            Bindingdata(false);
                        }
                        else
                        {
                            Bindingdata(true);
                        }
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
        private void grvBangCapCN_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Bindingdata(false);
        }

        private void grdBangCapCN_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (grvBangCapUV.RowCount == 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (e.KeyData == Keys.Delete)
            {
                DeleteData();
                if (grvBangCapUV.RowCount == 0)
                {
                    Bindingdata(true);
                }
            }
        }

        #endregion

    }
}
