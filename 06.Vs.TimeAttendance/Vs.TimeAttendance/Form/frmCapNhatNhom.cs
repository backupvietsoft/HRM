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
using DevExpress.XtraEditors.Repository;

namespace Vs.TimeAttendance
{
    public partial class frmCapNhatNhom : DevExpress.XtraEditors.XtraForm
    {
        RepositoryItemTimeEdit repositoryItemTimeEdit1;
        DateTime dNgay;
        public DataTable dtCapNhat;
        private DateTime dGioBatDau = new DateTime();
        private DateTime dGioKetThuc = new DateTime();
        private int iPhutBatDau = 0;
        private int iPhutKetThuc = 0;
        private int iPhutBatDauQuyDinh = 0;
        private int iPhutKetThucQuyDinh = 0;
        private int iPhutAnCa = 0;
        private double dbGioTangCa = 0;
        private int iRes = 0;
        private bool flag = false;
        public frmCapNhatNhom(DateTime dngay)
        {
            dNgay = dngay;
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root,windowsUIButton);
            repositoryItemTimeEdit1 = new RepositoryItemTimeEdit();
            iRes = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT [dbo].[fnKiemTraNgayNghi]('" + Convert.ToDateTime(dngay).ToString("MM/dd/yyyy") + "')"));
        }

        private void frmCapNhatNhom_Load(object sender, EventArgs e)
        {
            try
            {
                repositoryItemTimeEdit1.TimeEditStyle = TimeEditStyle.TouchUI;
                repositoryItemTimeEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
                repositoryItemTimeEdit1.Mask.EditMask = "HH:mm";

                repositoryItemTimeEdit1.NullText = "00:00";
                repositoryItemTimeEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                repositoryItemTimeEdit1.DisplayFormat.FormatString = "HH:mm";
                repositoryItemTimeEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                repositoryItemTimeEdit1.EditFormat.FormatString = "HH:mm";

                lblNgay.Text = "Ngày " + dNgay.ToString("dd/MM/yyyy");
                loadcbm();
                datGioBD.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                datGioBD.Properties.DisplayFormat.FormatString = "HH:mm:ss";
                datGioBD.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                datGioBD.Properties.EditFormat.FormatString = "HH:mm:ss";
                datGioBD.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
                datGioBD.Properties.Mask.EditMask = "HH:mm:ss";

                datGioKT.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                datGioKT.Properties.DisplayFormat.FormatString = "HH:mm:ss";
                datGioKT.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                datGioKT.Properties.EditFormat.FormatString = "HH:mm:ss";
                datGioKT.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
                datGioKT.Properties.Mask.EditMask = "HH:mm:ss";

                txtSoGioTC.Properties.DisplayFormat.FormatString = "00.00";
                txtSoGioTC.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                txtSoGioTC.Properties.EditFormat.FormatString = "00.00";
                txtSoGioTC.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                txtSoGioTC.Properties.Mask.EditMask = "00.00";
                txtSoGioTC.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtSoGioTC.Properties.Mask.UseMaskAsDisplayFormat = true;
                Commons.Modules.sLoad = "0Load";
            }
            catch { }
            //Commons.OSystems.SetDateEditFormat(datGioBD);
            //Commons.OSystems.SetDateEditFormat(datGioKT);
        }

        private void loadcbm()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNhomca", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_NHOM, dt, "ID_NHOM", "TEN_NHOM", "Ten_nhom");
                cboID_NHOM.EditValue = -1;
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

