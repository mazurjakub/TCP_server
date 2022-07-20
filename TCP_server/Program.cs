using System;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace TCP_server
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                TcpListener listener = new TcpListener(System.Net.IPAddress.Any, 1700);
                listener.Start();
                while (true)
                {
                    Console.WriteLine("Čekám na spojení.");
                    TcpClient client = listener.AcceptTcpClient();
                    Console.WriteLine("Klient přijat." + client.ToString());
                    NetworkStream stream = client.GetStream();
                    StreamReader sr = new StreamReader(client.GetStream());
                    StreamWriter sw = new StreamWriter(client.GetStream());
                    try
                    {
                        byte[] buffer = new byte[1024];
                        stream.Read(buffer, 0, buffer.Length);
                        int recv = 0;
                        foreach (byte b in buffer)
                        {
                            if (b != 0)
                            {
                                recv++;
                            }
                        }
                        string request = Encoding.UTF8.GetString(buffer, 0, recv);
                        Console.WriteLine("Žádost klienta přijata: " + request);
                        sw.WriteLine("Žádost úspěšně přijata.\n");
                        sw.Flush();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Něco se nepovedlo.");
                        sw.WriteLine(e.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}