using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Vs.Recruit
{
    public partial class frmPhieuDTDH : DevExpress.XtraEditors.XtraForm
    {
        public DataTable dt1;
        string strDuongDan = "";
        private bool flag = false;
        public frmPhieuDTDH()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, btnALL);
        }
        #region even
        private void frmPhieuDTDH_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.OSystems.SetDateEditFormat(datTuNgay);
            Commons.OSystems.SetDateEditFormat(datDNgay);
            Commons.Modules.sLoad = "";
            datTuNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Now.Month + "/" + DateTime.Now.Year));
            LoadData();
            EnabelButton(true);
        }

        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "themsua":
                        {
                            EnabelButton(false);
                            LoadData();
                            Commons.Modules.ObjSystems.AddnewRow(grvTaiLieuDTDH, true);
                            break;
                        }
                    case "xoa":
                        {
                            try
                            {
                                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanCoMuonXoaTaiLieuNayKhong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                                DataTable dt = new DataTable();
                                dt = Commons.Modules.ObjSystems.DataFocusRows((DataTable)grdTaiLieuDTDH.DataSource, grvTaiLieuDTDH);
                                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTTaiLieu" + Commons.Modules.iIDUser, dt, "");
                                string strSQL = "DECLARE @SQL NVARCHAR(500) SET @SQL = 'DELETE dbo.TAI_LIEU_DTDH FROM dbo.TAI_LIEU_DTDH T1 INNER JOIN " + "sBTTaiLieu" + Commons.Modules.iIDUser + " T2 ON T1.ID_TLDT = T2.ID_TLDT' EXEC(@SQL)";
                                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, strSQL);
                                Commons.Modules.ObjSystems.XoaTable("sBTTaiLieu" + Commons.Modules.iIDUser);
                                LoadData();
                            }
                            catch(Exception ex)
                            {
                                Commons.Modules.ObjSystems.XoaTable("sBTTaiLieu" + Commons.Modules.iIDUser);
                            }

                            break;
                        }
                    case "ghi":
                        {
                            if (flag == true) return;
                            string sBT = "sBTTaiLieu" + Commons.Modules.iIDUser;
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grdTaiLieuDTDH), "");
                            System.Data.SqlClient.SqlConnection conn;
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTAI_LIEU_DTDH", conn);
                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                            cmd.Parameters.Add("@sBT1", SqlDbType.NVarChar).Value = sBT;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();
                            LoadData();
                            EnabelButton(true);
                            break;
                        }
                    case "khongghi":
                        {
                            Commons.Modules.ObjSystems.DeleteAddRow(grvTaiLieuDTDH);
                            LoadData();
                            EnabelButton(true);
                            break;
                        }
                    case "thoat":
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region function
        private void LoadData()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTAI_LIEU_DTDH", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@dNgay1", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(datTuNgay.Text);
                cmd.Parameters.Add("@dNgay2", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(datDNgay.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdTaiLieuDTDH, grvTaiLieuDTDH, dt, true, true, false, true, true, this.Name);
                grvTaiLieuDTDH.Columns["ID_TLDT"].Visible = false;

                

                if (btnALL.Buttons[0].Properties.Visible)
                {
                    dt.Columns["TAI_LIEU"].ReadOnly = true;
                    grvTaiLieuDTDH.Columns["GHI_CHU"].OptionsColumn.AllowEdit = false;
                    grvTaiLieuDTDH.Columns["NGAY"].OptionsColumn.AllowEdit = false;
                    grvTaiLieuDTDH.Columns["TAI_LIEU"].OptionsColumn.AllowEdit = true;
                }
                else
                {
                    dt.Columns["TAI_LIEU"].ReadOnly = false;
                    grvTaiLieuDTDH.Columns["GHI_CHU"].OptionsColumn.AllowEdit = true;
                    grvTaiLieuDTDH.Columns["NGAY"].OptionsColumn.AllowEdit = true;
                    //grvTaiLieuDTDH.Columns["TAI_LIEU"].OptionsColumn.AllowEdit = true;
                }
                RepositoryItemButtonEdit btnEdit = new RepositoryItemButtonEdit();
                grvTaiLieuDTDH.Columns["TAI_LIEU"].ColumnEdit = btnEdit;
                btnEdit.ButtonClick += BtnEdit_ButtonClick;
            }
            catch (Exception ex) { }
        }
        private void BtnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (!btnALL.Buttons[0].Properties.Visible)
                {
                    ButtonEdit a = sender as ButtonEdit;
                    ofileDialog.Filter = "All Files|*.txt;*.docx;*.doc;*.pdf*.xls;*.xlsx;*.pptx;*.ppt|Text File (.txt)|*.txt|Word File (.docx ,.doc)|*.docx;*.doc|Spreadsheet (.xls ,.xlsx)|  *.xls ;*.xlsx";
                    //ofileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm|Word Documents(*.doc)|*.doc";
                    if (ofileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string sduongDan = ofileDialog.FileName.ToString().Trim();
                        if (ofileDialog.FileName.ToString().Trim() == "") return;
                        var strDuongDanTmp = Commons.Modules.ObjSystems.CapnhatTL("Tai_Lieu_DTDH", true);
                        strDuongDan = ofileDialog.FileName;
                        string[] sFile;
                        string TenFile;
                        TenFile = ofileDialog.SafeFileName.ToString();
                        sFile = System.IO.Directory.GetFiles(strDuongDanTmp);
                        if (Commons.Modules.ObjSystems.KiemFileTonTai(strDuongDanTmp + @"\" + ofileDialog.SafeFileName.ToString()) == false)
                            a.Text = strDuongDanTmp + @"\" + ofileDialog.SafeFileName.ToString();
                        else
                        {
                            TenFile = Commons.Modules.ObjSystems.STTFileCungThuMuc(strDuongDanTmp, TenFile);
                            a.Text = strDuongDanTmp + @"\" + TenFile;
                        }
                        Commons.Modules.ObjSystems.LuuDuongDan(strDuongDan, a.Text);

                    }
                }
                else
                {
                    Commons.Modules.ObjSystems.OpenHinh(grvTaiLieuDTDH.GetFocusedRowCellValue("TAI_LIEU").ToString());
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgBanKhongCoQuyenTruyCapDD"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgfrmThongBao"), MessageBoxButtons.OK);
            }
        }
        private void EnabelButton(bool visible)
        {
            btnALL.Buttons[0].Properties.Visible = visible;
            btnALL.Buttons[1].Properties.Visible = visible;
            btnALL.Buttons[2].Properties.Visible = visible;
            btnALL.Buttons[3].Properties.Visible = !visible;
            btnALL.Buttons[4].Properties.Visible = !visible;
            btnALL.Buttons[5].Properties.Visible = visible;

            datDNgay.Properties.ReadOnly = !visible;
            datTuNgay.Properties.ReadOnly = !visible;
        }
        #endregion
        private void datTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.ConvertDateTime(datTuNgay.Text);
            int t = DateTime.DaysInMonth(datTuNgay.DateTime.Year, datTuNgay.DateTime.Month);
            DateTime secondDateTime = new DateTime(datTuNgay.DateTime.Year, datTuNgay.DateTime.Month, t);
            datDNgay.EditValue = secondDateTime;
            LoadData();
            Commons.Modules.sLoad = "";
        }

        private void datDNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData();
        }

        private void grvTaiLieuDTDH_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
        }

        private void grvTaiLieuDTDH_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvTaiLieuDTDH_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvTaiLieuDTDH_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                if (Commons.Modules.sLoad == "0Load") return;

                DevExpress.XtraGrid.Views.Grid.GridView View = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                DevExpress.XtraGrid.Columns.GridColumn NGAY = View.Columns["NGAY"];

                if (View.GetRowCellValue(e.RowHandle, NGAY).ToString() == "")
                {
                    flag = true;
                    e.Valid = false;
                    View.SetColumnError(NGAY, "Ngày không được bỏ trống"); return;
                }
                flag = false;

                //CheckDuplicateKHNP(grvKHNP, (DataTable)grdKHNP.DataSource, e);
            }
            catch (Exception ex) { }
        }
    }
}
