using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Navigation;
using Vs.HRM;
using Vs.Report;
using System.Diagnostics;
using Vs.Payroll;
using VietSoftHRM;

namespace VietSoftHRM
{

    public partial class ucListLuong : DevExpress.XtraEditors.XtraUserControl
    {
        public Color color;
        public int iLoai;
        public int iIDOut;
        public string slinkcha;
        public string sLoad="";
        public ucListLuong(TileBar tileBar)
        {
            InitializeComponent();
        }
        //load tất danh mục từ menu
        private void LoadDSBaoCao()
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
                //element.Click += Element_Click;
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
        //private void Element_Click(object sender, EventArgs e)
        //{

        //    var button = sender as AccordionControlElement;
        //    if (sLoad == button.Name) return;
        //    Commons.Modules.ObjSystems.ShowWaitForm(this);
        //    sLoad = button.Name;
        //    lab_Link.Text = slinkcha + "/" + button.Text;
        //    switch (button.Name)
        //    {
        //        case "mnuBCDonVi":
        //            {
        //                if (!panel2.Controls.Contains(ucKeHoachNghiPhep.Instance))
        //                {
        //                    panel2.Controls.Clear();
        //                    panel2.Controls.Add(ucKeHoachNghiPhep.Instance);
        //                    ucKeHoachNghiPhep.Instance.Dock = DockStyle.Fill;
        //                    ucKeHoachNghiPhep.Instance.BringToFront();
        //                }
        //                break;
        //            }
        //        case "mnuBCXiNghiep":
        //            {
        //                if (!panel2.Controls.Contains(ucDaoTao.Instance))
        //                {
        //                    panel2.Controls.Clear();
        //                    panel2.Controls.Add(ucDaoTao.Instance);
        //                    ucDaoTao.Instance.Dock = DockStyle.Fill;
        //                    ucDaoTao.Instance.BringToFront();
        //                }
        //                break;
        //            }
        //        case "mnuBCTo":
        //            {
        //                ucQuyetDinhThoiViec thoiviec = new ucQuyetDinhThoiViec();
        //                panel2.Controls.Clear();
        //                panel2.Controls.Add(thoiviec);
        //                thoiviec.Dock = DockStyle.Fill;
        //                break;
        //            }
        //        default:
        //            break;
        //    }
        //    Commons.Modules.ObjSystems.HideWaitForm();

        //}
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
                case "mnuHopDongBH":
                    {
                        frmDonHangBan ctl = new frmDonHangBan(-1);
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ctl);
                        Commons.Modules.ObjSystems.HideWaitForm();
                        ctl.Dock = DockStyle.Fill;
                        break;
                    }

