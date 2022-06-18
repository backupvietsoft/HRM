using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Vs.Category
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sLoad();
        }

        private void sLoad()
        {
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetMenuPQ", Commons.Modules.UserName, Commons.Modules.TypeLanguage));

            tvwMenu.DataSource = null;
            tvwMenu.BeginUpdate();
            tvwMenu.DataSource = dtTmp;

            tvwMenu.KeyFieldName = "ID_MENU";
            tvwMenu.ParentFieldName = "MS_CHA";
            tvwMenu.OptionsBehavior.Editable = false;
            tvwMenu.PopulateColumns();
            tvwMenu.EndUpdate();

            tvwMenu.ExpandAll();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            sLoad();
        }
    }
}


//private void LoadTPTreeView()
//{

//    DataTable dtTmp = new DataTable();
//    //try
//    //{
//    //    dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
//    //    if (dtTmp.Rows.Count <= 0) return;

//    //    ID_TPTreeListLookUpEdit.KeyFieldName = "ID_QG";
//    //    ID_TPTreeListLookUpEdit.ParentFieldName = "MS_CHA";

//    //    //ID_TPSearchLookUpEdit.Properties.DisplayMember = "TEN";
//    //    //ID_TPSearchLookUpEdit.Properties.KeyMember = "ID_QG";

//    //    ID_TPTreeListLookUpEdit.DataSource = dtTmp;
//    //    ID_TPTreeListLookUpEdit.ExpandAll();



//    //    ID_TPSearchLookUpEdit.EditValue = dtTmp.Rows[0];
//    //    ID_TPTreeListLookUpEdit.SelectNode(ID_TPTreeListLookUpEdit.FindNodeByFieldValue("MS_CHA", dtTmp.Rows[0]["MS_CHA"]));
//    //}catch (Exception EX)
//    //{
//    //    XtraMessageBox.Show(EX.Message.ToString());
//    //}

//    try
//    {
//        string sSql = "SELECT CONVERT(NVARCHAR(250),'-1') AS MS_CHA,'QG' + CONVERT(NVARCHAR(250),ID_QG) AS ID_QG, TEN_QG FROM dbo.QUOC_GIA   UNION  SELECT 'QG' + CONVERT(NVARCHAR(250),ID_QG) AS MS_CHA, 'TP' + CONVERT(NVARCHAR(250),ID_TP) ,TEN_TP FROM dbo.THANH_PHO ";
//        dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));



//        treeListLookUpEdit1.Properties.DataSource = null;
//        treeListLookUpEdit1.Properties.DataSource = dtTmp;
//        treeListLookUpEdit1.Properties.DisplayMember = "TEN_QG";
//        treeListLookUpEdit1.Properties.ValueMember = "ID_QG";


//        treeListLookUpEdit1TreeList.KeyFieldName = "ID_QG";
//        treeListLookUpEdit1TreeList.ParentFieldName = "MS_CHA";
//        treeListLookUpEdit1TreeList.RootValue = DBNull.Value;


//        treeListLookUpEdit1TreeList.Size = treeListLookUpEdit1.CalcBestSize();



//        ID_TPSearchLookUpEdit.Properties.DataSource = null;
//        ID_TPSearchLookUpEdit.Properties.DataSource = dtTmp;
//        ID_TPSearchLookUpEdit.Properties.DisplayMember = "TEN_QG";
//        ID_TPSearchLookUpEdit.Properties.ValueMember = "ID_QG";


//        ID_TPTreeListLookUpEdit.KeyFieldName = "ID_QG";
//        ID_TPTreeListLookUpEdit.ParentFieldName = "MS_CHA";
//        ID_TPTreeListLookUpEdit.RootValue = DBNull.Value;


//        ID_TPTreeListLookUpEdit.Size = ID_TPSearchLookUpEdit.CalcBestSize();



//    }
//    catch (Exception EX)
//    {
//        XtraMessageBox.Show(EX.Message.ToString());

//    }     

//}




//private void treeListLookUpEdit1_QueryCloseUp(object sender, CancelEventArgs e)
//{
//    if ((sender as DevExpress.XtraEditors.TreeListLookUpEdit).Properties.TreeList.FocusedNode.Level == 0)
//    {
//        e.Cancel = true;
//    }
//}

//private void ID_TPSearchLookUpEdit_QueryCloseUp(object sender, CancelEventArgs e)
//{

//    if ((sender as DevExpress.XtraEditors.TreeListLookUpEdit).Properties.TreeList.FocusedNode.Level == 0)
//    {
//        (sender as DevExpress.XtraEditors.TreeListLookUpEdit).Properties.TreeList.RootValue = DBNull.Value;
//    }
//}