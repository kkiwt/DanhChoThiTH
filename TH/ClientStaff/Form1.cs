using System.Net.Sockets;

namespace ClientStaff
{
    public partial class Form1 : Form
    {
        private TcpClient _client;
        private StreamReader _reader;
        private StreamWriter _writer;
        private List<MenuItem> _menu = new List<MenuItem>();
        public Form1()
        {
            InitializeComponent();
            InitGrid();
        }
        private void InitGrid()
        {
            dgvMenu.AutoGenerateColumns = false;
            dgvMenu.Columns.Clear();

            var colId = new DataGridViewTextBoxColumn();
            colId.Name = "colId";
            colId.HeaderText = "ID";
            dgvMenu.Columns.Add(colId);

            var colName = new DataGridViewTextBoxColumn();
            colName.Name = "colName";
            colName.HeaderText = "Tên món";
            colName.Width = 200;
            dgvMenu.Columns.Add(colName);

            var colPrice = new DataGridViewTextBoxColumn();
            colPrice.Name = "colPrice";
            colPrice.HeaderText = "Giá";
            dgvMenu.Columns.Add(colPrice);

            var colQty = new DataGridViewTextBoxColumn();
            colQty.Name = "colQty";
            colQty.HeaderText = "Số lượng";
            dgvMenu.Columns.Add(colQty);
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                _client = new TcpClient();

                int port = 9000;
                _client.Connect(txtServerIP.Text, port);

                _reader = new StreamReader(_client.GetStream());
                _writer = new StreamWriter(_client.GetStream()) { AutoFlush = true };

                MessageBox.Show("Kết nối thành công");

                _writer.WriteLine("MENU");
                string menuStr = _reader.ReadLine();

                LoadMenuFromServerString(menuStr);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kết nối thất bại: " + ex.Message);
            }
        }


        private void LoadMenuFromServerString(string menuStr)
        {
            _menu.Clear();
            dgvMenu.Rows.Clear();

            if (string.IsNullOrEmpty(menuStr))
                return;

            foreach (var itemStr in menuStr.Split('|'))
            {
                if (string.IsNullOrWhiteSpace(itemStr)) continue;
                var parts = itemStr.Split(';');
                if (parts.Length < 3) continue;

                var item = new MenuItem
                {
                    Id = int.Parse(parts[0]),
                    Name = parts[1],
                    Price = int.Parse(parts[2])
                };
                _menu.Add(item);

                int rowIndex = dgvMenu.Rows.Add();
                var row = dgvMenu.Rows[rowIndex];
                row.Cells["colId"].Value = item.Id;
                row.Cells["colName"].Value = item.Name;
                row.Cells["colPrice"].Value = item.Price;
                row.Cells["colQty"].Value = 0;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void btnTinhTien_Click(object sender, EventArgs e)
        {

        }
    }
}
