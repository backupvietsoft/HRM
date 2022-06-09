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
using DevExpress.XtraBars.Docking2010;

namespace Vs.Payroll
{
    public partial class frmEditKTTenKhongDauSTK : DevExpress.XtraEditors.XtraForm
    {
        Int64 Id = 0;
        Boolean AddEdit = true;  // true la add false la edit
        public frmEditKTTenKhongDauSTK(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }

        private void frmEditKTTenKhongDauSTK_Load(object sender, EventArgs e)
        {
           // LoadCheDoNghi();
            if (!AddEdit) LoadText();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
        }
        private void frmEditKTTenKhongDauSTK_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

     //   private void LoadCheDoNghi()
     //   {
      //      DataTable dt = new DataTable();
       //     dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListLOAI_MAY", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
      //      MA_SO_LOAI_MAYTextEdit.Properties.DataSource = dt;
      //      MA_SO_LOAI_MAYTextEdit.Properties.ValueMember = "MS_LOAI_MAY";
      //      MA_SO_LOAI_MAYTextEdit.Properties.DisplayMember = "TEN_LOAI_MAY";
      //      MA_SO_LOAI_MAYTextEdit.Properties.PopulateViewColumns();
//
     //       try
      //      {
//
      //          MA_SO_LOAI_MAYTextEdit.Properties.View.Columns["ID_CHE_DO"].Visible = false;
     //           MA_SO_LOAI_MAYTextEdit.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.None;
      //          MA_SO_LOAI_MAYTextEdit.Properties.View.Columns["TEN_CHE_DO"].Caption = Commons.Modules.ObjLanguages.GetLanguage("ucListDMuc", "TEN_CHE_DO");
      //          MA_SO_LOAI_MAYTextEdit.Properties.View.Columns["TEN_CHE_DO"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
     //       }
      //      catch (Exception EX)
       //     {
       //         XtraMessageBox.Show(EX.Message.ToString());
      //      }
      //  }
        private void LoadText()
        {
            try
            {
                string sSql = "SELECT * FROM dbo.CONG_NHAN WHERE ID_CN =	" + Id.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                lblMS_CN.Text = "MS CN: " + dtTmp.Rows[0]["MS_CN"].ToString();
                lblHo.Text = (Commons.Modules.TypeLanguage==0? "Họ và tên: " : "Name: ") + dtTmp.Rows[0]["ho"].ToString();
                lblTen.Text = dtTmp.Rows[0]["ten"].ToString();
                TEN_KHONG_DAUTextEdit.EditValue = dtTmp.Rows[0]["TEN_KHONG_DAU"].ToString();
                SO_TAI_KHOANTextEdit.EditValue = dtTmp.Rows[0]["SO_TAI_KHOAN"];
              
               
            }
            catch (Exception EX)
            {

                XtraMessageBox.Show(EX.Message.ToString());
            }
        }
        
        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {

                    case "luu":
                        {
       
                            if (!dxValidationProvider1.Validate()) return;
                            //if (bKiemTrung()) return;
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateKTTenKhongDauSTK", (AddEdit ? -1 : Id),
                                SO_TAI_KHOANTextEdit.EditValue,
                                TEN_KHONG_DAUTextEdit.EditValue
                                ).ToString();
                           
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                            break;
                        }
                    case "thoat":
                        {
                            this.Close();
                            break;
                        }
                    default: break;
                }
            }
            catch (Exception EX)
            {

                XtraMessageBox.Show(EX.Message.ToString());
            }

        }
        //private bool bKiemTrung()
        //{
        //    try
        //    {
        //        DataTable dtTmp = new DataTable();
        //        Int16 iKiem = 0;

        //        iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_LDV",
        //            (AddEdit ? "-1" : Id.ToString()), "LY_DO_VANG", "TEN_LDV", KY_HIEUTextEdit.EditValue.ToString(),
        //            "", "", "", ""));
        //        if (iKiem > 0)
        //        {
        //            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTEN_LDVNayDaTonTai"));
        //            KY_HIEUTextEdit.Focus();
        //            return true;
        //        }

        //        iKiem = 0;

        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message.ToString());
        //        return true;
        //    }
        //    return false;
        //}
    }
}