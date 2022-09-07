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
    public partial class FormPhieuNhap : Form
    {
        public FormPhieuNhap()
        {
            InitializeComponent();
        }

        private void FrmPhieuNhap_Load(object sender, EventArgs e)
        {
            PHIEUNHAPRP phieunhaprp = new PHIEUNHAPRP();
            crystalReportViewer1.ReportSource = phieunhaprp;
            phieunhaprp.SetDatabaseLogon("sa", "123", "LAPTOP-NMNGHIA", "QL_SHOPQUANAO");
            phieunhaprp.SetParameterValue("MaPH", ThongTin.LayThongTinThanhTien());
            phieunhaprp.SetParameterValue("tenNV", ThongTin.LayThongTinTenNV());
            phieunhaprp.SetParameterValue("tenNCC", ThongTin.LayThongTintenKH());
            phieunhaprp.SetParameterValue("thoigian", ThongTin.LayThongTintg());
            crystalReportViewer1.ReportSource = phieunhaprp;
            crystalReportViewer1.Refresh();
        }
    }
}
