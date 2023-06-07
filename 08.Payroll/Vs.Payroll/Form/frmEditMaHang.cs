using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Vs.Payroll
{
    public partial class frmEditMaHang : DevExpress.XtraEditors.XtraForm
    {
        Int64 iIdOrder = -1;
        public frmEditMaHang(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
            iIdOrder = iId;
        }
        private void frmEditMaHang_Load(object sender, EventArgs e)
        {
            enableButon(true);
            Commons.Modules.sLoad = "0Load";
            try
            {
                Commons.Modules.ObjSystems.LoadCboDonVi(cboDonVi);

                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDON_VI", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                dt.Columns[1].ReadOnly = false;
                dt.Rows[0][1] = "";
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboDonVi, dt, "ID_DV", "TEN_DV", "TEN_DV");


                LoadCbo();

                if (iIdOrder != -1)
                    LoadText();
                else
                    LoadNull();
            }
            catch { }
            Commons.Modules.sLoad = "";
        }

        private void LoadCbo()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spMaHang", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 6;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Commons.Modules.iIDUser;
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.AcceptChanges();

                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboKhachHang, dt, "ID_DT", "TEN_NGAN", "TEN_NGAN");

                dt = new DataTable();
                dt = ds.Tables[1].Copy();
                dt.Rows[0][1] = "";
                dt.AcceptChanges();
                Commons.Modules.ObjSystems.MAutoCompleteTextEdit(txtLHH, dt, "TEN_LOAI_HH");

            }
            catch { }
        }

        private void LookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            //Display lookup editor's current value.
            LookUpEditBase lookupEditor = sender as LookUpEditBase;
            if (lookupEditor == null) return;
            //LabelControl label = labelDictionary[lookupEditor];
            //if (label == null) return;
            //if (lookupEditor.EditValue == null)
            //    label.Text = "Current EditValue: null";
            //else
            //    label.Text = "Current EditValue: " + lookupEditor.EditValue.ToString();
        }

        private void LoadText()
        {
            try
            {
                string sSql = "";
                sSql = "SELECT ID_DV, ID_DT,TEN_LOAI_HH, TEN_HH, NGAY_LAP, ISNULL(CLOSED,0) AS CLOSED,ID_ORD FROM dbo.DON_HANG_BAN_ORDER WHERE ID_ORD = " + iIdOrder.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                if (dtTmp.Rows.Count <= 0) return;
                try { cboDonVi.EditValue = Convert.ToInt64(dtTmp.Rows[0]["ID_DV"]); } catch { }
                try { cboKhachHang.EditValue = dtTmp.Rows[0]["ID_DT"]; } catch { }
                try { txtLHH.EditValue = dtTmp.Rows[0]["TEN_LOAI_HH"]; } catch { }
                try { txtTEN_HH.EditValue = dtTmp.Rows[0]["TEN_HH"]; } catch { }
                try { datNGAY_LAP.DateTime = DateTime.Parse(dtTmp.Rows[0]["NGAY_LAP"].ToString()); } catch { }
                try { chkCLOSED.EditValue = dtTmp.Rows[0]["CLOSED"]; } catch { }
            }
            catch { }
        }

        private void LoadNull()
        {
            iIdOrder = -1;
            cboDonVi.EditValue = null;
            cboKhachHang.EditValue = null;
            txtLHH.EditValue = null;
            txtTEN_HH.EditValue = "";
            datNGAY_LAP.DateTime = DateTime.Now.Date;
            chkCLOSED.Checked = false;
            cboDonVi.Focus();
        }



        private void enableButon(bool visible)
        {
            try
            {
                btnALL.Buttons[0].Properties.Visible = !visible;
                btnALL.Buttons[1].Properties.Visible = visible;
                btnALL.Buttons[2].Properties.Visible = visible;
                btnALL.Buttons[3].Properties.Visible = !visible;
            }
            catch
            {

            }
        }

        private void windowsUIButtonPanel2_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            try
            {
                switch (btn.Tag.ToString())
                {
                    case "them":
                        {
                            enableButon(true);
                            LoadNull();
                            break;
                        }

                    case "luu":
                        {


                            if (!KiemNull()) return;
                            if (KiemTrung()) return;
                            Luu();

                            break;
                        }
                    case "huy":
                        {
                            if (iIdOrder == -1) LoadNull(); else LoadText();
                            enableButon(false);
                            break;
                        }
                    case "thoat":
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


        private Boolean KiemNull()
        {
            Boolean bOK = true;
            try
            {
                if (string.IsNullOrWhiteSpace(txtTEN_HH.Text))
                {
                    //XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonLoaiHH"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //txtTEN_HH.Focus();
                    txtTEN_HH.ErrorText = Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaNhapTenHH");
                    bOK = false;
                }
                if (string.IsNullOrWhiteSpace(txtLHH.Text))
                {
                    //XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonLoaiHH"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //cboLHH.Focus();
                    txtLHH.ErrorText = Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonLoaiHH");
                    bOK = false;
                }
                if (string.IsNullOrWhiteSpace(cboKhachHang.Text))
                {
                    //XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonDoiTac"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //cboKhachHang.Focus();
                    cboKhachHang.ErrorText = Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonDoiTac");
                    bOK = false;
                }
                if (string.IsNullOrWhiteSpace(cboDonVi.Text))
                {
                    //XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonDonVi"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //cboDonVi.Focus();
                    cboDonVi.ErrorText = Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonDonVi");
                    bOK = false;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                bOK = false;
            }
            return bOK;
        }


        private Boolean KiemTrung()
        {
            try
            {

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spMaHang", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 4;
                cmd.Parameters.Add("@TEN_HH", SqlDbType.NVarChar).Value = txtTEN_HH.Text;
                cmd.Parameters.Add("@ID_DV", SqlDbType.BigInt).Value = cboDonVi.EditValue;
                cmd.Parameters.Add("@ID_ORD", SqlDbType.BigInt).Value = iIdOrder;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() != "0")
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", dt.Rows[0][1].ToString()), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private Boolean Luu()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spMaHang", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 5;
                cmd.Parameters.Add("@ID_ORD", SqlDbType.BigInt).Value = iIdOrder;
                cmd.Parameters.Add("@ID_DV", SqlDbType.BigInt).Value = cboDonVi.EditValue;
                cmd.Parameters.Add("@ID_DT", SqlDbType.BigInt).Value = cboKhachHang.EditValue;
                cmd.Parameters.Add("@TEN_DT", SqlDbType.NVarChar).Value = cboKhachHang.Text;
                cmd.Parameters.Add("@TEN_HH", SqlDbType.NVarChar).Value = txtTEN_HH.Text;
                cmd.Parameters.Add("@TEN_LHH", SqlDbType.NVarChar).Value = txtLHH.Text;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = datNGAY_LAP.DateTime.Date;
                cmd.Parameters.Add("@DDong", SqlDbType.Int).Value = (chkCLOSED.Checked ? 1 : 0);

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() == "-99")
                    {

                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", dt.Rows[0][1].ToString()), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        iIdOrder = Int64.Parse(dt.Rows[0][0].ToString());
                        this.BeginInvoke(new MethodInvoker(delegate
                        {
                            Commons.Modules.ObjSystems.MAutoCompleteTextEdit(txtLHH, "SELECT DISTINCT T1.TEN_LOAI_HH FROM  dbo.DON_HANG_BAN_ORDER T1 WHERE LTRIM(RTRIM(ISNULL(T1.TEN_LOAI_HH,''))) <> '' ORDER BY T1.TEN_LOAI_HH", "TEN_LOAI_HH");
                            LoadText();
                        }));

                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgLuuThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));

                        enableButon(false);
                        return false;

                    }
                }
                else
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgLuuKhongThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                return true;
            }
        }

        private void frmEditMaHang_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

    }

    // Represents a custom validation rule.
    public class CustomValidationRule : ValidationRule
    {
        public override bool Validate(Control control, object value)
        {
            string str = (string)value;
            string[] values = new string[] { "Dr.", "Mr.", "Mrs.", "Miss", "Ms." };
            bool res = false;
            foreach (string val in values)
            {
                if (ValidationHelper.Validate(str, ConditionOperator.BeginsWith,
                    val, null, null, false))
                {
                    string name = str.Substring(val.Length);
                    if (name.Trim().Length > 0) res = true;
                }
            }
            return res;
        }
    }
}
