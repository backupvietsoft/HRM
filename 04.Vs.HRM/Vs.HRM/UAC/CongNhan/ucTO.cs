using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraLayout;
using System.Threading;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace Vs.HRM
{
    public partial class ucTO : DevExpress.XtraEditors.XtraUserControl
    {
        public static ucTO _instance;
        DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboID_BV;
        int MS_TINH;
        public static ucTO Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucTO();
                return _instance;
            }
        }


        public ucTO()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
        }
        #region bảo hiểm y tế
        private void ucTO_Load(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            LoadGridTO();
        }
       
        #endregion

        #region hàm xử lý dữ liệu
        private void LoadGridTO()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListTO", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            if (grdTO.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdTO, grvTO, dt, false, false, false, false, true, this.Name);
            }
            else
            {
                grdTO.DataSource = dt;
            }
        }
       
        #endregion
    }
}
