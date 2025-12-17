using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace Bai4
{
    public class NetworkClient
    {
        private TcpClient client;
        private NetworkStream ns;

        public void Connect(string ip = "127.0.0.1", int port = 8080) // ket noi den server 
        {
            client = new TcpClient(ip, port);
            ns = client.GetStream();
        }

        public T SendRequest<T>(object request) // gui yeu cầu
        {
            string json = JsonConvert.SerializeObject(request, Formatting.Indented);
            byte[] data = Encoding.UTF8.GetBytes(json);
            ns.Write(data, 0, data.Length);

            byte[] buffer = new byte[4096];
            int bytes = ns.Read(buffer, 0, buffer.Length);
            string response = Encoding.UTF8.GetString(buffer, 0, bytes);
            return JsonConvert.DeserializeObject<T>(response);
        }
        public void Disconnect()
        {
            ns?.Close();
            client?.Close();
        }
    }
}
