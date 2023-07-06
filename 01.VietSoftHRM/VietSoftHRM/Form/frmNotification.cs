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
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraLayout;
using DevExpress.CodeParser;
using DevExpress.DataProcessing.InMemoryDataProcessor;
using DevExpress.Map.Native;
using DevExpress.XtraEditors.Repository;
using System.IO;
using DevExpress.XtraRichEdit.Import.Html;
using System.Net.Http;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Newtonsoft.Json;
using static Commons.OSystems;
using System.Net;
using DevExpress.XtraPrinting;
using CredentialManagement;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Google.Apis.Upload;
using DevExpress.Utils.CommonDialogs.Internal;
using FirebaseAdmin;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;

namespace VietSoftHRM
{
    public partial class frmNotification : DevExpress.XtraEditors.XtraForm
    {
        // Dữ liệu được chọn
        public DataTable TableSource;
        private DataRow _dtrow;
        public DataRow RowSelected
        {
            get
            {
                return _dtrow;
            }
        }

        GoogleCredential credential = null;
        string bucketName;
        StorageClient _storageClient;
        private static string apiKey = "AIzaSyDln2NkoGScX86HiXaLMhLdKM-9Mihm7VM";
        private static string Bucket = "uploadfile-754fe.appspot.com";
        private static string AuthEmail = "dattranlfc@gmail.com";
        private static string AuthPassword = "tandat";
        public frmNotification()
        {
            InitializeComponent();

            using (var jsonStream = new FileStream($"firebase-auth.json", FileMode.Open,
                   FileAccess.Read, FileShare.Read))
            {
                credential = GoogleCredential.FromStream(jsonStream);
            }
            _storageClient = StorageClient.Create(credential);

            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, tabbedControlGroup1, windowsUIButton);
            Commons.Modules.ObjSystems.ThayDoiNN(this);
        }

