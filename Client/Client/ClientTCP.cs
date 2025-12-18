// ClientTCP.cs

using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class ClientTCP
    {
        private readonly string Host;
        private readonly int Port;

        public ClientTCP(string host = "127.0.0.1", int port = 8000)
        {
            Host = host;
            Port = port;
        }

        public async Task<string> SendMessageAsync(string message)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    await client.ConnectAsync(Host, Port);
                    var stream = client.GetStream();


                    byte[] data = Encoding.UTF8.GetBytes(message);
                    await stream.WriteAsync(data, 0, data.Length);


                    byte[] lengthBytes = new byte[4];
                    int totalLengthRead = 0;
                    while (totalLengthRead < 4)
                    {
                        int bytesRead = await stream.ReadAsync(lengthBytes, totalLengthRead, 4 - totalLengthRead);
                        if (bytesRead == 0) return "ERROR: Phản hồi lỗi hoặc không đủ thông tin độ dài.";
                        totalLengthRead += bytesRead;
                    }

                    int totalLength = BitConverter.ToInt32(lengthBytes, 0);


                    byte[] messageBuffer = new byte[totalLength];
                    int totalBytesRead = 0;

                    while (totalBytesRead < totalLength)
                    {
                        int bytesRead = await stream.ReadAsync(
                            messageBuffer,
                            totalBytesRead,
                            totalLength - totalBytesRead
                        );

                        if (bytesRead == 0)
                        {
                            return "ERROR: Kết nối bị ngắt trước khi nhận đủ dữ liệu.";
                        }
                        totalBytesRead += bytesRead;
                    }
                    string response = Encoding.UTF8.GetString(messageBuffer, 0, totalBytesRead);


                    response = response
                        .Replace("\0", "")
                        .Replace("\r", "")
                        .Replace("\n", "")
                        .Trim();

                    client.Close();
                    return response;
                }
            }
            catch (Exception ex)
            {
                return $"ERROR: {ex.Message}";
            }
        }
    }
}