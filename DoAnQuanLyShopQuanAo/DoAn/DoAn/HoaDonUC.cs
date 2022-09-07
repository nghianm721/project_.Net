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
    public partial class HoaDonUC : UserControl
    {
        XuLyDuLieu xldl = new XuLyDuLieu("QL_SHOPQUANAO");
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataColumn[] primarykey = new DataColumn[1];
        DataColumn[] primarykey1 = new DataColumn[1];
        DataColumn[] primarykey2 = new DataColumn[2];
        DataColumn[] primarykey3 = new DataColumn[1];
        public HoaDonUC()
        {
            InitializeComponent();
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

        private void UserControlHoaDon_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            this.dataGridView2.DefaultCellStyle.ForeColor = Color.Black;
            this.dataGridView3.DefaultCellStyle.ForeColor = Color.Black;
            this.dataGridView4.DefaultCellStyle.ForeColor = Color.Black;
            createHoaDon();
            createCTHoaDon();
            createPhieuNhap();
            createCTPhieuNhap();
            dataGridView1.DataSource = xldl.DataSet.Tables["HOADON"];
            dataGridView3.DataSource = xldl.DataSet.Tables["PHIEUNHAP"];
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (e.RowIndex == -1) return;
            string a = dataGridView1.Rows[index].Cells[0].Value.ToString().Trim();
            dataGridView2.DataSource = xldl.getChiTietHoaDon(a);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (xldl.searchHoaDon(txtTimKiem.Text) != null)
            {
                dataGridView1.DataSource = xldl.searchHoaDon(txtTimKiem.Text);
            }
            else MessageBox.Show("Không tìm thấy");
        }

        private void BtnTroLai_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = xldl.DataSet.Tables["HOADON"];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (xldl.searchPhieuNhap(textBox1.Text) != null)
            {
                dataGridView3.DataSource = xldl.searchPhieuNhap(textBox1.Text);
            }
            else MessageBox.Show("Không tìm thấy");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView3.DataSource = xldl.DataSet.Tables["PHIEUNHAP"];
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (e.RowIndex == -1) return;
            string a = dataGridView3.Rows[index].Cells[0].Value.ToString().Trim();
            dataGridView4.DataSource = xldl.getChiTietPhieuNhap(a);
        }

        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            if (xldl.searchPhieuNhap(txtTimKiem.Text) != null)
            {
                dataGridView1.DataSource = xldl.searchHoaDon(txtTimKiem.Text);
            }
            else MessageBox.Show("Không tìm thấy");
        }

        private void BtnTroLai_Click_1(object sender, EventArgs e)
        {
            dataGridView1.DataSource = xldl.DataSet.Tables["HOADON"];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormDoanhThu hienthi = new FormDoanhThu();
            hienthi.ShowDialog();
        }
    }
}
