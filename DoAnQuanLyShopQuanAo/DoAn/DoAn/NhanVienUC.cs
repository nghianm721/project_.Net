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
    public partial class NhanVienUC : UserControl
    {
        XuLyDuLieu xldl = new XuLyDuLieu("QL_SHOPQUANAO");
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataColumn[] primarykey = new DataColumn[1];

        public NhanVienUC()
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
        
        private void Load_Combobox_GioiTinh()
        {
            string caulenh = "SELECT * FROM GIOI";
            DataTable dt = xldl.getDataTable(caulenh, "GIOI");
            cboGioiTinh.DataSource = dt;
            cboGioiTinh.DisplayMember = "GIOITINH";
            cboGioiTinh.ValueMember = "GIOITINH";
            cboGioiTinh.SelectedIndex = 0;
        }

        private void Load_Combobox_Quyen()
        {
            string caulenh = "SELECT * FROM QUYEN";
            DataTable dt = xldl.getDataTable(caulenh, "QUYEN");
            cboQuyen.DataSource = dt;
            cboQuyen.DisplayMember = "ADMIN";
            cboQuyen.ValueMember = "ADMIN";
            cboQuyen.SelectedIndex = 0;
        }

        public bool ktNgaySinhHopLe()
        {
            if (txtNgaySinh.Text!="")
            {
                try
                {
                    DateTime dateOfBirth = DateTime.ParseExact(txtNgaySinh.Text, "dd/MM/yyyy", null);
                    int tuoi = DateTime.Now.Year - dateOfBirth.Year;
                    if (tuoi < 18)
                    {
                        MessageBox.Show("Tuổi của bạn là: " + tuoi + " bạn chưa đủ tuổi!");
                        txtNgaySinh.ResetText();
                        txtNgaySinh.Focus();
                        return false;
                    }
                }
                catch
                {
                    MessageBox.Show("ngày sinh chưa hợp lệ!");
                    txtNgaySinh.ResetText();
                    txtNgaySinh.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Nhập ngày sinh dd/MM/yyyy!");
                txtNgaySinh.ResetText();
                txtNgaySinh.Focus();
                return false;
            }
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaNhanVien.Text == "" || txtTenNhanVien.Text == "" || txtTaiKhoan.Text == "" || txtMatKhau.Text == ""
               || txtEmail.Text == "" || txtDiaChi.Text == "" || txtDienThoai.Text == "")
                {
                    MessageBox.Show("Không được để trống");
                }
                else
                {
                    string manhanvien = txtMaNhanVien.Text.Trim();
                    string tenhanvien = txtTenNhanVien.Text.Trim();
                    string taikhoan = txtTaiKhoan.Text.Trim();
                    string matkhau = txtMatKhau.Text.Trim();
                    string gioitinh = cboGioiTinh.SelectedValue.ToString().Trim();
                    string email = txtEmail.Text.Trim();
                    string diachi = txtDiaChi.Text.Trim();
                    string tenhinh = txtTenHinh.Text.Trim();
                    string sdt = txtDienThoai.Text.Trim();
                    string quyen = cboQuyen.SelectedValue.ToString().Trim();
                    string ngaysinh = DateTime.ParseExact(txtNgaySinh.Text, "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                    string caulenh1 = "SELECT COUNT(*) FROM NHANVIEN WHERE ID = '" + manhanvien + "'";
                    string caulenh2 = "SELECT COUNT(*) FROM NHANVIEN WHERE UserName = '" + taikhoan + "'";

                    bool kq = xldl.kiemTraTrung(caulenh1);
                    bool kq2 = xldl.kiemTraTrung(caulenh2);
                    bool kq_tuoi = ktNgaySinhHopLe();

                    if (kq_tuoi == false)
                    {
                        return;
                    }
                    bool ktemail = xldl.kiemTraEmail(email);
                    if (ktemail == false)
                    {
                        MessageBox.Show("Định dạng email sai vui lòng nhập lại");
                        return;
                    }
                    if (kq == true)
                    {
                        MessageBox.Show("Đã tồn tại mã nhân viên này: " + manhanvien);
                        return;
                    }
                    if (kq2 == true)
                    {
                        MessageBox.Show("Đã tồn tại tên tài khoản này: " + taikhoan);
                        return;
                    }

                    caulenh1 = "INSERT NHANVIEN VALUES('" + manhanvien + "',N'" + tenhanvien + "','" + taikhoan + "','" + matkhau + "',N'" + gioitinh + "','" + ngaysinh + "','" + email + "','" + diachi + "','" + sdt + "','" + tenhinh + "','" + quyen + "')";

                    xldl.CapNhatDB(caulenh1);
                    MessageBox.Show("Thêm thành công!");

                    loadLaiData();
                }
                
            }
            catch
            {
                MessageBox.Show("Thất bại");
            }
        }

        private void loadLaiData() 
        {
            string loadLaiDuLieu = "SELECT * FROM NHANVIEN";
            dGVNhanVien.DataSource = xldl.LoadData(loadLaiDuLieu);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string manhanvien = txtMaNhanVien.Text.Trim();
                string caulenh = "SELECT COUNT(*) FROM NHANVIEN WHERE ID = '" + manhanvien + "'";

                bool kq = xldl.kiemTraTrung(caulenh);

                if (kq == false)
                {
                    MessageBox.Show("Không tồn tại mã nhân viên này: " + manhanvien);
                    return;
                }
                caulenh = "DELETE NHANVIEN WHERE ID = '" + manhanvien + "'";
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
                string taikhoan = txtTaiKhoan.Text.Trim();
                string matkhau = txtMatKhau.Text.Trim();
                string gioitinh = cboGioiTinh.SelectedValue.ToString().Trim();
                string email = txtEmail.Text.Trim();
                string diachi = txtDiaChi.Text.Trim();
                string tenhinh = txtTenHinh.Text.Trim();
                string sdt = txtDienThoai.Text.Trim();
                string quyen = cboQuyen.SelectedValue.ToString().Trim();
                string ngaysinh = DateTime.ParseExact(txtNgaySinh.Text, "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                string caulenh = "SELECT COUNT(*) FROM NHANVIEN WHERE ID = '" + manhanvien + "'";

                bool kq = xldl.kiemTraTrung(caulenh);

                if (kq == false)
                {
                    MessageBox.Show("Không tồn tại mã nhân viên này: " + manhanvien);
                    return;
                }

                caulenh = "UPDATE NHANVIEN SET HOTEN = N'" + tenhanvien + "', UserName = '" + taikhoan + "',PassWord = '" + matkhau + "',GIOITINH = N'" + gioitinh + "',NGAYSINH = '" + ngaysinh + "',EMAIL = '" + email + "',DIACHI = '" + diachi + "',SODIENTHOAI = '" + sdt + "',Image = '" + tenhinh + "', ADMIN = '" + quyen + "' WHERE ID = '" + manhanvien + "'";
                xldl.CapNhatDB(caulenh);
                MessageBox.Show("Sửa thành công!");

                loadLaiData();
            }
            catch
            {
                MessageBox.Show("Thất bại");
            }
           
        }
        
        string paths = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);

        private void btnUpHinh_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = "C:\\";
            open.Filter = "Image File (*.jpg)|*.jpg|All File (*.*)|*.*";
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string name = System.IO.Path.GetFileName(open.FileName);
                string luu = paths + "\\img\\" + name;
                try
                {
                    FileStream fs = new FileStream(open.FileName, FileMode.Open, FileAccess.Read);
                    System.IO.File.Copy(open.FileName, luu);
                    MessageBox.Show("Upload file ảnh thành công", "Thông báo");
                    txtTenHinh.Text = name;
                    picHinh.Image = System.Drawing.Image.FromStream(fs);
                    fs.Close();

                }
                catch
                {
                    MessageBox.Show("Hình ảnh đã tồn tại hoặc trùng tên, vui lòng kiểm tra lại");
                }
            }
        }

        private void loadDuLieu()
        {
            dGVNhanVien.DataSource = xldl.DataSet.Tables["NHANVIEN"];
        }

        private void UserControlNhanVien_Load(object sender, EventArgs e)
        {
            this.dGVNhanVien.DefaultCellStyle.ForeColor = Color.Black;
            createNhanVien();
            Load_Combobox_GioiTinh();
            Load_Combobox_Quyen();
           loadDuLieu();
        }

        private void dGVNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (e.RowIndex == -1) return;

            txtMaNhanVien.Text = dGVNhanVien.Rows[index].Cells[0].Value.ToString();
            txtTenNhanVien.Text = dGVNhanVien.Rows[index].Cells[1].Value.ToString();
            txtTaiKhoan.Text = dGVNhanVien.Rows[index].Cells[2].Value.ToString();
            txtMatKhau.Text = dGVNhanVien.Rows[index].Cells[3].Value.ToString();
            cboGioiTinh.Text = dGVNhanVien.Rows[index].Cells[4].Value.ToString();
            txtNgaySinh.Text = dGVNhanVien.Rows[index].Cells[5].Value.ToString();
            txtEmail.Text = dGVNhanVien.Rows[index].Cells[6].Value.ToString();
            txtDiaChi.Text = dGVNhanVien.Rows[index].Cells[7].Value.ToString();
            txtDienThoai.Text = dGVNhanVien.Rows[index].Cells[8].Value.ToString();
            txtTenHinh.Text = dGVNhanVien.Rows[index].Cells[9].Value.ToString();
            cboQuyen.Text = dGVNhanVien.Rows[index].Cells[10].Value.ToString();
            try
            {
                if (txtTenHinh.Text != " " && txtTenHinh.Text != "" && txtTenHinh.Text != null)
                {
                    string url = paths + "\\img\\" + txtTenHinh.Text;
                    picHinh.Image = Image.FromFile(url);
                    FileStream fs = new FileStream(url, FileMode.Open, FileAccess.Read);
                    picHinh.Image = System.Drawing.Image.FromStream(fs);
                    fs.Close();
                }
                else
                {
                    picHinh.Image = Image.FromFile(paths + "\\img\\no-image-available-icon-6.jpg");
                }
            }
            catch
            {
                picHinh.Image = Image.FromFile(paths + "\\img\\no-image-available-icon-6.jpg");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (xldl.searchNhanVien(txtTimKiem.Text) != null)
            {
                dGVNhanVien.DataSource = xldl.searchNhanVien(txtTimKiem.Text);

            }
            else MessageBox.Show("Không tìm thấy nhân viên");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loadDuLieu();
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            txtMaNhanVien.Enabled = true;
            txtMaNhanVien.Text = "";
            txtTenNhanVien.Text = "";
            txtTaiKhoan.Text = "";
            txtMatKhau.Text = "";
            txtNgaySinh.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
            txtTenHinh.Text = "";
            txtMaNhanVien.Focus();
        }
    }
}
