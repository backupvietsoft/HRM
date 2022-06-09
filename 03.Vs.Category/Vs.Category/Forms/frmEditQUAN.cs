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
using DevExpress.XtraEditors.DXErrorProvider;

namespace Vs.Category
{
    public partial class frmEditQUAN : DevExpress.XtraEditors.XtraForm
    {

        static Int64 iId = -1;
        static Boolean bAddEdit = true;  // true la add false la edit
        string MA = "";
        DataTable dtTPho = new DataTable();
        Boolean bLoad = true;
        public frmEditQUAN(Int64 iIdForm, Boolean bAddEditForm)
        {
            InitializeComponent();
            iId = iIdForm;
            bAddEdit = bAddEditForm;

            //CustomValidationRule cusValidation = new CustomValidationRule();
            //cusValidation.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDaTonTaiTrongCSDL");
            //cusValidation.ErrorType = ErrorType.Critical;


            //dxValidationProvider1.SetValidationRule(this.MS_QUANTextEdit, cusValidation);
            //dxValidationProvider1.SetValidationRule(this.TEN_QUANTextEdit, cusValidation);
            //dxValidationProvider1.SetValidationRule(this.TEN_QUAN_ATextEdit, cusValidation);
            //dxValidationProvider1.SetValidationRule(this.TEN_QUAN_HTextEdit, cusValidation);
            

        }

        private void frmEdirQUAN_Load(object sender, EventArgs e)
        {
            bLoad = true;
            LoadQuocGia();
            dtTPho.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListTHANH_PHO", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            bLoad = false;
            try
            {
                if (string.IsNullOrEmpty(ID_QGSearchLookUpEdit.EditValue.ToString()))
                    LoadThanhPho(-1);
                else
                    LoadThanhPho(Int64.Parse(ID_QGSearchLookUpEdit.EditValue.ToString()));
            }catch { LoadThanhPho(-1); }
            if (!bAddEdit) LoadText();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnWDUI);

        }

        private void frmEdirQUAN_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();


        private void LoadQuocGia()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListQUOC_GIA", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_QGSearchLookUpEdit, dt, "ID_QG", "TEN_QG", "TEN_QG");
       //     ID_QGSearchLookUpEdit.Properties.DataSource = dt;
       //     ID_QGSearchLookUpEdit.Properties.ValueMember = "ID_QG";
       //     ID_QGSearchLookUpEdit.Properties.DisplayMember = "TEN_QG";
       //     ID_QGSearchLookUpEdit.Properties.PopulateViewColumns();

