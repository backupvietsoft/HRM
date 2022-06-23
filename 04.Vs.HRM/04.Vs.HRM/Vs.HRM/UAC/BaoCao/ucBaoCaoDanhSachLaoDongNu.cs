﻿using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Report;

namespace Vs.HRM
{
    public partial class ucBaoCaoDanhSachLaoDongNu : DevExpress.XtraEditors.XtraUserControl
    {
        public ucBaoCaoDanhSachLaoDongNu()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "Print":
                    {

                        System.Data.SqlClient.SqlConnection conn;
                        DataTable dt = new DataTable();
                        Vs.Report.frmViewReport frm = new frmViewReport();
                        frm.rpt = new rptBCDSLaoDongNu(lk_NgayIn.DateTime, lk_NgayIn.DateTime, lk_NgayIn.DateTime);
                        try
                        {
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();

                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCDSLaoDongNu", conn);

                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                            cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = lk_NgayIn.EditValue;
                            cmd.CommandType = CommandType.StoredProcedure;
                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                            DataSet ds = new DataSet();
                            adp.Fill(ds);


                            dt = new DataTable();
                            dt = ds.Tables[1].Copy();
                            dt.TableName = "DA_TA2";
                            frm.AddDataSource(dt);

                            dt = new DataTable();
                            dt = ds.Tables[0].Copy();
                            dt.TableName = "DA_TA";
                            frm.AddDataSource(dt);

                        
                        }
                        catch 
                        {
                        }


                        frm.ShowDialog();
                        break;



                    }
                default:
                    break;
            }
        }

        private void ucBaoCaoDanhSachLaoDongNu_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(LK_DON_VI);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
            Commons.OSystems.SetDateEditFormat(lk_NgayIn);

            lk_NgayIn.EditValue = DateTime.Today;
            Commons.Modules.sLoad = "";

        }

        private void LK_DON_VI_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
        }

        private void LK_XI_NGHIEP_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
        }

        private void tablePanel1_Validated(object sender, EventArgs e)
        {

        }

       
       
    }
}