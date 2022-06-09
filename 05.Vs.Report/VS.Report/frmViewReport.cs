using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UserDesigner;
using System;
using System.Data;
using System.Windows.Forms;

namespace Vs.Report
{
    public partial class frmViewReport : XtraForm
    {
        //private DataSet dsReportSource;
        //private static DataTable dtrptHeader;
        public DataSet dsReport = new DataSet();
        public XtraReport rpt;
        //XtraForm frmWait;
        public void AddDataSource(DataTable tbSource)
        {
            try
            {
                try
                {
                    dsReport.Tables.Remove(tbSource.TableName);
                }
                catch { }
                dsReport.Tables.Add(tbSource.Copy());
            }
            catch { }
        }

        public void AddDataSource(DataSet dsSource)
        {
            try
            {
                try
                {
                    foreach (DataTable dt in dsReport.Tables)
                    {
                        dsReport.Tables.Remove(dt.TableName);
                    }
                }
                    catch { }

                foreach(DataTable dt in dsSource.Tables)
                {
                    dsReport.Tables.Add(dt.Copy());
                }
            }
            catch { }
        }

        public void RemoveDataSource()
        {
            dsReport.Tables.Clear();
        }

        public frmViewReport()
        {
            //frmWait = new XtraForm()
            //{
            //    FormBorderStyle = FormBorderStyle.None,
            //    Size = new System.Drawing.Size(300, 25),
            //    ShowInTaskbar = false,
            //    StartPosition = FormStartPosition.CenterScreen,
            //    TopMost = true
            //};
            Commons.Modules.ObjSystems.ShowWaitForm(this);
            InitializeComponent();

        }



        private void frmViewReport_Load(object sender, EventArgs e)
        {
            try
            {
                //ReportDesignTool designTool = new ReportDesignTool(rpt);

                //// Invoke the Ribbon End-User Report Designer form  
                //// and load the report into it.
                //designTool.ShowRibbonDesigner();

                //// Invoke the Ribbon End-User Report Designer form modally 
                //// with the specified look and feel settings.
                //designTool.ShowRibbonDesignerDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default);

                this.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());


            }
            catch { }

            #region Create form wait            
            // Create a ProgressBar along with its ReflectorBar.
            //ProgressBarControl progressBar = new ProgressBarControl();
            //ReflectorBar reflectorBar = new ReflectorBar(progressBar);

            //frmWait.Controls.Add(progressBar);
            //progressBar.Dock = DockStyle.Fill;
            //frmWait.Show();


            try
            {
                documentViewer1.PrintingSystem = rpt.PrintingSystem;
                rpt.DataSource = dsReport;
                //rpt.PrintingSystem.ProgressReflector = reflectorBar;


                //if (System.IO.File.Exists(filePath))
                //{
                //    rpt.LoadLayout("D:\\rptTienThuongXepLoai.prnx.repx");
                //}
                //else
                //{
                //    System.Console.WriteLine("The source file does not exist.");
                //}

                rpt.CreateDocument();
            }
            finally
            {
                // Unregister the reflector bar, so that it no longer
                // reflects the state of a ProgressReflector.
                ////////rpt.PrintingSystem.ResetProgressReflector();
                ////////frmWait.Close();
                ////////frmWait.Dispose();
            }
            #endregion

            Commons.Modules.ObjSystems.HideWaitForm();

        }

        private void documentViewer1_Load(object sender, EventArgs e)
        {
            documentViewer1.PrintingSystem = rpt.PrintingSystem;
            rpt.DataSource = dsReport;
            rpt.CreateDocument();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void bbiDesign_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Create a Design Tool instance with a report assigned to it.
            ReportDesignTool dt = new ReportDesignTool(rpt);

            // Access the report's properties.
            dt.Report.DrawGrid = false;

            // Access the Designer form's properties.
            dt.DesignForm.SetWindowVisibility(DesignDockPanelType.FieldList | DesignDockPanelType.PropertyGrid, false);

            // Show the Designer form, modally.
            //dt.ShowDesignerDialog();
            //dt.ShowRibbonDesignerDialog();
            dt.ShowDesigner(DevExpress.LookAndFeel.UserLookAndFeel.Default, DesignDockPanelType.PropertyGrid | DesignDockPanelType.ReportExplorer);



            //XRDesignRibbonForm designForm = new XRDesignRibbonForm();

            //// Create a new blank report.
            //designForm.OpenReport(rpt);

            //// Display the Report Designer form.
            ////designForm.Show();

            //// Display the Report Designer form, modally.
            //designForm.ShowDialog();

        }
        public Control FindFocusedControl(Control control)
        {
            var container = control as IContainerControl;
            while (container != null)
            {
                control = container.ActiveControl;
                container = control as IContainerControl;
            }
            return control;
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    Control cl = FindFocusedControl(this);
                    if (cl != null && cl.GetType() == typeof(TextBox))
                        return base.ProcessCmdKey(ref msg, keyData);
                    else
                        if (this.Name != "frmMain")
                        this.Close();
                    return true;
                case (Keys.Shift | Keys.F1):
                    frmHelp_View fr = new frmHelp_View();
                    //     fr.txHelp = rpt.Band.ToString();
                    fr.ShowDiaLogControls2(rpt);
                    return true;
                default: return base.ProcessCmdKey(ref msg, keyData);
            }
        }
    }
}