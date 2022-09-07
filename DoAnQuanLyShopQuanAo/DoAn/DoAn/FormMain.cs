using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnQuanLyShopQuanAo
{

    public partial class FormMain : Form
    {
        XuLyDuLieu xldl = new XuLyDuLieu("QL_SHOPQUANAO");

        public bool isAdmin = false;
       
        private string tenNhanVien;

        private string maNhanVien;

        public string MaNhanVien
        {
            get { return maNhanVien; }
            set { maNhanVien = value; }
        }

        public string TenNhanVien
        {
            get { return tenNhanVien; }
            set { tenNhanVien = value; }
        }

        public Point mouseLocation;
        NhanVienUC nhanvienUC;
        TrangChuUC trangchuUC;
        BanHangUC banhangUC;
        SanPhamUC sanphamUC;
        HoaDonUC hoadonUC;
        TaiKhoanUC taikhoanUC;
        DanhMucUC danhmucUC;

        public FormMain()
        {
            InitializeComponent();
            button1_Click(null, null);
        }

        private void click()
        {
            btn1.BackColor = colorDialog1.Color;
            btn2.BackColor = colorDialog1.Color;
            btn3.BackColor = colorDialog1.Color;
            btn4.BackColor = colorDialog1.Color;
            btn5.BackColor = colorDialog1.Color;
            btn6.BackColor = colorDialog1.Color;
            btn7.BackColor = colorDialog1.Color;
            button3.BackColor = colorDialog1.Color;
        }

        private void showUserControl(UserControl user)
        {
            pCenter.Controls.Add(user);
            user.Dock = DockStyle.Fill;
            user.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            click();
            btn1.BackColor = Color.BlueViolet;
            trangchuUC = new TrangChuUC();
            showUserControl(trangchuUC);
        }
        
        private void button2_Click(object sender, EventArgs e)
        {          
            click();
            btn2.BackColor = Color.BlueViolet;
            nhanvienUC = new NhanVienUC();
            showUserControl(nhanvienUC);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            click();
            btn3.BackColor = Color.BlueViolet;
            banhangUC = new BanHangUC();
            showUserControl(banhangUC);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            click();
            btn4.BackColor = Color.BlueViolet;
            sanphamUC = new SanPhamUC();
             showUserControl(sanphamUC);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            click();
            btn5.BackColor = Color.BlueViolet;
            hoadonUC = new HoaDonUC();
            showUserControl(hoadonUC);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            click();
            btn6.BackColor = Color.BlueViolet;
            NhapHangUC us6 = new NhapHangUC();
            showUserControl(us6);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string ten = "";

        public void FrmAdmin_Load(object sender, EventArgs e)
        {
            
            FormDangNhap flg = new FormDangNhap();
            if (flg.ShowDialog() == DialogResult.OK)
            {

                if (flg.admin == true)
                {
                    TenNhanVien = ThongTin.LayThongTinDangNhap();
                    MaNhanVien = ThongTin.LayThongTinID();
                    isAdmin = true;
                    label2.Text = "Người sử dụng: " + TenNhanVien;
                }
                else
                {
                    TenNhanVien = ThongTin.LayThongTinDangNhap();
                    MaNhanVien = ThongTin.LayThongTinID();
                    isAdmin = false;
                    label2.Text = "Người sử dụng: " + TenNhanVien;
                }
            }
            else 
                Application.Exit();

            btn2.Enabled = isAdmin;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pHeader_MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void pHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mous = Control.MousePosition;
                mous.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mous;
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            click();
            button3.BackColor = Color.BlueViolet;
            taikhoanUC = new TaiKhoanUC();
            showUserControl(taikhoanUC);
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            click();
            btn7.BackColor = Color.BlueViolet;
            danhmucUC = new DanhMucUC();
            showUserControl(danhmucUC);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
            FormDangNhap flg = new FormDangNhap();
            if (flg.ShowDialog() == DialogResult.OK)
            {

                if (flg.admin == true)
                {
                    TenNhanVien = ThongTin.LayThongTinDangNhap();
                    MaNhanVien = ThongTin.LayThongTinID();
                    isAdmin = true;
                    label2.Text = "Xin chào: " + TenNhanVien;
                }
                else
                {
                    TenNhanVien = ThongTin.LayThongTinDangNhap();
                    MaNhanVien = ThongTin.LayThongTinID();
                    isAdmin = false;
                    label2.Text = "Xin chào: " + TenNhanVien;
                }
            }
            else
                Application.Exit();

            btn2.Enabled = isAdmin;
        }
    }
}
