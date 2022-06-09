using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
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

namespace Vs.Payroll.Form
{
    public partial class frmInBaoCaoPCD : DevExpress.XtraEditors.XtraForm
    {
        DateTime dtNgay;
        Int64 iID_CHUYEN;
        Int64 iID_CHUYEN_SD;
        Int64 iID_ORD;
        public frmInBaoCaoPCD(DateTime dtngay, Int64 id_chuyen, Int64 id_chuyen_sd, Int64 id_ord)
        {
            InitializeComponent();
            dtNgay = dtngay;
            iID_CHUYEN = id_chuyen;
            iID_CHUYEN_SD = id_chuyen_sd;
            iID_ORD = id_ord;
        }

        #region even

        private void frmInBaoCaoPCD_Load(object sender, EventArgs e)
        {
            try
            {
                datTNgay.EditValue = Convert.ToDateTime(DateTime.Now.AddDays(-60));
                datDNgay.EditValue = DateTime.Now;
                Commons.Modules.sLoad = "0Load";
                
                //LoadCboMaQL();
                //LoadCboCNThucHienCD();
                Commons.Modules.sLoad = "";
                lblCongDoan.Enabled = false;
                lblCongNhan.Enabled = false;
                cboMaQL.Enabled = false;
                cboID_CN.Enabled = false;
                LoadNN();
            }
            catch { }
        }

        private void windowsUIButtonPanel1_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "in":
                    {
                        DataTable dt = new DataTable();
                        if (!dxValidationProvider1.Validate()) return;
                        dxValidationProvider1.Validate();
                        switch (rdo_ChonBaoCao.SelectedIndex)
                        {
                            case 0:
                                {
                                    InBCTongHop();
                                    break;
                                }
                            case 1:
                                {
                                    if (cboMaQL.Text.Trim() == "")
                                    {
                                        XtraMessageBox.Show(lblCongDoan.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                                        cboMaQL.Focus();
                                        return;
                                    }
                                    InDanhSachCN();
                                    break;
                                }
                            case 2:
                                {
                                    if (cboID_CN.Text.Trim() == "")
                                    {
                                        XtraMessageBox.Show(lblCongNhan.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                                        cboID_CN.Focus();
                                        return;
                                    }
                                    InBangNangXuat();
                                    break;
                                }
                            default:
                                break;
                        }
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

        private void rdo_ChonBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdo_ChonBaoCao.SelectedIndex == 0)
            {
                lblCongDoan.Enabled = false;
                cboMaQL.Enabled = false;
                lblCongNhan.Enabled = false;
                cboID_CN.Enabled = false;

                cboID_CN.EditValue = 0;
                cboMaQL.EditValue = 0;
            }
            if (rdo_ChonBaoCao.SelectedIndex == 1)
            {
                lblCongDoan.Enabled = true;
                cboMaQL.Enabled = true;

                lblCongNhan.Enabled = false;
                cboID_CN.Enabled = false;
                cboID_CN.EditValue = 0;
            }
            if (rdo_ChonBaoCao.SelectedIndex == 2)
            {
                lblCongDoan.Enabled = false;
                cboMaQL.Enabled = false;
                cboMaQL.EditValue = 0;

                lblCongNhan.Enabled = true;
                cboID_CN.Enabled = true;
            }
        }

        private void datTNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadCboMaQL();
            LoadCboCNThucHienCD();
        }

        private void datDNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadCboMaQL();
            LoadCboCNThucHienCD();
        }
        #endregion

