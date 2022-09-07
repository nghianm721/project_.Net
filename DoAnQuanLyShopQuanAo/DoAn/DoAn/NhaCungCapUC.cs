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
     
    public partial class NhaCungCapUC : UserControl
    {
        XuLyDuLieu xldl = new XuLyDuLieu("QL_SHOPQUANAO");
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataColumn[] primarykey = new DataColumn[1];

        public NhaCungCapUC()
        {
            InitializeComponent();
        }

        public void createNCC()
        {
            string caulenh = "select * from NHACUNGCAP";
            adapter = xldl.getDataAdapter(caulenh, "NHACUNGCAP");
            primarykey[0] = xldl.DataSet.Tables["NHACUNGCAP"].Columns["MANCC"];
            xldl.DataSet.Tables["NHACUNGCAP"].PrimaryKey = primarykey;
        }
        private void UserControlNhaCungCap_Load(object sender, EventArgs e)
        {
            txtDiaChi.Enabled = true;
            this.dGVLoaiSP.DefaultCellStyle.ForeColor = Color.Black;
            createNCC();
            dGVLoaiSP.DataSource = xldl.DataSet.Tables["NHACUNGCAP"];
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaNCC.Enabled = true;
            txtMaNCC.Text = "";
            txtTenNCC.Text = "";
            txtMaNCC.Focus();
            txtDiaChi.Text = "";
            txtSDT.Text = "";
        }

        private void loadLaiData()
        {
            string loadLaiDuLieu = "SELECT * FROM NHACUNGCAP";
            dGVLoaiSP.DataSource = xldl.LoadData(loadLaiDuLieu);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaNCC.Text == "" || txtTenNCC.Text == "" || txtDiaChi.Text == "" || txtSDT.Text == "")
                {
                    MessageBox.Show("Không được để trống");
                }
                else
                {
                    string ma = txtMaNCC.Text.Trim();
                    string ten = txtTenNCC.Text.Trim();
                    string dc = txtDiaChi.Text.Trim();
                    string sdt = txtSDT.Text.Trim();
                    string caulenh = "SELECT COUNT(*) FROM NHACUNGCAP WHERE MANCC = '" + ma + "'";
                    bool kq = xldl.kiemTraTrung(caulenh);


                    if (kq == true)
                    {
                        MessageBox.Show("Đã tồn tại mã này: " + ma);
                        return;
                    }

                    caulenh = "INSERT NHACUNGCAP VALUES('" + ma + "',N'" + ten + "',N'" + dc + "'," + sdt + ")";
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
                string ma = txtMaNCC.Text.Trim();
                string caulenh = "SELECT COUNT(*) FROM NHACUNGCAP WHERE MANCC = '" + ma + "'";

                bool kq = xldl.kiemTraTrung(caulenh);

                if (kq == false)
                {
                    MessageBox.Show("Không tồn tại mã này: " + ma);
                    return;
                }
                caulenh = "DELETE NHACUNGCAP WHERE MANCC = '" + ma + "'";
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
                string ma = txtMaNCC.Text.Trim();
                string ten = txtTenNCC.Text.Trim();
                string dc = txtDiaChi.Text.Trim();
                string sdt = txtSDT.Text.Trim();
                string caulenh = "SELECT COUNT(*) FROM NHACUNGCAP WHERE MANCC = '" + ma + "'";

                bool kq = xldl.kiemTraTrung(caulenh);

                if (kq == false)
                {
                    MessageBox.Show("Không tồn tại mã này: " + ma);
                    return;
                }

                caulenh = "UPDATE NHACUNGCAP SET TENNCC = N'" + ten + "',DIACHI = N'" + dc + "',SODIENTHOAI = " + sdt + " WHERE MANCC = '" + ma + "'";
                xldl.CapNhatDB(caulenh);
                MessageBox.Show("Sửa thành công!");

                loadLaiData();
            }
            catch
            {
                MessageBox.Show("Thất bại");
            }
        }

        private void dGVLoaiSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (e.RowIndex == -1) return;

            txtMaNCC.Text = dGVLoaiSP.Rows[index].Cells[0].Value.ToString();
            txtTenNCC.Text = dGVLoaiSP.Rows[index].Cells[1].Value.ToString();
            txtDiaChi.Text = dGVLoaiSP.Rows[index].Cells[2].Value.ToString();
            txtSDT.Text= dGVLoaiSP.Rows[index].Cells[3].Value.ToString();
        }
    }
}
