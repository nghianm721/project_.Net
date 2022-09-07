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
    public partial class NhapHangUC : UserControl
    {
        public string tenNV = "";
        public string maNV = "";

        XuLyDuLieu xldl = new XuLyDuLieu("QL_SHOPQUANAO");
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataColumn[] primarykey = new DataColumn[1];
        DataColumn[] primarykey1 = new DataColumn[1];
        DataColumn[] primarykey2 = new DataColumn[2];
        DataColumn[] primarykey3 = new DataColumn[1];

        public NhapHangUC()
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

        public void createPhieuNhap()
        {
            string caulenh = "select * from PHIEUNHAP";
            adapter = xldl.getDataAdapter(caulenh, "PHIEUNHAP");
            primarykey1[0] = xldl.DataSet.Tables["PHIEUNHAP"].Columns["MAPN"];
            xldl.DataSet.Tables["PHIEUNHAP"].PrimaryKey = primarykey1;
        }

        public void createCTPhieuNhap()
        {
            string caulenh = "select * from CHITIETPHIEUNHAP";
            adapter = xldl.getDataAdapter(caulenh, "CHITIETPHIEUNHAP");
            primarykey2[0] = xldl.DataSet.Tables["CHITIETPHIEUNHAP"].Columns["MAPN"];
            primarykey2[1] = xldl.DataSet.Tables["CHITIETPHIEUNHAP"].Columns["MASANPHAM"];
            xldl.DataSet.Tables["CHITIETPHIEUNHAP"].PrimaryKey = primarykey2;
        }

        public void createNhaCungCap()
        {
            string caulenh = "select * from KHACHHANG";
            adapter = xldl.getDataAdapter(caulenh, "KHACHHANG");
            primarykey3[0] = xldl.DataSet.Tables["KHACHHANG"].Columns["MAKHACHHANG"];
            xldl.DataSet.Tables["KHACHHANG"].PrimaryKey = primarykey3;
        }

        private void Load_Combobox_NhaCungCap()
        {
            string caulenh = "SELECT * FROM NHACUNGCAP";
            DataTable dt = xldl.getDataTable(caulenh, "NHACUNGCAP");
            cboNhaCungCap.DataSource = dt;
            cboNhaCungCap.DisplayMember = "TENNCC";
            cboNhaCungCap.ValueMember = "MANCC";
            cboNhaCungCap.SelectedIndex = 0;
        }

        private void loadDuLieuSanPham()
        {
            string caulenh = "SELECT* FROM SANPHAM";
            DataTable dt = xldl.getDataTable(caulenh, "SANPHAM");
            dgvSanPham.DataSource = dt;
            btnXoa.Enabled = false;
            btnLamMoi.Enabled = false;
            btnThanhToan.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaHoaDon.Text == "" || cboNhaCungCap.Text == "" || txtNhanVien.Text == "")
                {
                    MessageBox.Show("Không được để trống");
                }
                else
                {
                    btnXoa.Enabled = true;
                    btnLamMoi.Enabled = true;
                    btnThanhToan.Enabled = true;
                    int tong = 0;
                    int tongThanhTien = 0;
                    string masanpham = txtMaSanPham.Text.Trim();
                    string tensanpham = txtTenSanPham.Text.Trim();
                    double dongia = double.Parse(txtDonGia.Text.Trim());
                    string mahoadon = txtMaHoaDon.Text.Trim();
                    int soluongnhaphang = int.Parse(txtSoLuongNhapSP.Text.Trim());
                    string nhacungcap = cboNhaCungCap.SelectedValue.ToString().Trim();
                    string tennhanvien = txtNhanVien.Text.Trim();
                    string ngaycapnhat = DateTime.ParseExact(txtNgayLap.Text, "dd/MM/yyyy", null).ToString("yyyy/MM/dd");

                    double thanhtien = dongia * soluongnhaphang;
                    
                    string caulenh1 = "select count(*) from CHITIETPHIEUNHAP where MAPN = '" + mahoadon + "' AND MASANPHAM = '" + masanpham + "'";
                    
                    int dem1 = xldl.kiemTraKhoaNgoai(caulenh1);
                    
                    if (dem1 > 0)
                    {
                        string soluongthaydoi = "SELECT * FROM CHITIETPHIEUNHAP  where MAPN = '" + mahoadon + "' AND MASANPHAM = '" + masanpham + "'";

                        SqlDataReader drsl = xldl.getDataReader(soluongthaydoi);
                        while (drsl.Read())
                        {
                            txtSoLuong.Text = drsl["SOLUONG"].ToString();
                        }
                        drsl.Close();
                        xldl.dọngKetNoi();
                        //----------------------------------------------------------------------------------
                        int soluongTang = int.Parse(txtSoLuong.Text.Trim());
                        soluongTang++;
                        double thanhtientang = dongia * soluongTang;
                        string caulenhUpadate = "UPDATE CHITIETPHIEUNHAP SET SOLUONG = " + soluongTang + ", THANHTIEN = " + thanhtientang + " WHERE MASANPHAM = '" + masanpham + "' AND MAPN = '" + mahoadon + "'";
                        xldl.CapNhatDB(caulenhUpadate);
                        string soluonsanphamupdate = "UPDATE SANPHAM SET SOLUONGSP = " + soluongTang + " WHERE MASANPHAM = '" + masanpham + "'";

                        xldl.CapNhatDB(soluonsanphamupdate);
                        //----------------------------------------------------------------------------------
                        string tongthanhtien0 = "SELECT * FROM CHITIETPHIEUNHAP WHERE MAPN = N'" + mahoadon + "'";

                        SqlDataReader drthanhtien = xldl.getDataReader(tongthanhtien0);
                        while (drthanhtien.Read())
                        {
                            tongThanhTien += int.Parse(drthanhtien["THANHTIEN"].ToString());
                        }
                        drthanhtien.Close();
                        xldl.dọngKetNoi();
                        lbTongTien.Text = String.Format("{0:0,0.0}", tongThanhTien) + " VNĐ";
                        //----------------------------------------------------------------------------------
                        string soluongiohang2 = "SELECT * FROM CHITIETPHIEUNHAP WHERE MAPN = '" + mahoadon + "'";
                        SqlDataReader drslgio2 = xldl.getDataReader(soluongiohang2);
                        while (drslgio2.Read())
                        {
                            tong += int.Parse(drslgio2["SOLUONG"].ToString());
                        }
                        drslgio2.Close();
                        xldl.dọngKetNoi();
                        label11.Text = "Tổng số lượng trong giỏ hàng: " + tong.ToString() + " cái";
                        //----------------------------------------------------------------------------------
                        string loadLaiDuLieu2 = "SELECT * FROM CHITIETPHIEUNHAP WHERE MAPN = '" + mahoadon + "'";
                        dgvHoaDon.DataSource = xldl.LoadData(loadLaiDuLieu2);
                        return;
                    }
                    //----------------------------------------------------------------------------------
                    string caulenh2 = "select count(*) from CHITIETPHIEUNHAP where MAPN = '" + mahoadon + "'";
                    int dem = xldl.kiemTraKhoaNgoai(caulenh2);
                    if (dem > 0)
                    {
                        string caulenhCTHD = "INSERT CHITIETPHIEUNHAP (MAPN,MASANPHAM,SOLUONG,DONGIA,THANHTIEN) VALUES('" + mahoadon + "', '" + masanpham + "'," + soluongnhaphang + ", " + dongia + ", " + thanhtien + ")";
                        xldl.CapNhatDB(caulenhCTHD);
                        string soluongiohang1 = "SELECT * FROM CHITIETPHIEUNHAP WHERE MAPN = '" + mahoadon + "'";

                        SqlDataReader drslgio1 = xldl.getDataReader(soluongiohang1);
                        while (drslgio1.Read())
                        {
                            tong += int.Parse(drslgio1["SOLUONG"].ToString());
                        }

                        drslgio1.Close();
                        xldl.dọngKetNoi();
                        label11.Text = "Tổng số lượng trong giỏ hàng: " + tong.ToString() + " cái";

                        string loadLaiDuLieu1 = "SELECT * FROM CHITIETPHIEUNHAP WHERE MAPN = '" + mahoadon + "'";
                        dgvHoaDon.DataSource = xldl.LoadData(loadLaiDuLieu1);
                        //----------------------------------------------------------------------------------
                        string tongthanhtien1 = "SELECT * FROM CHITIETPHIEUNHAP WHERE MAPN = '" + mahoadon + "'";

                        SqlDataReader thanhtiendr1 = xldl.getDataReader(tongthanhtien1);
                        while (thanhtiendr1.Read())
                        {
                            tongThanhTien += int.Parse(thanhtiendr1["THANHTIEN"].ToString());
                        }
                        thanhtiendr1.Close();
                        xldl.dọngKetNoi();
                        lbTongTien.Text = String.Format("{0:0,0.0}", tongThanhTien) + " VNĐ";
                        return;
                    }

                    string strSQL = "INSERT PHIEUNHAP (MAPN,ID,MANCC,NGAYLAP,TONGTIEN) VALUES('" + mahoadon + "', '" + maNV + "','" + nhacungcap + "','" + ngaycapnhat + "', NULL)";

                    xldl.CapNhatDB(strSQL);

                    ThongTin.LuuThongTinThanhTien(txtMaHoaDon.Text);
                    string strSQLCHITIETHOADON = "INSERT CHITIETPHIEUNHAP (MAPN,MASANPHAM,SOLUONG,DONGIA,THANHTIEN) VALUES('" + mahoadon + "', '" + masanpham + "'," + soluongnhaphang + ", " + dongia + ", " + thanhtien + ")";
                    xldl.CapNhatDB(strSQLCHITIETHOADON);

                    string soluongiohang = "SELECT * FROM CHITIETPHIEUNHAP WHERE MAPN = N'" + mahoadon + "'";

                    SqlDataReader drslgio = xldl.getDataReader(soluongiohang);
                    while (drslgio.Read())
                    {
                        tong += int.Parse(drslgio["SOLUONG"].ToString());
                    }
                    drslgio.Close();
                    xldl.dọngKetNoi();

                    label11.Text = "Tổng số lượng trong giỏ hàng: " + tong.ToString() + " cái";

                    string tongthanhtien2 = "SELECT * FROM CHITIETPHIEUNHAP WHERE MAPN = N'" + mahoadon + "'";

                    SqlDataReader thanhtiendr2 = xldl.getDataReader(tongthanhtien2);
                    while (thanhtiendr2.Read())
                    {
                        tongThanhTien += int.Parse(thanhtiendr2["THANHTIEN"].ToString());
                    }
                    thanhtiendr2.Close();
                    xldl.dọngKetNoi();
                    lbTongTien.Text = String.Format("{0:0,0.0}", tongThanhTien) + " VNĐ";

                    string loadLaiDuLieu = "SELECT * FROM CHITIETPHIEUNHAP WHERE MAPN = '" + mahoadon + "'";
                    dgvHoaDon.DataSource = xldl.LoadData(loadLaiDuLieu);
                }

            }
            catch
            {
                MessageBox.Show("Thất bại");
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            string mahoadon = txtMaHoaDon.Text.Trim();
            int soluongnhaphang = int.Parse(txtSoLuongNhapSP.Text.Trim());
            string nhacungcap = cboNhaCungCap.SelectedValue.ToString().Trim();
            string tennhanvien = txtNhanVien.Text.Trim();
            string tenkhachang = "SELECT * FROM PHIEUNHAP WHERE MAPN = '" + mahoadon + "'";
            string tennv = "";
            string idtenNCC = "";
            string idtennv = "";
            string thoigian = "";
            SqlDataReader dr = xldl.getDataReader(tenkhachang);
            while (dr.Read())
            {
                thoigian = dr["NGAYLAP"].ToString();
                idtenNCC = dr["MANCC"].ToString();
                idtennv = dr["ID"].ToString();
            }
            dr.Close();
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

            string tenncc = "";
            string laytenncc = "SELECT * FROM NHACUNGCAP WHERE MANCC = '" + idtenNCC + "'";
            SqlDataReader dr1 = xldl.getDataReader(laytenncc);
            while (dr1.Read())
            {
                tenncc = dr1["TENNCC"].ToString();

            }
            dr1.Close();
            xldl.dọngKetNoi();

            ThongTin.LuuThongTintenKH(tenncc);
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn in hóa đơn không", "THÔNG BÁO", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                FormPhieuNhap tt = new FormPhieuNhap();
                tt.ShowDialog();
                int tongThanhTien = 0;
                mahoadon = txtMaHoaDon.Text.Trim();
                string tongthanhtien2 = "SELECT * FROM CHITIETPHIEUNHAP WHERE MAPN = N'" + mahoadon + "'";
                SqlDataReader thanhtiendr2 = xldl.getDataReader(tongthanhtien2);
                while (thanhtiendr2.Read())
                {
                    tongThanhTien += int.Parse(thanhtiendr2["THANHTIEN"].ToString());
                }
                thanhtiendr2.Close();
                xldl.dọngKetNoi();
                string updateTONGTIEN = "UPDATE PHIEUNHAP SET TONGTIEN = " + tongThanhTien + " WHERE MAPN = '" + mahoadon + "'";

                xldl.CapNhatDB(updateTONGTIEN);
                txtMaHoaDon.Enabled = true;
                checkBox2.Enabled = true;
                checkBox2.Checked = false;
                txtMaHoaDon.Text = "";
                string mahoadon1 = "";
                string loadLaiDuLieu = "SELECT * FROM CHITIETHOADON WHERE MAHD = '" + mahoadon1 + "'";
                dgvHoaDon.DataSource = xldl.LoadData(loadLaiDuLieu);
                txtMaHoaDon.Text = "";
                txtMaSanPham.Text = "";
                txtTenSanPham.Text = "";
                txtDonGia.Text = "";
                lbTongTien.Text = "0 VNĐ";
                label11.Text = "Tổng số lượng trong giỏ hàng: 0 món";
                return;
            }
            else if (dialogResult == DialogResult.No)
            {
                int tongThanhTien = 0;
                mahoadon = txtMaHoaDon.Text.Trim();
                string tongthanhtien2 = "SELECT * FROM CHITIETPHIEUNHAP WHERE MAPN = N'" + mahoadon + "'";
                SqlDataReader thanhtiendr2 = xldl.getDataReader(tongthanhtien2);
                while (thanhtiendr2.Read())
                {
                    tongThanhTien += int.Parse(thanhtiendr2["THANHTIEN"].ToString());
                }
                thanhtiendr2.Close();
                xldl.dọngKetNoi();
                string updateTONGTIEN = "UPDATE PHIEUNHAP SET TONGTIEN = " + tongThanhTien + " WHERE MAPN = '" + mahoadon + "'";

                xldl.CapNhatDB(updateTONGTIEN);
                txtMaHoaDon.Enabled = true;
                checkBox2.Enabled = true;
                checkBox2.Checked = false;
                txtMaHoaDon.Text = "";
                string mahoadon1 = "";
                string loadLaiDuLieu = "SELECT * FROM CHITIETHOADON WHERE MAHD = '" + mahoadon1 + "'";
                dgvHoaDon.DataSource = xldl.LoadData(loadLaiDuLieu);
                txtMaHoaDon.Text = "";
                //txtTenKhachHang.Text = "";
                txtMaSanPham.Text = "";
                txtTenSanPham.Text = "";
                txtDonGia.Text = "";
                lbTongTien.Text = "0 VNĐ";
                label11.Text = "Tổng số lượng trong giỏ hàng: 0 món";
            }
            
        }

        private void btnThemSoLuong_Click(object sender, EventArgs e)
        {
            int tong = 0;
            string masanpham = txtMaSanPhamAn.Text.Trim();
            string mahoadon = txtMaHoaDonAn.Text.Trim();
            string mahoadondanghien = txtMaHoaDon.Text.Trim();
            int sl = int.Parse(txtSoLuongSanPhamChon.Text.Trim());
            int dongiagoc = 0;
            int tongThanhTien = 0;
            int soluonggoc = 0;
            string dongia = "SELECT * FROM SANPHAM WHERE MASANPHAM = '" + masanpham + "'";
            SqlDataReader dongiadr = xldl.getDataReader(dongia);
            while (dongiadr.Read())
            {
                dongiagoc = int.Parse(dongiadr["DONGIA"].ToString());
                soluonggoc = int.Parse(dongiadr["SOLUONGSP"].ToString());
            }
            dongiadr.Close();

            string tongthanhtien2 = "SELECT * FROM CHITIETPHIEUNHAP WHERE MAPN = N'" + mahoadon + "'";

            double thanhtien = dongiagoc * (sl + soluonggoc);
            int soluongsauupdate = sl + soluonggoc;
            string soluongiohang = "UPDATE CHITIETPHIEUNHAP SET SOLUONG = " + soluongsauupdate + ", THANHTIEN = " + thanhtien + " WHERE MAPN = '" + mahoadon + "' AND MASANPHAM = '" + masanpham + "'";

            xldl.CapNhatDB(soluongiohang);
            string soluonsanphamupdate = "UPDATE SANPHAM SET SOLUONGSP = " + soluongsauupdate + " WHERE MASANPHAM = '" + masanpham + "'";

            xldl.CapNhatDB(soluonsanphamupdate);
            SqlDataReader thanhtiendr2 = xldl.getDataReader(tongthanhtien2);
            while (thanhtiendr2.Read())
            {
                tongThanhTien += int.Parse(thanhtiendr2["THANHTIEN"].ToString());
            }
            thanhtiendr2.Close();
            xldl.dọngKetNoi();
            lbTongTien.Text = String.Format("{0:0,0.0}", tongThanhTien) + " VNĐ";

            string soluongiohangThayDoi = "SELECT * FROM CHITIETPHIEUNHAP WHERE MAPN = N'" + mahoadon + "'";

            SqlDataReader drslgio = xldl.getDataReader(soluongiohangThayDoi);
            while (drslgio.Read())
            {
                tong += int.Parse(drslgio["SOLUONG"].ToString());
            }
            drslgio.Close();
            xldl.dọngKetNoi();

            label11.Text = "Số lượng trong giỏ hàng: " + tong.ToString() + " món";

            string loadLaiDuLieuSP = "SELECT * FROM SANPHAM";
            dgvHoaDon.DataSource = xldl.LoadData(loadLaiDuLieuSP);

            string loadLaiDuLieu = "SELECT * FROM CHITIETPHIEUNHAP WHERE MAPN = '" + mahoadondanghien + "'";
            dgvHoaDon.DataSource = xldl.LoadData(loadLaiDuLieu);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaHoaDon.Enabled = true;
            checkBox2.Enabled = true;
            checkBox2.Checked = false;
            string mahoadon = txtMaHoaDon.Text.Trim();
            string strSQL = "DELETE CHITIETPHIEUNHAP WHERE MAPN = '" + mahoadon + "'";
            xldl.CapNhatDB(strSQL);
            MessageBox.Show("Làm thành công nha ^^");
            string loadLaiDuLieu = "SELECT * FROM CHITIETPHIEUNHAP WHERE MAPN = '" + mahoadon + "'";
            dgvHoaDon.DataSource = xldl.LoadData(loadLaiDuLieu);
            txtMaHoaDon.Text = "";
            txtMaSanPham.Text = "";
            txtTenSanPham.Text = "";
            txtDonGia.Text = "";
            lbTongTien.Text = "0 VNĐ";
            label11.Text = "Tổng số lượng trong giỏ hàng: 0 món";
            txtSoLuong.Text = "";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int tongThanhTien = 0;
            int tong = 0;
            string mahoadon = txtMaHoaDon.Text.Trim();
            string masanpham = txtMaSanPhamAn.Text.Trim();
            string strSQL = "DELETE CHITIETPHIEUNHAP WHERE MAPN = '" + mahoadon + "' AND MASANPHAM = '" + masanpham + "'";
            xldl.CapNhatDB(strSQL);
            string soluongiohang = "SELECT * FROM CHITIETPHIEUNHAP WHERE MAPN = N'" + mahoadon + "'";

            SqlDataReader drslgio = xldl.getDataReader(soluongiohang);
            while (drslgio.Read())
            {
                tong += int.Parse(drslgio["SOLUONG"].ToString());
            }
            drslgio.Close();
            xldl.dọngKetNoi();

            label11.Text = "Tổng số lượng trong giỏ hàng: " + tong.ToString() + " cái";

            string tongthanhtien2 = "SELECT * FROM CHITIETPHIEUNHAP WHERE MAPN = N'" + mahoadon + "'";

            SqlDataReader thanhtiendr2 = xldl.getDataReader(tongthanhtien2);
            while (thanhtiendr2.Read())
            {
                tongThanhTien += int.Parse(thanhtiendr2["THANHTIEN"].ToString());
            }
            thanhtiendr2.Close();
            xldl.dọngKetNoi();

            lbTongTien.Text = String.Format("{0:0,0.0}", tongThanhTien) + " VNĐ";
            string loadLaiDuLieu = "SELECT * FROM CHITIETPHIEUNHAP WHERE MAPN = '" + mahoadon + "'";
            dgvHoaDon.DataSource = xldl.LoadData(loadLaiDuLieu);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                int Numrd;

                Random rd = new Random();
                Numrd = rd.Next(1, 100);//biến Numrd sẽ nhận có giá trị ngẫu nhiên trong khoảng 1000 đến 99999
                txtMaHoaDon.Text = "PN0" + rd.Next(1000, 99999).ToString();
                txtMaHoaDon.Enabled = false;
                checkBox2.Enabled = false;
            }
        }

        private void UserControlNhapHang_Load(object sender, EventArgs e)
        {
            this.dgvHoaDon.DefaultCellStyle.ForeColor = Color.Black;
            this.dgvSanPham.DefaultCellStyle.ForeColor = Color.Black;
            txtNgayLap.Text = DateTime.Now.ToString();
            createSanPham();
            createPhieuNhap();
            createCTPhieuNhap();
            createNhaCungCap();
            Load_Combobox_NhaCungCap();
            txtNhanVien.Text = ThongTin.LayThongTinDangNhap();
            maNV = ThongTin.LayThongTinID();
            
            loadDuLieuSanPham();
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (e.RowIndex == -1) return;

            txtMaSanPham.Text = dgvSanPham.Rows[index].Cells[0].Value.ToString().Trim();
            txtTenSanPham.Text = dgvSanPham.Rows[index].Cells[1].Value.ToString().Trim();
            txtDonGia.Text = dgvSanPham.Rows[index].Cells[4].Value.ToString().Trim();
            txtSoLuongNhapSP.Text = dgvSanPham.Rows[index].Cells[5].Value.ToString().Trim();
        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (e.RowIndex == -1) return;

            txtMaHoaDonAn.Text = dgvHoaDon.Rows[index].Cells[0].Value.ToString().Trim();
            txtMaSanPhamAn.Text = dgvHoaDon.Rows[index].Cells[1].Value.ToString().Trim();
            txtSoLuongSanPhamChon.Text = dgvHoaDon.Rows[index].Cells[2].Value.ToString().Trim();
            txtDonGiaAn.Text = dgvHoaDon.Rows[index].Cells[3].Value.ToString().Trim();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (xldl.searchSanPham(txtTimKiem.Text) != null)
            {
                dgvSanPham.DataSource = xldl.searchSanPham(txtTimKiem.Text);
            }
            else MessageBox.Show("Không tìm thấy");
        }

        private void BtnTroLai_Click(object sender, EventArgs e)
        {
            dgvSanPham.DataSource = xldl.DataSet.Tables["SANPHAM"];
        }
    }
}
