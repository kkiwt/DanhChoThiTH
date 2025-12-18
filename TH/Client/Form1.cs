using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;

namespace testBaoCao
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

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (_client == null || !_client.Connected)
            {
                MessageBox.Show("Chưa kết nối server");
                return;
            }

            if (!int.TryParse(txtTableNumber.Text, out int table))
            {
                MessageBox.Show("Số bàn không hợp lệ");
                return;
            }

            if (dgvMenu.CurrentRow == null)
            {
                MessageBox.Show("Chọn 1 món trước");
                return;
            }

            int id = Convert.ToInt32(dgvMenu.CurrentRow.Cells["colId"].Value);

            int qty = 0;
            var val = dgvMenu.CurrentRow.Cells["colQty"].Value;
            if (val != null)
                int.TryParse(val.ToString(), out qty);

            if (qty <= 0)
            {
                MessageBox.Show("Số lượng phải > 0");
                return;
            }

            string cmd = $"ORDER {table} {id} {qty}";
            _writer.WriteLine(cmd);

            string resp = _reader.ReadLine();
            if (resp.StartsWith("OK"))
            {
                var parts = resp.Split(' ');
                string money = parts.Length > 1 ? parts[1] : "";
                MessageBox.Show("Đặt thành công! Thành tiền: " + money);
            }
            else if (resp.StartsWith("ERROR"))
            {
                MessageBox.Show("Lỗi từ server: " + resp);
            }
        }

        private void txtTableNumber_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
