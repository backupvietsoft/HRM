using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraLayout;
using System.Threading;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraGrid.Views.Grid;

namespace Vs.TimeAttendance
{
    public partial class ucMaTheChamCong : DevExpress.XtraEditors.XtraUserControl
    {
        private static bool isAdd = false;
        public static ucMaTheChamCong _instance;
        public static ucMaTheChamCong Instance
        {   
            get
            {
                if (_instance == null)
                    _instance = new ucMaTheChamCong();
                return _instance;
            }
        }


        public ucMaTheChamCong()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this,new List<LayoutControlGroup>{ Root}, windowsUIButton);
        }
        #region Mã thẻ chấm công
        private void ucMaTheChamCong_Load(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDV);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            DateTime nht = DateTime.Now;
            dNgayXem.EditValue = DateTime.Now;
            Commons.OSystems.SetDateEditFormat(dNgayXem);
            LoadGridMaTheChamCong();
            Commons.Modules.sPS = "";
            enableButon();
        }
        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGridMaTheChamCong();
            Commons.Modules.sPS = "";
        }
        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGridMaTheChamCong();
            Commons.Modules.sPS = "";
        }
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadGridMaTheChamCong();
            Commons.Modules.sPS = "";
        }
        private void dNgayXem_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadGridMaTheChamCong();
            Commons.Modules.sPS = "";
        }
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "themsua":
                    {
                        if (grvMTCC.RowCount == 0)
                        {
                            //Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieu);
                            return;
                        }
                        grvMTCC.OptionsBehavior.Editable = true;
                        isAdd = true;
                        enableButon();
                        LockControl(false);
                        Commons.Modules.ObjSystems.AddnewRow(grvMTCC, false);
                        break;
                    }
                case "luu":
                    {
                        Validate();
                        if (grvMTCC.HasColumnErrors) return;
                        Savedata();
                        isAdd = false;
                        grvMTCC.OptionsBehavior.Editable = false;
                        enableButon();
                        LockControl(true);
                        LoadGridMaTheChamCong();
                        isAdd = false;
                        break;
                    }
                case "khongluu":
                    {
                        grvMTCC.OptionsBehavior.Editable = true;
                        isAdd = false;
                        enableButon();
                        LockControl(true);
                        LoadGridMaTheChamCong();
                        break;
                    }
                case "thoat":
                    {
                        isAdd = false;
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
                default:
                    break;
            }
        }
        #endregion

        #region hàm xử lý dữ liệu
        private void LoadGridMaTheChamCong()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListMTCC", dNgayXem.EditValue, cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            if (isAdd)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdMTCC, grvMTCC, dt, true, false, true, true, true, this.Name);
            }
            else
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdMTCC, grvMTCC, dt, true, false, true, true, true, this.Name);
            }
            grvMTCC.OptionsBehavior.Editable = false;
            grvMTCC.Columns["ID_CN"].OptionsColumn.ReadOnly = true;
            grvMTCC.Columns["ID_CN"].Visible = false;
            grvMTCC.Columns["MS_CN"].OptionsColumn.ReadOnly = true;
            grvMTCC.Columns["HO_TEN"].OptionsColumn.ReadOnly = true;
            grvMTCC.Columns["NGAY_VAO_CTY"].OptionsColumn.ReadOnly = true;
            grvMTCC.Columns["MS_CN"].Width = 100;
            grvMTCC.Columns["HO_TEN"].Width = 300;
            grvMTCC.Columns["NGAY_VAO_CTY"].Width = 100;
        }
        private void Savedata()
        {
            try
            {
                string stbUpDateMTCC = "tabMTCC" + Commons.Modules.UserName;
                //tạo một datatable 
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbUpDateMTCC, Commons.Modules.ObjSystems.ConvertDatatable(grvMTCC), "");
                string sSql = "UPDATE CONG_NHAN SET CONG_NHAN.MS_THE_CC = B.MS_THE_CC FROM CONG_NHAN A INNER JOIN "+ stbUpDateMTCC + " B ON A.ID_CN = B.ID_CN" + "";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                Commons.Modules.ObjSystems.XoaTable(stbUpDateMTCC);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void enableButon()
        {
            windowsUIButton.Buttons[0].Properties.Visible = !isAdd;
            windowsUIButton.Buttons[1].Properties.Visible = !isAdd;
            windowsUIButton.Buttons[2].Properties.Visible = isAdd;
            windowsUIButton.Buttons[3].Properties.Visible = isAdd;
        }
        private void LockControl(Boolean oLock)
        {
            try
            {
                cboDV.Enabled = oLock;
                cboXN.Enabled = oLock;
                cboTo.Enabled = oLock;
                dNgayXem.Enabled = oLock;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        #endregion

        private void grvMTCC_RowCountChanged(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            try
            {
                int index = ItemForSumNhanVien.Text.IndexOf(':');
                if (index > 0)
                {
                    if (view.RowCount > 0)
                    {
                        ItemForSumNhanVien.Text = ItemForSumNhanVien.Text.Substring(0, index) + ": " + view.RowCount.ToString();
                    }
                    else
                    {
                        ItemForSumNhanVien.Text = ItemForSumNhanVien.Text.Substring(0, index) + ": 0";
                    }

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
