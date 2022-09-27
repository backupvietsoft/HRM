using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Navigation;
using Vs.HRM;
using Vs.Recruit;
using Vs.Recruit.UAC;

namespace VietSoftHRM
{

    public partial class ucUngVien : DevExpress.XtraEditors.XtraUserControl
    {
        public Color color;
        public int iLoai;
        public int iIDOut;
        public string slinkcha;
        public string sLoad="";
        public ucUngVien(TileBar tileBar)
        {
            InitializeComponent();
        }
        //load tất danh mục từ menu
        private void LoadUngVien()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetMenuLeft", Commons.Modules.UserName, Commons.Modules.TypeLanguage, iLoai, Commons.Modules.sHideMenu));
            foreach (DataRow item in dt.Rows)
            {
                AccordionControlElement element = new AccordionControlElement();
                element.Expanded = true;
                element.Text = item["NAME"].ToString();
                element.Name = item["KEY_MENU"].ToString();
                element.Tag = item["CONTROLS"].ToString();
                accorMenuleft.Elements.Add(element);
                element.Click += Element_Click;
                DataTable dtchill = new DataTable();
                dtchill.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetMenuLeft", Commons.Modules.UserName, Commons.Modules.TypeLanguage, Convert.ToInt32(item["ID_MENU"]), Commons.Modules.sHideMenu));
                if (dtchill.Rows.Count > 0)
                {
                    foreach (DataRow itemchill in dtchill.Rows)
                    {
                        AccordionControlElement elementchill = new AccordionControlElement();
                        elementchill.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
                        elementchill.Text = itemchill["NAME"].ToString();
                        elementchill.Name = itemchill["KEY_MENU"].ToString();
                        elementchill.Tag = itemchill["CONTROLS"].ToString();
                        elementchill.Click += Elementchill_Click;
                        element.Elements.Add(elementchill);
                    }
                }
                else
                {
                    element.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
                }

            }
        }

        //sự kiện click cha
        private void Element_Click(object sender, EventArgs e)
        {

            var button = sender as AccordionControlElement;
            if (sLoad == button.Name) return;
            if (button.Style == DevExpress.XtraBars.Navigation.ElementStyle.Item)
            {
                //   button.Name.ToString()
                Commons.Modules.ObjSystems.GetPhanQuyen(button);

            }
            Commons.Modules.ObjSystems.ShowWaitForm(this);
            sLoad = button.Name;
            NONNlab_Link.Text = slinkcha + "/" + button.Text;
            switch (button.Name)
            {
                //case "mnuKHNghiPhep":
                //    {
                //        if (!panel2.Controls.Contains(ucKeHoachNghiPhep.Instance))
                //        {
                //            panel2.Controls.Clear();
                //            panel2.Controls.Add(ucKeHoachNghiPhep.Instance);
                //            ucKeHoachNghiPhep.Instance.Dock = DockStyle.Fill;
                //            ucKeHoachNghiPhep.Instance.BringToFront();
                //        }
                //        break;
                //    }
                //case "mnuDaoTao":
                //    {
                //        if (!panel2.Controls.Contains(ucDaoTao.Instance))
                //        {
                //            panel2.Controls.Clear();
                //            panel2.Controls.Add(ucDaoTao.Instance);
                //            ucDaoTao.Instance.Dock = DockStyle.Fill;
                //            ucDaoTao.Instance.BringToFront();
                //        }
                //        break;
                //    }
                //case "mnuThoiViec":
                //    {
                //        ucQuyetDinhThoiViec thoiviec = new ucQuyetDinhThoiViec();
                //        panel2.Controls.Clear();
                //        panel2.Controls.Add(thoiviec);
                //        thoiviec.Dock = DockStyle.Fill;
                //        break;
                //    }
                case "mnuUngVien":
                    {
                        ucQLUV ns = new ucQLUV();
                        ns.accorMenuleft = accorMenuleft;
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ns);
                        ns.lblUV = NONNlab_Link;
                        ns.lblUV.Tag = NONNlab_Link.Text;
                        ns.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuKHTD":
                    {
                        ucKeHoachTuyenDung ns = new ucKeHoachTuyenDung();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ns);
                        ns.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuYCTD":
                    {
                        ucYeuCauTuyenDung ns = new ucYeuCauTuyenDung();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ns);
                        ns.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuViTriTD":
                    {
                        ucVI_TRI_TUYEN_DUNG ns = new ucVI_TRI_TUYEN_DUNG(-1,-1,-1,true);
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ns);
                        ns.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuPhongVan":
                    {
                        //ucPhongVan ns = new ucPhongVan(-1);
                        //ns.accorMenuleft = accorMenuleft;
                        //panel2.Controls.Clear();
                        //panel2.Controls.Add(ns);
                        //ns.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuTiepNhanUngVien":
                    {
                        ucTiepNhanUngVien ns = new ucTiepNhanUngVien();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ns);
                        ns.Dock = DockStyle.Fill;
                        break;
                    }

                case "mnuDTDinhHuong":
                    {
                        ucDaoTaoDinhHuong ns = new ucDaoTaoDinhHuong();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ns);
                        ns.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuChuyenDLUV":
                    {
                        ucChuyenDuLieuNS ns = new ucChuyenDuLieuNS();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ns);
                        ns.Dock = DockStyle.Fill;
                        break;
                    }

                case "mnuKeHoachPV":
                    {
                        ucTiepNhanUngVien ns = new ucTiepNhanUngVien();
                        ns.accorMenuleft = accorMenuleft;
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ns);
                        ns.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuDinhBien_LD":
                    {
                        ucDinhBien ns = new ucDinhBien();
                        //ns.accorMenuleft = accorMenuleft;
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ns);
                        ns.Dock = DockStyle.Fill;
                        break;
                    }

