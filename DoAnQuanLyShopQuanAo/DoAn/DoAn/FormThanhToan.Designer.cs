namespace DoAnQuanLyShopQuanAo
{
    partial class FormThanhToan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.lbTongTien = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTienCuaKhach = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lbTraTienThua = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbTong = new System.Windows.Forms.Label();
            this.lbTenKhachHang = new System.Windows.Forms.Label();
            this.lbMaHoaDon = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(163, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(327, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Thông tin thanh toán";
            // 
            // lbTongTien
            // 
            this.lbTongTien.AutoSize = true;
            this.lbTongTien.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTongTien.Location = new System.Drawing.Point(73, 163);
            this.lbTongTien.Name = "lbTongTien";
            this.lbTongTien.Size = new System.Drawing.Size(115, 27);
            this.lbTongTien.TabIndex = 1;
            this.lbTongTien.Text = "Tổng tiền: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(73, 212);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 27);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tiền khách đưa";
            // 
            // txtTienCuaKhach
            // 
            this.txtTienCuaKhach.Font = new System.Drawing.Font("Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.txtTienCuaKhach.Location = new System.Drawing.Point(269, 212);
            this.txtTienCuaKhach.Name = "txtTienCuaKhach";
            this.txtTienCuaKhach.Size = new System.Drawing.Size(125, 24);
            this.txtTienCuaKhach.TabIndex = 3;
            this.txtTienCuaKhach.Text = "0";
            this.txtTienCuaKhach.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTienCuaKhach_KeyDown);
            this.txtTienCuaKhach.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTienCuaKhach_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(73, 266);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(211, 27);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tiền thừa trả khách: ";
            // 
            // lbTraTienThua
            // 
            this.lbTraTienThua.AutoSize = true;
            this.lbTraTienThua.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTraTienThua.ForeColor = System.Drawing.Color.Blue;
            this.lbTraTienThua.Location = new System.Drawing.Point(264, 266);
            this.lbTraTienThua.Name = "lbTraTienThua";
            this.lbTraTienThua.Size = new System.Drawing.Size(86, 27);
            this.lbTraTienThua.TabIndex = 5;
            this.lbTraTienThua.Text = "0 VNĐ ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(73, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 27);
            this.label5.TabIndex = 6;
            this.label5.Text = "Mã hóa đơn:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(73, 115);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(177, 27);
            this.label6.TabIndex = 7;
            this.label6.Text = "Tên khách hàng: ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(349, 322);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(175, 22);
            this.label7.TabIndex = 8;
            this.label7.Text = "Nhân viên bán hàng: ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(349, 354);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 22);
            this.label8.TabIndex = 9;
            this.label8.Text = "Thời gian:";
            // 
            // lbTong
            // 
            this.lbTong.AutoSize = true;
            this.lbTong.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTong.ForeColor = System.Drawing.Color.Red;
            this.lbTong.Location = new System.Drawing.Point(264, 163);
            this.lbTong.Name = "lbTong";
            this.lbTong.Size = new System.Drawing.Size(86, 27);
            this.lbTong.TabIndex = 10;
            this.lbTong.Text = "0 VNĐ ";
            // 
            // lbTenKhachHang
            // 
            this.lbTenKhachHang.AutoSize = true;
            this.lbTenKhachHang.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTenKhachHang.ForeColor = System.Drawing.Color.SteelBlue;
            this.lbTenKhachHang.Location = new System.Drawing.Point(264, 115);
            this.lbTenKhachHang.Name = "lbTenKhachHang";
            this.lbTenKhachHang.Size = new System.Drawing.Size(49, 27);
            this.lbTenKhachHang.TabIndex = 11;
            this.lbTenKhachHang.Text = "Tên";
            // 
            // lbMaHoaDon
            // 
            this.lbMaHoaDon.AutoSize = true;
            this.lbMaHoaDon.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMaHoaDon.ForeColor = System.Drawing.Color.SteelBlue;
            this.lbMaHoaDon.Location = new System.Drawing.Point(264, 70);
            this.lbMaHoaDon.Name = "lbMaHoaDon";
            this.lbMaHoaDon.Size = new System.Drawing.Size(49, 27);
            this.lbMaHoaDon.TabIndex = 12;
            this.lbMaHoaDon.Text = "Tên";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(419, 202);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 43);
            this.button1.TabIndex = 13;
            this.button1.Text = "Tính tiền";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(77, 337);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(135, 48);
            this.button2.TabIndex = 14;
            this.button2.Text = "IN HÓA ĐƠN";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FrmThanhToan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 415);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbMaHoaDon);
            this.Controls.Add(this.lbTenKhachHang);
            this.Controls.Add(this.lbTong);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbTraTienThua);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTienCuaKhach);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbTongTien);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Symbol", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmThanhToan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmThanhToan";
            this.Load += new System.EventHandler(this.FrmThanhToan_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbTongTien;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTienCuaKhach;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbTraTienThua;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbTong;
        private System.Windows.Forms.Label lbTenKhachHang;
        private System.Windows.Forms.Label lbMaHoaDon;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}