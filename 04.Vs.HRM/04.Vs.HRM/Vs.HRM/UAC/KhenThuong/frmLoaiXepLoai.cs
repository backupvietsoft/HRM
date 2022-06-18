using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars.Docking2010;

namespace Vs.HRM
{
    public partial class frmLoaiXepLoai : DevExpress.XtraEditors.XtraForm
    {
        public frmLoaiXepLoai()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this,Root,windowsUIButton);
            
        }

        private void LoadThang(DateTime dThang)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spLoaiXepLoai", "01/01/1900",  Commons.Modules.UserName, Commons.Modules.TypeLanguage, "", "Cbo"));

            if (grdThang.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dt, false, true, true, true, true, this.Name);
                grvThang.Columns["THANG_LXL"].Visible = false;
            }
            else
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dt, false, false, true, false, false, this.Name);


            try
            {
                if (dThang == Convert.ToDateTime("01/01/1900"))
                    cboThang.Text = dt.Rows[0][0].ToString();
            }
            catch { cboThang.Text = DateTime.Now.ToString("MM/yyyy"); }
        }
        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            LoadLuoi(false, -1);
        }

        private void grvThang_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                cboThang.Text = grvThang.GetFocusedRowCellValue("T_NAME").ToString();
            }
            catch { cboThang.Text = DateTime.Now.ToString("MM/yyyy"); }
            cboThang.ClosePopup();
        }

        private void calThang_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                cboThang.Text = calThang.DateTime.Date.ToString("MM/yyyy");
            }
            catch { cboThang.Text = DateTime.Now.ToString("MM/yyyy"); }
            cboThang.ClosePopup();
        }

        private void frmLoaiXepLoai_Load(object sender, EventArgs e)
        {
            Commons.Modules.sPS = "0Load";
            LoadThang(Convert.ToDateTime("01/01/1900"));
            Commons.Modules.sPS = "";
            LoadLuoi(false, -1);
            enableButon(true);
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
        }


        private void LoadLuoi(Boolean bThemSua, int id)
        {
            if (Commons.Modules.sPS == "0Load") return;
            DataTable dt = new DataTable();
            DateTime dtThang = Convert.ToDateTime("01/01/1900");
            try
            {
                dtThang = Convert.ToDateTime("01/" + cboThang.Text.ToString());
            }
            catch { }

            if (!bThemSua)
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spLoaiXepLoai", dtThang, Commons.Modules.UserName, Commons.Modules.TypeLanguage, "", "Grd"));
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_LXL"] };

                if (grdLXL.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdLXL, grvLXL, dt, false, true, true, true, true, this.Name);
                    grvLXL.Columns["HE_SO_LXL"].Width = 50;
                }
                else
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdLXL, grvLXL, dt, false, false, true, false, false, this.Name);

                grvLXL.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
                grvLXL.OptionsView.NewItemRowPosition = NewItemRowPosition.None;

            }
            else
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spLoaiXepLoai", dtThang, Commons.Modules.UserName, Commons.Modules.TypeLanguage, "", "Add"));
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_LXL"] };
                dt.Columns["THANG_LXL"].ReadOnly = false;

                Commons.Modules.ObjSystems.MLoadXtraGrid(grdLXL, grvLXL, dt, true, true, true, true, true, this.Name);
                if (grdLXL.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdLXL, grvLXL, dt, true, true, true, true, true, this.Name);
                    grvLXL.Columns["HE_SO_LXL"].Width = 50;
                }
                else
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdLXL, grvLXL, dt, true, false, true, false, false, this.Name);

                grvLXL.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
                grvLXL.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            }
            grvLXL.Columns["ID_LXL"].Visible = false;
            grvLXL.Columns["THANG_LXL"].Visible = false;

            grvLXL.Columns["HE_SO_LXL"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            grvLXL.Columns["HE_SO_LXL"].DisplayFormat.FormatString = Commons.Modules.sSoLeDG;

            if (id != -1)
            {
                try
                {
                    int index = dt.Rows.IndexOf(dt.Rows.Find(id));
                    grvLXL.FocusedRowHandle = grvLXL.GetRowHandle(index);
                }
                catch { }
            }


        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            if (btn == null || btn.Tag == null) return;
            try
            {
                switch (btn.Tag.ToString())
                {
                    case "them":
                        {
                            enableButon(false);
                            LoadLuoi(true, -1);
                            break;

                        }
                    case "khongghi":
                        {
                            grvLXL.RefreshData();
                            enableButon(true);
                            LoadLuoi(false, -1);
                            break;
                        }
                    case "ghi":
                        {

                            grvLXL.PostEditor();
                            grvLXL.UpdateCurrentRow();
                            if(!SaveXepLoaiKhenThuong())return;
                            enableButon(true);
                            DateTime dThang = Convert.ToDateTime(cboThang.Text);
                            Commons.Modules.sPS = "0Load";
                            LoadThang(dThang);
                            int idLXL = -1;
                            try
                            {
                                idLXL = int.Parse(grvLXL.GetFocusedRowCellValue("ID_LXL").ToString());
                            }
                            catch { }
                            Commons.Modules.sPS = "";
                            LoadLuoi(false, idLXL);
                            break;
                        }
                    case "thoat":
                        {
                            this.Close();
                            break;
                        }
                    case "xoa":
                        {
                            if (DeleteData(-1))
                            {
                                Commons.Modules.sPS = "0Load";
                                LoadThang(Convert.ToDateTime("01/01/1900"));
                                LoadLuoi(false, -1);
                                Commons.Modules.sPS = "";
                            }
                            break;
                        }
                    
                    default:
                        break;
                }
            }
            catch (Exception Ex)
            {
                XtraMessageBox.Show(Ex.Message.ToLower());
                Commons.Modules.sPS = "";
            }
        }

        private void enableButon(bool visible)
        {
            try
            {
                windowsUIButton.Buttons[0].Properties.Visible = visible;
                windowsUIButton.Buttons[1].Properties.Visible = visible;
                windowsUIButton.Buttons[2].Properties.Visible = visible;                
                windowsUIButton.Buttons[3].Properties.Visible = visible;
                windowsUIButton.Buttons[5].Properties.Visible = !visible;

                windowsUIButton.Buttons[4].Properties.Visible = !visible;
                cboThang.Enabled = visible;
            }
            catch { }

        }

        private bool SaveXepLoaiKhenThuong()
        {
            try
            {
                string sBT = "LXLHRM" + Commons.Modules.UserName;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, (DataTable)grdLXL.DataSource, "");
                DateTime dtThang = Convert.ToDateTime("01/01/1900");
                try
                {
                    dtThang = Convert.ToDateTime("01/" + cboThang.Text.ToString());
                }
                catch { }

                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spLoaiXepLoai", dtThang, Commons.Modules.UserName, Commons.Modules.TypeLanguage, sBT, "Save");
                Commons.Modules.ObjSystems.XoaTable(sBT);
                return true;
            }
            catch (Exception EX)
            {
                XtraMessageBox.Show(EX.Message);
                return false;
            }
        }

        private void grvLXL_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            DateTime dtThang = Convert.ToDateTime("01/01/1900");
            try
            {
                dtThang = Convert.ToDateTime("01/" + cboThang.Text.ToString());
            }
            catch { }

            GridView view = sender as GridView;
            view.SetRowCellValue(e.RowHandle, view.Columns["THANG_LXL"], dtThang.Date);
        }

        private void grvLXL_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            grvLXL.ClearColumnErrors();
            if (windowsUIButton.Buttons[0].Properties.Visible) return;
            GridView grv = (GridView)sender;
            if (grv.GetRowCellValue(e.RowHandle, grv.Columns["TEN_LXL"]).ToString() == "")
            {
                e.Valid = false;
                grv.SetColumnError(grv.Columns["TEN_LXL"], Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "msgChuaNhapTEN_LXL", Commons.Modules.TypeLanguage)); return;
            }
            else
            {
                
                if(!KiemTrung(grv, "TEN_LXL", grv.GetRowCellValue(e.RowHandle, grv.Columns["TEN_LXL"]).ToString().ToUpper()))
                {
                    grv.SetColumnError(grv.Columns["TEN_LXL"], Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "msgTEN_LXLTrung", Commons.Modules.TypeLanguage));
                    e.Valid = false;
                }

            }
            if (grv.GetRowCellValue(e.RowHandle, grv.Columns["HE_SO_LXL"]).ToString() == "")
            {
                e.Valid = false;
                grv.SetColumnError(grv.Columns["HE_SO_LXL"], Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "msgChuaNhapHE_SO_LXL", Commons.Modules.TypeLanguage)); return;
            }
            else
            {
                float iHS = -1;
                try
                {
                    float.TryParse(grv.GetRowCellValue(e.RowHandle, grv.Columns["HE_SO_LXL"]).ToString(),out iHS) ;
                    if (iHS ==0)
                    {
                        e.Valid = false;
                        grv.SetColumnError(grv.Columns["HE_SO_LXL"], Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "msgHE_SO_LXLLonHonKhong", Commons.Modules.TypeLanguage)); return;
                    }
                }
                catch {
                    e.Valid = false;
                    grv.SetColumnError(grv.Columns["HE_SO_LXL"], Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "msgChuaNhapHE_SO_LXL", Commons.Modules.TypeLanguage)); return;
                }

            }
            if (grv.GetRowCellValue(e.RowHandle, grv.Columns["TEN_LXL_A"]).ToString() != "")
            {
                if (!KiemTrung(grv, "TEN_LXL_A", grv.GetRowCellValue(e.RowHandle, grv.Columns["TEN_LXL_A"]).ToString().ToUpper()))
                {
                    grv.SetColumnError(grv.Columns["TEN_LXL_A"], Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "msgTenLoaiXepLoaiAnhTrung", Commons.Modules.TypeLanguage));
                    e.Valid = false;
                }
            }

            if (grv.GetRowCellValue(e.RowHandle, grv.Columns["TEN_LXL_H"]).ToString() != "")
            {
                if (!KiemTrung(grv,  "TEN_LXL_H", grv.GetRowCellValue(e.RowHandle, grv.Columns["TEN_LXL_H"]).ToString().ToUpper()))
                {
                    grv.SetColumnError(grv.Columns["TEN_LXL_H"], Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "msgTenLoaiXepLoaiHoaTrung", Commons.Modules.TypeLanguage));
                    e.Valid = false;
                }
            }

        }

        private Boolean KiemTrung(GridView grv,string sColumn, string sData)
        {
            try
            {
                if (grv == null) return false;
                DataTable dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grvLXL);
                DataRow[] dr;
                dr = dtTmp.Select(sColumn + "='" +sData+"'" ,sColumn,DataViewRowState.CurrentRows);
                if (dr.Count() > 1)
                    return false;

                return true;
            }
            catch
            { return false; }
            
        }

        private void grvLXL_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grdLXL_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete) return;
            int iIdLXL = -1;
            try { iIdLXL = int.Parse(grvLXL.GetFocusedRowCellValue("ID_LXL").ToString()); } catch { }
            if (iIdLXL == -1)
            {
                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgDuLieuDangSuDung);
                return;
                
            }

            if (DeleteData(iIdLXL))
            {
                if (windowsUIButton.Buttons[3].Properties.Visible)
                {
                    var grid = sender as DevExpress.XtraGrid.GridControl;
                    var view = grid.FocusedView as GridView;
                    view.DeleteSelectedRows();
                    
                }
                else
                {
                    Commons.Modules.sPS = "0Load";
                    LoadThang(Convert.ToDateTime("01/01/1900"));
                    Commons.Modules.sPS = "";
                    LoadLuoi(false, -1);
                    Commons.Modules.sPS = "";
                }
            }

        }

        private Boolean DeleteData(int iIdLXL)
        {
            if (grvLXL.RowCount == 0)
            {   
                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
                return false;
            }

            if (iIdLXL == -1)
            {
                if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoaAll) == DialogResult.No) return false;
            }
            else
            {
                if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return false;
            }

            DateTime dThang = Convert.ToDateTime("01/01/1900");
            try
            {
                dThang = Convert.ToDateTime("01/" + cboThang.Text.ToString());
            }
            catch { }
            //xóa
            try
            {
                // Thay ngon ngu bang id loai xep loai de where
                string sKiem = "";
                sKiem = Convert.ToString( SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spLoaiXepLoai", dThang, Commons.Modules.UserName, iIdLXL, "", "Delete"));
                if (sKiem == "0")
                {
                    return true;
                }
                else
                {
                    Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgDuLieuDangSuDung);
                    return false;
                }

            }
            catch (Exception ex)
            {

                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgDuLieuDangSuDung, ex.Message.ToString());
                return false;
            }
        }
    }
}