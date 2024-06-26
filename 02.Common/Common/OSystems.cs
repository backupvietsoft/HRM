﻿using Commons.Properties;
using DevExpress.Utils;
using DevExpress.Utils.Html;
using DevExpress.Utils.Layout;
using DevExpress.Xpo.DB;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraLayout;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using Microsoft.ApplicationBlocks.Data;
using Newtonsoft.Json;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Commons
{
    public class OSystems
    {

        private string strSql;
        public DataTable MOpenData()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD,VIETNAM AS NN FROM dbo.LANGUAGES WHERE FORM = 'ucLyLich'"));
            return dt;
        }

        public static void SetDateEditFormat(DateEdit dateEdit)
        {
            dateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dateEdit.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            dateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dateEdit.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            dateEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            dateEdit.Properties.Mask.EditMask = "dd/MM/yyyy";
        }
        public bool KiemFileTonTai(string sFile)
        {
            try
            {
                return (System.IO.File.Exists(sFile));
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string OpenFiles(string MFilter)
        {
            try
            {
                OpenFileDialog f = new OpenFileDialog();
                f.Filter = MFilter;
                try
                {
                    DialogResult res = f.ShowDialog();
                    if (res == DialogResult.OK)
                        return f.FileName;
                    return "";
                }
                catch
                {
                    return "";
                }
            }
            catch (Exception)
            {
                return "";
            }
        }
        public string LocKyTuDB(string sChuoi)
        {
            if (sChuoi.Length > 0)
                sChuoi = sChuoi.Replace("/", "-");
            if (sChuoi.Length > 0)
                sChuoi = sChuoi.Replace(@"\", "-");
            if (sChuoi.Length > 0)
                sChuoi = sChuoi.Replace("*", "-");
            if (sChuoi.Length > 0)
                sChuoi = sChuoi.Replace("-", "-");
            if (sChuoi.Length > 0)
                sChuoi = sChuoi.Replace(".", "-");
            if (sChuoi.Length > 0)
                sChuoi = sChuoi.Replace("!", "-");
            if (sChuoi.Length > 0)
                sChuoi = sChuoi.Replace("@", "-");
            if (sChuoi.Length > 0)
                sChuoi = sChuoi.Replace("#", "-");
            return sChuoi;
        }
        public string LayDuoiFile(string strFile)
        {
            string[] FILE_NAMEArr, arr;
            string FILE_NAME = "";
            FILE_NAMEArr = strFile.Split('\\');
            FILE_NAME = FILE_NAMEArr[FILE_NAMEArr.Length - 1];
            arr = FILE_NAME.Split('.');
            return "." + arr[arr.Length - 1];
        }
        public string STTFileCungThuMuc(string sThuMuc, string sFile)
        {
            string TenFile = sFile;
            string DuoiFile;
            try
            {
                DuoiFile = LayDuoiFile(sFile);
            }
            catch (Exception ex)
            {
                DuoiFile = "";
            }


            try
            {
                string[] sTongFile;
                int i = 1;

                TenFile = sFile;
                sTongFile = System.IO.Directory.GetFiles(sThuMuc);


                for (i = 1; i <= sTongFile.Length + 1; i++)
                {
                    if (System.IO.File.Exists(sThuMuc + @"\" + TenFile) == true)
                    {
                        if (i.ToString().Length == 1)
                            TenFile = sFile.Replace(DuoiFile, "-00" + i.ToString()) + DuoiFile;
                        else if (i.ToString().Length == 2)
                            TenFile = sFile.Replace(DuoiFile, "-0" + i.ToString()) + DuoiFile;
                        else
                            TenFile = sFile.Replace(DuoiFile, "-" + i.ToString()) + DuoiFile;
                    }
                    else
                        break;
                }
            }
            catch (Exception ex)
            {
                TenFile = "";
            }

            return TenFile;
        }
        public void Xoahinh(string strDuongdan)
        {
            if (System.IO.File.Exists(strDuongdan))
            {
                try
                {
                    System.IO.File.Delete(strDuongdan);
                }
                catch (Exception ex)
                {
                }
            }
        }
        public void DeleteDirectory(string strDuongdan)
        {
            if (System.IO.Directory.Exists(strDuongdan))
            {
                try
                {
                    System.IO.Directory.Delete(strDuongdan, true);
                }
                catch (Exception ex)
                {
                }
            }
        }

        public DateTime ConvertDateTime(string sDate)
        {
            System.Globalization.CultureInfo cultures = new System.Globalization.CultureInfo("en-US");
            DateTime ngay;
            try
            {
                ngay = DateTime.ParseExact(sDate, "dd/MM/yyyy", cultures);
                return ngay;
            }
            catch
            {
                try
                {
                    ngay = DateTime.ParseExact("01/" + sDate, "dd/MM/yyyy", cultures);
                    return ngay;
                }
                catch
                {
                    try
                    {
                        ngay = DateTime.ParseExact("01/0" + sDate, "dd/MM/yyyy", cultures);
                        return ngay;
                    }
                    catch
                    {
                        ngay = DateTime.ParseExact("01/01/" + sDate, "dd/MM/yyyy", cultures);
                        return ngay;
                    }
                }
            }
        }

        #region MessageChung
        //xoa
        public DialogResult msgHoi(string sThongBao)
        {
            //ThongBao.Thông_Báo

            DialogResult dl = XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", sThongBao),
                 (Commons.Modules.TypeLanguage == 0 ? ThongBao.msgTBV.ToString() : ThongBao.msgTBA), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return dl;
        }

        public void msgChung(string sThongBao)
        {
            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", sThongBao), (Commons.Modules.TypeLanguage == 0 ? ThongBao.msgTBV.ToString() : ThongBao.msgTBA), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void msgChung(string sThongBao, string sLoi)
        {
            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", sThongBao) + "\n" + sLoi, (Commons.Modules.TypeLanguage == 0 ? ThongBao.msgTBV.ToString() : ThongBao.msgTBA), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion
        public void MsgWarningVer()
        {
            System.ComponentModel.IContainer components = null;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager();

            DevExpress.Utils.Html.HtmlTemplateCollection htmlTemplateCollection1 = new DevExpress.Utils.Html.HtmlTemplateCollection();
            DevExpress.Utils.Html.HtmlTemplate htmlTemplate1 = new DevExpress.Utils.Html.HtmlTemplate();


            htmlTemplate1.Name = "htmlTemplate1";
            htmlTemplate1.Template = "<div class=\"frame\" id=\"frame\">\r\n\t<div class=\"content\">\r\n\t    <div class=\"text caption\">Thông báo</div>\r\n\t\t<div id=\"content\">\r\n\t\t   \t<div class=\"text message\">\r\n\t\t   \t\t<div class=\"title\">\r\n\t\t   \t\t\t<p>Phần mềm đang tạm thời ngưng hoạt động.<br> <br>Vui lòng liên hệ Vietsoft.</p>\r\n\t\t   \t\t</div>\r\n\t\t   \t\t<div class=\"container\"><p><b>Thông tin liên hệ:</b> <br><br> Ms Nguyễn thị Thùy Dương, MP và Zalo: 0986 778 578 <br><br>Email: <a href=\"mailto:sale@vietsoft.com.vn\">sale@vietsoft.com.vn</a> </p></div>\r\n\t\t   \t</div>\r\n\t\t</div>\r\n\t</div>\r\n\t<div class=\"buttons\">\r\n    \t<div class=\"button\" tabindex=\"3\" id=\"dialogresult-ok\">Exit</div>\r\n    </div>\r\n</div>\r\n";


            htmlTemplate1.Styles = "body{\r\n\tpadding: 15px;\r\n\tfont-size: 12pt;\r\n\tfont-family: \"Segoe UI\";\r\n\ttext-align: center;\r\n}\r\na{\r\n\tcolor: Blue;\r\n}\r\n\r\n.frame{\r\n\tcolor: Black;\r\n\tbackground-color: White;\r\n\tborder: 1px solid @Black/0.2;\r\n\tborder-radius: 10px;\r\n\tbox-shadow: 0px 5px 10px 0px rgba(0, 0, 0, 0.2);\r\n\twidth: 500px;\r\n}\r\n.content {\r\n\tpadding: 15px;\r\n}\r\n.text {\r\n\tpadding: 10px;\r\n\ttext-align: left;\r\n}\r\n.caption {\r\n\tfont-size: 15pt;\r\n\tfont-family: 'Segoe UI Semibold';\r\n}\r\n.title{\r\n\tdisplay: flex;\r\n\talign-items: center;\r\n\tjustify-content: center;\r\n\ttext-align: center;\r\n}\r\n.buttons {\r\n\tpadding: 20px;\r\n\tdisplay: flex;\r\n\tflex-direction: row;\r\n\tjustify-content: center;\r\n\tborder-top: 1px solid @Black/0.1;\r\n\tborder-radius: 0px 0px 10px 10px;\r\n}\r\n.button {\r\n\tcolor: Black;\r\n\tbackground-color: White;\r\n\tmin-width: 80px;\r\n\tmargin: 0px 5px;\r\n\tpadding: 5px;\r\n    border: 1px solid Black;\r\n    border-radius: 5px;\r\n    cursor: pointer;\r\n}\r\n.button:hover {\r\n\tbackground-color: Black;\r\n\tcolor: White;\r\n}\r\n";
            htmlTemplateCollection1.AddRange(new DevExpress.Utils.Html.HtmlTemplate[] {
            htmlTemplate1});

            DevExpress.Utils.SvgImageCollection svgImageCollection1 = new DevExpress.Utils.SvgImageCollection(components);

            svgImageCollection1.Add("del", "image://svgimages/diagramicons/del.svg");
            svgImageCollection1.Add("warning", "image://svgimages/icon builder/security_warningcircled2.svg");

            var args = new XtraMessageBoxArgs();
            args.HtmlTemplate.Assign(htmlTemplate1);
            args.HtmlImages = svgImageCollection1;
            args.Caption = "Thông báo";
            args.Text = "";
            args.DefaultButtonIndex = 1;

            DialogResult dr;
            dr = XtraMessageBox.Show(args);
            Application.Exit();

        }
        public int MsgDelete(string sText)
        {
            //Version osVersion = System.Environment.OSVersion.Version;
            //XtraMessageBox.Show(osVersion.Major.ToString());

            int result = 0;
            System.ComponentModel.IContainer components = null;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager();

            DevExpress.Utils.Html.HtmlTemplateCollection htmlTemplateCollection1 = new DevExpress.Utils.Html.HtmlTemplateCollection();
            DevExpress.Utils.Html.HtmlTemplate htmlTemplate1 = new DevExpress.Utils.Html.HtmlTemplate();


            htmlTemplate1.Name = "htmlTemplate1";
            htmlTemplate1.Template = "<div class=\"frame\" id=\"frame\">\r\n\t<div class=\"header\">\r\n\t\t<div class=\"header-element caption\">${Caption}</div>\r\n\t\t<div class=\"header-element close-button\" id=\"closebutton\">\r\n\t\t\t<img src=\"del\" class=\"close-button-img\">\r\n\t\t</div>\r\n\t</div>\r\n\t<div class=\"message-text\" id=\"content\">${MessageText}</div>\r\n\t<div class=\"buttons\">\r\n\t\t<div class=\"button delete\" tabindex=\"1\" id=\"dialogresult-ok\">DELETE</div>\r\n\t\t<div class=\"button cancel\" tabindex=\"2\" id=\"dialogresult-cancel\">CANCEL</div>\r\n\t</div>\r\n</div>";
            htmlTemplate1.Styles = "body{\r\n\tpadding: 15px;\r\n\tfont-size: 14px;\r\n\tfont-family: 'Segoe UI';\r\n}\r\n.frame {\r\n\tmin-width: 470px;\r\n\tbackground-color: @Window;\r\n\tborder-radius: 10px;\r\n\tbox-shadow: 0px 8px 10px 0px rgba(0, 0, 0, 0.2);\r\n}\r\n.header {\r\n\tbackground-color: @Critical;\r\n\tpadding: 5px;\r\n\tdisplay: flex;\r\n\talign-items: center;\r\n\tjustify-content: space-between;\r\n\tborder-radius: 10px 10px 0px 0px;\r\n}\r\n.header-element {\r\n\tmargin: 5px 5px 5px 25px;\r\n}\r\n.caption {\r\n\tcolor: @White;\r\n\tfont-weight: bold;\r\n}\r\n.close-button-img {\r\n\tfill: @White;\r\n\twidth: 18px;\r\n\theight: 18px;\r\n\topacity: 0.8;\r\n}\r\n.close-button {\r\n\tpadding: 5px;\r\n\tborder-radius: 4px;\r\n}\r\n.close-button:hover {\r\n\tbackground-color: @WindowText/0.1;\r\n}\r\n.close-button:active {\r\n\tbackground-color: @ControlText/0.05;\r\n}\r\n.message-text {\r\n\tmargin: 15px 30px;\r\n\tfont-size: 14px;\r\n\twhite-space: pre;\r\n\tcolor: @WindowText/0.8;\r\n}\r\n.buttons {\r\n\tmargin: 10px;\r\n\tdisplay: flex;\r\n\tjustify-content: flex-end;\r\n}\r\n.button {\r\n\tcolor: @Critical;\r\n\tborder-radius: 5px;\r\n\tpadding: 8px 24px;\r\n\tmargin: 0px 5px;\r\n\tborder: solid 1px @Transparent;\r\n}\r\n.button:hover {\r\n\tcolor: @White;\r\n\tbackground-color: @Critical;\r\n\tbox-shadow: 0px 0px 10px @Critical/0.5;\r\n}\r\n.button:focus {\r\n\tborder-color: @Critical;\r\n}\r\n#cancel {\r\n\tborder-color: @Critical;\r\n}";
            htmlTemplateCollection1.AddRange(new DevExpress.Utils.Html.HtmlTemplate[] {
            htmlTemplate1});
            // 
            // htmlTemplate1
            // 

            DevExpress.Utils.SvgImageCollection svgImageCollection1 = new DevExpress.Utils.SvgImageCollection(components);


            svgImageCollection1.Add("del", "image://svgimages/diagramicons/del.svg");

            var args = new XtraMessageBoxArgs();
            args.HtmlTemplate.Assign(htmlTemplate1);
            args.HtmlImages = svgImageCollection1;
            args.Caption = Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption");
            args.Text = sText;
            args.DefaultButtonIndex = 1;
            args.HtmlElementMouseClick += (s, argss) =>
            {
                //var status = argss.Element.ParentElement.ParentElement.ParentElement.FindElementById("frame");
                //status.ClearChildren();
                if (argss.HasClassName("cancel", false))
                {
                    result = 0;
                }
                if (argss.HasClassName("delete", false))
                {
                    result = 1;
                }
            };
            //SoundPlayer infoSoundPlayer = new SoundPlayer("Template\\Sound\\info.wav");
            //infoSoundPlayer.Play();
            XtraMessageBox.Show(args);
            return result;
        }
        public void MsgWarning(string sText)
        {
            int result = 0;
            System.ComponentModel.IContainer components = null;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager();

            DevExpress.Utils.Html.HtmlTemplateCollection htmlTemplateCollection1 = new DevExpress.Utils.Html.HtmlTemplateCollection();
            DevExpress.Utils.Html.HtmlTemplate htmlTemplate1 = new DevExpress.Utils.Html.HtmlTemplate();


            htmlTemplate1.Name = "htmlTemplate1";
            htmlTemplate1.Template = "<div class=\"frame\" id=\"frame\">\r\n    <div class=\"header\">\r\n        <div class=\"caption\">${Caption}</div>\r\n    \t<div class=\"close-button\" id=\"closebutton\">\r\n\t\t\t<img src=\"del\" class=\"close-button-img\" id=\"close\">\r\n\t\t</div>\r\n    </div>\r\n    <div class=\"content\" id=\"content\">\r\n    \t<img src=\"warning\" class=\"message icon\">\r\n    \t<div class=\"message text\">${MessageText}</div>\r\n    \t<div class=\"message button\" tabindex=\"1\" id=\"dialogresult-ok\">OK</div>\r\n    </div>\r\n</div>";
            htmlTemplate1.Styles = "body{\t\r\n\tpadding: 20px;\r\n\tfont-size: 14px;\r\n\tfont-family: 'Segoe UI';\r\n}\r\n.frame {\r\n\twidth: 350px;\r\n\tcolor: @ControlText;\r\n\tbackground-color: @Window;\r\n\tborder: 1px solid @Warning;\r\n\tborder-radius: 16px;\r\n\tdisplay: flex;\r\n\tflex-direction: column;\r\n\tjustify-content: center;\r\n\tbox-shadow: 0px 8px 16px @Warning/0.6;\r\n}\r\n.header {\r\n\tpadding: 8px;\r\n\tcolor: @White;\r\n\tbackground-color: @Warning;\r\n\tborder-radius: 15px 15px 0px 0px;\r\n\tdisplay: flex;\r\n\tjustify-content: space-between;\r\n\talign-items: center;\r\n}\r\n.caption {\r\n\tmargin: 0px 10px;\r\n\tfont-weight: bold;\r\n}\r\n.close-button {\r\n\tpadding: 8px;\r\n\tborder-radius: 5px;\r\n}\r\n.close-button:hover {\r\n\tbackground-color: @WindowText/0.1;\r\n}\r\n.close-button:active {\r\n\tbackground-color: @ControlText/0.05;\r\n}\r\n.close-button-img {\r\n\tfill: White;\r\n\twidth: 18px;\r\n\theight: 18px;\r\n\topacity: 0.8;\r\n}\r\n.content {\r\n\tdisplay: flex;\r\n\talign-items: center;\r\n\tflex-direction: column;\r\n\tpadding: 10px;\r\n}\r\n.message {\r\n\tmargin: 7px;\r\n}\r\n.icon {\r\n\twidth: 48px;\r\n\theight: 48px;\r\n\topacity: 0.8;\r\n}\r\n.text {\r\n\tcolor: @ControlText;\r\n\ttext-align: center;\r\n}\r\n.button {\r\n\tcolor: @Warning;\r\n\tpadding: 8px 24px;\r\n\tborder: 1px solid @Warning;\r\n\tborder-radius: 5px;\r\n}\r\n.button:hover {\r\n\tcolor: @White;\r\n\tbackground-color: @Warning;\r\n\tbox-shadow: 0px 0px 10px @Warning/0.5;\r\n}";
            htmlTemplateCollection1.AddRange(new DevExpress.Utils.Html.HtmlTemplate[] {
            htmlTemplate1});
            // 
            // htmlTemplate1
            // 

            DevExpress.Utils.SvgImageCollection svgImageCollection1 = new DevExpress.Utils.SvgImageCollection(components);

            svgImageCollection1.Add("del", "image://svgimages/diagramicons/del.svg");
            svgImageCollection1.Add("warning", "image://svgimages/icon builder/security_warningcircled2.svg");

            var args = new XtraMessageBoxArgs();
            args.HtmlTemplate.Assign(htmlTemplate1);
            args.HtmlImages = svgImageCollection1;
            args.Caption = Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption");
            args.Text = sText;
            args.DefaultButtonIndex = 1;
            //SoundPlayer warningSoundPlayer = new SoundPlayer("Template\\Sound\\warning.wav");
            //warningSoundPlayer.Play();
            XtraMessageBox.Show(args);
        }
        public int MsgQuestion(string sText)
        {
            int result = 0;
            System.ComponentModel.IContainer components = null;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager();

            DevExpress.Utils.Html.HtmlTemplateCollection htmlTemplateCollection1 = new DevExpress.Utils.Html.HtmlTemplateCollection();
            DevExpress.Utils.Html.HtmlTemplate htmlTemplate1 = new DevExpress.Utils.Html.HtmlTemplate();


            htmlTemplate1.Name = "htmlTemplate1";
            htmlTemplate1.Template = "<div class=\"frame\" id=\"frame\">\r\n    <div class=\"header\">\r\n        <div class=\"caption\">${Caption}</div>\r\n    \t<div class=\"close-button\" id=\"closebutton\">\r\n\t\t\t<img src=\"del\" class=\"close-button-img\" id=\"close\">\r\n\t\t</div>\r\n    </div>\r\n    <div class=\"content\" id=\"content\">\r\n    \t<img src=\"question\" class=\"message icon\">\r\n    \t<div class=\"message text\">${MessageText}</div>\r\n    \t<div class=\"btn\">\r\n    \t\t<div class=\"message button save\" tabindex=\"1\" id=\"dialogresult-ok\">Yes</div>\r\n\t    \t<div class=\"message button dontsave\" tabindex=\"2\" id=\"dialogresult-ok\">No</div>\r\n\t    \t<div class=\"message button cancel\" tabindex=\"3\" id=\"dialogresult-ok\">Cancel</div>\r\n    \t</div>\r\n    </div>\r\n</div>";
            htmlTemplate1.Styles = "body{\t\r\n\tpadding: 20px;\r\n\tfont-size: 14px;\r\n\tfont-family: 'Segoe UI';\r\n}\r\n.frame {\r\n\twidth: 450px;\r\n\tcolor: @ControlText;\r\n\tbackground-color: @Window;\r\n\tborder: 1px solid @Question;\r\n\tborder-radius: 16px;\r\n\tdisplay: flex;\r\n\tflex-direction: column;\r\n\tjustify-content: center;\r\n\tbox-shadow: 0px 8px 16px @Question/0.6;\r\n}\r\n.header {\r\n\tpadding: 8px;\r\n\tcolor: @White;\r\n\tbackground-color: @Question;\r\n\tborder-radius: 15px 15px 0px 0px;\r\n\tdisplay: flex;\r\n\tjustify-content: space-between;\r\n\talign-items: center;\r\n}\r\n.caption {\r\n\tmargin: 0px 10px;\r\n\tfont-weight: bold;\r\n}\r\n.close-button {\r\n\tpadding: 8px;\r\n\tborder-radius: 5px;\r\n}\r\n.close-button:hover {\r\n\tbackground-color: @WindowText/0.1;\r\n}\r\n.close-button:active {\r\n\tbackground-color: @ControlText/0.05;\r\n}\r\n.close-button-img {\r\n\tfill: White;\r\n\twidth: 18px;\r\n\theight: 18px;\r\n\topacity: 0.8;\r\n}\r\n.content {\r\n\tdisplay: flex;\r\n\talign-items: center;\r\n\tflex-direction: column;\r\n\tpadding: 10px;\r\n}\r\n.message {\r\n\tmargin: 7px;\r\n\t\r\n}\r\n.icon {\r\n\twidth: 48px;\r\n\theight: 48px;\r\n\topacity: 0.8;\r\n}\r\n.text {\r\n\tcolor: @ControlText;\r\n\ttext-align: center;\r\n}\r\n.btn {\r\n\ttext-align: center;\r\n\tflex-direction: row;\r\n\tdisplay: flex;\r\n\tjustify-content: space-around;\r\n}\r\n.button {\r\n\tcolor: @Question;\r\n\tpadding: 8px 24px;\r\n\tborder: 1px solid @Question;\r\n\tborder-radius: 5px;\r\n}\r\n.button:hover {\r\n\tcolor: @White;\r\n\tbackground-color: @Question;\r\n\tbox-shadow: 0px 0px 10px @Question/0.5;\r\n}";
            htmlTemplateCollection1.AddRange(new DevExpress.Utils.Html.HtmlTemplate[] {
            htmlTemplate1});
            // 
            // htmlTemplate1
            // 

            DevExpress.Utils.SvgImageCollection svgImageCollection1 = new DevExpress.Utils.SvgImageCollection(components);

            svgImageCollection1.Add("del", "image://svgimages/diagramicons/del.svg");
            svgImageCollection1.Add("question", "image://svgimages/icon builder/actions_question.svg");

            var args = new XtraMessageBoxArgs();
            args.HtmlTemplate.Assign(htmlTemplate1);
            args.HtmlImages = svgImageCollection1;
            args.Caption = Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption");
            args.Text = sText;
            args.DefaultButtonIndex = 1;
            args.HtmlElementMouseClick += (s, argss) =>
            {
                //var status = argss.Element.ParentElement.ParentElement.ParentElement.FindElementById("frame");
                //status.ClearChildren();
                if (argss.HasClassName("save", false))
                {
                    result = 1;
                }
                else
                {
                    result = 0;
                }

            };
            //SoundPlayer warningSoundPlayer = new SoundPlayer("Template\\Sound\\info.wav");
            //warningSoundPlayer.Play();
            XtraMessageBox.Show(args);
            return result;
        }
        public void MsgError(string sText)
        {
            int result = 0;
            System.ComponentModel.IContainer components = null;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager();

            DevExpress.Utils.Html.HtmlTemplateCollection htmlTemplateCollection1 = new DevExpress.Utils.Html.HtmlTemplateCollection();
            DevExpress.Utils.Html.HtmlTemplate htmlTemplate1 = new DevExpress.Utils.Html.HtmlTemplate();


            htmlTemplate1.Name = "htmlTemplate1";
            htmlTemplate1.Template = "<div class=\"frame\" id=\"frame\">\r\n    <div class=\"header\">\r\n        <div class=\"caption\">${Caption}</div>\r\n    \t<div class=\"close-button\" id=\"closebutton\">\r\n\t\t\t<img src=\"del\" class=\"close-button-img\" id=\"close\">\r\n\t\t</div>\r\n    </div>\r\n    <div class=\"content\" id=\"content\">\r\n    \t<img src=\"error\" class=\"message icon\">\r\n    \t<div class=\"message text\">${MessageText}</div>\r\n    \t<div class=\"btn\">\r\n    \t\t<div class=\"message button\" tabindex=\"1\" id=\"dialogresult-ok\">OK</div>\r\n    \t</div>\r\n    </div>\r\n</div>";
            htmlTemplate1.Styles = "body{\t\r\n\tpadding: 20px;\r\n\tfont-size: 14px;\r\n\tfont-family: 'Segoe UI';\r\n}\r\n.frame {\r\n\twidth: 450px;\r\n\tcolor: @ControlText;\r\n\tbackground-color: @Window;\r\n\tborder: 1px solid #d64550;\r\n\tborder-radius: 16px;\r\n\tdisplay: flex;\r\n\tflex-direction: column;\r\n\tjustify-content: center;\r\n\tbox-shadow: 0px 8px 16px @#d64550/0.6;\r\n}\r\n.header {\r\n\tpadding: 8px;\r\n\tcolor: @White;\r\n\tbackground-color: #d64550;\r\n\tborder-radius: 15px 15px 0px 0px;\r\n\tdisplay: flex;\r\n\tjustify-content: space-between;\r\n\talign-items: center;\r\n}\r\n.caption {\r\n\tmargin: 0px 10px;\r\n\tfont-weight: bold;\r\n}\r\n.close-button {\r\n\tpadding: 8px;\r\n\tborder-radius: 5px;\r\n}\r\n.close-button:hover {\r\n\tbackground-color: @WindowText/0.1;\r\n}\r\n.close-button:active {\r\n\tbackground-color: @ControlText/0.05;\r\n}\r\n.close-button-img {\r\n\tfill: White;\r\n\twidth: 18px;\r\n\theight: 18px;\r\n\topacity: 0.8;\r\n}\r\n.content {\r\n\tdisplay: flex;\r\n\talign-items: center;\r\n\tflex-direction: column;\r\n\tpadding: 10px;\r\n}\r\n.message {\r\n\tmargin: 7px;\r\n\t\r\n}\r\n.icon {\r\n\twidth: 48px;\r\n\theight: 48px;\r\n\topacity: 0.8;\r\n}\r\n.text {\r\n\tcolor: @ControlText;\r\n\ttext-align: center;\r\n}\r\n.btn {\r\n\ttext-align: center;\r\n\tflex-direction: row;\r\n\tdisplay: flex;\r\n\tjustify-content: space-around;\r\n}\r\n.button {\r\n\tcolor: @#d64550;\r\n\tpadding: 8px 24px;\r\n\tborder: 1px solid @#d64550;\r\n\tborder-radius: 5px;\r\n}\r\n.button:hover {\r\n\tcolor: @White;\r\n\tbackground-color: @#d64550;\r\n\tbox-shadow: 0px 0px 10px @#d64550/0.5;\r\n}";
            htmlTemplateCollection1.AddRange(new DevExpress.Utils.Html.HtmlTemplate[] {
            htmlTemplate1});
            // 
            // htmlTemplate1
            // 

            DevExpress.Utils.SvgImageCollection svgImageCollection1 = new DevExpress.Utils.SvgImageCollection(components);

            svgImageCollection1.Add("del", "image://svgimages/diagramicons/del.svg");
            svgImageCollection1.Add("error", "image://svgimages/outlook inspired/highimportance.svg");

            var args = new XtraMessageBoxArgs();
            args.HtmlTemplate.Assign(htmlTemplate1);
            args.HtmlImages = svgImageCollection1;
            args.Caption = Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption");
            args.Text = sText;
            args.DefaultButtonIndex = 1;
            //SoundPlayer errorSoundPlayer = new SoundPlayer("Template\\Sound\\error.wav");
            //errorSoundPlayer.Play();
            XtraMessageBox.Show(args);
        }
        public void msgCapNhat(int loai) // 1 success, 2 error
        {
            try
            {
                if (loai == 1)
                {
                    Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgLuuThanhCong"), Commons.Form_Alert.enmType.Success);
                }
                else
                {
                    Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_LuuKhongThanhCong"), Commons.Form_Alert.enmType.Error);
                }
            }
            catch { }
        }
       
        public void OpenHinh(string strDuongdan)
        {
            if (strDuongdan.Equals(""))
                return;
            //strDuongdan = strDuongdan.Replace(@"\",@"\");
            if (System.IO.File.Exists(strDuongdan))
            {
                try
                {
                    System.Diagnostics.Process.Start(strDuongdan);
                }
                catch (Exception ex)
                {
                }
            }
        }
        public void Alert(string msg, Form_Alert.enmType type)
        {
            Form_Alert frm = new Form_Alert();
            frm.showAlert(msg, type);
        }
        public string returnSps(bool khach, string sPs)
        {
            if (khach == true)
            {
                sPs = sPs + "_K";
            }
            return sPs;
        }
        public string CapnhatTL(string strFile)
        {
            strFile = LocKyTuDB(strFile);
            string SERVER_FOLDER_PATH = "";
            string SERVER_PATH = "";
            SERVER_PATH = Commons.Modules.sDDTaiLieu;
            if (!System.IO.Directory.Exists(SERVER_PATH))
                SERVER_PATH = "";
            if (!SERVER_PATH.EndsWith(@"\"))
                SERVER_PATH = SERVER_PATH + @"\";
            SERVER_FOLDER_PATH = SERVER_PATH + strFile;
            if (!System.IO.Directory.Exists(SERVER_FOLDER_PATH))
            {
                System.IO.Directory.CreateDirectory(SERVER_FOLDER_PATH);
            }
            return SERVER_FOLDER_PATH;
        }
        public string CapnhatTL(string strFile, bool locKyTu)
        {
            if (locKyTu == true)
            {
                strFile = LocKyTuDB(strFile);
            }
            string SERVER_FOLDER_PATH = "";
            string SERVER_PATH = "";
            SERVER_PATH = Commons.Modules.sDDTaiLieu;
            if (!System.IO.Directory.Exists(SERVER_PATH))
                SERVER_PATH = "";
            if (!SERVER_PATH.EndsWith(@"\"))
                SERVER_PATH = SERVER_PATH + @"\";
            SERVER_FOLDER_PATH = SERVER_PATH + strFile;
            if (!System.IO.Directory.Exists(SERVER_FOLDER_PATH))
            {
                System.IO.Directory.CreateDirectory(SERVER_FOLDER_PATH);
            }
            return SERVER_FOLDER_PATH;
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
        public bool UpdateSQL(string folderPath)
        {
            try
            {
                bool status = true;
                if (!System.IO.Directory.Exists(folderPath)) // kiểm tra xem forder đã có chưa , nếu chưa có thì tạo 
                {
                    System.IO.Directory.CreateDirectory(folderPath);
                }

                DirectoryInfo directory = new DirectoryInfo(folderPath);
                FileInfo[] files = directory.GetFiles("*.sql"); // lấy các file có đuôi là .sql
                if (files.Length == 0) return status; // nếu không có file nào thì return luôn
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                foreach (FileInfo file in files)
                {
                    string script = File.ReadAllText(file.FullName); // đọc text từng file

                    SqlCommand command = new SqlCommand(script, conn);
                    try
                    {
                        command.ExecuteNonQuery(); // chạy store
                        file.Delete(); // sau khi chạy xong thì xóa file
                        WriteLog("Executed script: " + file.Name + ""); // in ra kết quả
                    }
                    catch (Exception ex)
                    {
                        WriteLog(file.Name + " - " + ex.Message); // nếu sai sẽ in ra lỗi của câu store
                        file.Delete(); // báo lỗi thì xóa file luôn 
                        status = false;
                    }
                }
                return status;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                return false;
            }
        }
        public string STTFileCungThuMuc(string sThuMuc, string sFile, string sFileGoc)
        {
            string TenFile = sFile;
            string DuoiFile;
            try
            {
                DuoiFile = LayDuoiFile(sFile);
            }
            catch
            {
                DuoiFile = "";
            }
            try
            {
                string[] sTongFile;
                int i = 1;

                TenFile = sFile;
                try
                {
                    sTongFile = System.IO.Directory.GetFiles(sThuMuc);  //truong hop thu muc kg có file nao, catch va lay lun gten file do


                    List<string> filterKeywords = new List<string>() { sFileGoc };
                    List<string> sRe;
                    var result = from p in sTongFile
                                 where filterKeywords.Any(val => p.Contains(val))
                                 select p;

                    sRe = result.ToList();
                    TenFile = sFileGoc + "." + (sRe.Count + 1).ToString().PadLeft(3, '0') + System.IO.Path.GetExtension(sFile);
                }
                catch
                { }
            }
            catch (Exception ex)
            {
                TenFile = "";
            }

            return TenFile;
        }

        public bool LuuDuongDan(string strDUONG_DAN, string strHINH, string FormThuMuc)
        {
            string folderLocation = Commons.Modules.sDDTaiLieu + '\\' + FormThuMuc;
            string folderLocationFile = folderLocation + '\\' + strHINH;
            bool exists = System.IO.Directory.Exists(folderLocation);
            if (!exists)
            {
                System.IO.Directory.CreateDirectory(folderLocation);
            }
            if (!File.Exists(folderLocationFile))
            {
                if (System.IO.File.Exists(strDUONG_DAN))
                {
                    System.IO.File.Copy(strDUONG_DAN, folderLocation + '\\' + strHINH, true);
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public string FileCopy(string strDUONG_DAN, string strHINH, string FormThuMuc)
        {

            try
            {
                string folderLocation = Commons.Modules.sDDTaiLieu + '\\' + FormThuMuc;
                string folderLocationFile = folderLocation + '\\' + strHINH;
                bool exists = System.IO.Directory.Exists(folderLocation);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(folderLocation);
                }
                if (!File.Exists(folderLocationFile))
                {
                    try
                    {
                        System.IO.File.Copy(strDUONG_DAN + '\\' + strHINH, folderLocationFile, true);

                    }
                    catch
                    {
                    }
                }
                else
                {
                    folderLocationFile = "";
                }
                return folderLocationFile;
            }
            catch
            {
                return "";
            }
        }


        public void LuuDuongDan(string strDUONG_DAN, string strHINH)
        {
            if (strHINH.Equals(""))
                return;


            if (System.IO.File.Exists(strDUONG_DAN) & !System.IO.File.Exists(strHINH))
            {
                try
                {
                    //DirectoryInfo dir = new DirectoryInfo(System.IO.Path.GetDirectoryName(strHINH));
                    //foreach (FileInfo item in dir.EnumerateFiles())
                    //{
                    //    item.Delete();
                    //}
                    if (!System.IO.File.Exists(strHINH))
                    {
                        System.IO.File.Copy(strDUONG_DAN, strHINH);
                    }
                }
                catch
                {
                }
            }
        }

        public static void SetTimeEditFormat(TimeEdit timeEdit)
        {
            timeEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            timeEdit.Properties.DisplayFormat.FormatString = "HH:mm:ss";
            timeEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            timeEdit.Properties.EditFormat.FormatString = "HH:mm:ss";
            timeEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
        }

        public DateTime setDate1Month(DateTime date, int iLoai) // 0 ngay dau thang, 1 ngay cuoi thang
        {
            try
            {
                DateTime dNgaydauThang = new DateTime(date.Year, date.Month, 1);
                if (iLoai == 0)
                {
                    return dNgaydauThang;
                }
                else
                {
                    return dNgaydauThang.AddMonths(+1).AddDays(-1);
                }
            }
            catch
            {
                return DateTime.Now;
            }

        }


        public string KyHieuDV(Int64 ID_DV)
        {
            string KyHieuDV = "";
            try
            {

                string strSQL = "SELECT TOP 1 KY_HIEU FROM dbo.DON_VI WHERE (ID_DV = " + Convert.ToInt64(ID_DV) + " OR " + Convert.ToInt64(ID_DV) + " = -1)";
                KyHieuDV = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, string.IsNullOrEmpty(strSQL) ? "" : strSQL).ToString();
            }
            catch
            {
                if (Commons.Modules.ObjSystems.DataThongTinChung(-1).Rows[0]["KY_HIEU_DV"].ToString() == "MT")
                {
                    return KyHieuDV = "MT";
                }
                else
                {
                    return KyHieuDV = "SB";
                }
            }
            return KyHieuDV;
        }

        public void AddDropDownExcel(Microsoft.Office.Interop.Excel._Worksheet oSheet, Microsoft.Office.Interop.Excel.Range range, DataTable dtDuLieu, string sCotDuLieu)
        {
            try
            {
                var list = new System.Collections.Generic.List<string>();
                for (int i = 0; i < dtDuLieu.Rows.Count; i++)
                {
                    list.Add(dtDuLieu.Rows[i][sCotDuLieu].ToString().Trim());
                }
                var flatList = string.Join(",", list.ToArray());

                range.Validation.Delete();
                range.Validation.Add(
                   Microsoft.Office.Interop.Excel.XlDVType.xlValidateList,
                   Microsoft.Office.Interop.Excel.XlDVAlertStyle.xlValidAlertInformation,
                   Microsoft.Office.Interop.Excel.XlFormatConditionOperator.xlBetween,
                   flatList,
                   Type.Missing);
                range.Validation.IgnoreBlank = true;
                range.Validation.InCellDropdown = true;
                range.Validation.ErrorMessage = "Dữ liệu bạn nhập không đúng bạn có muốn tiếp tục?";
                range.Validation.ShowError = true;
                range.Validation.ErrorTitle = "Nhập sai dữ liệu";
            }
            catch { }
        }
        public string CharacterIncrement(int colCount)
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

        public bool setCheckImport(int iLoai) // iLoai = 1 Update khi mở form, 0 UPDATE = NULL  khi tắt form
        {
            try
            {
                string MName = "";
                try { MName = Environment.MachineName; } catch { }
                string strSQL = "";
                if (iLoai == 1)
                {
                    strSQL = "UPDATE dbo.THONG_TIN_CHUNG SET CHECK_IMPORT = CONVERT(NVARCHAR(20),GETDATE(),20) + N'," + MName + "' + N'," + Commons.Modules.UserName + "'";
                }
                else
                {
                    strSQL = "UPDATE dbo.THONG_TIN_CHUNG SET CHECK_IMPORT = NULL";
                }
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, strSQL);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public string getCheckImport()
        {
            string sName = "";
            try
            {
                return sName = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT CHECK_IMPORT FROM dbo.THONG_TIN_CHUNG").ToString();
            }
            catch { return sName = ""; }
        }
        public bool kiemTrungMS(string sTableName, string sDieuKien, string sValue)
        {
            try
            {
                if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.[" + sTableName + "] WHERE " + sDieuKien + " = N'" + sValue + "'")) > 0)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool checkVerDemo(Int64 idCustomer, Int64 idContract, int LoaiSP, out DateTime dNgayHH)
        {
            DateTime dNgay = DateTime.Now;
            dNgayHH = dNgay;
            try
            {
                DataTable dt = new DataTable();
                dt = getDataAPI("https://api.vietsoft.com.vn/VS.Api/Support/getLicense?NNgu=0&idCustomer=" + idCustomer + "&idContract=" + idContract + "&ID_LSP=" + LoaiSP + "");
                try
                {
                    dNgayHH = Convert.ToDateTime(dt.Rows[0]["NGAY_DEMO"]);
                }
                catch { }

                if (Convert.ToBoolean(dt.Rows[0]["HH_DEMO"]))
                    return true;
                else
                    return false;
            }
            catch
            {
                return true;
            }
        }

        public DataTable DataFocusRows(DataTable data, DevExpress.XtraGrid.Views.Grid.GridView grv)
        {
            DataTable dt = new DataTable();
            try
            {
                DataRow dr;
                DataRow row;
                dt = data.Clone();
                Int32[] selectedRowHandles = grv.GetSelectedRows();
                for (int i = 0; i < selectedRowHandles.Length; i++)
                {
                    int selectedRowHandle = selectedRowHandles[i];
                    if (selectedRowHandle >= 0)
                    {
                        dr = grv.GetDataRow(selectedRowHandle);
                        row = dt.NewRow();
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            row[dt.Columns[j].ColumnName] = dr[dt.Columns[j].ColumnName];
                        }
                        dt.Rows.Add(row);
                    }
                }
                return dt;
            }
            catch { return dt = null; }
        }

        public string KyHieuDV_CN(Int64 ID_CN)
        {
            string KyHieuDV = "";
            try
            {
                string strSQL = "SELECT DV.KY_HIEU FROM dbo.DON_VI DV INNER JOIN dbo.XI_NGHIEP XN ON XN.ID_DV = DV.ID_DV INNER JOIN dbo.[TO] T ON T.ID_XN = XN.ID_XN INNER JOIN dbo.CONG_NHAN CN ON T.ID_TO = CN.ID_TO WHERE CN.ID_CN = " + ID_CN + "";
                KyHieuDV = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, string.IsNullOrEmpty(strSQL) ? "" : strSQL).ToString();
            }
            catch { return KyHieuDV; }
            return KyHieuDV;
        }

        public static void SetDateRepositoryItemDateEdit(RepositoryItemDateEdit dateEdit)
        {
            dateEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dateEdit.DisplayFormat.FormatString = "dd/MM/yyyy";
            dateEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dateEdit.EditFormat.FormatString = "dd/MM/yyyy";
            dateEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            dateEdit.Mask.EditMask = "dd/MM/yyyy";
        }
        public int getTinhTrangLuongThang(DateTime dThang, Int64 ID_DV)
        {
            try
            {
                int resulst = 0;
                string sSql = "";
                sSql = "SELECT ISNULL(TINH_TRANG,1) TINH_TRANG FROM dbo.BANG_LUONG_DM_CHA WHERE THANG = (SELECT DATEADD(month, DATEDIFF(month, 0, '" + dThang.ToString("yyyyMMdd") + "'), 0)) AND ID_DV = " + ID_DV + "";
                resulst = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql)); //* Commons.Modules.iGio
                return resulst;
            }
            catch
            {
                return 0;
            }
        }

        public int TinhSoNgayTruLeChuNhat(DateTime TNgay, DateTime DNgay)
        {
            try
            {
                int resulst = 0;

                string sSql = "";
                sSql = "SELECT [dbo].[fnGetSoNgayTruLeChuNhat]('" + Convert.ToDateTime(TNgay).ToString("yyyyMMdd") + "','" + Convert.ToDateTime(DNgay).ToString("yyyyMMdd") + "')";
                resulst = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql)); //* Commons.Modules.iGio
                return resulst;
            }
            catch
            {
                return 0;
            }
        }
        public IEnumerable<Control> GetAllConTrol(Control control, IEnumerable<Type> filteringTypes)
        {
            var ctrls = control.Controls.Cast<Control>();

            return ctrls.SelectMany(ctrl => GetAllConTrol(ctrl, filteringTypes))
                        .Concat(ctrls)
                        .Where(ctl => filteringTypes.Any(t => ctl.GetType() == t));
        }

        #region LoadLookupedit
        public bool MLoadLookUpEdit(DevExpress.XtraEditors.LookUpEdit cbo, string sQuery, string Ma, string Ten, string TenCot)
        {
            try
            {
                cbo.Properties.DataSource = null;
                cbo.Properties.DisplayMember = "";
                cbo.Properties.ValueMember = "";
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, CommandType.Text, sQuery));
                cbo.Properties.DataSource = dtTmp;
                cbo.Properties.DisplayMember = Ten;
                cbo.Properties.ValueMember = Ma;
                cbo.Properties.Columns.Clear();
                cbo.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(Ten));
                cbo.Properties.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cbo.Properties.BestFitMode = BestFitMode.BestFit;
                cbo.Properties.SearchMode = SearchMode.AutoComplete;
                cbo.EditValue = dtTmp.Rows[0][Ma];
                if (dtTmp.Rows.Count > 10)
                    cbo.Properties.DropDownRows = 15;
                else
                    cbo.Properties.DropDownRows = 10;
                cbo.Properties.Columns[Ten].Caption = TenCot;
                if (TenCot.Trim() == "")
                    cbo.Properties.ShowHeader = false;
                else
                    cbo.Properties.ShowHeader = true;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool MLoadComboboxEdit(DevExpress.XtraEditors.ComboBoxEdit cbo, DataTable dt, string cot)
        {
            try
            {
                cbo.Properties.Items.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    cbo.Properties.Items.Add(item[cot]);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool MLoadComboboxEdit(DevExpress.XtraEditors.ComboBoxEdit cbo, DataRow[] dr, string cot)
        {
            try
            {
                cbo.Properties.Items.Clear();
                foreach (DataRow item in dr)
                {
                    cbo.Properties.Items.Add(item[cot]);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        //load lookup edit 
        public bool MLoadLookUpEditN(DevExpress.XtraEditors.LookUpEdit cbo, DataTable dtTmp, string Ma, string Ten, string TenCot, string GiaTri)
        {
            try
            {
                cbo.Properties.DataSource = null;
                cbo.Properties.DataSource = dtTmp;
                cbo.Properties.DisplayMember = Ten;
                cbo.Properties.ValueMember = Ma;
                cbo.Properties.Columns.Clear();
                cbo.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(Ten));
                cbo.Properties.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cbo.Properties.BestFitMode = BestFitMode.BestFit;
                cbo.Properties.SearchMode = SearchMode.AutoComplete;
                if (GiaTri != "") cbo.EditValue = dtTmp.Rows[0][Ma];
                if (dtTmp.Rows.Count > 10)
                    cbo.Properties.DropDownRows = 15;
                else
                    cbo.Properties.DropDownRows = 10;
                cbo.Properties.Columns[Ten].Caption = TenCot;
                if (TenCot.Trim() == "")
                    cbo.Properties.ShowHeader = false;
                else
                    cbo.Properties.ShowHeader = true;
                cbo.Properties.Columns[Ten].Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, "LookUpEdit", Ten, Modules.TypeLanguage);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool MLoadLookUpEditN(DevExpress.XtraEditors.LookUpEdit cbo, DataTable dtTmp, string Ma, string Ten, string TenCot, string GiaTri, bool CoNull)
        {
            try
            {
                if (CoNull)
                {
                    DataRow row = dtTmp.NewRow();
                    row[0] = -99;
                    row[1] = "";
                    dtTmp.Rows.InsertAt(row, 0);
                }
                cbo.Properties.DataSource = null;
                cbo.Properties.DataSource = dtTmp;
                cbo.Properties.DisplayMember = Ten;
                cbo.Properties.ValueMember = Ma;
                cbo.Properties.Columns.Clear();
                cbo.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(Ten));
                cbo.Properties.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cbo.Properties.BestFitMode = BestFitMode.BestFit;
                cbo.Properties.SearchMode = SearchMode.AutoComplete;
                if (GiaTri != "") cbo.EditValue = GiaTri;

                if (dtTmp.Rows.Count > 10)
                    cbo.Properties.DropDownRows = 15;
                else
                    cbo.Properties.DropDownRows = 10;
                cbo.Properties.Columns[Ten].Caption = TenCot;
                if (TenCot.Trim() == "")
                    cbo.Properties.ShowHeader = false;
                else
                    cbo.Properties.ShowHeader = true;
                cbo.Properties.Columns[Ten].Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, "LookUpEdit", Ten, Modules.TypeLanguage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool MLoadLookUpEdit(DevExpress.XtraEditors.LookUpEdit cbo, DataTable dtTmp, string Ma, string Ten, string TenCot)
        {
            try
            {
                cbo.Properties.DataSource = null;
                cbo.Properties.DataSource = dtTmp;
                cbo.Properties.DisplayMember = Ten;
                cbo.Properties.ValueMember = Ma;
                cbo.Properties.Columns.Clear();
                cbo.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(Ten));
                cbo.Properties.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cbo.Properties.BestFitMode = BestFitMode.BestFit;
                cbo.Properties.SearchMode = SearchMode.AutoComplete;
                cbo.EditValue = dtTmp.Rows[0][Ma];
                if (dtTmp.Rows.Count > 10)
                    cbo.Properties.DropDownRows = 15;
                else
                    cbo.Properties.DropDownRows = 10;
                cbo.Properties.Columns[Ten].Caption = TenCot;
                if (TenCot.Trim() == "")
                    cbo.Properties.ShowHeader = false;
                else
                    cbo.Properties.ShowHeader = true;
                cbo.Properties.Columns[Ten].Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, "LookUpEdit", Ten, Modules.TypeLanguage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool MLoadLookUpEdit(DevExpress.XtraEditors.LookUpEdit cbo, DataTable dtTmp, string Ma, string Ten, string TenCot, bool CoNull)
        {
            try
            {
                if (CoNull)
                    dtTmp.Rows.Add(-99, "");
                dtTmp.DefaultView.Sort = "" + Ma + " ASC ";
                cbo.Properties.DataSource = null;
                //cbo.Properties.DisplayMember = "";
                //cbo.Properties.ValueMember = "";
                cbo.Properties.DataSource = dtTmp;
                cbo.Properties.DisplayMember = Ten;
                cbo.Properties.ValueMember = Ma;
                cbo.Properties.Columns.Clear();
                cbo.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(Ten));
                cbo.Properties.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cbo.Properties.BestFitMode = BestFitMode.BestFit;
                cbo.Properties.SearchMode = SearchMode.AutoComplete;
                if (CoNull)
                    cbo.EditValue = dtTmp.Rows[dtTmp.Rows.Count - 1][Ma];
                else
                    cbo.EditValue = dtTmp.Rows[0][Ma];
                if (dtTmp.Rows.Count > 10)
                    cbo.Properties.DropDownRows = 15;
                else
                    cbo.Properties.DropDownRows = 10;
                cbo.Properties.Columns[Ten].Caption = TenCot;
                if (TenCot.Trim() == "")
                    cbo.Properties.ShowHeader = false;
                else
                    cbo.Properties.ShowHeader = true;
                cbo.Properties.Columns[Ten].Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, "LookUpEdit", Ten, Modules.TypeLanguage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }




        public bool MLoadLookUpEdit(DevExpress.XtraEditors.LookUpEdit cbo, string sStored, string Ma, string Ten, string TenCot, bool bStored)
        {
            try
            {
                DataTable dtTmp = new DataTable();
                if (bStored)
                    dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, sStored));
                else
                    dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, CommandType.Text, sStored));
                cbo.Properties.DataSource = null;
                cbo.Properties.DisplayMember = "";
                cbo.Properties.ValueMember = "";
                cbo.Properties.DataSource = dtTmp;
                cbo.Properties.DisplayMember = Ten;
                cbo.Properties.ValueMember = Ma;
                cbo.Properties.Columns.Clear();
                cbo.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(Ten));
                cbo.Properties.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cbo.Properties.BestFitMode = BestFitMode.BestFit;
                cbo.Properties.SearchMode = SearchMode.AutoComplete;
                cbo.EditValue = dtTmp.Rows[0][Ma];
                if (dtTmp.Rows.Count > 10)
                    cbo.Properties.DropDownRows = 15;
                else
                    cbo.Properties.DropDownRows = 10;
                cbo.Properties.Columns[Ten].Caption = TenCot;
                if (TenCot.Trim() == "")
                    cbo.Properties.ShowHeader = false;
                else
                    cbo.Properties.ShowHeader = true;
                cbo.Properties.Columns[Ten].Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, "LookUpEdit", Ten, Modules.TypeLanguage);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool MLoadLookUpEdit(DevExpress.XtraEditors.LookUpEdit cbo, string sStored, string Ma, string Ten, string TenCot, bool bStored, string Param)
        {
            try
            {
                DataTable dtTmp = new DataTable();
                if (bStored)
                    dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, sStored, Param));
                else
                    dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, CommandType.Text, sStored));
                cbo.Properties.DataSource = null;
                cbo.Properties.DisplayMember = "";
                cbo.Properties.ValueMember = "";

                cbo.Properties.DataSource = dtTmp;
                cbo.Properties.DisplayMember = Ten;
                cbo.Properties.ValueMember = Ma;
                cbo.Properties.Columns.Clear();
                cbo.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(Ten));
                cbo.Properties.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cbo.Properties.BestFitMode = BestFitMode.BestFit;
                cbo.Properties.SearchMode = SearchMode.AutoComplete;
                cbo.EditValue = dtTmp.Rows[0][Ma];
                if (dtTmp.Rows.Count > 10)
                    cbo.Properties.DropDownRows = 15;
                else
                    cbo.Properties.DropDownRows = 10;
                cbo.Properties.Columns[Ten].Caption = TenCot;
                if (TenCot.Trim() == "")
                    cbo.Properties.ShowHeader = false;
                else
                    cbo.Properties.ShowHeader = true;
                cbo.Properties.Columns[Ten].Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, "LookUpEdit", Ten, Modules.TypeLanguage);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool MLoadLookUpEdit(DevExpress.XtraEditors.LookUpEdit cbo, string sStored, string Ma, string Ten, string TenCot, bool bStored, string Param, string Param1)
        {
            try
            {
                DataTable dtTmp = new DataTable();
                if (bStored)
                    dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, sStored, Param, Param1));
                else
                    dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, CommandType.Text, sStored));
                cbo.Properties.DataSource = null;
                cbo.Properties.DisplayMember = "";
                cbo.Properties.ValueMember = "";
                cbo.Properties.DataSource = dtTmp;
                cbo.Properties.DisplayMember = Ten;
                cbo.Properties.ValueMember = Ma;
                cbo.Properties.Columns.Clear();
                cbo.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(Ten));
                cbo.Properties.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cbo.Properties.BestFitMode = BestFitMode.BestFit;
                cbo.Properties.SearchMode = SearchMode.AutoComplete;
                cbo.EditValue = dtTmp.Rows[0][Ma];
                if (dtTmp.Rows.Count > 10)
                    cbo.Properties.DropDownRows = 15;
                else
                    cbo.Properties.DropDownRows = 10;
                cbo.Properties.Columns[Ten].Caption = TenCot;
                if (TenCot.Trim() == "")
                    cbo.Properties.ShowHeader = false;
                else
                    cbo.Properties.ShowHeader = true;
                cbo.Properties.Columns[Ten].Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, "LookUpEdit", Ten, Modules.TypeLanguage);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool MLoadLookUpEditNoRemove(DevExpress.XtraEditors.LookUpEdit cbo, string sQuery, string Ma, string Ten, string TenCot)
        {
            try
            {
                cbo.Properties.DataSource = null;
                cbo.Properties.DisplayMember = "";
                cbo.Properties.ValueMember = "";

                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, CommandType.Text, sQuery));
                cbo.Properties.DataSource = dtTmp;
                cbo.Properties.DisplayMember = Ten;
                cbo.Properties.ValueMember = Ma;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cbo.Properties.BestFitMode = BestFitMode.BestFit;
                cbo.Properties.SearchMode = SearchMode.AutoComplete;
                cbo.EditValue = dtTmp.Rows[0][Ma];
                if (dtTmp.Rows.Count > 10)
                    cbo.Properties.DropDownRows = 15;
                else
                    cbo.Properties.DropDownRows = 10;
                cbo.Properties.Columns[Ten].Caption = TenCot;
                if (TenCot.Trim() == "")
                    cbo.Properties.ShowHeader = false;
                else
                    cbo.Properties.ShowHeader = true;
                cbo.Properties.Columns[Ten].Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, "LookUpEdit", Ten, Modules.TypeLanguage);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool MLoadLookUpEditNoRemove(DevExpress.XtraEditors.LookUpEdit cbo, DataTable dtTmp, string Ma, string Ten, string TenCot)
        {
            try
            {
                cbo.Properties.DataSource = null;
                cbo.Properties.DisplayMember = "";
                cbo.Properties.ValueMember = "";
                cbo.Properties.DataSource = dtTmp;
                cbo.Properties.DisplayMember = Ten;
                cbo.Properties.ValueMember = Ma;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cbo.Properties.BestFitMode = BestFitMode.BestFit;
                cbo.Properties.SearchMode = SearchMode.AutoComplete;
                cbo.EditValue = dtTmp.Rows[0][Ma];
                cbo.Properties.Columns.Clear();
                DevExpress.XtraEditors.Controls.LookUpColumnInfo column;
                for (int intColumn = 0; intColumn <= dtTmp.Columns.Count - 1; intColumn++)
                {
                    column = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
                    //column.Caption = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, sForm, dtTmp.Columns(intColumn).ColumnName, Commons.Modules.TypeLanguage);
                    column.FieldName = dtTmp.Columns[intColumn].ColumnName;
                    cbo.Properties.Columns.Add(column);
                }


                if (dtTmp.Rows.Count > 10)
                    cbo.Properties.DropDownRows = 15;
                else
                    cbo.Properties.DropDownRows = 10;
                cbo.Properties.Columns[Ten].Caption = TenCot;
                if (TenCot.Trim() == "")
                    cbo.Properties.ShowHeader = false;
                else
                    cbo.Properties.ShowHeader = true;


                cbo.Properties.Columns[Ten].Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, "LookUpEdit", Ten, Modules.TypeLanguage);


                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool MLoadLookUpEditNoRemove(DevExpress.XtraEditors.LookUpEdit cbo, string sStored, string Ma, string Ten, string TenCot, bool bStored)
        {
            try
            {
                DataTable dtTmp = new DataTable();
                if (bStored)
                    dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, sStored));
                else
                    dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, CommandType.Text, sStored));
                cbo.Properties.DataSource = null;
                cbo.Properties.DisplayMember = "";
                cbo.Properties.ValueMember = "";
                cbo.Properties.DataSource = dtTmp;
                cbo.Properties.DisplayMember = Ten;
                cbo.Properties.ValueMember = Ma;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cbo.Properties.BestFitMode = BestFitMode.BestFit;
                cbo.Properties.SearchMode = SearchMode.AutoComplete;
                cbo.EditValue = dtTmp.Rows[0][Ma];
                if (dtTmp.Rows.Count > 10)
                    cbo.Properties.DropDownRows = 15;
                else
                    cbo.Properties.DropDownRows = 10;
                cbo.Properties.Columns[Ten].Caption = TenCot;
                if (TenCot.Trim() == "")
                    cbo.Properties.ShowHeader = false;
                else
                    cbo.Properties.ShowHeader = true;
                cbo.Properties.Columns[Ten].Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, "LookUpEdit", Ten, Modules.TypeLanguage);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool MLoadLookUpEditNoRemove(DevExpress.XtraEditors.LookUpEdit cbo, string sStored, string Ma, string Ten, string TenCot, bool bStored, string Param)
        {
            try
            {
                DataTable dtTmp = new DataTable();
                if (bStored)
                    dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, sStored, Param));
                else
                    dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, CommandType.Text, sStored));
                cbo.Properties.DataSource = null;
                cbo.Properties.DisplayMember = "";
                cbo.Properties.ValueMember = "";

                cbo.Properties.DataSource = dtTmp;
                cbo.Properties.DisplayMember = Ten;
                cbo.Properties.ValueMember = Ma;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cbo.Properties.BestFitMode = BestFitMode.BestFit;
                cbo.Properties.SearchMode = SearchMode.AutoComplete;
                cbo.EditValue = dtTmp.Rows[0][Ma];
                if (dtTmp.Rows.Count > 10)
                    cbo.Properties.DropDownRows = 15;
                else
                    cbo.Properties.DropDownRows = 10;
                cbo.Properties.Columns[Ten].Caption = TenCot;
                if (TenCot.Trim() == "")
                    cbo.Properties.ShowHeader = false;
                else
                    cbo.Properties.ShowHeader = true;
                cbo.Properties.Columns[Ten].Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, "LookUpEdit", Ten, Modules.TypeLanguage);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool MLoadLookUpEditNoRemove(DevExpress.XtraEditors.LookUpEdit cbo, string sStored, string Ma, string Ten, string TenCot, bool bStored, string Param, string Param1)
        {
            try
            {
                DataTable dtTmp = new DataTable();
                if (bStored)
                    dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, sStored, Param, Param1));
                else
                    dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, CommandType.Text, sStored));
                cbo.Properties.DataSource = null;
                cbo.Properties.DisplayMember = "";
                cbo.Properties.ValueMember = "";
                cbo.Properties.DataSource = dtTmp;
                cbo.Properties.DisplayMember = Ten;
                cbo.Properties.ValueMember = Ma;

                cbo.Properties.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cbo.Properties.BestFitMode = BestFitMode.BestFit;
                cbo.Properties.SearchMode = SearchMode.AutoComplete;
                cbo.EditValue = dtTmp.Rows[0][Ma];
                if (dtTmp.Rows.Count > 10)
                    cbo.Properties.DropDownRows = 15;
                else
                    cbo.Properties.DropDownRows = 10;
                cbo.Properties.Columns[Ten].Caption = TenCot;
                if (TenCot.Trim() == "")
                    cbo.Properties.ShowHeader = false;
                else
                    cbo.Properties.ShowHeader = true;
                cbo.Properties.Columns[Ten].Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, "LookUpEdit", Ten, Modules.TypeLanguage);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region AutoComplete

        public bool MAutoCompleteTextEdit(DevExpress.XtraEditors.TextEdit txt, string sQuery, string Ma)
        {
            try
            {
                txt.MaskBox.AutoCompleteCustomSource = null;
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, CommandType.Text, sQuery));
                string[] postSource;
                dtTmp = dtTmp.DefaultView.ToTable(true, Ma);
                postSource = dtTmp.Rows.Cast<DataRow>().Select(dr => dr[Ma].ToString()).ToArray();
                var source = new AutoCompleteStringCollection();
                source.AddRange(postSource);
                txt.MaskBox.AutoCompleteCustomSource = source;
                txt.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txt.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool MAutoCompleteMemoEdit(DevExpress.XtraEditors.MemoEdit txt, DataTable dtData, string Ma)
        {
            try
            {
                txt.MaskBox.AutoCompleteCustomSource = null;
                string[] postSource;
                dtData = dtData.DefaultView.ToTable(true, Ma);
                postSource = dtData.Rows.Cast<DataRow>().Select(dr => dr[Ma].ToString()).ToArray();
                var source = new AutoCompleteStringCollection();
                source.AddRange(postSource);
                txt.MaskBox.AutoCompleteCustomSource = source;
                txt.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txt.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool MAutoCompleteTextEdit(DevExpress.XtraEditors.TextEdit txt, DataTable dtData, string Ma)
        {
            try
            {
                txt.MaskBox.AutoCompleteCustomSource = null;
                string[] postSource;
                dtData = dtData.DefaultView.ToTable(true, Ma);
                postSource = dtData.Rows.Cast<DataRow>().Select(dr => dr[Ma].ToString()).ToArray();
                var source = new AutoCompleteStringCollection();
                source.AddRange(postSource);
                txt.MaskBox.AutoCompleteCustomSource = source;
                txt.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txt.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                return true;
            }
            catch
            {
                return false;
            }
        }

        //public bool MAutoCompleteTextEdit(DevExpress.XtraEditors.Tẽt txt, DataTable dtData, string Ma)
        //{
        //    try
        //    {
        //        txt.MaskBox.AutoCompleteCustomSource = null;
        //        string[] postSource;
        //        dtData = dtData.DefaultView.ToTable(true, Ma);
        //        postSource = dtData.Rows.Cast<DataRow>().Select(dr => dr[Ma].ToString()).ToArray();
        //        var source = new AutoCompleteStringCollection();
        //        source.AddRange(postSource);
        //        txt.MaskBox.AutoCompleteCustomSource = source;
        //        txt.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
        //        txt.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        #endregion

        #region Load xtraserch
        //public void MLoadSearchLookUpEdit(DevExpress.XtraEditors.SearchLookUpEdit cbo, DataTable dtTmp, string Ma, string Ten, string TenCot, bool isNgonNgu = true, bool CoNull = false, bool GanGT = true, string GiaTri = "")
        //{
        //    try
        //    {
        //        if (CoNull)
        //        {
        //            DataRow row = dtTmp.NewRow();
        //            row[0] = -99;
        //            row[1] = "";
        //            dtTmp.Rows.InsertAt(row, 0);
        //        }
        //        cbo.Properties.DataSource = null;
        //        cbo.Properties.DisplayMember = "";
        //        cbo.Properties.ValueMember = "";
        //        //cbo.BindingContext = new BindingContext();
        //        cbo.Properties.DataSource = dtTmp;
        //        cbo.Properties.DisplayMember = Ten;
        //        cbo.Properties.ValueMember = Ma;
        //        cbo.Properties.BestFitMode = BestFitMode.BestFit;
        //        if (GanGT)
        //        {
        //            if (GiaTri == "")
        //            {
        //                if (CoNull)
        //                    cbo.EditValue = dtTmp.Rows[dtTmp.Rows.Count - 1][Ma];
        //                else
        //                    cbo.EditValue = dtTmp.Rows[0][Ma];
        //            }
        //            else
        //            {
        //                cbo.EditValue = GiaTri;
        //            }
        //        }

        //        cbo.Properties.PopulateViewColumns();
        //        cbo.Properties.View.Columns[0].Visible = false;
        //        cbo.Properties.View.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //        cbo.Properties.View.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
        //        cbo.Properties.View.Appearance.HeaderPanel.Options.UseTextOptions = true;
        //        if (isNgonNgu)
        //        {
        //            DevExpress.XtraGrid.Views.Grid.GridView grv = (DevExpress.XtraGrid.Views.Grid.GridView)cbo.Properties.PopupView;
        //            foreach (DevExpress.XtraGrid.Columns.GridColumn col in grv.Columns)
        //            {
        //                if (col.Visible)
        //                {
        //                    col.Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, "SearchLookUpEdit", col.FieldName, Modules.TypeLanguage);
        //                }
        //            }
        //            cbo.Refresh();
        //        }

        //    }
        //    catch
        //    {

        //    }
        //}

        public void MLoadSearchLookUpEdit(DevExpress.XtraEditors.SearchLookUpEdit cbo, DataTable dtTmp, string Ma, string Ten, string TenCot, bool isNgonNgu = true, bool CoNull = false, bool GanGT = true, string GiaTri = "")
        {
            try
            {
                if (CoNull)
                {
                    DataRow row = dtTmp.NewRow();
                    row[0] = -99;
                    row[1] = "";
                    try
                    {
                        dtTmp.Rows.InsertAt(row, 0);
                    }
                    catch { }
                }
                cbo.Properties.DataSource = null;
                cbo.Properties.DisplayMember = "";
                cbo.Properties.ValueMember = "";
                //cbo.BindingContext = new BindingContext();
                cbo.Properties.DataSource = dtTmp;
                cbo.Properties.DisplayMember = Ten;
                cbo.Properties.ValueMember = Ma;
                cbo.Properties.BestFitMode = BestFitMode.BestFit;
                if (GanGT)
                {
                    if (GiaTri == "")
                    {
                        if (CoNull)
                            cbo.EditValue = dtTmp.Rows[dtTmp.Rows.Count - 1][Ma];
                        else
                            cbo.EditValue = dtTmp.Rows[0][Ma];
                    }
                    else
                    {
                        cbo.EditValue = GiaTri;
                    }
                }

                cbo.Properties.PopulateViewColumns();
                cbo.Properties.View.Columns[0].Visible = false;
                cbo.Properties.View.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cbo.Properties.View.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cbo.Properties.View.Appearance.HeaderPanel.Options.UseTextOptions = true;
                if (isNgonNgu)
                {
                    DevExpress.XtraGrid.Views.Grid.GridView grv = (DevExpress.XtraGrid.Views.Grid.GridView)cbo.Properties.PopupView;
                    foreach (DevExpress.XtraGrid.Columns.GridColumn col in grv.Columns)
                    {
                        if (col.Visible)
                        {
                            col.Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, "SearchLookUpEdit", col.FieldName, Modules.TypeLanguage);
                        }
                    }
                    cbo.Refresh();
                }

            }
            catch
            {
                cbo.EditValue = -1;
            }
        }

        public void AddCombSearchLookUpEdit(RepositoryItemSearchLookUpEdit cboSearch, string Value, string Display, string cot, GridView grv, DataTable dtTmp, string form)
        {
            cboSearch.NullText = "";
            cboSearch.ValueMember = Value;
            cboSearch.DisplayMember = Display;
            cboSearch.DataSource = dtTmp;
            cboSearch.View.PopulateColumns(cboSearch.DataSource);
            cboSearch.View.Columns[Value].Visible = false;

            Commons.Modules.ObjSystems.MLoadNNXtraGrid(cboSearch.View, form);
            grv.Columns[cot].ColumnEdit = cboSearch;
        }
        public void LocationSizeForm(XtraUserControl frmMain, XtraForm frm)
        {

            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Size = new Size((frmMain.Width / 2) + (frm.Width / 2), (frmMain.Height / 2) + (frm.Height / 2));

        }
        #endregion

        #region Load xtragrid

        public bool MLoadXtraGridIP(DevExpress.XtraGrid.GridControl grd, DevExpress.XtraGrid.Views.Grid.GridView grv, DataTable dtTmp, bool MEditable, bool MPopulateColumns, bool MColumnAutoWidth, bool MBestFitColumns)
        {
            try
            {
                grd.DataSource = dtTmp;
                grv.OptionsBehavior.Editable = MEditable;
                grv.OptionsView.RowAutoHeight = true;

                if (MPopulateColumns == true)
                    grv.PopulateColumns();
                grv.OptionsView.ColumnAutoWidth = MColumnAutoWidth;
                grv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                grv.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                grv.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                //grv.OptionsView.AllowHtmlDrawHeaders = true;
                //grv.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                grv.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;

                if (MBestFitColumns)
                    grv.BestFitColumns();

                grv.OptionsBehavior.FocusLeaveOnTab = true;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void Grv_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e, GridView grv, string fName)
        {
            if (e.MenuType != DevExpress.XtraGrid.Views.Grid.GridMenuType.Column)
                return;
            try
            {
                DevExpress.XtraGrid.Menu.GridViewMenu headerMenu = (DevExpress.XtraGrid.Menu.GridViewMenu)e.Menu;

                if (headerMenu.Items.Count(x => x.Caption.Equals("Reset Grid")) > 0)
                {
                    return;
                }
                // menu resetgrid
                DevExpress.Utils.Menu.DXMenuItem menuItem = new DevExpress.Utils.Menu.DXMenuItem("Reset Grid");
                menuItem.BeginGroup = true;
                menuItem.Tag = e.Menu;
                menuItem.Click += delegate (object a, EventArgs b) { MenuItemReset(null, null, grv, fName); };
                headerMenu.Items.Add(menuItem);
                // menu resetgrid
                DevExpress.Utils.Menu.DXMenuItem menuSave = new DevExpress.Utils.Menu.DXMenuItem("Save Grid");
                menuSave.BeginGroup = true;
                menuSave.Tag = e.Menu;
                menuSave.Click += delegate (object a, EventArgs b) { MyMenuItemSave(null, null, grv, fName); };
                headerMenu.Items.Add(menuSave);

                // menu Delete
                DevExpress.Utils.Menu.DXMenuItem menuDelete = new DevExpress.Utils.Menu.DXMenuItem("Delete Grid");
                menuDelete.BeginGroup = true;
                menuDelete.Tag = e.Menu;
                menuDelete.Click += delegate (object a, EventArgs b) { MyMenuItemDelete(null, null, grv, fName); };
                headerMenu.Items.Add(menuDelete);

            }
            catch
            {
            }
        }

        private void Grv_DM_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e, GridView grv)
        {
            if (e.MenuType != DevExpress.XtraGrid.Views.Grid.GridMenuType.Column)
                return;
            try
            {
                DevExpress.XtraGrid.Menu.GridViewMenu headerMenu = (DevExpress.XtraGrid.Menu.GridViewMenu)e.Menu;

                if (headerMenu.Items.Count(x => x.Caption.Equals("Reset Grid")) > 0)
                {
                    return;
                }
                // menu resetgrid
                DevExpress.Utils.Menu.DXMenuItem menuItem = new DevExpress.Utils.Menu.DXMenuItem("Reset Grid");
                menuItem.BeginGroup = true;
                menuItem.Tag = e.Menu;
                menuItem.Click += delegate (object a, EventArgs b) { MenuItemReset(null, null, grv, Commons.Modules.sPS.Replace("spGetList", "frm")); };
                headerMenu.Items.Add(menuItem);
                // menu resetgrid
                DevExpress.Utils.Menu.DXMenuItem menuSave = new DevExpress.Utils.Menu.DXMenuItem("Save Grid");
                menuSave.BeginGroup = true;
                menuSave.Tag = e.Menu;
                menuSave.Click += delegate (object a, EventArgs b) { MyMenuItemSave(null, null, grv, Commons.Modules.sPS.Replace("spGetList", "frm")); };
                headerMenu.Items.Add(menuSave);

                // menu Delete
                DevExpress.Utils.Menu.DXMenuItem menuDelete = new DevExpress.Utils.Menu.DXMenuItem("Delete Grid");
                menuDelete.BeginGroup = true;
                menuDelete.Tag = e.Menu;
                menuDelete.Click += delegate (object a, EventArgs b) { MyMenuItemDelete(null, null, grv, Commons.Modules.sPS.Replace("spGetList", "frm")); };
                headerMenu.Items.Add(menuDelete);
            }
            catch
            {
            }
        }

        public void MenuItemReset(System.Object sender, System.EventArgs e, GridView grv, string fName)
        {
            if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.DINH_DANG_LUOI WHERE TEN_GRID ='" + grv.Name + "' AND TEN_FORM = '" + fName + "' ")) == 1)
            {
                //Co roi thi lay dinh dang dem vao
                string text = (Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT MAC_DINH FROM dbo.DINH_DANG_LUOI WHERE TEN_GRID ='" + grv.Name + "' AND TEN_FORM = '" + fName + "'")));
                byte[] byteArray = Encoding.ASCII.GetBytes(text);
                MemoryStream stream = new MemoryStream(byteArray);
                grv.RestoreLayoutFromStream(stream);
            }

        }


        public void MyMenuItemDelete(System.Object sender, System.EventArgs e, GridView grv, string fName)
        {
            SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.DINH_DANG_LUOI WHERE TEN_GRID = '" + grv.Name + "' AND TEN_FORM ='" + fName + "'");
        }
        public void MyMenuItemSave(System.Object sender, System.EventArgs e, GridView grv, string fName)
        {
            // SAVE  
            Stream str = new System.IO.MemoryStream();
            grv.SaveLayoutToStream(str);
            str.Seek(0, System.IO.SeekOrigin.Begin);
            StreamReader reader = new StreamReader(str);
            string text = reader.ReadToEnd();
            //kiểm tra xem tồn tại chưa có thì update chưa có thì inser
            if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.DINH_DANG_LUOI WHERE TEN_GRID ='" + grv.Name + "' AND TEN_FORM = '" + fName + "' ")) == 0)
            {
                //insert
                SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "INSERT INTO dbo.DINH_DANG_LUOI(TEN_FORM,TEN_GRID,DINH_DANG,MAC_DINH)VALUES(N'" + fName + "',N'" + grv.Name + "',N'" + text + "',N'" + text + "')");
            }
            else
            {
                //update
                SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "UPDATE dbo.DINH_DANG_LUOI SET DINH_DANG = '" + text + "' WHERE TEN_GRID = '" + grv.Name + "' AND TEN_FORM ='" + fName + "'");
                //UPDATE dbo.DINH_DANG_LUOI SET DINH_DANG = '" + text + "' WHERE TEN_GRID = '" + grv.Name + "' AND TEN_FORM = '" + fName + "'
            }
        }
        //public bool MLoadXtraGridDM(DevExpress.XtraGrid.GridControl grd, DevExpress.XtraGrid.Views.Grid.GridView grv, DataTable dtTmp, bool MEditable, bool MPopulateColumns, bool MColumnAutoWidth, bool MBestFitColumns, bool MloadNNgu, string fName)
        //{
        //    try
        //    {
        //        grd.DataSource = dtTmp;
        //        grv.OptionsBehavior.Editable = MEditable;
        //        grv.OptionsView.RowAutoHeight = true;

        //        if (MPopulateColumns == true)
        //            grv.PopulateColumns();
        //        grv.OptionsView.ColumnAutoWidth = MColumnAutoWidth;
        //        grv.OptionsView.AllowHtmlDrawHeaders = true;
        //        grv.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
        //        grv.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
        //        if (Commons.Modules.bSetUp == true)
        //        {
        //            grv.DoubleClick += delegate (object a, EventArgs b)
        //            {
        //                Grv_DoubleClickDM(a, b, fName);
        //            };
        //        }
        //        if (MBestFitColumns)
        //            grv.BestFitColumns();

        //        if (MloadNNgu)
        //            MLoadNNXtraGrid(grv, fName);

        //        grv.OptionsBehavior.FocusLeaveOnTab = true;
        //        //Commons.Modules.OXtraGrid.loadXmlgrd(grd);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        public bool MLoadXtraGrid(DevExpress.XtraGrid.GridControl grd, DevExpress.XtraGrid.Views.Grid.GridView grv, DataTable dtTmp, bool MEditable, bool MPopulateColumns, bool MColumnAutoWidth, bool MBestFitColumns, bool MloadNNgu, string fName)
        {
            try
            {
                grd.BindingContext = new BindingContext();
                grd.DataSource = dtTmp;
                grv.OptionsBehavior.Editable = MEditable;
                grv.OptionsView.RowAutoHeight = true;

                if (MPopulateColumns == true)
                    grv.PopulateColumns();
                grv.OptionsView.ColumnAutoWidth = MColumnAutoWidth;
                grv.OptionsView.AllowHtmlDrawHeaders = true;
                //grv.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                grv.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
                if (Commons.Modules.bSetUp == true)
                {
                    grv.DoubleClick += delegate (object a, EventArgs b) { Grv_DoubleClick(a, b, fName); };
                }
                if (MBestFitColumns)
                    grv.BestFitColumns();

                //kiểm tra có trong table định dạng lưới chưa có thì load
                if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.DINH_DANG_LUOI WHERE TEN_GRID ='" + grv.Name + "' AND TEN_FORM = '" + fName + "' ")) == 1)
                {
                    //Co roi thi lay dinh dang dem vao
                    string text = (Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT DINH_DANG FROM dbo.DINH_DANG_LUOI WHERE TEN_GRID ='" + grv.Name + "' AND TEN_FORM = '" + fName + "'")));
                    byte[] byteArray = Encoding.ASCII.GetBytes(text);
                    MemoryStream stream = new MemoryStream(byteArray);
                    grv.RestoreLayoutFromStream(stream);
                }
                else
                {
                    //chua co thi luu vao dinh dang voi mac dinh
                    Stream str = new System.IO.MemoryStream();
                    grv.SaveLayoutToStream(str);
                    str.Seek(0, System.IO.SeekOrigin.Begin);
                    StreamReader reader = new StreamReader(str);
                    string text = reader.ReadToEnd();
                    SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "INSERT INTO dbo.DINH_DANG_LUOI(TEN_FORM,TEN_GRID,DINH_DANG,MAC_DINH)VALUES(N'" + fName + "',N'" + grv.Name + "',N'" + text + "',N'" + text + "')");
                }
                if (Commons.Modules.bSetUp == true)
                {
                    grv.PopupMenuShowing += delegate (object a, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs b) { Grv_PopupMenuShowing(grv, b, grv, fName); };
                }
                grv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                grv.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                if (MloadNNgu)
                {
                    //Thread Thread3 = new Thread(delegate ()
                    //{
                    //    if (grd.InvokeRequired)
                    //    {
                    //        grd.Invoke(new MethodInvoker(delegate
                    //        {
                    //            MLoadNNXtraGrid(grv, fName);

                    //        }));
                    //    }
                    //}, 100); Thread3.Start();
                    MLoadNNXtraGrid(grv, fName);
                }
                grv.OptionsBehavior.FocusLeaveOnTab = true;
                //Commons.Modules.OXtraGrid.loadXmlgrd(grd);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool MLoadXtraGrid(DevExpress.XtraGrid.GridControl grd, DevExpress.XtraGrid.Views.BandedGrid.BandedGridView grv, DataTable dtTmp, bool MEditable, bool MPopulateColumns, bool MColumnAutoWidth, bool MBestFitColumns, bool MloadNNgu, string fName)
        {
            try
            {
                grd.BindingContext = new BindingContext();
                grd.DataSource = dtTmp;
                grv.OptionsBehavior.Editable = MEditable;
                grv.OptionsView.RowAutoHeight = true;

                if (MPopulateColumns == true)
                    grv.PopulateColumns();
                grv.OptionsView.ColumnAutoWidth = MColumnAutoWidth;
                grv.OptionsView.AllowHtmlDrawHeaders = true;
                //grv.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                grv.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
                if (Commons.Modules.bSetUp == true)
                {
                    grv.DoubleClick += delegate (object a, EventArgs b) { Grv_DoubleClick(a, b, fName); };
                }
                if (MBestFitColumns)
                    grv.BestFitColumns();

                //kiểm tra có trong table định dạng lưới chưa có thì load
                if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.DINH_DANG_LUOI WHERE TEN_GRID ='" + grv.Name + "' AND TEN_FORM = '" + fName + "' ")) == 1)
                {
                    //Co roi thi lay dinh dang dem vao
                    string text = (Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT DINH_DANG FROM dbo.DINH_DANG_LUOI WHERE TEN_GRID ='" + grv.Name + "' AND TEN_FORM = '" + fName + "'")));
                    byte[] byteArray = Encoding.ASCII.GetBytes(text);
                    MemoryStream stream = new MemoryStream(byteArray);
                    grv.RestoreLayoutFromStream(stream);
                }
                else
                {
                    //chua co thi luu vao dinh dang voi mac dinh
                    Stream str = new System.IO.MemoryStream();
                    grv.SaveLayoutToStream(str);
                    str.Seek(0, System.IO.SeekOrigin.Begin);
                    StreamReader reader = new StreamReader(str);
                    string text = reader.ReadToEnd();
                    SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "INSERT INTO dbo.DINH_DANG_LUOI(TEN_FORM,TEN_GRID,DINH_DANG,MAC_DINH)VALUES(N'" + fName + "',N'" + grv.Name + "',N'" + text + "',N'" + text + "')");
                }
                if (Commons.Modules.bSetUp == true)
                {
                    grv.PopupMenuShowing += delegate (object a, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs b) { Grv_PopupMenuShowing(grv, b, grv, fName); };
                }
                grv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                grv.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                if (MloadNNgu)
                {
                    //Thread Thread3 = new Thread(delegate ()
                    //{
                    //    if (grd.InvokeRequired)
                    //    {
                    //        grd.Invoke(new MethodInvoker(delegate
                    //        {
                    //            MLoadNNXtraGrid(grv, fName);

                    //        }));
                    //    }
                    //}, 100); Thread3.Start();
                    MLoadNNXtraGrid(grv, fName);
                }
                grv.OptionsBehavior.FocusLeaveOnTab = true;
                //Commons.Modules.OXtraGrid.loadXmlgrd(grd);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool MLoadXtraGridDM(DevExpress.XtraGrid.GridControl grd, DevExpress.XtraGrid.Views.Grid.GridView grv, DataTable dtTmp, bool MEditable, bool MPopulateColumns, bool MColumnAutoWidth, bool MBestFitColumns, bool MloadNNgu)
        {
            try
            {
                grd.DataSource = dtTmp;
                grv.OptionsBehavior.Editable = MEditable;
                grv.OptionsView.RowAutoHeight = true;

                if (MPopulateColumns == true)
                    grv.PopulateColumns();
                grv.OptionsView.ColumnAutoWidth = MColumnAutoWidth;
                grv.OptionsView.AllowHtmlDrawHeaders = true;
                grv.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                grv.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
                if (Commons.Modules.bSetUp == true)
                {
                    grv.DoubleClick += delegate (object a, EventArgs b) { Grv_DoubleClick(a, b, Commons.Modules.sPS.Replace("spGetList", "frm")); };
                }
                if (MBestFitColumns)
                    grv.BestFitColumns();

                //kiểm tra có trong table định dạng lưới chưa có thì load
                if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.DINH_DANG_LUOI WHERE TEN_GRID ='" + grv.Name + "' AND TEN_FORM = '" + Commons.Modules.sPS.Replace("spGetList", "frm") + "' ")) == 1)
                {
                    //Co roi thi lay dinh dang dem vao
                    string text = (Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT DINH_DANG FROM dbo.DINH_DANG_LUOI WHERE TEN_GRID ='" + grv.Name + "' AND TEN_FORM = '" + Commons.Modules.sPS.Replace("spGetList", "frm") + "'")));
                    byte[] byteArray = Encoding.ASCII.GetBytes(text);
                    MemoryStream stream = new MemoryStream(byteArray);
                    grv.RestoreLayoutFromStream(stream);
                }
                else
                {
                    //chua co thi luu vao dinh dang voi mac dinh
                    Stream str = new System.IO.MemoryStream();
                    grv.SaveLayoutToStream(str);
                    str.Seek(0, System.IO.SeekOrigin.Begin);
                    StreamReader reader = new StreamReader(str);
                    string text = reader.ReadToEnd();
                    SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "INSERT INTO dbo.DINH_DANG_LUOI(TEN_FORM,TEN_GRID,DINH_DANG,MAC_DINH)VALUES(N'" + Commons.Modules.sPS.Replace("spGetList", "frm") + "',N'" + grv.Name + "',N'" + text + "',N'" + text + "')");
                }

                if (Commons.Modules.bSetUp == true)
                {
                    grv.PopupMenuShowing += delegate (object a, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs b) { Grv_DM_PopupMenuShowing(grv, b, grv); };
                }


                grv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                grv.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                grv.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

                if (MloadNNgu)
                    MLoadNNXtraGrid(grv, Commons.Modules.sPS.Replace("spGetList", "frm"));

                grv.OptionsBehavior.FocusLeaveOnTab = true;
                //Commons.Modules.OXtraGrid.loadXmlgrd(grd);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public DataTable GetDataTableMultiSelect(DevExpress.XtraGrid.GridControl grd, DevExpress.XtraGrid.Views.Grid.GridView grv)
        {
            try
            {
                DataRow dr;
                DataRow row;
                DataTable dt = new DataTable();
                dt = ((DataTable)grd.DataSource).Clone();
                Int32[] selectedRowHandles = grv.GetSelectedRows();
                for (int i = 0; i < selectedRowHandles.Length; i++)
                {
                    int selectedRowHandle = selectedRowHandles[i];
                    if (selectedRowHandle >= 0)
                    {
                        dr = grv.GetDataRow(selectedRowHandle);
                        row = dt.NewRow();
                        for (int j = 0; j < grv.Columns.Count; j++)
                        {
                            row[j] = dr[j];
                        }
                        dt.Rows.Add(row);
                    }
                }
                return dt;
            }
            catch (Exception ex) { return null; }
        }

        private void Grv_DoubleClickDM(object sender, EventArgs e, string sName)
        {
            if (Form.ModifierKeys == Keys.Control)
            {
                try
                {
                    DevExpress.XtraGrid.Views.Grid.GridView View;
                    string sText = "";
                    View = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                    DevExpress.Utils.DXMouseEventArgs dxMouseEventArgs = e as DevExpress.Utils.DXMouseEventArgs;
                    DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hitInfo = View.CalcHitInfo(dxMouseEventArgs.Location);
                    if (hitInfo.InColumn)
                    {
                        try
                        {
                            sText = XtraInputBox.Show(hitInfo.Column.GetTextCaption(), "Sửa ngôn ngữ", "");
                            if (sText == "" || sText == null)
                                return;
                            else if (sText == "Windows.Forms.DialogResult.Retry")
                            {
                                sText = "";
                                CapNhapNN(sName, hitInfo.Column.FieldName, sText, true);
                            }
                            else
                                CapNhapNN(sName, hitInfo.Column.FieldName, sText, false);
                            sText = " SELECT TOP 1 " + (Commons.Modules.TypeLanguage == 0 ? "VIETNAM" : "ENGLISH") + " FROM LANGUAGES WHERE FORM = '" + sName + "' AND KEYWORD = '" + hitInfo.Column.FieldName + "' AND MS_MODULE = 'VS_HRM' ";
                            sText = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sText));
                            hitInfo.Column.Caption = sText;
                        }
                        catch
                        {
                        }
                    }
                    Commons.Modules.OXtraGrid.SaveXmlGrid(View.GridControl);
                }
                catch
                {
                }
            }
        }

        private void Grv_DoubleClick(object sender, EventArgs e, string sName)
        {
            if (Form.ModifierKeys == Keys.Control)
            {
                try
                {
                    DevExpress.XtraGrid.Views.Grid.GridView View;
                    string sText = "";
                    View = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                    DevExpress.Utils.DXMouseEventArgs dxMouseEventArgs = e as DevExpress.Utils.DXMouseEventArgs;
                    DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hitInfo = View.CalcHitInfo(dxMouseEventArgs.Location);
                    if (hitInfo.InColumn)
                    {
                        try
                        {
                            sText = XtraInputBox.Show(hitInfo.Column.GetTextCaption(), "Sửa ngôn ngữ", "");
                            if (sText == "" || sText == null)
                                return;
                            else if (sText == "Windows.Forms.DialogResult.Retry")
                            {
                                sText = "";
                                CapNhapNN(sName, hitInfo.Column.FieldName, sText, true);
                            }
                            else
                                CapNhapNN(sName, hitInfo.Column.FieldName, sText, false);
                            sText = " SELECT TOP 1 " + (Commons.Modules.TypeLanguage == 0 ? "VIETNAM" : "ENGLISH") + " FROM LANGUAGES WHERE FORM = '" + sName + "' AND KEYWORD = '" + hitInfo.Column.FieldName + "' AND MS_MODULE = 'VS_HRM' ";
                            sText = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sText));
                            hitInfo.Column.Caption = sText;
                        }
                        catch
                        {
                        }
                    }
                }
                catch
                {
                }
            }
        }

        private void CapNhapNN(string sForm, string sKeyWord, string sChuoi, bool bReset)
        {
            string sSql;
            if (bReset)
                sSql = "UPDATE LANGUAGES SET " + (Commons.Modules.TypeLanguage == 0 ? "VIETNAM" : "ENGLISH") + " = " + (Commons.Modules.TypeLanguage == 0 ? "VIETNAM_OR" : "ENGLISH_OR") + " WHERE FORM = '" + sForm + "' AND KEYWORD = '" + sKeyWord + "' AND MS_MODULE = 'VS_HRM'";
            else
                sSql = "UPDATE LANGUAGES SET " + (Commons.Modules.TypeLanguage == 0 ? "VIETNAM" : "ENGLISH") + " = N'" + sChuoi + "' WHERE FORM = '" + sForm + "' AND KEYWORD = '" + sKeyWord + "' AND MS_MODULE = 'VS_HRM'";
            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
        }

        public void MLoadNNXtraGrid(DevExpress.XtraGrid.Views.Grid.GridView grv, string fName)
        {

            grv.OptionsView.RowAutoHeight = true;
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT DISTINCT KEYWORD , CASE " + Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'" + fName + "' "));
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in grv.Columns)
            {
                if (col.Visible)
                {
                    col.Caption = GetNN(dtTmp, col.FieldName, fName);
                }
            }

        }

        public void MLoadNNXtraGrid(DevExpress.XtraGrid.Views.Grid.GridView grv, string fName, int NN)
        {
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT DISTINCT KEYWORD , CASE " + Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'" + fName + "' "));

            foreach (DevExpress.XtraGrid.Columns.GridColumn col in grv.Columns)
            {
                if (col.Visible)
                {
                    col.AppearanceHeader.Options.UseTextOptions = true;
                    col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    col.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
                    col.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    col.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

                    //col.Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, fName, col.FieldName, NN);
                    col.Caption = GetNN(dtTmp, col.FieldName, fName);
                }
            }
        }

        public void MFormatCol(GridView grv, string sColFormat, int iFormatString)
        {
            try
            {
                string sFormatString = "n" + iFormatString.ToString();
                RepositoryItemTextEdit txtEdit = new RepositoryItemTextEdit();
                txtEdit.Properties.DisplayFormat.FormatString = sFormatString;
                txtEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                txtEdit.Properties.EditFormat.FormatString = sFormatString;
                txtEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                txtEdit.Properties.Mask.EditMask = sFormatString;
                txtEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
                grv.Columns[sColFormat].ColumnEdit = txtEdit;
                //grv.Columns[sColFormat].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //grv.Columns[sColFormat].DisplayFormat.FormatString = sFormatString;
            }
            catch
            {
                grv.Columns[sColFormat].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                grv.Columns[sColFormat].DisplayFormat.FormatString = "N2";
            }
        }

        #endregion

        #region thay doi nn
        public void ThayDoiNN(Form frm)
        {
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD , CASE " + Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'" + frm.Name + "' "));
            frm.Text = GetNN(dtTmp, frm.Name, frm.Name);
            List<Control> resultControlList = new List<Control>();
            GetControlsCollection(frm, ref resultControlList, null);

            foreach (Control control1 in resultControlList)
            {
                try
                {
                    DoiNN(control1, frm, dtTmp);
                }
                catch
                { }
            }
        }

        public void ThayDoiNN(XtraForm frm)
        {
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD , CASE " + Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'" + frm.Name + "' "));
            frm.Text = GetNN(dtTmp, frm.Name, frm.Name);
            List<Control> resultControlList = new List<Control>();
            GetControlsCollection(frm, ref resultControlList, null);

            foreach (Control control1 in resultControlList)
            {
                try
                {
                    DoiNN(control1, frm, dtTmp);
                }
                catch
                { }
            }
        }

        public void ThayDoiNN(Form frm, WindowsUIButtonPanel btnWinUIB)
        {
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD , CASE " + Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'" + frm.Name + "' "));
            frm.Text = GetNN(dtTmp, frm.Name, frm.Name);
            List<Control> resultControlList = new List<Control>();
            GetControlsCollection(frm, ref resultControlList, null);

            foreach (Control control1 in resultControlList)
            {
                try
                {
                    DoiNN(control1, frm, dtTmp);
                }
                catch
                { }
            }

            for (int i = 0; i < btnWinUIB.Buttons.Count; i++)
            {
                try
                {
                    if (btnWinUIB.Buttons[i].Properties.Tag.ToString() != null)
                    {
                        btnWinUIB.Size = new Size(btnWinUIB.Size.Width, 50);
                        btnWinUIB.AllowGlyphSkinning = false;
                        btnWinUIB.Buttons[i].Properties.Caption = GetNN(dtTmp, btnWinUIB.Buttons[i].Properties.Tag.ToString(), frm.Name);
                        btnWinUIB.Buttons[i].Properties.ToolTip = GetNN(dtTmp, btnWinUIB.Buttons[i].Properties.Tag.ToString(), frm.Name);
                    }
                }
                catch
                {
                }
            }

        }

        public void ThayDoiNN(XtraReport report)
        {
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD , CASE " + Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'" + report.Tag.ToString() + "' "));

            foreach (DevExpress.XtraReports.UI.Band band in report.Bands)
            {
                foreach (DevExpress.XtraReports.UI.SubBand subband in band.SubBands)
                {
                    foreach (DevExpress.XtraReports.UI.XRControl control in subband)
                    {
                        if (control.GetType() == typeof(DevExpress.XtraReports.UI.XRTable))
                        {
                            DevExpress.XtraReports.UI.XRTable table = (DevExpress.XtraReports.UI.XRTable)control;
                            foreach (DevExpress.XtraReports.UI.XRTableRow row in table)
                            {
                                foreach (DevExpress.XtraReports.UI.XRTableCell cell in row)
                                {
                                    try
                                    {
                                        if (cell.Name.Substring(0, 3).ToString() == "xrT") break;
                                        cell.Text = GetNN(dtTmp, cell.Name, report.Tag.ToString());// translation processing here

                                    }
                                    catch
                                    {
                                        MessageBox.Show("err language substring");
                                    }


                                }
                            }
                        }
                        else
                        {
                            control.Text = GetNN(dtTmp, control.Name, report.Tag.ToString());
                        }
                    }
                }
                foreach (DevExpress.XtraReports.UI.XRControl control in band)
                {
                    if (control.GetType() == typeof(DevExpress.XtraReports.UI.XRTable))
                    {
                        DevExpress.XtraReports.UI.XRTable table = (DevExpress.XtraReports.UI.XRTable)control;
                        foreach (DevExpress.XtraReports.UI.XRTableRow row in table)
                        {
                            foreach (DevExpress.XtraReports.UI.XRTableCell cell in row)
                            {
                                try
                                {

                                    if (cell.Name.Substring(0, 3).ToString() == "xrT") break;
                                    cell.Text = GetNN(dtTmp, cell.Name, report.Tag.ToString());// translation processing here

                                }
                                catch
                                {
                                    MessageBox.Show("err language substring");
                                }

                            }
                        }
                    }
                    else
                    {
                        control.Text = GetNN(dtTmp, control.Name, report.Tag.ToString());
                    }

                }

            }
        }
        public void GetPhanQuyen(AccordionControlElement button)
        {
            if (button != null && button.Name != null)
                GetPhanQuyen(button.Name.ToString());
        }
        public void GetPhanQuyen(string button)
        {
            string sSql = " SELECT T1.ID_PERMISION FROM dbo.NHOM_MENU T1 INNER JOIN dbo.MENU T2 ON T2.ID_MENU = T1.ID_MENU INNER JOIN dbo.USERS T3 ON T3.ID_NHOM = T1.ID_NHOM WHERE	T2.KEY_MENU = N'" + button.ToString() + "' AND T3.USER_NAME = N'" + Commons.Modules.UserName + "' ";
            Commons.Modules.iPermission = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql).ToString());
        }
        public void SetPhanQuyen(DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel windowsUIButton)
        {
            int is_line = 1;
            for (int i = 0; i < windowsUIButton.Buttons.Count; i++)
            {
                WindowsUIButton btn = windowsUIButton.Buttons[i] as WindowsUIButton;
                try
                {
                    if (btn.Tag != null)
                    {
                        is_line = 1;
                        if (Commons.Modules.iPermission == 1)
                        {

                            windowsUIButton.Buttons[i].Properties.Enabled = true;
                        }
                        else if (Commons.Modules.iPermission == 2)
                        {

                            switch (btn.Tag)
                            {
                                // edit
                                case "them":
                                case "themsua":
                                case "capnhatphep":
                                case "xoa":
                                case "delete":
                                case "sua":
                                case "luu":
                                case "capnhat":
                                case "update":
                                case "resetpass":
                                case "CapNhap":
                                case "capnhatdieuchinh":
                                case "xoangay":
                                case "TongHopThongTin":
                                case "LinkTay":
                                case "LinkDuLieu":
                                case "linkExcel":
                                case "capnhatgio":
                                case "thuchien":
                                case "copycongdoan":
                                case "chamtudong":
                                case "laycong":
                                case "tinhdiemthang":
                                case "tinhluong":
                                case "ghi":
                                case "import":
                                case "export":
                                    //    windowsUIButton.Buttons[i].Properties.Visible = false;
                                    windowsUIButton.Buttons[i].Properties.Enabled = false;
                                    windowsUIButton.Buttons[i].Properties.ToolTip = "Chức năng chưa được phân quyền";
                                    break;
                                // viiew
                                case "in":
                                case "In":
                                case "intongquat":
                                case "print":
                                case "Print":
                                case "khongluu":
                                case "thoat":
                                case "trove":
                                    //  windowsUIButton.Buttons[i].Properties.Visible = true;
                                    windowsUIButton.Buttons[i].Properties.Enabled = true;
                                    break;
                                default:
                                    windowsUIButton.Buttons[i].Properties.Enabled = true;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (is_line == 1)
                            windowsUIButton.Buttons[i].Properties.Visible = true;
                        else
                        {
                            windowsUIButton.Buttons[i].Properties.Visible = false;
                            is_line++;
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        public static void DinhDangNgayThang(GridColumn gridcol)
        {
            switch (gridcol.FieldName)
            {
                case "CAP_NGAY":
                case "DEN_NGAY":
                case "DEN_THANG":
                case "NGAY_BAT_DAU_HD":
                case "NGAY_BD":
                case "NGAY_BD_THU_VIEC":
                case "NGAY_BI_TAI_NAN":
                case "NGAY_CAP":
                case "NGAY_CAP_CUU_TAI_CHO":
                case "NGAY_CAP_GP":
                case "NGAY_CHAM_DUT_NOP_BHXH":
                case "NGAY_DANH_GIA":
                case "NGAY_DBHXH":
                case "NGAY_DBHXH_DT":
                case "NGAY_HET_HAN":
                case "NGAY_HET_HD":
                case "NGAY_HH_GP":
                case "NGAY_HIEU_LUC":
                case "NGAY_HOC_VIEC":
                case "NGAY_KN_DANG":
                case "NGAY_KT":
                case "NGAY_KT_THU_VIEC":
                case "NGAY_KY":
                case "NGAY_NGHI_VIEC":
                case "NGAY_NGUNG_BHXH":
                case "NGAY_NHAN_DON":
                case "NGAY_NHAP_NGU":
                case "NGAY_QD":
                case "NGAY_RA_KHOI_DANG":
                case "NGAY_RA_KHOI_DOAN":
                case "NGAY_RA_VIEN":
                case "NGAY_SINH":
                case "NGAY_THAM_GIA_BHXH":
                case "NGAY_THOI_VIEC":
                case "NGAY_THU_HOI_BHYT":
                case "NGAY_THU_VIEC":
                case "NGAY_TKL":
                case "NGAY_TTXL":
                case "NGAY_VAO_CONG_DOAN":
                case "NGAY_VAO_CTY":
                case "NGAY_VAO_DANG":
                case "NGAY_VAO_DOAN":
                case "NGAY_VAO_LAM":
                case "NGAY_VAO_LAM_LAI":
                case "NGAY_VAO_VIEN":
                case "NGAY_XUAT_NGU":
                case "NgayBHXH":
                case "NGHI_DEN_NGAY":
                case "NGHI_TU_NGAY":
                case "THANG":
                case "THANG_KTT":
                case "THANG_LXL":
                case "THANG_TINH_LUONG_TC":
                case "TIME_LOGIN":
                case "TU_NGAY":
                case "TU_THANG":

                    gridcol.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    gridcol.DisplayFormat.FormatType = FormatType.DateTime;
                    gridcol.DisplayFormat.FormatString = "d";
                    break;
                default: break;
            }
        }
        public static void DinhDangNgayThang(TileView grvMain)
        {
            foreach (GridColumn gridcol in grvMain.Columns)
            {
                DinhDangNgayThang(gridcol);
            }
        }
        public static void DinhDangNgayThang(GridView grvMain)
        {
            foreach (GridColumn gridcol in grvMain.Columns)
            {
                DinhDangNgayThang(gridcol);
            }
        }
        public void ThayDoiNN(XtraUserControl frm)
        {
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD , CASE " + Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'" + frm.Name + "' "));
            frm.Text = GetNN(dtTmp, frm.Name, frm.Name);
            List<Control> resultControlList = new List<Control>();
            GetControlsCollection(frm, ref resultControlList, null);
            foreach (Control control1 in resultControlList)
            {
                try
                {
                    DoiNN(control1, frm, dtTmp);
                }
                catch
                { }
            }
            try
            {
                //MTabOrder MTab = new MTabOrder(frm);
                //MTab.MSetTabOrder(MTabOrder.TabScheme.AcrossFirst);
            }
            catch
            {
            }
        }

        public void ThayDoiNN(XtraUserControl frm, WindowsUIButtonPanel btnWinUIB)
        {
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD , CASE " + Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'" + frm.Name + "' "));
            frm.Text = GetNN(dtTmp, frm.Name, frm.Name);
            List<Control> resultControlList = new List<Control>();
            GetControlsCollection(frm, ref resultControlList, null);
            foreach (Control control in resultControlList)
            {
                try
                {
                    DoiNN(control, frm, dtTmp);
                }
                catch
                { }
            }
            try
            {
                //foreach (WindowsUIButton btn in btnWinUIB.Buttons.but)
                //{
                //    btn.Caption = GetNN(dtTmp, btn.Tag.ToString(), frm.Name);
                //}
                for (int i = 0; i < btnWinUIB.Buttons.Count; i++)
                {
                    try
                    {
                        if (btnWinUIB.Buttons[i].Properties.Tag.ToString() != null)
                        {
                            btnWinUIB.Size = new Size(btnWinUIB.Size.Width, 50);
                            btnWinUIB.AllowGlyphSkinning = false;
                            btnWinUIB.Buttons[i].Properties.Caption = GetNN(dtTmp, btnWinUIB.Buttons[i].Properties.Tag.ToString(), frm.Name);
                            btnWinUIB.Buttons[i].Properties.ToolTip = GetNN(dtTmp, btnWinUIB.Buttons[i].Properties.Tag.ToString(), frm.Name);
                        }
                    }
                    catch
                    {
                    }
                }
            }
            catch
            { }
        }
        public void ThayDoiNN(XtraForm frm, LayoutControlGroup group, WindowsUIButtonPanel btnWinUIB)
        {
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD , CASE " + Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'" + frm.Name + "' "));
            frm.Text = GetNN(dtTmp, frm.Name, frm.Name);
            //load nn control bên trong
            LoadNNGroupControl(frm, group, dtTmp);
            //load nn windowbutton
            try
            {
                //foreach (WindowsUIButton btn in btnWinUIB.Buttons.but)
                //{
                //    btn.Caption = GetNN(dtTmp, btn.Tag.ToString(), frm.Name);
                //}
                for (int i = 0; i < btnWinUIB.Buttons.Count; i++)
                {
                    try
                    {
                        if (btnWinUIB.Buttons[i].Properties.Tag.ToString() != null)
                        {
                            btnWinUIB.Size = new Size(btnWinUIB.Size.Width, 50);
                            btnWinUIB.AllowGlyphSkinning = false;
                            btnWinUIB.Buttons[i].Properties.Caption = GetNN(dtTmp, btnWinUIB.Buttons[i].Properties.Tag.ToString(), frm.Name);
                            btnWinUIB.Buttons[i].Properties.ToolTip = GetNN(dtTmp, btnWinUIB.Buttons[i].Properties.Tag.ToString(), frm.Name);
                        }
                    }
                    catch
                    {
                    }
                }
            }
            catch
            { }
        }
        public void MLoadCheckedComboBoxEdit(DevExpress.XtraEditors.CheckedComboBoxEdit cbo, DataTable dtTmp, string Ma, string Ten, string form, bool isNgonNgu)

        {
            try
            {
                cbo.Properties.DataSource = null;
                cbo.Properties.DisplayMember = "";
                cbo.Properties.ValueMember = "";

                cbo.Properties.DataSource = dtTmp;
                cbo.Properties.DisplayMember = Ten;
                cbo.Properties.ValueMember = Ma;
                //cbo.Properties.PopulateViewColumns();
                //cbo.Properties.View.Columns[0].Visible = false;
                cbo.Properties.AppearanceDropDown.Font = (Font)cbo.Font.Clone();
                cbo.Properties.DropDownRows = dtTmp.Rows.Count + 2;
                cbo.Refresh();

            }
            catch { }
        }
        private void LoadNNGroupControl(XtraForm frm, LayoutControlGroup group, DataTable dtTmp)
        {
            foreach (var gr in group.Items)
            {
                if (gr.GetType().Name == "LayoutControlGroup")
                {
                    LayoutControlGroup gro = (LayoutControlGroup)gr;
                    gro.Text = GetNN(dtTmp, gro.Name, frm.Name);
                    gro.DoubleClick += delegate (object a, EventArgs b) { ControlGroup_DoubleClick(gro, b, frm.Name); };
                    LoadNNGroupControl(frm, (LayoutControlGroup)gr, dtTmp);
                }
                else
                {
                    try
                    {
                        LayoutControlItem control1 = (LayoutControlItem)gr;
                        try
                        {
                            //    if (control1.Control.GetType().Name.ToLower() == "checkedit")
                            //    {
                            //        control1.Control.Text = GetNN(dtTmp, control1.Name, frm.Name);
                            //        control1.Control.DoubleClick += delegate (object a, EventArgs b) { CheckEdit_DoubleClick(control1.Control, b, frm.Name); };
                            //    }
                            //    else
                            if (control1.AppearanceItemCaption.ForeColor == Color.FromArgb(192, 0, 0))
                            {
                                control1.AppearanceItemCaption.ForeColor = Color.FromArgb(128, 0, 0);
                            }
                            if (control1.Control.GetType().Name.ToLower() == "radiogroup")
                            {
                                DoiNN(control1.Control, frm, dtTmp);
                            }
                            else
                            {
                                control1.Text = GetNN(dtTmp, control1.Name, frm.Name) + "  ";
                                control1.DoubleClick += delegate (object a, EventArgs b) { Control1_DoubleClick(control1, b, frm.Name); };

                            }
                            control1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 2, 2);
                            ((DevExpress.XtraEditors.BaseEdit)control1.Control).EnterMoveNextControl = true;


                        }
                        catch (Exception ex)
                        {
                            control1.Text = GetNN(dtTmp, control1.Name, frm.Name);

                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
        private void TabbedControlGroup_DoubleClick(object sender, EventArgs e, string sName)
        {
            if (Form.ModifierKeys == Keys.Control)
            {
                TabbedControlGroup Control;
                string sText = "";
                Control = (TabbedControlGroup)sender;
                LayoutGroup Ctl = Control.SelectedTabPage;
                try
                {
                    sText = XtraInputBox.Show(Ctl.Text, "Sửa ngôn ngữ", "");
                    if (sText == "" || sText == null)
                        return;
                    else
                        CapNhapNN(sName, Ctl.Name.ToUpper().Replace("ItemFor".ToUpper(), ""), sText, false);

                    sText = " SELECT TOP 1 " + (Commons.Modules.TypeLanguage == 0 ? "VIETNAM" : "ENGLISH") + " FROM LANGUAGES WHERE FORM = '" + sName + "' AND KEYWORD = '" + Ctl.Name.ToUpper().Replace("ItemFor".ToUpper(), "") + "' AND MS_MODULE = 'VS_HRM'";
                    sText = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sText));

                    Ctl.Text = sText;
                }
                catch
                {
                    sText = "";
                }
            }
        }

        private void ControlGroup_DoubleClick(object sender, EventArgs e, string sName)
        {
            if (Form.ModifierKeys == Keys.Control)
            {
                LayoutControlGroup Ctl;
                string sText = "";
                Ctl = (LayoutControlGroup)sender;
                try
                {
                    sText = XtraInputBox.Show(Ctl.Text, "Sửa ngôn ngữ", "");
                    if (sText == "" || sText == null)
                        return;
                    else
                        CapNhapNN(sName, Ctl.Name.ToUpper().Replace("ItemFor".ToUpper(), ""), sText, false);

                    sText = " SELECT TOP 1 " + (Commons.Modules.TypeLanguage == 0 ? "VIETNAM" : "ENGLISH") + " FROM LANGUAGES WHERE FORM = '" + sName + "' AND KEYWORD = '" + Ctl.Name.ToUpper().Replace("ItemFor".ToUpper(), "") + "' AND MS_MODULE = 'VS_HRM'";
                    sText = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sText));

                    Ctl.Text = sText;
                }
                catch
                {
                    sText = "";
                }
            }
        }
        private void CheckEdit_DoubleClick(object sender, EventArgs e, string sName)
        {
            if (Form.ModifierKeys == Keys.Control)
            {
                CheckEdit Ctl;
                string sText = "";
                Ctl = (CheckEdit)sender;
                try
                {
                    sText = XtraInputBox.Show(Ctl.Text, "Sửa ngôn ngữ", "");
                    if (sText == "" || sText == null)
                        return;
                    else
                        CapNhapNN(sName, Ctl.Name.ToUpper().Replace("ItemFor".ToUpper(), ""), sText, false);

                    sText = " SELECT TOP 1 " + (Commons.Modules.TypeLanguage == 0 ? "VIETNAM" : "ENGLISH") + " FROM LANGUAGES WHERE FORM = '" + sName + "' AND KEYWORD = '" + Ctl.Name.ToUpper().Replace("ItemFor".ToUpper(), "") + "' AND MS_MODULE = 'VS_HRM'";
                    sText = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sText));

                    Ctl.Text = sText;
                }
                catch
                {
                    sText = "";
                }
            }
        }

        private void Control1_DoubleClick(object sender, EventArgs e, string sName)
        {
            if (Form.ModifierKeys == Keys.Control)
            {
                LayoutControlItem Ctl;
                string sText = "";
                Ctl = (LayoutControlItem)sender;
                try
                {
                    sText = XtraInputBox.Show(Ctl.Text, "Sửa ngôn ngữ", "");
                    if (sText == "" || sText == null)
                        return;
                    else if (sText == "Windows.Forms.DialogResult.Retry")
                    {
                        sText = "";
                        CapNhapNN(sName, Ctl.Name.ToUpper().Replace("ItemFor".ToUpper(), ""), sText, true);
                    }
                    else
                        CapNhapNN(sName, Ctl.Name.ToUpper().Replace("ItemFor".ToUpper(), ""), sText, false);
                    sText = " SELECT TOP 1 " + (Commons.Modules.TypeLanguage == 0 ? "VIETNAM" : "ENGLISH") + " FROM LANGUAGES WHERE FORM = '" + sName + "' AND KEYWORD = '" + Ctl.Name.ToUpper().Replace("ItemFor".ToUpper(), "") + "' AND MS_MODULE = 'VS_HRM'";
                    sText = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sText));

                    Ctl.Text = sText;
                }
                catch
                {
                    sText = "";
                }
            }
        }

        public void ThayDoiNN(XtraUserControl frm, LayoutControlGroup group)
        {
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD , CASE " + Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'" + frm.Name + "' "));
            frm.Text = GetNN(dtTmp, frm.Name, frm.Name);
            //load nn control bên trong
            LoadNNGroupControl(frm, group, dtTmp);
            //load nn windowbitton
        }

        public void ThayDoiNN(XtraForm frm, LayoutControlGroup group)
        {
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD , CASE " + Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'" + frm.Name + "' "));
            frm.Text = GetNN(dtTmp, frm.Name, frm.Name);
            //load nn control bên trong
            LoadNNGroupControl(frm, group, dtTmp);
            //load nn windowbitton
        }


        private void LoadNNGroupControl(XtraUserControl frm, LayoutControlGroup group, DataTable dtTmp)
        {
            try
            {

                foreach (var gr in group.Items.Where(x => x.GetType().Name.Substring(0, 6).ToLower() == "layout"))
                {
                    if (gr.GetType().Name == "LayoutControlGroup")
                    {
                        LayoutControlGroup gro = (LayoutControlGroup)gr;
                        gro.Text = GetNN(dtTmp, gro.Name, frm.Name);
                        gro.AppearanceGroup.ForeColor = Color.FromArgb(0, 0, 192);
                        gro.DoubleClick += delegate (object a, EventArgs b) { ControlGroup_DoubleClick(gro, b, frm.Name); };
                        LoadNNGroupControl(frm, (LayoutControlGroup)gr, dtTmp);
                    }
                    else
                    {
                        try
                        {
                            LayoutControlItem control1 = (LayoutControlItem)gr;
                            try
                            {
                                if (control1.AppearanceItemCaption.ForeColor == Color.FromArgb(192, 0, 0))
                                {
                                    control1.AppearanceItemCaption.ForeColor = Color.FromArgb(128, 0, 0);
                                }
                                if (control1.Control.GetType().Name.ToLower() == "radiogroup")
                                {
                                    DoiNN(control1.Control, frm, dtTmp);
                                }
                                else
                                {
                                    control1.Text = GetNN(dtTmp, control1.Name, frm.Name);
                                    control1.DoubleClick += delegate (object a, EventArgs b) { Control1_DoubleClick(control1, b, frm.Name); };
                                }
                                //control1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 2, 2);
                                //((DevExpress.XtraEditors.BaseEdit)control1.Control).EnterMoveNextControl = true;
                            }
                            catch
                            { }
                        }
                        catch (Exception)
                        {
                        }
                    }

                }
            }
            catch
            {
            }
        }

        private void Gro_DoubleClick(object sender, EventArgs e)
        {
            //sữa ngon ngữ group
        }
        public void ThayDoiNN(XtraUserControl frm, LayoutControlGroup group, TabbedControlGroup Tab, WindowsUIButtonPanel btnWinUIB)
        {
            try
            {
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD , CASE " + Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'" + frm.Name + "' "));


                LoadNNGroupControl(frm, group, dtTmp);
                Tab.DoubleClick += delegate (object a, EventArgs b) { TabbedControlGroup_DoubleClick(Tab, b, frm.Name); };
                Tab.AppearanceTabPage.HeaderActive.ForeColor = Color.FromArgb(0, 0, 192);
                foreach (LayoutControlGroup item in Tab.TabPages)
                {
                    item.Text = GetNN(dtTmp, item.Name, frm.Name);
                    LoadNNGroupControl(frm, item, dtTmp);
                }

                for (int i = 0; i < btnWinUIB.Buttons.Count; i++)
                {
                    try
                    {
                        if (btnWinUIB.Buttons[i].Properties.Tag.ToString() != null)
                        {
                            btnWinUIB.Size = new Size(btnWinUIB.Size.Width, 50);
                            btnWinUIB.AllowGlyphSkinning = false;
                            btnWinUIB.Buttons[i].Properties.Caption = GetNN(dtTmp, btnWinUIB.Buttons[i].Properties.Tag.ToString(), frm.Name);
                            btnWinUIB.Buttons[i].Properties.ToolTip = GetNN(dtTmp, btnWinUIB.Buttons[i].Properties.Tag.ToString(), frm.Name);
                        }
                    }
                    catch
                    {
                    }
                }
            }
            catch
            { }
        }

        public void ThayDoiNN(XtraForm frm, LayoutControlGroup group, TabbedControlGroup Tab, WindowsUIButtonPanel btnWinUIB)
        {
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD , CASE " + Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'" + frm.Name + "' "));
            LoadNNGroupControl(frm, group, dtTmp);
            Tab.DoubleClick += delegate (object a, EventArgs b) { TabbedControlGroup_DoubleClick(Tab, b, frm.Name); };
            Tab.AppearanceTabPage.HeaderActive.ForeColor = Color.FromArgb(0, 0, 192);
            foreach (LayoutControlGroup item in Tab.TabPages)
            {
                item.Text = GetNN(dtTmp, item.Name, frm.Name);
                LoadNNGroupControl(frm, item, dtTmp);
            }
            try
            {
                for (int i = 0; i < btnWinUIB.Buttons.Count; i++)
                {
                    try
                    {
                        if (btnWinUIB.Buttons[i].Properties.Tag.ToString() != null)
                        {
                            btnWinUIB.Size = new Size(btnWinUIB.Size.Width, 50);
                            btnWinUIB.AllowGlyphSkinning = false;
                            btnWinUIB.Buttons[i].Properties.Caption = GetNN(dtTmp, btnWinUIB.Buttons[i].Properties.Tag.ToString(), frm.Name);
                            btnWinUIB.Buttons[i].Properties.ToolTip = GetNN(dtTmp, btnWinUIB.Buttons[i].Properties.Tag.ToString(), frm.Name);
                        }
                    }
                    catch
                    {
                    }
                }
            }
            catch
            { }
        }

        private void LoadNNGroupControl(LayoutControlGroup group, DataTable dtTmp, string name)
        {
            foreach (var gr in group.Items)
            {
                if (gr.GetType().Name == "LayoutControlGroup")
                {
                    LayoutControlGroup gro = (LayoutControlGroup)gr;
                    gro.Text = GetNN(dtTmp, gro.Name, name);
                    gro.AppearanceGroup.ForeColor = Color.FromArgb(0, 0, 192);
                    gro.DoubleClick += delegate (object a, EventArgs b) { ControlGroup_DoubleClick(gro, b, name); };
                    LoadNNGroupControl(gro, dtTmp, name);
                }
                else
                {
                    try
                    {
                        LayoutControlItem control1 = (LayoutControlItem)gr;
                        if (control1.AppearanceItemCaption.ForeColor == Color.FromArgb(192, 0, 0))
                        {
                            control1.AppearanceItemCaption.ForeColor = Color.FromArgb(128, 0, 0);
                        }
                        control1.Text = GetNN(dtTmp, control1.Name, name) + "  ";
                        control1.DoubleClick += delegate (object a, EventArgs b) { Control1_DoubleClick(control1, b, name); };
                        control1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 2, 2);
                        ((DevExpress.XtraEditors.BaseEdit)control1.Control).EnterMoveNextControl = true;

                    }
                    catch (Exception ex)
                    {
                    }
                }

            }
        }
        public void ThayDoiNN(XtraUserControl frm, List<LayoutControlGroup> group, WindowsUIButtonPanel btnWinUIB)
        {
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD , CASE " + Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'" + frm.Name + "' "));
            frm.Text = GetNN(dtTmp, frm.Name, frm.Name);

            List<Control> resultControlList = new List<Control>();
            GetControlsCollection(frm, ref resultControlList, null);
            foreach (Control control in resultControlList)
            {
                try
                {
                    DoiNN(control, frm, dtTmp);
                }
                catch
                { }
            }
            try
            {
                foreach (LayoutControlGroup gr in group)
                {
                    LoadNNGroupControl(gr, dtTmp, frm.Name);
                    gr.DoubleClick += delegate (object a, EventArgs b) { ControlGroup_DoubleClick(gr, b, frm.Name); };
                }
            }
            catch
            {
            }
            try
            {
                for (int i = 0; i < btnWinUIB.Buttons.Count; i++)
                {
                    try
                    {
                        if (btnWinUIB.Buttons[i].Properties.Tag.ToString() != null)
                        {
                            btnWinUIB.Size = new Size(btnWinUIB.Size.Width, 50);
                            btnWinUIB.AllowGlyphSkinning = false;
                            btnWinUIB.Buttons[i].Properties.Caption = GetNN(dtTmp, btnWinUIB.Buttons[i].Properties.Tag.ToString(), frm.Name);
                            btnWinUIB.Buttons[i].Properties.ToolTip = GetNN(dtTmp, btnWinUIB.Buttons[i].Properties.Tag.ToString(), frm.Name);
                        }
                    }
                    catch
                    {
                    }
                }
            }
            catch
            { }
        }
        public void ThayDoiNN(XtraUserControl frm, List<LayoutControlGroup> group, List<WindowsUIButtonPanel> btnWinUIB)
        {
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD , CASE " + Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'" + frm.Name + "' "));
            frm.Text = GetNN(dtTmp, frm.Name, frm.Name);

            List<Control> resultControlList = new List<Control>();
            GetControlsCollection(frm, ref resultControlList, null);
            foreach (Control control in resultControlList)
            {
                try
                {
                    DoiNN(control, frm, dtTmp);
                }
                catch
                { }
            }
            try
            {
                foreach (LayoutControlGroup gr in group)
                {
                    LoadNNGroupControl(gr, dtTmp, frm.Name);
                    gr.DoubleClick += delegate (object a, EventArgs b) { ControlGroup_DoubleClick(gr, b, frm.Name); };
                }
            }
            catch
            {
            }
            try
            {
                foreach (WindowsUIButtonPanel btn in btnWinUIB)
                {

                    for (int i = 0; i < btn.Buttons.Count; i++)
                    {
                        try
                        {
                            if (btn.Buttons[i].Properties.Tag.ToString() != null)
                            {
                                btn.Size = new Size(btn.Size.Width, 50);
                                btn.AllowGlyphSkinning = false;
                                btn.Buttons[i].Properties.Caption = GetNN(dtTmp, btn.Buttons[i].Properties.Tag.ToString(), frm.Name);
                                btn.Buttons[i].Properties.ToolTip = GetNN(dtTmp, btn.Buttons[i].Properties.Tag.ToString(), frm.Name);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch
            { }
        }
        public void DoiNN(Control Ctl, Form frm, DataTable dtNgu)
        {
            // iFontsize
            // sFontForm
            try
            {
                switch (Ctl.GetType().Name.ToString())
                {
                    case "LookUpEdit":
                        {
                            DevExpress.XtraEditors.LookUpEdit CtlDev;
                            CtlDev = (DevExpress.XtraEditors.LookUpEdit)Ctl;
                            CtlDev.Properties.NullText = "";
                            break;
                        }
                    case "Label":
                    case "RadioButton":
                    case "CheckBox":
                        {
                            if (Ctl.Name.ToUpper().Substring(0, 4) != "NONN" & Ctl.Name.Length > 4)
                                Ctl.Text = GetNN(dtNgu, Ctl.Name, frm.Name);// Modules.ObjLanguages.GetLanguage(Modules.ModuleName, frm.Name, Ctl.Name, Modules.TypeLanguage)

                            if (Ctl.GetType().Name.ToString() == "Label")
                            {
                                try
                                {
                                    //Ctl.MouseDoubleClick -= this.Label_MouseDoubleClick;
                                }
                                catch
                                {
                                }
                                try
                                {
                                    //Ctl.MouseDoubleClick += this.Label_MouseDoubleClick;
                                }
                                catch
                                {
                                }
                            }



                            if (Ctl.GetType().Name.ToString() == "RadioButton")
                            {
                                try
                                {
                                    //Ctl.MouseDoubleClick -= this.RadioButton_MouseDoubleClick;
                                }
                                catch
                                {
                                }
                                try
                                {
                                    //Ctl.MouseDoubleClick += this.RadioButton_MouseDoubleClick;
                                }
                                catch
                                {
                                }
                            }

                            if (Ctl.GetType().Name.ToString() == "CheckBox")
                            {
                                try
                                {
                                    //Ctl.MouseDoubleClick -= this.CheckBox_MouseDoubleClick;
                                }
                                catch
                                {
                                }
                                try
                                {
                                    //Ctl.MouseDoubleClick += this.CheckBox_MouseDoubleClick;
                                }
                                catch
                                {
                                }
                            }

                            break;
                        }

                    //case "GroupBox":
                    //    {
                    //        Ctl.Text = GetNN(dtNgu, Ctl.Name, frm.Name);
                    //        if ((Ctl.Name == "grbList"))
                    //        {
                    //            DataTable dtItem = new DataTable();
                    //            try
                    //            {
                    //                dtItem.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "Get_lstDanhsachbaocao", Commons.Modules.UserName, -1, Commons.Modules.TypeLanguage, 1));
                    //            }
                    //            catch (Exception ex)
                    //            {
                    //            }
                    //            foreach (Control ctl1 in Ctl.Controls)
                    //            {
                    //                if ((ctl1.GetType().Name.ToLower() == "navbarcontrol"))
                    //                {
                    //                    foreach (NavBarGroup cl in (NavBarControl)ctl1.Groups)
                    //                        cl.Caption = GetNN(dtNgu, cl.Name, frm.Name);
                    //                    foreach (NavBarItem cl in (NavBarControl)ctl1.Items)
                    //                    {
                    //                        try
                    //                        {
                    //                            cl.Caption = dtItem.Select().Where(x => x("REPORT_NAME").ToString().Trim() == cl.Name.Trim()).Take(1).Single()("TEN_REPORT");
                    //                        }
                    //                        catch (Exception ex)
                    //                        {
                    //                            cl.Caption = GetNN(dtNgu, cl.Name, frm.Name);
                    //                        }
                    //                    }
                    //                    break;
                    //                }
                    //            }
                    //        }

                    //        break;
                    //    }

                    case "TabPage":
                        {
                            Ctl.Text = GetNN(dtNgu, Ctl.Name, frm.Name);          // Modules.ObjLanguages.GetLanguage(Modules.ModuleName, frm.Name, Ctl.Name, Modules.TypeLanguage)
                            break;
                        }

                    case "LabelControl":
                    case "CheckButton":
                    case "CheckEdit":
                        {
                            if (Ctl.Name.ToUpper().Substring(0, 4) != "NONN" & Ctl.Name.Length > 4)
                                Ctl.Text = GetNN(dtNgu, Ctl.Name, frm.Name);
                            if (Ctl.GetType().Name.ToString() == "CheckEdit")
                            {
                                try
                                {
                                    Ctl.MouseDoubleClick += delegate (object a, MouseEventArgs b) { CheckEdit_MouseDoubleClick(Ctl, b, frm.Name); };
                                }
                                catch
                                {
                                }
                            }
                            break;
                        }
                    case "XtraTabPage":
                    case "GroupControl":
                        {
                            if (Ctl.Name.ToUpper().Substring(0, 4) != "NONN" & Ctl.Name.Length > 4)
                                Ctl.Text = GetNN(dtNgu, Ctl.Name, frm.Name);// Modules.ObjLanguages.GetLanguage(Modules.ModuleName, frm.Name, Ctl.Name, Modules.TypeLanguage)
                            if (Ctl.GetType().Name.ToString() == "LabelControl")
                            {
                                try
                                {
                                    Ctl.MouseDoubleClick += delegate (object a, MouseEventArgs b) { Label_MouseDoubleClick(Ctl, b, frm.Name); };

                                }
                                catch
                                {
                                }
                            }
                            if (Ctl.GetType().Name.ToString() == "CheckEdit")
                            {
                                try
                                {
                                    Ctl.MouseDoubleClick += delegate (object a, MouseEventArgs b) { CheckEdit_MouseDoubleClick(Ctl, b, frm.Name); };
                                }
                                catch
                                {
                                }
                            }
                            if (Ctl.GetType().Name.ToString() == "GroupControl")
                            {
                                try
                                {
                                    GroupControl CtlDev;
                                    CtlDev = (GroupControl)Ctl;
                                    CtlDev.AppearanceCaption.ForeColor = Color.FromArgb(0, 0, 192);
                                    CtlDev.MouseDoubleClick += delegate (object a, MouseEventArgs b) { Gropcontrol_MouseDoubleClick(Ctl, b, frm.Name); };
                                }
                                catch
                                {
                                }
                            }

                            break;
                        }

                    case "Button":
                        {
                            if (Ctl.Name.ToUpper().Substring(0, 4) != "NONN" & Ctl.Name.Length > 4)
                            {
                                Ctl.Text = GetNN(dtNgu, Ctl.Name, frm.Name);
                                //LoadImage(Ctl);
                            }

                            break;
                        }

                    case "SimpleButton":
                        {
                            DevExpress.XtraEditors.SimpleButton CtlDev;
                            CtlDev = (DevExpress.XtraEditors.SimpleButton)Ctl;
                            if (Ctl.Name.ToUpper().Substring(0, 4) != "NONN" & Ctl.Name.Length > 4)
                            {
                                Ctl.Text = GetNN(dtNgu, Ctl.Name, frm.Name);
                                //LoadImageDev(CtlDev);
                            }

                            break;
                        }

                    case "RadioGroup":
                        {
                            DevExpress.XtraEditors.RadioGroup radGroup;
                            radGroup = (DevExpress.XtraEditors.RadioGroup)Ctl;
                            for (int i = 0; i <= radGroup.Properties.Items.Count - 1; i++)
                            {
                                if (string.IsNullOrEmpty(radGroup.Properties.Items[i].Tag.ToString()))
                                    radGroup.Properties.Items[i].Tag = radGroup.Properties.Items[i].Description;
                                radGroup.Properties.Items[i].Description = GetNN(dtNgu, radGroup.Properties.Items[i].Tag.ToString(), frm.Name);
                                radGroup.DoubleClick += delegate (object a, EventArgs b) { RadGroup_DoubleClick(radGroup, b, frm.Name); };
                                // Modules.ObjLanguages.GetLanguage(Modules.ModuleName, frm.Name, radGroup.Properties.Items(i).Description, Modules.TypeLanguage)
                            }
                            try
                            {
                                if (radGroup.SelectedIndex == -1)
                                    radGroup.SelectedIndex = 0;
                            }
                            catch
                            {
                            }
                            break;
                        }

                    case "CheckedListBoxControl":
                        {
                            DevExpress.XtraEditors.CheckedListBoxControl chkGroup;
                            chkGroup = (DevExpress.XtraEditors.CheckedListBoxControl)Ctl;

                            for (int i = 0; i <= chkGroup.Items.Count - 1; i++)
                                chkGroup.Items[i].Description = GetNN(dtNgu, chkGroup.Items[i].Description, frm.Name);// Modules.ObjLanguages.GetLanguage(Modules.ModuleName, frm.Name, chkGroup.Items(i).Description, Modules.TypeLanguage)
                            break;
                        }

                    case "XtraTabControl":
                        {
                            DevExpress.XtraTab.XtraTabControl tabControl;
                            tabControl = (DevExpress.XtraTab.XtraTabControl)Ctl;
                            for (int i = 0; i <= tabControl.TabPages.Count - 1; i++)
                            {
                                tabControl.TabPages[i].Text = GetNN(dtNgu, tabControl.TabPages[i].Name, frm.Name);
                                tabControl.TabPages[i].DoubleClick += delegate (object a, EventArgs b) { OSystems_DoubleClick(tabControl.TabPages[i], b, frm.Name); };
                            }
                            break;
                        }

                        //case "GridControl":
                        //    {
                        //        DevExpress.XtraGrid.GridControl grid;
                        //        grid = (DevExpress.XtraGrid.GridControl)Ctl;
                        //        DevExpress.XtraGrid.Views.Grid.GridView mainView = (DevExpress.XtraGrid.Views.Grid.GridView)grid.MainView;
                        //        try { Commons.Modules.OXtraGrid.CreateMenuReset(grid); }
                        //        catch { }

                        //        foreach (DevExpress.XtraGrid.Views.Base.ColumnView view in grid.ViewCollection)
                        //        {
                        //            if ((view) is DevExpress.XtraGrid.Views.Grid.GridView)
                        //            {
                        //                foreach (DevExpress.XtraGrid.Columns.GridColumn col in view.Columns)
                        //                {
                        //                    if (col.Visible)
                        //                    {
                        //                        col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        //                        col.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                        //                        col.AppearanceHeader.Options.UseTextOptions = true;
                        //                        col.Caption = GetNN(dtNgu, col.FieldName, frm.Name);
                        //                        AutoCotDev(col);
                        //                    }
                        //                }
                        //                MVisGrid((DevExpress.XtraGrid.Views.Grid.GridView)view, frm.Name, view.Name.ToString(), Commons.Modules.UserName, true);
                        //                try
                        //                {
                        //                    //view.MouseUp -= this.GridView_MouseUp;
                        //                }
                        //                catch
                        //                {
                        //                }
                        //                try
                        //                {
                        //                    //view.MouseUp += this.GridView_MouseUp;
                        //                }
                        //                catch
                        //                {
                        //                }

                        //                try
                        //                {
                        //                    //view.DoubleClick -= this.GridView_DoubleClick;
                        //                }
                        //                catch
                        //                {
                        //                }

                        //                try
                        //                {
                        //                    //view.DoubleClick += this.GridView_DoubleClick;
                        //                }
                        //                catch
                        //                {
                        //                }
                        //            }
                        //        }

                        //        break;
                        //    }

                }
            }
            catch
            {
            }
        }
        public void DoiNN(Control Ctl, XtraUserControl frm, DataTable dtNgu)
        {
            // iFontsize
            // sFontForm
            try
            {
                switch (Ctl.GetType().Name.ToString())
                {
                    case "LookUpEdit":
                        {
                            DevExpress.XtraEditors.LookUpEdit CtlDev;
                            CtlDev = (DevExpress.XtraEditors.LookUpEdit)Ctl;
                            CtlDev.Properties.NullText = "";
                            break;
                        }
                    case "Label":
                    case "LayoutControlGroup":
                    case "LabelControl":
                    case "GroupControl":
                    case "TextBoxMaskBox":
                    case "RadioButton":
                    //case "CheckEdit":
                    case "CheckBox":

                        {
                            // CheckEdit
                            if (Ctl.Name.ToUpper().Substring(0, 4) != "NONN" & Ctl.Name.Length >= 4)
                                Ctl.Text = GetNN(dtNgu, Ctl.Name, frm.Name);// Modules.ObjLanguages.GetLanguage(Modules.ModuleName, frm.Name, Ctl.Name, Modules.TypeLanguage)

                            if (Ctl.GetType().Name.ToString() == "LabelControl")
                            {
                                try
                                {
                                    Ctl.MouseDoubleClick += delegate (object a, MouseEventArgs b) { Label_MouseDoubleClick(Ctl, b, frm.Name); };

                                }
                                catch
                                {
                                }
                            }
                            if (Ctl.GetType().Name.ToString() == "GroupControl")
                            {
                                try
                                {
                                    GroupControl CtlDev;
                                    CtlDev = (GroupControl)Ctl;
                                    CtlDev.AppearanceCaption.ForeColor = Color.FromArgb(0, 0, 192);
                                    CtlDev.MouseDoubleClick += delegate (object a, MouseEventArgs b) { Gropcontrol_MouseDoubleClick(Ctl, b, frm.Name); };
                                }
                                catch
                                {
                                }
                            }

                            if (Ctl.GetType().Name.ToString() == "RadioButton")
                            {
                                try
                                {
                                    //Ctl.MouseDoubleClick -= this.RadioButton_MouseDoubleClick;
                                }
                                catch
                                {
                                }
                                try
                                {
                                    //Ctl.MouseDoubleClick += this.RadioButton_MouseDoubleClick;
                                }
                                catch
                                {
                                }
                            }

                            if (Ctl.GetType().Name.ToString() == "CheckEdit")
                            {
                                try
                                {
                                    Ctl.MouseDoubleClick += Checkbox_MouseDoubleClick;

                                }
                                catch
                                {
                                }
                            }

                            break;
                        }

                    case "TabPage":
                        {
                            Ctl.Text = GetNN(dtNgu, Ctl.Name, frm.Name);
                            break;
                        }
                    case "Button":
                        {
                            if (Ctl.Name.ToUpper().Substring(0, 4) != "NONN" & Ctl.Name.Length > 4)
                            {
                                Ctl.Text = GetNN(dtNgu, Ctl.Name, frm.Name);
                                //LoadImage(Ctl);
                            }

                            break;
                        }

                    case "SimpleButton":
                        {
                            DevExpress.XtraEditors.SimpleButton CtlDev;
                            CtlDev = (DevExpress.XtraEditors.SimpleButton)Ctl;
                            if (Ctl.Name.ToUpper().Substring(0, 4) != "NONN" & Ctl.Name.Length > 4)
                            {
                                Ctl.Text = GetNN(dtNgu, Ctl.Name, frm.Name);
                                //LoadImageDev(CtlDev);
                            }

                            break;
                        }

                    case "RadioGroup":
                        {
                            DevExpress.XtraEditors.RadioGroup radGroup;
                            radGroup = (DevExpress.XtraEditors.RadioGroup)Ctl;
                            for (int i = 0; i <= radGroup.Properties.Items.Count - 1; i++)
                            {
                                if (string.IsNullOrEmpty(radGroup.Properties.Items[i].Tag.ToString()))
                                    radGroup.Properties.Items[i].Tag = radGroup.Properties.Items[i].Description;
                                radGroup.Properties.Items[i].Description = GetNN(dtNgu, radGroup.Properties.Items[i].Tag.ToString(), frm.Name);
                                radGroup.DoubleClick += delegate (object a, EventArgs b) { RadGroup_DoubleClick(radGroup, b, frm.Name); };

                                // Modules.ObjLanguages.GetLanguage(Modules.ModuleName, frm.Name, radGroup.Properties.Items(i).Description, Modules.TypeLanguage)
                            }
                            //try
                            //{
                            //    if (radGroup.SelectedIndex == -1)
                            //        radGroup.SelectedIndex = 0;
                            //}
                            //catch
                            //{
                            //}

                            break;
                        }

                    case "CheckedListBoxControl":
                        {
                            DevExpress.XtraEditors.CheckedListBoxControl chkGroup;
                            chkGroup = (DevExpress.XtraEditors.CheckedListBoxControl)Ctl;

                            for (int i = 0; i <= chkGroup.Items.Count - 1; i++)
                                chkGroup.Items[i].Description = GetNN(dtNgu, chkGroup.Items[i].Description, frm.Name);// Modules.ObjLanguages.GetLanguage(Modules.ModuleName, frm.Name, chkGroup.Items(i).Description, Modules.TypeLanguage)
                            break;
                        }

                    case "XtraTabControl":
                        {
                            DevExpress.XtraTab.XtraTabControl tabControl;
                            tabControl = (DevExpress.XtraTab.XtraTabControl)Ctl;
                            for (int i = 0; i <= tabControl.TabPages.Count - 1; i++)
                            {
                                tabControl.TabPages[i].Text = GetNN(dtNgu, tabControl.TabPages[i].Name, frm.Name);
                                tabControl.TabPages[i].DoubleClick += delegate (object a, EventArgs b) { OSystems_DoubleClick(tabControl.TabPages[i], b, frm.Name); };

                            }
                            break;
                        }

                        //case "GridControl":
                        //    {
                        //        DevExpress.XtraGrid.GridControl grid;
                        //        grid = (DevExpress.XtraGrid.GridControl)Ctl;
                        //        DevExpress.XtraGrid.Views.Grid.GridView mainView = (DevExpress.XtraGrid.Views.Grid.GridView)grid.MainView;
                        //        try { Commons.Modules.OXtraGrid.CreateMenuReset(grid); } catch { }

                        //        foreach (DevExpress.XtraGrid.Views.Base.ColumnView view in grid.ViewCollection)
                        //        {
                        //            if ((view) is DevExpress.XtraGrid.Views.Grid.GridView)
                        //            {
                        //                foreach (DevExpress.XtraGrid.Columns.GridColumn col in view.Columns)
                        //                {
                        //                    if (col.Visible)
                        //                    {
                        //                        col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        //                        col.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                        //                        col.AppearanceHeader.Options.UseTextOptions = true;
                        //                        col.Caption = GetNN(dtNgu, col.FieldName, frm.Name);      // Modules.ObjLanguages.GetLanguage(Modules.ModuleName, frm.Name, col.Name, Modules.TypeLanguage),

                        //                        AutoCotDev(col);
                        //                    }
                        //                }
                        //                MVisGrid((DevExpress.XtraGrid.Views.Grid.GridView)view, frm.Name, view.Name.ToString(), Commons.Modules.UserName, true);
                        //                try
                        //                {
                        //                    //view.MouseUp -= this.GridView_MouseUp;
                        //                }
                        //                catch
                        //                {
                        //                }
                        //                try
                        //                {
                        //                    //view.MouseUp += this.GridView_MouseUp;
                        //                }
                        //                catch
                        //                {
                        //                }

                        //                try
                        //                {
                        //                    //view.DoubleClick -= this.GridView_DoubleClick;
                        //                }
                        //                catch
                        //                {
                        //                }

                        //                try
                        //                {
                        //                    //view.DoubleClick += this.GridView_DoubleClick;
                        //                }
                        //                catch
                        //                {
                        //                }
                        //            }
                        //        }

                        //        break;
                        //    }

                        //case "DataGridView":
                        //    {
                        //        foreach (DataGridViewColumn cl in (DataGridView)Ctl.Columns)
                        //        {
                        //            cl.HeaderText = GetNN(dtNgu, cl.Name, frm.Name);
                        //            AutoCotGrid(cl);
                        //        }
                        //        (DataGridView)Ctl.ColumnHeadersDefaultCellStyle = Commons.Modules.DataGridViewCellStyle1;
                        //        (DataGridView)Ctl.DefaultCellStyle = Commons.Modules.DataGridViewCellStyle2;
                        //        MVisGrid((DataGridView)Ctl, frm.Name, (DataGridView)Ctl.Name.ToString(), Commons.Modules.UserName);
                        //        break;
                        //    }

                        //case "DataGridViewNew":
                        //    {
                        //        foreach (DataGridViewColumn cl in (DataGridView)Ctl.Columns)
                        //        {
                        //            cl.HeaderText = GetNN(dtNgu, cl.Name, frm.Name);
                        //            AutoCotGrid(cl);
                        //        }

                        //        MVisGrid((DataGridView)Ctl, frm.Name, (DataGridView)Ctl.Name.ToString(), Commons.Modules.UserName);
                        //        break;
                        //    }

                        //case "DataGridViewEditor":
                        //    {
                        //        foreach (DataGridViewColumn cl in (DataGridView)Ctl.Columns)
                        //        {
                        //            cl.HeaderText = GetNN(dtNgu, cl.Name, frm.Name);
                        //            AutoCotGrid(cl);
                        //        }

                        //        (DataGridView)Ctl.ColumnHeadersDefaultCellStyle = Commons.Modules.DataGridViewCellStyle1;
                        //        (DataGridView)Ctl.DefaultCellStyle = Commons.Modules.DataGridViewCellStyle2;

                        //        MVisGrid((DataGridView)Ctl, frm.Name, (DataGridView)Ctl.Name.ToString(), Commons.Modules.UserName);
                        //        break;
                        //    }

                        //case object _ when "NavBarControl" | "navBarControl":
                        //    {
                        //        foreach (NavBarGroup cl in (NavBarControl)Ctl.Groups)
                        //            cl.Caption = GetNN(dtNgu, cl.Name, frm.Name);
                        //        foreach (NavBarItem cl in (NavBarControl)Ctl.Items)
                        //            cl.Caption = GetNN(dtNgu, cl.Name, frm.Name);
                        //        break;
                        //    }
                }
            }
            catch
            {
            }
        }

        private void Ctl_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void OSystems_DoubleClick(object sender, EventArgs e, string sName)
        {
            if (Form.ModifierKeys == Keys.Control)
            {
                XtraTabPage Ctl;
                string sText = "";
                Ctl = (XtraTabPage)sender;
                try
                {
                    sText = XtraInputBox.Show(Ctl.Text, "Sửa ngôn ngữ", "");
                    if (sText == "" || sText == null)
                        return;
                    else if (sText == "Windows.Forms.DialogResult.Retry")
                    {
                        sText = "";
                        CapNhapNN(sName, Ctl.Name.ToUpper().Replace("ItemFor".ToUpper(), ""), sText, true);
                    }
                    else
                        CapNhapNN(sName, Ctl.Name.ToUpper().Replace("ItemFor".ToUpper(), ""), sText, false);
                    sText = " SELECT TOP 1 " + (Commons.Modules.TypeLanguage == 0 ? "VIETNAM" : "ENGLISH") + " FROM LANGUAGES WHERE FORM = '" + sName + "' AND KEYWORD = '" + Ctl.Name.ToUpper().Replace("ItemFor".ToUpper(), "") + "' AND MS_MODULE = 'VS_HRM'";
                    sText = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sText));

                    Ctl.Text = sText;
                }
                catch
                {
                    sText = "";
                }
            }
        }

        private void RadGroup_DoubleClick(object sender, EventArgs e, string sName)
        {
            //sữa ngon ngữ radio group
            if (Form.ModifierKeys == Keys.Control)
            {
                RadioGroup Control;
                string sText = "";
                Control = (RadioGroup)sender;
                RadioGroupItem Ctl = Control.Properties.Items[Control.SelectedIndex];
                try
                {
                    sText = XtraInputBox.Show(Ctl.Description.ToString(), "Sửa ngôn ngữ", "");
                    if (sText == "" || sText == null)
                        return;
                    else
                        CapNhapNN(sName, Ctl.Tag.ToString(), sText, false);
                    sText = " SELECT TOP 1 " + (Commons.Modules.TypeLanguage == 0 ? "VIETNAM" : "ENGLISH") + " FROM LANGUAGES WHERE FORM = '" + sName + "' AND KEYWORD = '" + Ctl.Tag.ToString() + "' AND MS_MODULE = 'VS_HRM'";
                    sText = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sText));

                    Ctl.Description = sText;
                }
                catch
                {
                    sText = "";
                }

            }
        }

        private void CheckEdit_MouseDoubleClick(object sender, MouseEventArgs e, string sName)
        {
            if (Form.ModifierKeys == Keys.Control & e.Button == MouseButtons.Left)
            {
                CheckEdit Ctl;
                string sText = "";
                Ctl = (CheckEdit)sender;
                try
                {
                    sText = XtraInputBox.Show(Ctl.Text, "Sửa ngôn ngữ", "");
                    if (sText == "" || sText == null)
                        return;
                    else
                        CapNhapNN(sName, Ctl.Name, sText, false);
                    sText = " SELECT TOP 1 " + (Commons.Modules.TypeLanguage == 0 ? "VIETNAM" : "ENGLISH") + " FROM LANGUAGES WHERE FORM = '" + sName + "' AND KEYWORD = '" + Ctl.Name + "' AND MS_MODULE = 'VS_HRM'";
                    sText = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sText));
                    Ctl.Text = sText;
                }
                catch
                {
                    sText = "";
                }
            }
        }

        private void Label_MouseDoubleClick(object sender, MouseEventArgs e, string sName)
        {
            if (Form.ModifierKeys == Keys.Control & e.Button == MouseButtons.Left)
            {
                LabelControl Ctl;
                string sText = "";
                Ctl = (LabelControl)sender;
                try
                {
                    sText = XtraInputBox.Show(Ctl.Text, "Sửa ngôn ngữ", "");
                    if (sText == "" || sText == null)
                        return;
                    else
                        CapNhapNN(sName, Ctl.Name, sText, false);
                    sText = " SELECT TOP 1 " + (Commons.Modules.TypeLanguage == 0 ? "VIETNAM" : "ENGLISH") + " FROM LANGUAGES WHERE FORM = '" + sName + "' AND KEYWORD = '" + Ctl.Name + "' AND MS_MODULE = 'VS_HRM'";
                    sText = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sText));
                    Ctl.Text = sText;
                }
                catch
                {
                    sText = "";
                }
            }
        }

        private void Gropcontrol_MouseDoubleClick(object sender, MouseEventArgs e, string sName)
        {
            if (Form.ModifierKeys == Keys.Control & e.Button == MouseButtons.Left)
            {
                GroupControl Ctl;
                string sText = "";
                Ctl = (GroupControl)sender;
                try
                {
                    sText = XtraInputBox.Show(Ctl.Text, "Sửa ngôn ngữ", "");
                    if (sText == "" || sText == null)
                        return;
                    else
                        CapNhapNN(sName, Ctl.Name, sText, false);
                    sText = " SELECT TOP 1 " + (Commons.Modules.TypeLanguage == 0 ? "VIETNAM" : "ENGLISH") + " FROM LANGUAGES WHERE FORM = '" + sName + "' AND KEYWORD = '" + Ctl.Name + "' AND MS_MODULE = 'VS_HRM'";
                    sText = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sText));
                    Ctl.Text = sText;
                }
                catch
                {
                    sText = "";
                }
            }
        }

        private void Checkbox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Form.ModifierKeys == Keys.Control & e.Button == MouseButtons.Left)
            {
                CheckEdit Ctl;
                string sText = "";
                Ctl = (CheckEdit)sender;
                try
                {
                    string sName = GetParentForm(Ctl).Name.ToString(); // DirectCast(Ctl.TopLevelControl, System.Windows.Forms.ContainerControl).ActiveControl.Name.ToString
                    if ("frmReports".ToUpper() == sName.ToUpper())
                    {
                        sName = Ctl.Parent.Parent.ToString().Substring(Ctl.Parent.Parent.ProductName.Length + 1);
                        sName = "SELECT TOP 1 REPORT_NAME FROM dbo.DS_REPORT WHERE NAMES = '" + sName + "' ";
                        try
                        {
                            sName = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sName));
                        }
                        catch
                        {
                            sName = GetParentForm(Ctl).Name.ToString();
                        }
                    }
                    if (sName.Trim().ToString() == "")
                        sName = GetParentForm(Ctl).Name.ToString();
                    sText = XtraInputBox.Show(Ctl.Text, "Sửa ngôn ngữ", "");
                    if (sText == "" || sText == null)
                        return;
                    else
                        sText = " SELECT TOP 1 " + (Commons.Modules.TypeLanguage == 0 ? "VIETNAM" : "ENGLISH") + " FROM LANGUAGES WHERE FORM = '" + sName + "' AND KEYWORD = '" + Ctl.Name + "' AND MS_MODULE = 'VS_HRM'";
                    sText = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sText));
                    Ctl.Text = sText;
                }
                catch
                {
                    sText = "";
                }
            }
        }


        public Form GetParentForm(Control parent)
        {
            Form form = parent as Form;
            if (form != null)
                return form;
            if (parent != null)
                return GetParentForm(parent.Parent);
            return null/* TODO Change to default(_) if this is not a reference type */;
        }
        public void MVisGrid(DevExpress.XtraGrid.Views.Grid.GridView grv, string sForm, string sControl, string UName, bool MDev)
        {
            try
            {
                DataTable dtTmp = new DataTable();
                string sDLieuForm = "";
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "MGetDsCotVis", sForm, sControl, UName));
                if (dtTmp.Rows.Count <= 0)
                    return;

                sDLieuForm = Convert.ToString(dtTmp.Rows[0]["COL_VIS"].ToString());
                if (sDLieuForm.ToUpper() == "ALL")
                    return;


                string[] chuoi_tach = sDLieuForm.Split(new Char[] { '@' });

                foreach (string s in chuoi_tach)
                {
                    if (s.ToString().Trim() != "")
                    {
                        try
                        {
                            grv.Columns[s].Visible = false;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public void AutoCotDev(DevExpress.XtraGrid.Columns.GridColumn col)
        {
            try
            {
                if (col.ColumnType.ToString() == typeof(DateTime).ToString())
                    col.BestFit();
                else if (col.Name.Contains("MS_MAY"))
                    col.BestFit();
                else if (col.Name.Contains("MS_PT"))
                    col.BestFit();
            }
            catch
            {
            }
        }
        public string ConvertNumberToText(double number, string tiente)
        {
            string text = number.ToString("#");
            string[] array = new string[]
            {
        "không",
        "một",
        "hai",
        "ba",
        "bốn",
        "năm",
        "sáu",
        "bảy",
        "tám",
        "chín"
            };
            string[] array2 = new string[]
            {
        "",
        "nghìn",
        "triệu",
        "tỷ"
            };
            string text2 = " ";
            bool flag = false;
            double num = 0.0;
            try
            {
                num = Convert.ToDouble(text.ToString());
            }
            catch
            {
            }
            if (num < 0.0)
            {
                num = -num;
                text = num.ToString();
                flag = true;
            }
            int i = text.Length;
            if (i == 0)
            {
                text2 = array[0] + text2;
            }
            else
            {
                int num2 = 0;
                while (i > 0)
                {
                    int num3 = Convert.ToInt32(text.Substring(i - 1, 1));
                    i--;
                    int num4;
                    if (i > 0)
                    {
                        num4 = Convert.ToInt32(text.Substring(i - 1, 1));
                    }
                    else
                    {
                        num4 = -1;
                    }
                    i--;
                    int num5;
                    if (i > 0)
                    {
                        num5 = Convert.ToInt32(text.Substring(i - 1, 1));
                    }
                    else
                    {
                        num5 = -1;
                    }
                    i--;
                    if (num3 > 0 || num4 > 0 || num5 > 0 || num2 == 3)
                    {
                        text2 = array2[num2] + text2;
                    }
                    num2++;
                    if (num2 > 3)
                    {
                        num2 = 1;
                    }
                    if (num3 == 1 && num4 > 1)
                    {
                        text2 = "một " + text2;
                    }
                    else if (num3 == 5 && num4 > 0)
                    {
                        text2 = "lăm " + text2;
                    }
                    else if (num3 > 0)
                    {
                        text2 = array[num3] + " " + text2;
                    }
                    if (num4 < 0)
                    {
                        break;
                    }
                    if (num4 == 0 && num3 > 0)
                    {
                        text2 = "lẻ " + text2;
                    }
                    if (num4 == 1)
                    {
                        text2 = "mười " + text2;
                    }
                    if (num4 > 1)
                    {
                        text2 = array[num4] + " mươi " + text2;
                    }
                    if (num5 < 0)
                    {
                        break;
                    }
                    if (num5 > 0 || num4 > 0 || num3 > 0)
                    {
                        text2 = array[num5] + " trăm " + text2;
                    }
                    text2 = " " + text2;
                }
            }
            if (flag)
            {
                text2 = "Âm " + text2;
            }
            return text2.Replace("  ", " ") + tiente;
        }


        public string GetNN(DataTable dtNN, string sKeyWord, string sFormName)
        {
            string sNN = "";
            try
            {
                sNN = dtNN.Select("KEYWORD = '" + sKeyWord.ToUpper().Replace("ItemFor".ToUpper(), "") + "' OR KEYWORD = '" + sKeyWord + "' ")[0][1].ToString();
            }
            catch
            {
                if (sKeyWord.Substring(0, 2).ToString().ToLower() == "ch")
                {
                    //sNN = "";
                    sNN = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, sFormName, sKeyWord, Modules.TypeLanguage);
                }
                else
                {
                    sNN = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, sFormName, sKeyWord, Modules.TypeLanguage);
                }
            }
            return sNN;
        }
        public void GetControlsCollection(Control root, ref List<Control> AllControls, Func<Control, Control> filter)
        {
            foreach (Control child in root.Controls)
            {
                if (Commons.Modules.lstControlName.Any(x => x.ToString() == child.GetType().Name))
                    AllControls.Add(child);
                if (child.Controls.Count > 0)
                    GetControlsCollection(child, ref AllControls, filter);
            }
        }
        #endregion

        #region MA HOA

        static string SecurityKey = "vietsoft.com.vn";
        static string chuoi = "_13579_";
        /// <summary>
        /// Encrypt a string using dual encryption method. Return a encrypted cipher Text
        /// </summary>
        /// <param name="toEncrypt">string to be encrypted</param>
        /// <param name="useHashing">use hashing? send to for extra secirity</param>
        /// <returns></returns>
        /// 
        public string Encrypt(string toEncrypt, bool useHashing)
        {
            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(chuoi + toEncrypt + chuoi);

                System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
                // Get the key from config file
                string key = SecurityKey; /*(string)settingsReader.GetValue("SecurityKey", typeof(String));*/
                //System.Windows.Forms.MessageBox.Show(key);
                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    hashmd5.Clear();
                }
                else
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch
            {
                byte[] byteData = Encoding.Unicode.GetBytes("");
                return Convert.ToBase64String(byteData);
            }
        }
        /// <summary>
        /// DeCrypt a string using dual encryption method. Return a DeCrypted clear string
        /// </summary>
        /// <param name="cipherString">encrypted string</param>
        /// <param name="useHashing">Did you use hashing to encrypt this data? pass true is yes</param>
        /// <returns></returns>
        public string Decrypt(string cipherString, bool useHashing)
        {
            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = Convert.FromBase64String(cipherString);

                System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
                //Get your key from config file to open the lock!
                string key = SecurityKey;//(string)settingsReader.GetValue("SecurityKey", typeof(String));

                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    hashmd5.Clear();
                }
                else
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();
                return UTF8Encoding.UTF8.GetString(resultArray).Split(new string[] { chuoi }, StringSplitOptions.None)[1];
            }
            catch
            {
                byte[] byteData = Encoding.Unicode.GetBytes("");
                //return UTF8Encoding.UTF8.GetString(byteData).Split(new string[] { chuoi }, StringSplitOptions.None)[1];
                return Convert.ToBase64String(byteData);
            }
        }


        #endregion

        public Int16 MCot(string sCot)
        {
            string sStmp = "";
            try
            {
                for (int i = 0; i <= sCot.Length - 1; i++)
                {
                    if (sStmp.Length == 0)
                        sStmp = MTimCot(sCot.Substring(i, 1));
                    else
                        sStmp = sStmp + MTimCot(sCot.Substring(i, 1));
                }
            }
            catch
            {
            }
            try
            {
                return Int16.Parse(sStmp);
            }
            catch { return 1; }
        }

        private string MTimCot(string sCot)
        {
            string sTmp = "0";
            try
            {
                if (sCot == "!") return "1";
                if (sCot == "@") return "2";
                if (sCot == "#") return "3";
                if (sCot == "$") return "4";
                if (sCot == "%") return "5";
                if (sCot == "^") return "6";
                if (sCot == "&") return "7";
                if (sCot == "*") return "8";
                if (sCot == "(") return "9";
                if (sCot == ")") return "1";
            }
            catch
            { return "1"; }
            return sTmp;
        }

        #region call api
        public string GetAPI(string url)
        {
            string response = "";
            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                WebClient client = new WebClient();
                client.Encoding = System.Text.UTF8Encoding.UTF8;
                string s = JsonConvert.DeserializeObject(client.DownloadString(Modules.sUrlCheckServer + url)).ToString();
                response = Decrypt(s.ToString(), true);
            }
            catch
            {
                response = "";
            }
            return response;
        }

        public DataTable getDataAPI(string path)
        {
            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                string response = client.DownloadString(path);

                DataTable dt = new DataTable();
                dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.DeserializeObject(response).ToString());

                return dt;
            }
            catch
            {
                return null;
            }
        }
        public object postWebApi(object data, Uri webApiUrl)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            // Set the header so it knows we are sending JSON
            client.Headers[HttpRequestHeader.ContentType] = "application/json";

            // Serialise the data we are sending in to JSON
            string serialisedData = JsonConvert.SerializeObject(data);

            // Make the request
            string response = client.UploadString(webApiUrl, serialisedData);

            // Deserialise the response into a GUID
            return JsonConvert.DeserializeObject(response);
        }


        #endregion

        #region ql user

        public bool checkExitsUser(string sUserName)
        {
            string sSql = "";
            string MName = "";
            try { MName = Environment.MachineName; } catch { }
            sSql = "SELECT COUNT(*) FROM dbo.LOGIN WHERE USER_LOGIN = '" + sUserName + "' AND M_NAME <> N'" + MName + "'";
            if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql).ToString()) > 0)
            {
                return false;
            }
            return true;
        }

        public bool checkExitsUserLG(string sUserName)
        {
            string sSql = "";
            string MName = "";
            try { MName = Environment.MachineName; } catch { }
            sSql = "SELECT COUNT(*) FROM dbo.LOGIN WHERE USER_LOGIN = '" + sUserName + "'";
            if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql).ToString()) > 0)
            {
                return false;
            }
            return true;
        }

        public bool User(string User, int iHD)
        {
            //iHD = 1 là thêm = 2 xóa.
            string sSql = "";
            string MName = "";
            try { MName = Environment.MachineName; } catch { }
            //if (iHD == 1)
            //{
            //    sSql = "INSERT INTO dbo.LOGIN(USER_LOGIN,TIME_LOGIN,ID)VALUES('" + User + "',GETDATE()," + Commons.Modules.iIDUser + ")";
            //}
            try
            {


                if (iHD == 2)
                {
                    sSql = "DELETE FROM dbo.LOGIN WHERE USER_LOGIN = '" + User + "' ";
                    SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                    return true;
                }
                else
                {
                    sSql = "DELETE FROM dbo.LOGIN WHERE USER_LOGIN = '" + User + "' ";
                    SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);

                    sSql = "INSERT dbo.LOGIN(USER_LOGIN, TIME_LOGIN, ID,[USER_NAME],[M_NAME], [VERSION]) VALUES(N'" + User + "',GETDATE(), " + Commons.Modules.iIDUser.ToString() + " , N'" + LoadIPLocal() + "', N'" + MName + "', '" + Commons.Modules.sInfoClient + "' )";
                    SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.MsgError(ex.Message);
                return false;
            }
            //if (iHD == 3)
            //{
            //    sSql = "UPDATE dbo.LOGIN SET TIME_LOGIN = GETDATE() WHERE USER_LOGIN = '" + User + "'";
            //}
            //try
            //{
            //    SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
            //    return true;
            //}
            //catch
            //{
            //    return false;
            //}


        }
        public String LoadIPLocal()
        {
            try
            {
                string ipAddress = "";
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                ipAddress = Convert.ToString(ipHostInfo.AddressList.FirstOrDefault(address => address.AddressFamily == AddressFamily.InterNetwork));
                return ipAddress;
            }
            catch { return "1.2.3.4"; }
        }
        #endregion

        #region creatbt
        public bool MCreateTableToDatatable(string connectionString, string tableSQLName, DataTable table, string sTaoTable)
        {
            try
            {
                if (sTaoTable == "")
                {
                    if (!MCreateTable(tableSQLName, table, connectionString))
                        return false;
                }
                else
                {
                    Commons.Modules.ObjSystems.XoaTable(tableSQLName, connectionString);
                    SqlHelper.ExecuteReader(connectionString, CommandType.Text, sTaoTable);
                }

                using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    System.Data.SqlClient.SqlBulkCopy bulkCopy = new System.Data.SqlClient.SqlBulkCopy(connection, System.Data.SqlClient.SqlBulkCopyOptions.TableLock | System.Data.SqlClient.SqlBulkCopyOptions.FireTriggers | System.Data.SqlClient.SqlBulkCopyOptions.UseInternalTransaction, null);

                    bulkCopy.DestinationTableName = tableSQLName;
                    connection.Open();

                    bulkCopy.WriteToServer(table);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                return false;
            }
            return true;
        }
        public bool MCreateTable(string tableName, DataTable table, string connectionString)
        {
            int i = 1;
            try
            {
                string sql = "CREATE TABLE " + tableName + " (" + "\n";

                // columns
                foreach (DataColumn col in table.Columns)
                {

                    sql += "[" + col.ColumnName + "] " + MGetTypeSql(col.DataType, col.MaxLength > 500 ? 500 : col.MaxLength, 10, 2) + "," + "\n";
                    i += 1;
                }
                sql = sql.Substring(0, sql.Length - 2);
                sql += ")";

                Commons.Modules.ObjSystems.XoaTable(tableName);
                SqlHelper.ExecuteReader(connectionString, CommandType.Text, sql);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void XoaTable(string strTableName)
        {
            try
            {
                strSql = "DROP TABLE " + strTableName;
                SqlHelper.ExecuteScalar(IConnections.CNStr, CommandType.Text, strSql);
            }
            catch
            {
            }
        }

        public void XoaTable(string strTableName, string sCNStr)
        {
            try
            {
                strSql = "DROP TABLE " + strTableName;
                SqlHelper.ExecuteScalar(sCNStr, CommandType.Text, strSql);
            }
            catch
            {
            }
        }




        public string MGetTypeSql(object type, int columnSize, int numericPrecision, int numericScale)
        {
            switch (type.ToString())
            {
                case "System.String":
                    {
                        if ((columnSize >= 2147483646))
                            return "NVARCHAR(MAX)";
                        else
                            return (columnSize == -1) ? "NVARCHAR(MAX)" : "NVARCHAR(" + columnSize.ToString() + ")";
                    }

                case "System.Decimal":
                    {
                        if (numericScale > 0)
                            return "REAL";
                        else if (numericPrecision > 10)
                            return "BIGINT";
                        else
                            return "INT";
                    }

                case "System.Boolean":
                    {
                        return "BIT";
                    }

                case "System.Double":
                    {
                        return "FLOAT";
                    }

                case "System.Single":
                    {
                        return "REAL";
                    }

                case "System.Int64":
                    {
                        return "BIGINT";
                    }

                case "System.Int16":
                    {
                        return "INT";
                    }

                case "System.Int32":
                    {
                        return "INT";
                    }

                case "System.DateTime":
                    {
                        return "DATETIME";
                    }

                case "System.Byte[]":
                    {
                        return "IMAGE";
                    }
                case "System.Byte":
                    {
                        return "tinyint";
                    }

                case "System.Drawing.Image":
                    {
                        return "IMAGE";
                    }

                default:
                    {
                        throw new Exception(type.ToString() + " not implemented.");
                    }
            }
        }
        #endregion

        #region add combobox search

        public void ClearValidationProvider(DXValidationProvider validationProvider)
        {
            FieldInfo fi = typeof(DXValidationProvider).GetField("errorProvider", BindingFlags.NonPublic | BindingFlags.Instance);
            DXErrorProvider errorProvier = fi.GetValue(validationProvider) as DXErrorProvider;
            foreach (Control c in validationProvider.InvalidControls)
            {
                errorProvier.SetError(c, null);
            }

        }

        public void AddCombSearchLookUpEdit(RepositoryItemSearchLookUpEdit cboSearch, string Value, string Display, GridView grv, DataTable dtTmp)
        {
            cboSearch.NullText = "";
            cboSearch.ValueMember = Value;
            cboSearch.DisplayMember = Display;
            cboSearch.DataSource = dtTmp;
            grv.Columns[Value].ColumnEdit = cboSearch;

            foreach (DevExpress.XtraGrid.Columns.GridColumn col in grv.Columns)
            {
                if (col.Visible)
                {

                    col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    col.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    col.AppearanceHeader.Options.UseTextOptions = true;
                    col.Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, "RepositoryItemSearchLookUpEdit", col.FieldName, Modules.TypeLanguage);
                }
            }


        }

        public void AddCombXtra(string Value, string Display, GridView grv, string sSql, string cotan, string fName)
        {
            DataTable tempt = new DataTable();
            tempt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, sSql, Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
            RepositoryItemSearchLookUpEdit cbo = new RepositoryItemSearchLookUpEdit();
            cbo.NullText = "";
            cbo.ValueMember = Value;
            cbo.DisplayMember = Display;
            cbo.DataSource = tempt;
            grv.Columns[Value].ColumnEdit = cbo;
            cbo.View.PopulateColumns(cbo.DataSource);
            Commons.Modules.ObjSystems.MLoadNNXtraGrid(cbo.View, fName);
            cbo.View.Columns[cotan].Visible = false;
        }
        public void AddCombXtra(string Value, string Display, GridView grv, string sSql)
        {
            DataTable tempt = new DataTable();
            tempt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, sSql, Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
            RepositoryItemSearchLookUpEdit cbo = new RepositoryItemSearchLookUpEdit();
            cbo.NullText = "";
            cbo.ValueMember = Value;
            cbo.DisplayMember = Display;
            cbo.DataSource = tempt;

            grv.Columns[Value].ColumnEdit = cbo;
            /*
            DevExpress.XtraGrid.Views.Grid.GridView grv2 = (DevExpress.XtraGrid.Views.Grid.GridView)cbo.DataSource;
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in grv2.Columns)
            {
                if (col.Visible)
                {

                    col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    col.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    col.AppearanceHeader.Options.UseTextOptions = true;
                    col.Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, "RepositoryItemSearchLookUpEdit", col.FieldName, Modules.TypeLanguage);
                }
            }
            */
        }

        public void AddCombXtra(string Value, string Display, GridView grv, DataTable dt)
        {
            RepositoryItemSearchLookUpEdit cbo = new RepositoryItemSearchLookUpEdit();
            cbo.NullText = "";
            cbo.ValueMember = Value;
            cbo.DisplayMember = Display;
            cbo.DataSource = dt;
            grv.Columns[Value].ColumnEdit = cbo;
        }

        public void AddCombXtra(string Value, string Display, GridView grv, DataTable dt, string cotan, string fName, bool CoNull = false)
        {
            if (CoNull)
                dt.Rows.Add(-99, "");
            RepositoryItemSearchLookUpEdit cbo = new RepositoryItemSearchLookUpEdit();
            cbo.NullText = "";
            cbo.ValueMember = Value;
            cbo.DisplayMember = Display;
            cbo.DataSource = dt;
            cbo.BestFitMode = BestFitMode.BestFitResizePopup;
            grv.Columns[Value].ColumnEdit = cbo;
            cbo.View.PopulateColumns(cbo.DataSource);
            cbo.View.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            cbo.View.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            Commons.Modules.ObjSystems.MLoadNNXtraGrid(cbo.View, fName);
            cbo.View.Columns[cotan].Visible = false;
        }

        public void AddCombXtra(string Value, string Display, GridView grv, DataTable tempt, bool Search, string cotan, string fName, bool CoNull = false, bool sort = true)
        {
            if (CoNull)
            {
                DataRow row = tempt.NewRow();
                row[0] = -99;
                row[1] = "";
                tempt.Rows.InsertAt(row, 0);
            }
            if (Search == true)
            {
                RepositoryItemSearchLookUpEdit cbo = new RepositoryItemSearchLookUpEdit();
                cbo.NullText = "";
                cbo.ValueMember = Value;
                cbo.DisplayMember = Display;
                cbo.DataSource = tempt;
                cbo.View.PopulateColumns(cbo.DataSource);
                cbo.View.Columns[cotan].Visible = false;
                cbo.View.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cbo.View.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                Commons.Modules.ObjSystems.MLoadNNXtraGrid(cbo.View, fName);
                grv.Columns[Value].ColumnEdit = cbo;
            }
            else
            {
                RepositoryItemLookUpEdit cbo = new RepositoryItemLookUpEdit();
                cbo.NullText = "";
                cbo.ValueMember = Value;
                cbo.DisplayMember = Display;
                cbo.DataSource = tempt;
                grv.Columns[Value].ColumnEdit = cbo;
                cbo.PopulateColumns();
                cbo.Columns[cotan].Visible = false;
                if (sort == true)
                {
                    cbo.SortColumnIndex = 1;
                }
                cbo.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cbo.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cbo.Columns[Display].Caption = Commons.Modules.ObjLanguages.GetLanguage(fName, Display);
            }
        }


        public void AddCombXtra(string Value, string Display, string Cot, GridView grv, DataTable tempt, bool Search, string cotan, string fName, bool CoNull = false)
        {
            if (CoNull)
                tempt.Rows.Add(-99, "");
            if (Search == true)
            {
                RepositoryItemSearchLookUpEdit cbo = new RepositoryItemSearchLookUpEdit();
                cbo.NullText = "";
                cbo.ValueMember = Value;
                cbo.DisplayMember = Display;
                cbo.DataSource = tempt;
                cbo.View.PopulateColumns(cbo.DataSource);
                cbo.View.Columns[cotan].Visible = false;
                cbo.View.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cbo.View.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                Commons.Modules.ObjSystems.MLoadNNXtraGrid(cbo.View, fName);
                grv.Columns[Cot].ColumnEdit = cbo;
            }
            else
            {
                RepositoryItemLookUpEdit cbo = new RepositoryItemLookUpEdit();
                cbo.NullText = "";
                cbo.ValueMember = Value;
                cbo.DisplayMember = Display;
                cbo.DataSource = tempt;
                grv.Columns[Cot].ColumnEdit = cbo;
                cbo.PopulateColumns();
                cbo.Columns[cotan].Visible = false;
                cbo.SortColumnIndex = 1;
                cbo.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cbo.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cbo.Columns[Display].Caption = Commons.Modules.ObjLanguages.GetLanguage(fName, Display);
            }
        }


        public void AddCombXtra(string Value, string Display, GridView grv, DataTable tempt, bool Search)
        {
            if (Search == true)
            {
                RepositoryItemSearchLookUpEdit cbo = new RepositoryItemSearchLookUpEdit();
                cbo.NullText = "";
                cbo.ValueMember = Value;
                cbo.DisplayMember = Display;
                cbo.DataSource = tempt;
                grv.Columns[Value].ColumnEdit = cbo;
            }
            else
            {
                RepositoryItemLookUpEdit cbo = new RepositoryItemLookUpEdit();
                cbo.NullText = "";
                cbo.ValueMember = Value;
                cbo.DisplayMember = Display;
                cbo.DataSource = tempt;
                grv.Columns[Value].ColumnEdit = cbo;
            }
        }

        public void AddCombo(string Value, string Display, GridView grv, DataTable tempt)
        {
            try
            {
                RepositoryItemLookUpEdit cbo = new RepositoryItemLookUpEdit();
                cbo.NullText = "";
                cbo.ValueMember = Value;
                cbo.DisplayMember = Display;
                cbo.DataSource = tempt;
                //cbo.Columns[Value].Visible = false;
                cbo.BestFitMode = BestFitMode.BestFitResizePopup;
                cbo.DropDownRows = tempt.Rows.Count;
                cbo.SearchMode = SearchMode.AutoComplete;
                cbo.AutoSearchColumnIndex = 1;
                cbo.PopulateColumns();
                grv.Columns[Value].ColumnEdit = cbo;
                grv.BestFitColumns();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void AddMonth(string Value, GridView grv)
        {
            try
            {
                RepositoryItemDateEdit dEdit = new RepositoryItemDateEdit();
                grv.Columns[Value].ColumnEdit = dEdit;
                dEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                dEdit.DisplayFormat.FormatString = "MM/yyyy";
                dEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                dEdit.EditFormat.FormatString = "MM/yyyy";
                dEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                dEdit.MaskSettings.Set("mask", "MM/yyyy");
                dEdit.VistaCalendarInitialViewStyle = DevExpress.XtraEditors.VistaCalendarInitialViewStyle.YearView;
                dEdit.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
            }
            catch
            {
            }
        }


        public void AddComboAnID(string Value, string Display, GridView grv, DataTable tempt)
        {
            try
            {
                RepositoryItemLookUpEdit cbo = new RepositoryItemLookUpEdit();
                cbo.NullText = "";
                cbo.ValueMember = Value;
                cbo.DisplayMember = Display;
                cbo.DataSource = tempt;
                //cbo.Columns[Value].Visible = false;
                cbo.BestFitMode = BestFitMode.BestFitResizePopup;
                cbo.DropDownRows = tempt.Rows.Count;
                cbo.SearchMode = SearchMode.AutoComplete;
                cbo.AutoSearchColumnIndex = 1;
                cbo.PopulateColumns();
                cbo.Columns[0].Visible = false;
                cbo.Columns[1].Caption = Commons.Modules.ObjLanguages.GetLanguage("frmDanhgia", "Ten_NDDG");
                grv.Columns[Value].ColumnEdit = cbo;
                grv.BestFitColumns();
            }
            catch
            {
            }
        }
        public void AddCombo(string Value, string Display, GridView grv, DataTable tempt, bool FontVni)
        {
            try
            {
                RepositoryItemLookUpEdit cbo = new RepositoryItemLookUpEdit();
                cbo.AppearanceDropDown.Options.UseFont = true;
                cbo.NullText = "";
                cbo.ValueMember = Value;
                cbo.DisplayMember = Display;
                cbo.DataSource = tempt;
                cbo.BestFitMode = BestFitMode.BestFitResizePopup;
                cbo.DropDownRows = tempt.Rows.Count;
                cbo.SearchMode = SearchMode.AutoComplete;
                cbo.AutoSearchColumnIndex = 1;
                cbo.PopulateColumns();
                cbo.Columns[0].Visible = false;
                grv.Columns[Value].ColumnEdit = cbo;
                grv.BestFitColumns();
            }
            catch
            {
            }
        }
        public void AddCombobyTree(string Value, string Display, TreeList tree, DataTable tempt)
        {
            RepositoryItemLookUpEdit cbo = new RepositoryItemLookUpEdit();
            cbo.NullText = "";
            cbo.ValueMember = Value;
            cbo.DisplayMember = Display;
            cbo.DataSource = tempt;
            tree.Columns[Value].ColumnEdit = cbo;
        }
        public void AddButonEdit(string Value, GridView view, OpenFileDialog ofdfile, string follder)
        {
            RepositoryItemButtonEdit txtfile = new RepositoryItemButtonEdit();
            view.Columns[Value].ColumnEdit = txtfile;
            txtfile.ButtonClick += delegate (object a, ButtonPressedEventArgs b) { txtfile_ButtonClick(txtfile, null, ofdfile, follder); };
            txtfile.DoubleClick += delegate (object a, EventArgs b) { Txtfile_DoubleClick(txtfile, null, ofdfile, follder); };
        }

        private void Txtfile_DoubleClick(object sender, EventArgs e, OpenFileDialog ofileDialog, string follder)
        {
            try
            {
                ButtonEdit a = sender as ButtonEdit;
                Commons.Modules.ObjSystems.OpenHinh(Commons.Modules.sDDTaiLieu + '\\' + follder + '\\' + a.Text);
            }
            catch
            {
            }
        }
        private void LayDuongDan(OpenFileDialog ofdfile, ButtonEdit txtTaiLieu, string follder)
        {
            try
            {
                var strDuongDanTmp = Commons.Modules.ObjSystems.CapnhatTL(follder);
                string[] sFile;
                string TenFile;

                TenFile = ofdfile.SafeFileName.ToString();
                sFile = System.IO.Directory.GetFiles(strDuongDanTmp);

                if (Commons.Modules.ObjSystems.KiemFileTonTai(strDuongDanTmp + @"\" + ofdfile.SafeFileName.ToString()) == false)
                    txtTaiLieu.Text = strDuongDanTmp + @"\" + ofdfile.SafeFileName.ToString();
                else
                {
                    TenFile = Commons.Modules.ObjSystems.STTFileCungThuMuc(strDuongDanTmp, TenFile);
                    txtTaiLieu.Text = strDuongDanTmp + @"\" + TenFile;
                }
            }
            catch (Exception)
            {
            }
        }
        private void txtfile_ButtonClick(object sender, ButtonPressedEventArgs e, OpenFileDialog ofileDialog, string follder)
        {
            try
            {
                ButtonEdit a = (ButtonEdit)sender;
                if (ofileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (ofileDialog.FileName.ToString().Trim() == "") return;
                    Commons.Modules.ObjSystems.LuuDuongDan(ofileDialog.FileName, ofileDialog.SafeFileName, follder);
                    //a.Text = ofileDialog.SafeFileName;
                }
            }
            catch
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgBanKhongCoQuyenTruyCapDD"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgfrmThongBao"), MessageBoxButtons.OK);
            }
        }

        public void RowFilter(GridControl grid, GridColumn column, string value)
        {
            GridControl _grid = grid;
            GridView _view = grid.MainView as GridView;
            GridColumn _column = column;
            DataTable dt = new DataTable();
            dt = (DataTable)_grid.DataSource;
            if (dt == null) return;
            try
            {
                dt.DefaultView.RowFilter = column.FieldName + " = " + value;
                //_view.SelectRow(0);
            }
            catch (Exception ex)
            {
                dt.DefaultView.RowFilter = "1 = 0";
            }
        }

        public void RowFilter(GridControl grid, GridColumn column1, GridColumn column2, string value1, string value2)
        {
            GridControl _grid = grid;
            GridView _view = grid.MainView as GridView;
            DataTable dt = new DataTable();
            dt = (DataTable)_grid.DataSource;
            if (dt == null) return;
            try
            {
                dt.DefaultView.RowFilter = column1.FieldName + " = " + value1 + " AND " + column2.FieldName + " = " + value2;
                _view.SelectRow(0);
            }
            catch
            {
                dt.DefaultView.RowFilter = "1 = 0";
            }
        }

        #endregion
        public void AddnewRow(GridView view, bool add)
        {
            view.OptionsBehavior.Editable = true;
            if (add == true)
            {
                view.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
                view.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
                view.FocusedRowHandle = GridControl.NewItemRowHandle;
            }
        }
        public void DeleteAddRow(GridView view)
        {
            view.OptionsBehavior.Editable = false;
            view.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
        }
        #region lấy table từ grid
        public DataTable ConvertDatatable(GridControl grid)
        {
            grid.Views[0].PostEditor();
            grid.Views[0].UpdateCurrentRow();
            DataTable dt = new DataTable();
            dt = (DataTable)grid.DataSource;
            return dt;
        }

        public string ConvertCombototext(DataTable dt)
        {
            string resulst = "Giá trị nhập vào :";
            foreach (DataRow item in dt.Rows)
            {
                resulst += '\n' + item[1].ToString().Trim();
            }
            return resulst;
        }

        public DataTable ConvertDatatable(GridView view)
        {
            view.PostEditor();
            view.UpdateCurrentRow();
            DataView dt = (DataView)view.DataSource;
            if (dt == null)
                return null;
            DataTable tempt = dt.ToTable();
            return tempt;

        }


        public DataRow ThongTinChung()
        {
            DataTable tempt = new DataTable();
            tempt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM dbo.THONG_TIN_CHUNG"));
            return tempt.Rows[0];
        }

        public DataRow BLMCPC(Int64 idcn, DateTime ngayhd)
        {
            if (ngayhd > DateTime.MinValue)
            {
                DataTable tempt = new DataTable();
                tempt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM [funGetLuongKyHopDong](" + idcn + ",'" + ngayhd.ToString("MM/dd/yyyy") + "')"));
                if (tempt.Rows.Count == 0)
                    tempt.Rows.Add(idcn, 0, 0, 0);
                return tempt.Rows[0]; ;
            }
            return null;
        }
        public DataRow TienTroCap(Int64 idcn, DateTime ngaynv, int idldtv)
        {
            //ID_CN	LUONG_TRO_CAP	TIEN_TRO_CAP
            DataTable tempt = new DataTable();
            tempt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM [dbo].[GetTienTroCap]('" + ngaynv.ToString("MM/dd/yyyy") + "'," + idcn + "," + idldtv + ")"));
            return tempt.Rows[0];
        }

        public DataRow TienPhep(Int64 idcn, DateTime ngaynv)
        {
            //ID_CN	LUONG_TP	SO_NGAY_PHEP	TIEN_PHEP
            DataTable tempt = new DataTable();
            tempt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM [dbo].[GetTienPhep]('" + ngaynv.ToString("MM/dd/yyyy") + "'," + idcn + ")"));
            return tempt.Rows[0];
        }



        #endregion

        #region Loadcombo phân quyền
        public void LoadCboDonVi(SearchLookUpEdit cboSearch_DV)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDON_VI", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch_DV, dt, "ID_DV", "TEN_DV", "TEN_DV");
                //Modules.ObjLanguages.GetLanguage(Modules.ModuleName, fName, col.FieldName, Modules.TypeLanguage);
                //abc

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        public void LoadCboDonViKO(SearchLookUpEdit cboSearch_DV)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDON_VI", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch_DV, dt, "ID_DV", "TEN_DV", "TEN_DV");
                //Modules.ObjLanguages.GetLanguage(Modules.ModuleName, fName, col.FieldName, Modules.TypeLanguage);
                //abc

                cboSearch_DV.EditValue = 1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        public void LoadCboNguyenQuan(SearchLookUpEdit cboSearch)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNguyenQuan", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch, dt, "NGUYEN_QUAN", "NGUYEN_QUAN2", "NGUYEN_QUAN2");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        public void LoadCboTruongDaoTao(SearchLookUpEdit cboSearch)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTruongDaoTao", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch, dt, "TRUONG_DT", "TRUONG_DT2", "TRUONG_DT2");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        public void LoadCboLinhVucDaoTao(SearchLookUpEdit cboSearch)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLinhcVucDaoTao", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch, dt, "LINH_VUC_DT", "LINH_VUC_DT2", "LINH_VUC_DT2");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        public void LoadCboHinhThucDaoTao(SearchLookUpEdit cboSearch)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboHinhThucDaoTao", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch, dt, "HINH_THUC_DT", "HINH_THUC_DT2", "HINH_THUC_DT2");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        public void LoadCboKhoaDaoTao(SearchLookUpEdit cboSearch)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboKhoaDaoTao", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch, dt, "ID_KDT", "TEN_KHOA_DT", "TEN_KHOA_DT");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        public void LoadCboXiNghiep(SearchLookUpEdit cboSearch_DV, SearchLookUpEdit cboSearch_XN)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboXI_NGHIEP", cboSearch_DV.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch_XN, dt, "ID_XN", "TEN_XN", "TEN_XN");
                cboSearch_XN.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        public void LoadCboTo(SearchLookUpEdit cboSearch_DV, SearchLookUpEdit cboSearch_XN, SearchLookUpEdit cboSearch_TO)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTO", cboSearch_DV.EditValue, cboSearch_XN.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch_TO, dt, "ID_TO", "TEN_TO", "TEN_TO");
                cboSearch_TO.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        public void LoadCboTo(SearchLookUpEdit cboSearch_DV, SearchLookUpEdit cboSearch_XN, SearchLookUpEdit cboSearch_TO, bool bLuong = false)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, bLuong ? "spGetComboToTinhLuong" : "spGetComboTO", cboSearch_DV.EditValue, cboSearch_XN.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch_TO, dt, "ID_TO", "TEN_TO", "TEN_TO");
                cboSearch_TO.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        public void LoadCboLDV(SearchLookUpEdit cboSearch_LDV)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLDV", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1, -1, "-1"));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch_LDV, dt, "ID_LDV", "TEN_LDV", "TEN_LDV");
                cboSearch_LDV.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        public void LoadCboCN(SearchLookUpEdit cboSearch_CN)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCongNhan", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch_CN, dt, "ID_CN", "HO_TEN", "HO_TEN");
                cboSearch_CN.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        public void LoadCboQHGD(SearchLookUpEdit cboSearch_QHGD)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboQH_GD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch_QHGD, dt, "ID_QH", "TEN_QH", "TEN_QH");
                cboSearch_QHGD.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        #endregion

        #region hinh
        //public byte[] SaveHinh(Image inImg)
        //{
        //    System.Drawing.ImageConverter imgCon = new System.Drawing.ImageConverter();
        //    return (byte[])imgCon.ConvertTo(inImg, typeof(byte[]));
        //}
        //public Image LoadHinh(Byte[] hinh)
        //{
        //    Byte[] data = new Byte[0];
        //    data = (Byte[])(hinh);
        //    MemoryStream mem = new MemoryStream(data);
        //    return Image.FromStream(mem);
        //}

        #endregion
        public void LoadCboTTHD(SearchLookUpEdit cboSearch_TTHD)
        {
            try
            {
                DataTable dt = new DataTable();

                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTinhTrangHD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch_TTHD, dt, "ID_TT_HD", "TEN_TT_HD", "TEN_TT_HD");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        public void LoadCboTTHT(SearchLookUpEdit cboSearch_TTHT)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTinhTrangHT", Commons.Modules.UserName, Commons.Modules.TypeLanguage, -1, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch_TTHT, dt, "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        #region data combobox hay dùng
        public DataTable DataLyDoVang(bool coAll, int tinhBH = -1)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLDV", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll, tinhBH));
            return dt;
        }
        public DataTable DataLyDoVang(bool coAll, int tinhBH = -1, string msHienThi = "-1") // -1 hiện tất cả(dùng để load các combo các dữ liệu cũ cho không bị null) , -2 chỉ hiển thị các mã số có mã số hiển thị khác null, dùng để sử dụng các before pubpoup
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLDV", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll, tinhBH, msHienThi));
            return dt;
        }
        public DataTable DataLoaiDieuChinh(bool coAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListLOAI_DIEU_CHINH", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataLoaiCV(bool coAll, Int64 idXN, Int64 iIDDV = -1)
        {
            //ID_LCV,TEN_LCV
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLoaiCV", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll, idXN, iIDDV));
            return dt;
        }
        public DataTable DataLoaiDanhGia(bool coAll)
        {
            //ID_LDG,TEN_LOAI_DANH_GIA
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLoaiDanhGia", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataChucVu(bool coAll, int idLCV)
        {
            //ID_CV,TEN_CV
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboChucVu", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll, idLCV));
            return dt;
        }

        public DataTable DataDanToc(bool coAll)
        {
            //ID_DT,TEN_DT
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDanToc", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataThanhPho(int ID_QG, bool coAll)
        {
            //ID_TP,TEN_TP
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboThanhPho", ID_QG, Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataLoaiSanPham(bool coAll)
        {
            //ID_LSP,TEN_SP
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLoaiSanPham", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataLoaiHangHoa(bool coAll)
        {
            //ID_LSP,TEN_SP
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLoaiHangHoa", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataNhomHangHoa(bool coAll)
        {
            //ID_LSP,TEN_SP
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNhomHangHoa", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataToChuyen(bool coAll)
        {
            //ID_LSP,TEN_SP
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTOCHUYEN", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataCUM(int ID_LSP, bool coAll)
        {
            //ID_CUM,TEN_CUM
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCUM", ID_LSP, Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataBacTho(bool coAll)
        {
            //ID_LSP,TEN_SP
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboBacTho", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataPhuCap(string ngay)
        {
            //ID_LSP,TEN_SP
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetTenPC", Convert.ToDateTime(ngay)));
            return dt;
        }
        public DataTable DataLoaiMay(bool coAll)
        {
            //ID_LSP,TEN_SP
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLoaiMay", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataBenhVien(bool coAll)
        {
            //ID_BV,TEN_BV
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboBenhVien", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataBenhVien(bool coAll, int ID_TINH)
        {
            //ID_BV,TEN_BV
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboBenhVien_Loc", ID_TINH, Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataQuan(int ID_TP, bool coAll)
        {
            //ID_QUAN,TEN_QUAN
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboQuan", ID_TP, Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataPhuongXa(int ID_QUAN, bool coAll)
        {
            //ID_QUAN,TEN_QUAN
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboPhuongXa", ID_QUAN, Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }



        public DataTable DataLyDoThoiViec()
        {
            //ID_LD_TV,TEN_LD_TV
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComBoLyDoThoiViec", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            return dt;
        }

        //public DataTable DataChucVu(bool coAll)
        //{
        //    //ID_CV,TEN_CV
        //    DataTable dt = new DataTable();
        //    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboChucVu", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
        //    return dt;
        //}

        public DataTable DataXepLoai(bool coAll)
        {
            //ID_XL,TEN_XL
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboXepLoai", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataNgachLuong(bool coAll)
        {
            //"ID_NL","TEN_NL"
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNgachLuong", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataCotCapNhat(bool coAll)
        {
            //"ID_COT","TEN_COT"
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCotCapNhat", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataBacLuong(Int64 idnl, Int64 ID_DV, DateTime ngayQD, bool coAll)
        {
            //ID_BL, T1.TEN_BL
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboBacLuong", idnl, ID_DV, ngayQD, Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataNhom(bool coAll)
        {
            //ID_NHOM,TEN_NHOM
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNhomChamCong", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataNhomUser(bool coAll)
        {
            //ID_NHOM,TEN_NHOM
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetNhom", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataCa(int ID_NHOM)
        {
            //ID_CA,CA
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT DISTINCT CA AS ID_CA,CA FROM CHE_DO_LAM_VIEC WHERE ID_NHOM = " + ID_NHOM + " OR " + ID_NHOM + " = -1 ORDER BY CA"));
            return dt;
        }

        public DataTable DataPhanBo(bool coAll)
        {
            //ID_LPB,TEN_LPB
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboPhanBo", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataThongTinChung()
        {
            //ID_CV,TEN_CV
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetThongTinChung", Commons.Modules.UserName, Commons.Modules.TypeLanguage, -1));
            dt.TableName = "TTC";
            return dt;
        }

        public DataTable DataThongTinChung(int? iID_DV = -1)
        {
            //ID_CV,TEN_CV
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetThongTinChung", Commons.Modules.UserName, Commons.Modules.TypeLanguage, iID_DV));
            dt.TableName = "TTC";
            return dt;
        }
        public DataTable DataReportHeader(int ID_DV)
        {
            //ID_CV,TEN_CV
            DataTable dt = new DataTable();
            if (Commons.Modules.loadHeader == 1)
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetHeaderDonVi", Commons.Modules.UserName, Commons.Modules.TypeLanguage, ID_DV));
            }
            else
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetThongTinDonVi", Commons.Modules.UserName, Commons.Modules.TypeLanguage, ID_DV));
            }
            dt.TableName = "TTC";
            return dt;
        }

        public DataTable DataKhenThuongKyLuat(bool coAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboKhenThuongKyLuat", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataXacNhanGio(bool coAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComBoXAC_NHAN_GIO", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataLoaiTienThuong(bool coAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComBoDM_LOAI_TIEN_THUONG", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataLoaiKhenThuong(bool coAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLoaiKhenThuong", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataNguoiKy()
        {
            //ID_NK, HO_TEN
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNguoiKy", Commons.Modules.UserName));
            return dt;
        }

        public DataTable DataYeuCauTD(bool coAll, int TT)
        {
            //ID_YCTD,MA_YCTD
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboYeuCauTD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll, TT));
            return dt;
        }

        public DataTable DataKeHoachPV(bool coAll, int TT)
        {
            //ID_KHPV,SO_KHPV
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboKeHoachPV", coAll, TT));
            return dt;
        }

        public DataTable DataTinhTrang(bool coAll)
        {
            //ID_TT, TenTT
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTinhTrang", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataTinhTrangDT(bool coAll)
        {
            //T.ID_TT_DT, TEN_TT_DT
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTinhTrangDT", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataTinhTrangTD(bool coAll)
        {
            //ID_TTTD, Ten_TTTD
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTinhTrangTD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataTinhTrangPV(bool coAll)
        {
            //ID_TT_KHPV, TEN_TT_KHPV
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTinhTrangPV", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataTinhTrangYC(bool coAll)
        {
            //ID_TTYC, Ten_TTYC
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTinhTrangYC", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataTinhTrangCVYC(bool coAll)
        {
            //ID_TT_VT, Ten_TT_VT
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDataTinhTrangCVYC", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataTinhTrangDuyet(bool coAll)
        {
            //ID_TTD,TEN_TT_DUYET
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTinhTrangDuyet", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataCongNhanTheoDK(bool coAll, Int32 ID_DV, Int32 ID_XN, Int32 ID_TO, DateTime TNgay, DateTime DNgay)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCongNhanTheoDieuKien", Commons.Modules.UserName, Commons.Modules.TypeLanguage, ID_DV, ID_XN, ID_TO, TNgay, DNgay, coAll));
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Int64 GetNguoiKyMacDinh()
        {
            try
            {
                return Convert.ToInt64(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.fnGetNguoiKyMacDinh()"));
            }
            catch
            {
                return 0;
            }
        }
        public DataTable DataQuocGia(bool coAll)
        {
            //ID_QG,TEN_QG
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboQuocGia", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataTinHTrangHD(bool coAll)
        {
            //"ID_TT_HD", "TEN_TT_HD",
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTinHTrangHD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataLoaiTinHTrangHT(bool coAll)
        {
            //"ID_LTTHT", "TEN_LOAI_TTHT,
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLoaiTinhTrangHT", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataTinHTrangHT(int ID_LTTHT, bool coAll)
        {
            //"ID_TT_HT", "TEN_TT_HT,
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTinHTrangHT", Commons.Modules.UserName, Commons.Modules.TypeLanguage, ID_LTTHT, coAll));
            return dt;
        }

        public DataTable DataTinhTrangUV(bool coAll)
        {
            //"ID_TT_UV", "TEN_TT_UV,
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTinhTrangUV", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataTinhTrangUVPV(bool coAll)
        {
            //"DAT", "TEN_TT_UVPV,
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTinhTrangUVPV", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataCongNhanVien(bool coAll)
        {
            //"ID_CV", "TEN_CV,
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCongNhanVien", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataCongTheoNgayUV()
        {
            //"ID_CV", "TEN_CV,
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLocTheoNgayUV", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            return dt;
        }

        public DataTable DataTinHTrangHN(bool coAll)
        {
            //"ID_TT_HT", "TEN_TT_HT,
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTinHTrangHN", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }


        public DataTable DataNguyenNhanTN(bool coAll)
        {
            //         ID_NGUYEN_NHAN,TEN_NGUYEN_NHAN
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNguyenNhanTN", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataYeuToTN(bool coAll)
        {
            //ID_GAY_TAI_NAN,TEN_YEU_TO
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboYeuToTN", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataNgheNghiep(bool coAll)
        {
            //ID_NGHE_NGHIEP,TEN_NGHE_NGHIEP
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNgheNghiep", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataMucDoTN(bool coAll)
        {
            //ID_MUCDO,TEN_MUCDO
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComBoMucDo", Commons.Modules.TypeLanguage));
            return dt;
        }
        public DataTable DataTinhTrangGiaDinh(bool coAll)
        {
            //ID_TT_HN,TEN_TT_HN
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComBoTinhTrangHonNhan", Commons.Modules.TypeLanguage));
            return dt;
        }


        public DataTable DataNoiDungDanhGia(bool coAll)
        {
            //ID_NDDG,TEN_NDDG
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNoiDungDanhGia", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataPhai()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboPhai", Commons.Modules.TypeLanguage));
            return dt;
        }
        public DataTable DataHinhThucTuyen(bool coAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboHinhThucTuyen", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataMucUuTienTD(bool coAll)
        {
            //ID_MUT,TEN_MUT
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboMucDoUuTienTD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataMucLuong(bool coAll)
        {
            //ID_ML,TEN_ML
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboMucLuong", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }


        public DataTable DataLoaiCV(bool coAll)
        {
            //ID_LCV,TEN_LCV
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLoaiCV", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll, -1, -1));
            return dt;
        }
        public DataTable DataMucDoTieng(bool coAll)
        {
            //ID_MD,TEN_MD
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboMucDoTieng", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }


        public DataTable DataViTri(Int64 iID_YCTD, bool ColAll)
        {
            //ID_VTTD,TEN_VTTD
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboViTriTheoYeuCau", iID_YCTD, Commons.Modules.UserName, Commons.Modules.TypeLanguage, ColAll));
            return dt;
        }

        public DataTable DataDanhGiaTayNghe(bool coAll)
        {
            //ID_DGTN,TEN_DGTN
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDanhGiaTayNge", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataNguoiDanhGia(Int64 iYCTD, Int64 iVTTD, Int64 iDV, Int64 iXN, int active, int iLoaiDG = -1)
        {
            //ID_NGUOI_DGTN,TEN_NGUOI_DGTN
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNguoiDanhGia", Commons.Modules.UserName, iYCTD, iVTTD, iDV, iXN, active, iLoaiDG));
            return dt;
        }

        public DataTable DataDanhNoiDungDT(bool coAll)
        {
            //ID_DGTN,TEN_DGTN
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNoiDungDT", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataLoaiHinhCV(bool coAll)
        {
            //ID_LHCV,TEN_LHCV
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLoaiHinhCV", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataKinhNghiemLV(bool coAll)
        {
            //ID_KNLV,TEN_KNLV
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboKinhNghiemLV", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataLoaiTuyen(bool coAll)
        {
            //ID_LOAI_TUYEN,TEN_LOAI_TUYEN
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLoaiTuyen", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataNganhTD(bool coAll)
        {
            //ID_NGANH_TD,TEN_NGANH_TD
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNganhTD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataNguonTD(bool coAll)
        {
            //ID_NTD,TEN_NTD
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNguonTD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataCTL(bool coAll)
        {
            //ID_CTL,TEN_CTL
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_CTL,TEN as TEN_CTL FROM dbo.CACH_TINH_LUONG ORDER BY TEN"));
            return dt;
        }

        public DataTable DataLoaiHDLD(bool coAll)
        {
            //ID_LHDLD,TEN_LHDLD
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLoaiHopDongLD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }


        public DataTable DataLoaiTrinhDo(bool coAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLoaiTrinhDo", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataTayNghe(bool coAll)
        {
            //ID_TAY_NGHE,TEN_TAY_NGHE
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTayNghe", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataChuyenMon(bool coAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboChuyenMon", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataNoiDungThuongKhacLuong(bool coAll, int id = -1)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNoiDungThuongKhacLuong", id, Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataLoaiQuyetDinh(bool CoAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComBoLoaiQuyetDinh", Commons.Modules.TypeLanguage));
            return dt;
        }
        public DataTable DataHinhThucTroCap(int id, bool coAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboHTNhanTC", Commons.Modules.UserName, Commons.Modules.TypeLanguage, id, coAll));
            return dt;
        }
        public DataTable DataCongNhan(bool coAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCongNhan", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataCongNhanTheoLoaiCV(Int64 iIDLCV)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCongNhanTheoLoaiCV", Commons.Modules.UserName, Commons.Modules.TypeLanguage, iIDLCV));
            return dt;
        }
        public DataTable DataCongNhanTheoBoPhan(Int64 iIDXN)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCongNhanTheoBP", Commons.Modules.UserName, Commons.Modules.TypeLanguage, iIDXN));
            return dt;
        }

        public DataTable TruongBoPhan(Int64 iID_YCTD)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComBoTruongBoPhan", iID_YCTD));
            return dt;
        }

        public DataTable DataCongNhan(bool coAll, int TT)
        {
            //1 còn làm
            //2 đã nghĩ
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComBoCongNhanTheoTT", Commons.Modules.UserName, TT, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataUngVienTheoTT(bool coAll, int TT)
        {
            //1 chưa truyển
            //2 đã tuyển
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboUngVienTheoTT", Commons.Modules.UserName, TT, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataDonVi(bool coAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDON_VI", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            //Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboDV, dt, "ID_DV", "TEN_DV", "TEN_DV");
            return dt;
        }
        public DataTable DataDonVi(bool coAll, string sUserName) // THEO ADMIN
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDON_VI", sUserName, Commons.Modules.TypeLanguage, coAll));
            //Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboDV, dt, "ID_DV", "TEN_DV", "TEN_DV");
            return dt;
        }
        public DataTable DataXiNghiep(int iddv, bool coAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboXI_NGHIEP", iddv, Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataXiNghiep(int iddv, bool coAll, string sUserName)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboXI_NGHIEP", iddv, sUserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataTo(int iddv, int idxn, bool CoAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTO", iddv, idxn, Commons.Modules.UserName, Commons.Modules.TypeLanguage, CoAll));
            return dt;
        }
        public DataTable DataTo(int iddv, int idxn, bool CoAll, string sUserName)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTO", iddv, idxn, sUserName, Commons.Modules.TypeLanguage, CoAll));
            return dt;
        }
        public DataTable DataToTheoLoaiChuyen(int iddv, int idxn, bool CoAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboToTheoLoaiChuyen", iddv, idxn, Commons.Modules.UserName, Commons.Modules.TypeLanguage, CoAll));
            return dt;
        }

        public DataTable DataTDVH(int LoaiTD, bool CoAll)
        {
            //ID_TDVH,TEN_TDVH
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTrinhDo", LoaiTD, Commons.Modules.UserName, Commons.Modules.TypeLanguage, CoAll));
            return dt;
        }
        public DataTable DataQHGD(bool CoAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboQuanHeGD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, CoAll));
            return dt;
        }

        public DataTable DataLoaiQuocTich(bool CoAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLoaiQuocTich", Commons.Modules.UserName, Commons.Modules.TypeLanguage, CoAll));
            return dt;
        }
        public DataTable DataCapGiayPhep(bool CoAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCapGiayPhep", Commons.Modules.UserName, Commons.Modules.TypeLanguage, CoAll));
            return dt;
        }

        public DataTable DataLyDoGiamLDNN(bool CoAll)
        {
            //ID_LDG_LDNN,TEN_LDG_LDNN
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLyDoGiamLDNN", Commons.Modules.UserName, Commons.Modules.TypeLanguage, CoAll));
            return dt;
        }

        public int DataTinhTrangBangLuong(int ID_DV, DateTime dThang) // 1: Đang tính lương, 2 đã khóa
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetTinhTrangThang", Commons.Modules.UserName, Commons.Modules.TypeLanguage, ID_DV, dThang, Commons.Modules.iLoaiKhoa));
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            catch
            {
                return -1;
            }
        }

        #endregion

        #region Định dạng
        public string sDinhDangSoLe(int iSoLe)
        {
            string sChuoi = "#,##0";
            if (iSoLe != 0)
            {
                sChuoi = sChuoi + ".";
                for (int i = 0; i <= iSoLe - 1; i++)
                    sChuoi = sChuoi + "0";
            }
            return sChuoi;
        }

        public string sDinhDangSoLe(int iSoLe, string sChuoi)
        {
            if (iSoLe != 0)
            {
                sChuoi = sChuoi + ".";
                for (int i = 0; i <= iSoLe - 1; i++)
                    sChuoi = sChuoi + "0";
            }
            return sChuoi;
        }
        #endregion
        public bool IsnullorEmpty(object input)
        {
            bool resust = false;
            try
            {
                if (input.ToString() == "" || input.ToString() == "0")
                {
                    resust = true;
                }
            }
            catch (Exception)
            {
                resust = true;
            }
            return resust;
        }


        public void MChooseGrid(bool bChose, string sCot, DevExpress.XtraGrid.Views.Grid.GridView grv)
        {
            try
            {
                int i;
                i = 0;
                for (i = 0; i <= grv.RowCount; i++)
                {
                    grv.SetRowCellValue(i, sCot, bChose);
                    grv.UpdateCurrentRow();
                }
            }
            catch
            {
            }
        }

        public void GotoHome(XtraUserControl uc)
        {
            try
            {
                foreach (Control c in uc.ParentForm.Controls)
                {
                    if (c.GetType().Name.ToString() == "TablePanel")
                    {
                        TablePanel table = c as TablePanel;
                        foreach (Control item in table.Controls)
                        {
                            if (item.GetType().Name.ToString() == "TileBar")
                            {
                                TileBar tb = item as TileBar;
                                tb.SelectedItem = tb.GetTileGroupByName("titlegroup").GetTileItemByName("58");
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }
        public void GotoCongNhan(NavigationFrame uc)
        {
            try
            {
                foreach (Control c in uc.Controls)
                {
                    if (c.GetType().Name.ToString() == "TablePanel")
                    {
                        TablePanel table = c as TablePanel;
                        foreach (Control item in table.Controls)
                        {
                            if (item.GetType().Name.ToString() == "TileBar")
                            {
                                TileBar tb = item as TileBar;
                                tb.SelectedItem = tb.GetTileGroupByName("titlegroup").GetTileItemByName("45");
                            }
                        }

                    }

                }
            }
            catch (Exception ex) { }
        }

        public SplashScreenManager splashScreenManager1;
        public SplashScreenManager ShowWaitForm(XtraUserControl a)
        {
            if (splashScreenManager1 != null) splashScreenManager1.Dispose();
            splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(a.ParentForm, typeof(frmWaitFormCustom), true, true, false);
            splashScreenManager1.ShowWaitForm();
            Thread.Sleep(100);
            return splashScreenManager1;
        }
        public SplashScreenManager ShowWaitForm(XtraForm a)
        {
            if (splashScreenManager1 != null) splashScreenManager1.Dispose();
            splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(a, typeof(frmWaitFormCustom), true, true, false);
            splashScreenManager1.ShowWaitForm();
            Thread.Sleep(100);
            return splashScreenManager1;
        }
        public void HideWaitForm()
        {
            try
            {
                splashScreenManager1.CloseWaitForm();
            }
            catch
            {
            }
        }

        public void showWaitFormVietSoft(Action worker, XtraForm a)
        {
            try
            {
                using(frmWaitFormVS frm = new frmWaitFormVS(worker))
                {
                    frm.StartPosition = FormStartPosition.CenterParent; // Hiển thị form chờ đợi ở giữa form chính
                    frm.Show(a); // Sử dụng phương thức Show thay vì ShowDialog
                }
            }
            catch { }
        }

        public void showWaitFormVietSoft(Action worker, XtraUserControl a)
        {
            try
            {
                using (frmWaitFormVS frm = new frmWaitFormVS(worker))
                {
                    frm.StartPosition = FormStartPosition.CenterParent; // Hiển thị form chờ đợi ở giữa form chính
                    frm.ShowDialog(a); // Sử dụng phương thức Show thay vì ShowDialog
                }
            }
            catch { }
        }
    }
}
