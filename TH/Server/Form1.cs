using System.Collections.Concurrent;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Linq;


namespace Server
{
    public partial class Form1 : Form
    {
        private TcpListener _listener;
        private bool _isRunning = false;
        private List<MenuItem> _menu = new List<MenuItem>();

        private ConcurrentDictionary<int, List<OrderItem>> _ordersByTable
            = new ConcurrentDictionary<int, List<OrderItem>>();

        private BindingList<OrderItem> _ordersView = new BindingList<OrderItem>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitMenuFileAndLoad();
        }
        private void InitMenuFileAndLoad()
        {
            string exeDir = AppDomain.CurrentDomain.BaseDirectory;
            string menuPath = Path.Combine(exeDir, "menu.txt");
            if (!File.Exists(menuPath))
            {
                string[] defaultMenu =
                {
            "1;Phở Bò;50000",
            "2;Cơm Tấm;40000",
            "3;Gỏi Cuốn;30000",
            "4;Bún Chả;45000",
            "5;Bánh Mì;20000"
        };

                File.WriteAllLines(menuPath, defaultMenu);
            }

            LoadMenuFromFile(menuPath);
        }


        private void LoadMenuFromFile(string path)
        {
            _menu.Clear();
            foreach (var line in File.ReadAllLines(path))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var parts = line.Split(';');
                if (parts.Length < 3) continue;

                _menu.Add(new MenuItem
                {
                    Id = int.Parse(parts[0]),
                    Name = parts[1],
                    Price = int.Parse(parts[2])
                });
            }
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            _listener = new TcpListener(IPAddress.Any, 9000);
            _listener.Start();
            _isRunning = true;
            AppendStatus("Server started at port 9000");

            while (_isRunning)
            {
                var client = await _listener.AcceptTcpClientAsync();
                AppendStatus("Client connected");
                _ = Task.Run(() => HandleClient(client));
            }
        }
        private void AppendStatus(string msg)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AppendStatus), msg);
                return;
            }
            lstStatus.Items.Add(msg);
        }
        private void HandleClient(TcpClient client)
        {
            using (client)
            using (var stream = client.GetStream())
            using (var reader = new StreamReader(stream))
            using (var writer = new StreamWriter(stream) { AutoFlush = true })
            {
                try
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(' ');
                        if (parts.Length == 0) continue;

                        string cmd = parts[0].ToUpper();

                        if (cmd == "MENU")
                        {
                            string menuStr = string.Join("|",
                                _menu.Select(m => $"{m.Id};{m.Name};{m.Price}"));
                            writer.WriteLine(menuStr);
                        }
                        else if (cmd == "ORDER")
                        {
                            if (parts.Length < 4)
                            {
                                writer.WriteLine("ERROR Sai cú pháp ORDER");
                                continue;
                            }

                            int table = int.Parse(parts[1]);
                            int id = int.Parse(parts[2]);
                            int qty = int.Parse(parts[3]);

                            var item = _menu.FirstOrDefault(m => m.Id == id);
                            if (item == null || qty <= 0)
                            {
                                writer.WriteLine("ERROR Món không tồn tại hoặc số lượng không hợp lệ");
                                continue;
                            }

                            var order = new OrderItem
                            {
                                TableNumber = table,
                                Item = item,
                                Quantity = qty
                            };

                            var list = _ordersByTable.GetOrAdd(table, _ => new List<OrderItem>());
                            lock (list)
                            {
                                list.Add(order);
                            }

                            this.Invoke(new Action(() =>
                            {
                                _ordersView.Add(order);
                            }));

                            writer.WriteLine($"OK {order.Total}");
                        }
                        else if (cmd == "QUIT")
                        {
                            writer.WriteLine("BYE");
                            break;
                        }
                        else
                        {
                            writer.WriteLine("ERROR Lệnh không hỗ trợ");
                        }
                    }
                }
                catch (Exception ex)
                {
                    AppendStatus("Client error: " + ex.Message);
                }
            }

            AppendStatus("Client disconnected");
        }

    }
}
