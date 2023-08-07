using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Vs.HRM
{
    public partial class ucCTQLNS : DevExpress.XtraEditors.XtraUserControl
    {
        List<LabelControl> List;
        private string tab = "";
        public DataTable dt;
        public bool flag = false; // flag = false load như bình thường, flag == true thì load theo từng tab được chỉ định
        public string sTenLab = "";
        private LabelControl labelTemp;
        private bool changeCN = false;
        public LabelControl labelNV;
        public ucCTQLNS(Int64 iIdCN)
        {
            InitializeComponent();
            //Commons.Modules.OXtraGrid.MFieldRequest(labLyLich);
            //Commons.Modules.OXtraGrid.MFieldRequest(labHopDong);
            //Commons.Modules.OXtraGrid.MFieldRequest(labCongTac);
            //Commons.Modules.OXtraGrid.MFieldRequest(labTienLuong);
            //Commons.Modules.OXtraGrid.MFieldRequest(labDanhGia);
            //Commons.Modules.OXtraGrid.MFieldRequest(labKhanThuong);
            //Commons.Modules.OXtraGrid.MFieldRequest(labTaiNan);
            navigationFrame1.AllowTransitionAnimation = DevExpress.Utils.DefaultBoolean.True;
            navigationFrame1.TransitionAnimationProperties.FrameCount = 0;
            navigationFrame1.TransitionAnimationProperties.FrameInterval = 0;

            Commons.Modules.ObjSystems.ThayDoiNN(this);
            Commons.Modules.iCongNhan = iIdCN;
            //Commons.Modules.bEnabel = false; //bEnabel == false load view, bEnabel == true thì ko cho click ở chỗ khác
        }
        private void ucCTQLNS_Load(object sender, EventArgs e)
        {

            try
            {
                XuLyTab();
                if (flag == true)
                {
                    switch (sTenLab)
                    {
                        case "labHopDong":
                            {
                                Lb_Click(labHopDong, null);
                                break;
                            }
                        case "labCongTac":
                            {
                                Lb_Click(labCongTac, null);
                                break;
                            }
                        case "labTienLuong":
                            {
                                Lb_Click(labTienLuong, null);
                                break;
                            }
                        case "LabKhenThuong":
                            {
                                Lb_Click(labKhanThuong, null);
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
                else
                {
                    Lb_Click(labLyLich, null);
                }
                Commons.Modules.sLoad = "0Load";
                string sSQL = "SELECT ID_CN, MS_CN, HO + ' ' + ISNULL(TEN,'') HO_TEN FROM dbo.CONG_NHAN ORDER BY MS_CN";
                if (Commons.Modules.KyHieuDV == "TG")
                {
                    sSQL = "SELECT ID_CN, MS_CN, HO + ' ' + ISNULL(TEN,'') HO_TEN, ISNULL(SO_CMND,0) SO_CMND, ISNULL(SO_BHXH,0) SO_BHXH FROM dbo.CONG_NHAN ORDER BY MS_CN";
                }
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSQL));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboCongNhan, dt, "ID_CN", "MS_CN", "MS_CN", true, true, false);
                cboCongNhan.EditValue = Convert.ToInt64(Commons.Modules.iCongNhan);
                Commons.Modules.sLoad = "";
            }
            catch { }
        }
        private void XuLyTab()
        {
            List = new List<LabelControl>() { labLyLich, labCongTac, labHopDong, labTienLuong, labKhanThuong, labTaiNan, labDanhGia };
            foreach (LabelControl lb in List)
            {
                lb.Click += Lb_Click;
            }
        }
        private void Lb_Click(object sender, EventArgs e)
        {
            if (Commons.Modules.bChangeForm == true)
            {
                Commons.Modules.ObjSystems.MsgWarning("Dữ liệu chưa được lưu !");
                return;
            }
            //if (Commons.Modules.bEnabel == true)
            //{
            //    return;
            //}
            try
            {
                var lable = sender as LabelControl;
                if (flag == false)
                {
                    if (Commons.Modules.iCongNhan == 0 && lable.Name != "labLyLich") return;
                }
                if (tab == lable.Name && !changeCN) return;
                changeCN = false;
                Commons.Modules.ObjSystems.ShowWaitForm(this);
                foreach (LabelControl lc in List)
                {
                    if (lable.Name == lc.Name)
                    {
                        lc.Appearance.ForeColor = Color.FromArgb(0, 0, 192);
                        LoaduacCongNhan(lc.Name);
                        tab = lable.Name;
                        //return;
                    }
                    if (lable.Name != lc.Name)
                    {
                        lc.Appearance.ForeColor = Color.Empty;
                        //lc.Appearance.Font = this.Font;
                    }
                }
                labelTemp = lable;
                Commons.Modules.ObjSystems.HideWaitForm();

            }
            catch
            {
            }
        }
        private void Selecttab(NavigationPage page)
        {

            Thread thread = new Thread(delegate ()
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        navigationFrame1.SelectedPage = page;
                    }));
                }
            }, 100); thread.Start();
        }
        private ucLyLich ll;
        private ucHopDong hd;
        private ucTienLuong tl;
        private ucQTCongTac qtct;
        private ucKhenThuong ktkl;
        private ucTaiNanLD tnld;
        private ucDanhGia dg;
        private void LoaduacCongNhan(string tenlable)
        {
            switch (tenlable)
            {
                case "labLyLich":
                    {
                        if (navigationPage1.Controls.Count == 0)
                        {
                            ll = new ucLyLich(Commons.Modules.iCongNhan);
                            ll.Dock = DockStyle.Fill;
                            navigationPage1.Controls.Add(ll);
                        }
                        else
                        {
                            ll.BinDingData(false);
                        }
                        Selecttab(navigationPage1);
                        break;
                    }
                case "labCongTac":
                    {
                        if (navigationPage2.Controls.Count == 0)
                        {
                            qtct = new ucQTCongTac(Commons.Modules.iCongNhan);
                            qtct.Dock = DockStyle.Fill;
                            navigationPage2.Controls.Add(qtct);
                        }
                        else
                        {
                            qtct.LoadgrdCongTac(Convert.ToInt32(Commons.Modules.iCongNhan));
                            //qtct.Load_ChuyenTu();
                        }
                        Selecttab(navigationPage2);
                        break;
                    }
                case "labTienLuong":
                    {
                        if (navigationPage3.Controls.Count == 0)
                        {
                            tl = new ucTienLuong(Commons.Modules.iCongNhan);
                            tl.Dock = DockStyle.Fill;
                            navigationPage3.Controls.Add(tl);
                        }
                        else
                        {
                            tl.LoadgrdTienLuong(Convert.ToInt32(Commons.Modules.iCongNhan));
                        }
                        Selecttab(navigationPage3);
                        break;
                    }
                case "labHopDong":
                    {
                        if (navigationPage4.Controls.Count == 0)
                        {
                            hd = new ucHopDong(Commons.Modules.iCongNhan);
                            //hd.ucNS = new ucCTQLNS(Commons.Modules.iCongNhan);
                            hd.Dock = DockStyle.Fill;
                            navigationPage4.Controls.Add(hd);
                        }
                        else
                        {
                            hd.LoadgrdHopDong(-1);
                        }
                        Selecttab(navigationPage4);
                        break;
                    }

                case "labKhanThuong":
                    {
                        if (navigationPage5.Controls.Count == 0)
                        {
                            ktkl = new ucKhenThuong(Commons.Modules.iCongNhan);
                            ktkl.Dock = DockStyle.Fill;
                            navigationPage5.Controls.Add(ktkl);
                        }
                        else
                        {
                            ktkl.LoadgrdKhenThuong(-1);
                        }
                        Selecttab(navigationPage5);
                        break;
                    }
                case "labTaiNan":
                    {
                        if (navigationPage6.Controls.Count == 0)
                        {
                            tnld = new ucTaiNanLD(Commons.Modules.iCongNhan);
                            tnld.Dock = DockStyle.Fill;
                            navigationPage6.Controls.Add(tnld);
                        }
                        else
                        {
                            tnld.LoadgrdTaiNan(-1);
                        }
                        Selecttab(navigationPage6);
                        break;
                    }
                case "labDanhGia":
                    {
                        if (navigationPage7.Controls.Count == 0)
                        {
                            dg = new ucDanhGia(Commons.Modules.iCongNhan);
                            dg.Dock = DockStyle.Fill;
                            navigationPage7.Controls.Add(dg);
                        }
                        else
                        {
                            dg.LoadGrdBangDanhGia(-1);
                        }
                        Selecttab(navigationPage7);
                        break;
                    }
                //case "labBangCap":
                //    {
                //        if (navigationPage8.Controls.Count == 0)
                //        {
                //            ucBangCap ll = new ucBangCap(Commons.Modules.iCongNhan);
                //            ll.Dock = DockStyle.Fill;
                //            navigationPage8.Controls.Add(ll);
                //        }
                //        Selecttab(navigationPage8);
                //        break;
                //    }
                default:
                    break;
            }
        }
        private NavigationPage checkfameexits(string tab)
        {
            try
            {
                NavigationPage page = new NavigationPage();
                foreach (NavigationPage item in navigationFrame1.Pages)
                {
                    if (item.Tag.ToString() == tab)
                    {
                        page = item;
                        return page;
                    }
                    else
                    {
                        return null;
                    }
                }
                return page;
            }
            catch
            {
                return null;
            }
        }

        private void cboCongNhan_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Commons.Modules.sLoad == "0Load") return;
                Commons.Modules.sLoad = "0Load";
                Commons.Modules.iCongNhan = Convert.ToInt64(cboCongNhan.EditValue);
                changeCN = true;
                Lb_Click(labelTemp, null);
                string sSQL = "SELECT TOP 1 TEN_TO FROM dbo.[TO] T1 INNER JOIN dbo.CONG_NHAN T2 ON T1.ID_TO = T2.ID_TO WHERE T2.ID_CN = " + Commons.Modules.iCongNhan + "";
                string sTenTo = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSQL));
                labelNV.Text = cboCongNhan.Text + " - " + ((System.Data.DataRowView)cboCongNhan.GetSelectedDataRow()).Row.ItemArray[2].ToString() + " - " + sTenTo;
                Commons.Modules.sLoad = "";
            }
            catch { }

        }
    }
}
