using System;
using System.Collections.Generic;

namespace Commons
{
    public class Modules
    {

        private static string _ModuleName;
        private static string myHost = System.Net.Dns.GetHostName();

        public static List<string> lstControlName
        {
            get
            {
                return _lstControlName;
            }
            set
            {
                _lstControlName = value;
            }
        }

        // Private Shared _lstControlName As New List(Of String)(New String() {"LookUpEdit", "Label", "RadioButton", "CheckBox", "GroupBox", "TabPage", "LabelControl", "CheckButton", "CheckEdit", "XtraTabPage", "GroupControl", "Button", "SimpleButton", "RadioGroup", "CheckedListBoxControl", "XtraTabControl", "GridControl", "DataGridView", "DataGridViewNew", "DataGridViewEditor", "NavBarControl", "navBarControl", "TextEdit", "TextBox", "ComboBox", "ButtonEdit", "MemoEdit"}) '"DateEdit",

        private static List<string> _lstControlName = new List<string>(new string[] { "LookUpEdit", "RadioButton", "CheckBox", "GroupBox", "TabPage", "LabelControl", "CheckButton", "CheckEdit", "XtraTabPage", "GroupControl", "Button", "SimpleButton", "RadioGroup", "CheckedListBoxControl", "XtraTabControl", "GridControl", "DataGridView", "DataGridViewNew", "DataGridViewEditor", "NavBarControl", "navBarControl", "BarManager", "TextEdit", "tablePanel", "navigationFrame", "navigationPage", "LayoutControlGroup" });

        //dinh nghia ID cho cac form danh muc khi tra ve
        private static string _sId;
        public static string sId
        {
            get
            {
                return _sId;
            }
            set
            {
                _sId = value;
            }
        }



        private static string _sIdHT; // Định nghĩa ID cho các form hệ thống
        public static string sIdHT
        {
            get
            {
                return _sIdHT;
            }
            set
            {
                _sIdHT = value;
            }
        }

        private static bool _chamCongK;
        public static bool chamCongK
        {
            get
            {
                return _chamCongK;
            }
            set
            {
                _chamCongK = value;
            }
        }
        //SetUP, nếu setup = true thì được phép sửa lưới
        private static bool _bSetUp;
        public static bool bSetUp
        {
            get
            {
                return _bSetUp;
            }
            set
            {
                _bSetUp = value;
            }
        }
        private static bool _bEnabel;
        public static bool bEnabel
        {
            get
            {
                return _bEnabel;
            }
            set
            {
                _bEnabel = value;
            }
        }

        private static string _msgTitle;
        public static string msgTitle
        {
            get
            {
                return _msgTitle;
            }
            set
            {
                _msgTitle = value;
            }
        }
        //định nghĩa giờ làm mặc định
        private static double _iGio;
        public static double iGio
        {
            get
            {
                return _iGio;
            }
            set
            {
                _iGio = value;
            }
        }

        //định nghĩa license
        private static int _iLic = -1;
        public static int iLic
        {
            get
            {
                return _iLic;
            }
            set
            {
                _iLic = value;
            }
        }

        //định nghĩa id khách hàng
        private static int _iCustomerID;
        public static int iCustomerID
        {
            get
            {
                return _iCustomerID;
            }
            set
            {
                _iCustomerID = value;
            }
        }

        private static int _iLOAI_CN;
        public static int iLOAI_CN
        {
            get
            {
                return _iLOAI_CN;
            }
            set
            {
                _iLOAI_CN = value;
            }
        }


        //định nghĩa version PM
        private static string _sVersion;
        public static string sVersion
        {
            get
            {
                return _sVersion;
            }
            set
            {
                _sVersion = value;
            }
        }
     

        private static string _LicensePro;
        public static string LicensePro
        {
            get
            {
                return _LicensePro;
            }
            set
            {
                _LicensePro = value;
            }
        }

        private static string _sInfoSer;
        public static string sInfoSer
        {
            get
            {
                return _sInfoSer;
            }
            set
            {
                _sInfoSer = value;
            }
        }

