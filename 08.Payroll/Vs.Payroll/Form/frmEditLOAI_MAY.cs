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
    public partial class frmEditLOAI_MAY : DevExpress.XtraEditors.XtraForm
    {
        Int64 Id = 0;
        Boolean AddEdit = true;  // true la add false la edit
        public frmEditLOAI_MAY(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }

        private void frmEditLOAI_MAY_Load(object sender, EventArgs e)
        {
            // LoadCheDoNghi();
            if (!AddEdit)
            {
                LoadText();
            }
            else
            {
                SD_TRONG_QTCNCheckEdit.Checked = true;
            }

            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
        }
        private void frmEditLOAI_MAY_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

   
        private void LoadText()
        {
            try
            {
                string sSql = "SELECT * FROM dbo.LOAI_MAY WHERE ID_LM =	" + Id.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                TEN_LOAI_MAYTextEdit.EditValue = dtTmp.Rows[0]["TEN_LOAI_MAY"].ToString();
                TEN_LOAI_MAY_ATextEdit.EditValue = dtTmp.Rows[0]["TEN_LOAI_MAY_A"].ToString();
                TEN_LOAI_MAY_HTextEdit.EditValue = dtTmp.Rows[0]["TEN_LOAI_MAY_H"].ToString();
                KY_HIEUTextEdit.EditValue = dtTmp.Rows[0]["KI_HIEU"].ToString();
                SD_TRONG_QTCNCheckEdit.EditValue = dtTmp.Rows[0]["SD_QTCN"];
                THU_TU_MAYTextEdit.EditValue = dtTmp.Rows[0]["STT_MAY"].ToString();
            }
            catch (Exception EX)
            {

                XtraMessageBox.Show(EX.Message.ToString());
            }
        }
        private void LoadTextNull()
        {
            try
            {
                TEN_LOAI_MAYTextEdit.EditValue = String.Empty;
                TEN_LOAI_MAY_ATextEdit.EditValue = String.Empty;
                TEN_LOAI_MAY_HTextEdit.EditValue = String.Empty;
                KY_HIEUTextEdit.EditValue = String.Empty;
                THU_TU_MAYTextEdit.EditValue = String.Empty;
                SD_TRONG_QTCNCheckEdit.EditValue = false;
                TEN_LOAI_MAYTextEdit.Focus();
            }
            catch { }
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
                           
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateLOAI_MAY", (AddEdit ? -1 : Id),
                                TEN_LOAI_MAYTextEdit.EditValue,
                                TEN_LOAI_MAY_ATextEdit.EditValue,
                                TEN_LOAI_MAY_HTextEdit.EditValue,
                                (THU_TU_MAYTextEdit.EditValue == "") ? THU_TU_MAYTextEdit.EditValue = null : THU_TU_MAYTextEdit.EditValue,
                                SD_TRONG_QTCNCheckEdit.EditValue,
                                KY_HIEUTextEdit.EditValue
                                ).ToString();
                            if (AddEdit)
                            {
                                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_ThemThanhCongBanCoMuonTiepTuc"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
       
    }
}