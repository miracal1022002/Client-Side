using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
class Program
{
    static void Main(string[] args)
    {
        connection:
        try
        {
            TcpClient client = new TcpClient("192.168.64.128", 1302);
            string clientRequest = "Request 1";

            int byteCount = Encoding.ASCII.GetByteCount(clientRequest+1);
            byte[] sendData = new byte[byteCount];
            sendData = Encoding.ASCII.GetBytes(clientRequest);

            NetworkStream stream = client.GetStream();
            stream.Write(sendData, 0, sendData.Length);
            Console.WriteLine("Sending request to server");

            StreamReader sr = new StreamReader(stream);
            string response = sr.ReadLine();
            Console.WriteLine(response);

            stream.Close();
            client.Close();
            Console.ReadKey();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            goto connection;
        }
    }
}