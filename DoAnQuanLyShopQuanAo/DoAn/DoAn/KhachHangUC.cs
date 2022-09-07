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
    public partial class KhachHangUC : UserControl
    {
        XuLyDuLieu xldl = new XuLyDuLieu("QL_SHOPQUANAO");
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataColumn[] primarykey = new DataColumn[1];

        public KhachHangUC()
        {
            InitializeComponent();
        }
        public void createKH()
        {
            string caulenh = "select * from KHACHHANG";
            adapter = xldl.getDataAdapter(caulenh, "KHACHHANG");
            primarykey[0] = xldl.DataSet.Tables["KHACHHANG"].Columns["MAKHACHHANG"];
            xldl.DataSet.Tables["KHACHHANG"].PrimaryKey = primarykey;
        }

        private void Load_Combobox_GioiTinh()
        {
            string caulenh = "SELECT * FROM GIOI";
            DataTable dt = xldl.getDataTable(caulenh, "GIOI");
            cboGioiTinh.DataSource = dt;
            cboGioiTinh.DisplayMember = "GIOITINH";
            cboGioiTinh.ValueMember = "GIOITINH";

            cboGioiTinh.SelectedIndex = 0;
        }
        private void loadLaiData()
        {
            string loadLaiDuLieu = "SELECT * FROM KHACHHANG";
            dGVNhanVien.DataSource = xldl.LoadData(loadLaiDuLieu);
        }

        private void UserControlKhachHang_Load(object sender, EventArgs e)
        {
            this.dGVNhanVien.DefaultCellStyle.ForeColor = Color.Black;
            createKH();
            Load_Combobox_GioiTinh();
            loadLaiData();
        }

        private void dGVNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (e.RowIndex == -1) return;
            txtMaNhanVien.Text = dGVNhanVien.Rows[index].Cells[0].Value.ToString();
            txtTenNhanVien.Text = dGVNhanVien.Rows[index].Cells[1].Value.ToString();
            cboGioiTinh.Text = dGVNhanVien.Rows[index].Cells[3].Value.ToString();
            txtNgaySinh.Text = dGVNhanVien.Rows[index].Cells[4].Value.ToString();
            txtDiaChi.Text = dGVNhanVien.Rows[index].Cells[5].Value.ToString();
            txtDienThoai.Text = dGVNhanVien.Rows[index].Cells[2].Value.ToString();
          
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
           
            txtMaNhanVien.Text = "Tự Động";
            txtTenNhanVien.Text = "";
            txtNgaySinh.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaNhanVien.Text == "" || txtTenNhanVien.Text == "" || txtDiaChi.Text == "" || txtDienThoai.Text == "")
                {
                    MessageBox.Show("Không được để trống");
                }
                else
                {
                    string manhanvien = txtMaNhanVien.Text.Trim();
                    string tenhanvien = txtTenNhanVien.Text.Trim();
                    string gioitinh = cboGioiTinh.SelectedValue.ToString().Trim();
                    string diachi = txtDiaChi.Text.Trim();
                    string sdt = txtDienThoai.Text.Trim();
                    string ngaysinh = DateTime.ParseExact(txtNgaySinh.Text, "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                    string caulenh = "INSERT KHACHHANG VALUES(N'" + tenhanvien + "','" + sdt + "',N'" + gioitinh + "','" + ngaysinh + "',N'" + diachi + "')";

                    xldl.CapNhatDB(caulenh);
                    loadLaiData();

                    MessageBox.Show("Thêm thành công!");
                }

            }
            catch
            {
                MessageBox.Show("Thất bại");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string manhanvien = txtMaNhanVien.Text.Trim();
                string caulenh = "SELECT COUNT(*) FROM KHACHHANG WHERE MAKHACHHANG = '" + manhanvien + "'";

                bool kq = xldl.kiemTraTrung(caulenh);

                if (kq == false)
                {
                    MessageBox.Show("Không tồn tại mã này: " + manhanvien);
                    return;
                }
                caulenh = "DELETE KHACHHANG WHERE MAKHACHHANG = '" + manhanvien + "'";
                xldl.CapNhatDB(caulenh);
                MessageBox.Show("Xóa thành công!");

                loadLaiData();
            }
            catch
            {
                MessageBox.Show("Thất bại");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string manhanvien = txtMaNhanVien.Text.Trim();
                string tenhanvien = txtTenNhanVien.Text.Trim();
                string gioitinh = cboGioiTinh.SelectedValue.ToString().Trim();
                string diachi = txtDiaChi.Text.Trim();
                string sdt = txtDienThoai.Text.Trim();
                string ngaysinh = DateTime.ParseExact(txtNgaySinh.Text, "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                string caulenh = "SELECT COUNT(*) FROM KHACHHANG WHERE MAKHACHHANG = '" + manhanvien + "'";

                bool kq = xldl.kiemTraTrung(caulenh);

                if (kq == false)
                {
                    MessageBox.Show("Không tồn tại mã này: " + manhanvien);
                    return;
                }

                caulenh = "UPDATE KHACHHANG SET TENKHACHHANG = N'" + tenhanvien + "',GIOITINH = N'" + gioitinh + "',NGAYSINH = '" + ngaysinh + "',DIACHI = N'" + diachi + "',SDT = '" + sdt + "' WHERE MAKHACHHANG = '" + manhanvien + "'";
                xldl.CapNhatDB(caulenh);

                loadLaiData();
                MessageBox.Show("Sửa thành công!");
            }
            catch
            {
                MessageBox.Show("Thất bại");
            }
        }
    }
}
