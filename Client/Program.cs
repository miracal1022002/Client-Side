using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
class Program
{
    static string serverIP;
    static Int32 serverPort;
    static void Main(string[] args)
    {
        connection:
        try
        {
            TcpClient client = new TcpClient();
            serverIP = "192.168.64.128";
            serverPort = 1302;
            while (!client.Connected)
            {
                try
                {
                    client.Connect(serverIP, serverPort);
                    Console.WriteLine("Connected to server successfully \nServer IP: " + serverIP + " --- Server Port: " + serverPort);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Failed to connect to server");
                }
            }
            Console.WriteLine("Please enter your name: ");
            string? clientRequest = Console.ReadLine();
            int byteCount = Encoding.ASCII.GetByteCount(clientRequest+1);
            byte[] sendData = new byte[byteCount];
            sendData = Encoding.ASCII.GetBytes(clientRequest);

            NetworkStream stream = client.GetStream();
            stream.Write(sendData, 0, sendData.Length);
            Console.WriteLine("Request sent to server");

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