                default:
                    break;
            }
            Commons.Modules.ObjSystems.HideWaitForm();

        }
        //sự kiện click con
        private void Elementchill_Click(object sender, EventArgs e)
        {
            var button = sender as AccordionControlElement;
            if (sLoad == button.Name) return;
            Commons.Modules.ObjSystems.ShowWaitForm(this);
            Commons.Modules.ObjSystems.GetPhanQuyen(button);
            sLoad = button.Name;
            NONNlab_Link.Text = slinkcha + "/" + button.Text;
            switch (button.Name)
            {
                case "mnuUngVien":
                    {
                        ucQLUV ns = new ucQLUV();
                        ns.accorMenuleft = accorMenuleft;
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ns);
                        ns.lblUV = NONNlab_Link;
                        ns.lblUV.Tag = NONNlab_Link.Text;
                        ns.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuKeHoachLaoDongNam":
                    {
                        ucDinhBienLD ns = new ucDinhBienLD();
                        //ns.accorMenuleft = accorMenuleft;
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ns);
                        ns.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuDinhBien_LD":
                    {
                        ucDinhBien ns = new ucDinhBien();
                        //ns.accorMenuleft = accorMenuleft;
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ns);
                        ns.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuKHTD":
                    {
                        ucKeHoachTuyenDung ns = new ucKeHoachTuyenDung();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ns);
                        ns.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuViTriTD":
                    {
                        ucVI_TRI_TUYEN_DUNG ns = new ucVI_TRI_TUYEN_DUNG(-1, -1, -1, true);
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ns);
                        ns.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuYCTD":
                    {
                        ucYeuCauTuyenDung ns = new ucYeuCauTuyenDung();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ns);
                        ns.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuPhongVan":
                    {
                        ucPhongVan ns = new ucPhongVan();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ns);
                        ns.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuDanhGiaTN":
                    {
                        ucDanhGiaTayNghe ns = new ucDanhGiaTayNghe();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ns);
                        ns.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuTiepNhanUngVien":
                    {
                        ucTiepNhanUngVien ns = new ucTiepNhanUngVien();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ns);
                        ns.Dock = DockStyle.Fill;
                        break;
                    }

                case "mnuDTDinhHuong":
                    {
                        ucDaoTaoDinhHuong ns = new ucDaoTaoDinhHuong();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ns);
                        ns.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuChuyenDLUV":
                    {
                        ucChuyenDuLieuNS ns = new ucChuyenDuLieuNS();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ns);
                        ns.Dock = DockStyle.Fill;
                        break;
                    }

                case "mnuKeHoachPV":
                    {
                        ucKeHoachPhongVan ns = new ucKeHoachPhongVan();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ns);
                        ns.Dock = DockStyle.Fill;
                        break;
                    }
                #region Bao cao
                case "mnuBCDSUVTuyenDung":
                    {
                        ucBaoCaoDSUVThamGiaTD tmp = new ucBaoCaoDSUVThamGiaTD();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                #endregion  

                default:
                    break;
            }
            Commons.Modules.ObjSystems.HideWaitForm();
           // accorMenuleft.OptionsMinimizing.State = DevExpress.XtraBars.Navigation.AccordionControlState.Minimized;
        }
        private void ucUngVien_Load(object sender, EventArgs e)
        {
            slinkcha = NONNlab_Link.Text;
            LoadUngVien();
            try
            {
                accorMenuleft.SelectElement(accorMenuleft.Elements[0].Elements[0]);
                Element_Click(accorMenuleft.Elements[0].Elements[0], null);
            }
            catch
            {
            }
        }
    }
}
