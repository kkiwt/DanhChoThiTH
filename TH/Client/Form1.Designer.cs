namespace testBaoCao
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
            btnConnect = new Button();
            btnOrder = new Button();
            txtTableNumber = new TextBox();
            txtServerIP = new TextBox();
            dgvMenu = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvMenu).BeginInit();
            SuspendLayout();
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(12, 12);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(94, 29);
            btnConnect.TabIndex = 0;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnOrder
            // 
            btnOrder.Location = new Point(387, 306);
            btnOrder.Name = "btnOrder";
            btnOrder.Size = new Size(65, 63);
            btnOrder.TabIndex = 1;
            btnOrder.Text = "Order ";
            btnOrder.UseVisualStyleBackColor = true;
            btnOrder.Click += btnOrder_Click;
            // 
            // txtTableNumber
            // 
            txtTableNumber.Location = new Point(74, 306);
            txtTableNumber.Name = "txtTableNumber";
            txtTableNumber.Size = new Size(125, 27);
            txtTableNumber.TabIndex = 3;
            txtTableNumber.TextChanged += txtTableNumber_TextChanged;
            // 
            // txtServerIP
            // 
            txtServerIP.Location = new Point(74, 342);
            txtServerIP.Name = "txtServerIP";
            txtServerIP.Size = new Size(125, 27);
            txtServerIP.TabIndex = 4;
            // 
            // dgvMenu
            // 
            dgvMenu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMenu.Location = new Point(12, 47);
            dgvMenu.Name = "dgvMenu";
            dgvMenu.RowHeadersWidth = 51;
            dgvMenu.Size = new Size(440, 248);
            dgvMenu.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 309);
            label1.Name = "label1";
            label1.Size = new Size(56, 20);
            label1.TabIndex = 7;
            label1.Text = "Bàn số:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 345);
            label2.Name = "label2";
            label2.Size = new Size(24, 20);
            label2.TabIndex = 8;
            label2.Text = "IP:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(463, 381);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dgvMenu);
            Controls.Add(txtServerIP);
            Controls.Add(txtTableNumber);
            Controls.Add(btnOrder);
            Controls.Add(btnConnect);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dgvMenu).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnConnect;
        private Button btnOrder;
        private TextBox txtTableNumber;
        private TextBox txtServerIP;
        private DataGridView dgvMenu;
        private Label label1;
        private Label label2;
    }
}
