using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Bai4
{
    public class CheckedListBoxExtend : CheckedListBox
    {
        private readonly HashSet<int> disabledItems = new HashSet<int>();

        public void SetItemEnabled(int index, bool enabled)
        {
            if (enabled)
                disabledItems.Remove(index);
            else
                disabledItems.Add(index);

            this.Invalidate(GetItemRectangle(index));
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= Items.Count)
                return;

            bool isDisabled = disabledItems.Contains(e.Index);
            bool isChecked = GetItemChecked(e.Index);

            Color backColor = isDisabled ? SystemColors.ControlLight : e.BackColor;
            Color foreColor = isDisabled ? SystemColors.GrayText : e.ForeColor;

            e.Graphics.FillRectangle(new SolidBrush(backColor), e.Bounds);

            // Vẽ checkbox
            CheckBoxState state = isChecked ? CheckBoxState.CheckedNormal : CheckBoxState.UncheckedNormal;
            CheckBoxRenderer.DrawCheckBox(e.Graphics, new Point(e.Bounds.X + 2, e.Bounds.Y + 1), state);

            // Vẽ text
            TextRenderer.DrawText(
                e.Graphics,
                Items[e.Index].ToString(),
                e.Font,
                new Point(e.Bounds.X + 20, e.Bounds.Y + 1),
                foreColor
            );
        }

        protected override void OnItemCheck(ItemCheckEventArgs ice)
        {
            if (disabledItems.Contains(ice.Index))
            {
                ice.NewValue = ice.CurrentValue; // chặn check/uncheck nếu item bị disable
            }
            base.OnItemCheck(ice);
        }
    }
    public static partial class InitialClient
    {
        public static string ShowInputDialog(string text, string title)
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 180,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = title,
                StartPosition = FormStartPosition.CenterScreen
            };

            Label textLabel = new Label() { Left = 20, Top = 20, Text = text, AutoSize = true };
            TextBox inputBox = new TextBox() { Left = 20, Top = 50, Width = 340 };

            Button confirmation = new Button()
            {
                Text = "OK",
                Left = 270,
                Width = 90,
                Top = 85,
                DialogResult = DialogResult.OK
            };

            confirmation.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : string.Empty;
        }
        public static void ExportStatisticsToFile(string filePath, Dictionary<string, FilmInfo> filmData, HashSet<string> bookedSeat)
        {
            try
            {
                if (filmData == null || filmData.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu phim để thống kê!");
                    return;
                }

                // Gom thống kê số ghế đã đặt theo từng phim
                var stats = new Dictionary<string, int>();
                foreach (var key in bookedSeat)
                {
                    string film = key.Split('_')[0];
                    if (!stats.ContainsKey(film))
                        stats[film] = 0;
                    stats[film]++;
                }

                using (var sw = new System.IO.StreamWriter(filePath, false))
                {
                    sw.WriteLine("=== THỐNG KÊ ĐẶT VÉ ===");
                    sw.WriteLine($"Thời gian: {DateTime.Now}");
                    sw.WriteLine();

                    foreach (var film in filmData.Keys)
                    {
                        int count = stats.ContainsKey(film) ? stats[film] : 0;
                        sw.WriteLine($"{film}: {count} vé đã đặt");
                    }

                    sw.WriteLine("\n--- Tổng cộng ---");
                    sw.WriteLine($"Tổng số vé đã đặt: {stats.Values.Sum()}");
                }

                MessageBox.Show($"Đã xuất thống kê ra file: {filePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi ghi file: {ex.Message}");
            }
        }

    }
}
