using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ServerChat
{
    class Program
    {
        private static TcpListener tListener;
        private static List<TcpClient> tClientList = new List<TcpClient>();

        static void Main(string[] args)
        {
            tListener = new TcpListener(IPAddress.Any, 10000);
            tListener.Start();

            Console.WriteLine("Server avviato");

            while (true)
            {
                TcpClient tClient = tListener.AcceptTcpClient();
                tClientList.Add(tClient);

                Thread t = new Thread(ThreadClientListener);
                t.Start(tClient);
            }
        }

        public static void ThreadClientListener(object obj)
        {
            TcpClient tClient = (TcpClient)obj;
            StreamReader reader = new StreamReader(tClient.GetStream());

            Console.WriteLine("Client connesso");

            while (true)
            {
                string message = reader.ReadLine();
                BroadCastMessage(message, tClient);
                Console.WriteLine(message);
            }
        }

        public static void BroadCastMessage(string msg, TcpClient tClient)
        {
            foreach (TcpClient c in tClientList)
            {
                if (c != tClient)
                {
                    StreamWriter sWriter = new StreamWriter(c.GetStream());
                    sWriter.WriteLine(msg);
                    sWriter.Flush();
                }
            }
        }
    }
}
