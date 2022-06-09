using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010;
using Microsoft.ApplicationBlocks.Data;
using Vs.Report;

namespace Vs.HRM
{
    public partial class ucTinhHinhSuDungLaoDong : DevExpress.XtraEditors.XtraUserControl
    {
        public ucTinhHinhSuDungLaoDong()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            dtNAM.EditValue = DateTime.Now;
            dtTmp = LoadText();
            ShowText(dtTmp);
        }
        bool flag = true;
        private void ShowText(DataTable dtTmp)
        {
            try
            {
                flag = false;

                if (dtTmp != null &&dtTmp.Rows.Count>0)
                {
                    Id =  Int64.Parse( dtTmp.Rows[0]["ID"].ToString());
                    rdo_ChonBaoCao.SelectedIndex = ( (bool.Parse( dtTmp.Rows[0]["DAU_NAM"].ToString()) == true) ? 0 : 1);
                    dtNAM.DateTime = new DateTime(int.Parse(dtTmp.Rows[0]["NAM"].ToString()), 1, 1);
                    txTONG_DK.EditValue = dtTmp.Rows[0]["TONG_DK"].ToString();
                    txTONG_DK_NU.EditValue = dtTmp.Rows[0]["TONG_DK_NU"].ToString();
                    txLD_KTH_DK.EditValue = dtTmp.Rows[0]["LD_KTH_DK"].ToString();
                    txLD_KTH_DK_NU.EditValue = dtTmp.Rows[0]["LD_KTH_DK_NU"].ToString();
                    txLD_13_DK.EditValue = dtTmp.Rows[0]["LD_13_DK"].ToString();
                    txLD_13_DK_NU.EditValue = dtTmp.Rows[0]["LD_13_DK_NU"].ToString();
                    txLD_D1_DK.EditValue = dtTmp.Rows[0]["LD_D1_DK"].ToString();
                    txLD_D1_DK_NU.EditValue = dtTmp.Rows[0]["LD_D1_DK_NU"].ToString();
                    txTU_TUYEN.EditValue = dtTmp.Rows[0]["TU_TUYEN"].ToString();
                    txTUYEN_QUA_TT.EditValue = dtTmp.Rows[0]["TUYEN_QUA_TT"].ToString();
                    AddEdit = false;
                }
                else
                {
                    Id = -1;
                    txTONG_DK.EditValue = 0;
                    txTONG_DK_NU.EditValue = 0;
                    txLD_KTH_DK.EditValue = 0;
                    txLD_KTH_DK_NU.EditValue = 0;
                    txLD_13_DK.EditValue = 0;
                    txLD_13_DK_NU.EditValue = 0;
                    txLD_D1_DK.EditValue = 0;
                    txLD_D1_DK_NU.EditValue = 0;
                    txTU_TUYEN.EditValue = 0;
                    txTUYEN_QUA_TT.EditValue = 0;
                    AddEdit = true;
                }
                flag = true;

            }
            catch (Exception EX)
            {
                XtraMessageBox.Show(EX.Message.ToString());
            }
        }
        DataTable dtTmp;
        private DataTable LoadText()
        {
            try
            {
                string sSql = "";

                sSql += "SELECT";
                sSql += " ID ";
                sSql += ",[NAM]";
                sSql += ",[DAU_NAM]";
                sSql += ",[TONG_DK]";
                sSql += ",[TONG_DK_NU]";
                sSql += ",[LD_KTH_DK]";
                sSql += ",[LD_KTH_DK_NU]";
                sSql += ",[LD_13_DK]";
                sSql += ",[LD_13_DK_NU]";
                sSql += ",[LD_D1_DK]";
                sSql += ",[LD_D1_DK_NU]";
                sSql += ",[TU_TUYEN]";
                sSql += ",[TUYEN_QUA_TT]";
                sSql += "FROM[LAO_DONG_DU_KIEN]";
                sSql += " ";
                sSql += " WHERE [NAM] = " + dtNAM.DateTime.Year;
                sSql += "AND [DAU_NAM] =" + ( (rdo_ChonBaoCao.SelectedIndex == 0) ? 1 : 0);
                dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                if (dtTmp != null && dtTmp.Rows.Count > 0)
                {
                    AddEdit = true;
                }
                else
                {
                    AddEdit = false;
                    Id = -1;

                }
                return dtTmp;
            }
            catch
            {
                AddEdit = false;
            }
            return null;
        }
        static Int64 Id = -1;
        static Boolean AddEdit = true;  // true la add false la edit
        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "Print":
                    {
                        // lưu trươc khi in 
                        try
                        {
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateLAO_DONG_DU_KIEN",
                                (AddEdit ? -1 : Id).ToString(),
                                (dtNAM.EditValue == null) ? 0 : dtNAM.DateTime.Year,
                                (rdo_ChonBaoCao.SelectedIndex==0)? 1 : 0,
                                (txTONG_DK.EditValue == null) ? 0 : txTONG_DK.EditValue,
                                (txTONG_DK_NU.EditValue == null) ? 0 : txTONG_DK_NU.EditValue,
                                (txLD_KTH_DK.EditValue == null) ? 0 : txLD_KTH_DK.EditValue,
                                (txLD_KTH_DK_NU.EditValue == null) ? 0 : txLD_KTH_DK_NU.EditValue,
                                (txLD_13_DK.EditValue == null) ? 0 : txLD_13_DK.EditValue,
                                (txLD_13_DK_NU.EditValue == null) ? 0 : txLD_13_DK_NU.EditValue,
                                (txLD_D1_DK.EditValue == null) ? 0 : txLD_D1_DK.EditValue,
                                (txLD_D1_DK_NU.EditValue == null) ? 0 : txLD_D1_DK_NU.EditValue,
                                (txTU_TUYEN.EditValue == null) ? 0 : txTU_TUYEN.EditValue,
                                (txTUYEN_QUA_TT.EditValue == null) ? 0 : txTUYEN_QUA_TT.EditValue
                                ).ToString();

                            Id = Int64.Parse( Commons.Modules.sId);
                            if (Id != -1)
                                AddEdit = false;
                        }
                        catch (Exception ex)
                        {

                        }

