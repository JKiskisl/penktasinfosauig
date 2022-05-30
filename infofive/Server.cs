using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace infofive
{
    class Server
    {
        public static void Start(int port, ref BigInteger[] keyPub, ref string message, ref string signature)
        {
            try
            {
                IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse("0.0.0.0"), port);
                var listener = new TcpListener(ipEnd);
                listener.Start();
                while (true)
                {
                    var client = listener.AcceptTcpClient();
                    var packet = new Reader(client.GetStream());
                    keyPub[0] = BigInteger.Parse(packet.ReadMessage());
                    keyPub[1] = BigInteger.Parse(packet.ReadMessage());
                    message = packet.ReadMessage();
                    signature = packet.ReadMessage();
                    break;
                }
                listener.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
