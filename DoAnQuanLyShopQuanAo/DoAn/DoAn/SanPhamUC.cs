using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DoAnQuanLyShopQuanAo
{
    public partial class SanPhamUC : UserControl
    {
        XuLyDuLieu xldl = new XuLyDuLieu("QL_SHOPQUANAO");
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataColumn[] primarykey = new DataColumn[1];

        public SanPhamUC()
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

        private void Load_Combobox_LoaiSanPham()
        {
            string caulenh = "SELECT * FROM LOAISANPHAM";
            DataTable dt = xldl.getDataTable(caulenh, "LOAISANPHAM");
            cboLoaiSanPham.DataSource = dt;
            cboLoaiSanPham.DisplayMember = "TENLOAI";
            cboLoaiSanPham.ValueMember = "MALOAI";
            cboLoaiSanPham.SelectedIndex = 0;
        }

        private void Load_Combobox_ThuongHieu()
        {
            string caulenh = "SELECT * FROM THUONGHIEU";
            DataTable dt = xldl.getDataTable(caulenh, "THUONGHIEU");
            cboThuongHieu.DataSource = dt;
            cboThuongHieu.DisplayMember = "TENTHUONGHIEU";
            cboThuongHieu.ValueMember = "MATHUONGHIEU";
            cboThuongHieu.SelectedIndex = 0;
        }

        private void loadLaiData()
        {
            string loadLaiDuLieu = "SELECT * FROM SANPHAM";
            dGVSanPham.DataSource = xldl.LoadData(loadLaiDuLieu);
        }

        private void UserControlSanPham_Load(object sender, EventArgs e)
        {
            this.dGVSanPham.DefaultCellStyle.ForeColor = Color.Black;
            createSanPham();
            Load_Combobox_LoaiSanPham();
            Load_Combobox_ThuongHieu();
            loadDuLieu();
        }

        string paths = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);

        private void btnUpHinh_Click(object sender, System.EventArgs e)
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
            dGVSanPham.DataSource = xldl.DataSet.Tables["SANPHAM"];
        }

        private void dGVSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (e.RowIndex == -1) return;

            txtMaSanPham.Text = dGVSanPham.Rows[index].Cells[0].Value.ToString();
            txtTenSanPham.Text = dGVSanPham.Rows[index].Cells[1].Value.ToString();
            cboLoaiSanPham.Text = dGVSanPham.Rows[index].Cells[2].Value.ToString();
            cboThuongHieu.Text = dGVSanPham.Rows[index].Cells[3].Value.ToString();
            txtDonGia.Text = dGVSanPham.Rows[index].Cells[4].Value.ToString();
            txtSoLuongSP.Text = dGVSanPham.Rows[index].Cells[5].Value.ToString();
            txtLoiNhuan.Text = dGVSanPham.Rows[index].Cells[6].Value.ToString();
            txtMoTa.Text = dGVSanPham.Rows[index].Cells[7].Value.ToString();
            txtNgayCapNhat.Text = dGVSanPham.Rows[index].Cells[8].Value.ToString();
            txtTenHinh.Text = dGVSanPham.Rows[index].Cells[9].Value.ToString();

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

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (xldl.searchSanPham(txtTimKiem.Text) != null)
            {
                dGVSanPham.DataSource = xldl.searchSanPham(txtTimKiem.Text);
            }
            else MessageBox.Show("Không tìm thấy sản phầm này!");
        }

        private void BtnTroLai_Click(object sender, EventArgs e)
        {
            loadDuLieu();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaSanPham.Enabled = true;
            txtTenSanPham.Text = "";
            txtDonGia.Text = "";
            txtMoTa.Text = "";
            txtTenHinh.Text = "";
            txtSoLuongSP.Text = "";
            txtLoiNhuan.Text = "";
            txtMaSanPham.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaSanPham.Text == "" || txtTenSanPham.Text == "" || txtDonGia.Text == "" || txtMoTa.Text == "" || txtSoLuongSP.Text == "")
                {
                    MessageBox.Show("Không được để trống");
                }
                else
                {
                    string masanpham = txtMaSanPham.Text.Trim();
                    string tensanpham = txtTenSanPham.Text.Trim();
                    int dongia = int.Parse(txtDonGia.Text.Trim());
                    string mota = txtMoTa.Text.Trim();
                    string loaisanpham = cboLoaiSanPham.SelectedValue.ToString().Trim();
                    string thuonghieu = cboThuongHieu.SelectedValue.ToString().Trim();
                    string tenhinh = txtTenHinh.Text.Trim();
                    int soluong = int.Parse(txtSoLuongSP.Text.Trim());
                    float loi = float.Parse(txtLoiNhuan.Text.Trim());
                    string ngaycapnhat = DateTime.ParseExact(txtNgayCapNhat.Text, "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                    string caulenh = "SELECT COUNT(*) FROM SANPHAM WHERE MASANPHAM = '" + masanpham + "'";

                    bool kq = xldl.kiemTraTrung(caulenh);
                   
                    if (kq == true)
                    {
                        MessageBox.Show("Mã sản phẩm này đã tồn tại : " + masanpham);
                        return;
                    }

                    caulenh = "INSERT SANPHAM VALUES('" + masanpham + "',N'" + tensanpham + "','" + loaisanpham + "','" + thuonghieu + "'," + dongia + "," + soluong + ",'" + loi + "',N'" + mota + "','" + ngaycapnhat + "','" + tenhinh + "')";

                    xldl.CapNhatDB(caulenh);
                    loadLaiData();

                    MessageBox.Show("Thêm thành công!");
                }

            }
            catch
            {
                MessageBox.Show("Thất bại!");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string masanpham = txtMaSanPham.Text.Trim();
                string caulenh = "SELECT COUNT(*) FROM SANPHAM WHERE MASANPHAM = '" + masanpham + "'";

                bool kq = xldl.kiemTraTrung(caulenh);

                if (kq == false)
                {
                    MessageBox.Show("Mã sản phẩm này không tồn tại : " + masanpham);
                    return;
                }

                caulenh = "DELETE SANPHAM WHERE MASANPHAM = '" + masanpham + "'";
                xldl.CapNhatDB(caulenh);
                MessageBox.Show("Xóa thành công!");

                loadLaiData();
            }
            catch
            {
                MessageBox.Show("Xóa thất bại");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string masanpham = txtMaSanPham.Text.Trim();
                string tensanpham = txtTenSanPham.Text.Trim();
                int dongia = int.Parse(txtDonGia.Text.Trim());
                string mota = txtMoTa.Text.Trim();
                int soluong = int.Parse(txtSoLuongSP.Text.Trim());
                float loi = float.Parse(txtLoiNhuan.Text.Trim());
                string loaisanpham = cboLoaiSanPham.SelectedValue.ToString().Trim();
                string thuonghieu = cboThuongHieu.SelectedValue.ToString().Trim();
                string tenhinh = txtTenHinh.Text.Trim();
                string ngaycapnhat = DateTime.ParseExact(txtNgayCapNhat.Text, "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                string caulenh = "SELECT COUNT(*) FROM SANPHAM WHERE MASANPHAM = '" + masanpham + "'";

                bool kq = xldl.kiemTraTrung(caulenh);

                if (kq == false)
                {
                    MessageBox.Show("Mã sản phầm này không tồn tại: " + masanpham);
                    return;
                }

                caulenh = "UPDATE SANPHAM SET TENSANPHAM = N'" + tensanpham + "', MALOAI = '" + loaisanpham + "',MATHUONGHIEU = '" + thuonghieu + "',DONGIA = " + dongia + ",SOLUONGSP = " + soluong + ",LOINHUAN = '" + loi + "',MOTA = N'" + mota + "',NGAYCAPNHAT = '" + ngaycapnhat + "',HINHANH = '" + tenhinh + "' WHERE MASANPHAM = '" + masanpham + "'";
                xldl.CapNhatDB(caulenh);
                loadLaiData();
                MessageBox.Show("Sửa thành công!");
            }
            catch
            {
                MessageBox.Show("Sửa thất bại!");
            }
        }
    }
}
