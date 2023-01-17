using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Report;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using System.Drawing;
using System.Reflection;
using System.Linq;
using Microsoft.ApplicationBlocks.Data;

namespace Vs.Payroll
{
    public partial class frmCapNhatNhanhPCD : DevExpress.XtraEditors.XtraForm
    {
        public DataTable dtTemp;
        public int iLoai = -1; // 0 công nhân công đoạn , 1 công đoạn công nhân
        public int isoLuong = 0;
        public frmCapNhatNhanhPCD()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, windowsUIButton);
        }

        //sự kiên load form
        private void frmCapNhatNhanhPCD_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "luu":
                    {
                        dtTemp= new DataTable();
                        dtTemp = Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdData, grvData);
                        this.DialogResult= DialogResult.OK; 
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

        private void LoadData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dtTemp.Copy();
                dt.Columns.Add("CHON", System.Type.GetType("System.Boolean"));
                dt.Columns["CHON"].ReadOnly = false;
                dt.Columns.Add("SO_LUONG", System.Type.GetType("System.Int32"));
                dt.Columns["SO_LUONG"].ReadOnly = false;
                dt.AsEnumerable().ToList<DataRow>().ForEach(r => r["SO_LUONG"] = isoLuong);
                dt.AcceptChanges();
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, true, true, false, true, true, this.Name);
                grvData.Columns["CHON"].Visible = false;
                grvData.OptionsSelection.MultiSelect = true;
                grvData.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                if (iLoai == 1)
                {
                    grvData.Columns["ID_CN"].Visible= false;
                    grvData.Columns["MS_CN"].Visible= false;
                }
                if (iLoai == 2)
                {
                    grvData.Columns["ID_CD"].Visible = false;
                    grvData.Columns["DON_GIA"].OptionsColumn.AllowEdit = false;
                    grvData.Columns["TEN_CD"].OptionsColumn.AllowEdit = false;
                    grvData.Columns["MaQL"].OptionsColumn.AllowEdit = false;
                }
                try
                {
                    grvData.OptionsSelection.CheckBoxSelectorField = "CHON";
                    grvData.Columns["CHON"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                }
                catch { }
            }
            catch { }
        }
    }
}