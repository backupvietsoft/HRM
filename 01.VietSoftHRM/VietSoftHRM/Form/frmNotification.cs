using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraLayout;
using DevExpress.XtraEditors.Repository;
using System.IO;
using Newtonsoft.Json;
using System.Net;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Google.Apis.Upload;
using System.Threading;
using DevExpress.Utils.Menu;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

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

		private static Bitmap screenBitmap;
		private static Graphics screenGraphics;

		GoogleCredential credential = null;
		StorageClient _storageClient;
		private static string apiKey = "AIzaSyDln2NkoGScX86HiXaLMhLdKM-9Mihm7VM";
		private static string Bucket = "uploadfile-754fe.appspot.com";
		private static string AuthEmail = "dattranlfc@gmail.com";
		private static string AuthPassword = "tandat";
		public frmNotification()
		{
			InitializeComponent();

			//using (var jsonStream = new FileStream($"firebase-auth.json", FileMode.Open,
			//	   FileAccess.Read, FileShare.Read))
			//{
			//	credential = GoogleCredential.FromStream(jsonStream);
			//}
			//_storageClient = StorageClient.Create(credential);

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
			progressBar1.BeginInvoke(new Action(() =>
			{
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

		private void ProcessClipboardText(string clipboardText)
		{
			// Xử lý nội dung đã sao chép, ví dụ:
			// Giả sử nội dung có định dạng: "ID: <id> | Password: <password>"
			// Ta cần tách lấy <id> và <password>

			int idStartIndex = clipboardText.IndexOf("ID:") + 3;
			int idEndIndex = clipboardText.IndexOf("|", idStartIndex);
			string id = clipboardText.Substring(idStartIndex, idEndIndex - idStartIndex).Trim();

			int passwordStartIndex = clipboardText.IndexOf("Password:") + 9;
			string password = clipboardText.Substring(passwordStartIndex).Trim();
		}

		public class WindowsApi
		{
			[DllImport("User32.dll", EntryPoint = "FindWindow")]
			public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

			[DllImport("User32.dll", EntryPoint = "FindWindowEx")]
			public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName, string lpWindowName);


			[DllImport("User32.dll", EntryPoint = "SendMessage")]
			public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, StringBuilder lParam);

			[DllImport("user32.dll", EntryPoint = "GetWindowText")]
			public static extern int GetWindowText(IntPtr hwnd, StringBuilder lpString, int cch);

			[DllImport("user32.dll", SetLastError = true)]
			public static extern IntPtr GetWindow(IntPtr hWnd, GetWindowCmd uCmd);

			[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
			public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

			[DllImport("user32.dll", EntryPoint = "ShowWindow")]
			public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
		}
		public enum GetWindowCmd : uint
		{
			GW_HWNDFIRST = 0,
			GW_HWNDLAST = 1,
			GW_HWNDNEXT = 2,
			GW_HWNDPREV = 3,
			GW_OWNER = 4,
			GW_CHILD = 5,
			GW_ENABLEDPOPUP = 6
		}
		private static Regex userReg;

		public class TeamviewerHelper
		{
			static TeamviewerHelper()
			{
				userReg = new Regex(@"\d+ \d+ \d+", RegexOptions.Singleline | RegexOptions.Compiled);
			}
			public TeamviewerHelper()
			{
				Username = string.Empty;
				Password = string.Empty;
				Holder = string.Empty;
			}
			internal int _count;
			public string Username;
			public string Password;
			public string Holder;
		}
		public static TeamviewerHelper GetUser(string titleApp, string className)
		{
			TeamviewerHelper user = new TeamviewerHelper();
			IntPtr tvHwnd = WindowsApi.FindWindow(null, titleApp);
			if (tvHwnd != IntPtr.Zero)
			{
				IntPtr winParentPtr = WindowsApi.GetWindow(tvHwnd, GetWindowCmd.GW_CHILD);
				while (winParentPtr != IntPtr.Zero)
				{

					IntPtr winSubPtr = WindowsApi.GetWindow(winParentPtr, GetWindowCmd.GW_CHILD);
					while (winSubPtr != IntPtr.Zero)
					{
						StringBuilder controlName = new StringBuilder(512);
						WindowsApi.GetClassName(winSubPtr, controlName, controlName.Capacity);

						if (controlName.ToString() == className)
						{
							var a = controlName;
							StringBuilder winMessage = new StringBuilder(512);
							WindowsApi.SendMessage(winSubPtr, 0xD, (IntPtr)winMessage.Capacity, winMessage);
							string message = winMessage.ToString();
							if (userReg.IsMatch(message))
							{
								user.Username = message;
								user._count += 1;

							}
							else if (user.Password != string.Empty)
							{
								user.Holder = message;
								user._count += 1;
							}
							else
							{
								user.Password = message;
								user._count += 1;
							}
							if (user._count == 100)
							{
								return user;
							}
						}
						winSubPtr = WindowsApi.GetWindow(winSubPtr, GetWindowCmd.GW_HWNDNEXT);
					}
					winParentPtr = WindowsApi.GetWindow(winParentPtr, GetWindowCmd.GW_HWNDNEXT);
				}
			}
			return user;
		}
		private async void BtnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			Process.Start("C:\\Program Files (x86)\\UltraViewer\\UltraViewer_Desktop.exe");

			// Tạm dừng thực thi trong 5 giây
			Thread.Sleep(5000);

			//var userInfo = GetUser("UltraViewer 6.4 - Free", "WindowsForms10.EDIT.app.0.34f5582_r14_ad1");

			//// Gửi phím Tab để di chuyển đến ô ID
			//InputSimulator inputSimulator = new InputSimulator();
			//inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.MENU,VirtualKeyCode.TAB);
			//inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
			//inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
			//inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
			//inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
			//inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
			//inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
			//inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
			//inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
			//inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
			//inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
			//inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
			//inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
			//inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
			//inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
			//inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
			//inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
			//inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);



			//inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_C);

			//string clipboardText = Clipboard.GetText();
			//IntPtr ultraViewerWindowHandle = FindWindowByCaption(IntPtr.Zero, "UltraViewer");


			//// Lấy kích thước màn hình
			int screenWidth = Screen.PrimaryScreen.Bounds.Width;
			int screenHeight = Screen.PrimaryScreen.Bounds.Height;

			// Tạo một Bitmap mới với kích thước của màn hình
			using (Bitmap bitmap = new Bitmap(screenWidth, screenHeight))
			{
				// Tạo đối tượng Graphics từ Bitmap
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					// Sao chép nội dung của màn hình vào Bitmap
					graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
				}

				// Lưu Bitmap vào một tập tin
				string filePath = txtDuongDanFile.Text + "\\screenshot.png"; // Đường dẫn và tên tập tin để lưu
				bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);

				XtraMessageBox.Show("Đã chụp màn hình và lưu vào: " + filePath);
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
		public async Task<BaseResponse> UploadFileAsync(string path)
		{
			try
			{


				//WebClient client = new WebClient();
				//client.Encoding = Encoding.UTF8;
				//// Đăng ký sự kiện ProgressChanged để cập nhật giá trị của progressBar

				//client.UploadProgressChanged += (s, ev) =>
				//{
				//                progressBar1.Value = ev.ProgressPercentage;
				//            };
				//            //client.Headers.Add("Content-Type", "application/octet-stream");
				//            client.UploadFileAsync(new Uri(path), "Release.zip");
				//            //string response = await client.DownloadStringTaskAsync(path);

				string requestUrl = $"{path}?path={Uri.EscapeDataString(txtDuongDanFile.Text)}&fileName={Uri.EscapeDataString(grvBKData.GetFocusedRowCellValue("TEN_FILE").ToString())}";

				string filePath = txtDuongDanFile.Text + grvBKData.GetFocusedRowCellValue("TEN_FILE").ToString();
				var client = new WebClient();
				client.UploadProgressChanged += Client_UploadProgressChanged;
				client.Headers.Add("Content-Type", "binary/octet-stream");
				string response = await client.UploadStringTaskAsync(requestUrl, "POST", filePath);
				BaseResponse result = JsonConvert.DeserializeObject<BaseResponse>(response);
				return result;
			}
			catch (Exception ex)
			{
				Commons.Modules.ObjSystems.MsgError(ex.Message);
				return null;
			}
		}
		public async Task<List<string>> GetListFileAsync(string path)
		{
			try
			{
				var client = new WebClient();
				client.UploadProgressChanged += Client_UploadProgressChanged;
				client.Headers.Add("Content-Type", "binary/octet-stream");
				string response = await client.DownloadStringTaskAsync(path);
				var result = JsonConvert.DeserializeObject<List<string>>(response);
				return result;
			}
			catch (Exception ex)
			{
				Commons.Modules.ObjSystems.MsgError(ex.Message);
				return null;
			}
		}
		private void Client_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
		{
			progressBar1.Value = e.ProgressPercentage;
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
		#region Upload, DownLoad File

		private async void UpDownLoadFile()
		{
			try
			{
				//this.Cursor = Cursors.WaitCursor;
				////var result = await callAPI("http://192.168.2.114:7174/api/home/upload-file?fileName=" + grvBKData.GetFocusedRowCellValue("TEN_FILE") + "&path=" + txtDuongDanFile.Text + "");
				//var result = await UploadFileAsync("http://192.168.2.114:7174/api/home/upload-file");
				//if (result.isSuccessStatusCode)
				//{
				//    this.Cursor = Cursors.Default;
				//    Commons.Modules.ObjSystems.Alert("Upload file thành công", Commons.Form_Alert.enmType.Success);
				//}
				//else
				//{
				//    this.Cursor = Cursors.Default;
				//    Commons.Modules.ObjSystems.MsgError(result.message);
				//}
				this.Cursor = Cursors.WaitCursor;


				if (rdoFile.SelectedIndex == 0)
				{
					string filePath = txtDuongDanFile.Text + grvBKData.GetFocusedRowCellValue("TEN_FILE").ToString();

					using (var fileStream = new FileStream(filePath, FileMode.Open,
						FileAccess.Read, FileShare.Read))
					{
						progressBar1.Maximum = (int)fileStream.Length;

						var uploadObjectOptions = new UploadObjectOptions
						{

							ChunkSize = UploadObjectOptions.MinimumChunkSize
						};
						var progressReporter = new Progress<IUploadProgress>(OnUploadProgress);
						//var result = await _storageClient.UploadObjectAsync(Bucket, Path.GetFileName(dlg.FileName), "application/octet-stream", fileStream, uploadObjectOptions, progress: progressReporter);
						var result = await _storageClient.UploadObjectAsync(Bucket, Path.GetFileName(filePath), "application/octet-stream", fileStream);
						this.Cursor = Cursors.Default;
						Commons.Modules.ObjSystems.Alert("Upload file thành công", Commons.Form_Alert.enmType.Success);
						//btn_getFiles_Click(sender, e);
					}
				}
				else
				{

					var token = new CancellationTokenSource().Token;
					string localPath = txtDuongDanFile.Text + "\\" + grvBKData.GetFocusedRowCellValue("name");
					if (txtDuongDanFile.Text == "")
					{
						this.Cursor = Cursors.Default;
						Commons.Modules.ObjSystems.MsgError("Đường dẫn lưu file không tồn tại");
						return;
					}
					using (var fileStream = File.Create(localPath))
					{
						//progressBar1.Maximum = int.Parse(lbl_byte.Text);

						var downloadObjectOptions = new DownloadObjectOptions
						{
							ChunkSize = UploadObjectOptions.MinimumChunkSize
						};
						//var progressReporter = new Progress<IDownloadProgress>(OnDownloadProgress);
						await _storageClient.DownloadObjectAsync(Bucket, Path.GetFileName(txtDuongDanFile.Text + "\\" + grvBKData.GetFocusedRowCellValue("name")), fileStream, downloadObjectOptions, token).ConfigureAwait(true);
						Commons.Modules.ObjSystems.Alert("Dowload file thành công", Commons.Form_Alert.enmType.Success);
						this.Cursor = Cursors.Default;
					}
				}


			}
			catch (Exception ex)
			{
				this.Cursor = Cursors.Default;
				XtraMessageBox.Show(ex.Message);
			}
		}

		public string BytesToReadableValue(long number)
		{
			var suffixes = new List<string> { " B", " KB", " MB", " GB", " TB", " PB" };

			for (int i = 0; i < suffixes.Count; i++)
			{
				long temp = number / (int)Math.Pow(1024, i + 1);

				if (temp == 0)
				{
					return (number / (int)Math.Pow(1024, i)) + suffixes[i];
				}
			}

			return number.ToString();
		}
		public class fileInfo
		{
			public string id { get; set; }
			public string md5 { get; set; }
			public string name { get; set; }
			public string size { get; set; }
			public string sizeText { get; set; }
		}
		#endregion



		private async void rdoFile_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				grdBKData.DataSource = null;
				if (rdoFile.SelectedIndex == 1)
				{
					var files = new List<fileInfo>();

					foreach (var obj in _storageClient.ListObjects(Bucket, ""))
					{
						var file = new fileInfo();
						file.id = obj.Generation.ToString();
						file.md5 = obj.Md5Hash;
						file.name = obj.Name;
						file.size = obj.Size + "";
						file.sizeText = BytesToReadableValue(long.Parse(obj.Size.ToString()));
						files.Add(file);

					}

					DataTable dt = new DataTable();
					dt.Columns.Add("id", typeof(string));
					dt.Columns.Add("md5", typeof(string));
					dt.Columns.Add("name", typeof(string));
					dt.Columns.Add("size", typeof(string));
					dt.Columns.Add("sizeText", typeof(string));

					foreach (var fileDetails in files)
					{
						DataRow row = dt.NewRow();
						row["id"] = fileDetails.id;
						row["md5"] = fileDetails.md5;
						row["name"] = fileDetails.name;
						row["size"] = fileDetails.size;
						row["sizeText"] = fileDetails.sizeText;
						dt.Rows.Add(row);
					}
					Commons.Modules.ObjSystems.MLoadXtraGrid(grdBKData, grvBKData, dt, true, true, false, true, true, this.Name);
					RepositoryItemButtonEdit btnEdit = new RepositoryItemButtonEdit();
					//grvBKData.Columns["TEN_FILE"].ColumnEdit = btnEdit;
					//btnEdit.ButtonClick += BtnEdit_ButtonClick;
				}
			}
			catch (Exception ex)
			{
				Commons.Modules.ObjSystems.MsgError(ex.Message);
			}

		}

		class RowInfo
		{
			public RowInfo(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
			{
				this.RowHandle = rowHandle;
				this.View = view;
			}
			public DevExpress.XtraGrid.Views.Grid.GridView View;
			public int RowHandle;
		}
		public DXMenuItem MCreateMenuUpDownLoadFile(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
		{
			string sStr = rdoFile.SelectedIndex == 0 ? "Upload" : "Download";
			DXMenuItem menuThongTinNS = new DXMenuItem(sStr, new EventHandler(CapNhatTT));
			menuThongTinNS.Tag = new RowInfo(view, rowHandle);
			return menuThongTinNS;
		}
		public void CapNhatTT(object sender, EventArgs e)
		{
			try
			{
				UpDownLoadFile();
			}
			catch (Exception ex) { Commons.Modules.ObjSystems.MsgError(ex.Message); }
		}
		private void grvBKData_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
		{
			try
			{
				DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
				if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
				{
					int irow = e.HitInfo.RowHandle;
					e.Menu.Items.Clear();
					DevExpress.Utils.Menu.DXMenuItem itemTTNS = MCreateMenuUpDownLoadFile(view, irow);
					e.Menu.Items.Add(itemTTNS);
				}
			}
			catch
			{
			}
		}
	}
}