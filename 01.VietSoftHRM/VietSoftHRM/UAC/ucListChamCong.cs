using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Navigation;
using Vs.HRM;
using Vs.TimeAttendance;
using Vs.Report;
using System.Diagnostics;

namespace VietSoftHRM
{

    public partial class ucListChamCong : DevExpress.XtraEditors.XtraUserControl
    {
        public Color color;
        public int iLoai;
        public int iIDOut;
        public string slinkcha;
        public string sLoad="";
        public ucListChamCong(TileBar tileBar)
        {
            InitializeComponent();
        }
        //load tất danh mục từ menu
        private void LoadDSBaoCao()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetMenuLeft", Commons.Modules.UserName, Commons.Modules.TypeLanguage, iLoai));
            foreach (DataRow item in dt.Rows)
            {
                AccordionControlElement element = new AccordionControlElement();
                element.Expanded = true ;
                element.Text = item["NAME"].ToString();
                element.Name = item["KEY_MENU"].ToString();
                element.Tag = item["CONTROLS"].ToString();
                accorMenuleft.Elements.Add(element);
                element.Click += Element_Click;
                DataTable dtchill = new DataTable();
                dtchill.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetMenuLeft", Commons.Modules.UserName, Commons.Modules.TypeLanguage, Convert.ToInt32(item["ID_MENU"])));
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
                case "mnuGoiChamCong":
                    {
                        Process.Start(@"D:\VietSoft_ERP\FileExcel\ChamCong.exe");
                        break;
                    }
                default:
                    break;
            }
            Commons.Modules.ObjSystems.HideWaitForm();

        }
        //sự kiện click con
        //private void Element_Click(object sender, EventArgs e)
        //{
        //    var button = sender as AccordionControlElement;
        //    if (sLoad == button.Name) return;
        //    Commons.Modules.ObjSystems.ShowWaitForm(this);
        //    sLoad = button.Name;
        //    lab_Link.Text = slinkcha + "/" + button.Text;
        //    switch (button.Name)
        //    {
        //        case "mnuGoiChamCong":
        //            {
        //                Process.Start(@"D:\VietSoft_ERP\FileExcel\ChamCong.exe");
        //                break;
        //            }

        //        default:
        //            break;
        //    }
        //    Commons.Modules.ObjSystems.HideWaitForm();
        //}

        private void Elementchill_Click(object sender, EventArgs e)
        {
            var button = sender as AccordionControlElement;
            if (sLoad == button.Name) return;
            Commons.Modules.ObjSystems.ShowWaitForm(this);
            sLoad = button.Name;
            NONNlab_Link.Text = slinkcha + "/" + button.Text;
            if (button.Style == DevExpress.XtraBars.Navigation.ElementStyle.Item)
            {
                Commons.Modules.ObjSystems.GetPhanQuyen(button);

            }
            switch (button.Name)
            {
                case "mnuDangKiLamThem":
                    {
                        ucDangKiLamThem tmp = new ucDangKiLamThem();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }

                case "mnuTinhDiemThang":
                    {
                        ucDiemChuyenCanThang tmp = new ucDiemChuyenCanThang();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuViPhamNoiQuyLD":
                    {
                        ucViPhamNoiQuyLD tmp = new ucViPhamNoiQuyLD();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuCheDoLamViec":
                    {
                        ucCheDoLamViec tmp = new ucCheDoLamViec();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuCheDoChamCongNhanVien":
                    {
                        ucCheDoChamCongNhanVien tmp = new ucCheDoChamCongNhanVien();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuCongNhanKip":
                    {
                        ucDSCongNhanKip tmp = new ucDSCongNhanKip();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuKhongTinhPhep":
                    {
                        ucKhongTinhPhep tmp = new ucKhongTinhPhep();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuDangKiCaDem":
                    {
                        ucDangKiCaDem tmp = new ucDangKiCaDem();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuDKChamTuDong":
                    {
                        ucDangKiChamTuDong tmp = new ucDangKiChamTuDong();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuDKKhongTinhChuyenCan":
                    {
                        ucDangKiKhongTinhChuyenCan tmp = new ucDangKiKhongTinhChuyenCan();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuMaTheChamCong":
                    {
                        ucMaTheChamCong tmp = new ucMaTheChamCong();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuKeHoachDiCa":
                    {
                        ucKeHoachDiCa tmp = new ucKeHoachDiCa();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuVachTheLoi":
                    {
                        //frmVachTheLoi tmp = new frmVachTheLoi();
                        //panel2.Controls.Clear();
                        //panel2.Controls.Add(tmp);
                        //tmp.Dock = DockStyle.Fill;
                        //break;
                        ucVachTheLoi tmp = new ucVachTheLoi();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuCapNhatGio":
                    {
                        ucCapNhatGio tmp = new ucCapNhatGio();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuViPhamQuetThe":
                    {
                        ucViPhamQuetThe tmp = new ucViPhamQuetThe();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuLinkDuLieuChamCong":
                    {
                        frmLinkDuLieuChamCong tmp = new frmLinkDuLieuChamCong();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuTinhPhepThang":
                    {
                        ucPhepThang tmp = new ucPhepThang();
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
        private void ucListChamCong_Load(object sender, EventArgs e)
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