        #region function 
        private void LoadNN()
        {
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, windowsUIButtonPanel1);
            rdo_ChonBaoCao.Properties.Items[0].Description = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "rdoTongHopCD");
            rdo_ChonBaoCao.Properties.Items[1].Description = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "rdoDSCNThucHien");
            rdo_ChonBaoCao.Properties.Items[2].Description = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "rdoDSCDTheoCN");
        }

        private void LoadCboMaQL()
        {
            System.Data.SqlClient.SqlConnection conn;
            DataTable dt = new DataTable();
            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetComboDSCDThucHien", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(datTNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(datDNgay.EditValue);
                cmd.Parameters.Add("@ID_CHUYEN", SqlDbType.BigInt).Value = iID_CHUYEN;
                cmd.Parameters.Add("@ID_CHUYEN_SD", SqlDbType.BigInt).Value = iID_CHUYEN_SD;
                cmd.Parameters.Add("@ID_ORD", SqlDbType.BigInt).Value = iID_ORD;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);

                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if(cboMaQL.Properties.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboMaQL, dt, "ID_CD", "TEN_CD", "TEN_CD", false);
                    cboMaQL.Properties.View.PopulateColumns(cboMaQL.Properties.DataSource);
                    cboMaQL.Properties.View.Columns["ID_CD"].Visible = false;
                    cboMaQL.Properties.View.Columns["THU_TU_CONG_DOAN"].Visible = false;
                    try { cboMaQL.Properties.View.Columns["MaQL"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "MaQL"); } catch { }
                    try { cboMaQL.Properties.View.Columns["TEN_CD"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_CD"); } catch { }
                }
                else
                {
                    cboMaQL.Properties.DataSource = dt;
                }

                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            catch { }
        }

        private void LoadCboCNThucHienCD()
        {
            System.Data.SqlClient.SqlConnection conn;
            DataTable dt = new DataTable();
            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetComboDSCNThucHienCD", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(datTNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(datDNgay.EditValue);
                cmd.Parameters.Add("@ID_CHUYEN", SqlDbType.BigInt).Value = iID_CHUYEN;
                cmd.Parameters.Add("@ID_CHUYEN_SD", SqlDbType.BigInt).Value = iID_CHUYEN_SD;
                cmd.Parameters.Add("@ID_ORD", SqlDbType.BigInt).Value = iID_ORD;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);

                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (cboID_CN.Properties.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_CN, dt, "ID_CN", "HO_TEN", "HO_TEN", false);
                    cboID_CN.Properties.View.PopulateColumns(cboID_CN.Properties.DataSource);
                    cboID_CN.Properties.View.Columns["ID_CN"].Visible = false;
                    cboID_CN.Properties.View.Columns["MS_CN"].Visible = false;
                    try { cboID_CN.Properties.View.Columns["HO_TEN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "HO_TEN"); } catch { }
                }
                else
                {
                    cboID_CN.Properties.DataSource = dt;
                }

                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            catch { }
        }

        private void InBCTongHop()
        {
            System.Data.SqlClient.SqlConnection conn;
            frmViewReport frm = new frmViewReport();
            frm.rpt = new rptBangTongHopCongDoan(DateTime.Now,Convert.ToDateTime(datTNgay.EditValue),Convert.ToDateTime(datDNgay.EditValue));
            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCTongHopCongDoan", conn);

                cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = string.IsNullOrEmpty(datTNgay.EditValue.ToString()) ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(datTNgay.EditValue).ToShortDateString();
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = string.IsNullOrEmpty(datDNgay.EditValue.ToString()) ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(datDNgay.EditValue).ToShortDateString();
                cmd.Parameters.Add("@ID_CHUYEN", SqlDbType.Int).Value = iID_CHUYEN;
                cmd.Parameters.Add("@ID_CHUYEN_SD", SqlDbType.Int).Value = iID_CHUYEN_SD;
                cmd.Parameters.Add("@ID_ORD", SqlDbType.Int).Value = iID_ORD;

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);

                //body
                DataTable dt2 = new DataTable();
                dt2 = ds.Tables[1].Copy();
                dt2.TableName = "DATA2";
                frm.AddDataSource(dt2);

                //Header
                DataTable dt1 = new DataTable();
                dt1 = ds.Tables[0].Copy();
                dt1.TableName = "DATA1";
                frm.AddDataSource(dt1);


                //Lon nhat
                DataTable dt3 = new DataTable();
                dt3 = ds.Tables[2].Copy();
                dt3.TableName = "DATA3";
                frm.AddDataSource(dt3);

                //Nho nhat
                DataTable dt4 = new DataTable();
                dt4 = ds.Tables[3].Copy();
                dt4.TableName = "DATA4";
                frm.AddDataSource(dt4);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            frm.ShowDialog();
        }

        private void InDanhSachCN()
        {
            System.Data.SqlClient.SqlConnection conn;
            frmViewReport frm = new frmViewReport();
            frm.rpt = new rptDanhSachCongNhan(DateTime.Now, Convert.ToDateTime(datTNgay.EditValue), Convert.ToDateTime(datDNgay.EditValue));
            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCDanhSachCNThucHienCD", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = string.IsNullOrEmpty(datTNgay.EditValue.ToString()) ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(datTNgay.EditValue).ToShortDateString();
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = string.IsNullOrEmpty(datDNgay.EditValue.ToString()) ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(datDNgay.EditValue).ToShortDateString();
                cmd.Parameters.Add("@ID_CHUYEN", SqlDbType.Int).Value = iID_CHUYEN;
                cmd.Parameters.Add("@ID_CHUYEN_SD", SqlDbType.Int).Value = iID_CHUYEN_SD;
                cmd.Parameters.Add("@ID_ORD", SqlDbType.Int).Value = iID_ORD;
                cmd.Parameters.Add("@ID_CD", SqlDbType.Int).Value = Convert.ToInt32(cboMaQL.EditValue);

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);

                //body
                DataTable dt2 = new DataTable();
                dt2 = ds.Tables[1].Copy();
                dt2.TableName = "DATA2";
                frm.AddDataSource(dt2);

                //Header
                DataTable dt1 = new DataTable();
                dt1 = ds.Tables[0].Copy();
                dt1.TableName = "DATA1";
                frm.AddDataSource(dt1);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            frm.ShowDialog();
        }

        private void InBangNangXuat()
        {
            System.Data.SqlClient.SqlConnection conn;
            frmViewReport frm = new frmViewReport();
            frm.rpt = new rptBangNangSuat(DateTime.Now, Convert.ToDateTime(datTNgay.EditValue), Convert.ToDateTime(datDNgay.EditValue));
            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCDanhSachCDThucHienCN", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = string.IsNullOrEmpty(datTNgay.EditValue.ToString()) ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(datTNgay.EditValue).ToShortDateString();
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = string.IsNullOrEmpty(datDNgay.EditValue.ToString()) ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(datDNgay.EditValue).ToShortDateString();
                cmd.Parameters.Add("@ID_CHUYEN", SqlDbType.Int).Value = iID_CHUYEN;
                cmd.Parameters.Add("@ID_CHUYEN_SD", SqlDbType.Int).Value = iID_CHUYEN_SD;
                cmd.Parameters.Add("@ID_ORD", SqlDbType.Int).Value = iID_ORD;
                cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = Convert.ToInt32(cboID_CN.EditValue);

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);

                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "DATA";
                frm.AddDataSource(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            frm.ShowDialog();
        }


        #endregion

       
    }
}
