namespace Bai4
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void NutClient_Click(object sender, EventArgs e)
        {
            ClientKhachHang KH = new ClientKhachHang();
            KH.Show();
            this.Hide();
        }

        private void Server_Click(object sender, EventArgs e)
        {
            Server Svr = new Server();
            Svr.Show();
            this.Hide();
        }
    }
}
