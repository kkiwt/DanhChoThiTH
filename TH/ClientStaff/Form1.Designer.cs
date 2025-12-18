namespace ClientStaff
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            lstStatus = new ListBox();
            lblTongTien = new Label();
            txtTableToPay = new TextBox();
            btnTinhTien = new Button();
            dgvMenu = new DataGridView();
            btnConnect = new Button();
            txtServerIP = new TextBox();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvMenu).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 297);
            label1.Name = "label1";
            label1.Size = new Size(96, 20);
            label1.TabIndex = 14;
            label1.Text = "Nhập số bàn:";
            // 
            // lstStatus
            // 
            lstStatus.FormattingEnabled = true;
            lstStatus.Location = new Point(463, 41);
            lstStatus.Name = "lstStatus";
            lstStatus.Size = new Size(325, 244);
            lstStatus.TabIndex = 13;
            // 
            // lblTongTien
            // 
            lblTongTien.AutoSize = true;
            lblTongTien.Location = new Point(463, 298);
            lblTongTien.Name = "lblTongTien";
            lblTongTien.Size = new Size(72, 20);
            lblTongTien.TabIndex = 12;
            lblTongTien.Text = "Tổng tiền";
            // 
            // txtTableToPay
            // 
            txtTableToPay.Location = new Point(112, 294);
            txtTableToPay.Name = "txtTableToPay";
            txtTableToPay.Size = new Size(121, 27);
            txtTableToPay.TabIndex = 11;
            // 
            // btnTinhTien
            // 
            btnTinhTien.Location = new Point(239, 294);
            btnTinhTien.Name = "btnTinhTien";
            btnTinhTien.Size = new Size(94, 29);
            btnTinhTien.TabIndex = 10;
            btnTinhTien.Text = "Tính tiền";
            btnTinhTien.UseVisualStyleBackColor = true;
            btnTinhTien.Click += btnTinhTien_Click;
            // 
            // dgvMenu
            // 
            dgvMenu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMenu.Location = new Point(12, 41);
            dgvMenu.Name = "dgvMenu";
            dgvMenu.RowHeadersWidth = 51;
            dgvMenu.Size = new Size(445, 244);
            dgvMenu.TabIndex = 9;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(12, 6);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(94, 29);
            btnConnect.TabIndex = 8;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // txtServerIP
            // 
            txtServerIP.Location = new Point(112, 340);
            txtServerIP.Name = "txtServerIP";
            txtServerIP.Size = new Size(121, 27);
            txtServerIP.TabIndex = 15;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 343);
            label2.Name = "label2";
            label2.Size = new Size(66, 20);
            label2.TabIndex = 16;
            label2.Text = "IP Server";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(796, 398);
            Controls.Add(label2);
            Controls.Add(txtServerIP);
            Controls.Add(label1);
            Controls.Add(lstStatus);
            Controls.Add(lblTongTien);
            Controls.Add(txtTableToPay);
            Controls.Add(btnTinhTien);
            Controls.Add(dgvMenu);
            Controls.Add(btnConnect);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvMenu).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ListBox lstStatus;
        private Label lblTongTien;
        private TextBox txtTableToPay;
        private Button btnTinhTien;
        private DataGridView dgvMenu;
        private Button btnConnect;
        private TextBox txtServerIP;
        private Label label2;
    }
}
