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
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraGrid.Views.Grid;

namespace Vs.Category
{
    public partial class frmEditMAU_BAO_CAO : DevExpress.XtraEditors.XtraForm
    {
        Boolean AddEdit = true;  // true la add false la edit

        public frmEditMAU_BAO_CAO(Int64 idmbc, bool addedit)
        {
            InitializeComponent();
        }

        RepositoryItemMemoEdit RepositoryMemoEdit1 = new RepositoryItemMemoEdit();

        #region even
        private void frmEditMAU_BAO_CAO_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                LoadCbo();
                Commons.Modules.ObjSystems.AddnewRow(grvMauBC, false);
                LoadgrdMBC();
                enable(true);
                Commons.Modules.sLoad = "";
                //Commons.Modules.iCongNhan = 161;
                //ucThuongKhacLuong ns = new ucThuongKhacLuong();
                //ucDaoTao ns = new ucDaoTao();
                //ucDanhGia ns = new ucDanhGia(161);
                //ucHopDong ns = new ucHopDong(161);
                //ucLyLich ns = new ucLyLich(161);
                //ucTaiNanLD ns = new ucTaiNanLD(24);
                //ucBHXHThang ns = new ucBHXHThang();
                //this.Controls.Clear();
                //this.Controls.Add(ns);
                //ns.Dock = DockStyle.Fill;
                Commons.Modules.ObjSystems.ThayDoiNN(this, Root, windowsUIButton);
            }
            catch { }
        }
        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();

            switch (btn.Tag.ToString())
            {
                case "themsua":
                    {
                        //var word = new Microsoft.Office.Interop.Word.Application();
                        //word.WindowState = WdWindowState.wdWindowStateNormal;
                        //Microsoft.Office.Interop.Word.Document doc = word.Documents.Add();
                        //Microsoft.Office.Interop.Word.Paragraph paragraph = doc.Paragraphs.Add();
                        //paragraph.Range.Text = richEditControl1.Text;
                        //doc.SaveAs2(@"D:\test1.docx");
                        //Process.Start(@"D:\test1.docx");
                        //Commons.Modules.ObjSystems.AddnewRow(grvMauBC, true);

                        enable(false);
                        break;
                    }
                case "luu":
                    {
                        grdMauBC.MainView.CloseEditor();
                        grvMauBC.UpdateCurrentRow();
                        Savedata();
                        Commons.Modules.ObjSystems.AddnewRow(grvMauBC, false);
                        enable(true);
                        LoadgrdMBC();
                        //var word = new Microsoft.Office.Interop.Word.Application();
                        //object miss = System.Reflection.Missing.Value;
                        //object path = @"D:\testCshap.docx";
                        //object readOnly = true;
                        //object missing = System.Type.Missing;
                        //Document doc = word.Documents.Open(ref path,
                        //        ref miss, ref miss, ref miss, ref miss,
                        //        ref miss, ref miss, ref miss, ref miss,
                        //        ref miss, ref miss, ref miss, ref miss,
                        //        ref miss, ref miss, ref miss);
                        //for (int i = 0; i < doc.Paragraphs.Count; i++)
                        //{
                        //    richEditControl1.Text += "\r\n" + doc.Paragraphs[i + 1].Range.Text.ToString();
                        //}
                        break;
                    }
                case "khongluu":
                    {
                        enable(true);
                        LoadgrdMBC();
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
        #endregion

        #region function
        private void LoadCbo()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLOAI_RPT", Commons.Modules.TypeLanguage, 0, Commons.Modules.ObjSystems.DataThongTinChung(-1).Rows[0]["KY_HIEU_DV"].ToString()));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboLoai_RPT, dt, "ID_LOAI_RPT", "TEN_NGAN", "TEN_NGAN", true, false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void Savedata()
        {
            try
            {
                DataTable dt = new DataTable();
                string sBTUpDateMBC = "tabMBC" + Commons.Modules.UserName;
                //tạo một datatable 
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTUpDateMBC, Commons.Modules.ObjSystems.ConvertDatatable(grvMauBC), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveMauBaoCao", sBTUpDateMBC, Convert.ToInt64(cboLoai_RPT.EditValue));
                //string sSql = "UPDATE MAU_BAO_CAO SET ID_LOAI_RPT = " + Convert.ToInt64(cboLoai_RPT.EditValue) + ", PARAGRAP_1 = B.PARAGRAP_1 FROM MAU_BAO_CAO A INNER JOIN " + stbUpDateMBC + " B ON A.ID_MBC = B.ID_MBC AND A.STT_MAU = B.STT_MAU WHERE ISNULL(B.ID_MBC, -99) IN (SELECT ID_MBC FROM dbo.MAU_BAO_CAO)    INSERT INTO dbo.MAU_BAO_CAO(STT_MAU,PARAGRAP_1,ID_LOAI_RPT) SELECT STT_MAU, PARAGRAP_1, "+ Convert.ToInt64(cboLoai_RPT.EditValue) +" FROM "  + stbUpDateMBC + " B WHERE ISNULL(B.ID_MBC, -99) NOT IN(SELECT ISNULL(ID_MBC,-98) FROM dbo.MAU_BAO_CAO)";
                //SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                Commons.Modules.ObjSystems.XoaTable(sBTUpDateMBC);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void enable(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = !visible;
            windowsUIButton.Buttons[2].Properties.Visible = !visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;

            grvMauBC.OptionsBehavior.Editable = !visible;
        }

        private void LoadgrdMBC()
        {
            try
            {

                DataTable dt = new DataTable();
                DataTable dtTTC = new DataTable();
                dtTTC = Commons.Modules.ObjSystems.DataThongTinChung(-1);
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetMauBaoCao", Commons.Modules.TypeLanguage, Convert.ToInt64(cboLoai_RPT.EditValue), dtTTC.Rows[0]["KY_HIEU_DV"].ToString()));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdMauBC, grvMauBC, dt, false, false, false, false, true, this.Name);

                grvMauBC.Columns["ID_MBC"].Visible = false;
                grvMauBC.Columns["ID_LOAI_RPT"].Visible = false;
                grvMauBC.Columns["TEN_NGAN"].OptionsColumn.AllowEdit = false;
                grvMauBC.Columns["STT_MAU"].OptionsColumn.AllowEdit = false;
                RepositoryMemoEdit1.WordWrap = true;
                grvMauBC.Columns["PARAGRAP_1"].ColumnEdit = RepositoryMemoEdit1;
            }
            catch { }
        }

        #endregion

        private void cboLoai_RPT_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadgrdMBC();
        }

        private void grvMauBC_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)grdMauBC.DataSource;
                //dt.Rows[dt.Rows.Count + 1]["STT_MAU"] = Convert.ToInt32(dt.Rows[dt.Rows.Count -1]["STT_MAU"]);
                view.SetFocusedRowCellValue("STT_MAU", Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["STT_MAU"]) + 1);
            }
            catch
            {

            }
        }
    }
}