using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DoAnQuanLyShopQuanAo
{
    public partial class FormThanhToan : Form
    {
        XuLyDuLieu xldl = new XuLyDuLieu("QL_SHOPQUANAO");
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataColumn[] primarykey = new DataColumn[1];
        DataColumn[] primarykey1 = new DataColumn[1];
        DataColumn[] primarykey2 = new DataColumn[2];
        DataColumn[] primarykey3 = new DataColumn[1];

        public FormThanhToan()
        {
            InitializeComponent();
        }

        public void createSanPham()
        {
            string caulenh = "select * from SANPHAM";
            adapter = xldl.getDataAdapter(caulenh, "SANPHAM");
            primarykey[0] = xldl.DataSet.Tables["SANPHAM"].Columns["MASANPHAM"];
            xldl.DataSet.Tables["SANPHAM"].PrimaryKey = primarykey;
        }

        public void createKhachHang()
        {
            string caulenh = "select * from KHACHHANG";
            adapter = xldl.getDataAdapter(caulenh, "KHACHHANG");
            primarykey3[0] = xldl.DataSet.Tables["KHACHHANG"].Columns["MAKHACHHANG"];
            xldl.DataSet.Tables["KHACHHANG"].PrimaryKey = primarykey3;
        }

        public void createHoaDon()
        {
            string caulenh = "select * from HOADON";
            adapter = xldl.getDataAdapter(caulenh, "HOADON");
            primarykey1[0] = xldl.DataSet.Tables["HOADON"].Columns["MAHD"];
            xldl.DataSet.Tables["HOADON"].PrimaryKey = primarykey1;
        }

        public void createCTHoaDon()
        {
            string caulenh = "select * from CHITIETHOADON";
            adapter = xldl.getDataAdapter(caulenh, "CHITIETHOADON");
            primarykey2[0] = xldl.DataSet.Tables["CHITIETHOADON"].Columns["MAHD"];
            primarykey2[1] = xldl.DataSet.Tables["CHITIETHOADON"].Columns["MASANPHAM"];
            xldl.DataSet.Tables["CHITIETHOADON"].PrimaryKey = primarykey2;
        }

        int tongtientraluu;

        private void FrmThanhToan_Load(object sender, EventArgs e)
        {
            createHoaDon();
            createCTHoaDon();
            createSanPham();
            createKhachHang();

            BanHangUC a = new BanHangUC();
            int tongThanhTien = 0;
            string mahoadon = ThongTin.LayThongTinThanhTien();
            lbMaHoaDon.Text = mahoadon;

            string tongthanhtien2 = "SELECT * FROM CHITIETHOADON WHERE MAHD = N'" + mahoadon + "'";

            SqlDataReader thanhtiendr2 = xldl.getDataReader(tongthanhtien2);
            while (thanhtiendr2.Read())
            {
                tongThanhTien += int.Parse(thanhtiendr2["THANHTIEN"].ToString());
            }
            thanhtiendr2.Close();
            xldl.dọngKetNoi();
            tongtientraluu = tongThanhTien;
            lbTong.Text = String.Format("{0:0,0}", tongThanhTien) + " VNĐ";

            string tenkhachang = "SELECT * FROM HOADON WHERE MAHD = N'" + mahoadon + "'";

            string idten = "";
            string idtennv = "";
            string thoigian = "";
            SqlDataReader dr = xldl.getDataReader(tenkhachang);
            while (dr.Read())
            {
                thoigian = dr["NGAYLAP"].ToString();
                idten = dr["MAKHACHHANG"].ToString();
                idtennv = dr["ID"].ToString();
            }
            dr.Close();
            xldl.dọngKetNoi();

            string tenkh = "";
            string tennv = "";
            string laytenkh = "SELECT * FROM KHACHHANG WHERE MAKHACHHANG = '" + idten + "'";
            SqlDataReader dr1 = xldl.getDataReader(laytenkh);
            while (dr1.Read())
            {
                tenkh = dr1["TENKHACHHANG"].ToString();
            }
            dr1.Close();
            xldl.dọngKetNoi();

            string laytennv = "SELECT * FROM NHANVIEN WHERE ID = '" + idtennv + "'";
            SqlDataReader dr2 = xldl.getDataReader(laytennv);
            while (dr2.Read())
            {
                tennv = dr2["HOTEN"].ToString();

            }
            dr2.Close();
            xldl.dọngKetNoi();
            ThongTin.LuuThongTintg(thoigian);
            ThongTin.LuuThongTinTenNV(tennv);
            lbTenKhachHang.Text = tenkh;
            ThongTin.LuuThongTintenKH(tenkh);
            label7.Text = "Nhân viên bán hàng: " + tennv;
            label8.Text = "Thời gian: "+ thoigian;
        }

        private void txtTienCuaKhach_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) button1_Click(sender, e);
        }

        private void txtTienCuaKhach_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int tongtientra = 0;
                int tien = 0;
                tien = int.Parse(txtTienCuaKhach.Text);
                tongtientra = tien - tongtientraluu;

                if (tien < tongtientraluu)
                {
                    MessageBox.Show("Tiền khách đưa không đủ");
                    return;
                }
                ThongTin.LuuThongTienthoi(tongtientra);
                lbTraTienThua.Text = String.Format("{0:0,0.0}", tongtientra) + " VND";
            }
            catch { MessageBox.Show("Lỗi vui lòng thử lại"); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormHoaDon a = new FormHoaDon();
            a.ShowDialog();
        }
    }
}