        private static string _sInfoClient;
        public static string sInfoClient
        {
            get
            {
                return _sInfoClient;
            }
            set
            {
                _sInfoClient = value;
            }
        }

        

        //định nghĩa IP
        private static string _sIP;
        public static string sIP
        {
            get
            {
                return _sIP;
            }
            set
            {
                _sIP = value;
            }
        }

        //định nghĩa link api
        private static string _sUrlCheckServer;
        public static string sUrlCheckServer
        {
            get
            {
                return _sUrlCheckServer;
            }
            set
            {
                _sUrlCheckServer = value;
            }
        }

        private static string _sHideMenu;
        public static string sHideMenu
        {
            get
            {
                return _sHideMenu;
            }
            set
            {
                _sHideMenu = value;
            }
        }
        private static string _sDDTaiLieu;
        public static string sDDTaiLieu
        {
            get
            {
                return _sDDTaiLieu;
            }
            set
            {
                _sDDTaiLieu = value;
            }
        }

        //định nghĩa làm tròn giờ
        private static int _iLamTronGio;
        public static int iLamTronGio
        {
            get
            {
                return _iLamTronGio;
            }
            set
            {
                _iLamTronGio = value;
            }
        }

        //định nghĩa số giờ làm tăng ca trong ngày cho chấm công khách
        private static int _iSNNgay;
        public static int iSNNgay
        {
            get
            {
                return _iSNNgay;
            }
            set
            {
                _iSNNgay = value;
            }
        }

        //định nghĩa số giờ làm tăng ca trong tuần cho chấm công khách
        private static int _iSNTuan;
        public static int iSNTuan
        {
            get
            {
                return _iSNTuan;
            }
            set
            {
                _iSNTuan = value;
            }
        }

        //định nghĩa số giờ làm tăng ca trong tháng cho chấm công khách
        private static int _iSNThang;
        public static int iSNThang
        {
            get
            {
                return _iSNThang;
            }
            set
            {
                _iSNThang = value;
            }
        }

        //định nghĩa ngày nghĩ ngày làm việc(0:không nghĩ---------- 1: nghĩ chủ nhật, 2----- nghĩ thứ 7,chủ nhật)
        private static int _iNNghi;
        public static int iNNghi
        {
            get
            {
                return _iNNghi;
            }
            set
            {
                _iNNghi = value;
            }
        }
        //dinh nghia cau store dung cho toan bo danh muc
        private static string _sPS;
        public static string sPS
        {
            get
            {
                return _sPS;
            }
            set
            {
                _sPS = value;
            }
        }
        private static Int64 _iCongNhan;
        public static Int64 iCongNhan
        {
            get
            {
                return _iCongNhan;
            }
            set
            {
                _iCongNhan = value;
            }
        }

        private static Int64 _iUngVien;
        public static Int64 iUngVien
        {
            get
            {
                return _iUngVien;
            }
            set
            {
                _iUngVien = value;
            }
        }

        //dinh nghia phan quyen
        //dtTempt.Rows.Add(1, "Full access");
        //dtTempt.Rows.Add(2, "Read Only");
        private static int _iPermission;
        public static int iPermission
        {
            get
            {
                return _iPermission;
            }
            set
            {
                _iPermission = value;
            }
        }

        //cham cong 
        private static Boolean _bolLinkCC;
        public static Boolean bolLinkCC
        {
            get
            {
                return _bolLinkCC;
            }
            set
            {
                _bolLinkCC = value;
            }
        }

        private static DateTime _dLinkCC;
        public static DateTime dLinkCC
        {
            get
            {
                return _dLinkCC;
            }
            set
            {
                _dLinkCC = value;
            }
        }

        //iLink = 1 load txt
        //iLink = 2 load access
        //iLink = 3 Load csdl

