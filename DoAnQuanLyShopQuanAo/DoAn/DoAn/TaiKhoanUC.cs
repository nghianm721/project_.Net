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
    public partial class TaiKhoanUC : UserControl
    {
        XuLyDuLieu xldl = new XuLyDuLieu("QL_SHOPQUANAO");
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataColumn[] primarykey = new DataColumn[1];

        public TaiKhoanUC()
        {
            InitializeComponent();
        }

        public void createNhanVien()
        {
            string caulenh = "select * from NHANVIEN";
            adapter = xldl.getDataAdapter(caulenh, "NHANVIEN");
            primarykey[0] = xldl.DataSet.Tables["NHANVIEN"].Columns["ID"];
            xldl.DataSet.Tables["NHANVIEN"].PrimaryKey = primarykey;
        }

        string paths = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
        string tendangnhap = "";
        string matkhau = "";
        string idnhanvien = ThongTin.LayThongTinID();

        private void UserControlHeThong_Load(object sender, EventArgs e)
        {
            string tennv = "";
            string gioitinh = "";
            string ngaysinh = "";
            string email = "";
            string diachi = "";
            string sdt = "";
            string hinh = "";
            createNhanVien();
            string nhanvienthongtin = "SELECT * FROM NHANVIEN WHERE ID = '" + idnhanvien + "'";

            SqlDataReader dr = xldl.getDataReader(nhanvienthongtin);
            while (dr.Read())
            {
                tendangnhap = dr["UserName"].ToString();
                matkhau = dr["PassWord"].ToString();
                tennv = dr["HOTEN"].ToString();
                gioitinh = dr["GIOITINH"].ToString();
                ngaysinh = dr["NGAYSINH"].ToString();
                email = dr["EMAIL"].ToString();
                diachi = dr["DIACHI"].ToString();
                sdt = dr["SODIENTHOAI"].ToString();
                hinh = dr["Image"].ToString();
            }
            dr.Close();
            xldl.dọngKetNoi();

            txtTenDN.Text = tendangnhap;
            txtMaNhanVien.Text = idnhanvien;
            txtTenNhanVien.Text = tennv;
            txtGioiTinh.Text = gioitinh;
            txtNgaySinh.Text = ngaysinh;
            txtDiaChi.Text = diachi;
            txtDienThoai.Text = sdt;
            txtEmail.Text = email;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtMKCu.Text == matkhau)
            {
                if (txtMKMoi.Text.Equals(txtXacNhanMK.Text) == true)
                {
                    try
                    {
                        string caulenh = "UPDATE NHANVIEN SET PassWord = '" + txtXacNhanMK.Text + "' WHERE ID = '" + idnhanvien + "'";
                        xldl.CapNhatDB(caulenh);

                        MessageBox.Show("Đổi mật khẩu thành công!");
                    }
                    catch
                    {
                        MessageBox.Show("Thất bại");
                    }
                }
                else
                {
                    MessageBox.Show("Mật khẩu nhập lại không chính xác vui lòng nhập lại");
                }
            }
            else
            {
                MessageBox.Show("Mật khẩu không chính xác vui lòng nhập lại");
            }
        }
    }
}
