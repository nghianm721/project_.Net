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
    public partial class BanHangUC : UserControl
    {
        public string tenNV = "";
        public string maNV = "";

        XuLyDuLieu xldl = new XuLyDuLieu("QL_SHOPQUANAO");

        SqlDataAdapter adapter = new SqlDataAdapter();
        DataColumn[] primarykey = new DataColumn[1];
        DataColumn[] primarykey1 = new DataColumn[1];
        DataColumn[] primarykey2 = new DataColumn[2];
        DataColumn[] primarykey3 = new DataColumn[1];

        public BanHangUC()
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
        public void createKhachHang()
        {
            string caulenh = "select * from KHACHHANG";
            adapter = xldl.getDataAdapter(caulenh, "KHACHHANG");
            primarykey3[0] = xldl.DataSet.Tables["KHACHHANG"].Columns["MAKHACHHANG"];
            xldl.DataSet.Tables["KHACHHANG"].PrimaryKey = primarykey3;
        }
        private void LoadcbKH()
        {
            string caulenh = "SELECT * FROM KHACHHANG";
            DataTable dt = xldl.getDataTable(caulenh, "KHACHHANG");
            cbKhachHang.DataSource = dt;
            cbKhachHang.DisplayMember = "TENKHACHHANG";
            cbKhachHang.ValueMember = "MAKHACHHANG";

            cbKhachHang.SelectedIndex = 0;
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
        
        private void loadDuLieuHoaDon()
        {
            dgvHoaDon.DataSource = xldl.DataSet.Tables["HOADON"];
        }
        
        public void UserControlBanHang_Load(object sender, EventArgs e)
        {
            this.dgvHoaDon.DefaultCellStyle.ForeColor = Color.Black;
            this.dgvSanPham.DefaultCellStyle.ForeColor = Color.Black;
            txtNgayLap.Text = DateTime.Now.ToString();
            createSanPham();
            createHoaDon();
            createCTHoaDon();
            createKhachHang();
            
            txtNhanVien.Text = ThongTin.LayThongTinDangNhap();
            maNV = ThongTin.LayThongTinID();
                    
            loadDuLieuSanPham();
           
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            
                FormThanhToan thanhToan = new FormThanhToan();
                thanhToan.ShowDialog();
                int tongThanhTien = 0;
                string mahoadon = txtMaHoaDon.Text.Trim();
                string tongthanhtien2 = "SELECT * FROM CHITIETHOADON WHERE MAHD = N'" + mahoadon + "'";
                SqlDataReader thanhtiendr2 = xldl.getDataReader(tongthanhtien2);
                while (thanhtiendr2.Read())
                {
                    tongThanhTien += int.Parse(thanhtiendr2["THANHTIEN"].ToString());
                }
                thanhtiendr2.Close();
                xldl.dọngKetNoi();
                string updateTONGTIEN = "UPDATE HOADON SET TONGTIEN = " + tongThanhTien + " WHERE MAHD = '" + mahoadon + "'";

                xldl.CapNhatDB(updateTONGTIEN);
                txtMaHoaDon.Enabled = true;
                checkBox2.Enabled = true;
                checkBox2.Checked = false;
                txtMaHoaDon.Text = "";
                string mahoadon1 = "";
                string loadLaiDuLieu = "SELECT * FROM CHITIETHOADON WHERE MAHD = '" + mahoadon1 + "'";
                dgvHoaDon.DataSource = xldl.LoadData(loadLaiDuLieu);
                txtMaHoaDon.Text = "";
                txtTenKhachHang.Text = "";
                txtMaSanPham.Text = "";
                txtTenSanPham.Text = "";
                txtDonGia.Text = "";
                lbTongTien.Text = "0 VNĐ";
                label11.Text = "Tổng số lượng trong giỏ hàng: 0 món";
           
            
        }

        int dongiatinh = 0;
        float loi = 0;
        string paths = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
        private void dgvSanPham_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (e.RowIndex == -1) return;
           txtMaSanPham.Text = dgvSanPham.Rows[index].Cells[0].Value.ToString().Trim();
           txtTenSanPham.Text = dgvSanPham.Rows[index].Cells[1].Value.ToString().Trim();
           //txtdongiagocan.Text = dgvSanPham.Rows[index].Cells[4].Value.ToString().Trim();
           dongiatinh = int.Parse(dgvSanPham.Rows[index].Cells[4].Value.ToString().Trim());
            loi = float.Parse(dgvSanPham.Rows[index].Cells[6].Value.ToString().Trim());
            float tinh = dongiatinh + (dongiatinh * loi);
            txtDonGia.Text = tinh.ToString();
            txtdongiagocan.Text = dgvSanPham.Rows[index].Cells[9].Value.ToString();
            try
            {
                if (txtdongiagocan.Text != " " && txtdongiagocan.Text != "" && txtdongiagocan.Text != null)
                {
                    string url = paths + "\\img\\" + txtdongiagocan.Text;
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
       
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaHoaDon.Text == "" || txtNhanVien.Text == "")
                {
                    MessageBox.Show("Không được để trống");
                }
                else
                {
                    dgvHoaDon.Enabled = true;
                    btnXoa.Enabled = true;
                    btnLamMoi.Enabled = true;
                    btnThanhToan.Enabled = true;
                    int soluongtrukhimua = 1;
                    int soluong = 1;
                    int tong = 0;
                    int tongThanhTien = 0;
                    string masanpham = txtMaSanPham.Text.Trim();
                    string tensanpham = txtTenSanPham.Text.Trim();
                    int dongiagoc = int.Parse(txtDonGia.Text.Trim());
                    double dongia = double.Parse(txtDonGia.Text.Trim());
                    string mahoadon = txtMaHoaDon.Text.Trim();
                    string tenkhachhang = "";
                    if (checkBox1.Checked == true)
                    {
                        string makh = cbKhachHang.SelectedValue.ToString().Trim();
                        string mk = "SELECT * FROM KHACHHANG WHERE MAKHACHHANG = '" + makh + "'";
                        SqlDataReader drmk = xldl.getDataReader(mk);
                        while (drmk.Read())
                        {
                            tenkhachhang = drmk["TENKHACHHANG"].ToString();
                        }
                        drmk.Close();
                        xldl.dọngKetNoi();

                    }
                    else
                    {
                        if (txtMaHoaDon.Text == "" || txtNhanVien.Text == ""||txtTenKhachHang.Text == "")
                        {
                            MessageBox.Show("Không được để trống");
                            return;
                        }
                        else
                        {
                            tenkhachhang = txtTenKhachHang.Text.Trim();
                        }
                    }
                   
                    string tennhanvien = txtNhanVien.Text.Trim();
                    string ngaycapnhat = DateTime.ParseExact(txtNgayLap.Text, "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                   
                    double thanhtien = dongia * soluong;




                    string strSql1 = "select count(*) from CHITIETHOADON where MAHD = '" + mahoadon + "' AND MASANPHAM = '" + masanpham + "'";
                    int dem1 = xldl.kiemTraKhoaNgoai(strSql1);
                    if (dem1 > 0)
                    {
                        MessageBox.Show("sản phẩm đã có trong giỏ hàng của bạn");
                        return;
                    }
                    //----------------------------------------------------------------------------------
                    string strSql = "select count(*) from CHITIETHOADON where MAHD = '" + mahoadon + "'";
                    int dem = xldl.kiemTraKhoaNgoai(strSql);
                    if (dem > 0)
                    {
                        int sltra1 = 0;
                        string trasl1 = "SELECT * FROM SANPHAM WHERE MASANPHAM = '" + txtMaSanPham.Text + "'";
                        SqlDataReader tr1 = xldl.getDataReader(trasl1);
                        while (tr1.Read())
                        {
                            sltra1 = int.Parse(tr1["SOLUONGSP"].ToString());
                        }
                        tr1.Close();
                        xldl.dọngKetNoi();
                        if (sltra1 == 0)
                        {
                            MessageBox.Show("hết hàng");
                            return;
                        }

                        string strSQLCHITIETHOADON1 = "INSERT CHITIETHOADON (MAHD,MASANPHAM,SOLUONG,THANHTIEN,GIAGOC) VALUES('" + mahoadon + "', '" + masanpham + "'," + soluong + ", " + dongia + "," + dongiagoc + ")";
                        xldl.CapNhatDB(strSQLCHITIETHOADON1);
                        string soluongiohang1 = "SELECT * FROM CHITIETHOADON WHERE MAHD = '" + mahoadon + "'";


                        SqlDataReader drslgio1 = xldl.getDataReader(soluongiohang1);
                        while (drslgio1.Read())
                        {
                            tong += int.Parse(drslgio1["SOLUONG"].ToString());
                        }
                        drslgio1.Close();
                        xldl.dọngKetNoi();


                        label11.Text = "Tổng số lượng trong giỏ hàng: " + tong.ToString() + " cái";
                        string loadLaiDuLieu1 = "SELECT * FROM CHITIETHOADON WHERE MAHD = '" + mahoadon + "'";
                        dgvHoaDon.DataSource = xldl.LoadData(loadLaiDuLieu1);

                        //----------------------------------------------------------------------------------
                        string tongthanhtien1 = "SELECT * FROM CHITIETHOADON WHERE MAHD = '" + mahoadon + "'";

                            
                        SqlDataReader thanhtiendr1 = xldl.getDataReader(tongthanhtien1);
                        while (thanhtiendr1.Read())
                        {
                            tongThanhTien += int.Parse(thanhtiendr1["THANHTIEN"].ToString());
                        }
                        thanhtiendr1.Close();
                        xldl.dọngKetNoi();

                        int tru1khiclickthem1 = sltra1 - soluongtrukhimua;
                        string updateSoLuong = "UPDATE SANPHAM SET SOLUONGSP = " + tru1khiclickthem1 + " WHERE MASANPHAM = '" + masanpham + "'";
                        xldl.CapNhatDB(updateSoLuong);

                        lbTongTien.Text = String.Format("{0:0,0.0}", tongThanhTien) + " VNĐ";
                        return;
                    }

                    int sltra = 0;
                    string trasl = "SELECT * FROM SANPHAM WHERE MASANPHAM = '" + txtMaSanPham.Text + "'";
                    SqlDataReader tr = xldl.getDataReader(trasl);
                    while (tr.Read())
                    {
                        sltra = int.Parse(tr["SOLUONGSP"].ToString());
                    }
                    tr.Close();
                    xldl.dọngKetNoi();

                    if (sltra == 0)
                    {
                        MessageBox.Show("hết hàng");
                        return;
                    }

                    string caulenhKH = "INSERT KHACHHANG (TENKHACHHANG,SDT,GIOITINH,NGAYSINH,DIACHI) VALUES (N'" + tenkhachhang + "',NULL, NULL, NULL, NULL)";
                    xldl.CapNhatDB(caulenhKH);

                    string caulenhMaKH = "SELECT * FROM KHACHHANG WHERE TENKHACHHANG = N'" + tenkhachhang + "'";
                   
                    SqlDataReader dr = xldl.getDataReader(caulenhMaKH);
                    while (dr.Read())
                    {
                        txtTenKhachHangAn.Text = dr["MAKHACHHANG"].ToString();
                    }
                    dr.Close();
                    xldl.dọngKetNoi();
                    string makhachhang = txtTenKhachHangAn.Text.Trim();

                   
                    string caulenhHD = "INSERT HOADON (MAHD,NGAYLAP,MAKHACHHANG,ID,TONGTIEN) VALUES('" + mahoadon + "', '" + ngaycapnhat + "'," + makhachhang + ", '" + maNV + "', NULL)";

                    xldl.CapNhatDB(caulenhHD);

                    ThongTin.LuuThongTinThanhTien(txtMaHoaDon.Text);
                    string strSQLCHITIETHOADON = "INSERT CHITIETHOADON (MAHD,MASANPHAM,SOLUONG,THANHTIEN,GIAGOC) VALUES('" + mahoadon + "', '" + masanpham + "'," + soluong + ", " + thanhtien + "," + dongiagoc + ")";
                    xldl.CapNhatDB(strSQLCHITIETHOADON);

                    string soluongiohang = "SELECT * FROM CHITIETHOADON WHERE MAHD = '" + mahoadon + "'";


                    SqlDataReader drslgio = xldl.getDataReader(soluongiohang);
                    while (drslgio.Read())
                    {
                        tong += int.Parse(drslgio["SOLUONG"].ToString());
                    }
                    drslgio.Close();
                    xldl.dọngKetNoi();
                     
                    label11.Text = "Tổng số lượng trong giỏ hàng: " + tong.ToString() + " cái";

                    string tongthanhtien2 = "SELECT * FROM CHITIETHOADON WHERE MAHD = '" + mahoadon + "'";

                    SqlDataReader thanhtiendr2 = xldl.getDataReader(tongthanhtien2);
                    while (thanhtiendr2.Read())
                    {
                        tongThanhTien += int.Parse(thanhtiendr2["THANHTIEN"].ToString());
                    }
                    thanhtiendr2.Close();
                    xldl.dọngKetNoi();
                    lbTongTien.Text = String.Format("{0:0,0.0}", tongThanhTien) + " VNĐ";
                   
                    int tru1khiclickthem = sltra - soluongtrukhimua;
                    caulenhHD = "UPDATE SANPHAM SET SOLUONGSP = " + tru1khiclickthem + " WHERE MASANPHAM = '" + masanpham + "'";
                    xldl.CapNhatDB(caulenhHD);

                    string loadLaiDuLieu = "SELECT * FROM CHITIETHOADON WHERE MAHD = '" + mahoadon + "'";
                    dgvHoaDon.DataSource = xldl.LoadData(loadLaiDuLieu);
                }
            }
            catch
            {
                MessageBox.Show("thất bại");
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                int Numrd;  
                
                Random rd = new Random();
                Numrd = rd.Next(1, 100);//biến Numrd sẽ nhận có giá trị ngẫu nhiên trong khoảng 1 đến 100
                txtMaHoaDon.Text = "HD0" +  rd.Next(1000, 99999).ToString();
                txtMaHoaDon.Enabled = false;
                checkBox2.Enabled = false;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaHoaDon.Enabled = true;
            checkBox2.Enabled = true;
            checkBox2.Checked = false;
            cbKhachHang.Visible = false;
            string mahoadon = txtMaHoaDon.Text.Trim();
            string strSQL = "DELETE CHITIETHOADON WHERE MAHD = '" + mahoadon + "'";
            xldl.CapNhatDB(strSQL);
            MessageBox.Show("Làm mới thành công!");
            string loadLaiDuLieu = "SELECT * FROM CHITIETHOADON WHERE MAHD = '" + mahoadon + "'";
            dgvHoaDon.DataSource = xldl.LoadData(loadLaiDuLieu);
            txtMaHoaDon.Text = "";
            txtTenKhachHang.Text = "";
            txtMaSanPham.Text = "";
            txtTenSanPham.Text = "";
            txtDonGia.Text = "";
            lbTongTien.Text = "0 VNĐ";
            label11.Text = "Tổng số lượng trong giỏ hàng: 0 món";
            txtSoLuong.Text = "";
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

        private void btnThemSoLuong_Click(object sender, EventArgs e)
        {
            int tong = 0;
            int slsp = 0;
            string masanpham = txtMaSanPhamAn.Text.Trim();
            string mahoadon = txtMaHoaDonAn.Text.Trim();
            string mahoadondanghien = txtMaHoaDon.Text.Trim();
            int sl = int.Parse(txtSoLuongSanPhamChon.Text.Trim());
            int dongiagoc = 0;
            int tongThanhTien = 0;
            string dongia = "SELECT * FROM SANPHAM WHERE MASANPHAM = '" + masanpham + "'";
            SqlDataReader dongiadr = xldl.getDataReader(dongia);
            while (dongiadr.Read())
            {
                dongiagoc = int.Parse(dongiadr["DONGIA"].ToString());
            }
            dongiadr.Close();
            xldl.dọngKetNoi();
            int sltronggio = 0;
            string sltra = "SELECT * FROM CHITIETHOADON WHERE MASANPHAM = '" + masanpham + "' AND  MAHD = '" + mahoadon + "'";
            SqlDataReader tr12 = xldl.getDataReader(sltra);
            while (tr12.Read())
            {
                sltronggio = int.Parse(tr12["SOLUONG"].ToString());
            }
            tr12.Close();
            xldl.dọngKetNoi();

            string trado = "SELECT * FROM SANPHAM WHERE MASANPHAM = '" + masanpham + "'";
            SqlDataReader tr = xldl.getDataReader(trado);
            while (tr.Read())
            {
                slsp = int.Parse(tr["SOLUONGSP"].ToString());
            }
            tr.Close();
            xldl.dọngKetNoi();
            int soluonghoantra = slsp + sltronggio;
            string strSQL = "UPDATE SANPHAM SET SOLUONGSP = " + soluonghoantra + " WHERE MASANPHAM = '" + masanpham + "'";
            xldl.CapNhatDB(strSQL);
            int slsp2 = 0;
            string trado2 = "SELECT * FROM SANPHAM WHERE MASANPHAM = '" + masanpham + "'";
            SqlDataReader tr3 = xldl.getDataReader(trado2);
            while (tr3.Read())
            {
                slsp2 = int.Parse(tr3["SOLUONGSP"].ToString());
            }
            tr3.Close();
            xldl.dọngKetNoi();
            int slupdate = slsp2 - sl;
            
            if (slupdate < 0)
            {
               
                MessageBox.Show("Số lượng trong kho không đủ!");
                strSQL = "UPDATE SANPHAM SET SOLUONGSP = " + slsp + " WHERE MASANPHAM = '" + masanpham + "'";
                xldl.CapNhatDB(strSQL);

                return;
            }
             strSQL = "UPDATE SANPHAM SET SOLUONGSP = " + slupdate + " WHERE MASANPHAM = '" + masanpham + "'";
            xldl.CapNhatDB(strSQL);

            float ln = 0;

            string loinhuan = "SELECT * FROM SANPHAM WHERE MASANPHAM = '" + masanpham + "'";
            SqlDataReader drln = xldl.getDataReader(loinhuan);
            while (drln.Read())
            {
                ln = float.Parse(drln["LOINHUAN"].ToString());
            }

            drln.Close();
            xldl.dọngKetNoi();

            double thanhtien = ((dongiagoc * ln) + dongiagoc) * sl;
            string soluongiohang = "UPDATE CHITIETHOADON SET SOLUONG = " + sl + ", THANHTIEN = "+thanhtien+" WHERE MAHD = '" + mahoadon + "' AND MASANPHAM = '" + masanpham + "'";
            xldl.CapNhatDB(soluongiohang);

            string tongthanhtien2 = "SELECT * FROM CHITIETHOADON WHERE MAHD = '" + mahoadon + "'";
            SqlDataReader thanhtiendr2 = xldl.getDataReader(tongthanhtien2);
            while (thanhtiendr2.Read())
            {
                tongThanhTien += int.Parse(thanhtiendr2["THANHTIEN"].ToString());
            }

            thanhtiendr2.Close();
            xldl.dọngKetNoi();
            lbTongTien.Text = String.Format("{0:0,0.0}", tongThanhTien) + " VNĐ";

            string soluongiohangThayDoi = "SELECT * FROM CHITIETHOADON WHERE MAHD = N'" + mahoadon + "'";

            SqlDataReader drslgio = xldl.getDataReader(soluongiohangThayDoi);
            while (drslgio.Read())
            {
                tong += int.Parse(drslgio["SOLUONG"].ToString());
            }

            drslgio.Close();
            xldl.dọngKetNoi();

            label11.Text = "Số lượng trong giỏ hàng: " + tong.ToString() + " món";

            string loadLaiDuLieu = "SELECT * FROM CHITIETHOADON WHERE MAHD = '" + mahoadondanghien + "'";
            dgvHoaDon.DataSource = xldl.LoadData(loadLaiDuLieu);
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int tongThanhTien = 0;
            int tong = 0;
            string mahoadon = txtMaHoaDon.Text.Trim();
            string masanpham = txtMaSanPhamAn.Text.Trim();
            int slsp = 0;
            int sltronggio = 0;
            string sltra = "SELECT * FROM CHITIETHOADON WHERE MASANPHAM = '" + masanpham + "' AND  MAHD = '" + mahoadon + "'";
            SqlDataReader tr12 = xldl.getDataReader(sltra);
            while (tr12.Read())
            {
                sltronggio = int.Parse(tr12["SOLUONG"].ToString());
            }
            tr12.Close();
            xldl.dọngKetNoi();

            string trado = "SELECT * FROM SANPHAM WHERE MASANPHAM = '" + masanpham + "'";
            SqlDataReader tr = xldl.getDataReader(trado);
            while (tr.Read())
            {
                slsp = int.Parse(tr["SOLUONGSP"].ToString());
            }
            tr.Close();
            xldl.dọngKetNoi();
            int soluonghoantra = slsp + sltronggio;
            string strSQL12 = "UPDATE SANPHAM SET SOLUONGSP = " + soluonghoantra + " WHERE MASANPHAM = '" + masanpham + "'";
            xldl.CapNhatDB(strSQL12);

            string strSQL = "DELETE CHITIETHOADON WHERE MAHD = '" + mahoadon + "' AND MASANPHAM = '" + masanpham + "'";
            xldl.CapNhatDB(strSQL);


            string soluongiohang = "SELECT * FROM CHITIETHOADON WHERE MAHD = N'" + mahoadon + "'";


            SqlDataReader drslgio = xldl.getDataReader(soluongiohang);
            while (drslgio.Read())
            {
                tong += int.Parse(drslgio["SOLUONG"].ToString());
            }
            drslgio.Close();
            xldl.dọngKetNoi();


            label11.Text = "Tổng số lượng trong giỏ hàng: " + tong.ToString() + " cái";



            string tongthanhtien2 = "SELECT * FROM CHITIETHOADON WHERE MAHD = N'" + mahoadon + "'";


            SqlDataReader thanhtiendr2 = xldl.getDataReader(tongthanhtien2);
            while (thanhtiendr2.Read())
            {
                tongThanhTien += int.Parse(thanhtiendr2["THANHTIEN"].ToString());
            }
            thanhtiendr2.Close();
            xldl.dọngKetNoi();
            lbTongTien.Text = String.Format("{0:0,0.0}", tongThanhTien) + " VNĐ";
            string loadLaiDuLieu = "SELECT * FROM CHITIETHOADON WHERE MAHD = '" + mahoadon + "'";
            dgvHoaDon.DataSource = xldl.LoadData(loadLaiDuLieu);
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            FormHoaDon a = new FormHoaDon();
            a.ShowDialog();
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                cbKhachHang.Visible = true;
                LoadcbKH();
            }
            else
            {
                cbKhachHang.Visible = false;
            }
        }

        private void txtSoLuongSanPhamChon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                textKhuyenmai.Enabled = true;

            }
            else
            {
                textKhuyenmai.Enabled = false;
            }
        }
    }
}