                string sSql = "SELECT DISTINCT ID_CDLV ID_CA, CA, GIO_BD, GIO_KT, PHUT_BD, PHUT_KT FROM CHE_DO_LAM_VIEC WHERE ID_NHOM= " + cboID_NHOM.EditValue + " AND TANG_CA = 1 ORDER BY CA";
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));

                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboCA, dt, "ID_CA", "CA", "Ca_lam",true);
                cboCA.Properties.View.Columns["GIO_BD"].ColumnEdit = this.repositoryItemTimeEdit1;
                cboCA.Properties.View.Columns["GIO_KT"].ColumnEdit = this.repositoryItemTimeEdit1;
                cboCA.Properties.View.Columns["PHUT_BD"].Visible = false;
                cboCA.Properties.View.Columns["PHUT_KT"].Visible = false;
                cboCA.EditValue = Convert.ToInt64(-1);
            }
            catch (Exception ex)
            {
                
            }
        }


        private void cboID_nhom_EditValueChanged(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "";
            loadcbm_ca();
        }

        private Boolean kiemtrong()
        {
            if (cboID_NHOM.Text == "")
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaNhapNhom"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboID_NHOM.Focus();
                return false;
            }
            if (cboCA.Text == "")
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaNhapCa"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboCA.Focus();
                return false;
            }

            if (datGioBD.Text == "")
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaNhapNgay_BD"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                datGioBD.Focus();
                return false;
            }
            if (datGioKT.Text == "")
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaNhapNgay_KT"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                datGioKT.Focus();
                return false;
            }
            //if (Convert.ToDateTime(datGioKT.EditValue) <= Convert.ToDateTime(datGioBD.EditValue))
            //{
            //    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgGioKTPhaiLonHonGioBD"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}
            //if (Convert.ToDateTime(dNgay).DayOfWeek.ToString() != "Sunday" && Convert.ToDateTime(dNgay).DayOfWeek.ToString() != "Saturday")
            //{
            //    DataTable dt = new DataTable();
            //    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT NGAY FROM dbo.NGAY_NGHI_LE"));
            //    try
            //    {
            //        if (dt.AsEnumerable().Where(x => x.Field<string>("NGAY").Trim().Equals(dNgay.ToString())).CopyToDataTable().Rows.Count > 1)
            //        {
            //            return true;
            //        }
            //    }
            //    catch
            //    {
            //        try
            //        {
            //            DateTime dGioBD = Convert.ToDateTime(cboCA.Properties.View.GetFocusedRowCellValue("GIO_BD"));
            //            DateTime dGioKT = Convert.ToDateTime(cboCA.Properties.View.GetFocusedRowCellValue("GIO_KT"));
            //            if (Convert.ToDateTime("01/01/1900 " + Convert.ToDateTime(datGioBD.EditValue).TimeOfDay.ToString()) < dGioBD || Convert.ToDateTime("01/01/1900 " + Convert.ToDateTime(datGioBD.EditValue).TimeOfDay.ToString()) > dGioKT)
            //            {
            //                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgGioBatDauPhaiTrongKhoangThoiGianChoPhep"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                return false;
            //            }
            //            if (Convert.ToDateTime("01/01/1900 " + Convert.ToDateTime(datGioKT.EditValue).TimeOfDay.ToString()) < dGioBD || Convert.ToDateTime("01/01/1900 " + Convert.ToDateTime(datGioKT.EditValue).TimeOfDay.ToString()) > dGioKT)
            //            {
            //                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgGioKetThucPhaiTrongKhoangThoiGianChoPhep"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                return false;
            //            }
            //        }
            //        catch { }

            //    }

            //}
            return true;
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "capnhat":
                        {
                            if (flag == true) return;
                            if (!kiemtrong()) return;

                            //ID_NHOM = Convert.ToInt32(cboID_NHOM.EditValue);
                            //sCa = cboCA.EditValue.ToString();
                            //dGioBD = Convert.ToDateTime(datGioBD.EditValue);
                            //dGioKT = Convert.ToDateTime(datGioKT.EditValue);
                            //fSoPhutAnCa = Convert.ToDouble(txtPhutAnCa.Text);
                            //fSoGio = Convert.ToDouble(txtSoGioTC.Text);
                            //this.DialogResult = DialogResult.OK;
                            //this.Close();
                            DataColumn dtC;
                            DataRow dtR;
                            if (dtCapNhat != null)
                            {

                            }
                            else
                            {
                                dtCapNhat = new DataTable();
                                dtC = new DataColumn();
                                dtC.DataType = typeof(int);
                                dtC.ColumnName = "ID_NHOM";
                                dtC.Caption = "ID_NHOM";
                                dtC.ReadOnly = false;
                                dtCapNhat.Columns.Add(dtC);

                                dtC = new DataColumn();
                                dtC.DataType = typeof(int);
                                dtC.ColumnName = "ID_CA";
                                dtC.Caption = "ID_CA";
                                dtC.ReadOnly = false;
                                dtCapNhat.Columns.Add(dtC);

                                dtC = new DataColumn();
                                dtC.DataType = typeof(string);
                                dtC.ColumnName = "CA";
                                dtC.Caption = "CA";
                                dtC.ReadOnly = false;
                                dtCapNhat.Columns.Add(dtC);

                                dtC = new DataColumn();
                                dtC.DataType = typeof(DateTime);
                                dtC.ColumnName = "GIO_BD";
                                dtC.Caption = "GIO_BD";
                                dtC.ReadOnly = false;
                                dtCapNhat.Columns.Add(dtC);

                                dtC = new DataColumn();
                                dtC.DataType = typeof(DateTime);
                                dtC.ColumnName = "GIO_KT";
                                dtC.Caption = "GIO_KT";
                                dtC.ReadOnly = false;
                                dtCapNhat.Columns.Add(dtC);

                                dtC = new DataColumn();
                                dtC.DataType = typeof(double);
                                dtC.ColumnName = "PHUT_AN_CA";
                                dtC.Caption = "PHUT_AN_CA";
                                dtC.ReadOnly = false;
                                dtCapNhat.Columns.Add(dtC);

                                dtC = new DataColumn();
                                dtC.DataType = typeof(double);
                                dtC.ColumnName = "SO_GIO_TC";
                                dtC.Caption = "SO_GIO_TC";
                                dtC.ReadOnly = false;
                                dtCapNhat.Columns.Add(dtC);
                            }

                            dtR = dtCapNhat.NewRow();
                            dtR["ID_NHOM"] = Convert.ToInt32(cboID_NHOM.EditValue);
                            dtR["ID_CA"] = Convert.ToInt32(cboCA.EditValue);
                            dtR["CA"] = cboCA.Text;
                            dtR["GIO_BD"] = Convert.ToDateTime("01/01/1900 " + Convert.ToDateTime(datGioBD.EditValue).TimeOfDay.ToString());
                            dtR["GIO_KT"] = Convert.ToDateTime("01/01/1900 " + Convert.ToDateTime(datGioKT.EditValue).TimeOfDay.ToString());
                            dtR["PHUT_AN_CA"] = txtPhutAnCa.Text == "" ? 0 : Convert.ToDouble(txtPhutAnCa.Text);
                            dtR["SO_GIO_TC"] = txtSoGioTC.Text == "" ? 0 : Convert.ToDouble(txtSoGioTC.Text);
                            dtCapNhat.Rows.Add(dtR);
                            //if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgThemDuLieuThanhCongBanCoMuonTiepTuc"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) return;
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
            catch (Exception ex) { }

        }

        private void cboCA_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Commons.Modules.sLoad == "0Load") return;
                iPhutBatDauQuyDinh = Convert.ToInt32(cboCA.Properties.View.GetFocusedRowCellValue("PHUT_BD"));
                iPhutKetThucQuyDinh = Convert.ToInt32(cboCA.Properties.View.GetFocusedRowCellValue("PHUT_KT"));
                iPhutAnCa = 0;
                datGioBD.DateTime = Convert.ToDateTime(cboCA.Properties.View.GetFocusedRowCellValue("GIO_BD"));
                datGioKT.DateTime = Convert.ToDateTime(cboCA.Properties.View.GetFocusedRowCellValue("GIO_KT"));
            }
            catch { }

        }

        private void datGioBD_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                iPhutBatDau = datGioBD.Text == "" ? 0 : Convert.ToInt32(datGioBD.DateTime.Hour * 60 + datGioBD.DateTime.Minute);

                if (iPhutBatDau < iPhutKetThucQuyDinh - 1440)
                {
                    iPhutBatDau = iPhutBatDau + 1440;
                }

                dbGioTangCa = Convert.ToDouble((iPhutKetThuc - (iPhutBatDau + iPhutAnCa))) / 60;

                txtSoGioTC.Text = dbGioTangCa.ToString();
            }
            catch { }
        }

        private void datGioKT_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                iPhutKetThuc = datGioKT.Text == "" ? 0 : Convert.ToInt32(datGioKT.DateTime.Hour * 60 + datGioKT.DateTime.Minute);
                if (iPhutKetThuc < iPhutBatDau && iPhutKetThucQuyDinh > 1440)
                {
                    iPhutKetThuc = iPhutKetThuc + 1440;
                }

                dbGioTangCa = Convert.ToDouble((iPhutKetThuc - (iPhutBatDau + iPhutAnCa))) / 60;

                txtSoGioTC.Text = dbGioTangCa.ToString();
            }
            catch { }
        }

        private void txtPhutAnCa_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                iPhutAnCa = txtPhutAnCa.Text == "" ? 0 : Convert.ToInt32(txtPhutAnCa.Text);
                dbGioTangCa = Convert.ToDouble((iPhutKetThuc - (iPhutBatDau + iPhutAnCa))) / 60;

                txtSoGioTC.Text = dbGioTangCa.ToString();

            }
            catch { }
        }

        private void datGioBD_Validating(object sender, CancelEventArgs e)
        {
            var edit = sender as DateEdit;
            flag = false;
            DateTime.TryParse(edit.DateTime.ToString(), out dGioBatDau);
            iPhutBatDau = dGioBatDau.Hour * 60 + dGioBatDau.Minute;
            if (iPhutBatDau < iPhutKetThucQuyDinh - 1440)
            {
                iPhutBatDau = iPhutBatDau + 1440;
            }
 
            if (iPhutBatDau < iPhutBatDauQuyDinh && iRes == 0)
            {
                flag = true;
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgGioBatDauPhaiTrongKhoangThoiGianChoPhep"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
            if (iPhutBatDau > iPhutKetThuc)
            {
                flag = true;
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgGioBDPhaiNhoHonGioKT"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }

        private void datGioKT_Validating(object sender, CancelEventArgs e)
        {
            var edit = sender as DateEdit;
            flag = false;
            DateTime.TryParse(edit.DateTime.ToString(), out dGioKetThuc);
            iPhutKetThuc = dGioKetThuc.Hour * 60 + dGioKetThuc.Minute;
            if (iPhutKetThuc < iPhutBatDau && iPhutKetThucQuyDinh > 1440)
            {
                iPhutKetThuc = iPhutKetThuc + 1440;
            }            

            if (iPhutKetThuc > iPhutKetThucQuyDinh && iRes == 0)
            {
                flag = true;
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgGioKetThucPhaiTrongKhoangThoiGianChoPhep"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
 
            if (iPhutKetThuc < iPhutBatDau)
            {
                flag = true;
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgGioKTPhaiLonHonGioBD"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }

    }
}