                      // lấy dữ liệu sau khi lưu
                    switch (rdo_ChonBaoCao.SelectedIndex)
                        {
                            case 0:
                                {
                                    System.Data.SqlClient.SqlConnection conn;
                                    DataTable dt = new DataTable();
                                    frmViewReport frm = new frmViewReport();
                                    DateTime firstDateTime = new DateTime(Convert.ToInt32(dtNAM.DateTime.Year), 1, 1);
                                    DateTime secondDateTime = new DateTime(Convert.ToInt32(dtNAM.DateTime.Year), 6, 30);
                                    string sTieuDe = "TÌNH HÌNH SỬ DỤNG LAO ĐỘNG 6 THÁNG ĐẦU NĂM " + Convert.ToString(dtNAM.DateTime.Year);
                                    frm.rpt = new rptBCTinhHinhSuDungLaoDong(lk_NgayIn.DateTime,sTieuDe);

                                    try
                                    {
                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCTinhHinhSuDungLaoDong", conn);

                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dtNAM.EditValue;
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


                                }
                                break;
                            case 1:
                                {
                                    DateTime firstDateTime = new DateTime(Convert.ToInt32(dtNAM.DateTime.Year), 7, 1);
                                    DateTime secondDateTime = new DateTime(Convert.ToInt32(dtNAM.DateTime.Year), 12, 31);
                                    string sTieuDe = "TÌNH HÌNH SỬ DỤNG LAO ĐỘNG 6 THÁNG CUÔI NĂM NĂM " + Convert.ToString(dtNAM.DateTime.Year);
                                    DataTable dt = new DataTable();
                                    System.Data.SqlClient.SqlConnection conn1;
                                    DataTable dt1 = new DataTable();
                                    frmViewReport frm1 = new frmViewReport();
                                    frm1.rpt = new rptBCTinhHinhSuDungLaoDong(lk_NgayIn.DateTime, sTieuDe);

                                    try
                                    {
                                        conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn1.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCTinhHinhSuDungLaoDong", conn1);

                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = firstDateTime;
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = secondDateTime;
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


                                    frm1.ShowDialog();
                                }
                                break;
                          
                            default:
                                break;
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        private void ucTinhHinhSuDungLaoDong_Load(object sender, EventArgs e)
        {
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(LK_DON_VI);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
            dtNAM.EditValue = DateTime.Today;
            lk_NgayIn.EditValue = DateTime.Today;
        }

        private void LK_DON_VI_EditValueChanged(object sender, EventArgs e)
        {
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
        }

        private void LK_XI_NGHIEP_EditValueChanged(object sender, EventArgs e)
        {
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
        }

        private void dtNAM_Validated(object sender, EventArgs e)
        {
            if (flag)
            {
                dtTmp = LoadText();
                ShowText(dtTmp);
            }
        }

        private void rdo_ChonBaoCao_TabIndexChanged(object sender, EventArgs e)
        {
         
        }

        private void rdo_ChonBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (flag)
            {
                dtTmp = LoadText();
                ShowText(dtTmp);
            }
        }

        private void txLD_KTH_DK_NU_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
