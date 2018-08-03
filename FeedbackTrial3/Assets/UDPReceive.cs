using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Security.Cryptography;

namespace UDP_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Send("TEST STRING");
            Console.Read();
        }
        static void Send(string Message)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPAddress broadcast = IPAddress.Parse("192.168.1.130");
            byte[] sendbuf = Encoding.ASCII.GetBytes(Message);
            IPEndPoint ep = new IPEndPoint(broadcast, 11000);
            s.SendTo(sendbuf, ep);
        }
    }
}