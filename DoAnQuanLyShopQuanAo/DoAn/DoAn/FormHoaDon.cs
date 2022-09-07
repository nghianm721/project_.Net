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
    public partial class FormHoaDon : Form
    {
        public FormHoaDon()
        {
            InitializeComponent();
        }

        private void FrmHoaDon_Load(object sender, EventArgs e)
        {
            HoaDonRP hoadonrp = new HoaDonRP();
            crystalReportViewer1.ReportSource = hoadonrp;
            hoadonrp.SetDatabaseLogon("sa", "123", "LAPTOP-NMNGHIA", "QL_SHOPQUANAO");
            hoadonrp.SetParameterValue("MaHD", ThongTin.LayThongTinThanhTien());
            hoadonrp.SetParameterValue("tenNV", ThongTin.LayThongTinTenNV());
            hoadonrp.SetParameterValue("TenKH", ThongTin.LayThongTintenKH());
            hoadonrp.SetParameterValue("thoigian", ThongTin.LayThongTintg());
            crystalReportViewer1.ReportSource = hoadonrp;
            crystalReportViewer1.Refresh();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
