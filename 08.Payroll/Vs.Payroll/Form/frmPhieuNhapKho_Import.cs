using System;
using DevExpress.Spreadsheet;
using DevExpress.DataAccess.Excel;
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
using System.Collections;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Data.SqlClient;

namespace VS.ERP
{
    public partial class frmPhieuNhapKho_Import : DevExpress.XtraEditors.XtraForm
    {
        string ChuoiKT = "";
        string ChuoiKTMa = "";
        GridView viewChung;
        Point ptChung;
        public DataTable dtCHON;
        private DataTable DT_HH;
        private Int64 iID_PNK;
        public frmPhieuNhapKho_Import(Int64 ID_PNK, DataTable dt_HH)
        {
            iID_PNK = ID_PNK;
            DT_HH = dt_HH;
            InitializeComponent();
        }
       


        #region Event
        private void frmPhieuNhapKho_Import_Load(object sender, EventArgs e)
        {
            LoadNN();
        }
        private void txtPath_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //string sPath = "";
            //sPath = Commons.Mod.OS.OpenFiles("All Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx|" + "All Files (*.*)|*.*");




            //if (sPath == "") return;
            //txtPath.Text = sPath;
            //try
            //{
            //    cboSheet.Properties.Items.Clear();
            //    Workbook workbook = new Workbook();

            //    string ext = System.IO.Path.GetExtension(sPath);
            //    if (ext.ToLower() == ".xlsx")
            //        workbook.LoadDocument(txtPath.Text, DocumentFormat.Xlsx);
            //    else
            //        workbook.LoadDocument(txtPath.Text, DocumentFormat.Xls);
            //    List<string> wSheet = new List<string>();
            //    for (int i = 0; i < workbook.Worksheets.Count; i++)
            //    {
            //        wSheet.Add(workbook.Worksheets[i].Name.ToString());
            //    }
            //    cboSheet.Properties.Items.AddRange(wSheet);

            //    cboSheet.EditValue = wSheet[0].ToString();
            //}
            //catch (InvalidOperationException ex)
            //{ XtraMessageBox.Show(ex.Message); }
        }
        private void cboSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            //grdChung.DataSource = null;
            //if (cboSheet.Text.Trim() == "")
            //{

            //    return;
            //}
            //try
            //{

            //    var source = new ExcelDataSource();
            //    source.FileName = txtPath.Text;
            //    var worksheetSettings = new ExcelWorksheetSettings(cboSheet.Text);
            //    source.SourceOptions = new ExcelSourceOptions(worksheetSettings);
            //    source.Fill();
            //    DataTable dtemp = ToDataTable(source);
            //    dtemp.Columns.Add("XOA", System.Type.GetType("System.Boolean"));
            //    grdChung.DataSource = dtemp;

