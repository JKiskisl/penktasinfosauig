using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace infofive
{
    class Reader
    {
        private readonly NetworkStream ns;
        public Reader(NetworkStream networkStream)
        {
            ns = networkStream;
        }

        public int ReadOpCode()
        {
            return ns.ReadByte();
        }

        public string ReadMessage()
        {
            byte[] msgLengthBuffer = new byte[4];
            ns.Read(msgLengthBuffer, 0, 4);
            var length = BitConverter.ToInt32(msgLengthBuffer, 0);
            byte[] msgBuffer = new byte[length];
            ns.Read(msgBuffer, 0, length);
            return Encoding.ASCII.GetString(msgBuffer);
        }
    }
}
