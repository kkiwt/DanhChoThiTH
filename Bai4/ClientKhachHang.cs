using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Bai4
{
    public partial class Client : Form
    {
        private Dictionary<string, FilmInfo> filmData = new(); // Dữ liệu phim từ Server
        private HashSet<string> bookedSeat = new();            // Danh sách ghế đã đặt (đồng bộ từ Server)
        private List<(string Film, string Theater, string Seat)> choosingSeat = new();
        private NetworkClient netClient = new();
        private System.Windows.Forms.Timer syncTimer;

        public Client()
        {
            InitializeComponent();
            richTextBox1.ReadOnly = true;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;

            // Kết nối tới Server khi khởi động
            try
            {
                netClient.Connect();
                MessageBox.Show("Đã kết nối tới Server!", "Thông báo");
                LoadFilmDataFromServer();
                LoadBookedSeatsFromServer();
                syncTimer = new System.Windows.Forms.Timer();
                syncTimer.Interval = 2000; // 2000ms = 2 giây
                syncTimer.Tick += SyncTimer_Tick;
                syncTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể kết nối Server: {ex.Message}");
                return;
            }

            // Tạo danh sách ghế
            for (int i = 0; i < 3; i++)
            {
                for (int j = 1; j <= 5; j++)
                    checkedListBox1.Items.Add((char)('A' + i) + j.ToString());
            }

            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            Them.Click += Them_Click;
            Xoa.Click += Xoa_Click;
            ThanhToan.Click += ThanhToan_Click;
            NutDatTiep.Click += NutDatTiep_Click;
            NutThongKe.Click += NutThongKe_Click;
        }

        // 🔹 Lấy danh sách phim từ Server
        private void LoadFilmDataFromServer()
        {
            filmData = netClient.SendRequest<Dictionary<string, FilmInfo>>(new { action = "get_films" });
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(filmData.Keys.ToArray());

            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        // 🔹 Lấy danh sách ghế đã đặt từ Server
        private void LoadBookedSeatsFromServer()
        {
            bookedSeat = netClient.SendRequest<HashSet<string>>(new { action = "get_booked" });
        }

        private string GetTypeSeat(string seat)
        {
            if (seat == "A1" || seat == "A5" || seat == "C1" || seat == "C5") return "Vớt";
            else if (seat.StartsWith("A") || seat.StartsWith("C")) return "Thường";
            else return "VIP";
        }

        private decimal CalculatePrice(decimal basePrice, string seat)
        {
            string type = GetTypeSeat(seat);
            return type switch
            {
                "Vớt" => basePrice * 0.25m,
                "VIP" => basePrice * 2m,
                _ => basePrice
            };
        }

        // Khi chọn phim → nạp danh sách rạp tương ứng
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            resetCheckedListBox();

            if (comboBox1.SelectedIndex == -1) return;
            string selectedFilm = comboBox1.SelectedItem.ToString();

            if (filmData.ContainsKey(selectedFilm))
            {
                comboBox2.Items.AddRange(filmData[selectedFilm].Theaters.ToArray());
                comboBox2.SelectedIndex = 0;
            }
        }

        // Khi chọn rạp → hiển thị ghế đã bị đặt (đồng bộ)
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            resetCheckedListBox();
            LoadBookedSeatsFromServer(); // luôn đồng bộ danh sách mới nhất

            if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1) return;
            string film = comboBox1.SelectedItem.ToString();
            string theater = comboBox2.SelectedItem.ToString();

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                string seat = checkedListBox1.Items[i].ToString();
                string key = $"{film}_{theater}_{seat}";

                if (bookedSeat.Contains(key))
                {
                    checkedListBox1.SetItemChecked(i, true);
                    checkedListBox1.SetItemCheckState(i, CheckState.Indeterminate);
                    checkedListBox1.SetItemEnabled(i, false); // custom disable nếu bạn có extension
                }
            }
        }

        // Nút "Thêm"
        private void Them_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn phim và rạp!", "Thông báo");
                return;
            }

            string film = comboBox1.SelectedItem.ToString();
            string theater = comboBox2.SelectedItem.ToString();
            decimal basePrice = filmData[film].BasePrice;

            var selectedSeats = checkedListBox1.CheckedItems.Cast<string>().ToList();
            if (selectedSeats.Count == 0)
            {
                MessageBox.Show("Hãy chọn ít nhất 1 ghế!");
                return;
            }

            foreach (var seat in selectedSeats)
            {
                string key = $"{film}_{theater}_{seat}";
                if (bookedSeat.Contains(key)) continue;

                if (!choosingSeat.Any(x => x.Film == film && x.Theater == theater && x.Seat == seat))
                {
                    choosingSeat.Add((film, theater, seat));
                    richTextBox1.AppendText($"{film} | Rạp {theater} | Ghế {seat} | {GetTypeSeat(seat)} | {CalculatePrice(basePrice, seat):#,##0}₫\n");
                }
            }
        }

        private void Xoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xóa toàn bộ lựa chọn?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                choosingSeat.Clear();
                richTextBox1.Clear();
                resetCheckedListBox();
            }
        }

        // Nút "Thanh Toán" — gửi yêu cầu đặt vé lên Server
        private void ThanhToan_Click(object sender, EventArgs e)
        {
            if (choosingSeat.Count == 0)
            {
                MessageBox.Show("Chưa chọn vé nào!");
                return;
            }

            string name = InitialClient.ShowInputDialog("Nhập tên khách hàng:", "Thông tin khách hàng");
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Tên không được để trống!");
                return;
            }

            decimal total = 0;
            foreach (var seat in choosingSeat)
            {
                var res = netClient.SendRequest<Dictionary<string, string>>(new
                {
                    action = "book",
                    film = seat.Film,
                    theater = seat.Theater,
                    seat = seat.Seat
                });

                if (res["status"] == "ok")
                {
                    bookedSeat.Add($"{seat.Film}_{seat.Theater}_{seat.Seat}");
                    total += CalculatePrice(filmData[seat.Film].BasePrice, seat.Seat);
                }
                else
                    MessageBox.Show(res["msg"], "Thông báo");
            }

            richTextBox1.AppendText($"\n--- Thanh toán thành công ---\nKhách: {name}\nTổng tiền: {total:#,##0}₫\n----------------------------\n\n");
            choosingSeat.Clear();

            // Làm mới danh sách ghế đã đặt
            LoadBookedSeatsFromServer();
            comboBox2_SelectedIndexChanged(sender, e);
        }

        private void NutDatTiep_Click(object sender, EventArgs e)
        {
            choosingSeat.Clear();
            richTextBox1.Clear();
            LoadBookedSeatsFromServer();
            resetCheckedListBox();
        }

        private void NutThongKe_Click(object sender, EventArgs e)
        {
            InitialClient.ExportStatisticsToFile("output5.txt", filmData, bookedSeat);
        }

        private void resetCheckedListBox()
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
                checkedListBox1.SetItemCheckState(i, CheckState.Unchecked);
            }
        }

        private void NutThoat_Click(object sender, EventArgs e)
        {
            try
            {
                netClient.Disconnect();
                MessageBox.Show("Đã ngắt kết nối khỏi server!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi ngắt kết nối: {ex.Message}");
            }
        }
        private void SyncTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                // Lấy lại danh sách ghế mới nhất
                LoadBookedSeatsFromServer();

                // Nếu đang chọn phim & rạp, cập nhật lại trạng thái ghế hiển thị
                if (comboBox1.SelectedIndex != -1 && comboBox2.SelectedIndex != -1)
                {
                    string film = comboBox1.SelectedItem.ToString();
                    string theater = comboBox2.SelectedItem.ToString();

                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        string seat = checkedListBox1.Items[i].ToString();
                        string key = $"{film}_{theater}_{seat}";

                        if (bookedSeat.Contains(key))
                        {
                            checkedListBox1.SetItemChecked(i, true);
                            checkedListBox1.SetItemCheckState(i, CheckState.Indeterminate);
                            checkedListBox1.SetItemEnabled(i, false);
                        }
                        else
                        {
                            checkedListBox1.SetItemEnabled(i, true);
                            checkedListBox1.SetItemChecked(i, false);
                            checkedListBox1.SetItemCheckState(i, CheckState.Unchecked);
                        }
                    }
                }
            }
            catch
            {
                // Nếu mất kết nối tạm thời, có thể bỏ qua lỗi nhẹ
            }
        }
    }
}
