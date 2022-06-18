namespace VietSoftHRM
{
    partial class frmDashBoard
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDashBoard));
            DevExpress.DashboardCommon.Dimension dimension1 = new DevExpress.DashboardCommon.Dimension();
            DevExpress.DashboardCommon.Measure measure1 = new DevExpress.DashboardCommon.Measure();
            DevExpress.DashboardCommon.Dimension dimension2 = new DevExpress.DashboardCommon.Dimension();
            DevExpress.DashboardCommon.Measure measure2 = new DevExpress.DashboardCommon.Measure();
            DevExpress.DashboardCommon.DashboardLayoutGroup dashboardLayoutGroup1 = new DevExpress.DashboardCommon.DashboardLayoutGroup();
            DevExpress.DashboardCommon.DashboardLayoutItem dashboardLayoutItem1 = new DevExpress.DashboardCommon.DashboardLayoutItem();
            this.dashboardSqlDataSource1 = new DevExpress.DashboardCommon.DashboardSqlDataSource();
            this.treemapDashboardItem1 = new DevExpress.DashboardCommon.TreemapDashboardItem();
            ((System.ComponentModel.ISupportInitialize)(this.dashboardSqlDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treemapDashboardItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(dimension1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(measure1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(dimension2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(measure2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // dashboardSqlDataSource1
            // 
            this.dashboardSqlDataSource1.ComponentName = "dashboardSqlDataSource1";
            this.dashboardSqlDataSource1.ConnectionName = "192.168.2.33\\SQL2008";
            this.dashboardSqlDataSource1.Name = "SQL Data Source 1";
            storedProcQuery1.Name = "spDBoard";
            storedProcQuery1.StoredProcName = "spDBoard";
            this.dashboardSqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery1});
            this.dashboardSqlDataSource1.ResultSchemaSerializable = resources.GetString("dashboardSqlDataSource1.ResultSchemaSerializable");
            // 
            // treemapDashboardItem1
            // 
            dimension1.DataMember = "TEN";
            this.treemapDashboardItem1.Arguments.AddRange(new DevExpress.DashboardCommon.Dimension[] {
            dimension1});
            this.treemapDashboardItem1.ComponentName = "treemapDashboardItem1";
            measure1.DataMember = "MS_CN";
            measure1.SummaryType = DevExpress.DashboardCommon.SummaryType.Count;
            dimension2.DataMember = "DIA_CHI_TAM_TRU";
            measure2.DataMember = "HO";
            measure2.SummaryType = DevExpress.DashboardCommon.SummaryType.Count;
            this.treemapDashboardItem1.DataItemRepository.Clear();
            this.treemapDashboardItem1.DataItemRepository.Add(measure1, "DataItem0");
            this.treemapDashboardItem1.DataItemRepository.Add(dimension1, "DataItem1");
            this.treemapDashboardItem1.DataItemRepository.Add(dimension2, "DataItem2");
            this.treemapDashboardItem1.DataItemRepository.Add(measure2, "DataItem3");
            this.treemapDashboardItem1.DataMember = "spDBoard";
            this.treemapDashboardItem1.DataSource = this.dashboardSqlDataSource1;
            this.treemapDashboardItem1.HiddenDimensions.AddRange(new DevExpress.DashboardCommon.Dimension[] {
            dimension2});
            this.treemapDashboardItem1.HiddenMeasures.AddRange(new DevExpress.DashboardCommon.Measure[] {
            measure2});
            this.treemapDashboardItem1.InteractivityOptions.IgnoreMasterFilters = false;
            this.treemapDashboardItem1.Name = "Treemap 1";
            this.treemapDashboardItem1.ShowCaption = true;
            this.treemapDashboardItem1.Values.AddRange(new DevExpress.DashboardCommon.Measure[] {
            measure1});
            // 
            // frmDashBoard
            // 
            this.DataSources.AddRange(new DevExpress.DashboardCommon.IDashboardDataSource[] {
            this.dashboardSqlDataSource1});
            this.Items.AddRange(new DevExpress.DashboardCommon.DashboardItem[] {
            this.treemapDashboardItem1});
            dashboardLayoutItem1.DashboardItem = this.treemapDashboardItem1;
            dashboardLayoutGroup1.ChildNodes.AddRange(new DevExpress.DashboardCommon.DashboardLayoutNode[] {
            dashboardLayoutItem1});
            dashboardLayoutGroup1.DashboardItem = null;
            this.LayoutRoot = dashboardLayoutGroup1;
            this.Title.Text = "Dashboard";
            ((System.ComponentModel.ISupportInitialize)(this.dashboardSqlDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(dimension1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(measure1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(dimension2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(measure2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treemapDashboardItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.DashboardCommon.DashboardSqlDataSource dashboardSqlDataSource1;
        private DevExpress.DashboardCommon.TreemapDashboardItem treemapDashboardItem1;
    }
}
