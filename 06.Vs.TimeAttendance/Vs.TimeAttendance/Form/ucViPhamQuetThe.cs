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
using DevExpress.XtraEditors.Repository;

namespace Vs.TimeAttendance
{
    public partial class ucViPhamQuetThe : DevExpress.XtraEditors.XtraUserControl
    {
        private bool isAdd = false;
        public static ucViPhamQuetThe _instance;
        public static ucViPhamQuetThe Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucViPhamQuetThe();
                return _instance;
            }
        }


        public ucViPhamQuetThe()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this,new List<LayoutControlGroup>{ Root}, windowsUIButton);
        }
        #region Vi phạm quẹt thẻ

        RepositoryItemTimeEdit repositoryItemTimeEdit1;
        private void ucViPhamQuetThe_Load(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            Commons.Modules.sPS = "0Load";

            repositoryItemTimeEdit1 = new RepositoryItemTimeEdit();
            repositoryItemTimeEdit1.TimeEditStyle = TimeEditStyle.TouchUI;
            repositoryItemTimeEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            repositoryItemTimeEdit1.Mask.EditMask = "HH:mm";

            repositoryItemTimeEdit1.NullText = "00:00";
            repositoryItemTimeEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            repositoryItemTimeEdit1.DisplayFormat.FormatString = "HH:mm";
            repositoryItemTimeEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            repositoryItemTimeEdit1.EditFormat.FormatString = "HH:mm";

            Commons.Modules.ObjSystems.LoadCboDonVi(cboDV);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);

            LoadNgay();

            enableButon();
            LoadGridVPQuetThe();
            Commons.Modules.sPS = "";
        }
        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGridVPQuetThe();
            Commons.Modules.sPS = "";
        }
        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGridVPQuetThe();
            Commons.Modules.sPS = "";
        }
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadGridVPQuetThe();
            Commons.Modules.sPS = "";
        }
        private void dNgayXem_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadGridVPQuetThe();
            Commons.Modules.sPS = "";
        }

        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadGridVPQuetThe();
            Commons.Modules.sPS = "";
        }
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "capnhatgio":
                    {
                        UpdateTimekeeping(Convert.ToDateTime(cboNgay.EditValue));
                        LoadGridVPQuetThe();
                        break;
                    }
                case "in":
                    {
                        break;
                    }
                case "themsua":
                    {
                        isAdd = true;
                        enableButon();
                        LoadGridVPQuetThe();
                        Commons.Modules.ObjSystems.AddnewRow(grvVPQuetThe, false);
                        break;
                    }
                case "xoa":
                    {
                        if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteNVVPQT"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No) return;
                        //enableButon();
                        XoaData();
                        LoadGridVPQuetThe();
                        break;
                    }
                case "luu":
                    {
                        Validate();
                        if (grvVPQuetThe.HasColumnErrors) return;
                        Savedata();
                        isAdd = false;
                        enableButon();
                        LoadGridVPQuetThe();
                        break;
                    }
                case "khongluu":
                    {
                        isAdd = false;
                        enableButon();
                        LoadGridVPQuetThe();
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
                default:
                    break;
            }
        }
        #endregion

        private void UpdateTimekeeping(DateTime dDate)
        {
            //DataTable dt = new DataTable();
            try
            {
                string stbVPQuetThe = "VPQuetThe" + Commons.Modules.UserName;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbVPQuetThe, Commons.Modules.ObjSystems.ConvertDatatable(grvVPQuetThe), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spUpdateViPhamQuetThe", Convert.ToDateTime(cboNgay.EditValue), stbVPQuetThe);
                Commons.Modules.ObjSystems.XoaTable(stbVPQuetThe);
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_CapNhatThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception EX)
            {
                XtraMessageBox.Show(EX.Message.ToString());
            }
        }

        private void XoaData()
        {
            string sSql = "";
            try
            {
                //xóa 1 dòng
                sSql = "DELETE DSCN_VP_QUET_THE WHERE CONVERT(NVARCHAR,NGAY,112) = '" + Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd")
                      + "' AND ID_CN  = " + grvVPQuetThe.GetFocusedRowCellValue("ID_CN") + "";

                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);


                //xóa nhiều dòng
                //string XoaData = "Xoa_DSCN_VP_QUET_THE" + Commons.Modules.UserName;
                //Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, XoaData, Commons.Modules.ObjSystems.ConvertDatatable(grvVPQuetThe), "");
                //string sSql = "DELETE DSCN_VP_QUET_THE WHERE CONVERT(NVARCHAR,NGAY,112) = '" + Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd") 
                //             + "' AND ID_CN IN (SELECT ID_CN FROM "+ XoaData +")";
                //SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                //Commons.Modules.ObjSystems.XoaTable(XoaData);
            }
            catch
            {

            }
        }

        #region hàm xử lý dữ liệu
        private void LoadGridVPQuetThe()
        {
            try
            {
                DataTable dt = new DataTable();
                if (isAdd)
                {
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetEditVPQuetThe", cboNgay.EditValue, cboDV.EditValue,
                                cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdVPQuetThe, grvVPQuetThe, dt, true, false, true, true, true, this.Name);
                    dt.Columns["VP_GD"].ReadOnly = false;
                    dt.Columns["VP_GV"].ReadOnly = false;
                    dt.Columns["GIO_DEN"].ReadOnly = false;
                    dt.Columns["GIO_VE"].ReadOnly = false;
                    //grvVPQuetThe.Columns["GIO_DEN"].OptionsColumn.ReadOnly = true;
                    //grvVPQuetThe.Columns["GIO_VE"].OptionsColumn.ReadOnly = true;
                }
                else
                {
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListViPhamQuetThe", cboNgay.EditValue, cboDV.EditValue, 
                                                    cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdVPQuetThe, grvVPQuetThe, dt, false, false, true, true, true, this.Name);
                }
            }
            catch
            {

            }
            grvVPQuetThe.Columns["ID_CN"].OptionsColumn.ReadOnly = true;
            grvVPQuetThe.Columns["MS_CN"].OptionsColumn.ReadOnly = true;
            grvVPQuetThe.Columns["HO_TEN"].OptionsColumn.ReadOnly = true;
            grvVPQuetThe.Columns["ID_CN"].Visible = false;

            grvVPQuetThe.Columns["GIO_DEN"].ColumnEdit = repositoryItemTimeEdit1;
            grvVPQuetThe.Columns["GIO_VE"].ColumnEdit = repositoryItemTimeEdit1;
        }

        private void Savedata()
        {
            string stbVPQT = "stbVPQT" + Commons.Modules.UserName;
            try
            {
                var test = grvVPQuetThe.RowCount;
                //tạo một datatable 
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbVPQT, Commons.Modules.ObjSystems.ConvertDatatable(grvVPQuetThe), "");
                string sSql = "DELETE DSCN_VP_QUET_THE WHERE  CONVERT(NVARCHAR,NGAY,112) = '" + Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd") 
                             + "' AND ID_CN IN (SELECT ID_CN FROM " + stbVPQT + ")"
                             + " INSERT INTO DSCN_VP_QUET_THE (NGAY, ID_CN, VP_GV, VP_GD, GIO_DEN, GIO_VE) SELECT '" 
                             + Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd") + "' AS NGAY, ID_CN,VP_GV, VP_GD, CASE WHEN VP_GD = 1 THEN GIO_DEN" +
                             " ELSE '' END AS GIO_DEN,CASE WHEN VP_GV = 1 THEN GIO_VE ELSE '' END AS GIO_VE "
                             + " FROM "+ stbVPQT + " B WHERE VP_GV = 1 OR VP_GD = 1";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                Commons.Modules.ObjSystems.XoaTable(stbVPQT);
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
            windowsUIButton.Buttons[2].Properties.Visible = !isAdd;
            windowsUIButton.Buttons[3].Properties.Visible = !isAdd;
            windowsUIButton.Buttons[4].Properties.Visible = isAdd;
            windowsUIButton.Buttons[5].Properties.Visible = isAdd;
            windowsUIButton.Buttons[6].Properties.Visible = isAdd;
            windowsUIButton.Buttons[7].Properties.Visible = isAdd;
        }
        #endregion

        private void grvDSDKLD_RowCountChanged(object sender, EventArgs e)
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

        private void calThang_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                cboNgay.Text = calThang.DateTime.ToString("dd/MM/yyyy");
                DataTable dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grdThang);
            }
            catch
            {
                cboNgay.Text = calThang.DateTime.ToString("dd/MM/yyyy");
            }
            cboNgay.ClosePopup();
        }

        private void LoadNgay()
        {
            try
            {
                DataTable dtNgay = new DataTable();
                string sSql = "SELECT DISTINCT  NGAY FROM dbo.DSCN_VP_QUET_THE ORDER BY NGAY DESC";
                dtNgay.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtNgay, false, true, true, true, true, this.Name);

                if(dtNgay.Rows.Count > 0)
                {
                    cboNgay.EditValue = dtNgay.Rows[0][0];
                }
                else
                {
                    cboNgay.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
            catch
            {
            }
        }

        private void grvThang_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                cboNgay.Text = Convert.ToDateTime(grvThang.GetFocusedRowCellValue("NGAY").ToString()).ToShortDateString();
            }
            catch { }
            cboNgay.ClosePopup();
        }

        private void grvVPQuetThe_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            if(e.Column.FieldName == "VP_GD")
            {
                if(Convert.ToInt32(e.Value) == 1)  //có check
                {
                    grvVPQuetThe.Columns["GIO_DEN"].OptionsColumn.ReadOnly = false;
                }
                else //không check
                {
                    //grvVPQuetThe.SetFocusedRowCellValue("GIO_DEN", "");
                    grvVPQuetThe.Columns["GIO_DEN"].OptionsColumn.ReadOnly = true;
                }
            }
            else if (e.Column.FieldName == "VP_GV")
            {
                if (Convert.ToInt32(e.Value) == 1)  //có check
                {
                    grvVPQuetThe.Columns["GIO_VE"].OptionsColumn.ReadOnly = false;
                }
                else //không check
                {
                    //grvVPQuetThe.SetFocusedRowCellValue("GIO_VE", "");
                    grvVPQuetThe.Columns["GIO_VE"].OptionsColumn.ReadOnly = true;
                }
            }
        }

        private void grvVPQuetThe_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            //GridView view = sender as GridView;

            DevExpress.XtraGrid.Views.Grid.GridView View = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            DevExpress.XtraGrid.Columns.GridColumn gioDen = View.Columns["GIO_DEN"];
            DevExpress.XtraGrid.Columns.GridColumn gioVe = View.Columns["GIO_VE"];

            if (Convert.ToInt32(grvVPQuetThe.GetFocusedRowCellValue("VP_GD")) == 1)
            {
                if (Convert.ToDateTime(grvVPQuetThe.GetFocusedRowCellValue("GIO_DEN")).ToString("HHmm") == "0000")
                {
                    e.Valid = false;
                    View.SetColumnError(gioDen, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, 
                                        this.Name, "MsgGioDenIsRequired", Commons.Modules.TypeLanguage)); return;
                }
            }
            if (Convert.ToInt32(grvVPQuetThe.GetFocusedRowCellValue("VP_GV")) == 1)
            {
                if (Convert.ToDateTime(grvVPQuetThe.GetFocusedRowCellValue("GIO_VE")).ToString("HHmm") == "0000")
                {
                    e.Valid = false;
                    View.SetColumnError(gioVe, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, 
                                        "MsgGioVeIsRequired", Commons.Modules.TypeLanguage)); return;
                }
            }
        }

        private void grvVPQuetThe_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvVPQuetThe_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

    }
}
