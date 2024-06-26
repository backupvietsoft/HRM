﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace Vs.Report
{
    public partial class rptQuyetDinhChamDutHDLD_DM : DevExpress.XtraReports.UI.XtraReport
    {
        public rptQuyetDinhChamDutHDLD_DM(DateTime ngayin)
        {
            InitializeComponent();
            //Commons.Modules.ObjSystems.ThayDoiNN(this);
            DataTable dtNgu = new DataTable();
            dtNgu.Load(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'NgayThangNam' "));
            DataTable dt = Commons.Modules.ObjSystems.DataThongTinChung(-1);
            this.DataSource = dt;
            //try
            //{
            //    picLogo.SizeF = new SizeF((float)Convert.ToDecimal(dt.Rows[0]["LG_WITH"]), (float)Convert.ToDecimal(dt.Rows[0]["LG_HEIGHT"]));
            //}
            //catch { }
            string Ngay = "0" + ngayin.Day;
            string Thang = "0" + ngayin.Month;
            string Nam = "00" + ngayin.Year;

            //lbNgayKy.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay", "NgayThangNam") + " " + Ngay.Substring(Ngay.Length - 2, 2) + " " +
            //    Commons.Modules.ObjSystems.GetNN(dtNgu, "Thang", "NgayThangNam") + " " + Thang.Substring(Thang.Length - 2, 2) + " " +
            //    Commons.Modules.ObjSystems.GetNN(dtNgu, "Nam", "NgayThangNam") + " " + Nam.Substring(Nam.Length - 4, 4);
        }

    }
}


