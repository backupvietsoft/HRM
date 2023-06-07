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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;

namespace Vs.TimeAttendance
{
    public partial class frmLinklBangTay : DevExpress.XtraEditors.XtraForm
    {
        public DateTime ngaylink;
        public int flag = 0;
        RepositoryItemTimeEdit repositoryItemTimeEdit1;
        public frmLinklBangTay()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, btnALL);
            repositoryItemTimeEdit1 = new RepositoryItemTimeEdit();

        }
        private void frmLinklBangTay_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";

            repositoryItemTimeEdit1.TimeEditStyle = TimeEditStyle.TouchUI;
            repositoryItemTimeEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            repositoryItemTimeEdit1.Mask.EditMask = "HH:mm:ss";

            repositoryItemTimeEdit1.NullText = "00:00:00";
            repositoryItemTimeEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            repositoryItemTimeEdit1.DisplayFormat.FormatString = "HH:mm:ss";
            repositoryItemTimeEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            repositoryItemTimeEdit1.EditFormat.FormatString = "HH:mm:ss";


            //         //ID_NHOM,TEN_NHOM
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cboNhomCC, Commons.Modules.ObjSystems.DataNhom(true), "ID_NHOM", "TEN_NHOM", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_NHOM"));
            LoadLookupCa();
            if (flag == 0)
            {
                ngaylink = DateTime.Now;
            }
 
            datNgayCC.DateTime = ngaylink;
            datNgayDen.DateTime = ngaylink;
            datNgayVe.DateTime = ngaylink;

            //dinh dang ngay gio
            Commons.OSystems.SetDateEditFormat(datNgayCC);
            Commons.OSystems.SetDateEditFormat(datNgayDen);
            Commons.OSystems.SetDateEditFormat(datNgayVe);
            Commons.OSystems.SetTimeEditFormat(timGioDen);
            Commons.OSystems.SetTimeEditFormat(timGioVe);

            Commons.Modules.ObjSystems.LoadCboDonVi(cboDV);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            Commons.Modules.ObjSystems.SetPhanQuyen(btnALL);

            LoadGridCongNhan();
            Commons.Modules.sLoad = "";
        }

        private void LoadLookupCa()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT  CA, MIN(CONVERT(VARCHAR,GIO_BD,108)) AS GIO_BD, MAX(CONVERT(VARCHAR,GIO_KT,108)) AS  GIO_KT FROM CHE_DO_LAM_VIEC WHERE ID_NHOM = " + cboNhomCC.EditValue + " AND TANG_CA = 0 GROUP BY CA ORDER BY CA"));
            Commons.Modules.ObjSystems.MLoadLookUpEditNoRemove(cboHS, dt, "CA", "CA", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "CA"));
            cboHS.Properties.Columns["GIO_BD"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "GIO_BD");
            cboHS.Properties.Columns["GIO_KT"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "GIO_KT");

        }

        private void LoadGridCongNhan()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCongLinkChamCongTay", cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, datNgayCC.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage,Commons.Modules.chamCongK));
            dt.Columns["CHON"].ReadOnly = false;
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].ReadOnly = true;
            }
            dt.Columns["GIO_DEN"].ReadOnly = false;
            dt.Columns["GIO_VE"].ReadOnly = false;

            if (grdChamCongTay.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdChamCongTay, grvChamCongTay, dt, true, false, true, true, true,this.Name);
                grvChamCongTay.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
                grvChamCongTay.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                grvChamCongTay.OptionsSelection.CheckBoxSelectorField = "CHON";
                grvChamCongTay.Columns["ID_CN"].Visible = false;
                grvChamCongTay.Columns["CHON"].Visible = false;
                grvChamCongTay.Columns["ID_NHOM"].Visible = false;
                grvChamCongTay.Columns["GIO_DEN"].ColumnEdit = this.repositoryItemTimeEdit1;
                grvChamCongTay.Columns["GIO_VE"].ColumnEdit = this.repositoryItemTimeEdit1;
            }
            else
            {
                grdChamCongTay.DataSource = dt;
            }

            try
            {
                grvChamCongTay.OptionsSelection.CheckBoxSelectorField = "CHON";
                grvChamCongTay.Columns["CHON"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            }
            catch { }
        }

        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGridCongNhan();
            Commons.Modules.sLoad = "";
        }

        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGridCongNhan();
            Commons.Modules.sLoad = "";
        }

        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGridCongNhan();
            Commons.Modules.sLoad = "";
        }
        private void cboNhomCC_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadLookupCa();
            Commons.Modules.sLoad = "";
        }

        private void cboHS_EditValueChanged(object sender, EventArgs e)
        {
            //bing đinh dữ liệu com bo vào
            try
            {

                DataRowView row = (DataRowView)cboHS.GetSelectedDataRow();
                timGioDen.EditValue = row["GIO_BD"].ToString();
                timGioVe.EditValue = row["GIO_KT"].ToString();
            }
            catch
            {

            }

        }

        private void datNgayCC_EditValueChanged(object sender, EventArgs e)
        {
            datNgayDen.DateTime = datNgayCC.DateTime;
            datNgayVe.DateTime = datNgayCC.DateTime;
            LoadGridCongNhan();
        }


        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "themgio":
                    {
                        try
                        {
                            string sBT = "BTKinkTay" + Commons.Modules.iIDUser;
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grvChamCongTay), "");
                            DataTable dt = new DataTable();
                            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spThemDuLieuQuetTheTay",
                             cboNhomCC.EditValue, cboHS.EditValue, datNgayDen.DateTime, timGioDen.EditValue, datNgayVe.DateTime, timGioVe.EditValue, sBT));
                            grdChamCongTay.DataSource = dt;
                            Commons.Modules.ObjSystems.XoaTable(sBT);
                        }
                        catch (Exception ex)
                        {
                            Commons.Modules.ObjSystems.XoaTable("BTKinkTay" + Commons.Modules.iIDUser);
                            //XtraMessageBox.Show(ex.Message.ToString());
                        }

                        break;
                    }
                case "capnhat":
                    {
                        try
                        {

                            //kiểm tra lưới có chọn không 
                            DataTable dt = Commons.Modules.ObjSystems.ConvertDatatable(grvChamCongTay);
                            if (dt.AsEnumerable().Count(x => x["CHON"].ToString().ToLower() == "true") == 0)
                            {
                                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgBanChuaChonDuLieu);
                                return;
                            }

                            string sBT = "BTKinkTay" + Commons.Modules.sId;
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grvChamCongTay), "");

                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveDuLieuQuetTheTay", datNgayCC.DateTime, sBT,Commons.Modules.chamCongK);

                            LoadGridCongNhan();
                        }
                        catch (Exception ex)
                        {
                        }
                        break;
                    }
                case "thoat":
                    {
                        this.Close();
                        break;
                    }
            }
        }

        private void grvChamCongTay_RowCountChanged(object sender, EventArgs e)
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