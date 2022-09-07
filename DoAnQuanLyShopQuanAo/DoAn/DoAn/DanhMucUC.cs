using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnQuanLyShopQuanAo
{
    public partial class DanhMucUC : UserControl
    {
        public DanhMucUC()
        {
            InitializeComponent();
        }
        private void showUserControl(UserControl user)
        {
            pcenter.Controls.Add(user);
            user.Dock = DockStyle.Fill;
            user.BringToFront();
        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            LoaiHangUC loaihangUC = new LoaiHangUC();
            showUserControl(loaihangUC);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ThuongHieuUC thuonghieuUC = new ThuongHieuUC();
            showUserControl(thuonghieuUC);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

            KhachHangUC khachhangUC = new KhachHangUC();
            showUserControl(khachhangUC);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            NhaCungCapUC nhacungcapUC = new NhaCungCapUC();
            showUserControl(nhacungcapUC);
        }
    }
}
