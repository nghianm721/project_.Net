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
    public partial class ThuongHieuUC : UserControl
    {
        XuLyDuLieu xldl = new XuLyDuLieu("QL_SHOPQUANAO");
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataColumn[] primarykey = new DataColumn[1];

        public ThuongHieuUC()
        {
            InitializeComponent();
        }

        public void createThuongHieu()
        {
            string caulenh = "select * from THUONGHIEU";
            adapter = xldl.getDataAdapter(caulenh, "THUONGHIEU");
            primarykey[0] = xldl.DataSet.Tables["THUONGHIEU"].Columns["MATHUONGHIEU"];
            xldl.DataSet.Tables["THUONGHIEU"].PrimaryKey = primarykey;
        }

        private void UserControlThuongHieu_Load(object sender, EventArgs e)
        {
            this.dGVThuongHieu.DefaultCellStyle.ForeColor = Color.Black;
            createThuongHieu();
            dGVThuongHieu.DataSource = xldl.DataSet.Tables["THUONGHIEU"];
        }

        private void dGVThuongHieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (e.RowIndex == -1) return;

            txtMa.Text = dGVThuongHieu.Rows[index].Cells[0].Value.ToString();
            txtTen.Text = dGVThuongHieu.Rows[index].Cells[1].Value.ToString();
        }

        private void loadLaiData()
        {
            string loadLaiDuLieu = "SELECT * FROM THUONGHIEU";
            dGVThuongHieu.DataSource = xldl.LoadData(loadLaiDuLieu);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMa.Enabled = true;
            txtMa.Text = "";
            txtTen.Text = "";
            txtMa.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMa.Text == "" || txtTen.Text == "")
                {
                    MessageBox.Show("Không được để trống");
                }
                else
                {
                    string mathuonghieu = txtMa.Text.Trim();
                    string tenthuonghieu = txtTen.Text.Trim();
                    string caulenh = "SELECT COUNT(*) FROM THUONGHIEU WHERE MATHUONGHIEU = '" + mathuonghieu + "'";

                    bool kq = xldl.kiemTraTrung(caulenh);

                    if (kq == true)
                    {
                        MessageBox.Show("Đã tồn tại mã Thương hiệu này: " + mathuonghieu);
                        return;
                    }

                    caulenh = "INSERT THUONGHIEU VALUES('" + mathuonghieu + "',N'" + tenthuonghieu + "')";
                    xldl.CapNhatDB(caulenh);
                    MessageBox.Show("Thêm thành công!");

                    loadLaiData();
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
                string ma = txtMa.Text.Trim();
                string caulenh = "SELECT COUNT(*) FROM THUONGHIEU WHERE MATHUONGHIEU = '" + ma + "'";

                bool kq = xldl.kiemTraTrung(caulenh);

                if (kq == false)
                {
                    MessageBox.Show("Không tồn tại mã thương hiệu này: " + ma);
                    return;
                }
                caulenh = "DELETE THUONGHIEU WHERE MATHUONGHIEU = '" + ma + "'";
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
                string ma = txtMa.Text.Trim();
                string ten = txtTen.Text.Trim();
                string caulenh = "SELECT COUNT(*) FROM THUONGHIEU WHERE MATHUONGHIEU = '" + ma + "'";

                bool kq = xldl.kiemTraTrung(caulenh);

                if (kq == false)
                {
                    MessageBox.Show("Không tồn tại mã thương hiệu này: " + ma);
                    return;
                }

                caulenh = "UPDATE THUONGHIEU SET TENTHUONGHIEU = N'" + ten + "' WHERE MATHUONGHIEU = '" + ma + "'";
                xldl.CapNhatDB(caulenh);
                MessageBox.Show("Sửa thành công!");

                loadLaiData();
            }
            catch
            {
                MessageBox.Show("thất bại");
            }
        }
    }
}
