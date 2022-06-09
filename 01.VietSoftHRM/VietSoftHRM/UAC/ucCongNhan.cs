using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Navigation;
using Vs.HRM;

namespace VietSoftHRM
{

    public partial class ucCongNhan : DevExpress.XtraEditors.XtraUserControl
    {
        public Color color;
        public int iLoai;
        public int iIDOut;
        public string slinkcha;
        public string sLoad="";
        public ucCongNhan(TileBar tileBar)
        {
            InitializeComponent();
        }
        //load tất danh mục từ menu
        private void LoadCongNhan()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetMenuLeft", Commons.Modules.UserName, Commons.Modules.TypeLanguage, iLoai));
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
                case "mnuKHNghiPhep":
                    {
                        if (!panel2.Controls.Contains(ucKeHoachNghiPhep.Instance))
                        {
                            panel2.Controls.Clear();
                            panel2.Controls.Add(ucKeHoachNghiPhep.Instance);
                            ucKeHoachNghiPhep.Instance.Dock = DockStyle.Fill;
                            ucKeHoachNghiPhep.Instance.BringToFront();
                        }
                        break;
                    }
                case "mnuDaoTao":
                    {
                        if (!panel2.Controls.Contains(ucDaoTao.Instance))
                        {
                            panel2.Controls.Clear();
                            panel2.Controls.Add(ucDaoTao.Instance);
                            ucDaoTao.Instance.Dock = DockStyle.Fill;
                            ucDaoTao.Instance.BringToFront();
                        }
                        break;
                    }
                case "mnuThoiViec":
                    {
                        ucQuyetDinhThoiViec thoiviec = new ucQuyetDinhThoiViec();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(thoiviec);
                        thoiviec.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuNhanSu":
                    {
                        ucQLNS ns = new ucQLNS();
                        ns.accorMenuleft = accorMenuleft;
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
                case "mnuKHNghiPhep":
                    {
                        //if (!panel2.Controls.Contains(ucKeHoachNghiPhep.Instance))
                        //{
                        //    panel2.Controls.Clear();
                        //    panel2.Controls.Add(ucKeHoachNghiPhep.Instance);
                        //    ucKeHoachNghiPhep.Instance.Dock = DockStyle.Fill;
                        //    ucKeHoachNghiPhep.Instance.BringToFront();
                        //}
                        //break;
                        ucKeHoachNghiPhep kehoachnghiphep = new ucKeHoachNghiPhep();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(kehoachnghiphep);
                        kehoachnghiphep.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuDaoTao":
                    {
                        //if (!panel2.Controls.Contains(ucDaoTao.Instance))
                        //{
                        //    panel2.Controls.Clear();
                        //    panel2.Controls.Add(ucDaoTao.Instance);
                        //    ucDaoTao.Instance.Dock = DockStyle.Fill;
                        //    ucDaoTao.Instance.BringToFront();
                        //}
                        //break;
                        ucDaoTao daotao = new ucDaoTao();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(daotao);
                        daotao.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuThoiViec":
                    {
                        ucQuyetDinhThoiViec thoiviec = new ucQuyetDinhThoiViec();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(thoiviec);
                        thoiviec.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuNhanSu":
                    {
                        ucQLNS ns = new ucQLNS();
                        
                        ns.accorMenuleft = accorMenuleft;
                        panel2.Controls.Clear();
                        panel2.Controls.Add(ns);
                        ns.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuTroCap":
                    {
                        ucTroCapBHXH tc = new ucTroCapBHXH();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tc);
                        tc.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuCapNhatLCB":
                    {
                        ucCapNhatLCB lcb = new ucCapNhatLCB();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(lcb);
                        lcb.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBaoHiemYTe":
                    {
                        ucBaoHiemYTe tc = new ucBaoHiemYTe();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tc);
                        tc.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuNgungBHXH":
                    {
                        ucNgungDongBHXH tc = new ucNgungDongBHXH();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tc);
                        tc.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuThamGiaBHXH":
                    {
                        ucThamGiaBHXH tc = new ucThamGiaBHXH();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tc);
                        tc.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuBHXHThang":
                    {
                        ucBHXHThang tc = new ucBHXHThang();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tc);
                        tc.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuXepLoaiKhenThuong":
                    {
                        ucXepLoaiKhenThuong tc = new ucXepLoaiKhenThuong();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tc);
                        tc.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuThuongKhacLuong":
                    {
                        ucThuongKhacLuong tc = new ucThuongKhacLuong();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tc);
                        tc.Dock = DockStyle.Fill;
                        break;
                    }
                case "mnuThuongXepLoai":
                    {
                        ucTienThuongXepLoai tc = new ucTienThuongXepLoai();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tc);
                        tc.Dock = DockStyle.Fill;
                        break;
                    }

                default:
                    break;
            }
            Commons.Modules.ObjSystems.HideWaitForm();
           // accorMenuleft.OptionsMinimizing.State = DevExpress.XtraBars.Navigation.AccordionControlState.Minimized;
        }
        private void ucCongNhan_Load(object sender, EventArgs e)
        {
            slinkcha = NONNlab_Link.Text;
            LoadCongNhan();
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