            //    Commons.Mod.OS.MLoadXtraGrid(grdChung, grvChung, dtemp, true, true, false, true);
            //}
            //catch (Exception ex)
            //{ XtraMessageBox.Show(ex.Message); }
        }
        private void btnThucHien_Click(object sender, EventArgs e)
        {
            //Import((DataTable)grdChung.DataSource);
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void grvChung_KeyDown(object sender, KeyEventArgs e)
        {
            if (grvChung.RowCount < 1) return;

            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Bạn có chắc xóa dòng dữ liệu này ?", this.Text, MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
                //GridView view = sender as GridView;
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                //view.DeleteRow(view.FocusedRowHandle);

                if (view.SelectedRowsCount != 0)
                {
                    view.GridControl.BeginUpdate();
                    List<int> selectedLogItems = new List<int>(view.GetSelectedRows());

                    for (int i = selectedLogItems.Count - 1; i >= 0; i--)
                    {
                        view.DeleteRow(selectedLogItems[i]);
                    }
                    view.GridControl.EndUpdate();

                }
                else if (view.FocusedRowHandle != GridControl.InvalidRowHandle)
                {
                    view.DeleteRow(view.FocusedRowHandle);
                }
            }

        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult res = XtraMessageBox.Show("Bạn có chắc xóa dữ liệu?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.No) return;
            DataTable dtTmp = new DataTable();
            dtTmp = (DataTable)grdChung.DataSource;

            try
            {
                // _table.DefaultView.RowFilter = "XOA = False";
                //_table = _table.DefaultView.ToTable()

                dtTmp.AcceptChanges();
                foreach (DataRow dr in dtTmp.Rows)
                {
                    if (dr["XOA"].ToString() == "True")
                    {
                        dr.Delete();
                    }
                }

            }
            catch
            {
                XtraMessageBox.Show("Không xóa được. Bạn vui lòng kiểm tra lại dữ liệu !", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            dtTmp.AcceptChanges();
        }
        private void grvChung_DoubleClick(object sender, EventArgs e)
        {

            DXMouseEventArgs ea = e as DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(ea.Location);
            if (info.InRow || info.InRowCell)
            {
                string colCaption = info.Column == null ? "N/A" : info.Column.GetCaption();
                MessageBox.Show(string.Format("DoubleClick on row: {0}, column: {1}.", info.RowHandle, colCaption));
            }


            //if (col == -1)
            //    return;
        }
        private void grvChung_ShownEditor(object sender, EventArgs e)
        {
            viewChung = (GridView)sender;
            ptChung = viewChung.GridControl.PointToClient(Control.MousePosition);
            viewChung.ActiveEditor.DoubleClick += new EventHandler(ActiveEditor_DoubleClick);
        }
        private void ActiveEditor_DoubleClick(object sender, System.EventArgs e)
        {
            DoRowDoubleClick(viewChung, ptChung);
            grvChung.RefreshData();
        }

        #endregion

        #region Function
        public void LoadNN()
        {
            //Commons.Mod.OS.ThayDoiNN(this, dataLayoutControl1);
        }
        private DataTable ToDataTable(ExcelDataSource excelDataSource)
        {
            IList list = ((IListSource)excelDataSource).GetList();
            DevExpress.DataAccess.Native.Excel.DataView dataView = (DevExpress.DataAccess.Native.Excel.DataView)list;
            List<PropertyDescriptor> props = dataView.Columns.ToList<PropertyDescriptor>();
            DataTable table = new DataTable();
            int StopColumns = 0;
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                if (prop.Name.Trim().Length >= 6 && prop.Name.Trim().Substring(0, 6).ToLower() == "column")
                {
                    StopColumns = i;
                    break;
                }
                table.Columns.Add(prop.Name.Trim(), prop.PropertyType);
            }
            object[] values = new object[StopColumns];
            foreach (DevExpress.DataAccess.Native.Excel.ViewRow item in list)
            {
                for (int i = 0; i < StopColumns; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
        //private void Import(DataTable dtImport)
        //{
        //    int col = 0;
        //    int count = grvChung.RowCount;
        //    #region Khai Bao Bien
        //    bool BATCH_NUMBEROK = true;
        //    bool BAO_GOI_NUMBEROK = true;
        //    bool MS_HHOK = true;
        //    bool MAUOK = true;
        //    bool SIZEOK = true;
        //    bool SO_LUONGOK = true;
        //    //bool GHI_CHU_1OK = true;
        //    //bool GHI_CHU_2OK = true;

        //    string BATCH_NUMBER = "";
        //    string BAO_GOI_NUMBER = "";
        //    string MS_HH = "";
        //    string MAU = "";
        //    string SIZE = "";
        //    double SO_LUONG = 0;
        //    string GHI_CHU_1 = "";
        //    string GHI_CHU_2 = "";

        //    //string sSql = "";
        //    int errorCount = 0;
        //    #endregion

        //    DataTable dtTmp = new DataTable();
        //    #region Do du lieu tu file excel vao CSDL
        //    //tạo bảng tạm chứa dữ liệu từ lưới
        //    //tạo bảo tạm theo cấu trúc

        //    #endregion

        //    #region Status bar
        //    prbIN.Position = 0;
        //    prbIN.Properties.Step = 1;
        //    prbIN.Properties.PercentView = true;
        //    prbIN.Properties.Maximum = dtImport.Rows.Count;
        //    prbIN.Properties.Minimum = 0;
        //    #endregion
        //    foreach (DataRow dr in dtImport.Rows)
        //    {
        //        BATCH_NUMBEROK = true;
        //        BAO_GOI_NUMBEROK = true;
        //        MS_HHOK = true;
        //        MAUOK = true;
        //        SIZEOK = true;
        //        SO_LUONGOK = true;
        //        //GHI_CHU_1OK = true;
        //        //GHI_CHU_2OK = true;


        //        dr.ClearErrors();
        //        dr["XOA"] = 0;
        //        col = 0;
        //        #region BATCH_NUMBER
        //        try
        //        {
        //            BATCH_NUMBER = dr[col].ToString().Trim();
        //            if (KiemKyTu(BATCH_NUMBER, ChuoiKTMa))
        //            {
        //                dr.SetColumnError(grvChung.Columns[col].FieldName.ToString(), "Mã lô có chứa ký tự đặc biệt");
        //                BATCH_NUMBEROK = false;
        //            }
        //        }
        //        catch { }
        //        #endregion

        //        col = 1;
        //        #region BAO_GOI_NUMBER
        //        try
        //        {
        //            BAO_GOI_NUMBER = dr[col].ToString().Trim();
        //            string str = "";
        //            if (KiemKyTu(BAO_GOI_NUMBER, ChuoiKTMa))
        //            {
        //                str = "Mã bao gói có chứa ký tự đặc biệt";
        //                BAO_GOI_NUMBEROK = false;
        //            }

        //            List<SqlParameter> lPar = new List<SqlParameter>
        //            {
        //                new SqlParameter("@iLoai", 11),
        //                new SqlParameter("@ID_PNK", iID_PNK),
        //                new SqlParameter("@BAO_GOI_NUMBER", BAO_GOI_NUMBER),
        //            };

        //            if (Convert.ToInt32(VsMain.MExecuteScalar("spPhieuNhapKho", lPar)) == 1)
        //            {
        //                if (str != "")
        //                    str += "\n";
        //                str += "Mã bao gói đã tồn tại";
        //                BAO_GOI_NUMBEROK = false;
        //            }

        //            int KiemTrung = 0;
        //            for (int i = 0; i < dtImport.Rows.Count; i++)
        //            {
        //                if ((!string.IsNullOrEmpty(dr[col].ToString().Trim())) && BAO_GOI_NUMBER == (string.IsNullOrEmpty(dtImport.Rows[i]["BAO_GOI_NUMBER"].ToString().Trim()) ? "" : dtImport.Rows[i]["BAO_GOI_NUMBER"].ToString().Trim()))
        //                {
        //                    KiemTrung++;
        //                }
        //            }

        //            if (KiemTrung > 1)
        //            {
        //                if (str != "")
        //                    str += "\n";
        //                str += "Mã bao gói không được trùng";
        //                BAO_GOI_NUMBEROK = false;
        //            }

        //            dr.SetColumnError(grvChung.Columns[col].FieldName.ToString(), str);
        //        }
        //        catch { }
        //        #endregion

        //        col = 2;
        //        #region MS_HH
        //        try
        //        {
        //            MS_HH = dr[col].ToString().Trim();
        //            if (KiemKyTu(MS_HH, ChuoiKTMa))
        //            {
        //                dr.SetColumnError(grvChung.Columns[col].FieldName.ToString(), "Mã hàng hóa có chứa ký tự đặc biệt");
        //                MS_HHOK = false;
        //            }
        //        }
        //        catch { }
        //        #endregion

        //        col = 3;
        //        #region MAU
        //        try
        //        {
        //            MAU = dr[col].ToString().Trim();
        //            if (KiemKyTu(MAU, ChuoiKTMa))
        //            {
        //                dr.SetColumnError(grvChung.Columns[col].FieldName.ToString(), "Màu hàng hóa có chứa ký tự đặc biệt");
        //                MAUOK = false;
        //            }
        //        }
        //        catch { }
        //        #endregion

        //        col = 4;
        //        #region SIZE
        //        try
        //        {
        //            SIZE = dr[col].ToString().Trim();
        //            if (KiemKyTu(SIZE, ChuoiKTMa))
        //            {
        //                dr.SetColumnError(grvChung.Columns[col].FieldName.ToString(), "Size hàng hóa có chứa ký tự đặc biệt");
        //                SIZEOK = false;
        //            }
        //        }
        //        catch { }
        //        #endregion

        //        col = 5;
        //        #region SO_LUONG
        //        try
        //        {
        //            try
        //            {
        //                if (KiemDuLieuSo(dr, col, "", 0, 0, false))
        //                {
        //                    SO_LUONG = string.IsNullOrEmpty(dr[col].ToString()) ? 0 : Convert.ToDouble(dr[col]);
        //                    SO_LUONGOK = true;
        //                }
        //            }
        //            catch
        //            {
        //                dr.SetColumnError(grvChung.Columns[col].FieldName.ToString(), "Phải là số");
        //                SO_LUONGOK = false;
        //            }
        //        }
        //        catch { }
        //        #endregion


        //        col = 6;
        //        #region GHI_CHU_1
        //        try
        //        {
        //            GHI_CHU_1 = dr[col].ToString().Trim();
        //        }
        //        catch { }
        //        #endregion

        //        col = 7;
        //        #region GHI_CHU_2
        //        try
        //        {
        //            GHI_CHU_2 = dr[col].ToString().Trim();
        //        }
        //        catch { }
        //        #endregion

        //        #region MS_HH, MAU, SIZE
        //        if (DT_HH.Select("MS_HH = '" + MS_HH + "' AND MAU = '" + MAU + "' AND SIZE = '" + SIZE + "'").Length == 0)
        //        {
        //            dr.SetColumnError(grvChung.Columns[2].FieldName.ToString(), "Mã hàng hóa, màu, size không tồn tại");
        //            MS_HHOK = false;

        //            dr.SetColumnError(grvChung.Columns[3].FieldName.ToString(), "Mã hàng hóa, màu, size không tồn tại");
        //            MAUOK = false;

        //            dr.SetColumnError(grvChung.Columns[4].FieldName.ToString(), "Mã hàng hóa, màu, size không tồn tại");
        //            SIZEOK = false;
        //        }

        //        #endregion

        //        if (BATCH_NUMBEROK == true && BAO_GOI_NUMBEROK == true && MS_HHOK == true && MAUOK == true
        //            && SIZEOK == true && SO_LUONGOK == true)
        //        {
        //            dr["XOA"] = 0;
        //        }
        //        else
        //        {
        //            dr["XOA"] = 1;
        //            errorCount++;
        //        }


        //        #region prb
        //        try
        //        {
        //            prbIN.PerformStep();
        //            prbIN.Update();
        //        }
        //        catch { }
        //        #endregion
        //    }
        //    #region check success
        //    if (errorCount == 0)
        //    {
        //        DialogResult res = XtraMessageBox.Show(Commons.Mod.OS.GetLanguage(this.Name, "msgBanCoChacImport"), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);


        //        if (res == DialogResult.Yes)
        //        {
        //            DataTable dt = new DataTable();
        //            dt = ((DataTable)grdChung.DataSource).Copy();

        //            dt.Columns.Add("ID_HH", typeof(Int64));

        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                string sMS_HH = string.IsNullOrEmpty(dt.Rows[i]["MS_HH"].ToString()) ? "" : dt.Rows[i]["MS_HH"].ToString();
        //                string sMAU = string.IsNullOrEmpty(dt.Rows[i]["MAU"].ToString()) ? "" : dt.Rows[i]["MAU"].ToString();
        //                string sSIZE = string.IsNullOrEmpty(dt.Rows[i]["SIZE"].ToString()) ? "" : dt.Rows[i]["SIZE"].ToString();

        //                for (int j = 0; j < DT_HH.Rows.Count; j++)
        //                {
        //                    if ((string.IsNullOrEmpty(DT_HH.Rows[j]["MS_HH"].ToString()) ? "" : DT_HH.Rows[j]["MS_HH"].ToString()) == sMS_HH && (string.IsNullOrEmpty(DT_HH.Rows[j]["MAU"].ToString()) ? "" : DT_HH.Rows[j]["MAU"].ToString()) == sMAU && (string.IsNullOrEmpty(DT_HH.Rows[j]["SIZE"].ToString()) ? "" : DT_HH.Rows[j]["SIZE"].ToString()) == sSIZE)
        //                    {
        //                        dt.Rows[i]["ID_HH"] = DT_HH.Rows[j]["ID_HH"];
        //                        break;
        //                    }
        //                }
        //            }

        //            dtCHON = dt;

        //            DialogResult = DialogResult.OK;
        //        }
        //    }
        //    else
        //    {
        //        XtraMessageBox.Show("Một số dữ liệu chưa hợp lệ, bạn vui lòng kiểm tra và sửa lại trước khi import!");
        //        prbIN.Position = dtImport.Rows.Count;
        //    }
        //    prbIN.Position = dtImport.Rows.Count;
        //    #endregion
        //}
        private bool KiemtraTenBoPhan(string[] array, ref int col)
        {
            bool resulst = true;
            for (int i = array.Count() - 1; i >= 0; i--)
            {
                if (array[i].Trim() != "")
                {
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (array[j].Trim() == "")
                        {
                            col = j;
                            return false;
                        }
                        else
                        {
                            resulst = true;
                        }
                    }
                }
            }
            return resulst;

        }
        private bool KiemDuLieuSo(DataRow dr, int iCot, string sTenKTra, double GTSoSanh, double GTMacDinh, Boolean bKiemNull)
        {
            string sDLKiem;
            sDLKiem = dr[grvChung.Columns[iCot].FieldName.ToString()].ToString();
            double DLKiem;
            if (bKiemNull)
            {
                if (string.IsNullOrEmpty(sDLKiem))
                {
                    dr.SetColumnError(grvChung.Columns[iCot].FieldName.ToString(), sTenKTra + " không được để trống");
                    dr["XOA"] = 1;
                    return false;
                }
                else
                {
                    if (!double.TryParse(dr[grvChung.Columns[iCot].FieldName.ToString()].ToString(), out DLKiem))
                    {
                        dr.SetColumnError(grvChung.Columns[iCot].FieldName.ToString(), sTenKTra + " phải là số");
                        dr["XOA"] = 1;
                        return false;
                    }
                    else
                    {
                        if (GTSoSanh != -999999)
                        {
                            if (DLKiem < GTSoSanh)
                            {
                                dr.SetColumnError(grvChung.Columns[iCot].FieldName.ToString(), sTenKTra + " không được nhỏ hơn " + GTSoSanh.ToString());
                                dr["XOA"] = 1;
                                return false;
                            }

                            DLKiem = Math.Round(DLKiem, 8);
                            dr[grvChung.Columns[iCot].FieldName.ToString()] = DLKiem.ToString();

                        }
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(sDLKiem) && GTMacDinh != -999999)
                {
                    dr[grvChung.Columns[iCot].FieldName.ToString()] = GTMacDinh;
                    DLKiem = GTMacDinh;
                    sDLKiem = GTMacDinh.ToString();
                }

                if (!string.IsNullOrEmpty(sDLKiem))
                {
                    if (!double.TryParse(dr[grvChung.Columns[iCot].FieldName.ToString()].ToString(), out DLKiem))
                    {
                        dr.SetColumnError(grvChung.Columns[iCot].FieldName.ToString(), sTenKTra + " phải là số");
                        dr["XOA"] = 1;
                        return false;
                    }
                    else
                    {
                        if (GTSoSanh != -999999)
                        {
                            if (DLKiem < GTSoSanh)
                            {
                                dr.SetColumnError(grvChung.Columns[iCot].FieldName.ToString(), sTenKTra + " không được nhỏ hơn " + GTSoSanh.ToString());
                                dr["XOA"] = 1;
                                return false;
                            }

                            DLKiem = Math.Round(DLKiem, 8);
                            dr[grvChung.Columns[iCot].FieldName.ToString()] = DLKiem.ToString();
                        }

                    }
                }


            }



            return true;
        }
        private bool KiemKyTu(string strInput, string strChuoi)
        {

            if (strChuoi == "") strChuoi = ChuoiKT;

            for (int i = 0; i < strInput.Length; i++)
            {
                for (int j = 0; j < strChuoi.Length; j++)
                {
                    if (strInput[i] == strChuoi[j])
                    {
                        return true;
                    }
                }
            }
            if (strInput.Contains("//"))
            {
                return true;
            }
            return false;
        }
        private void DoRowDoubleClick(GridView view, Point pt)
        {
            //if (cboSheet.Text == "") return;
            //DataTable dtTmp = new DataTable();
            //try
            //{
            //    DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo info = view.CalcHitInfo(pt);
            //    int col = -1;
            //    col = info.Column.AbsoluteIndex;
            //    if (col == -1)
            //        return;

            //    System.Data.DataRow row = grvChung.GetDataRow(info.RowHandle);
            //    if (col == 3)
            //    {


            //        string sSql = "SELECT T2.TEN_NHH, TEN_LHH FROM dbo.LOAI_HANG_HOA T1 INNER JOIN dbo.NHOM_HANG_HOA T2 ON T2.ID_NHH = T1.ID_NHH ORDER BY T2.THU_TU,T1.THU_TU, T2.TEN_NHH,T1.TEN_LHH  ";
            //        dtTmp = new DataTable();
            //        dtTmp.Load(SqlHelper.ExecuteReader(Commons.Mod.CNStr, CommandType.Text, sSql));
            //        KiemData(dtTmp, "TEN_LHH", info.RowHandle, col, row);
            //        row.ClearErrors();
            //    }
            //    if (col == 4 || col == 8 || col == 12)
            //    {


            //        string sSql = "SELECT TEN_DVT, SO_SO_LE FROM dbo.DON_VI_TINH WHERE INACTIVE = 0 ORDER BY THU_TU  ";
            //        dtTmp = new DataTable();
            //        dtTmp.Load(SqlHelper.ExecuteReader(Commons.Mod.CNStr, CommandType.Text, sSql));
            //        KiemData(dtTmp, "TEN_DVT", info.RowHandle, col, row);
            //        row.ClearErrors();
            //    }
            //    if (col == 6 || col == 10)
            //    {


            //        string sSql = "SELECT TEN_NGAN,TEN_CTY_DAY_DU FROM dbo.DOI_TAC ORDER BY TEN_NGAN,TEN_CTY_DAY_DU  ";
            //        dtTmp = new DataTable();
            //        dtTmp.Load(SqlHelper.ExecuteReader(Commons.Mod.CNStr, CommandType.Text, sSql));
            //        KiemData(dtTmp, "TEN_NGAN", info.RowHandle, col, row);
            //        row.ClearErrors();
            //    }
            //    grvChung.RefreshData();
            //}
            //catch { }
        }
        private void KiemData(string Table, string Field, int dong, int Cot, DataRow row)
        {
            //try
            //{

            //    frmBOMView frmPopUp = new frmBOMView("-99");
            //    frmPopUp.TableSource = GetList(Table);
            //    if (frmPopUp.ShowDialog() == DialogResult.OK)
            //        row[Cot] = frmPopUp.RowSelected[Field].ToString();
            //}
            //catch { }
        }
        public DataTable GetList(string tableName)
        {
            //var con = new SqlConnection(Commons.Mod.CNStr);
            //string cmdText = "select * from " + tableName;
            //SqlCommand command = new SqlCommand(cmdText, con);
            //SqlDataAdapter da = new SqlDataAdapter(command);
            //DataSet ds = new DataSet();
            //da.Fill(ds, "ds1");
            //return ds.Tables["ds1"];
            return null;
        }
        private void KiemData(DataTable Table, string Field, int dong, int Cot, DataRow row)
        {
            //try
            //{
            //    frmBOMView frmPopUp = new frmBOMView("-99");
            //    frmPopUp.TableSource = Table;
            //    if (frmPopUp.ShowDialog() == DialogResult.OK)
            //    {

            //        grvChung.SetFocusedRowCellValue(grvChung.Columns[Cot].Name.ToString(), frmPopUp.RowSelected[Field].ToString());
            //        row[Cot] = frmPopUp.RowSelected[Field].ToString();

            //    }
            //}
            //catch { }
        }
        #endregion
    }






}