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
    public partial class FormDoanhThu : Form
    {
        public FormDoanhThu()
        {
            InitializeComponent();
        }

        private void DoanhThu_Load(object sender, EventArgs e)
        {
            DoanhThuRP hienthi = new DoanhThuRP();
            crystalReportViewer1.ReportSource = hienthi;
            hienthi.SetDatabaseLogon("sa", "123", "LAPTOP-NMNGHIA", "QL_SHOPQUANAO");
        }
    }
}
