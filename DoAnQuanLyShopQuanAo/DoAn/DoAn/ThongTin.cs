using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnQuanLyShopQuanAo
{
    class ThongTin
    {
        static string user = "";
        static string id = "";
        static string thanhtien = "";
        static string tenNV = "";
        static string tenKH = "";
        static string tg = "";
        static int Tienthoi = 0;

        public static void LuuThongTinDangNhap(string _user)
        {
            user = _user;
        }
        public static string LayThongTinDangNhap()
        {
            return user;
        }
        public static void LuuThongTinID(string _ID)
        {
            id = _ID;
        }
        public static string LayThongTinID()
        {
            return id;
        }
        public static void LuuThongTinThanhTien(string _thanhtien)
        {
            thanhtien = _thanhtien;
        }
        public static string LayThongTinThanhTien()
        {
            return thanhtien;
        }
        public static void LuuThongTinTenNV(string _tenNV)
        {
            tenNV = _tenNV;
        }
        public static string LayThongTinTenNV()
        {
            return tenNV;
        }
        public static void LuuThongTintenKH(string _tenKH)
        {
            tenKH = _tenKH;
        }
        public static string LayThongTintenKH()
        {
            return tenKH;
        }
        public static void LuuThongTintg(string _tg)
        {
            tg = _tg;
        }
        public static string LayThongTintg()
        {
            return tg;
        }
        public static void LuuThongTienthoi(int _Tienthoi)
        {
            Tienthoi = _Tienthoi;
        }
        public static int LayThongTienthoi()
        {
            return Tienthoi;
        }
    }
}