            try
            {

                ID_QGSearchLookUpEdit.Properties.View.Columns["ID_QG"].Visible = false;
            //    ID_QGSearchLookUpEdit.Properties.View.Columns["ID_QG"].Visible = false;
            //    ID_QGSearchLookUpEdit.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.None;

           //     ID_QGSearchLookUpEdit.Properties.View.Columns["TEN_QG"].Caption = Commons.Modules.ObjLanguages.GetLanguage("ucListDMuc", "TEN_QG");
            //    ID_QGSearchLookUpEdit.Properties.View.Columns["TEN_QG"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                string sSql = "";

                if (bAddEdit)
                {
                    try
                    {
                        sSql = " SELECT TOP 1 T2.ID_QG FROM dbo.QUAN T1 INNER JOIN dbo.THANH_PHO T2 ON T2.ID_TP = T1.ID_TP WHERE T1.ID_QUAN = 234"; //+ iId.ToString();
                        sSql = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql).ToString());
                        ID_QGSearchLookUpEdit.EditValue = Convert.ToInt64(sSql);
                    }
                    catch
                    {
                        if (dt.Rows.Count > 0) ID_QGSearchLookUpEdit.EditValue = dt.Rows[0][0];
                    }

                }


            }
            catch (Exception EX)
            {
                XtraMessageBox.Show(EX.Message.ToString());

            }
        }

        private void LoadThanhPho(Int64 iIdQGia)
        {
            if (bLoad) return;


            try
            {
                DataTable dt = new DataTable();
                dt = dtTPho.Copy();
                dt.DefaultView.RowFilter = " ID_QG = " + iIdQGia.ToString();
                dt = dt.DefaultView.ToTable();
                ID_TPSearchLookUpEdit.Properties.DataSource = null;
                ID_TPSearchLookUpEdit.EditValue = null;
                ID_TPSearchLookUpEdit.Properties.View.Columns.Clear();
                /*
                ID_TPSearchLookUpEdit.Properties.DataSource = dt;
                ID_TPSearchLookUpEdit.Properties.ValueMember = "ID_TP";
                ID_TPSearchLookUpEdit.Properties.DisplayMember = "TEN_TP";
                ID_TPSearchLookUpEdit.Properties.PopulateViewColumns();

                ID_TPSearchLookUpEdit.Properties.View.Columns["ID_TP"].Visible = false;
                ID_TPSearchLookUpEdit.Properties.View.Columns["ID_QG"].Visible = false;
                ID_TPSearchLookUpEdit.Properties.View.Columns["TEN_QG"].Visible = false;
                ID_TPSearchLookUpEdit.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.None;
                ID_TPSearchLookUpEdit.Properties.View.Columns["TEN_TP"].Caption = Commons.Modules.ObjLanguages.GetLanguage("ucListDMuc", "TEN_TP");
                ID_TPSearchLookUpEdit.Properties.View.Columns["TEN_TP"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                */
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_TPSearchLookUpEdit, dt, "ID_TP", "TEN_TP", "TEN_TP");
                if (ID_TPSearchLookUpEdit.Properties.View.Columns["ID_TP"] != null) ID_TPSearchLookUpEdit.Properties.View.Columns["ID_TP"].Visible = false;
                if (ID_TPSearchLookUpEdit.Properties.View.Columns["ID_QG"] != null) ID_TPSearchLookUpEdit.Properties.View.Columns["ID_QG"].Visible = false;
                if (ID_TPSearchLookUpEdit.Properties.View.Columns["TEN_QG"] != null) ID_TPSearchLookUpEdit.Properties.View.Columns["TEN_QG"].Visible = false;
                string sSql = "";

                if (bAddEdit)
                {
                    try
                    {
                        if (dt.Rows.Count > 0)
                        {
                            sSql = "SELECT T2.ID_TP FROM dbo.QUAN T1 INNER JOIN dbo.THANH_PHO T2 ON T2.ID_TP = T1.ID_TP WHERE T1.ID_QUAN =  " + iId.ToString();
                            sSql = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql).ToString());
                            ID_TPSearchLookUpEdit.EditValue = Convert.ToInt64(sSql);
                        }
                    }
                    catch
                    {
                        if (dt.Rows.Count > 0) ID_TPSearchLookUpEdit.EditValue = dt.Rows[0][0];
                    }

                }


            }
            catch (Exception EX)
            {
                XtraMessageBox.Show(EX.Message.ToString());

            }
        }

        private void LoadText()
        {
            try
            {
                string sSql = "";
                sSql = "SELECT T1.*,T2.ID_QG FROM dbo.QUAN T1 INNER JOIN dbo.THANH_PHO T2 ON T2.ID_TP = T1.ID_TP WHERE ID_QUAN =  " + iId.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                if (dtTmp.Rows.Count <= 0) return;

                bLoad = true;
                ID_QGSearchLookUpEdit.EditValue = dtTmp.Rows[0]["ID_QG"];
                bLoad = false;
                try
                {
                    if (string.IsNullOrEmpty(ID_QGSearchLookUpEdit.EditValue.ToString()))
                        LoadThanhPho(-1);
                    else
                        LoadThanhPho(Int64.Parse(ID_QGSearchLookUpEdit.EditValue.ToString()));
                }
                catch { LoadThanhPho(-1); }
                ID_TPSearchLookUpEdit.EditValue = dtTmp.Rows[0]["ID_TP"];
                MS_QUANTextEdit.EditValue = dtTmp.Rows[0]["MS_QUAN"];
                MA = dtTmp.Rows[0]["MS_QUAN"].ToString();
                TEN_QUANTextEdit.EditValue = dtTmp.Rows[0]["TEN_QUAN"];
                TEN_QUAN_ATextEdit.EditValue = dtTmp.Rows[0]["TEN_QUAN_A"];
                TEN_QUAN_HTextEdit.EditValue = dtTmp.Rows[0]["TEN_QUAN_H"];

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());

            }

        }

        private void LoadTextNull()
        {
            try
            {
                MS_QUANTextEdit.EditValue = String.Empty;
                TEN_QUANTextEdit.EditValue = String.Empty;
                TEN_QUAN_ATextEdit.EditValue = String.Empty;
                TEN_QUAN_HTextEdit.EditValue = String.Empty;
                MS_QUANTextEdit.Focus();
            }
            catch { }
        }


        private void btnWDUI_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            DevExpress.XtraBars.Docking2010.WindowsUIButton btn = e.Button as DevExpress.XtraBars.Docking2010.WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            try
            {
                switch (btn.Tag.ToString())
                {
                    case "luu":
                        {
                            if (!dxValidationProvider1.Validate()) return;
                            if (bKiemTrung()) return;
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateQuan", (bAddEdit ? -1 : iId), MS_QUANTextEdit.EditValue, TEN_QUANTextEdit.EditValue, TEN_QUAN_ATextEdit.EditValue, TEN_QUAN_HTextEdit.EditValue,ID_TPSearchLookUpEdit.EditValue).ToString();

                            if (bAddEdit)
                            {
                                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_ThemThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    LoadTextNull();
                                    return;
                                }
                            }
                            this.DialogResult = DialogResult.OK;
                            this.Close();
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

        private void ID_QGSearchLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (bLoad) return;
            try
            {
                if (string.IsNullOrEmpty(ID_QGSearchLookUpEdit.EditValue.ToString()))
                    LoadThanhPho(-1);
                else
                    LoadThanhPho(Int64.Parse(ID_QGSearchLookUpEdit.EditValue.ToString()));
            }
            catch { LoadThanhPho(-1); }
        }

        private bool bKiemTrung()
        {
            try
            {

                DataTable dtTmp = new DataTable();
                Int16 iKiem = 0;
                if (bCheckMS_QUAN() & MA != MS_QUANTextEdit.EditValue.ToString())
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_MaSoTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                    MS_QUANTextEdit.Focus();
                    return true;
                }
                

                iKiem = 0;
                iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_QUAN", (bAddEdit ? "-1" : iId.ToString()), "QUAN", "TEN_QUAN",TEN_QUANTextEdit.EditValue,"","","","").ToString());
                if (iKiem > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                    TEN_QUANTextEdit.Focus();
                    return true;
                }


                iKiem = 0;
                if (!string.IsNullOrEmpty(TEN_QUANTextEdit.EditValue.ToString()))
                {     
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_QUAN", (bAddEdit ? "-1" : iId.ToString()), "QUAN", "TEN_QUAN_A", TEN_QUAN_ATextEdit.EditValue, "", "", "", "").ToString());
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        TEN_QUAN_ATextEdit.Focus();
                        return true;
                    }
                }
                iKiem = 0;
                if (!string.IsNullOrEmpty(TEN_QUAN_HTextEdit.EditValue.ToString()))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_QUAN", (bAddEdit ? "-1" : iId.ToString()), "QUAN", "TEN_QUAN_H", TEN_QUAN_HTextEdit.EditValue, "", "", "", "").ToString());
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        TEN_QUAN_HTextEdit.Focus();
                        return true;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show(ex.Message.ToString());
                return false;
            }
            return false;
        }

        private bool bCheckMS_QUAN()
        {
            try
            { 
                string sSql = "SELECT COUNT(*) AS SL FROM dbo.QUAN T1 INNER JOIN dbo.THANH_PHO T2 ON T2.ID_TP = T1.ID_TP  WHERE (ID_QUAN <> " + (bAddEdit ? "-1" : iId.ToString()) + ") AND (T2.ID_QG = " + ID_QGSearchLookUpEdit.EditValue + ") AND (T1.MS_QUAN = '" +  MS_QUANTextEdit.EditValue + "')";
                int  iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr,CommandType.Text, sSql).ToString());
                if (iKiem > 0) return true; else return false;
                
            }
            catch 
            {return false;}
        }

        //public class CustomValidationRule : ValidationRule
        //{
        //    public override bool Validate(Control control, object value)
        //    {
        //        if (control.Name.ToString().ToUpper() == "MS_QUANTextEdit".ToUpper())
        //        {
        //            if (!string.IsNullOrEmpty(control.Text.ToString().Trim()))
        //            {
        //                this.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDaTonTaiTrongCSDL");
        //                this.ErrorType = ErrorType.Critical;
        //                control.Focus();
        //                return CheckWhetherExists(value, "MS_QUAN");
                        
        //            }
        //            else
        //            {
        //                this.ErrorText = Commons.Modules.ObjLanguages.GetLanguage("frmMain", "msgVuiLongNhapDuLieu");
        //                this.ErrorType = ErrorType.Warning;
        //                return false;
        //            }
        //        }



        //        if (control.Name.ToString().ToUpper() == "TEN_QUAN_ATextEdit".ToUpper() && !string.IsNullOrEmpty(control.Text.ToString().Trim()))
        //        {


        //            if (!CheckWhetherExists(value, "TEN_QUAN_A"))
        //            {
        //                this.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDaTonTaiTrongCSDL");
        //                this.ErrorType = ErrorType.Critical;
        //                control.Focus();
        //                return false;
        //            }
                    
        //        }

        //        if (control.Name.ToString().ToUpper() == "TEN_QUAN_HTextEdit".ToUpper() && !string.IsNullOrEmpty(control.Text.ToString().Trim()))
        //        {

        //            if (!CheckWhetherExists(value, "TEN_QUAN_H"))
        //            {
        //                this.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDaTonTaiTrongCSDL");
        //                this.ErrorType = ErrorType.Critical;
        //                control.Focus();
        //                return false;
        //            }
                    
                    
        //        }


        //        if (control.Name.ToString().ToUpper() == "TEN_QUANTextEdit".ToUpper())
        //        {
        //            if (!string.IsNullOrEmpty(control.Text.ToString().Trim()))
        //            {
        //                this.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDaTonTaiTrongCSDL");
        //                this.ErrorType = ErrorType.Critical;
        //                control.Focus();
        //                return CheckWhetherExists(value, "TEN_QUAN");
        //            }
        //            else
        //            {
        //                this.ErrorText = Commons.Modules.ObjLanguages.GetLanguage("frmMain", "msgVuiLongNhapDuLieu");
        //                this.ErrorType = ErrorType.Warning;
        //                return false;
        //            }

        //        }
        //        return true;
        //    }
        //}

        //static bool CheckWhetherExists(object value, string colName)
        //{
        //    int iKiem = 0;
        //    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_QUAN", (bAddEdit ? "-1" : iId.ToString()), "QUAN", colName, value.ToString()).ToString());
        //    if (iKiem > 0)
        //        return false;
        //    else
        //        return true;
            
        //}



         

    }
}