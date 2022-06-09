using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Vs.Payroll
{
    public partial class ucNangSuatChuyen : DevExpress.XtraEditors.XtraUserControl
    {
        private static bool isAdd = false;

        public static ucLayChamCong _instance;
        private bool thangtruoc;

        public static ucLayChamCong Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucLayChamCong();
                return _instance;
            }
        }

        public ucNangSuatChuyen()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);

        }

        #region even
        private void ucNangSuatChuyen_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                LoadThang();
                Commons.Modules.ObjSystems.LoadCboDonVi(cboDonVi);
                Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
                LoadGrdNangSuatChuyen();



                //cboCV.EditValueChanged += CboCa_EditValueChanged;
                enabledButton(true);
                Commons.Modules.sLoad = "";
            }
            catch { }
        }

        private void cboCa_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                Int64 id_cv = Convert.ToInt64(grvData.GetFocusedRowCellValue("ID_TO"));
                if (sender is LookUpEdit cbo)
                {
                    try
                    {
                        DataTable DataCombo = (DataTable)cbo.Properties.DataSource;
                        DataTable DataLuoi = Commons.Modules.ObjSystems.ConvertDatatable(grdData);
                        var DataNewCombo = DataCombo.AsEnumerable().Where(r => !DataLuoi.AsEnumerable()
                        .Any(r2 => r["ID_TO"].ToString().Trim() == r2["ID_TO"].ToString().Trim())).CopyToDataTable();
                        cbo.Properties.DataSource = null;
                        cbo.Properties.DataSource = DataNewCombo;
                    }
                    catch
                    {
                        cbo.Properties.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdNangSuatChuyen();
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }

        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            LoadGrdNangSuatChuyen();
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }

        private void cboXiNghiep_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdNangSuatChuyen();
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }
        private void btnALL_ButtonClick(object sender, ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "themsua":
                        {
                            enabledButton(false);
                            Commons.Modules.ObjSystems.AddnewRow(grvData, true);
                            break;
                        }

                    case "xoa":
                        {
                            DeleteData();
                            break;
                        }

                    case "ghi":
                        {
                            grdData.MainView.CloseEditor();
                            grvData.UpdateCurrentRow();
                            string sBT_grvNangSuatChuyen = "sBT_grvNangSuatChuyen" + Commons.Modules.UserName;
                            //DateTime ngay = Convert.ToDateTime(cboNgay.EditValue);
                            //DateTime ngay = DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures);
                            //dtNgay = DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures);
                            try
                            {

                                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT_grvNangSuatChuyen, (DataTable)grdData.DataSource, "");

                                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveNSChuyenThang", sBT_grvNangSuatChuyen, Convert.ToDateTime(cboThang.EditValue).ToString("yyyyMMdd"));
                                Commons.Modules.ObjSystems.XoaTable(sBT_grvNangSuatChuyen);
                            }
                            catch (Exception ex) { }

                            LoadGrdNangSuatChuyen();
                            Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                            enabledButton(true);
                            break;
                        }

                    case "khongghi":
                        {
                            enabledButton(true);
                            LoadGrdNangSuatChuyen();
                            Commons.Modules.ObjSystems.DeleteAddRow(grvData);
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
            catch (Exception EX)
            {
                XtraMessageBox.Show(EX.Message.ToString());
            }
        }
        private void calThang_DateTimeCommit(object sender, EventArgs e)
        {
           
        }

        private void grvThang1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboThang.Text = grvThang1.GetFocusedRowCellValue("THANG").ToString();
            }
            catch { LoadNull(); }
            cboThang.ClosePopup();
        }
        #endregion

        #region function
        private void enabledButton(bool visible)
        {
            btnALL.Buttons[0].Properties.Visible = visible;
            btnALL.Buttons[1].Properties.Visible = visible;
            btnALL.Buttons[2].Properties.Visible = visible;
            btnALL.Buttons[3].Properties.Visible = !visible;
            btnALL.Buttons[4].Properties.Visible = !visible;
            btnALL.Buttons[5].Properties.Visible = visible;

            grvData.OptionsBehavior.Editable = !visible;

            cboThang.Properties.ReadOnly = visible;
            cboDonVi.Properties.ReadOnly = visible;
            cboXiNghiep.Properties.ReadOnly = visible;
        }

        public void LoadThang()
        {
            try
            {
                //ItemForDateThang.Visibility = LayoutVisibility.Never;
                DataTable dtthang = new DataTable();
                string sSql = "SELECT disTINCT SUBSTRING(CONVERT(VARCHAR(10),THANG,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),THANG,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM dbo.NANG_SUAT_CHUYEN_THANG ORDER BY Y DESC , M DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang1, dtthang, false, true, true, true, true, this.Name);
                grvThang1.Columns["M"].Visible = false;
                grvThang1.Columns["Y"].Visible = false;

                cboThang.Text = grvThang1.GetFocusedRowCellValue("THANG").ToString();
            }
            catch (Exception ex)
            {
                cboThang.Text = DateTime.Now.Month + "/" + DateTime.Now.Year;
            }
        }

        private void LoadGrdNangSuatChuyen()
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spNangSuatChuyenThang", Commons.Modules.UserName, Commons.Modules.TypeLanguage, Convert.ToDateTime(cboThang.EditValue), Convert.ToInt64(cboXiNghiep.EditValue)));

                dt.Columns["ID_TO"].ReadOnly = false;
                dt.Columns["NANG_SUAT"].ReadOnly = false;
                if (grdData.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, true, true, false, true, true, this.Name);
                    //grvData.Columns["TEN_TO"].Visible = false;
                }
                else
                {
                    grdData.DataSource = dt;
                }

                //DataTable dID_TO = new DataTable();
                //dID_TO.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTO", cboDonVi.EditValue, cboXiNghiep.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
                //Commons.Modules.ObjSystems.AddCombXtra("ID_CTL", "TEN", grvData, dID_TO, false, "ID_CTL", "CACH_TINH_LUONG");
                //grvCachTinhLuong.Columns["THANG"].Visible = false;
                //grvCachTinhLuong.Columns["ID_CN"].Visible = false;
                //grvCachTinhLuong.Columns["ID_CTL"].Width = 250;
                //grvCachTinhLuong.Columns["ID_CV"].Width = 250;

                RepositoryItemLookUpEdit cboTo = new RepositoryItemLookUpEdit();

                cboTo.NullText = "";
                cboTo.ValueMember = "ID_TO";
                cboTo.DisplayMember = "TEN_TO";
                cboTo.DataSource = Commons.Modules.ObjSystems.DataTo(Convert.ToInt32(cboDonVi.EditValue), Convert.ToInt32(cboXiNghiep.EditValue), false);
                cboTo.Columns.Clear();

                cboTo.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_TO"));
                cboTo.Columns["ID_TO"].Visible = false;

                cboTo.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_TO"));
                cboTo.Columns["TEN_TO"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_TO");

                cboTo.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboTo.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                grvData.Columns["ID_TO"].ColumnEdit = cboTo;
                cboTo.BeforePopup += cboCa_BeforePopup;
                cboTo.EditValueChanged += CboTo_EditValueChanged;

                cboTo.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.SingleClick;
            }
            catch
            {

            }
        }
        private void CboTo_EditValueChanged(object sender, EventArgs e)
        {
            //LookUpEdit lookUp = sender as LookUpEdit;
            //DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            //try
            //{
            //    grvData.SetFocusedRowCellValue("TEN_TO", dataRow.Row["TEN_TO"]);
            //}
            //catch
            //{

            //}
        }

        /// <summary>
        /// load null cboNgay
        /// </summary>
        private void LoadNull()
        {
            try
            {
                if (cboThang.Text == "") cboThang.Text = DateTime.Now.ToShortDateString();
            }
            catch (Exception ex)
            {
                cboThang.Text = "";
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }






        #endregion

        private void grvData_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvData_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvData_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {

        }

        private void grvData_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteData();
                grvData.DeleteSelectedRows();
            }
        }

        private void DeleteData()
        {
            try
            {
                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Xoa"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    String sSql = " DELETE FROM dbo.NANG_SUAT_CHUYEN_THANG WHERE ID_TO = " + grvData.GetFocusedRowCellValue("ID_TO").ToString() + " AND   THANG = convert(varchar, " + Convert.ToDateTime(cboThang.EditValue).ToString("yyyyMMdd") + " ,111) ";

                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                }
                else
                    return;
            }

            catch
            {

            }
        }

        private void calThang_DateTimeCommit_1(object sender, EventArgs e)
        {
            try
            {
                cboThang.Text = calThang.DateTime.ToString("MM/yyyy");
                DataTable dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grdThang);
                DataRow[] dr;
                dr = dtTmp.Select("NGAY_TTXL" + "='" + cboThang.Text + "'", "NGAY_TTXL", DataViewRowState.CurrentRows);
                if (dr.Count() == 1)
                {
                }
                else { }
            }
            catch (Exception ex)
            {
                cboThang.Text = calThang.DateTime.ToString("MM/yyyy");
            }
            cboThang.ClosePopup();
        }
    }
}