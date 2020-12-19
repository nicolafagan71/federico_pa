using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace ClientChat
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TcpClient tClient = new TcpClient("localhost", 10000);
                Console.WriteLine("Connesso al server...\r");

                Thread t = new Thread(Read);
                t.Start(tClient);

                StreamWriter sWriter = new StreamWriter(tClient.GetStream());

                while (true)
                {
                    if (tClient.Connected)
                    {
                        string input = Console.ReadLine();
                        sWriter.WriteLine(input);
                        sWriter.Flush();
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }

            Console.ReadKey();
        }

        static void Read(object obj)
        {
            TcpClient tClient = (TcpClient)obj;
            StreamReader sReader = new StreamReader(tClient.GetStream());

            while (true)
            {
                try
                {
                    string message = sReader.ReadLine();
                    Console.WriteLine(message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    break;
                }
            }
        }
    }
}
