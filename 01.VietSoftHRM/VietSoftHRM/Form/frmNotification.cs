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
		HttpClient _httpClient;
		public frmNotification()
		{
			InitializeComponent();
			Commons.Modules.ObjSystems.ThayDoiNN(this, Root, tabbedControlGroup1, windowsUIButton);
			Commons.Modules.ObjSystems.ThayDoiNN(this);
			_httpClient = new HttpClient();
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
		private async void BtnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{
				var result = await callAPI("http://192.168.2.114:7174/api/home/upload-file?fileName=" + grvBKData.GetFocusedRowCellValue("TEN_FILE") + "&path=" + txtDuongDanFile.Text + "");
				if (result.isSuccessStatusCode)
				{
					Commons.Modules.ObjSystems.Alert("Upload file thành công", Commons.Form_Alert.enmType.Success);
				}
				else
				{
					Commons.Modules.ObjSystems.MsgError(result.message);
				}
			}
			catch (Exception ex)
			{
				XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgBanKhongCoQuyenTruyCapDD"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgfrmThongBao"), MessageBoxButtons.OK);
			}
		}

		private void txtDuongDanFile_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				LoadFile(getAttachment(1, txtDuongDanFile.Text));
			}
		}

		#region api
		public async Task<BaseResponse> callAPI(string path)
		{
			try
			{
				System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

				WebClient client = new WebClient();
				client.Encoding = Encoding.UTF8;
                // Đăng ký sự kiện ProgressChanged để cập nhật giá trị của progressBar

                client.DownloadProgressChanged += (s, ev) =>
                {
                    progressBar1.BeginInvoke(new Action(() =>
                    {
                        progressBar1.Value = ev.ProgressPercentage;
                    }));
                };

                string response = await client.DownloadStringTaskAsync(path);
				DataTable dt = new DataTable();
				//dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.DeserializeObject(response).ToString());
				BaseResponse result = JsonConvert.DeserializeObject<BaseResponse>(response);
				return result;
			}
			catch
			{
				return null;
			}
		}
		
		public class BaseResponse
		{
			public int statusCode { get; set; }
			public string message { get; set; }
			public string responseData { get; set; }
			public IList<string> responseHeader { get; set; }
			public bool isSuccessStatusCode { get; set; }
			public string resourceKey { get; set; }
			public int timeExecute { get; set; }
			public string trackingMessage { get; set; }

		}
		#endregion
	}
}