using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vs.Recruit.UAC.ctUngVien
{
    public partial class frmUpdateTTUV : DevExpress.XtraEditors.XtraForm
    {
        public Int64 iIDTTHD;
        public Int64 iIDTTHT;
        public frmUpdateTTUV()
        {
            InitializeComponent();
        }

        private void LoadNN()
        {
            Commons.Modules.ObjSystems.ThayDoiNN(this);
        }
        #region even
        private void frmUpdateTTUV_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            //ID_TT_HDLookUpEdit
            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_TT_HDLookUpEdit, Commons.Modules.ObjSystems.DataTinHTrangHD(false), "ID_TT_HD", "TEN_TT_HD", "TEN_TT_HD", "", true);

            //ID_TT_HTLookUpEdit
            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_TT_HTLookUpEdit, Commons.Modules.ObjSystems.DataTinHTrangHT(false), "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT", "", true);
            Commons.Modules.sLoad = "";
            LoadNN();
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
                            if (KiemTrong()) return;
                            dxValidationProvider1.Validate();

                            iIDTTHD = Convert.ToInt64(ID_TT_HDLookUpEdit.EditValue);
                            iIDTTHT = Convert.ToInt64(ID_TT_HTLookUpEdit.EditValue);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                            break;
                        }
                    case "thoat":
                        {
                            this.Close();
                            break;
                        }
                }
            }
            catch { }
        }

        #endregion

        #region function
        private bool KiemTrong()
        {
            try
            {

                if (Convert.ToInt32(ID_TT_HDLookUpEdit.EditValue) < 0)
                {
                    XtraMessageBox.Show(ItemForID_TT_HD.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                    ID_TT_HDLookUpEdit.Focus();
                    return true;
                }

                if (Convert.ToInt32(ID_TT_HTLookUpEdit.EditValue) < 0)
                {
                    XtraMessageBox.Show(ItemForID_TT_HT.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                    ID_TT_HTLookUpEdit.Focus();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }
        #endregion


    }
}
