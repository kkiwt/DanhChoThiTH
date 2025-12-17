namespace Bai4
{
    partial class ClientKhachHang
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
            Them = new Button();
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();
            checkedListBox1 = new CheckedListBoxExtend();
            richTextBox1 = new RichTextBox();
            Xoa = new Button();
            ThanhToan = new Button();
            NutDatTiep = new Button();
            NutThongKe = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            NutThoat = new Button();
            SuspendLayout();
            // 
            // Them
            // 
            Them.Location = new Point(95, 300);
            Them.Margin = new Padding(3, 4, 3, 4);
            Them.Name = "Them";
            Them.Size = new Size(75, 29);
            Them.TabIndex = 0;
            Them.Text = "Thêm";
            Them.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(179, 73);
            comboBox1.Margin = new Padding(3, 4, 3, 4);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 28);
            comboBox1.TabIndex = 1;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(416, 76);
            comboBox2.Margin = new Padding(3, 4, 3, 4);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(121, 28);
            comboBox2.TabIndex = 2;
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(95, 169);
            checkedListBox1.Margin = new Padding(3, 4, 3, 4);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(213, 92);
            checkedListBox1.TabIndex = 3;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(350, 169);
            richTextBox1.Margin = new Padding(3, 4, 3, 4);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(340, 251);
            richTextBox1.TabIndex = 4;
            richTextBox1.Text = "";
            // 
            // Xoa
            // 
            Xoa.Location = new Point(225, 300);
            Xoa.Margin = new Padding(3, 4, 3, 4);
            Xoa.Name = "Xoa";
            Xoa.Size = new Size(75, 29);
            Xoa.TabIndex = 5;
            Xoa.Text = "Xoá";
            Xoa.UseVisualStyleBackColor = true;
            // 
            // ThanhToan
            // 
            ThanhToan.Location = new Point(95, 347);
            ThanhToan.Margin = new Padding(3, 4, 3, 4);
            ThanhToan.Name = "ThanhToan";
            ThanhToan.Size = new Size(106, 29);
            ThanhToan.TabIndex = 6;
            ThanhToan.Text = "Thanh toán";
            ThanhToan.UseVisualStyleBackColor = true;
            // 
            // NutDatTiep
            // 
            NutDatTiep.Location = new Point(225, 347);
            NutDatTiep.Margin = new Padding(3, 4, 3, 4);
            NutDatTiep.Name = "NutDatTiep";
            NutDatTiep.Size = new Size(75, 29);
            NutDatTiep.TabIndex = 7;
            NutDatTiep.Text = "Đặt tiếp";
            NutDatTiep.UseVisualStyleBackColor = true;
            // 
            // NutThongKe
            // 
            NutThongKe.Location = new Point(95, 393);
            NutThongKe.Margin = new Padding(3, 4, 3, 4);
            NutThongKe.Name = "NutThongKe";
            NutThongKe.Size = new Size(88, 29);
            NutThongKe.TabIndex = 8;
            NutThongKe.Text = "Thống kê";
            NutThongKe.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(92, 76);
            label1.Name = "label1";
            label1.Size = new Size(81, 20);
            label1.TabIndex = 10;
            label1.Text = "Chọn phim";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(347, 76);
            label2.Name = "label2";
            label2.Size = new Size(69, 20);
            label2.TabIndex = 11;
            label2.Text = "Chọn rạp";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(92, 135);
            label3.Name = "label3";
            label3.Size = new Size(72, 20);
            label3.TabIndex = 12;
            label3.Text = "Chọn ghế";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(350, 134);
            label4.Name = "label4";
            label4.Size = new Size(89, 20);
            label4.TabIndex = 13;
            label4.Text = "Phim đã đặt";
            // 
            // NutThoat
            // 
            NutThoat.Location = new Point(225, 393);
            NutThoat.Margin = new Padding(3, 4, 3, 4);
            NutThoat.Name = "NutThoat";
            NutThoat.Size = new Size(75, 29);
            NutThoat.TabIndex = 14;
            NutThoat.Text = "Thoát";
            NutThoat.UseVisualStyleBackColor = true;
            NutThoat.Click += NutThoat_Click;
            // 
            // Client
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(727, 524);
            Controls.Add(NutThoat);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(NutThongKe);
            Controls.Add(NutDatTiep);
            Controls.Add(ThanhToan);
            Controls.Add(Xoa);
            Controls.Add(richTextBox1);
            Controls.Add(checkedListBox1);
            Controls.Add(comboBox2);
            Controls.Add(comboBox1);
            Controls.Add(Them);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Client";
            Text = "Form6";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Them;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private CheckedListBoxExtend checkedListBox1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button Xoa;
        private System.Windows.Forms.Button ThanhToan;
        private System.Windows.Forms.Button NutDatTiep;
        private System.Windows.Forms.Button NutThongKe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button NutThoat;
    }
}