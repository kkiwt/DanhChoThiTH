namespace Bai4
{
    partial class Server
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
            btnListen = new Button();
            txtLog = new RichTextBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // btnListen
            // 
            btnListen.Location = new Point(153, 4);
            btnListen.Margin = new Padding(2);
            btnListen.Name = "btnListen";
            btnListen.Size = new Size(115, 32);
            btnListen.TabIndex = 0;
            btnListen.Text = "Listen";
            btnListen.UseVisualStyleBackColor = true;
            btnListen.Click += btnListen_Click;
            // 
            // txtLog
            // 
            txtLog.Location = new Point(78, 40);
            txtLog.Margin = new Padding(2);
            txtLog.Name = "txtLog";
            txtLog.Size = new Size(575, 361);
            txtLog.TabIndex = 1;
            txtLog.Text = "";
            // 
            // button1
            // 
            button1.Location = new Point(439, 6);
            button1.Name = "button1";
            button1.Size = new Size(167, 29);
            button1.TabIndex = 2;
            button1.Text = "Stop Connection";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Server
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(757, 426);
            Controls.Add(button1);
            Controls.Add(btnListen);
            Controls.Add(txtLog);
            Margin = new Padding(2);
            Name = "Server";
            Text = "Server";
            ResumeLayout(false);
        }

        #endregion

        private Button btnListen;
        private RichTextBox txtLog;
        private Button button1;
    }
}