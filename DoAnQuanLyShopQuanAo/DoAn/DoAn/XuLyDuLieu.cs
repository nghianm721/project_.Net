using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace DoAnQuanLyShopQuanAo
{
    public class XuLyDuLieu
    {
        SqlConnection cnn;

        string tennv;

        public string Tennv
        {
            get { return tennv; }
            set { tennv = value; }
        }

        string manv;

        public string Manv
        {
            get { return manv; }
            set { manv = value; }
        }
        string username;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        string password;

        public string PassWord
        {
            get { return password; }
            set { password = value; }
        }


        public SqlConnection Cnn
        {
            get { return cnn; }
            set { cnn = value; }
        }

        string caulenh;

        public string CauLenh
        {
            get { return caulenh; }
            set { caulenh = value; }
        }

        private DataSet dataSet;

        public DataSet DataSet
        {
            get { return dataSet; }
            set { dataSet = value; }
        }

        public XuLyDuLieu(string tenDataSet)
        {
            CauLenh = "Data Source= LAPTOP-NMNGHIA;Initial Catalog=QL_SHOPQUANAO;User ID=sa;Password=123";
            Cnn = new SqlConnection(caulenh);
            DataSet = new DataSet(tenDataSet);
        }
        // nếu đóng thì mở
        public void moKetNoi()
        {
            if (Cnn.State == ConnectionState.Closed)
                Cnn.Open();
        }
        //nếu mở thì đóng
        public void dọngKetNoi()
        {
            if (Cnn.State == ConnectionState.Open)
                Cnn.Close();
        }

        public void disposeKetNoi()
        {
            if (Cnn.State == ConnectionState.Open)
                Cnn.Close();
            Cnn.Dispose(); //giải phóng bộ nhớ
        }
        // cập nhật lại database
        public void CapNhatDB(string lenhSQL)
        {
            //mở
            moKetNoi();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = lenhSQL;
            cmd.Connection = Cnn;
            cmd.ExecuteNonQuery();

            //đóng
            dọngKetNoi();
        }
        //đếm các phần tử có trong database (kiểm tra trùng)
        public int kiemTraKhoaNgoai(string lenhSQL)
        {
            moKetNoi();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = lenhSQL;
            cmd.Connection = Cnn;
            int dem = (int)cmd.ExecuteScalar();
            //đếm

            //đóng
            dọngKetNoi();
            return dem;
        }
        //nến trùng thì đếm ở trên lớn hơn 0
        public bool kiemTraTrung(string lenhSQL)
        {
            int dem = kiemTraKhoaNgoai(lenhSQL);
            if (dem > 0)
                return true; //trùng
            return false; //không trùng
        }

        public SqlDataReader getDataReader(string lenhSQL)
        {
            moKetNoi();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = lenhSQL;
            cmd.Connection = Cnn;
            return cmd.ExecuteReader();
        }

        public DataTable getDataTable(string lenhSQL, string tenBang)
        {
            moKetNoi();
            SqlDataAdapter adapter = new SqlDataAdapter(lenhSQL, this.cnn);
            adapter.Fill(DataSet, tenBang);
            dọngKetNoi();
            return DataSet.Tables[tenBang];
        }

        public SqlDataAdapter getDataAdapter(string lenhSQL, string tenBang)
        {
            moKetNoi();
            SqlDataAdapter adater = new SqlDataAdapter(lenhSQL, this.cnn);
            adater.Fill(DataSet, tenBang);
            dọngKetNoi();
            return adater;
        }

        public bool CompareValue(string tenDataTable, string tenDataColume, string Value)
        {
            SqlCommand cmm;
            int dem = 0;
            moKetNoi();
            string lenhRequed = string.Format("select * from {0}", tenDataTable);
            cmm = new SqlCommand(lenhRequed, cnn);

            SqlDataReader reader = cmm.ExecuteReader();

            //So sánh giá trị với từng dòng truyền vào
            while (reader.Read())
            {
                //Kiểm tra sự tồn tại của giá trị trong cột
                if (reader[tenDataColume].ToString() == Value)
                    dem++;
            }

            cmm.Dispose();
            reader.Dispose();

            if (dem > 0)
                return true;
            else
                return false;
        }

        public bool kiemTraAdmin(string tenDataTable, string tenDataColume, string tenDataColume2, string tenCotAdmin, string username, string password)
        {
            SqlCommand cmm;
            int dem = 0;
            moKetNoi();
            string strReq = string.Format("select * from {0}", tenDataTable);
            cmm = new SqlCommand(strReq, cnn);

            SqlDataReader reader = cmm.ExecuteReader();

            //So sánh giá trị với từng dòng truyền vào
            while (reader.Read())
            {

                //Kiểm tra sự tồn tại của giá trị trong cột
                if (reader[tenDataColume].ToString() == username && reader[tenDataColume2].ToString() == password)
                    if (bool.Parse(reader[tenCotAdmin].ToString()) == true)
                        dem++;
            }

            cmm.Dispose();
            reader.Dispose();

            if (dem > 0)
                return true;
            else
                return false;
        }

        public DataTable searchNhanVien(string manv)
        {
            DataTable table = new DataTable();
            moKetNoi();
            string lenh = string.Format("SELECT * FROM NHANVIEN WHERE HOTEN like '%" + manv + "%'");
            SqlDataAdapter adapter = new SqlDataAdapter(lenh, this.cnn);
            adapter.Fill(table);

            dọngKetNoi();
            return table;
        }

        public DataTable searchSanPham(string masp)
        {
            DataTable table = new DataTable();
            moKetNoi();
            string lenh = string.Format("SELECT * FROM SANPHAM WHERE MASANPHAM = '" + masp + "'");
            SqlDataAdapter adapter = new SqlDataAdapter(lenh, this.cnn);
            adapter.Fill(table);

            dọngKetNoi();
            return table;
        }

        public DataTable searchHoaDon(string mahd)
        {
            DataTable table = new DataTable();
            moKetNoi();
            string lenh = string.Format("SELECT * FROM HOADON WHERE MAHD = '" + mahd + "'");
            SqlDataAdapter adapter = new SqlDataAdapter(lenh, this.cnn);
            adapter.Fill(table);

            dọngKetNoi();
            return table;
        }

        public DataTable searchPhieuNhap(string mapn)
        {
            DataTable table = new DataTable();
            moKetNoi();
            string lenh = string.Format("SELECT * FROM PHIEUNHAP WHERE MAPN = '" + mapn + "'");
            SqlDataAdapter adapter = new SqlDataAdapter(lenh, this.cnn);
            adapter.Fill(table);
            dọngKetNoi();
            return table;
        }

        public DataTable LoadData(string lenhSQL)
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(lenhSQL, this.cnn);
            adapter.Fill(table);
            return table;
        }

        public DataTable ChiTietHoaDon(string macthd)
        {
            string lenh = string.Format("SELECT * From CHITIETHOADON where MAHD ='" + macthd + "'");
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(lenh, this.cnn);
            adapter.Fill(table);
            return table;

        }

        public DataTable getChiTietHoaDon(string macthd)
        {
            return ChiTietHoaDon(macthd);
        }

        public DataTable ChiTietPhieuNhap(string mapn)
        {
            string lenh = string.Format("SELECT * From CHITIETPHIEUNHAP where MAPN ='" + mapn + "'");
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(lenh, this.cnn);
            adapter.Fill(table);
            return table;

        }

        public DataTable getChiTietPhieuNhap(string mapn)
        {
            return ChiTietPhieuNhap(mapn);
        }

        public bool kiemTraEmail(string pEmail)
        {
            pEmail = pEmail ?? string.Empty;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(pEmail))
                return (true);
            else
                return (false);
        }
    }
}
