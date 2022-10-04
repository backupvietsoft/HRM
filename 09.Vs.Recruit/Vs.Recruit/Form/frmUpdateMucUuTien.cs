using System;
using System.Data;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Vs.Recruit
{
    public partial class frmUpdateMucUuTien : DevExpress.XtraEditors.XtraForm
    {
        public ArrayList listChon;
        public frmUpdateMucUuTien()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root);
        }
        private void frmUpdateMucUuTien_Load(object sender, EventArgs e)
        {
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cboMucUT, Commons.Modules.ObjSystems.DataMucUuTienTD(false), "ID_MUT", "TEN_MUT", "TEN_MUT");
        }
        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "luu":
                    {
                        try
                        {
                            for (int i = 0; i < listChon.Count; i++)
                            {
                                Int64 ID_YCTD = Convert.ToInt64(((System.Data.DataRow)listChon[i]).ItemArray[0]);
                                Int64 ID_VTTD = Convert.ToInt64(((System.Data.DataRow)listChon[i]).ItemArray[1]);

                                SqlHelper.ExecuteScalar(Commons.IConnections.CNStr,CommandType.Text, "UPDATE dbo.YCTD_VI_TRI_TUYEN SET ID_MUT = "+cboMucUT.EditValue+" WHERE ID_YCTD = "+ ID_YCTD + " AND ID_VTTD = "+ ID_VTTD +"");
                            }
                            this.Close();
                        }
                        catch { }
                        break;
                    }
                case "thoat":
                    {
                        this.Close();
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