                case "mnuQuyTrinhCongNghe":
                    {
                        frmQTCN ctl = new frmQTCN();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ctl);
                        Commons.Modules.ObjSystems.HideWaitForm();
                        ctl.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuPhieuCongDoan":
                    {
                        frmPhieuCongDoan ctl = new frmPhieuCongDoan();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ctl);
                        Commons.Modules.ObjSystems.HideWaitForm();
                        ctl.Dock = DockStyle.Fill;
                        break;
                    }
                
                case "mnuTinhLuongThang":
                    {
                        ucTinhLuong ctl = new ucTinhLuong();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ctl);
                        Commons.Modules.ObjSystems.HideWaitForm();
                        ctl.Dock = DockStyle.Fill;
                        break;
                    }

                case "mnuTinhLuongThang 13":
                    {
                        ucBangLuongThang13 ctl = new ucBangLuongThang13();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ctl);
                        Commons.Modules.ObjSystems.HideWaitForm();
                        ctl.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuQuyDinhThamNien":
                    {
                        ucQuyDinhThamNien qdtn = new ucQuyDinhThamNien();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(qdtn);
                        Commons.Modules.ObjSystems.HideWaitForm();
                        qdtn.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuGiamTruGiaCanh":
                    {
                        ucGiamTruGiaCanh gtgc = new ucGiamTruGiaCanh();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(gtgc);
                        Commons.Modules.ObjSystems.HideWaitForm();
                        gtgc.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuQuyDinhPhuCap":
                    {
                        ucQuyDinhPhuCap gtgc = new ucQuyDinhPhuCap();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(gtgc);
                        Commons.Modules.ObjSystems.HideWaitForm();
                        gtgc.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuThueThuNhap":
                    {
                        ucThueThuNhap ttn = new ucThueThuNhap();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ttn);
                        Commons.Modules.ObjSystems.HideWaitForm();
                        ttn.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuDangKyBHXH":
                    {
                        ucDangKyBHXH ttn = new ucDangKyBHXH();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ttn);
                        Commons.Modules.ObjSystems.HideWaitForm();
                        ttn.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuMucBuLuong":
                    {
                        ucMucBuLuong ttn = new ucMucBuLuong();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ttn);
                        Commons.Modules.ObjSystems.HideWaitForm();
                        ttn.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuQuyDinhTamUng":
                    {
                        ucQuyDinhTamUng ttn = new ucQuyDinhTamUng();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ttn);
                        Commons.Modules.ObjSystems.HideWaitForm();
                        ttn.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuDanhSachTamUng":
                    {
                        ucDanhSachTamUng ttn = new ucDanhSachTamUng();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ttn);
                        Commons.Modules.ObjSystems.HideWaitForm();
                        ttn.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuTienThuongPC":
                    {
                        ucTienThuongPhuCap ctl = new ucTienThuongPhuCap();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ctl);
                        Commons.Modules.ObjSystems.HideWaitForm();
                        ctl.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuHoTroTienDo":
                    {
                        ucDSCNHoTroTienDo ttn = new ucDSCNHoTroTienDo();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ttn);
                        Commons.Modules.ObjSystems.HideWaitForm();
                        ttn.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuCachTinhLuong":
                    {
                        ucCachTinhLuong ctl = new ucCachTinhLuong();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ctl);
                        Commons.Modules.ObjSystems.HideWaitForm();
                        ctl.Dock = DockStyle.Fill;
                        break;
                    }

                case "mnuNangSuatChuyen":
                    {
                        ucNangSuatChuyen ctl = new ucNangSuatChuyen();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ctl);
                        Commons.Modules.ObjSystems.HideWaitForm();
                        ctl.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuLuongKhoan":
                    {
                        ucLuongKhoan ctl = new ucLuongKhoan();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ctl);
                        Commons.Modules.ObjSystems.HideWaitForm();
                        ctl.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuTienCongTru":
                    {
                        ucTienCongTru ctl = new ucTienCongTru();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ctl);
                        Commons.Modules.ObjSystems.HideWaitForm();
                        ctl.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuDanhSachCNKhongTinhPhuCap":
                    {
                        //ucTienCongTru ctl = new ucTienCongTru();
                        ucDSCNKhongTinhPhuCap ctl = new ucDSCNKhongTinhPhuCap();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ctl);
                        Commons.Modules.ObjSystems.HideWaitForm();
                        ctl.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuLayChamCong":
                    {
                        //ucTienCongTru ctl = new ucTienCongTru();
                        ucLayChamCong ctl = new ucLayChamCong();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ctl);
                        Commons.Modules.ObjSystems.HideWaitForm();
                        ctl.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuTinhLuong":
                    {
                        //ucTienCongTru ctl = new ucTienCongTru();
                        ucTinhLuong ctl = new ucTinhLuong();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ctl);
                        Commons.Modules.ObjSystems.HideWaitForm();
                        ctl.Dock = DockStyle.Fill;
                        break;
                    }

                #region Bao cao
                case "mnuBCDMCD":
                    {
                        ucBaoCaoDMCD tmp = new ucBaoCaoDMCD();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        Commons.Modules.ObjSystems.HideWaitForm();
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBCLuongSanPham":
                    {
                        ucBCLuongSanPham tmp = new ucBCLuongSanPham();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        Commons.Modules.ObjSystems.HideWaitForm();
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBCLuongThang":
                    {
                        ucBCLuongThang tmp = new ucBCLuongThang();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        Commons.Modules.ObjSystems.HideWaitForm();
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBCLuongThang13":
                    {
                        ucBCLuongThang13 tmp = new ucBCLuongThang13();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        Commons.Modules.ObjSystems.HideWaitForm();
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                #endregion
                default:
                    break;
            }
            //accorMenuleft.OptionsMinimizing.State = DevExpress.XtraBars.Navigation.AccordionControlState.Minimized;
        }
        private void ucListLuong_Load(object sender, EventArgs e)
        {
            slinkcha = NONNlab_Link.Text;
            LoadDSBaoCao();
            try
            {
                accorMenuleft.SelectElement(accorMenuleft.Elements[0].Elements[0]);
                Elementchill_Click(accorMenuleft.Elements[0].Elements[0], null);
            }
            catch
            {
            }
        }
        
    }
}
