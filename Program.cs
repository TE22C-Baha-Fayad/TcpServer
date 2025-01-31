using System.Data.Common;
using System.Net.Sockets;
using SimpleTCP;
using SimpleTCP.Server;
using TcpServer;
namespace TcpServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Server = new SimpleTcpServer();
            Server.ClientConnected += OnClientConnected;
            Server.DataReceived += OnDataRecieved;
            const int port = 5000;
            Server.Start(port);
            Console.WriteLine($"Server started on port {port}!");
            Console.ReadLine();
        }

        private static void OnClientConnected(object? sender, object e)
        {
            Console.WriteLine("Client Connected");
        }
        private static void OnDataRecieved(object? sender, Message e)
        {
            float[] terms = ByteArrayToFloatArray(e.Data);
            float answer = terms[0] + terms[1];
            e.Reply($"Answer: {answer.ToString()}");
        }
        static float[] ByteArrayToFloatArray(byte[] byteArray)
        {
            // Create a float array with the correct size (each float is 4 bytes)
            float[] floatArray = new float[byteArray.Length / 4];

            // Copy the byte array into the float array
            Buffer.BlockCopy(byteArray, 0, floatArray, 0, byteArray.Length);

            return floatArray;
        }


    }
}