        private void frmNotification_Load(object sender, EventArgs e)
        {
            try
            {

                Commons.Modules.sLoad = "0Load";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT NOI_DUNG, ISNULL(T2.FULL_NAME, T2.USER_NAME) UNAME , TINH_TRANG FROM dbo.NOTIFICATION T1 LEFT JOIN dbo.USERS T2 ON T2.ID_USER = T1.ID_USER"));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdSource, grvSource, dt, true, true, false, true, true, this.Name);
                LoadCboSP();
                Commons.Modules.sLoad = "";
                //Commons.Modules.ObjSystems.MAutoCompleteMemoEdit(txtCauQuery, Commons.Modules.ObjSystems.DataCongNhan(false), "TEN_CN");
            }
            catch { }
        }

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "EXEC":
                        {
                            if (tabbedControlGroup1.SelectedTabPage == tabThongBao)
                            {
                                try
                                {
                                    grvSource.CloseEditor();
                                    grvSource.UpdateCurrentRow();
                                    string sSQL = "UPDATE dbo.NOTIFICATION SET NOI_DUNG = N'" + grvSource.GetFocusedRowCellValue("NOI_DUNG") + "', ID_USER = " + Commons.Modules.iIDUser + ", " +
                                        "TINH_TRANG = " + Convert.ToInt32(grvSource.GetFocusedRowCellValue("TINH_TRANG")) + "";
                                    SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSQL);

                                    Commons.Modules.ObjSystems.Alert("Cập nhật thành công", Commons.Form_Alert.enmType.Success);
                                }
                                catch
                                {
                                    Commons.Modules.ObjSystems.Alert("Cập nhật không thành công", Commons.Form_Alert.enmType.Error);
                                }
                            }
                            else
                            {
                                ExecQuery();
                            }
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
            catch { }
        }

        private void txtCauQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                ExecQuery();
            }
        }

        private void ExecQuery()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (txtCauQuery.Text == "")
                {
                    grdQuery.DataSource = null;
                    return;
                }
                string sSQL = txtCauQuery.SelectedText;
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSQL));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdQuery, grvQuery, dt, false, true, false, true, false, this.Name);
                this.Cursor = Cursors.Default;
                Commons.Modules.ObjSystems.Alert("Commands completed successfully.", Commons.Form_Alert.enmType.Success);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Commons.Modules.ObjSystems.MsgError(ex.Message);
            }
        }
        private void LoadCboSP()
        {
            try
            {
                string sSQL = "SELECT -1 ID ,NULL [TYPE], NULL TEN_PROCEDURES UNION SELECT ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) STT,type ,name  FROM sys.objects WHERE type IN ('P','TF','FN')";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSQL));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearchSP, dt, "ID", "TEN_PROCEDURES", "TEN_PROCEDURES", true, false);
            }
            catch { }
        }

        private void cboSearchSP_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            string sSQL = "SELECT STUFF(OBJECT_DEFINITION(object_id),CHARINDEX('CREATE',OBJECT_DEFINITION(object_id)),LEN('CREATE'),'ALTER')  From sys.objects where name='" + cboSearchSP.Text + "'";

            string sProc = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSQL));
            //Commons.Modules.ObjSystems.MAutoCompleteMemoEdit(txtCauQuery, dt, "TEN_TABLE");
            txtCauQuery.Text = sProc;
        }

        private void txtCauQuery_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabbedControlGroup1_SelectedPageChanged(object sender, LayoutTabPageChangedEventArgs e)
        {
            if (tabbedControlGroup1.SelectedTabPage == tabBKData)
            {

            }
        }
        private string getAttachment(int iLoai, string sPath) // iLoai = 0 lấy fullName, 1 lấy Name
        {
            try
            {
                string str = "";
                if (sPath != "")
                {
                    DirectoryInfo d = new DirectoryInfo(sPath); //Assuming Test is your Folder
                    FileInfo[] Files = d.GetFiles(); //Getting Text files

                    foreach (FileInfo file in Files)
                    {
                        str = str + (iLoai == 0 ? file.FullName : file.Name) + ";";
                    }
                    if (str != "")
                    {
                        str = str.Substring(0, str.Length - 1);
                    }
                }
                return str;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private void LoadFile(string sFile)
        {
            try
            {
                string sSQL = "SELECT value TEN_FILE FROM STRING_SPLIT('" + sFile + "',';')";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSQL));
                dt.Columns["TEN_FILE"].ReadOnly = true;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdBKData, grvBKData, dt, true, true, false, true, true, this.Name);
                RepositoryItemButtonEdit btnEdit = new RepositoryItemButtonEdit();
                grvBKData.Columns["TEN_FILE"].ColumnEdit = btnEdit;
                btnEdit.ButtonClick += BtnEdit_ButtonClick;
            }
            catch { }
        }
        void UpdateProgressBar(long value)
        {
            progressBar1.BeginInvoke(new Action(() => {
                progressBar1.Value = (int)value;
            }));
        }
        void OnUploadProgress(Google.Apis.Upload.IUploadProgress progress)
        {
            switch (progress.Status)
            {
                case Google.Apis.Upload.UploadStatus.Starting:
                    progressBar1.Minimum = 0;
                    progressBar1.Value = 0;

                    break;
                case Google.Apis.Upload.UploadStatus.Completed:
                    progressBar1.Value = progressBar1.Maximum;
                    break;
                case Google.Apis.Upload.UploadStatus.Uploading:
                    UpdateProgressBar(progress.BytesSent);

                    break;
                case Google.Apis.Upload.UploadStatus.Failed:
                    MessageBox.Show("Upload failed"
                                                   + Environment.NewLine
                                                   + progress.Exception);
                    break;
            }
        }


        public static string GetMimeType(string filePath)
        {
            string mimeType = "application/unknown";
            string ext = Path.GetExtension(filePath).ToLower();

            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();

            return mimeType;
        }

        private async void BtnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {

                // Lấy ID của tệp đã upload
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void txtDuongDanFile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadFile(getAttachment(1, txtDuongDanFile.Text));
            }
        }


        private async void rdoFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //grdBKData.DataSource = null;
                //if (rdoFile.SelectedIndex == 1)
                //{
                //    List<string> listFile = await GetListFileAsync("https://api.vietsoft.com.vn/VS.Api/Support/GetFileAsync");
                //    string sListTenFile = "";
                //    foreach (var item in listFile)
                //    {
                //        sListTenFile = sListTenFile + item.ToString() + ";";
                //    }
                //    sListTenFile = sListTenFile.Substring(0, sListTenFile.Length - 1);
                //    string sSQL = "SELECT value TEN_FILE FROM STRING_SPLIT('" + sListTenFile + "',';')";
                //    DataTable dt = new DataTable();
                //    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSQL));
                //    Commons.Modules.ObjSystems.MLoadXtraGrid(grdBKData, grvBKData, dt, true, true, false, true, true, this.Name);
                //    RepositoryItemButtonEdit btnEdit = new RepositoryItemButtonEdit();
                //    grvBKData.Columns["TEN_FILE"].ColumnEdit = btnEdit;
                //    btnEdit.ButtonClick += BtnEdit_ButtonClick;
                //}
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.MsgError(ex.Message);
            }

        }
    }
}