        private static int _iLink;
        public static int iLink
        {
            get
            {
                return _iLink;
            }
            set
            {
                _iLink = value;
            }
        }
        private static string _connect;
        public static string connect
        {
            get
            {
                return _connect;
            }
            set
            {
                _connect = value;
            }
        }


        public static string ModuleName
        {
            get
            {
                return _ModuleName;
            }
            set
            {
                _ModuleName = value;
            }
        }
        
        private static string _UserName = string.Empty;
        public static string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
            }
        }

        private static Int64 _iIDUser = 0;
        public static Int64 iIDUser
        {
            get
            {
                return _iIDUser;
            }
            set
            {
                _iIDUser = value;
            }
        }

        private static Int64 _iIDNhom = 0;
        public static Int64 iIDNhom
        {
            get
            {
                return _iIDNhom;
            }
            set
            {
                _iIDNhom = value;
            }
        }


        private static int _TypeLanguage;
        public static int TypeLanguage
        {
            get
            {
                return _TypeLanguage;
            }
            set
            {
                _TypeLanguage = value;
            }
        }
        private static OSystems _OSystems = new OSystems();
        public static OSystems ObjSystems
        {
            get
            {
                return _OSystems;
            }
            set
            {
                _OSystems = value;
            }
        }

        private static MExcel _MExcel = new MExcel();
        public static MExcel MExcel
        {
            get
            {
                return _MExcel;
            }
            set
            {
                _MExcel = value;
            }
        }

        private static bool _ChangLanguage;
        public static bool ChangLanguage
        {
            get
            {
                return _ChangLanguage;
            }
            set
            {
                _ChangLanguage = value;
            }
        }


        private static OXtraGrid _OXtraGrid = new OXtraGrid();
        public static OXtraGrid OXtraGrid
        {
            get
            {
                return _OXtraGrid;
            }
            set
            {
                _OXtraGrid = value;
            }
        }

        private static OLanguages _OLanguages = new OLanguages();
        public static OLanguages ObjLanguages
        {
            get
            {
                return _OLanguages;
            }
            set
            {
                _OLanguages = value;
            }
        }
        
        // Xac dinh thong tin cong ty
        private static string _sPrivate;
        public static string sPrivate
        {
            get
            {
                return _sPrivate.ToUpper();
            }
            set
            {
                _sPrivate = value.ToUpper();
            }
        }

        private static int _iSoLeSL;
        public static int iSoLeSL
        {
            get
            {
                return _iSoLeSL;
            }
            set
            {
                _iSoLeSL = value;
            }
        }

        private static int _iSoLeDG;
        public static int iSoLeDG
        {
            get
            {
                return _iSoLeDG;
            }
            set
            {
                _iSoLeDG = value;
            }
        }
        
        private static int _iSoLeTT;
        public static int iSoLeTT
        {
            get
            {
                return _iSoLeTT;
            }
            set
            {
                _iSoLeTT = value;
            }
        }

        private static string _sSoLeSL;
        public static string sSoLeSL
        {
            get
            {
                return _sSoLeSL;
            }
            set
            {
                _sSoLeSL = value;
            }
        }

        private static string _sSoLeDG;
        public static string sSoLeDG
        {
            get
            {
                return _sSoLeDG;
            }
            set
            {
                _sSoLeDG = value;
            }
        }

        private static string _sSoLeTT;
        public static string sSoLeTT
        {
            get
            {
                return _sSoLeTT;
            }
            set
            {
                _sSoLeTT = value;
            }
        }
        
        private static string _sTenNhanVienMD;
        public static string sTenNhanVienMD
        {
            get
            {
                return _sTenNhanVienMD;
            }
            set
            {
                _sTenNhanVienMD = value;
            }
        }

        private static string _sMaNhanVienMD;
        public static string sMaNhanVienMD
        {
            get
            {
                return _sMaNhanVienMD;
            }
            set
            {
                _sMaNhanVienMD = value;
            }
        }

        private static string _sLoad;
        public static string sLoad
        {
            get
            {
                return _sLoad;
            }
            set
            {
                _sLoad = value;
            }
        }

    }
}
