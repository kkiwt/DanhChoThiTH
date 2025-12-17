namespace Bai4
{
    partial class Dashboard
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
            NutClient = new Button();
            Server = new Button();
            SuspendLayout();
            // 
            // NutClient
            // 
            NutClient.Location = new Point(98, 71);
            NutClient.Margin = new Padding(2, 2, 2, 2);
            NutClient.Name = "NutClient";
            NutClient.Size = new Size(153, 35);
            NutClient.TabIndex = 0;
            NutClient.Text = "Client";
            NutClient.UseVisualStyleBackColor = true;
            NutClient.Click += NutClient_Click;
            // 
            // Server
            // 
            Server.Location = new Point(371, 74);
            Server.Margin = new Padding(2, 2, 2, 2);
            Server.Name = "Server";
            Server.Size = new Size(171, 33);
            Server.TabIndex = 1;
            Server.Text = "Server";
            Server.UseVisualStyleBackColor = true;
            Server.Click += Server_Click;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(640, 170);
            Controls.Add(Server);
            Controls.Add(NutClient);
            Margin = new Padding(2, 2, 2, 2);
            Name = "Dashboard";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button NutClient;
        private Button Server;
    }
}
