using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Bai4
{
    public partial class Server : Form
    {
        private TcpListener listener;
        private bool isListening = false;
        private Dictionary<string, FilmInfo> films = new();
        private HashSet<string> bookedSeats = new();
        private List<TcpClient> connectedClients = new();

        public Server()
        {
            InitializeComponent();
        }

        // Khi nhấn "Listen" để bật server
        private async void btnListen_Click(object sender, EventArgs e)
        {
            if (isListening)
            {
                Log("Server đã chạy rồi!");
                return;
            }

            LoadFilmData("input5.txt");

            listener = new TcpListener(IPAddress.Any, 8080);
            listener.Start();
            isListening = true;

            Log("Server đang chạy trên cổng 8080...");

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                connectedClients.Add(client);
                Log($"Client {client.Client.RemoteEndPoint} đã kết nối");
                _ = HandleClient(client);
            }
        }

        //Xử lý từng client
        private async Task HandleClient(TcpClient client)
        {
            using NetworkStream ns = client.GetStream();
            byte[] buffer = new byte[4096];

            while (true)
            {
                int bytes = 0;
                try
                {
                    bytes = await ns.ReadAsync(buffer, 0, buffer.Length);
                }
                catch { break; }

                if (bytes == 0) break;

                string req = Encoding.UTF8.GetString(buffer, 0, bytes);
                string res = ProcessRequest(req, client);

                byte[] data = Encoding.UTF8.GetBytes(res);
                await ns.WriteAsync(data, 0, data.Length);
            }

            Log($"Client {client.Client.RemoteEndPoint} đã ngắt kết nối");
        }

        // Xử lý request JSON từ client
        private string ProcessRequest(string json, TcpClient client)
        {
            try
            {
                dynamic req = JsonConvert.DeserializeObject(json);
                string action = req.action;

                if (action == "get_films")
                    return JsonConvert.SerializeObject(films, Formatting.Indented);

                if (action == "get_booked")
                    return JsonConvert.SerializeObject(bookedSeats, Formatting.Indented);

                if (action == "book")
                {
                    string film = req.film;
                    string theater = req.theater;
                    string seat = req.seat;
                    string key = $"{film}_{theater}_{seat}";

                    if (bookedSeats.Contains(key))
                    {
                        Log($"{client.Client.RemoteEndPoint} cố đặt ghế đã có: {key}");
                        return JsonConvert.SerializeObject(new { status = "failed", msg = "Ghế đã được đặt!" }, Formatting.Indented);
                    }

                    bookedSeats.Add(key);
                    Log($"{client.Client.RemoteEndPoint} đặt vé thành công: {key} | Tổng vé: {bookedSeats.Count}");
                    return JsonConvert.SerializeObject(new { status = "ok", msg = "Đặt vé thành công!" }, Formatting.Indented);
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { status = "error", msg = ex.Message }, Formatting.Indented);
            }

            return JsonConvert.SerializeObject(new { status = "error", msg = "Lệnh không hợp lệ!" }, Formatting.Indented);
        }

        // Đọc dữ liệu phim từ file input5.txt
        private void LoadFilmData(string path)
        {
            films.Clear();

            if (!File.Exists(path))
            {
                Log($"Không tìm thấy file {path}");
                return;
            }

            string[] lines = File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i++)
            {
                string name = lines[i].Trim();
                if (string.IsNullOrWhiteSpace(name)) continue;

                if (decimal.TryParse(lines[i + 1].Trim(), out decimal price))
                {
                    string[] theaters = lines[i + 2].Trim().Split(',');
                    films[name] = new FilmInfo
                    {
                        Name = name,
                        BasePrice = price,
                        Theaters = theaters.Select(t => t.Trim()).ToList()
                    };
                    i += 2;
                }
            }

            Log($"Đã tải {films.Count} phim từ file {path}");
        }

        // Ghi log ra textbox
        private void Log(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Log(message)));
                return;
            }

            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\r\n");
        }

        // Tắt server
        private void button1_Click(object sender, EventArgs e)
        {
            if (!isListening)
            {
                Log("Server chưa chạy!");
                return;
            }

            isListening = false;
            listener.Stop();

            foreach (var c in connectedClients)
            {
                try { c.Close(); } catch { }
            }
            connectedClients.Clear();

            Log("Server đã tắt!");
        }
    }

    // Thông tin phim
    public class FilmInfo
    {
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public List<string> Theaters { get; set; }
        public int TotalSeats { get; } = 15;
    }
}
