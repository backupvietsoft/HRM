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
using System.Diagnostics;
using Vs.Report;
using DevExpress.XtraBars.Docking2010;

namespace Vs.TimeAttendance.Form
{
    public partial class frmInKehoachdica : DevExpress.XtraEditors.XtraForm
    {
        public frmInKehoachdica()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this,Root);
        }

        private void frmInKehoachdica_Load(object sender, EventArgs e)
        {
            loadcbm();
            DateTime dtTN = DateTime.Today;
            dtTN = dtTN.AddDays(-dtTN.Day + 1);
            DateTime dtDN = dtTN.AddMonths(1);
            dtDN = dtDN.AddDays(-1);
            txtTngay.EditValue = dtTN;
            txtDngay.EditValue = dtDN;

            Commons.OSystems.SetDateEditFormat(txtTngay);
            Commons.OSystems.SetDateEditFormat(txtDngay);

            ngayin.EditValue = DateTime.Today;
        }

        private void loadcbm()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNhomca", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_nhom, dt, "ID_NHOM", "TEN_NHOM", "Ten_nhom");
                cboID_nhom.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void loadcbm_ca()
        {
            try
            {
                DataTable dt = new DataTable();
                
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCaTheoKHDC", cboID_nhom.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));

                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboCa, dt, "ID_CA", "CA", "Ca_lam");
                cboCa.EditValue = -1;

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

       
        private void cboID_nhom_EditValueChanged(object sender, EventArgs e)
        {
            loadcbm_ca();
        }

        private Boolean kiemtrong()
        {
            if (cboID_nhom.Text == "")
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "messchuanhapNhom"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboID_nhom.Focus();
                return false;
            }
            if (cboCa.Text == "")
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "messchuanhapca"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboCa.Focus();
                return false;
            }

            if (txtTngay.Text == "")
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "messchuanhapngay_bd"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTngay.Focus();
                return false;
            }
            if (txtDngay.Text == "")
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "messchuanhapngay_kt"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDngay.Focus();
                return false;
            }
            return true;
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "In":
                    {
                        if (!kiemtrong()) return;
                        try
                        {
                            System.Data.SqlClient.SqlConnection conn;
                            DataTable dt = new DataTable();
                            frmViewReport frm = new frmViewReport();
                            //string tieude = "KẾ HOẠCH ĐI CA";
                            frm.rpt = new rptKeHoachDiCa(DateTime.Today, txtTngay.DateTime, txtDngay.DateTime);

                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();

                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCKeHoachDiCa", conn);
                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd.Parameters.AddWithValue("Nhom_sx", cboID_nhom.EditValue);
                            cmd.Parameters.AddWithValue("CA", cboCa.EditValue);
                            cmd.Parameters.AddWithValue("TNGAY", txtTngay.DateTime);
                            cmd.Parameters.AddWithValue("DNGAY", txtDngay.DateTime);

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
                case "thoat":
                    {
                        this.Close();
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
