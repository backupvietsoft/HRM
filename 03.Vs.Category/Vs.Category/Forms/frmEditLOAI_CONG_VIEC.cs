using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;

namespace Vs.Category
{
    public partial class frmEditLOAI_CONG_VIEC : DevExpress.XtraEditors.XtraForm
    {
        Int64 Id = 0;
        Boolean AddEdit = true;  // true la add false la edit
        public frmEditLOAI_CONG_VIEC(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);

            Id = iId;
            AddEdit = bAddEdit;
        }

        private void frmEditLOAI_CONG_VIEC_Load(object sender, EventArgs e)
        {
            LoadLoaiTO();
            if (!AddEdit) LoadText();
        }

        private void frmEditLOAI_CONG_VIEC_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

        private void LoadLoaiTO()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListLOAI_TO", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_LTSearchLookUpEdit, dt, "ID_LT", "TEN_LT", "TEN_LT");
        //    ID_LTSearchLookUpEdit.Properties.DataSource = dt;
        //    ID_LTSearchLookUpEdit.Properties.ValueMember = "ID_LT";
        //    ID_LTSearchLookUpEdit.Properties.DisplayMember = "TEN_LT";
        //    ID_LTSearchLookUpEdit.Properties.PopulateViewColumns();

            try
            {

                if(ID_LTSearchLookUpEdit.Properties.View.Columns["ID_LT"]!=null) ID_LTSearchLookUpEdit.Properties.View.Columns["ID_LT"].Visible = false;
             //   ID_LTSearchLookUpEdit.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.None;
            //    ID_LTSearchLookUpEdit.Properties.View.Columns["TEN_LT"].Caption = Commons.Modules.ObjLanguages.GetLanguage("ucListDMuc", "TEN_LT");
            //    ID_LTSearchLookUpEdit.Properties.View.Columns["TEN_LT"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }
            catch (Exception EX)
            {
                XtraMessageBox.Show(EX.Message.ToString());
            }
        }
        private void LoadText()
        {
            try
            {
                string sSql = "SELECT ID_LCV, TEN_LCV, TEN_LCV_A, TEN_LCV_H, DOC_HAI, PHEP_CT, ID_LT, STT " +
                    "FROM LOAI_CONG_VIEC WHERE ID_LCV =	" + Id.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                TEN_LCVTextEdit.EditValue = dtTmp.Rows[0]["TEN_LCV"].ToString();
                TEN_LCV_ATextEdit.EditValue = dtTmp.Rows[0]["TEN_LCV_A"].ToString();
                TEN_LCV_HTextEdit.EditValue = dtTmp.Rows[0]["TEN_LCV_H"].ToString();
                DOC_HAICheckEdit.EditValue = Convert.ToBoolean(dtTmp.Rows[0]["DOC_HAI"]);
                PHEP_CTTextEdit.EditValue = dtTmp.Rows[0]["PHEP_CT"].ToString();
                ID_LTSearchLookUpEdit.EditValue = dtTmp.Rows[0]["ID_LT"];
                txtSTT.EditValue = Convert.ToInt32(dtTmp.Rows[0]["STT"]);
            }
            catch 
            {
            }
        }
        private void LoadTextNull()
        {
            try
            {
                TEN_LCVTextEdit.EditValue = String.Empty;
                TEN_LCV_ATextEdit.EditValue = String.Empty;
                TEN_LCV_HTextEdit.EditValue = String.Empty;
                DOC_HAICheckEdit.EditValue = false;
                PHEP_CTTextEdit.EditValue = 0;
                TEN_LCVTextEdit.Focus();
                txtSTT.EditValue = 1;
            }
            catch { }
        }
        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {

                    case "luu":
                        {
                            if (!dxValidationProvider1.Validate()) return;
                            if (bKiemTrung()) return;
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateLOAI_CONG_VIEC", (AddEdit ? -1 : Id),
                                TEN_LCVTextEdit.EditValue, TEN_LCV_ATextEdit.EditValue,
                                TEN_LCV_HTextEdit.EditValue, DOC_HAICheckEdit.EditValue,
                                (PHEP_CTTextEdit.EditValue == null) ? 0 : PHEP_CTTextEdit.EditValue,
                                ID_LTSearchLookUpEdit.EditValue,
                                (txtSTT.EditValue == "") ? txtSTT.EditValue = null : txtSTT.EditValue
                                ).ToString();
                            if (AddEdit)
                            {
                                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_ThemThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    LoadTextNull();
                                    return;
                                }
                            }
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                            break;
                        }
                    case "huy":
                        {
                            this.Close();
                            break;
                        }
                    default: break;
                }
            }
            catch (Exception EX)
            {

                XtraMessageBox.Show(EX.Message.ToString());
            }

        }
        private bool bKiemTrung()
        {
            try
            {
                DataTable dtTmp = new DataTable();
                Int16 iKiem = 0;

                iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_LCV",
                    (AddEdit ? "-1" : Id.ToString()), "LOAI_CONG_VIEC", "TEN_LCV", TEN_LCVTextEdit.EditValue.ToString(),
                    "", "", "", ""));
                if (iKiem > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                    TEN_LCVTextEdit.Focus();
                    return true;
                }

                iKiem = 0;

                if (!string.IsNullOrEmpty(TEN_LCV_ATextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_LCV",
                        (AddEdit ? "-1" : Id.ToString()), "LOAI_CONG_VIEC", "TEN_LCV_A", TEN_LCV_ATextEdit.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        TEN_LCV_ATextEdit.Focus();
                        return true;
                    }
                }

                iKiem = 0;
                if (!string.IsNullOrEmpty(TEN_LCV_HTextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_LCV",
                        (AddEdit ? "-1" : Id.ToString()), "LOAI_CONG_VIEC", "TEN_LCV_H", TEN_LCV_HTextEdit.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        TEN_LCV_HTextEdit.Focus();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                return true;
            }
            return false;
        }
    }
}