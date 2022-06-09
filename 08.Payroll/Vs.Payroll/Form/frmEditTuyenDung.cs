using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;

namespace Vs.Payroll
{
    public partial class frmEditTuyenDung : DevExpress.XtraEditors.XtraForm
    {
        Int64 iIdTo = 0;
        Boolean bAddEditTo = true;  // true la add false la edit

        public frmEditTuyenDung(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
            iIdTo = iId;
            bAddEditTo = bAddEdit;
        }
        private void frmEditTuyenDung_Load(object sender, EventArgs e)
        {
            LoadCombobox();
            if (!bAddEditTo) LoadText();
        }
        private void LoadCombobox()
        {
            try
            {
                Commons.Modules.ObjSystems.MLoadLookUpEdit(TEN_LOAI_SAN_PHAMLookUpEdit, Commons.Modules.ObjSystems.DataLoaiSanPham(false),"ID_LSP", "TEN_LOAI_SAN_PHAM",Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_LOAI_SAN_PHAM"),true);
            }
            catch
            {
            }
        }
        private void LoadText()
        {
            string sSql = "";
            sSql = "SELECT * FROM dbo.[CUM] WHERE ID_CUM = " + iIdTo.ToString();
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
            if (dtTmp.Rows.Count <= 0) return;

            TEN_LOAI_SAN_PHAMLookUpEdit.EditValue = dtTmp.Rows[0]["ID_LSP"];
            MS_CUMTextEdit.EditValue = dtTmp.Rows[0]["MS_CUM"];
            TEN_CUMTextEdit.EditValue = dtTmp.Rows[0]["TEN_CUM"];
            STT_CUMTextEdit.EditValue = dtTmp.Rows[0]["STT"];
            TINH_TGCheckEdit.EditValue = dtTmp.Rows[0]["TINH_TG"];
            LOAI_CUMTextEdit.EditValue = dtTmp.Rows[0]["LOAI_CUM"];
            CUM_PSCheckEdit.EditValue = dtTmp.Rows[0]["CUM_PS"];
            CUM_CUOICheckEdit.EditValue = dtTmp.Rows[0]["CUM_CUOI"];

           

        }

        private void windowsUIButtonPanel2_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            try
            {
                switch (btn.Tag.ToString())
                {
                    case "luu":
                        {
                            if (!dxValidationProvider1.Validate()) return;
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateCUM", (bAddEditTo ? -1 : iIdTo), 
                                MS_CUMTextEdit.EditValue, 
                                TEN_CUMTextEdit.EditValue, 
                                STT_CUMTextEdit.EditValue, 
                                TEN_LOAI_SAN_PHAMLookUpEdit.EditValue, 
                                TINH_TGCheckEdit.EditValue, 
                                LOAI_CUMTextEdit.EditValue,  
                                CUM_PSCheckEdit.EditValue, 
                                CUM_CUOICheckEdit.EditValue
                            //    @ID_CUM INT,
                            //    @MS_CUM NVARCHAR(10),
                            //    @TEN_CUM NVARCHAR(100),
                            //    @STT SMALLINT,
                            //    @ID_LSP INT,
                            //    @TINH_TG BIT,
                            //    @LOAI_CUM NVARCHAR(10),
                            //    @CUM_PS BIT,
                            //    @CUM_CUOI BIT
                                ).ToString();
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
            }catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void frmEditTuyenDung_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

    }

}
