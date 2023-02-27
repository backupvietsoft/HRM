using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Navigation;
using Vs.HRM;
using Vs.TimeAttendance;
using Vs.Report;
using Vs.Payroll;
using Vs.Recruit;

namespace VietSoftHRM
{

    public partial class ucListBaoCao : DevExpress.XtraEditors.XtraUserControl
    {
        public Color color;
        public int iLoai;
        public int iIDOut;
        public string slinkcha;
        public string sLoad="";
        public ucListBaoCao(TileBar tileBar)
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
                case "mnuBCDonVi":
                    {
                        ucBaoCaoDonVi InDonVi = new ucBaoCaoDonVi();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(InDonVi);
                        InDonVi.Dock = DockStyle.Fill;

                        break;
                    }
                case "mnuBCXiNghiep":
                    {
                        ucBaoCaoXiNghiep InXiNghiep = new ucBaoCaoXiNghiep();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(InXiNghiep);
                        InXiNghiep.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBCTo":
                    {
                        ucBaoCaoTo InTo = new ucBaoCaoTo();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(InTo);
                        InTo.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBCTongHopNam":
                    {
                        ucBaoCaoTongHopNam tmp = new ucBaoCaoTongHopNam();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBDTangGiamCN":
                    {
                        ucBaoCaoBDTangGiamCN InBDTangGiamCN = new ucBaoCaoBDTangGiamCN();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(InBDTangGiamCN);
                        InBDTangGiamCN.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBCThongKeCNTheoBD":
                    {
                        ucBaoCaoThongKeCongNhanBD temp = new ucBaoCaoThongKeCongNhanBD();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(temp);
                        temp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBCLDTo":
                    {
                        ucBaoCaoLaoDongTo InLDTo = new ucBaoCaoLaoDongTo();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(InLDTo);
                        InLDTo.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBCQLy":
                    {
                        ucBaoCaoQuanLy InQuanLy = new ucBaoCaoQuanLy();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(InQuanLy);
                        InQuanLy.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBCCongDoan":
                    {
                        ucBaoCaoCongDoan InBCCD = new ucBaoCaoCongDoan();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(InBCCD);
                        InBCCD.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBCGiaDinh":
                    {
                        ucBaoCaoQHGD InBCGD = new ucBaoCaoQHGD();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(InBCGD);
                        InBCGD.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBCLDTinh":
                    {
                        ucBaoCaoLaoDongTinh InLDTinh = new ucBaoCaoLaoDongTinh();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(InLDTinh);
                        InLDTinh.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBCCongNhan":
                    {
                        ucBaoCaoCongNhan InCN = new ucBaoCaoCongNhan();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(InCN);
                        InCN.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBCLDHuu":
                    {
                        ucBaoCaoLaoDongHuu InLDHuu = new ucBaoCaoLaoDongHuu();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(InLDHuu);
                        InLDHuu.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBCThamNien":
                    {
                        ucBaoCaoThamNien InDSTN = new ucBaoCaoThamNien();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(InDSTN);
                        InDSTN.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBCTangLD":
                    {
                        ucBaoCaoTangLaoDong InDSTangLD = new ucBaoCaoTangLaoDong();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(InDSTangLD);
                        InDSTangLD.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBCGiamLD":
                    {
                        ucBaoCaoGiamLaoDong InDSGiamLD = new ucBaoCaoGiamLaoDong();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(InDSGiamLD);
                        InDSGiamLD.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBCHDLD":
                    {
                        ucBaoCaoHopDong InHDLD = new ucBaoCaoHopDong();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(InHDLD);
                        InHDLD.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBCTHLDThang":
                    {
                        ucBaoCaoLaoDongThang InLDT = new ucBaoCaoLaoDongThang();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(InLDT);
                        InLDT.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBLThang":
                    {
                        ucBangLuongThangNhanVien tmp = new ucBangLuongThangNhanVien();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBCDSLaoDongNu":
                    {
                        ucBaoCaoDanhSachLaoDongNu tmp = new ucBaoCaoDanhSachLaoDongNu();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuTHSDLDThang":
                    {
                        ucBaoCaoTinhHinhSuDungLaoDong tmp = new ucBaoCaoTinhHinhSuDungLaoDong();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnBCCongTac":
                    {
                        ucBaoCaoCongTac tmp = new ucBaoCaoCongTac();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnBCDNMuaBHTaiNan":
                    {
                        ucBaoCaoBaoHiemTaiNan tmp = new ucBaoCaoBaoHiemTaiNan();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }

                case "mnBCKhenThuongKyLuat":
                    {
                        ucBaoCaoKhenThuongKyLuat tmp = new ucBaoCaoKhenThuongKyLuat();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnBCDanhGianNV":
                    {
                        ucBaoCaoDanhGianNV tmp = new ucBaoCaoDanhGianNV();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnBCLaoDongNN":
                    {
                        ucBaoCaoLaoDongNuocNgoai tmp = new ucBaoCaoLaoDongNuocNgoai();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnBCCNTheoTinhThanh":
                    {
                        ucBaoCaoCongNhanTheoTinhThanh tmp = new ucBaoCaoCongNhanTheoTinhThanh();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnBCTrinhDoChuyenMon":
                    {
                        ucBaoCaoTrinhDoChuyenMon tmp = new ucBaoCaoTrinhDoChuyenMon();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnQuaTrinhDaoTao":
                    {
                        ucBaoCaoQuaTrinhDaoTao tmp = new ucBaoCaoQuaTrinhDaoTao();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }

                case "mnuTDHTC":
                    {
                        //ucBaoCaoHuongTroCapBHXH tmp = new ucBaoCaoHuongTroCapBHXH();
                        //panel2.Controls.Clear();
                        //panel2.Controls.Add(tmp);
                        //tmp.Dock = DockStyle.Fill;
                        break;
                    }

                case "mnuNhomBCNgay":
                    {
                        ucBaoCaoTheoNgay tmp = new ucBaoCaoTheoNgay();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBCGiaiDoan":
                    {
                        ucBaoCaoGiaiDoan tmp = new ucBaoCaoGiaiDoan();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBCTongHopThang":
                    {
                        ucBaoCaoTongHopThang tmp = new ucBaoCaoTongHopThang();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBCQuanLyPhep":
                    {
                        ucBaoCaoQuanLyPhep tmp = new ucBaoCaoQuanLyPhep();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBCDMCD":
                    {
                        ucBaoCaoDMCD tmp = new ucBaoCaoDMCD();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBCLuongSanPham":
                    {
                        ucBCLuongSanPham tmp = new ucBCLuongSanPham();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBCLuongThang":
                    {
                        ucBCLuongThang tmp = new ucBCLuongThang();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBCLuongThang13":
                    {
                        ucBCLuongThang13 tmp = new ucBCLuongThang13();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBCDSUVTuyenDung":
                    {
                        ucBaoCaoDSUVThamGiaTD tmp = new ucBaoCaoDSUVThamGiaTD();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                default:
                    {
                        ucBlank tmp = new ucBlank();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                    }

                    break;
            }
            Commons.Modules.ObjSystems.HideWaitForm();
            //accorMenuleft.OptionsMinimizing.State = DevExpress.XtraBars.Navigation.AccordionControlState.Minimized;
        }
        private void ucListBaoCao_Load(object sender, EventArgs e)
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
