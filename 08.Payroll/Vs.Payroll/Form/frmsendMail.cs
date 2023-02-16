using System;
using System.Data;
using System.Drawing;
using System.Linq;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;
using System.Reflection;
using DataTable = System.Data.DataTable;
using Excel;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils.CommonDialogs;
using DevExpress.Utils.Menu;
using static DevExpress.CodeParser.CodeStyle.Formatting.Rules.Spacing;
using System.Net.Mail;
using System.Net.Mime;
using static DevExpress.XtraEditors.Mask.MaskSettings;
using System.Web;

namespace Vs.Payroll
{
    public partial class frmsendMail : DevExpress.XtraEditors.XtraForm
    {
        private int iLoai = 0;
        private int iID_DV = 0;
        private string sTenDV = "";
        string sLink_TL_CC = "";
        string sLink_TL_TL = "";
        DateTime datNgayOld; // lưu ngày trước đó của datDNgay
        public frmsendMail(int Loai, int ID_DV, string TenDV) // iLoai = 1 chấm công, 2 là tính lương
        {
            InitializeComponent();
            iLoai = Loai;
            iID_DV = ID_DV;
            sTenDV = TenDV;
        }

        private void frmsendMail_Load(object sender, EventArgs e)
        {
            datTNgay.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            datDNgay.DateTime = DateTime.Now;
            Commons.OSystems.SetDateEditFormat(datTNgay);
            Commons.OSystems.SetDateEditFormat(datDNgay);
            datNgayOld = datDNgay.DateTime;
            sLink_TL_TL = addFoder(iLoai == 1 ? "ChamCong" : "TinhLuong", sTenDV);
            txtDuongDanTL.Text = sLink_TL_TL;
            LoadFile(getAttachment(1, txtDuongDanTL.Text));
            string sSQL = "SELECT CASE " + iLoai + " WHEN 1 THEN EMAIL_CHAM_CONG WHEN 2 THEN EMAIL_TINH_LUONG END EMAIL FROM dbo.DON_VI WHERE ID_DV = " + iID_DV + " ";
            txtEmail.Text = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSQL));
            LoadText(iID_DV, iLoai);
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, btnALL);
        }
        private void frmsendMail_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();
        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {


                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {

                    case "in":
                        {
                            this.Cursor = Cursors.WaitCursor;
                            Commons.Modules.ObjSystems.ShowWaitForm(this);
                            switch (iLoai)
                            {

                                case 1:
                                    {
                                        sLink_TL_CC = addFoder("ChamCong", sTenDV);

                                        break;
                                    }
                                case 2:
                                    {
                                        sLink_TL_TL = addFoder("TinhLuong", sTenDV);
                                        LuongSPChiTietTheoNgay(iID_DV, sLink_TL_TL, datTNgay.DateTime, datDNgay.DateTime);
                                        LuongSPTongHopNgay(iID_DV, sLink_TL_TL);
                                        LuongSPTongHopThang(iID_DV, sLink_TL_TL, datTNgay.DateTime, datDNgay.DateTime);
                                        LuongSPTongHopTN(iID_DV, sLink_TL_TL, datTNgay.DateTime, datDNgay.DateTime);

                                        txtDuongDanTL.Text = sLink_TL_TL;
                                        LoadFile(getAttachment(1, txtDuongDanTL.Text));
                                        break;
                                    }
                                default:
                                    {
                                        break;
                                    }
                            }
                            break;
                        }
                    case "sendmail":
                        {

                            //TEST

                            //using (MailMessage mm = new MailMessage("conghuong@nviet.onmicrosoft.com", "dattranlfc1010@gmail.com"))
                            //{

                            //    string AttachmentPath = "F:\\FILE_GuiMail";
                            //    mm.Subject = "Birthday Greetings";
                            //    var htmlStringBody = HttpUtility.HtmlEncode(txtBody.Text);
                            //    mm.Body = string.Format("<!DOCTYPE html>\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n\r\n<head>\r\n    <title></title>\r\n</head>\r\n\r\n<body>\r\n    <p style='margin:0in;font-size:15px;font-family:\"Calibri\",sans-serif;line-height:14.4pt;'><strong><span style='font-size:13px;font-family:\"Arial\",sans-serif;color:black;'>Thanks &amp; Best regards,</span></strong></p>\r\n    <table style=\"border: none;width:6.0in;border-collapse:collapse;\">\r\n        <tbody>\r\n            <tr>\r\n                <td colspan=\"2\" style=\"width: 6in;border-top: none;border-right: none;border-left: none;border-image: initial;border-bottom: 1pt solid rgb(56, 86, 35);padding: 0in 5.4pt;height: 26.5pt;vertical-align: top;\">\r\n                    <p style='margin:0in;font-size:15px;font-family:\"Calibri\",sans-serif;margin-top:0in;margin-right:-5.75pt;margin-bottom:.0001pt;margin-left:-5.2pt;line-height:105%;'><strong><span style='font-size:16px;line-height:105%;font-family:\"Arial\",sans-serif;color:black;'>Lien Nguyen Thi</span></strong></p>\r\n                    <p style='margin:0in;font-size:15px;font-family:\"Calibri\",sans-serif;margin-top:0in;margin-right:-5.75pt;margin-bottom:.0001pt;margin-left:-5.2pt;line-height:105%;'><strong><span style='font-size:11px;line-height:  105%;font-family:\"Arial\",sans-serif;color:black;'>&nbsp;</span></strong></p>\r\n                    <p style='margin:0in;font-size:15px;font-family:\"Calibri\",sans-serif;margin-top:0in;margin-right:-5.75pt;margin-bottom:.0001pt;margin-left:-5.2pt;line-height:105%;'><strong><span style='font-size:13px;line-height:105%;font-family:\"Arial\",sans-serif;color:black;'>HR Executive &nbsp; &nbsp; &nbsp; &nbsp;I &nbsp; &nbsp; &nbsp; C&Ocirc;NG TY CỔ PHẦN MAY DUY MINH (DUY MINH GARMENT JSC)</span></strong></p>\r\n                </td>\r\n            </tr>\r\n            <tr>\r\n                <td style=\"width: 108.55pt;padding: 0in 5.4pt;vertical-align: top;\">\r\n                    <p style='margin:0in;font-size:15px;font-family:\"Calibri\",sans-serif;margin-top:0in;margin-right:-5.25pt;margin-bottom:.0001pt;margin-left:-5.75pt;line-height:105%;'><span style='font-size:13px;line-height:  105%;font-family:\"Arial\",sans-serif;color:black;'>&nbsp;<img src=\"http://27.74.240.29/Vietsoft/Tailieu/FileGuiMail/logoDM.png\" style=\"width: 94.5pt; height: 94.5pt;\" alt=\"image\"></span></p>\r\n                </td>\r\n                <td style=\"width: 323.45pt;padding: 0in 5.4pt;vertical-align: top;\">\r\n                    <p style='margin:0in;font-size:15px;font-family:\"Calibri\",sans-serif;margin-top:6.0pt;margin-right:-5.2pt;margin-bottom:  6.0pt;margin-left:0in;line-height:105%;'><strong><span style='font-size:13px;line-height:105%;font-family:\"Arial\",sans-serif;color:black;'>Address 1:&nbsp;</span></strong><span style='font-size:13px;line-height:  105%;font-family:\"Arial\",sans-serif;color:black;background:white;'>Bao Minh Industrial Park, Vu Ban District, Nam Dinh</span></p>\r\n                    <p style='margin:0in;font-size:15px;font-family:\"Calibri\",sans-serif;margin-top:6.0pt;margin-right:-5.2pt;margin-bottom:  6.0pt;margin-left:0in;line-height:105%;'><strong><span style='font-size:13px;line-height:105%;font-family:\"Arial\",sans-serif;color:black;'>Tel<span style=\"background:  white;\">:&nbsp;</span></span></strong><em><span style='font-size:13px;line-height:105%;font-family:\"Arial\",sans-serif;color:black;background:white;'>(+84)228 659 3999</span></em></p>\r\n                    <p style='margin:0in;font-size:15px;font-family:\"Calibri\",sans-serif;margin-top:6.0pt;margin-right:-5.2pt;margin-bottom:  6.0pt;margin-left:0in;line-height:105%;'><strong><span style='font-size:13px;line-height:105%;font-family:\"Arial\",sans-serif;color:black;'>Address 2:&nbsp;</span></strong><span style='font-size:13px;line-height:  105%;font-family:\"Arial\",sans-serif;color:black;background:white;'>Truc Noi Commune,Truc Ninh District, Nam Dinh</span></p>\r\n                    <p style='margin:0in;font-size:15px;font-family:\"Calibri\",sans-serif;margin-top:6.0pt;margin-right:-5.2pt;margin-bottom:  6.0pt;margin-left:0in;line-height:105%;'><strong><span style='font-size:13px;line-height:105%;font-family:\"Arial\",sans-serif;color:black;'>Tel<span style=\"background:  white;\">:&nbsp;</span></span></strong><em><span style='font-size:13px;line-height:105%;font-family:\"Arial\",sans-serif;color:black;background:white;'>(+84)228 655 6777 &nbsp; &nbsp; &nbsp;&nbsp;</span></em><strong><span style='font-size:13px;line-height:105%;font-family:\"Arial\",sans-serif;color:black;background:white;'>Ext:&nbsp;</span></strong><span style='font-size:13px;line-height:  105%;font-family:\"Arial\",sans-serif;color:black;background:white;'>108</span></p>\r\n                    <p style='margin:0in;font-size:15px;font-family:\"Calibri\",sans-serif;margin-top:6.0pt;margin-right:-5.2pt;margin-bottom:  6.0pt;margin-left:0in;line-height:105%;'><strong><span style='font-size:13px;line-height:105%;font-family:\"Arial\",sans-serif;color:black;'>Mobile:&nbsp;</span></strong><em><span style='font-size:13px;line-height:105%;font-family:\"Arial\",sans-serif;color:black;'>(+84) &nbsp;&nbsp;</span></em><strong><span style='font-size:13px;line-height:105%;font-family:\"Arial\",sans-serif;color:black;'>Email:&nbsp;</span></strong><a href=\"mailto:liennguyen@duyminhgarment.com\">liennguyen<span style='font-size:13px;line-height:  105%;font-family:\"Arial\",sans-serif;'>@duyminhgarment.com</span></a><span style='font-size:13px;line-height:  105%;font-family:\"Arial\",sans-serif;color:black;'>&nbsp;</span></p>\r\n                </td>\r\n            </tr>\r\n            <tr>\r\n                <td style=\"width: 108.55pt;padding: 0in 5.4pt;vertical-align: top;\">\r\n                    <p style='margin:0in;font-size:15px;font-family:\"Calibri\",sans-serif;margin-right:-5.25pt;line-height:105%;'><span style='font-size:13px;line-height:  105%;font-family:\"Arial\",sans-serif;color:#1F497D;'>&nbsp;</span></p>\r\n                </td>\r\n                <td style=\"width: 323.45pt;padding: 0in 5.4pt;vertical-align: top;\">\r\n                    <p style='margin:0in;font-size:15px;font-family:\"Calibri\",sans-serif;margin-right:-5.05pt;line-height:105%;'><strong><span style='font-size:13px;line-height:105%;font-family:\"Arial\",sans-serif;color:#0F442C;'>&nbsp;</span></strong></p>\r\n                </td>\r\n            </tr>\r\n        </tbody>\r\n    </table>\r\n    <p style='margin:0in;font-size:15px;font-family:\"Calibri\",sans-serif;'><img src=\"http://27.74.240.29/VietSoft/Tailieu/FileGuiMail/KhachHangDM.png\" style=\"width: 6in; height: 40.5pt;\" alt=\"image\"></p>\r\n    <p><br></p>\r\n <br>" + htmlStringBody + "</body>\r\n\r\n</html>");
                            //    mm.IsBodyHtml = true;
                            //    SmtpClient smtp = new SmtpClient();
                            //    smtp.Host = "smtp.gmail.com";
                            //    smtp.EnableSsl = true;
                            //    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential();
                            //    credentials.UserName = "dattranlfc@gmail.com";
                            //    credentials.Password = "shdjyeofzkrapiql";
                            //    smtp.UseDefaultCredentials = true;
                            //    smtp.Credentials = credentials;
                            //    smtp.Port = 587;
                            //    smtp.Send(mm);
                            //    WriteLog("Email sent successfully to: " + "dat" + " " + "bamboo2711@gmail.com");
                            //}




                            if (txtDuongDanTL.Text == "")
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "lblDuongDanKhongDuocTrong"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgfrmThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtDuongDanTL.Focus();
                                return;
                            }
                            if (txtEmail.Text == "")
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "lblEmailKhongDuocTrong"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgfrmThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtEmail.Focus();
                                return;
                            }
                            if (txtSubject.Text == "")
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "lblSubjectKhongDuocTrong"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgfrmThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtSubject.Focus();
                                return;
                            }

                            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgBanCoChacMuonGuiMail"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgfrmThongBao"), MessageBoxButtons.YesNo) == DialogResult.No) return;
                            try
                            {
                                string sLinkImageLogo = "http://27.74.240.29/VietSoft/Tailieu/FileGuiMail/logoDM.png";
                                string sLinkImageCus = "http://27.74.240.29/VietSoft/Tailieu/FileGuiMail/KhachHangDM.png";
                                var htmlStringBody = HttpUtility.HtmlEncode(txtBody.Text);
                                htmlStringBody = string.Format("<!DOCTYPE html>\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n\r\n<head>\r\n    <title></title>\r\n</head>\r\n\r\n<body>\r\n    <p style='margin:0in;font-size:15px;font-family:\"Calibri\",sans-serif;line-height:14.4pt;'><strong><span style='font-size:13px;font-family:\"Arial\",sans-serif;color:black;'>Thanks &amp; Best regards,</span></strong></p>\r\n    <table style=\"border: none;width:6.0in;border-collapse:collapse;\">\r\n        <tbody>\r\n            <tr>\r\n                <td colspan=\"2\" style=\"width: 6in;border-top: none;border-right: none;border-left: none;border-image: initial;border-bottom: 1pt solid rgb(56, 86, 35);padding: 0in 5.4pt;height: 26.5pt;vertical-align: top;\">\r\n                    <p style='margin:0in;font-size:15px;font-family:\"Calibri\",sans-serif;margin-top:0in;margin-right:-5.75pt;margin-bottom:.0001pt;margin-left:-5.2pt;line-height:105%;'><strong><span style='font-size:16px;line-height:105%;font-family:\"Arial\",sans-serif;color:black;'>Lien Nguyen Thi</span></strong></p>\r\n                    <p style='margin:0in;font-size:15px;font-family:\"Calibri\",sans-serif;margin-top:0in;margin-right:-5.75pt;margin-bottom:.0001pt;margin-left:-5.2pt;line-height:105%;'><strong><span style='font-size:11px;line-height:  105%;font-family:\"Arial\",sans-serif;color:black;'>&nbsp;</span></strong></p>\r\n                    <p style='margin:0in;font-size:15px;font-family:\"Calibri\",sans-serif;margin-top:0in;margin-right:-5.75pt;margin-bottom:.0001pt;margin-left:-5.2pt;line-height:105%;'><strong><span style='font-size:13px;line-height:105%;font-family:\"Arial\",sans-serif;color:black;'>HR Executive &nbsp; &nbsp; &nbsp; &nbsp;I &nbsp; &nbsp; &nbsp; C&Ocirc;NG TY CỔ PHẦN MAY DUY MINH (DUY MINH GARMENT JSC)</span></strong></p>\r\n                </td>\r\n            </tr>\r\n            <tr>\r\n                <td style=\"width: 108.55pt;padding: 0in 5.4pt;vertical-align: top;\">\r\n                    <p style='margin:0in;font-size:15px;font-family:\"Calibri\",sans-serif;margin-top:0in;margin-right:-5.25pt;margin-bottom:.0001pt;margin-left:-5.75pt;line-height:105%;'><span style='font-size:13px;line-height:  105%;font-family:\"Arial\",sans-serif;color:black;'>&nbsp;<img src=\"" + sLinkImageLogo + "\" style=\"width: 94.5pt; height: 94.5pt;\" alt=\"image\"></span></p>\r\n                </td>\r\n                <td style=\"width: 323.45pt;padding: 0in 5.4pt;vertical-align: top;\">\r\n                    <p style='margin:0in;font-size:15px;font-family:\"Calibri\",sans-serif;margin-top:6.0pt;margin-right:-5.2pt;margin-bottom:  6.0pt;margin-left:0in;line-height:105%;'><strong><span style='font-size:13px;line-height:105%;font-family:\"Arial\",sans-serif;color:black;'>Address 1:&nbsp;</span></strong><span style='font-size:13px;line-height:  105%;font-family:\"Arial\",sans-serif;color:black;background:white;'>Bao Minh Industrial Park, Vu Ban District, Nam Dinh</span></p>\r\n                    <p style='margin:0in;font-size:15px;font-family:\"Calibri\",sans-serif;margin-top:6.0pt;margin-right:-5.2pt;margin-bottom:  6.0pt;margin-left:0in;line-height:105%;'><strong><span style='font-size:13px;line-height:105%;font-family:\"Arial\",sans-serif;color:black;'>Tel<span style=\"background:  white;\">:&nbsp;</span></span></strong><em><span style='font-size:13px;line-height:105%;font-family:\"Arial\",sans-serif;color:black;background:white;'>(+84)228 659 3999</span></em></p>\r\n                    <p style='margin:0in;font-size:15px;font-family:\"Calibri\",sans-serif;margin-top:6.0pt;margin-right:-5.2pt;margin-bottom:  6.0pt;margin-left:0in;line-height:105%;'><strong><span style='font-size:13px;line-height:105%;font-family:\"Arial\",sans-serif;color:black;'>Address 2:&nbsp;</span></strong><span style='font-size:13px;line-height:  105%;font-family:\"Arial\",sans-serif;color:black;background:white;'>Truc Noi Commune,Truc Ninh District, Nam Dinh</span></p>\r\n                    <p style='margin:0in;font-size:15px;font-family:\"Calibri\",sans-serif;margin-top:6.0pt;margin-right:-5.2pt;margin-bottom:  6.0pt;margin-left:0in;line-height:105%;'><strong><span style='font-size:13px;line-height:105%;font-family:\"Arial\",sans-serif;color:black;'>Tel<span style=\"background:  white;\">:&nbsp;</span></span></strong><em><span style='font-size:13px;line-height:105%;font-family:\"Arial\",sans-serif;color:black;background:white;'>(+84)228 655 6777 &nbsp; &nbsp; &nbsp;&nbsp;</span></em><strong><span style='font-size:13px;line-height:105%;font-family:\"Arial\",sans-serif;color:black;background:white;'>Ext:&nbsp;</span></strong><span style='font-size:13px;line-height:  105%;font-family:\"Arial\",sans-serif;color:black;background:white;'>108</span></p>\r\n                    <p style='margin:0in;font-size:15px;font-family:\"Calibri\",sans-serif;margin-top:6.0pt;margin-right:-5.2pt;margin-bottom:  6.0pt;margin-left:0in;line-height:105%;'><strong><span style='font-size:13px;line-height:105%;font-family:\"Arial\",sans-serif;color:black;'>Mobile:&nbsp;</span></strong><em><span style='font-size:13px;line-height:105%;font-family:\"Arial\",sans-serif;color:black;'>(+84) &nbsp;&nbsp;</span></em><strong><span style='font-size:13px;line-height:105%;font-family:\"Arial\",sans-serif;color:black;'>Email:&nbsp;</span></strong><a href=\"mailto:liennguyen@duyminhgarment.com\">liennguyen<span style='font-size:13px;line-height:  105%;font-family:\"Arial\",sans-serif;'>@duyminhgarment.com</span></a><span style='font-size:13px;line-height:  105%;font-family:\"Arial\",sans-serif;color:black;'>&nbsp;</span></p>\r\n                </td>\r\n            </tr>\r\n            <tr>\r\n                <td style=\"width: 108.55pt;padding: 0in 5.4pt;vertical-align: top;\">\r\n                    <p style='margin:0in;font-size:15px;font-family:\"Calibri\",sans-serif;margin-right:-5.25pt;line-height:105%;'><span style='font-size:13px;line-height:  105%;font-family:\"Arial\",sans-serif;color:#1F497D;'>&nbsp;</span></p>\r\n                </td>\r\n                <td style=\"width: 323.45pt;padding: 0in 5.4pt;vertical-align: top;\">\r\n                    <p style='margin:0in;font-size:15px;font-family:\"Calibri\",sans-serif;margin-right:-5.05pt;line-height:105%;'><strong><span style='font-size:13px;line-height:105%;font-family:\"Arial\",sans-serif;color:#0F442C;'>&nbsp;</span></strong></p>\r\n                </td>\r\n            </tr>\r\n        </tbody>\r\n    </table>\r\n    <p style='margin:0in;font-size:15px;font-family:\"Calibri\",sans-serif;'><img src=\"" + sLinkImageCus + "\" style=\"width: 6in; height: 40.5pt;\" alt=\"image\"></p>\r\n    <p><br></p>\r\n <br>" + htmlStringBody + "</body>\r\n\r\n</html>");
                                this.Cursor = Cursors.WaitCursor;
                                Commons.Modules.ObjSystems.ShowWaitForm(this);

                                System.Data.SqlClient.SqlConnection conn;
                                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                conn.Open();
                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spSendMail", conn);
                                cmd.Parameters.Add("@DV", SqlDbType.NVarChar).Value = Commons.Modules.KyHieuDV;
                                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = iID_DV;
                                cmd.Parameters.Add("@FileNameAttach", SqlDbType.NVarChar).Value = getAttachment(0, txtDuongDanTL.Text);
                                cmd.Parameters.Add("@sSubJect", SqlDbType.NVarChar).Value = txtSubject.Text;
                                cmd.Parameters.Add("@sBody", SqlDbType.NVarChar).Value = htmlStringBody;
                                cmd.Parameters.Add("@sListEmail", SqlDbType.NVarChar).Value = txtEmail.Text;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.ExecuteNonQuery();
                                conn.Close();
                                Commons.Modules.ObjSystems.Alert("Gửi mail thành công", Commons.Form_Alert.enmType.Success);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            break;
                        }
                    case "thoat":
                        {
                            this.Close();
                            break;
                        }
                    default: break;
                }

                this.Cursor = Cursors.Default;
                Commons.Modules.ObjSystems.HideWaitForm();
            }
            catch (Exception EX)
            {
                this.Cursor = Cursors.Default;
                Commons.Modules.ObjSystems.HideWaitForm();
                XtraMessageBox.Show(EX.Message.ToString());
            }
        }
        private void txtDuongDanTL_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                Process.Start(txtDuongDanTL.Text);
            }
            catch { }
        }

        #region function

        private string addFoder(string sLoai, string sTenDV) // sLoai
        {
            try
            {
                string strDuongDanTmp = Commons.Modules.ObjSystems.CapnhatTL("FileGuiMail\\" + sTenDV + "\\" + sLoai + "" + '\\' + DateTime.Now.ToString("ddMMyyyy"), false);
                return strDuongDanTmp;
            }
            catch
            {
                return "";
            }
        }
        private bool deleteFile(string path)
        {
            try
            {
                System.IO.DirectoryInfo di = new DirectoryInfo(path);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private string getAttachment(int iLoai, string sPath) // iLoai = 0 lấy fullName, 1 lấy Name
        {
            try
            {
                string str = "";
                if (sPath != "")
                {
                    DirectoryInfo d = new DirectoryInfo(sPath); //Assuming Test is your Folder
                    FileInfo[] Files = d.GetFiles(); //Getting Text files

                    foreach (FileInfo file in Files)
                    {
                        str = str + (iLoai == 0 ? file.FullName : file.Name) + ";";
                    }
                    if (str != "")
                    {
                        str = str.Substring(0, str.Length - 1);
                    }
                }
                return str;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private void LoadFile(string sFile)
        {
            try
            {
                string sSQL = "SELECT value TAI_LIEU FROM STRING_SPLIT('" + sFile + "',';')";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSQL));
                dt.Columns["TAI_LIEU"].ReadOnly = true;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, true, true, false, true, true, this.Name);
                RepositoryItemButtonEdit btnEdit = new RepositoryItemButtonEdit();
                grvData.Columns["TAI_LIEU"].ColumnEdit = btnEdit;
                btnEdit.ButtonClick += BtnEdit_ButtonClick;
            }
            catch { }
        }
        private void LoadText(int iID_DV, int Loai)
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spSendMail", conn);
                cmd.Parameters.Add("@DV", SqlDbType.NVarChar).Value = Commons.Modules.KyHieuDV;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = iID_DV;
                cmd.Parameters.Add("@iLoai_Email", SqlDbType.Int).Value = Loai;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();

                txtSubject.Text = dt.Rows[0]["SUB"].ToString();
                txtBody.Text = dt.Rows[0]["BODY"].ToString();
            }
            catch { }
        }
        private void BtnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                Commons.Modules.ObjSystems.OpenHinh(txtDuongDanTL.Text + @"\" + grvData.GetFocusedRowCellValue("TAI_LIEU").ToString());
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgBanKhongCoQuyenTruyCapDD"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgfrmThongBao"), MessageBoxButtons.OK);
            }
        }
        private void WriteLog(string message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            string file = path + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";

            if (!System.IO.File.Exists(file))
            {
                using (StreamWriter sw = File.CreateText(file))
                {
                    sw.WriteLine(message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(file))
                {
                    sw.WriteLine(message);
                }
            }
        }

        #region Excel BaoCao
        private void LuongSPChiTietTheoNgay(int iID_DV, string path, DateTime NgayDauThang, DateTime NgayCuoiThang)
        {
            try
            {
                if (System.IO.File.Exists(path + @"\BCChiTietNgay_" + (iID_DV == 1 ? "DM1" : "DM2") + "_" + datDNgay.DateTime.ToString("yyyyMMdd") + ".xlsx")) return;

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtTTChung;
                DataTable dtChuyen;
                DataTable dtBCLSP;

                dtTTChung = new DataTable();
                dtTTChung = Commons.Modules.ObjSystems.DataThongTinChung();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetLSPChuyen", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = "admin";
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = iID_DV;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = NgayDauThang;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = NgayCuoiThang;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtChuyen = new DataTable();
                dtChuyen = ds.Tables[0].Copy();
                if (dtChuyen.Rows.Count == 0)
                {
                    WriteLog("Error Báo cáo chi tiết ngày: Không có dữ liệu in");
                    return;
                }
                Microsoft.Office.Interop.Excel.Application oApp;
                Microsoft.Office.Interop.Excel.Workbook oBook;
                Excel.Worksheet oSheet;
                oApp = new Microsoft.Office.Interop.Excel.Application();
                oApp.Visible = false;
                oBook = oApp.Workbooks.Add();
                oSheet = (Excel.Worksheet)oBook.Worksheets.get_Item(1);

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 11;
                int oRow = 1;

                foreach (DataRow rowC in dtChuyen.Rows)
                {
                    TaoTTChung(oSheet, 1, 2, 1, 7, 0, 0);
                    oSheet.Name = rowC[1].ToString();
                    if (oRow == 1)
                    {
                        Microsoft.Office.Interop.Excel.Range row4_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[4, 15]];
                        row4_TieuDe_BaoCao.Merge();
                        row4_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                        row4_TieuDe_BaoCao.Font.Name = fontName;
                        row4_TieuDe_BaoCao.Font.Bold = true;
                        row4_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        row4_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row4_TieuDe_BaoCao.RowHeight = 30;
                        row4_TieuDe_BaoCao.Value2 = "BẢNG KÊ SẢN LƯỢNG CHUYỀN MAY THÁNG " + Convert.ToDateTime(DateTime.Now).ToString("MM/yyyy");
                        oRow = 6;
                    }

                    Microsoft.Office.Interop.Excel.Range row_Chuyen = oSheet.Range[oSheet.Cells[oRow, 7], oSheet.Cells[oRow, 7]];
                    row_Chuyen.Merge();
                    row_Chuyen.Value2 = rowC[1].ToString();
                    row_Chuyen.Font.Size = fontSizeNoiDung;
                    row_Chuyen.Font.Name = fontName;
                    row_Chuyen.Font.Bold = true;
                    row_Chuyen.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    row_Chuyen.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row_Chuyen.RowHeight = 30;

                    oRow++;

                    System.Data.SqlClient.SqlCommand cmdCT = new System.Data.SqlClient.SqlCommand("rptBCLuongSPChiTietNgay", conn);
                    cmdCT.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = "admin";
                    cmdCT.Parameters.Add("@NNgu", SqlDbType.Int).Value = 0;
                    cmdCT.Parameters.Add("@Dvi", SqlDbType.Int).Value = iID_DV;
                    cmdCT.Parameters.Add("@XN", SqlDbType.Int).Value = -1;
                    cmdCT.Parameters.Add("@TO", SqlDbType.Int).Value = -1;
                    cmdCT.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = rowC[0];
                    cmdCT.Parameters.Add("@TNgay", SqlDbType.Date).Value = NgayDauThang;
                    cmdCT.Parameters.Add("@DNgay", SqlDbType.Date).Value = NgayCuoiThang;
                    cmdCT.CommandType = CommandType.StoredProcedure;
                    System.Data.SqlClient.SqlDataAdapter adpCT = new System.Data.SqlClient.SqlDataAdapter(cmdCT);

                    DataSet dsCT = new DataSet();
                    adpCT.Fill(dsCT);
                    dtBCLSP = new DataTable();
                    dtBCLSP = dsCT.Tables[0].Copy();
                    int totalColumn = dtBCLSP.Columns.Count;


                    oSheet.Cells[oRow, 1] = "STT";
                    oSheet.Cells[oRow, 1].ColumnWidth = 10;
                    oSheet.Cells[oRow, 2] = "Ngày";
                    oSheet.Cells[oRow, 2].ColumnWidth = 15;
                    oSheet.Cells[oRow, 3] = "Họ tên";
                    oSheet.Cells[oRow, 3].ColumnWidth = 25;
                    oSheet.Cells[oRow, 4] = "Mã NV";
                    oSheet.Cells[oRow, 4].ColumnWidth = 15;
                    oSheet.Cells[oRow, 5] = "Mã NV (4 số)";
                    oSheet.Cells[oRow, 5].ColumnWidth = 10;
                    oSheet.Cells[oRow, 6] = "Bộ phận";
                    oSheet.Cells[oRow, 6].ColumnWidth = 35;
                    oSheet.Cells[oRow, 7] = "Mã đơn hàng";
                    oSheet.Cells[oRow, 7].ColumnWidth = 25;
                    oSheet.Cells[oRow, 8] = "Mã công đoạn";
                    oSheet.Cells[oRow, 8].ColumnWidth = 10;
                    oSheet.Cells[oRow, 9] = "Tên công đoạn";
                    oSheet.Cells[oRow, 9].ColumnWidth = 35;
                    oSheet.Cells[oRow, 10] = "Sản lượng ghi nhận";
                    oSheet.Cells[oRow, 10].ColumnWidth = 15;
                    oSheet.Cells[oRow, 11] = "Đơn giá";
                    oSheet.Cells[oRow, 11].ColumnWidth = 15;
                    oSheet.Cells[oRow, 12] = "Tổng tiền lương";
                    oSheet.Cells[oRow, 12].ColumnWidth = 10;
                    oSheet.Cells[oRow, 13] = "Tổng SL theo MVN";
                    oSheet.Cells[oRow, 13].ColumnWidth = 15;
                    oSheet.Cells[oRow, 14] = "Tổng SL theo Cđoan";
                    oSheet.Cells[oRow, 14].ColumnWidth = 15;
                    oSheet.Cells[oRow, 15] = "Ghi chú";
                    oSheet.Cells[oRow, 15].ColumnWidth = 15;

                    Microsoft.Office.Interop.Excel.Range row_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, 1], oSheet.Cells[oRow, totalColumn]];
                    row_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                    row_TieuDe_BaoCao.Font.Name = fontName;
                    row_TieuDe_BaoCao.Font.Bold = true;
                    row_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row_TieuDe_BaoCao.Cells.WrapText = true;
                    row_TieuDe_BaoCao.Interior.Color = Color.FromArgb(198, 224, 180);
                    BorderAround(row_TieuDe_BaoCao);

                    oRow++;
                    DataRow[] dr = dtBCLSP.Select();
                    string[,] rowData = new string[dr.Count(), dtBCLSP.Columns.Count];

                    int rowCnt = 0;
                    int rowBD = oRow;
                    foreach (DataRow row in dtBCLSP.Rows)
                    {
                        for (int col = 0; col < dtBCLSP.Columns.Count; col++)
                        {
                            rowData[rowCnt, col] = row[col].ToString();
                        }
                        rowCnt++;
                    }
                    oRow = rowBD + rowCnt - 1;
                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, totalColumn]].Value2 = rowData;
                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, totalColumn]].Font.Size = fontSizeNoiDung;
                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, totalColumn]].Font.Name = fontName;
                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, totalColumn]].Font.Name = fontName;
                    oSheet.Range[oSheet.Cells[rowBD, 8], oSheet.Cells[oRow, totalColumn]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    BorderAround(oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, totalColumn]]);

                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, 2]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Range[oSheet.Cells[rowBD, 5], oSheet.Cells[oRow, 5]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                    Microsoft.Office.Interop.Excel.Range formatRange;
                    formatRange = oSheet.Range[oSheet.Cells[rowBD, 2], oSheet.Cells[oRow, 2]];
                    formatRange.NumberFormat = "dd/MM/yyyy";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                    formatRange = oSheet.Range[oSheet.Cells[rowBD, 10], oSheet.Cells[oRow, 10]];
                    formatRange.NumberFormat = "#,##0;(#,##0.000); ; ";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                    formatRange = oSheet.Range[oSheet.Cells[rowBD, 11], oSheet.Cells[oRow, 11]];
                    formatRange.NumberFormat = "#,##0.00;(#,##0.000); ; ";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                    formatRange = oSheet.Range[oSheet.Cells[rowBD, 12], oSheet.Cells[oRow, 12]];
                    formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                    formatRange = oSheet.Range[oSheet.Cells[rowBD, 13], oSheet.Cells[oRow, 13]];
                    formatRange.NumberFormat = "#,##0;(#,##0.000); ; ";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                    formatRange = oSheet.Range[oSheet.Cells[rowBD, 14], oSheet.Cells[oRow, 14]];
                    formatRange.NumberFormat = "#,##0;(#,##0.000); ; ";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                    oSheet.get_Range("I" + (rowBD - 2).ToString() + "").Value2 = "=SUBTOTAL(9,L" + rowBD + ":L" + oRow.ToString() + ")";
                    oSheet.get_Range("I" + (rowBD - 2).ToString() + "").NumberFormat = "#,##0;(#,##0); ; ";
                    oSheet.get_Range("I" + (rowBD - 2).ToString() + "").Font.Size = fontSizeNoiDung;
                    oSheet.get_Range("I" + (rowBD - 2).ToString() + "").Font.Name = fontName;
                    oSheet.get_Range("I" + (rowBD - 2).ToString() + "").Font.Bold = true;
                    oSheet.get_Range("I" + (rowBD - 2).ToString() + "").Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oRow = oRow + 2;

                    oRow = 1;
                    oSheet = (Excel.Worksheet)oBook.ActiveSheet;
                    oSheet = oBook.Worksheets.Add(After: oBook.Sheets[oBook.Sheets.Count]);
                }
                oBook.Sheets[1].Activate();
                oBook.SaveAs(path + @"\BCChiTietNgay_" + (iID_DV == 1 ? "DM1" : "DM2") + "_" + datDNgay.DateTime.ToString("yyyyMMdd") + ".xlsx",
                    AccessMode: Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive);
                oBook.Close();
            }
            catch (Exception ex)
            {
                WriteLog("Error Báo cáo chi tiết ngày: " + ex.Message);
            }
        } // 1
        private void LuongSPTongHopNgay(int iID_DV, string path)
        {
            try
            {
                if (System.IO.File.Exists(path + @"\LSPTongHopNgay_" + (iID_DV == 1 ? "DM1" : "DM2") + "_" + datDNgay.DateTime.ToString("yyyyMMdd") + ".xlsx")) return;

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 10;
                int fontSizeNoiDung = 10;

                TaoTTChung(oSheet, 1, 2, 1, 7, 0, 0);
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptLuongSPTongHopNgay", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = iID_DV;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = datDNgay.DateTime;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();
                if (dtBCThang.Rows.Count == 0)
                {
                    WriteLog("Error Báo cáo lương sẩn phẩm tổng hợp ngày: Không có dữ liệu in");
                    return;
                }

                int lastColumn = 0;
                lastColumn = dtBCThang.Columns.Count;


                Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[5, 1], oSheet.Cells[5, lastColumn]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BẢNG THỐNG KÊ TIỀN LƯƠNG SẢN PHẨM NGÀY " + datDNgay.DateTime.ToString("dd/MM/yyyy") + "";

                Range row4_TieuDe_Format = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, lastColumn]];
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(255, 242, 204);

                Range row4_TieuDe_TTNV = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, 1]];
                row4_TieuDe_TTNV.Value2 = "STT";
                row4_TieuDe_TTNV.ColumnWidth = 10;

                Range row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 2], oSheet.Cells[7, 2]];
                row4_TieuDe_TTC.Value2 = "Mã nhân viên";
                row4_TieuDe_TTC.ColumnWidth = 11;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 3], oSheet.Cells[7, 3]];
                row4_TieuDe_TTC.Value2 = "Họ tên";
                row4_TieuDe_TTC.ColumnWidth = 20;


                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 4], oSheet.Cells[7, 4]];
                row4_TieuDe_TTC.Value2 = "Bộ phận";
                row4_TieuDe_TTC.ColumnWidth = 35;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 5], oSheet.Cells[7, 5]];
                row4_TieuDe_TTC.Value2 = "Tiền lương sản phẩm";
                row4_TieuDe_TTC.ColumnWidth = 15;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 6], oSheet.Cells[7, 6]];
                row4_TieuDe_TTC.Value2 = "Số giờ thực tế";
                row4_TieuDe_TTC.ColumnWidth = 15;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 7], oSheet.Cells[7, 7]];
                row4_TieuDe_TTC.Value2 = "Lương SP bình quân 1 giờ";
                row4_TieuDe_TTC.ColumnWidth = 15;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 8], oSheet.Cells[7, 8]];
                row4_TieuDe_TTC.Value2 = "Lương ngày theo giờ HC";
                row4_TieuDe_TTC.ColumnWidth = 15;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 9], oSheet.Cells[7, 9]];
                row4_TieuDe_TTC.Value2 = "Summary theo ngày";
                row4_TieuDe_TTC.ColumnWidth = 15;

                DataRow[] dr = dtBCThang.Select();
                string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];

                int rowCnt = 0;
                foreach (DataRow row in dr)
                {
                    for (int col_bd = 0; col_bd < dtBCThang.Columns.Count; col_bd++)
                    {
                        rowData[rowCnt, col_bd] = row[col_bd].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 7;
                oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[rowCnt, lastColumn]].Value2 = rowData;

                rowCnt++;
                Microsoft.Office.Interop.Excel.Range formatRange;
                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 3]];
                formatRange.Merge();
                formatRange.Value2 = "Tổng";
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 10;


                formatRange = oSheet.Range[oSheet.Cells[8, 5], oSheet.Cells[rowCnt, 5]];
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }

                formatRange = oSheet.Range[oSheet.Cells[8, 6], oSheet.Cells[rowCnt, 6]];
                formatRange.NumberFormat = "#,##0.0;(#,##0.0); ; ";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }

                formatRange = oSheet.Range[oSheet.Cells[8, 7], oSheet.Cells[rowCnt, 7]];
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }

                formatRange = oSheet.Range[oSheet.Cells[8, 8], oSheet.Cells[rowCnt, 8]];
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }

                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[1].Copy();
                string sLCBNgay = dtBCThang.Rows[0][0].ToString();
                string sForMatLCB = dtBCThang.Rows[0][1].ToString();
                sForMatLCB = sForMatLCB.Replace(',', '.');

                for (int i = 0; i < rowCnt - 8; i++)
                {
                    formatRange = oSheet.Range[oSheet.Cells[(i + 8), 7], oSheet.Cells[(i + 8), 7]];
                    formatRange.Value = "=IFERROR(" + CellAddress(oSheet, (i + 8), 5) + "/" + CellAddress(oSheet, (i + 8), 6) + ",0)";

                    formatRange = oSheet.Range[oSheet.Cells[(i + 8), 8], oSheet.Cells[(i + 8), 8]];
                    formatRange.Value = "=" + CellAddress(oSheet, (i + 8), 7) + " * 9.6";

                    formatRange = oSheet.Range[oSheet.Cells[(i + 8), 9], oSheet.Cells[(i + 8), 9]];
                    formatRange.Value = "=+IF(" + CellAddress(oSheet, (i + 8), 8) + "<" + sLCBNgay + @","" < " + sForMatLCB + @""",IF(" + CellAddress(oSheet, (i + 8), 8) + ">=" + sLCBNgay + @","" >= " + sForMatLCB + @"""))";
                }


                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 5], oSheet.Cells[rowCnt, 5]];
                formatRange.Value2 = "=SUBTOTAL(9," + CellAddress(oSheet, 8, 5) + ":" + CellAddress(oSheet, rowCnt - 1, 5) + ")";
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 10;


                formatRange = oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[rowCnt, lastColumn]];
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;


                BorderAround(oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[rowCnt, lastColumn]]);

                oSheet = (Excel.Worksheet)oWB.ActiveSheet;
                oSheet = oWB.Worksheets.Add(After: oWB.Sheets[oWB.Sheets.Count]);
                oSheet.Name = "Tổng hợp";


                formatRange = oSheet.Range[oSheet.Cells[1, 1], oSheet.Cells[1, 8]];
                formatRange.Merge();
                formatRange.Value2 = "BẢNG TỔNG HỢP LƯƠNG SẢN PHẨM THEO TỔ SX NGÀY " + datDNgay.DateTime.ToString("dd/MM/yyyy");
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 16;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                formatRange = oSheet.Range[oSheet.Cells[3, 1], oSheet.Cells[3, 8]];
                formatRange.Font.Size = fontSizeTieuDe;
                formatRange.Font.Name = fontName;
                formatRange.Font.Bold = true;
                formatRange.WrapText = true;
                formatRange.NumberFormat = "@";
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRange.Interior.Color = Color.FromArgb(255, 242, 204);

                formatRange = oSheet.Range[oSheet.Cells[3, 1], oSheet.Cells[3, 1]];
                formatRange.Value = "STT";
                formatRange.ColumnWidth = 5;

                formatRange = oSheet.Range[oSheet.Cells[3, 2], oSheet.Cells[3, 2]];
                formatRange.Value = "Bộ phận";
                formatRange.ColumnWidth = 35;

                formatRange = oSheet.Range[oSheet.Cells[3, 3], oSheet.Cells[3, 3]];
                formatRange.Value = "Số LĐ";
                formatRange.ColumnWidth = 15;

                formatRange = oSheet.Range[oSheet.Cells[3, 4], oSheet.Cells[3, 4]];
                formatRange.Value = "Số LĐTT có mặt";
                formatRange.ColumnWidth = 15;

                formatRange = oSheet.Range[oSheet.Cells[3, 5], oSheet.Cells[3, 5]];
                formatRange.Value = "Số LĐ có mặt";
                formatRange.ColumnWidth = 15;

                formatRange = oSheet.Range[oSheet.Cells[3, 6], oSheet.Cells[3, 6]];
                formatRange.Value = "Tổng tiền lương";
                formatRange.ColumnWidth = 15;

                formatRange = oSheet.Range[oSheet.Cells[3, 7], oSheet.Cells[3, 7]];
                formatRange.Value = "Lương SP BQ / người";
                formatRange.ColumnWidth = 15;

                formatRange = oSheet.Range[oSheet.Cells[3, 8], oSheet.Cells[3, 8]];
                formatRange.Value = "Ghi chú";
                formatRange.ColumnWidth = 15;



                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[2].Copy();
                lastColumn = dtBCThang.Columns.Count;
                dr = dtBCThang.Select();
                rowData = new string[dr.Count(), dtBCThang.Columns.Count];

                rowCnt = 0;
                foreach (DataRow row in dr)
                {
                    for (int col_bd = 0; col_bd < dtBCThang.Columns.Count; col_bd++)
                    {
                        rowData[rowCnt, col_bd] = row[col_bd].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 3;
                oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[rowCnt, lastColumn]].Value2 = rowData;


                formatRange = oSheet.Range[oSheet.Cells[4, 3], oSheet.Cells[rowCnt, 3]];
                formatRange.NumberFormat = "#,##0.0;(#,##0.0); ; ";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }

                formatRange = oSheet.Range[oSheet.Cells[4, 4], oSheet.Cells[rowCnt, 4]];
                formatRange.NumberFormat = "#,##0.0;(#,##0.0); ; ";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }


                formatRange = oSheet.Range[oSheet.Cells[4, 5], oSheet.Cells[rowCnt, 5]];
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }

                formatRange = oSheet.Range[oSheet.Cells[4, 6], oSheet.Cells[rowCnt, 6]];
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }

                formatRange = oSheet.Range[oSheet.Cells[4, 7], oSheet.Cells[rowCnt, 7]];
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }


                for (int i = 0; i < rowCnt - 3; i++)
                {
                    formatRange = oSheet.Range[oSheet.Cells[(i + 4), 7], oSheet.Cells[(i + 4), 7]];
                    formatRange.Value = "=IFERROR(" + CellAddress(oSheet, (i + 4), 6) + "/" + CellAddress(oSheet, (i + 4), 5) + ",0)";
                }

                rowCnt++;
                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 2]];
                formatRange.Merge();
                formatRange.Value = "Tổng";
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.Font.Bold = true;


                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 3], oSheet.Cells[rowCnt, 3]];
                formatRange.Value = "=SUBTOTAL(9," + CellAddress(oSheet, 4, 3) + ":" + CellAddress(oSheet, rowCnt - 1, 3) + ")";
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.Font.Bold = true;
                formatRange.NumberFormat = "#,##0.0;(#,##0.0); ; ";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }

                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 4], oSheet.Cells[rowCnt, 4]];
                formatRange.Value = "=SUBTOTAL(9," + CellAddress(oSheet, 4, 4) + ":" + CellAddress(oSheet, rowCnt - 1, 4) + ")";
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.Font.Bold = true;
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }

                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 5], oSheet.Cells[rowCnt, 5]];
                formatRange.Value = "=SUBTOTAL(9," + CellAddress(oSheet, 4, 5) + ":" + CellAddress(oSheet, rowCnt - 1, 5) + ")";
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.Font.Bold = true;
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }

                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 6], oSheet.Cells[rowCnt, 6]];
                formatRange.Value = "=SUBTOTAL(9," + CellAddress(oSheet, 4, 6) + ":" + CellAddress(oSheet, rowCnt - 1, 6) + ")";
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.Font.Bold = true;
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }

                formatRange = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[rowCnt, lastColumn]];
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;

                for (int i = 1; i < 5; i++)
                {
                    if (i != 2)
                    {
                        formatRange = oSheet.Range[oSheet.Cells[4, i], oSheet.Cells[rowCnt, i]];
                        formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    }
                }

                BorderAround(oSheet.Range[oSheet.Cells[3, 1], oSheet.Cells[rowCnt, lastColumn]]);

                oWB.Sheets[1].Activate();

                // filter

                oWB.SaveAs(path + @"\LSPTongHopNgay_" + (iID_DV == 1 ? "DM1" : "DM2") + "_" + datDNgay.DateTime.ToString("yyyyMMdd") + ".xlsx",
                   AccessMode: Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive);
                oWB.Close();
            }
            catch (Exception ex)
            {
                WriteLog("Error Báo cáo lương sản phẩm tổng hợp ngày: " + ex.Message);
            }
        } // 2
        private void LuongSPTongHopThang(int iID_DV, string path, DateTime dNgayDauThang, DateTime dNgayCuoiThang)
        {
            try
            {

                if (System.IO.File.Exists(path + @"\LSPTongHopThang_" + (iID_DV == 1 ? "DM1" : "DM2") + "_" + datDNgay.DateTime.ToString("yyyyMMdd") + ".xlsx")) return;

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                DateTime TuNgay = dNgayDauThang;
                DateTime DenNgay = dNgayCuoiThang;

                int soNgay = DenNgay.Day - TuNgay.Day;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptLuongSPTongHopThang", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = iID_DV;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dNgayDauThang;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dNgayCuoiThang;
                cmd.Parameters.Add("@bNgoaiChuyen", SqlDbType.Bit).Value = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();
                if (dtBCThang.Rows.Count == 0)
                {
                    WriteLog("Error Báo cáo lương sẩn phẩm tổng hợp tháng: Không có dữ liệu in");
                    return;
                }

                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 10;
                int fontSizeNoiDung = 10;

                int lastColumn = 0;
                lastColumn = dtBCThang.Columns.Count;
                int ngayCongChuanThang = 1;
                try
                {
                    DateTime NgayDauThang = new DateTime(datTNgay.DateTime.Year, datTNgay.DateTime.Month, 1);
                    DateTime NgayCuoiThang = NgayDauThang.AddMonths(1).AddDays(-1);
                    ngayCongChuanThang = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.fnGetSoNgayCongQuiDinhThang('" + NgayDauThang.ToString("MM/dd/yyyy") + "','" + NgayCuoiThang.ToString("MM/dd/yyyy") + "')"));
                }
                catch { }

                TaoTTChung(oSheet, 1, 2, 1, 7, 0, 0);

                Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[5, 1], oSheet.Cells[5, 7]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BẢNG THỐNG KÊ TIỀN LƯƠNG SẢN PHẨM THÁNG " + Convert.ToDateTime(DateTime.Now).ToString("MM/yyyy") + "";

                row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[6, 7]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "Từ ngày " + Convert.ToDateTime(dNgayDauThang).ToString("dd/MM/yyyy") + " đến ngày " + Convert.ToDateTime(dNgayCuoiThang).ToString("dd/MM/yyyy");

                Range row4_TieuDe_Format = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, lastColumn]];
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(221, 235, 247);

                Range row4_TieuDe_TTNV = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, 1]];
                row4_TieuDe_TTNV.Value2 = "STT";
                row4_TieuDe_TTNV.ColumnWidth = 10;

                Range row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 2], oSheet.Cells[7, 2]];
                row4_TieuDe_TTC.Value2 = "Mã nhân viên";
                row4_TieuDe_TTC.ColumnWidth = 11;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 3], oSheet.Cells[7, 3]];
                row4_TieuDe_TTC.Value2 = "Họ tên";
                row4_TieuDe_TTC.ColumnWidth = 20;


                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 4], oSheet.Cells[7, 4]];
                row4_TieuDe_TTC.Value2 = "Bộ phận";
                row4_TieuDe_TTC.ColumnWidth = 35;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 5], oSheet.Cells[7, 5]];
                row4_TieuDe_TTC.Value2 = "Tình trạng nhân sự";
                row4_TieuDe_TTC.ColumnWidth = 15;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 6], oSheet.Cells[7, 6]];
                row4_TieuDe_TTC.Value2 = "Ngày vào";
                row4_TieuDe_TTC.ColumnWidth = 15;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 7], oSheet.Cells[7, 7]];
                row4_TieuDe_TTC.Value2 = "Thâm niên Tính lương (tháng)";
                row4_TieuDe_TTC.ColumnWidth = 15;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 8], oSheet.Cells[7, 8]];
                row4_TieuDe_TTC.Value2 = "Phân loại thâm niên";
                row4_TieuDe_TTC.ColumnWidth = 15;

                int iCot = 9;
                while (TuNgay <= DenNgay)
                {

                    row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                    row4_TieuDe_TTC.Value2 = TuNgay.ToString("dd/MM/yyyy");
                    row4_TieuDe_TTC.ColumnWidth = 15;

                    if (TuNgay.DayOfWeek.ToString() == "Sunday" || TuNgay.DayOfWeek.ToString() == "Saturday")
                    {
                        row4_TieuDe_TTC.Interior.Color = Color.FromArgb(255, 255, 0);
                    }
                    TuNgay = TuNgay.AddDays(1);
                    iCot++;
                }

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Lương sản phẩm ngày thường";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Lương SP ngày T7,CN";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Tổng kê tháng";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "TG HC";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "OT 150%";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "OT 200%";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "OT 300%";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Lương BQ 1h";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Lương SP HC";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Lương SP HC dự tính tháng";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Summarry (Tổng lương SP gốc)";
                row4_TieuDe_TTC.ColumnWidth = 25;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Summarry (Summarry (Tổng lương SP HC dự tính Tháng " + DateTime.Now.Month.ToString() + "-" + ngayCongChuanThang + " ngày))";
                row4_TieuDe_TTC.ColumnWidth = 25;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Lương SP HC BQ/ngày";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Ghi chú";
                row4_TieuDe_TTC.ColumnWidth = 25;

                oSheet.Application.ActiveWindow.SplitColumn = 6;
                oSheet.Application.ActiveWindow.SplitRow = 7;
                oSheet.Application.ActiveWindow.FreezePanes = true;


                int rowCnt = 7;
                int stt = 0;

                int iCotSauNgay = 8 + soNgay + 1;

                TuNgay = TuNgay = dNgayDauThang;
                foreach (DataRow row2 in dtBCThang.Rows)
                {
                    stt++;
                    rowCnt++;

                    dynamic[] arr = {row2["STT"].ToString(), row2["MS_CN"].ToString(), row2["HO_TEN"].ToString(), row2["TEN_TO"].ToString(),
                        row2["TINH_TRANG_HT"].ToString(), row2["NGAY_VAO_LAM"].ToString(), row2["THAM_NIEN"].ToString(), row2["PHAN_LOAI_TN"].ToString()
                    };
                    while (TuNgay <= DenNgay)
                    {
                        arr = arr.Append(row2["NGAY_" + TuNgay.Day.ToString() + ""].ToString()).ToArray();
                        TuNgay = TuNgay.AddDays(1);
                    }
                    //for (int i = 1; i <= soNgay + 1; i++)
                    //{
                    //    //arr[i + (arr.Length)] = row2["NGAY_" + i + ""].ToString();
                    //    arr = arr.Append(row2["NGAY_" + i + ""].ToString()).ToArray();
                    //}

                    arr = arr.Append(row2["LUONG_SP_NGAY_THUONG"].ToString()).ToArray();
                    arr = arr.Append(row2["LUONG_SP_OT"].ToString()).ToArray();
                    arr = arr.Append("=" + CellAddress(oSheet, rowCnt, iCotSauNgay + 1) + "+" + CellAddress(oSheet, rowCnt, iCotSauNgay + 2) + "").ToArray(); // AN + AO
                    arr = arr.Append(row2["TG_HC"].ToString()).ToArray();
                    arr = arr.Append(row2["OT_150"].ToString()).ToArray();
                    arr = arr.Append(row2["OT_200"].ToString()).ToArray();
                    arr = arr.Append(row2["OT_300"].ToString()).ToArray();
                    arr = arr.Append("=IFERROR(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + "/(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 4) + "+" + CellAddress(oSheet, rowCnt, iCotSauNgay + 5) + "+" + CellAddress(oSheet, rowCnt, iCotSauNgay + 6) + "+" + CellAddress(oSheet, rowCnt, iCotSauNgay + 7) + "),0)").ToArray();
                    arr = arr.Append("=IFERROR(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 4) + "*" + CellAddress(oSheet, rowCnt, iCotSauNgay + 8) + ",0)").ToArray();
                    arr = arr.Append("=IFERROR(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 8) + "*9.6*" + ngayCongChuanThang + ",0)").ToArray();
                    arr = arr.Append(@"=+IF(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @"<500000,"" < 500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @">=500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @"<=1000000),""500.000 -<= 1000.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + ">100000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @"<=1500000),"" > 1.000.000 -<= 1.500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + ">1500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @"<=2000000),"" > 1.500.000 -<= 2.000.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + ">=2000000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @"<=2500000),""2.000.000 -<= 2.500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + ">=2500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @"<=3000000),""2.500.000 -<= 3.000.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + ">=3000000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @"<=3500000),""3.000.000 -<= 3.500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + ">=3500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @"<4100000),""3.500.000 -< 4.100.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @">=4100000),"" >= 4.100.000"",0)))))))))").ToArray();
                    arr = arr.Append(@"=+IF(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<500000,"" < 500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + ">=500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<=1000000),""500.000 -<= 1000.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + ">100000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<=1500000),"" > 1.000.000 -<= 1.500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + ">1500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<=2000000),"" > 1.500.000 -<= 2.000.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + ">=2000000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<=2500000),""2.000.000 -<= 2.500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + ">=2500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<=3000000),""2.500.000 -<= 3.000.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + ">=3000000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<=3500000),""3.000.000 -<= 3.500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + ">=3500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<4100000),""3.500.000 -< 4.100.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @">=4100000),"" >= 4.100.000"",0)))))))))").ToArray();
                    arr = arr.Append("=" + CellAddress(oSheet, rowCnt, iCotSauNgay + 8) + "*9.6").ToArray();
                    arr = arr.Append(row2["GHI_CHU"].ToString()).ToArray();

                    //string s = @"=+IF("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<500000,"" < 500.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">=500000,"+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<=1000000),""500.000 -<= 1000.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">100000,"+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<=1500000),"" > 1.000.000 -<= 1.500.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">1500000,"+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<=2000000),"" > 1.500.000 -<= 2.000.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">=2000000,"+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<=2500000),""2.000.000 -<= 2.500.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">=2500000,"+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<=3000000),""2.500.000 -<= 3.000.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">=3000000,"+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<=3500000),""3.000.000 -<= 3.500.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">=3500000,"+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<=4100000),""3.500.000 -<= 4.100.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">=4100000),"" >= 4.100.000"",0)))))))))";

                    Range rowData = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, lastColumn]];
                    rowData.Font.Size = fontSizeNoiDung;
                    rowData.Font.Name = fontName;
                    rowData.Value2 = arr;

                    TuNgay = dNgayDauThang;
                }

                rowCnt++;

                for (int colSUM = 9; colSUM < dtBCThang.Columns.Count; colSUM++)
                {
                    //ws.Cells[row, colSUM] = "=SUBTOTAL(9,N10:N" + (row - 1).ToString() + ")";
                    oSheet.Cells[rowCnt, colSUM] = "=SUBTOTAL(9," + CellAddress(oSheet, 8, colSUM) + ":" + CellAddress(oSheet, rowCnt - 1, colSUM) + ")";
                    oSheet.Cells[rowCnt, colSUM].Font.Bold = true;
                }

                Microsoft.Office.Interop.Excel.Range formatRange;
                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 3]];
                formatRange.Merge();
                formatRange.Value2 = "Tổng";
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 10;

                for (int i = 1; i <= soNgay + 4; i++) //cộng thêm 3 cột sau ngày
                {
                    formatRange = oSheet.Range[oSheet.Cells[8, i + 8], oSheet.Cells[rowCnt, i + 8]];
                    formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    try
                    {
                        formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    }
                    catch { }
                }


                for (int i = 1; i <= 4; i++) //cộng thêm 3 cột sau ngày
                {
                    formatRange = oSheet.Range[oSheet.Cells[8, (iCotSauNgay + 3) + i], oSheet.Cells[rowCnt, (iCotSauNgay + 3) + i]];
                    formatRange.NumberFormat = "#,##0.00;(#,##0.0); ; ";

                    try
                    {
                        formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    }
                    catch { }

                }

                for (int i = 1; i <= 3; i++) //cộng thêm 3 cột sau ngày
                {
                    formatRange = oSheet.Range[oSheet.Cells[8, (iCotSauNgay + 7) + i], oSheet.Cells[rowCnt, (iCotSauNgay + 7) + i]];
                    formatRange.NumberFormat = "#,##0;(#,##0); ; ";

                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }

                formatRange = oSheet.Range[oSheet.Cells[8, iCotSauNgay + 13], oSheet.Cells[rowCnt, iCotSauNgay + 13]];
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";

                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }

                formatRange = oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[rowCnt - 1, 1]];
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[8, 7], oSheet.Cells[rowCnt - 1, 7]];
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[8, lastColumn - 3], oSheet.Cells[rowCnt - 1, lastColumn - 3]];
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[8, lastColumn - 2], oSheet.Cells[rowCnt - 1, lastColumn - 2]];
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[rowCnt, lastColumn]];
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;

                BorderAround(oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[rowCnt, lastColumn]]);

                Commons.Modules.MExcel.ThemDong(oSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 2, 7);

                oSheet = (Excel.Worksheet)oWB.ActiveSheet;
                oSheet = oWB.Worksheets.Add(After: oWB.Sheets[oWB.Sheets.Count]);



                dtBCThang = new DataTable();

                TuNgay = dNgayDauThang;
                DenNgay = dNgayCuoiThang;

                soNgay = DenNgay.Day - TuNgay.Day;

                cmd = new System.Data.SqlClient.SqlCommand("rptLuongSPTongHopThang", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = iID_DV;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dNgayDauThang;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dNgayCuoiThang;
                cmd.Parameters.Add("@bNgoaiChuyen", SqlDbType.Bit).Value = 1;
                cmd.CommandType = CommandType.StoredProcedure;
                adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();

                lastColumn = dtBCThang.Columns.Count;
                ngayCongChuanThang = 1;
                try
                {
                    DateTime NgayDauThang = new DateTime(datTNgay.DateTime.Year, datTNgay.DateTime.Month, 1);
                    DateTime NgayCuoiThang = NgayDauThang.AddMonths(1).AddDays(-1);
                    ngayCongChuanThang = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.fnGetSoNgayCongQuiDinhThang('" + NgayDauThang.ToString("MM/dd/yyyy") + "','" + NgayCuoiThang.ToString("MM/dd/yyyy") + "')"));
                }
                catch { }

                TaoTTChung(oSheet, 1, 2, 1, 7, 0, 0);

                row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[5, 1], oSheet.Cells[5, 7]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BẢNG THỐNG KÊ TIỀN LƯƠNG SẢN PHẨM THÁNG " + Convert.ToDateTime(DateTime.Now).ToString("MM/yyyy") + "";

                row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[6, 7]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "Từ ngày " + Convert.ToDateTime(dNgayDauThang).ToString("dd/MM/yyyy") + " đến ngày " + Convert.ToDateTime(dNgayCuoiThang).ToString("dd/MM/yyyy");

                row4_TieuDe_Format = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, lastColumn]];
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(221, 235, 247);

                row4_TieuDe_TTNV = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, 1]];
                row4_TieuDe_TTNV.Value2 = "STT";
                row4_TieuDe_TTNV.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 2], oSheet.Cells[7, 2]];
                row4_TieuDe_TTC.Value2 = "Mã nhân viên";
                row4_TieuDe_TTC.ColumnWidth = 11;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 3], oSheet.Cells[7, 3]];
                row4_TieuDe_TTC.Value2 = "Họ tên";
                row4_TieuDe_TTC.ColumnWidth = 20;


                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 4], oSheet.Cells[7, 4]];
                row4_TieuDe_TTC.Value2 = "Bộ phận";
                row4_TieuDe_TTC.ColumnWidth = 35;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 5], oSheet.Cells[7, 5]];
                row4_TieuDe_TTC.Value2 = "Tình trạng nhân sự";
                row4_TieuDe_TTC.ColumnWidth = 15;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 6], oSheet.Cells[7, 6]];
                row4_TieuDe_TTC.Value2 = "Ngày vào";
                row4_TieuDe_TTC.ColumnWidth = 15;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 7], oSheet.Cells[7, 7]];
                row4_TieuDe_TTC.Value2 = "Thâm niên Tính lương (tháng)";
                row4_TieuDe_TTC.ColumnWidth = 15;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 8], oSheet.Cells[7, 8]];
                row4_TieuDe_TTC.Value2 = "Phân loại thâm niên";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot = 9;
                while (TuNgay <= DenNgay)
                {

                    row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                    row4_TieuDe_TTC.Value2 = TuNgay.ToString("dd/MM/yyyy");
                    row4_TieuDe_TTC.ColumnWidth = 15;

                    if (TuNgay.DayOfWeek.ToString() == "Sunday" || TuNgay.DayOfWeek.ToString() == "Saturday")
                    {
                        row4_TieuDe_TTC.Interior.Color = Color.FromArgb(255, 255, 0);
                    }
                    TuNgay = TuNgay.AddDays(1);
                    iCot++;
                }

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Lương sản phẩm ngày thường";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Lương SP ngày T7,CN";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Tổng kê tháng";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "TG HC";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "OT 150%";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "OT 200%";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "OT 300%";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Lương BQ 1h";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Lương SP HC";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Lương SP HC dự tính tháng";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Summarry (Tổng lương SP gốc)";
                row4_TieuDe_TTC.ColumnWidth = 25;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Summarry (Summarry (Tổng lương SP HC dự tính Tháng " + DateTime.Now.Month.ToString() + "-" + ngayCongChuanThang + " ngày))";
                row4_TieuDe_TTC.ColumnWidth = 25;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Lương SP HC BQ/ngày";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Ghi chú";
                row4_TieuDe_TTC.ColumnWidth = 25;

                oSheet.Application.ActiveWindow.SplitColumn = 6;
                oSheet.Application.ActiveWindow.SplitRow = 7;
                oSheet.Application.ActiveWindow.FreezePanes = true;


                rowCnt = 7;
                stt = 0;

                iCotSauNgay = 8 + soNgay + 1;

                TuNgay = TuNgay = dNgayDauThang;
                foreach (DataRow row2 in dtBCThang.Rows)
                {
                    stt++;
                    rowCnt++;

                    dynamic[] arr = {row2["STT"].ToString(), row2["MS_CN"].ToString(), row2["HO_TEN"].ToString(), row2["TEN_TO"].ToString(),
                        row2["TINH_TRANG_HT"].ToString(), row2["NGAY_VAO_LAM"].ToString(), row2["THAM_NIEN"].ToString(), row2["PHAN_LOAI_TN"].ToString()
                    };
                    while (TuNgay <= DenNgay)
                    {
                        arr = arr.Append(row2["NGAY_" + TuNgay.Day.ToString() + ""].ToString()).ToArray();
                        TuNgay = TuNgay.AddDays(1);
                    }
                    //for (int i = 1; i <= soNgay + 1; i++)
                    //{
                    //    //arr[i + (arr.Length)] = row2["NGAY_" + i + ""].ToString();
                    //    arr = arr.Append(row2["NGAY_" + i + ""].ToString()).ToArray();
                    //}

                    arr = arr.Append(row2["LUONG_SP_NGAY_THUONG"].ToString()).ToArray();
                    arr = arr.Append(row2["LUONG_SP_OT"].ToString()).ToArray();
                    arr = arr.Append("=" + CellAddress(oSheet, rowCnt, iCotSauNgay + 1) + "+" + CellAddress(oSheet, rowCnt, iCotSauNgay + 2) + "").ToArray(); // AN + AO
                    arr = arr.Append(row2["TG_HC"].ToString()).ToArray();
                    arr = arr.Append(row2["OT_150"].ToString()).ToArray();
                    arr = arr.Append(row2["OT_200"].ToString()).ToArray();
                    arr = arr.Append(row2["OT_300"].ToString()).ToArray();
                    arr = arr.Append("=IFERROR(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + "/(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 4) + "+" + CellAddress(oSheet, rowCnt, iCotSauNgay + 5) + "+" + CellAddress(oSheet, rowCnt, iCotSauNgay + 6) + "+" + CellAddress(oSheet, rowCnt, iCotSauNgay + 7) + "),0)").ToArray();
                    arr = arr.Append("=IFERROR(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 4) + "*" + CellAddress(oSheet, rowCnt, iCotSauNgay + 8) + ",0)").ToArray();
                    arr = arr.Append("=IFERROR(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 8) + "*9.6*" + ngayCongChuanThang + ",0)").ToArray();
                    arr = arr.Append(@"=+IF(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @"<500000,"" < 500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @">=500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @"<=1000000),""500.000 -<= 1000.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + ">100000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @"<=1500000),"" > 1.000.000 -<= 1.500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + ">1500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @"<=2000000),"" > 1.500.000 -<= 2.000.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + ">=2000000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @"<=2500000),""2.000.000 -<= 2.500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + ">=2500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @"<=3000000),""2.500.000 -<= 3.000.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + ">=3000000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @"<=3500000),""3.000.000 -<= 3.500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + ">=3500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @"<4100000),""3.500.000 -< 4.100.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @">=4100000),"" >= 4.100.000"",0)))))))))").ToArray();
                    arr = arr.Append(@"=+IF(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<500000,"" < 500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + ">=500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<=1000000),""500.000 -<= 1000.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + ">100000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<=1500000),"" > 1.000.000 -<= 1.500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + ">1500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<=2000000),"" > 1.500.000 -<= 2.000.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + ">=2000000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<=2500000),""2.000.000 -<= 2.500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + ">=2500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<=3000000),""2.500.000 -<= 3.000.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + ">=3000000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<=3500000),""3.000.000 -<= 3.500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + ">=3500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<4100000),""3.500.000 -< 4.100.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @">=4100000),"" >= 4.100.000"",0)))))))))").ToArray();
                    arr = arr.Append("=" + CellAddress(oSheet, rowCnt, iCotSauNgay + 8) + "*9.6").ToArray();
                    arr = arr.Append(row2["GHI_CHU"].ToString()).ToArray();

                    //string s = @"=+IF("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<500000,"" < 500.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">=500000,"+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<=1000000),""500.000 -<= 1000.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">100000,"+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<=1500000),"" > 1.000.000 -<= 1.500.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">1500000,"+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<=2000000),"" > 1.500.000 -<= 2.000.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">=2000000,"+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<=2500000),""2.000.000 -<= 2.500.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">=2500000,"+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<=3000000),""2.500.000 -<= 3.000.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">=3000000,"+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<=3500000),""3.000.000 -<= 3.500.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">=3500000,"+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<=4100000),""3.500.000 -<= 4.100.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">=4100000),"" >= 4.100.000"",0)))))))))";

                    Range rowData = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, lastColumn]];
                    rowData.Font.Size = fontSizeNoiDung;
                    rowData.Font.Name = fontName;
                    rowData.Value2 = arr;

                    TuNgay = dNgayDauThang;
                }

                rowCnt++;

                for (int colSUM = 9; colSUM < dtBCThang.Columns.Count; colSUM++)
                {
                    //ws.Cells[row, colSUM] = "=SUBTOTAL(9,N10:N" + (row - 1).ToString() + ")";
                    oSheet.Cells[rowCnt, colSUM] = "=SUBTOTAL(9," + CellAddress(oSheet, 8, colSUM) + ":" + CellAddress(oSheet, rowCnt - 1, colSUM) + ")";
                    oSheet.Cells[rowCnt, colSUM].Font.Bold = true;
                }

                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 3]];
                formatRange.Merge();
                formatRange.Value2 = "Tổng";
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 10;

                for (int i = 1; i <= soNgay + 4; i++) //cộng thêm 3 cột sau ngày
                {
                    formatRange = oSheet.Range[oSheet.Cells[8, i + 8], oSheet.Cells[rowCnt, i + 8]];
                    formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    try
                    {
                        formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    }
                    catch { }
                }


                for (int i = 1; i <= 4; i++) //cộng thêm 3 cột sau ngày
                {
                    formatRange = oSheet.Range[oSheet.Cells[8, (iCotSauNgay + 3) + i], oSheet.Cells[rowCnt, (iCotSauNgay + 3) + i]];
                    formatRange.NumberFormat = "#,##0.00;(#,##0.0); ; ";

                    try
                    {
                        formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    }
                    catch { }

                }

                for (int i = 1; i <= 3; i++) //cộng thêm 3 cột sau ngày
                {
                    formatRange = oSheet.Range[oSheet.Cells[8, (iCotSauNgay + 7) + i], oSheet.Cells[rowCnt, (iCotSauNgay + 7) + i]];
                    formatRange.NumberFormat = "#,##0;(#,##0); ; ";

                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }

                formatRange = oSheet.Range[oSheet.Cells[8, iCotSauNgay + 13], oSheet.Cells[rowCnt, iCotSauNgay + 13]];
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";

                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }

                formatRange = oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[rowCnt - 1, 1]];
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[8, 7], oSheet.Cells[rowCnt - 1, 7]];
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[8, lastColumn - 3], oSheet.Cells[rowCnt - 1, lastColumn - 3]];
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[8, lastColumn - 2], oSheet.Cells[rowCnt - 1, lastColumn - 2]];
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[rowCnt, lastColumn]];
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;

                BorderAround(oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[rowCnt, lastColumn]]);

                Commons.Modules.MExcel.ThemDong(oSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 2, 7);
                oWB.Sheets[1].Activate();

                // filter
                oWB.SaveAs(path + @"\LSPTongHopThang_" + (iID_DV == 1 ? "DM1" : "DM2") + "_" + datDNgay.DateTime.ToString("yyyyMMdd") + ".xlsx",
                  AccessMode: Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive);
                oWB.Close();
            }
            catch (Exception ex)
            {
                WriteLog("Error Báo cáo lương sản phẩm tổng hợp tháng: " + ex.Message);
            }
        } // 3
        private void LuongSPTheoTungCongDoan(int iID_DV, string path, DateTime dNgayDauThang, DateTime dNgayCuoiThang)
        {
            try
            {
                if (System.IO.File.Exists(path + @"\LSPTheoCongDoan_" + (iID_DV == 1 ? "DM1" : "DM2") + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx")) return;

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtTTChung;
                DataTable dtChuyen;
                DataTable dtBCLSP;

                dtTTChung = new DataTable();
                dtTTChung = Commons.Modules.ObjSystems.DataThongTinChung();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetLSPChuyen", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = iID_DV;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dNgayDauThang;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dNgayCuoiThang;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtChuyen = new DataTable();
                dtChuyen = ds.Tables[0].Copy();
                if (dtChuyen.Rows.Count == 0)
                {
                    WriteLog("Error Bảng tổng hợp lương sản phẩm theo công đoạn: Không có dữ liệu in");
                    return;
                }
                Microsoft.Office.Interop.Excel.Application oApp;
                Microsoft.Office.Interop.Excel.Workbook oBook;
                Microsoft.Office.Interop.Excel.Worksheet oSheet;
                oApp = new Microsoft.Office.Interop.Excel.Application();
                oApp.Visible = false;

                oBook = oApp.Workbooks.Add();
                oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oBook.Worksheets.get_Item(1);

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 11;
                int oRow = 1;

                foreach (DataRow rowC in dtChuyen.Rows)
                {

                    oSheet.Name = rowC[1].ToString();

                    if (oRow == 1)
                    {
                        Microsoft.Office.Interop.Excel.Range row1_ThongTinCty = oSheet.get_Range("A1", "H1");
                        row1_ThongTinCty.Merge();
                        row1_ThongTinCty.Font.Size = fontSizeNoiDung;
                        row1_ThongTinCty.Font.Name = fontName;
                        row1_ThongTinCty.Font.Bold = true;
                        row1_ThongTinCty.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        row1_ThongTinCty.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row1_ThongTinCty.Value2 = dtTTChung.Rows[0][0];

                        Microsoft.Office.Interop.Excel.Range row2_ThongTinCty = oSheet.get_Range("A2", "H2");
                        row2_ThongTinCty.Merge();
                        row2_ThongTinCty.Font.Size = fontSizeNoiDung;
                        row2_ThongTinCty.Font.Name = fontName;
                        row2_ThongTinCty.Font.Bold = true;
                        row2_ThongTinCty.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        row2_ThongTinCty.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row2_ThongTinCty.Value2 = dtTTChung.Rows[0][2];

                        Microsoft.Office.Interop.Excel.Range row4_TieuDe_BaoCao = oSheet.get_Range("A4", "M4");
                        row4_TieuDe_BaoCao.Merge();
                        row4_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                        row4_TieuDe_BaoCao.Font.Name = fontName;
                        row4_TieuDe_BaoCao.Font.Bold = true;
                        row4_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        row4_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row4_TieuDe_BaoCao.RowHeight = 30;
                        row4_TieuDe_BaoCao.Value2 = "BẢNG KÊ SẢN LƯỢNG THÁNG " + Convert.ToDateTime(DateTime.Now).ToString("MM/yyyy");

                        Microsoft.Office.Interop.Excel.Range row5_TieuDe_BaoCao = oSheet.get_Range("A5", "M5");
                        row5_TieuDe_BaoCao.Merge();
                        row5_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                        row5_TieuDe_BaoCao.Font.Name = fontName;
                        row5_TieuDe_BaoCao.Font.Bold = true;
                        row5_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        row5_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row5_TieuDe_BaoCao.RowHeight = 20;
                        row5_TieuDe_BaoCao.Value2 = "Từ ngày " + Convert.ToDateTime(dNgayDauThang).ToString("dd/MM/yyyy") + " đến ngày " + Convert.ToDateTime(dNgayCuoiThang).ToString("dd/MM/yyyy");

                        oRow = 6;
                    }

                    Microsoft.Office.Interop.Excel.Range row_Chuyen = oSheet.get_Range("G" + oRow.ToString(), "G" + oRow.ToString());
                    row_Chuyen.Merge();
                    row_Chuyen.Value2 = rowC[1].ToString();
                    row_Chuyen.Font.Size = fontSizeNoiDung;
                    row_Chuyen.Font.Name = fontName;
                    row_Chuyen.Font.Bold = true;
                    row_Chuyen.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    row_Chuyen.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row_Chuyen.RowHeight = 30;

                    row_Chuyen = oSheet.get_Range("B" + oRow.ToString(), "B" + oRow.ToString());
                    row_Chuyen.Font.Size = fontSizeNoiDung;
                    row_Chuyen.Font.Name = fontName;
                    row_Chuyen.Font.Bold = true;
                    row_Chuyen.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    row_Chuyen.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row_Chuyen.Value2 = "Tổ trưởng";
                    oRow++;

                    System.Data.SqlClient.SqlCommand cmdCT = new System.Data.SqlClient.SqlCommand("rptBangLSPTongHopMHTheoCN_DM", conn);
                    cmdCT.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                    cmdCT.Parameters.Add("@NNgu", SqlDbType.Int).Value = 0;
                    cmdCT.Parameters.Add("@DVi", SqlDbType.Int).Value = iID_DV;
                    cmdCT.Parameters.Add("@XN", SqlDbType.Int).Value = -1;
                    cmdCT.Parameters.Add("@TO", SqlDbType.Int).Value = -1;
                    cmdCT.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = rowC[0];
                    cmdCT.Parameters.Add("@TNgay", SqlDbType.Date).Value = dNgayDauThang;
                    cmdCT.Parameters.Add("@DNgay", SqlDbType.Date).Value = dNgayCuoiThang;
                    cmdCT.CommandType = CommandType.StoredProcedure;
                    System.Data.SqlClient.SqlDataAdapter adpCT = new System.Data.SqlClient.SqlDataAdapter(cmdCT);

                    DataSet dsCT = new DataSet();
                    adpCT.Fill(dsCT);
                    dtBCLSP = new DataTable();
                    dtBCLSP = dsCT.Tables[0].Copy();
                    int totalColumn = dtBCLSP.Columns.Count;
                    string lastColumn = string.Empty;
                    lastColumn = CharacterIncrement(totalColumn - 1);

                    oSheet.Cells[oRow, 1] = "Stt";
                    oSheet.Cells[oRow, 1].ColumnWidth = 6;
                    oSheet.Cells[oRow, 2] = "Mã NV";
                    oSheet.Cells[oRow, 2].ColumnWidth = 15;
                    oSheet.Cells[oRow, 3] = "Họ tên";
                    oSheet.Cells[oRow, 3].ColumnWidth = 25;
                    oSheet.Cells[oRow, 4] = "Bộ phận";
                    oSheet.Cells[oRow, 4].ColumnWidth = 15;
                    oSheet.Cells[oRow, 5] = "Mã hàng";
                    oSheet.Cells[oRow, 5].ColumnWidth = 10;
                    oSheet.Cells[oRow, 6] = "Mã công đoạn";
                    oSheet.Cells[oRow, 6].ColumnWidth = 8;
                    oSheet.Cells[oRow, 7] = "Tên công đoạn";
                    oSheet.Cells[oRow, 7].ColumnWidth = 35;
                    oSheet.Cells[oRow, 8] = "Tổng sản lượng cá nhân đã kê";
                    oSheet.Cells[oRow, 8].ColumnWidth = 10;
                    oSheet.Cells[oRow, 9] = "Sản lượng nhập";
                    oSheet.Cells[oRow, 9].ColumnWidth = 10;
                    oSheet.Cells[oRow, 10] = "Sản lượng sau điều chỉnh";
                    oSheet.Cells[oRow, 10].ColumnWidth = 10;
                    oSheet.Cells[oRow, 11] = "Tổng sản lượng công đoạn";
                    oSheet.Cells[oRow, 11].ColumnWidth = 10;
                    oSheet.Cells[oRow, 12] = "Sản lượng chốt tính lương";
                    oSheet.Cells[oRow, 12].ColumnWidth = 15;
                    oSheet.Cells[oRow, 13] = "Thừa(-)/ Thiếu(+)";
                    oSheet.Cells[oRow, 13].ColumnWidth = 10;
                    oSheet.Cells[oRow, 14] = "Đơn giá";
                    oSheet.Cells[oRow, 14].ColumnWidth = 10;
                    oSheet.Cells[oRow, 15] = "Thành tiền";
                    oSheet.Cells[oRow, 15].ColumnWidth = 15;

                    string LastTitleColumn = string.Empty;
                    LastTitleColumn = "O";
                    Microsoft.Office.Interop.Excel.Range row_TieuDe_BaoCao = oSheet.get_Range("A" + oRow.ToString(), LastTitleColumn + oRow.ToString());
                    row_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                    row_TieuDe_BaoCao.Font.Name = fontName;
                    row_TieuDe_BaoCao.Font.Bold = true;
                    row_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row_TieuDe_BaoCao.Cells.WrapText = true;
                    BorderAround(row_TieuDe_BaoCao);

                    oRow++;
                    DataRow[] dr = dtBCLSP.Select();
                    string[,] rowData = new string[dr.Count(), dtBCLSP.Columns.Count];

                    int rowCnt = 0;
                    int rowBD = oRow;
                    foreach (DataRow row in dtBCLSP.Rows)
                    {
                        for (int col = 0; col < dtBCLSP.Columns.Count; col++)
                        {
                            rowData[rowCnt, col] = row[col].ToString();
                        }
                        rowCnt++;
                    }



                    oRow = rowBD + rowCnt - 1;
                    oSheet.get_Range("A" + rowBD, lastColumn + oRow.ToString()).Value2 = rowData;
                    oSheet.get_Range("A" + rowBD, LastTitleColumn + oRow.ToString()).Font.Size = fontSizeNoiDung;
                    oSheet.get_Range("A" + rowBD, LastTitleColumn + oRow.ToString()).Font.Name = fontName;
                    oSheet.get_Range("A" + rowBD, "A" + oRow.ToString()).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    BorderAround(oSheet.get_Range("A" + rowBD, LastTitleColumn + oRow.ToString()));

                    Microsoft.Office.Interop.Excel.Range formatRange;
                    Microsoft.Office.Interop.Excel.Range formatRange1;

                    formatRange1 = oSheet.get_Range("J" + rowBD, "J" + rowBD.ToString());
                    formatRange1.Value2 = "=H8+I8";
                    //title.Value2 = "=" + Commons.Modules.MExcel.TimDiemExcel(DONG + 1, i - 2) + "-" + Commons.Modules.MExcel.TimDiemExcel(DONG + 1, i-1) + "";
                    formatRange = oSheet.get_Range("J" + rowBD, "J" + oRow.ToString());
                    if (dtBCLSP.Rows.Count > 1)
                    {
                        formatRange1.AutoFill(formatRange, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                    }

                    formatRange = oSheet.get_Range("H" + rowBD, "H" + oRow.ToString());
                    formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    formatRange = oSheet.get_Range("I" + rowBD, "I" + oRow.ToString());
                    formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    formatRange = oSheet.get_Range("J" + rowBD, "J" + oRow.ToString());
                    formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    formatRange = oSheet.get_Range("K" + rowBD, "K" + oRow.ToString());
                    formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    formatRange = oSheet.get_Range("L" + rowBD, "L" + oRow.ToString());
                    formatRange.NumberFormat = "#,##0.000;(#,##0.000); ; ";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                    formatRange = oSheet.get_Range("M" + rowBD, "M" + oRow.ToString());
                    formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                    formatRange = oSheet.get_Range("N" + rowBD, "N" + oRow.ToString());
                    formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                    formatRange = oSheet.get_Range("O" + rowBD, "O" + oRow.ToString());
                    formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);


                    oSheet.get_Range("O" + (rowBD - 2).ToString() + "").Value2 = "=SUBTOTAL(9,O" + rowBD + ":O" + oRow.ToString() + ")";
                    oSheet.get_Range("O" + (rowBD - 2).ToString() + "").NumberFormat = "#,##0;(#,##0); ; ";
                    oSheet.get_Range("O" + (rowBD - 2).ToString() + "").Font.Size = fontSizeNoiDung;
                    oSheet.get_Range("O" + (rowBD - 2).ToString() + "").Font.Name = fontName;
                    oSheet.get_Range("O" + (rowBD - 2).ToString() + "").Font.Bold = true;
                    oSheet.get_Range("O" + (rowBD - 2).ToString() + "").Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oRow = 1;
                    oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oBook.ActiveSheet;
                    oSheet = oBook.Worksheets.Add(After: oBook.Sheets[oBook.Sheets.Count]);
                }
                oBook.Sheets[1].Activate();

                oBook.SaveAs(path + @"\LSPTheoCongDoan_" + (iID_DV == 1 ? "DM1" : "DM2") + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx",
                 AccessMode: Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive);


            }
            catch (Exception ex)
            {
                WriteLog("Error Bảng lương sản phẩm theo từng công đoạn: " + ex.Message);
            }
        } //4
        private void LuongSPMaHangTheoCN(int iID_DV, string path, DateTime dNgayDauThang, DateTime dNgayCuoiThang)
        {
            try
            {
                if (System.IO.File.Exists(path + @"\LSPCNTheoChuyen_" + (iID_DV == 1 ? "DM1" : "DM2") + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx")) return;

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtTTChung;
                DataTable dtChuyen;
                DataTable dtBCLSP;

                dtTTChung = new DataTable();
                dtTTChung = Commons.Modules.ObjSystems.DataThongTinChung();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetLSPChuyen", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = iID_DV;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dNgayDauThang;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dNgayCuoiThang;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtChuyen = new DataTable();
                dtChuyen = ds.Tables[0].Copy();
                if (dtChuyen.Rows.Count == 0)
                {
                    WriteLog("Error Bảng tổng hợp lương sản phẩm mã hàng theo công nhân: Không có dữ liệu in");
                    return;
                }
                Microsoft.Office.Interop.Excel.Application oApp;
                Microsoft.Office.Interop.Excel.Workbook oBook;
                Microsoft.Office.Interop.Excel.Worksheet oSheet;
                oApp = new Microsoft.Office.Interop.Excel.Application();
                oApp.Visible = false;

                oBook = oApp.Workbooks.Add();
                oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oBook.Worksheets.get_Item(1);

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 12;
                int oRow = 1;

                foreach (DataRow rowC in dtChuyen.Rows)
                {
                    oSheet.Name = rowC[1].ToString();
                    if (oRow == 1)
                    {
                        Microsoft.Office.Interop.Excel.Range row1_ThongTinCty = oSheet.get_Range("A1", "H1");
                        row1_ThongTinCty.Merge();
                        row1_ThongTinCty.Font.Size = fontSizeNoiDung;
                        row1_ThongTinCty.Font.Name = fontName;
                        row1_ThongTinCty.Font.Bold = true;
                        row1_ThongTinCty.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        row1_ThongTinCty.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row1_ThongTinCty.Value2 = dtTTChung.Rows[0][0];

                        Microsoft.Office.Interop.Excel.Range row2_ThongTinCty = oSheet.get_Range("A2", "H2");
                        row2_ThongTinCty.Merge();
                        row2_ThongTinCty.Font.Size = fontSizeNoiDung;
                        row2_ThongTinCty.Font.Name = fontName;
                        row2_ThongTinCty.Font.Bold = true;
                        row2_ThongTinCty.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        row2_ThongTinCty.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row2_ThongTinCty.Value2 = dtTTChung.Rows[0][2];

                        Microsoft.Office.Interop.Excel.Range row4_TieuDe_BaoCao = oSheet.get_Range("A4", "H4");
                        row4_TieuDe_BaoCao.Merge();
                        row4_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                        row4_TieuDe_BaoCao.Font.Name = fontName;
                        row4_TieuDe_BaoCao.Font.Bold = true;
                        row4_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        row4_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row4_TieuDe_BaoCao.RowHeight = 30;
                        row4_TieuDe_BaoCao.Value2 = "BẢNG LƯƠNG SẢN PHẨM MÃ HÀNG CÔNG NHÂN THEO CHUYỀN";

                        Microsoft.Office.Interop.Excel.Range row5_TieuDe_BaoCao = oSheet.get_Range("A5", "H5");
                        row5_TieuDe_BaoCao.Merge();
                        row5_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                        row5_TieuDe_BaoCao.Font.Name = fontName;
                        row5_TieuDe_BaoCao.Font.Bold = true;
                        row5_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        row5_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row5_TieuDe_BaoCao.RowHeight = 20;
                        row5_TieuDe_BaoCao.Value2 = "Từ ngày " + Convert.ToDateTime(dNgayDauThang).ToString("dd/MM/yyyy") + " đến ngày " + Convert.ToDateTime(dNgayCuoiThang).ToString("dd/MM/yyyy");

                        oRow = 7;
                    }

                    Microsoft.Office.Interop.Excel.Range row_Chuyen = oSheet.get_Range("A" + oRow.ToString(), "H" + oRow.ToString());
                    row_Chuyen.Merge();
                    row_Chuyen.Value2 = "Chuyền : " + rowC[1].ToString();
                    row_Chuyen.Font.Size = fontSizeNoiDung;
                    row_Chuyen.Font.Name = fontName;
                    row_Chuyen.Font.Bold = true;
                    row_Chuyen.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    row_Chuyen.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row_Chuyen.RowHeight = 30;

                    oRow++;

                    System.Data.SqlClient.SqlCommand cmdCT = new System.Data.SqlClient.SqlCommand("rptBangLSPChiTietMHCNTheoChuyen", conn);
                    cmdCT.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                    cmdCT.Parameters.Add("@NNgu", SqlDbType.Int).Value = 0;
                    cmdCT.Parameters.Add("@Dvi", SqlDbType.Int).Value = iID_DV;
                    cmdCT.Parameters.Add("@XN", SqlDbType.Int).Value = -1;
                    cmdCT.Parameters.Add("@TO", SqlDbType.Int).Value = -1;
                    cmdCT.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = rowC[0];
                    cmdCT.Parameters.Add("@TNgay", SqlDbType.Date).Value = dNgayDauThang;
                    cmdCT.Parameters.Add("@DNgay", SqlDbType.Date).Value = dNgayCuoiThang;
                    cmdCT.CommandType = CommandType.StoredProcedure;
                    System.Data.SqlClient.SqlDataAdapter adpCT = new System.Data.SqlClient.SqlDataAdapter(cmdCT);

                    DataSet dsCT = new DataSet();
                    adpCT.Fill(dsCT);
                    dtBCLSP = new DataTable();
                    dtBCLSP = dsCT.Tables[0].Copy();
                    int totalColumn = dtBCLSP.Columns.Count;
                    string lastColumn = string.Empty;
                    lastColumn = CharacterIncrement(totalColumn - 1);

                    DataRow[] dr = dtBCLSP.Select();
                    string[,] rowData = new string[dr.Count(), dtBCLSP.Columns.Count];

                    int oCol = 1;
                    foreach (DataColumn col in dtBCLSP.Columns)
                    {
                        oSheet.Cells[oRow, oCol] = col.Caption;
                        oSheet.Cells[oRow, oCol].ColumnWidth = 12;
                        //oSheet.Cells[oRow, oCol].Wraptext = true;
                        oCol = oCol + 1;
                    }

                    oSheet.Cells[oRow, 1] = "Stt";
                    oSheet.Cells[oRow, 1].ColumnWidth = 6;
                    oSheet.Cells[oRow, 2] = "Mã NV";
                    oSheet.Cells[oRow, 2].ColumnWidth = 12;
                    oSheet.Cells[oRow, 3] = "Họ tên";
                    oSheet.Cells[oRow, 3].ColumnWidth = 35;
                    oSheet.Cells[oRow, 4] = "Bộ phận";
                    oSheet.Cells[oRow, 4].ColumnWidth = 20;
                    oSheet.Cells[oRow, 5] = "Chuyền/Phòng";
                    oSheet.Cells[oRow, 5].ColumnWidth = 20;
                    oSheet.Cells[oRow, totalColumn + 1] = "Tổng cộng";
                    oSheet.Cells[oRow, totalColumn + 1].ColumnWidth = 15;
                    oSheet.Cells[oRow, totalColumn + 2] = "CN ký xác nhận";

                    string LastTitleColumn = string.Empty;
                    LastTitleColumn = CharacterIncrement(totalColumn + 1);
                    Microsoft.Office.Interop.Excel.Range row_TieuDe_BaoCao = oSheet.get_Range("A" + oRow.ToString(), LastTitleColumn + oRow.ToString());
                    row_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                    row_TieuDe_BaoCao.Font.Name = fontName;
                    row_TieuDe_BaoCao.Font.Bold = true;
                    row_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row_TieuDe_BaoCao.Cells.WrapText = true;
                    BorderAround(row_TieuDe_BaoCao);

                    oRow++;
                    int rowCnt = 0;
                    int rowBD = oRow;
                    foreach (DataRow row in dtBCLSP.Rows)
                    {
                        for (int col = 0; col < dtBCLSP.Columns.Count; col++)
                        {
                            rowData[rowCnt, col] = row[col].ToString();
                        }
                        rowCnt++;
                    }
                    oRow = rowBD + rowCnt - 1;
                    oSheet.get_Range("A" + rowBD, lastColumn + oRow.ToString()).Value2 = rowData;
                    oSheet.get_Range("A" + rowBD, LastTitleColumn + oRow.ToString()).Font.Size = fontSizeNoiDung;
                    oSheet.get_Range("A" + rowBD, LastTitleColumn + oRow.ToString()).Font.Name = fontName;
                    oSheet.get_Range("A" + rowBD, "A" + oRow.ToString()).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    BorderAround(oSheet.get_Range("A" + rowBD, LastTitleColumn + oRow.ToString()));

                    Microsoft.Office.Interop.Excel.Range formatRange;
                    string CurentColumn = string.Empty;
                    for (int colMH = 5; colMH <= totalColumn - 1; colMH++)
                    {
                        CurentColumn = CharacterIncrement(colMH);
                        formatRange = oSheet.get_Range(CurentColumn + rowBD, CurentColumn + oRow.ToString());
                        formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                        formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    }

                    //set formular
                    oSheet.Cells[rowBD, totalColumn + 1] = "=SUM(F" + rowBD + ":" + lastColumn + rowBD + ")";
                    oSheet.Cells[rowBD, totalColumn + 1].NumberFormat = "#,##0;(#,##0); ; ";
                    oSheet.Cells[rowBD, totalColumn + 1].Copy();

                    CurentColumn = CharacterIncrement(totalColumn);
                    Microsoft.Office.Interop.Excel.Range formularRange = oSheet.get_Range(CurentColumn + (rowBD + 1).ToString(), CurentColumn + oRow.ToString());
                    formularRange.PasteSpecial(Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormulas);
                    formularRange.NumberFormat = "#,##0;(#,##0); ; ";

                    oRow++;
                    Microsoft.Office.Interop.Excel.Range row_TongCong = oSheet.get_Range("A" + oRow.ToString(), "E" + oRow.ToString());
                    row_TongCong.Merge();
                    row_TongCong.Font.Size = fontSizeNoiDung;
                    row_TongCong.Font.Name = fontName;
                    row_TongCong.Font.Bold = true;
                    row_TongCong.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row_TongCong.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row_TongCong.RowHeight = 30;
                    row_TongCong.Value2 = "Tổng cộng";

                    for (int colMH = 6; colMH <= totalColumn + 1; colMH++)
                    {
                        CurentColumn = CharacterIncrement(colMH - 1);
                        oSheet.Cells[oRow, colMH] = "=SUM(" + CurentColumn + rowBD.ToString() + ":" + CurentColumn + (oRow - 1).ToString() + ")";
                        oSheet.Cells[oRow, colMH].NumberFormat = "#,##0;(#,##0); ; ";
                    }

                    Microsoft.Office.Interop.Excel.Range row_Format_TongCong = oSheet.get_Range("A" + oRow.ToString(), LastTitleColumn + oRow.ToString());
                    row_Format_TongCong.Font.Size = fontSizeNoiDung;
                    row_Format_TongCong.Font.Name = fontName;
                    row_Format_TongCong.Font.Bold = true;
                    row_Format_TongCong.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    BorderAround(row_Format_TongCong);

                    oRow = 1;
                    oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oBook.ActiveSheet;
                    oSheet = oBook.Worksheets.Add(After: oBook.Sheets[oBook.Sheets.Count]);
                }
                oBook.Sheets[1].Activate();
                oBook.SaveAs(path + @"\LSPCNTheoChuyen_" + (iID_DV == 1 ? "DM1" : "DM2") + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx",
                AccessMode: Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive);


            }
            catch (Exception ex)
            {
                WriteLog("Error Bảng lương sản phẩm mã hàng theo công nhân: " + ex.Message);
            }

        } //5

        private void LuongSPTheoCongNhan(int iID_DV, string path, DateTime dNgayDauThang, DateTime dNgayCuoiThang)
        {

            try
            {
                if (System.IO.File.Exists(path + @"\BCSanLuongTheoCN_" + (iID_DV == 1 ? "DM1" : "DM2") + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx")) return;

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCLSP;

                System.Data.SqlClient.SqlCommand cmdCT = new System.Data.SqlClient.SqlCommand("rptBangLSPChiTietMHNgayTheoCN_DM", conn);
                cmdCT.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmdCT.Parameters.Add("@NNgu", SqlDbType.Int).Value = 0;
                cmdCT.Parameters.Add("@DVi", SqlDbType.Int).Value = iID_DV;
                cmdCT.Parameters.Add("@XN", SqlDbType.Int).Value = -1;
                cmdCT.Parameters.Add("@TO", SqlDbType.Int).Value = -1;
                cmdCT.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = -1;
                cmdCT.Parameters.Add("@TNgay", SqlDbType.Date).Value = dNgayDauThang;
                cmdCT.Parameters.Add("@DNgay", SqlDbType.Date).Value = dNgayCuoiThang;
                cmdCT.Parameters.Add("@SendMail", SqlDbType.Bit).Value = 1;

                cmdCT.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adpCT = new System.Data.SqlClient.SqlDataAdapter(cmdCT);

                DataSet dsCT = new DataSet();
                adpCT.Fill(dsCT);
                dtBCLSP = new DataTable();
                dtBCLSP = dsCT.Tables[0].Copy();
                if (dtBCLSP.Rows.Count == 0)
                {
                    WriteLog("Error Bảng tổng hợp lương sản phẩm theo công nhân: Không có dữ liệu in");
                    return;
                }

                Microsoft.Office.Interop.Excel.Application oApp;
                Microsoft.Office.Interop.Excel.Workbook oBook;
                Excel.Worksheet oSheet;

                oApp = new Microsoft.Office.Interop.Excel.Application();
                oApp.Visible = false;

                oBook = oApp.Workbooks.Add();
                oSheet = (Excel.Worksheet)oBook.Worksheets.get_Item(1);

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 11;
                int oRow = 1;

                int lastColumn = dtBCLSP.Columns.Count;

                TaoTTChung(oSheet, 1, 2, 1, 7, 0, 0);


                Microsoft.Office.Interop.Excel.Range row4_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[4, lastColumn]];
                row4_TieuDe_BaoCao.Merge();
                row4_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row4_TieuDe_BaoCao.Font.Name = fontName;
                row4_TieuDe_BaoCao.Font.Bold = true;
                row4_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_BaoCao.RowHeight = 30;
                row4_TieuDe_BaoCao.Value2 = "BÁO CÁO SẢN LƯỢNG THEO CÔNG NHÂN";

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[5, 1], oSheet.Cells[5, lastColumn]];
                row5_TieuDe_BaoCao.Merge();
                row5_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                row5_TieuDe_BaoCao.Font.Name = fontName;
                row5_TieuDe_BaoCao.Font.Bold = true;
                row5_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row5_TieuDe_BaoCao.RowHeight = 20;
                row5_TieuDe_BaoCao.Value2 = "Từ ngày " + dNgayDauThang.ToString("dd/MM/yyyy") + " đến ngày " + dNgayCuoiThang.ToString("dd/MM/yyyy");

                oRow = 7;

                oRow++;

                oSheet.Cells[oRow, 1] = "STT";
                oSheet.Cells[oRow, 1].ColumnWidth = 9;
                oSheet.Cells[oRow, 2] = "Mã thẻ CNV";
                oSheet.Cells[oRow, 2].ColumnWidth = 15;
                oSheet.Cells[oRow, 3] = "Họ tên";
                oSheet.Cells[oRow, 3].ColumnWidth = 25;
                oSheet.Cells[oRow, 4] = "Mã hàng";
                oSheet.Cells[oRow, 4].ColumnWidth = 10;
                oSheet.Cells[oRow, 5] = "Mã công đoạn";
                oSheet.Cells[oRow, 5].ColumnWidth = 8;
                oSheet.Cells[oRow, 6] = "Tên công đoạn";
                oSheet.Cells[oRow, 6].ColumnWidth = 35;
                oSheet.Cells[oRow, 7] = "Tổng SL CN kê";
                oSheet.Cells[oRow, 7].ColumnWidth = 10;
                oSheet.Cells[oRow, 8] = "Đơn giá";
                oSheet.Cells[oRow, 8].ColumnWidth = 10;
                oSheet.Cells[oRow, 9] = "Thành tiền";
                oSheet.Cells[oRow, 9].ColumnWidth = 15;
                oSheet.Cells[oRow, 10] = "Chuyền thực hiện";
                oSheet.Cells[oRow, 10].ColumnWidth = 20;


                Microsoft.Office.Interop.Excel.Range row_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, 1], oSheet.Cells[oRow, lastColumn]];
                row_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                row_TieuDe_BaoCao.Font.Name = fontName;
                row_TieuDe_BaoCao.Font.Bold = true;
                row_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row_TieuDe_BaoCao.Cells.WrapText = true;
                BorderAround(row_TieuDe_BaoCao);

                oRow++;
                DataRow[] dr = dtBCLSP.Select();
                string[,] rowData = new string[dr.Count(), dtBCLSP.Columns.Count];

                int rowCnt = 0;
                int rowBD = oRow;
                foreach (DataRow row in dtBCLSP.Rows)
                {
                    for (int col = 0; col < dtBCLSP.Columns.Count; col++)
                    {
                        rowData[rowCnt, col] = row[col].ToString();
                    }
                    rowCnt++;
                }
                oRow = rowBD + rowCnt - 1;
                oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Value2 = rowData;
                oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Font.Size = fontSizeNoiDung;
                oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Font.Name = fontName;
                BorderAround(oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]]);

                Microsoft.Office.Interop.Excel.Range formatRange;


                formatRange = oSheet.Range[oSheet.Cells[rowBD, 7], oSheet.Cells[oRow, 7]];
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                formatRange = oSheet.Range[oSheet.Cells[rowBD, 8], oSheet.Cells[oRow, 8]];
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.NumberFormat = "#,##0.00;(#,##0.000); ; ";
                formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                formatRange = oSheet.Range[oSheet.Cells[rowBD, 9], oSheet.Cells[oRow, 9]];
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                //can giua

                // STT
                formatRange = oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, 1]];
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                // ma ql
                formatRange = oSheet.Range[oSheet.Cells[rowBD, 5], oSheet.Cells[oRow, 5]];
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


                // SUM
                formatRange = oSheet.Range[oSheet.Cells[7, 9], oSheet.Cells[7, 9]];
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 12;
                formatRange.Font.Bold = true;
                formatRange.Value = "=SUBTOTAL(9,I" + rowBD + ":I" + oRow.ToString() + ")";
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                oBook.SaveAs(path + @"\BCSanLuongTheoCN_" + (iID_DV == 1 ? "DM1" : "DM2") + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx",
                AccessMode: Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive);

            }
            catch (Exception ex)
            {
                WriteLog("Error Bảng lương sản phẩm theo công nhân: " + ex.Message);
            }
        } //6

        private void LuongSPTongHopTN(int iID_DV, string path, DateTime dNgayDauThang, DateTime dNgayCuoiThang)
        {
            try
            {
                if (System.IO.File.Exists(path + @"\BCPhanTichLSP_" + (iID_DV == 1 ? "DM1" : "DM2") + "_" + datDNgay.DateTime.ToString("yyyyMMdd") + ".xlsx")) return;

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                DateTime NgayDauThang = new DateTime(datTNgay.DateTime.Year, datTNgay.DateTime.Month, 1);
                DateTime NgayCuoiThang = NgayDauThang.AddMonths(1).AddDays(-1);

                DateTime TuNgay = dNgayDauThang;
                DateTime DenNgay = dNgayCuoiThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptPVTheoThamNien", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = iID_DV;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = datTNgay.DateTime;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = datDNgay.DateTime;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();
                if (dtBCThang.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBaoCaoThamNienKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Cursor = Cursors.Default;
                    return;
                }

                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 10;
                int fontSizeNoiDung = 10;

                int lastColumn = 0;
                lastColumn = dtBCThang.Columns.Count;

                TaoTTChung(oSheet, 1, 2, 1, 7, 0, 0);

                Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[5, 1], oSheet.Cells[5, 1]];
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BẢNG THỐNG KÊ TIỀN LƯƠNG SẢN PHẨM THEO PHÒNG BAN VÀ THÂM NIÊN THÁNG " + datTNgay.DateTime.ToString("MM/yyyy") + "";

                row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[6, 3], oSheet.Cells[6, 3]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "Từ ngày " + Convert.ToDateTime(datTNgay.EditValue).ToString("dd/MM/yyyy") + " đến ngày " + Convert.ToDateTime(datDNgay.EditValue).ToString("dd/MM/yyyy");


                Range row4_TieuDe_Format = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[8, lastColumn]];
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(221, 235, 247);

                Range row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[8, 1]];
                row4_TieuDe_TTC.Merge();
                row4_TieuDe_TTC.Value2 = "Bộ phận";
                row4_TieuDe_TTC.ColumnWidth = 35;

                string SoTienCongChuanThang = "0";
                try
                {
                    SoTienCongChuanThang = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT FORMAT(ROUND(CONVERT(INT,T1.LUONG_TOI_THIEU / dbo.fnGetSoNgayCongQuiDinhThang('" + NgayDauThang.ToString("MM/dd/yyyy") + "','" + NgayCuoiThang.ToString("MM/dd/yyyy") + "')),2),'N0') FROM dbo.LUONG_TOI_THIEU T1 INNER JOIN (SELECT ID_LTT, MAX(NGAY_QD) MAX_NGAY FROM dbo.LUONG_TOI_THIEU WHERE ID_DV = " + iID_DV + " GROUP BY ID_LTT) T2 ON T2.ID_LTT = T1.ID_LTT AND T1.NGAY_QD = T2.MAX_NGAY"));
                    //string s = Convert.ToString(SoTienCongChuanThang).ToString("#,##0.00");
                }
                catch { }
                int iCot = 2;
                while (TuNgay <= DenNgay)
                {
                    row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot + 2]];
                    row4_TieuDe_TTC.Merge();
                    row4_TieuDe_TTC.Value = TuNgay.ToString("dd/MM/yyyy");

                    if (TuNgay.DayOfWeek.ToString() == "Sunday" || TuNgay.DayOfWeek.ToString() == "Saturday")
                    {
                        row4_TieuDe_TTC.Interior.Color = Color.FromArgb(255, 255, 0);
                    }

                    row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[8, iCot], oSheet.Cells[8, iCot]];
                    row4_TieuDe_TTC.Value2 = ">=" + SoTienCongChuanThang + "";
                    row4_TieuDe_TTC.ColumnWidth = 10;

                    iCot++;
                    row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[8, iCot], oSheet.Cells[8, iCot]];
                    row4_TieuDe_TTC.Value2 = "<" + SoTienCongChuanThang + "";
                    row4_TieuDe_TTC.ColumnWidth = 10;

                    iCot++;
                    row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[8, iCot], oSheet.Cells[8, iCot]];
                    row4_TieuDe_TTC.Value2 = "Grand Total";
                    row4_TieuDe_TTC.ColumnWidth = 10;

                    TuNgay = TuNgay.AddDays(1);
                    iCot++;
                }

                int rowCnt = 8;
                foreach (DataRow row2 in dtBCThang.Rows)
                {
                    rowCnt++;
                    dynamic[] arr = { row2["TEN_TO"].ToString() };
                    TuNgay = datTNgay.DateTime;
                    while (TuNgay <= DenNgay)
                    {
                        arr = arr.Append(row2["GREATER_" + TuNgay.Day + ""].ToString()).ToArray();
                        arr = arr.Append(row2["LESS_" + TuNgay.Day + ""].ToString()).ToArray();
                        arr = arr.Append(row2["TOTAL_" + TuNgay.Day + ""].ToString()).ToArray();

                        TuNgay = TuNgay.AddDays(1);
                    }
                    Range rowData = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, lastColumn]];
                    rowData.Font.Size = fontSizeNoiDung;
                    rowData.Font.Name = fontName;
                    rowData.Value2 = arr;
                }

                rowCnt++;
                Microsoft.Office.Interop.Excel.Range formatRange;

                for (int colSUM = 2; colSUM <= dtBCThang.Columns.Count; colSUM++)
                {
                    //ws.Cells[row, colSUM] = "=SUBTOTAL(9,N10:N" + (row - 1).ToString() + ")";
                    oSheet.Cells[rowCnt, colSUM] = "=SUM(" + CellAddress(oSheet, 8, colSUM) + ":" + CellAddress(oSheet, rowCnt - 1, colSUM) + ")";
                    oSheet.Cells[rowCnt, colSUM].Font.Bold = true;

                    formatRange = oSheet.Range[oSheet.Cells[9, colSUM], oSheet.Cells[rowCnt + 1, colSUM]];
                    formatRange.NumberFormat = "#,##0.00;(#,##0.0); ; ";
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    try
                    {
                        formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    }
                    catch { }
                }

                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 1]];
                formatRange.Value2 = "Tổng";
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 10;

                BorderAround(oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[rowCnt, lastColumn]]);


                ////////////////////////////////////////////////////////////////// Bảng 2 /////////////////////////////////////////////////
                rowCnt = rowCnt + 5;
                int luongCBNgay = 0;
                try
                {
                    luongCBNgay = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.fnGetSoNgayCongQuiDinhThang('" + NgayDauThang.ToString("MM/dd/yyyy") + "','" + NgayCuoiThang.ToString("MM/dd/yyyy") + "')"));
                }
                catch { }

                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 1]];
                formatRange.Value2 = "Tổng lương SP HC tháng " + datTNgay.DateTime.Month + "-" + luongCBNgay + " ngày (theo bộ phận)";
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 10;

                rowCnt++;
                row4_TieuDe_Format = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 11]];
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(221, 235, 247);


                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 1]];
                row4_TieuDe_TTC.Merge();
                row4_TieuDe_TTC.Value2 = "Bộ phận";
                row4_TieuDe_TTC.ColumnWidth = 35;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 2], oSheet.Cells[rowCnt, 2]];
                row4_TieuDe_TTC.Value2 = "<500.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 3], oSheet.Cells[rowCnt, 3]];
                row4_TieuDe_TTC.Value2 = "500.000-<=1000.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 4], oSheet.Cells[rowCnt, 4]];
                row4_TieuDe_TTC.Value2 = ">1.000.000-<=1.500.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 5], oSheet.Cells[rowCnt, 5]];
                row4_TieuDe_TTC.Value2 = ">1.500.000-<=2.000.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 6], oSheet.Cells[rowCnt, 6]];
                row4_TieuDe_TTC.Value2 = "2.000.000-<=2.500.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 7], oSheet.Cells[rowCnt, 7]];
                row4_TieuDe_TTC.Value2 = "2.500.000-<=3.000.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 8], oSheet.Cells[rowCnt, 8]];
                row4_TieuDe_TTC.Value2 = "3.000.000-<=3.500.000";
                row4_TieuDe_TTC.ColumnWidth = 10;


                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 9], oSheet.Cells[rowCnt, 9]];
                row4_TieuDe_TTC.Value2 = "3.500.000-<4.100.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 10], oSheet.Cells[rowCnt, 10]];
                row4_TieuDe_TTC.Value2 = ">=4.100.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 11], oSheet.Cells[rowCnt, 11]];
                row4_TieuDe_TTC.Value2 = "Grand Total";
                row4_TieuDe_TTC.ColumnWidth = 10;

                dtBCThang = ds.Tables[1].Copy();
                int currentRow = rowCnt;
                foreach (DataRow row2 in dtBCThang.Rows)
                {
                    rowCnt++;
                    dynamic[] arr = { row2["TEN_TO"].ToString(), row2["1"].ToString(), row2["2"].ToString(), row2["3"].ToString(), row2["4"].ToString(), row2["5"].ToString(), row2["6"].ToString(),
                        row2["7"].ToString(), row2["8"].ToString(), row2["9"].ToString(),
                        "=SUM(" + CellAddress(oSheet, rowCnt, 2) + ":" + CellAddress(oSheet, rowCnt, 10) + ")"
                    };
                    Range rowData = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 11]];
                    rowData.Font.Size = fontSizeNoiDung;
                    rowData.Font.Name = fontName;
                    rowData.Value2 = arr;
                }
                rowCnt++;
                for (int colSUM = 2; colSUM <= dtBCThang.Columns.Count; colSUM++)
                {
                    //ws.Cells[row, colSUM] = "=SUBTOTAL(9,N10:N" + (row - 1).ToString() + ")";
                    oSheet.Cells[rowCnt, colSUM] = "=SUM(" + CellAddress(oSheet, currentRow + 1, colSUM) + ":" + CellAddress(oSheet, rowCnt - 1, colSUM) + ")";
                    oSheet.Cells[rowCnt, colSUM].Font.Bold = true;

                    formatRange = oSheet.Range[oSheet.Cells[currentRow + 1, colSUM], oSheet.Cells[rowCnt + 1, colSUM]];
                    formatRange.NumberFormat = "#,##0.00;(#,##0.0); ; ";
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    try
                    {
                        formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    }
                    catch { }
                }

                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 1]];
                formatRange.Value2 = "Tổng";
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 10;

                BorderAround(oSheet.Range[oSheet.Cells[currentRow, 1], oSheet.Cells[rowCnt, 11]]);

                ////////////////////////////////////////////////////////////////// Bảng 3 /////////////////////////////////////////////////

                rowCnt = rowCnt + 5;

                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 1]];
                formatRange.Value2 = "Tổng lương SP HC tháng " + datTNgay.DateTime.Month + "-" + luongCBNgay + " ngày (theo thâm niên)";
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 10;

                rowCnt++;
                row4_TieuDe_Format = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 11]];
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(221, 235, 247);


                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 1]];
                row4_TieuDe_TTC.Merge();
                row4_TieuDe_TTC.Value2 = "Length of service";
                row4_TieuDe_TTC.ColumnWidth = 35;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 2], oSheet.Cells[rowCnt, 2]];
                row4_TieuDe_TTC.Value2 = "<500.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 3], oSheet.Cells[rowCnt, 3]];
                row4_TieuDe_TTC.Value2 = "500.000-<=1000.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 4], oSheet.Cells[rowCnt, 4]];
                row4_TieuDe_TTC.Value2 = ">1.000.000-<=1.500.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 5], oSheet.Cells[rowCnt, 5]];
                row4_TieuDe_TTC.Value2 = ">1.500.000-<=2.000.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 6], oSheet.Cells[rowCnt, 6]];
                row4_TieuDe_TTC.Value2 = "2.000.000-<=2.500.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 7], oSheet.Cells[rowCnt, 7]];
                row4_TieuDe_TTC.Value2 = "2.500.000-<=3.000.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 8], oSheet.Cells[rowCnt, 8]];
                row4_TieuDe_TTC.Value2 = "3.000.000-<=3.500.000";
                row4_TieuDe_TTC.ColumnWidth = 10;


                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 9], oSheet.Cells[rowCnt, 9]];
                row4_TieuDe_TTC.Value2 = "3.500.000-<=4.100.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 10], oSheet.Cells[rowCnt, 10]];
                row4_TieuDe_TTC.Value2 = ">=4.100.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 11], oSheet.Cells[rowCnt, 11]];
                row4_TieuDe_TTC.Value2 = "Grand Total";
                row4_TieuDe_TTC.ColumnWidth = 10;

                dtBCThang = ds.Tables[2].Copy();
                currentRow = rowCnt;
                foreach (DataRow row2 in dtBCThang.Rows)
                {
                    rowCnt++;
                    dynamic[] arr = { row2["THAM_NIEN"].ToString(), row2["1"].ToString(), row2["2"].ToString(), row2["3"].ToString(), row2["4"].ToString(), row2["5"].ToString(), row2["6"].ToString(),
                        row2["7"].ToString(), row2["8"].ToString(), row2["9"].ToString(),
                        "=SUM(" + CellAddress(oSheet, rowCnt, 2) + ":" + CellAddress(oSheet, rowCnt, 10) + ")"
                    };
                    Range rowData = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 11]];
                    rowData.Font.Size = fontSizeNoiDung;
                    rowData.Font.Name = fontName;
                    rowData.Value2 = arr;
                }
                rowCnt++;
                for (int colSUM = 2; colSUM <= dtBCThang.Columns.Count; colSUM++)
                {
                    //ws.Cells[row, colSUM] = "=SUBTOTAL(9,N10:N" + (row - 1).ToString() + ")";
                    oSheet.Cells[rowCnt, colSUM] = "=SUM(" + CellAddress(oSheet, currentRow + 1, colSUM) + ":" + CellAddress(oSheet, rowCnt - 1, colSUM) + ")";
                    oSheet.Cells[rowCnt, colSUM].Font.Bold = true;

                    formatRange = oSheet.Range[oSheet.Cells[currentRow + 1, colSUM], oSheet.Cells[rowCnt + 1, colSUM]];
                    formatRange.NumberFormat = "#,##0.00;(#,##0.0); ; ";
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    try
                    {
                        formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    }
                    catch { }
                }

                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 1]];
                formatRange.Value2 = "Tổng";
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 10;

                BorderAround(oSheet.Range[oSheet.Cells[currentRow, 1], oSheet.Cells[rowCnt, 11]]);

                Commons.Modules.MExcel.ThemDong(oSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 2, 7);

                oWB.SaveAs(path + @"\BCPhanTichLSPTheoBoPhanThamNien_" + (iID_DV == 1 ? "DM1" : "DM2") + "_" + datDNgay.DateTime.ToString("yyyyMMdd") + ".xlsx",
                  AccessMode: Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive);
                oWB.Close();
            }
            catch (Exception ex)
            {
                WriteLog("Error Bảng lương sản phẩm theo công nhân: " + ex.Message);
            }
        } //7

        private void BorderAround(Microsoft.Office.Interop.Excel.Range range)
        {
            Microsoft.Office.Interop.Excel.Borders borders = range.Borders;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            borders.Color = Color.Black;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalUp].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalDown].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
        }
        public int TaoTTChung(Excel.Worksheet MWsheet, int DongBD, int CotBD, int DongKT, int CotKT, float MLeft, float MTop)
        {
            try
            {
                DataTable dtTmp = Commons.Modules.ObjSystems.DataThongTinChung();
                Microsoft.Office.Interop.Excel.Range CurCell = MWsheet.Range[MWsheet.Cells[DongBD, 1], MWsheet.Cells[DongKT, 1]];
                CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);

                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT - 3]];
                CurCell.Merge(true);
                CurCell.Font.Bold = true;
                CurCell.Borders.LineStyle = 0;
                CurCell.Value2 = dtTmp.Rows[0]["TEN_CTY"];

                DongBD += 1;
                DongKT += 1;
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, "A"], MWsheet.Cells[DongKT, "A"]];
                CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
                CurCell.Merge(true);
                CurCell.Font.Bold = true;
                CurCell.Borders.LineStyle = 0;
                CurCell.Value2 = Commons.Modules.ObjLanguages.GetLanguage("frmChung", "diachi") + " : " + dtTmp.Rows[0]["DIA_CHI"].ToString();

                DongBD += 1;
                DongKT += 1;
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, "A"], MWsheet.Cells[DongKT, "A"]];
                CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
                CurCell.Merge(true);
                CurCell.Font.Bold = true;
                CurCell.Borders.LineStyle = 0;
                CurCell.Value2 = Commons.Modules.ObjLanguages.GetLanguage("frmChung", "dienthoai") + " : " + dtTmp.Rows[0]["DIEN_THOAI"] + "  " + Commons.Modules.ObjLanguages.GetLanguage("frmChung", "Fax") + " : " + dtTmp.Rows[0]["FAX"].ToString();

                //DongBD += 1;
                //DongKT += 1;
                //CurCell = MWsheet.Range[MWsheet.Cells[DongBD, "A"], MWsheet.Cells[DongKT, "A"]];
                //CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);
                //CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
                //CurCell.Merge(true);
                //CurCell.Font.Bold = true;
                //CurCell.Borders.LineStyle = 0;
                //CurCell.Value2 = "Email : " + dtTmp.Rows[0]["EMAIL"];

                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Masters");
                GetImage((byte[])dtTmp.Rows[0]["LOGO"], System.Windows.Forms.Application.StartupPath, "logo.bmp");
                MWsheet.Shapes.AddPicture(System.Windows.Forms.Application.StartupPath + @"\logo.bmp", Office.MsoTriState.msoFalse, Office.MsoTriState.msoCTrue, MLeft, MTop, 50, 50);
                System.IO.File.Delete(System.Windows.Forms.Application.StartupPath + @"\logo.bmp");

                return DongBD + 1;
            }
            catch
            {
                return DongBD + 1;
            }
        }
        public void GetImage(byte[] Logo, string sPath, string sFile)
        {
            try
            {
                string strPath = sPath + @"\" + sFile;
                System.IO.MemoryStream stream = new System.IO.MemoryStream(Logo);
                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                img.Save(strPath);
            }
            catch (Exception)
            {
            }
        }

        private string CellAddress(Excel.Worksheet sht, int row, int col)
        {
            return RangeAddress(sht.Cells[row, col]);
        }
        private string RangeAddress(Microsoft.Office.Interop.Excel.Range rng)
        {
            object missing = null;
            return rng.get_AddressLocal(false, false, Microsoft.Office.Interop.Excel.XlReferenceStyle.xlA1,
                   missing, missing);
        }
        static string CharacterIncrement(int colCount)
        {
            int TempCount = 0;
            string returnCharCount = string.Empty;

            if (colCount <= 25)
            {
                TempCount = colCount;
                char CharCount = Convert.ToChar((Convert.ToInt32('A') + TempCount));
                returnCharCount += CharCount;
                return returnCharCount;
            }
            else
            {
                var rev = 0;

                while (colCount >= 26)
                {
                    colCount = colCount - 26;
                    rev++;
                }

                returnCharCount += CharacterIncrement(rev - 1);
                returnCharCount += CharacterIncrement(colCount);
                return returnCharCount;
            }
        }

        #endregion
        #region chuotphai
        class RowInfo
        {
            public RowInfo(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
            {
                this.RowHandle = rowHandle;
                this.View = view;
            }
            public DevExpress.XtraGrid.Views.Grid.GridView View;
            public int RowHandle;
        }
        //Thong tin nhân sự
        public DXMenuItem MDeleteFile(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblDelete", Commons.Modules.TypeLanguage);
            DXMenuItem menuThongTinNS = new DXMenuItem(sStr, new EventHandler(Delete));
            menuThongTinNS.Tag = new RowInfo(view, rowHandle);
            return menuThongTinNS;
        }
        public void Delete(object sender, EventArgs e)
        {
            try
            {
                FileInfo file = new FileInfo(txtDuongDanTL.Text + @"\" + grvData.GetFocusedRowCellValue("TAI_LIEU").ToString());
                file.Delete();
                LoadFile(getAttachment(1, txtDuongDanTL.Text));
            }
            catch (Exception ex) { }
        }
        private void grvData_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    int irow = e.HitInfo.RowHandle;
                    e.Menu.Items.Clear();
                    DevExpress.Utils.Menu.DXMenuItem itemTTNS = MDeleteFile(view, irow);
                    e.Menu.Items.Add(itemTTNS);
                }
            }
            catch
            {
            }
        }

        #endregion

        #endregion

        private void datDNgay_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSubject.Text.IndexOf(datNgayOld.ToString("dd/MM/yyyy")) != -1)
                {
                    txtSubject.Text = txtSubject.Text.Replace("" + datNgayOld.ToString("dd/MM/yyyy") + "", "" + datDNgay.Text + "");
                }
                if (txtBody.Text.IndexOf(datNgayOld.ToString("dd/MM/yyyy")) != -1)
                {
                    txtBody.Text = txtBody.Text.Replace("" + datNgayOld.ToString("dd/MM/yyyy") + "", "" + datDNgay.Text + "");
                }
                datNgayOld = datDNgay.DateTime;
            }
            catch
            {
                datNgayOld = datDNgay.DateTime;
            }

        }
    }
}