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
    public partial class LoaiHangUC : UserControl
    {
        XuLyDuLieu xldl = new XuLyDuLieu("QL_SHOPQUANAO");
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataColumn[] primarykey = new DataColumn[1];

        public LoaiHangUC()
        {
            InitializeComponent();
        }
        public void createLoai()
        {
            string caulenh = "select * from LOAISANPHAM";
            adapter = xldl.getDataAdapter(caulenh, "LOAISANPHAM");
            primarykey[0] = xldl.DataSet.Tables["LOAISANPHAM"].Columns["MALOAI"];
            xldl.DataSet.Tables["LOAISANPHAM"].PrimaryKey = primarykey;
        }
        private void UserControlTheLoai_Load(object sender, EventArgs e)
        {
            this.dGVLoaiSP.DefaultCellStyle.ForeColor = Color.Black;
            createLoai();
            dGVLoaiSP.DataSource = xldl.DataSet.Tables["LOAISANPHAM"];
        }

        private void dGVLoaiSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (e.RowIndex == -1) return;
            txtMaSLoai.Text = dGVLoaiSP.Rows[index].Cells[0].Value.ToString();
            txtTenLoai.Text = dGVLoaiSP.Rows[index].Cells[1].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaSLoai.Enabled = true;
            txtMaSLoai.Text = "";
            txtTenLoai.Text = "";
            txtMaSLoai.Focus();
        }
        private void loadLaiData()
        {
            string loadLaiDuLieu = "SELECT * FROM LOAISANPHAM";
            dGVLoaiSP.DataSource = xldl.LoadData(loadLaiDuLieu);
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaSLoai.Text == "" || txtTenLoai.Text == "")
                {
                    MessageBox.Show("Không được để trống");
                }
                else
                {
                    string maloai = txtMaSLoai.Text.Trim();
                    string tenloai = txtTenLoai.Text.Trim();

                    string strSQL = "SELECT COUNT(*) FROM LOAISANPHAM WHERE MALOAI = '" + maloai + "'";
                    bool kq = xldl.kiemTraTrung(strSQL);


                    if (kq == true)
                    {
                        MessageBox.Show("Đã tồn tại mã sản loại này: " + maloai);
                        return;
                    }

                    strSQL = "INSERT LOAISANPHAM VALUES('" + maloai + "',N'" + tenloai + "')";

                    xldl.CapNhatDB(strSQL);
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
                string maloai = txtMaSLoai.Text.Trim();
                string caulenh = "SELECT COUNT(*) FROM LOAISANPHAM WHERE MALOAI = '" + maloai + "'";

                bool kq = xldl.kiemTraTrung(caulenh);

                if (kq == false)
                {
                    MessageBox.Show("Không tồn tại mã Loại này: " + maloai);
                    return;
                }

                caulenh = "DELETE LOAISANPHAM WHERE MALOAI = '" + maloai + "'";
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
                string maloai = txtMaSLoai.Text.Trim();
                string tenloai = txtTenLoai.Text.Trim();
                string caulenh = "SELECT COUNT(*) FROM LOAISANPHAM WHERE MALOAI = '" + maloai + "'";

                bool kq = xldl.kiemTraTrung(caulenh);
                if (kq == false)
                {
                    MessageBox.Show("Không tồn tại mã loại này: " + maloai);
                    return;
                }

                caulenh = "UPDATE LOAISANPHAM SET TENLOAI = N'" + tenloai + "' WHERE MALOAI = '" + maloai + "'";
                xldl.CapNhatDB(caulenh);
                MessageBox.Show("Sửa thành công!");

                loadLaiData();
            }
            catch
            {
                MessageBox.Show("Thất bại");
            }
        }
    }
}
