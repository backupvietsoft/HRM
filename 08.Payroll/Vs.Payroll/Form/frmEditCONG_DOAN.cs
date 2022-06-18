using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;
using System.Reflection;

namespace Vs.Payroll
{
    public partial class frmEditCONG_DOAN : DevExpress.XtraEditors.XtraForm
    {
        Int64 iIdTo = -1;
        Int64 ID_LSP = -1;
        Boolean bAddEditTo = true;  // true la add false la edit
        string MS = "", TEN = "";

        public frmEditCONG_DOAN(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
            Commons.OSystems.SetDateEditFormat(NGAY_LAPDateEdit);
            iIdTo = iId;
            bAddEditTo = bAddEdit;
        }
        private void frmEditCONG_DOAN_Load(object sender, EventArgs e)
        {
            LoadCombobox();
            if (!bAddEditTo) LoadText();
        }
        private void LoadCombobox()
        {

            try
            {
                Commons.Modules.ObjSystems.MLoadLookUpEdit(TEN_LOAI_SAN_PHAMLookUpEdit, Commons.Modules.ObjSystems.DataLoaiSanPham(false), "ID_NHH", "TEN_NHH", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_NHH"), true);
                Commons.Modules.ObjSystems.MLoadLookUpEdit(CUMLookUpEdit, Commons.Modules.ObjSystems.DataCUM(Convert.ToInt32(TEN_LOAI_SAN_PHAMLookUpEdit.EditValue), false), "ID_CUM", "TEN_CUM", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_CUM"), true);
                Commons.Modules.ObjSystems.MLoadLookUpEdit(BAC_THOLookUpEdit, Commons.Modules.ObjSystems.DataBacTho(false), "ID_BT", "TEN_BAC_THO", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_BAC_THO"), true);
                Commons.Modules.ObjSystems.MLoadLookUpEdit(LOAI_MAYLookUpEdit, Commons.Modules.ObjSystems.DataLoaiMay(false), "ID_LM", "TEN_LOAI_MAY", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_LOAI_MAY"), true);
            }
            catch
            {
            }
        }
        private void LoadText()
        {
            string sSql = "";
            sSql = "SELECT NHOM_HANG_HOA.ID_NHH, CUM.ID_CUM, MS_CD, TEN_CD, TEN_CD_A, TEN_CD_H, HSBT.ID_BT, TGTK, LOAI_MAY.ID_LM, YEU_CAU_KT, CU_GA_LAP, NGAY_LAP, CONG_DOAN.STT FROM CONG_DOAN INNER JOIN CUM ON CONG_DOAN.ID_CUM = CUM.ID_CUM INNER JOIN NHOM_HANG_HOA ON CUM.ID_NHH = NHOM_HANG_HOA.ID_NHH INNER JOIN HSBT ON CONG_DOAN.ID_BT = HSBT.ID_BT INNER JOIN LOAI_MAY ON CONG_DOAN.ID_LM = LOAI_MAY.ID_LM WHERE ID_CD = " + iIdTo.ToString();
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
            if (dtTmp.Rows.Count <= 0) return;
            TEN_LOAI_SAN_PHAMLookUpEdit.EditValue = dtTmp.Rows[0]["ID_NHH"];
            CUMLookUpEdit.EditValue = dtTmp.Rows[0]["ID_CUM"];
            BAC_THOLookUpEdit.EditValue = dtTmp.Rows[0]["ID_BT"];
            LOAI_MAYLookUpEdit.EditValue = dtTmp.Rows[0]["ID_LM"];
            MS_CDTextEdit.EditValue = dtTmp.Rows[0]["MS_CD"];
            MS = dtTmp.Rows[0]["MS_CD"].ToString();
            TEN_CDTextEdit.EditValue = dtTmp.Rows[0]["TEN_CD"];
            TEN_CD_ATextEdit.EditValue = dtTmp.Rows[0]["TEN_CD_A"];
            TEN_CD_HTextEdit.EditValue = dtTmp.Rows[0]["TEN_CD_H"];
            TEN = dtTmp.Rows[0]["TEN_CD"].ToString();
            TGLTTextEdit1.EditValue = dtTmp.Rows[0]["TGTK"];
            CU_GA_LAPTextEdit.EditValue = dtTmp.Rows[0]["CU_GA_LAP"];
            YC_KTTextEdit.EditValue = dtTmp.Rows[0]["YEU_CAU_KT"];
            NGAY_LAPDateEdit.EditValue = dtTmp.Rows[0]["NGAY_LAP"];
            txtSTT.EditValue = dtTmp.Rows[0]["STT"];
            //CHONCheckEdit.EditValue = dtTmp.Rows[0]["CHON"];
            //DA_SDCheckEdit.EditValue = dtTmp.Rows[0]["DA_SU_DUNG"];
        }

        private void windowsUIButtonPanel2_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            try
            {
                switch (btn.Tag.ToString())
                {
                    case "luu":
                        {
                            if (!dxValidationProvider1.Validate()) return;
                            if (TEN_LOAI_SAN_PHAMLookUpEdit.Text == "")
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgchuachonloaisp"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                                TEN_LOAI_SAN_PHAMLookUpEdit.Focus();
                                return;
                            }
                            if (CUMLookUpEdit.Text == "")
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgchuachoncum"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                                CUMLookUpEdit.Focus();
                                return;
                            }
                            if (LOAI_MAYLookUpEdit.Text == "")
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgchuachonloaimay"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                                LOAI_MAYLookUpEdit.Focus();
                                return;
                            }
                            if (BAC_THOLookUpEdit.Text == "")
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgchuachonbactho"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                                BAC_THOLookUpEdit.Focus();
                                return;
                            }
                            if (NGAY_LAPDateEdit.Text == "")
                            {
                                NGAY_LAPDateEdit.EditValue = DateTime.Now.ToShortDateString();

                            }
                            if (KiemTrung()) return;

                            #region Them
                            System.Data.SqlClient.SqlConnection conn;
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spUpdateCONG_DOAN", conn);
                            if (bAddEditTo)
                            {
                                cmd.Parameters.Add("@ID_CD", SqlDbType.Int).Value = -1;
                            }
                            else
                            {
                                cmd.Parameters.Add("@ID_CD", SqlDbType.Int).Value = iIdTo;
                            }

                            cmd.Parameters.Add("@ID_CUM", SqlDbType.Int).Value = CUMLookUpEdit.EditValue;
                            cmd.Parameters.Add("@MS_CD", SqlDbType.NVarChar).Value = MS_CDTextEdit.Text;
                            cmd.Parameters.Add("@TEN_CD", SqlDbType.NVarChar).Value = TEN_CDTextEdit.Text;
                            cmd.Parameters.Add("@TEN_CD_A", SqlDbType.NVarChar).Value = TEN_CD_ATextEdit.Text;
                            cmd.Parameters.Add("@TEN_CD_H", SqlDbType.NVarChar).Value = TEN_CD_HTextEdit.Text;
                            cmd.Parameters.Add("@ID_BT", SqlDbType.Int).Value = BAC_THOLookUpEdit.EditValue;

                            cmd.Parameters.Add("@ID_LM", SqlDbType.Int).Value = LOAI_MAYLookUpEdit.EditValue;
                            cmd.Parameters.Add("@TGTK", SqlDbType.SmallInt).Value = (TGLTTextEdit1.EditValue == null ? 0 : (TGLTTextEdit1.EditValue));
                            cmd.Parameters.Add("@CU_GA_LAP", SqlDbType.NVarChar).Value = CU_GA_LAPTextEdit.Text;

                            cmd.Parameters.Add("@YEU_CAU_KT", SqlDbType.NVarChar).Value = YC_KTTextEdit.Text;
                            cmd.Parameters.Add("@NGAY_LAP", SqlDbType.DateTime).Value = NGAY_LAPDateEdit.DateTime;
                            cmd.Parameters.Add("@STT", SqlDbType.Int).Value = (txtSTT.EditValue == "") ? txtSTT.EditValue = null : txtSTT.EditValue;

                            //cmd.Parameters.Add("@CHON", SqlDbType.Bit).Value = CHONCheckEdit.Checked == true ? true : false;
                            //cmd.Parameters.Add("@DA_SU_DUNG", SqlDbType.Bit).Value = DA_SDCheckEdit.Checked == true ? true : false;


                            cmd.CommandType = CommandType.StoredProcedure;
                            Commons.Modules.sId = Convert.ToString(cmd.ExecuteScalar());


                            this.DialogResult = DialogResult.OK;
                            this.Close();
                            #endregion

                            break;
                        }
                    case "huy":
                        {
                            this.DialogResult = DialogResult.Cancel;
                            this.Close();
                            break;
                        }
                    default: break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private Boolean KiemTrung()
        {

            try
            {
                string sSql = "";
                string tenSql = "";
                if (bAddEditTo || MS != MS_CDTextEdit.EditValue.ToString())
                {
                    sSql = "SELECT COUNT(*) FROM [CONG_DOAN] WHERE MS_CD = '" + MS_CDTextEdit.EditValue + "'";

                    if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql)) != 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_MaSoTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));

                        return true;
                    }
                }

                if (bAddEditTo || TEN != TEN_CDTextEdit.EditValue.ToString())
                {

                    tenSql = "SELECT TEN_CD FROM CONG_DOAN WHERE TEN_CD = '" + TEN_CDTextEdit.EditValue + "'";

                    if (Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, tenSql)) == Convert.ToString((TEN_CDTextEdit.EditValue)))
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));

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


        private void frmEditCONG_DOAN_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

        private void TEN_LOAI_SAN_PHAMLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            Commons.Modules.ObjSystems.MLoadLookUpEdit(CUMLookUpEdit, Commons.Modules.ObjSystems.DataCUM(Convert.ToInt32(TEN_LOAI_SAN_PHAMLookUpEdit.EditValue), false), "ID_CUM", "TEN_CUM", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_CUM"));
        }
    }

}
