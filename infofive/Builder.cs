using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infofive
{
    class Builder
    {
        private MemoryStream ms;

        public Builder()
        {
            ms = new MemoryStream();
        }

        public void WriteOpCode(byte opCode)
        {
            ms.WriteByte(opCode);
        }

        public void WriteMessage(string message)
        {
            var msgLength = message.Length;
            ms.Write(BitConverter.GetBytes(msgLength), 0, 4);
            ms.Write(Encoding.ASCII.GetBytes(message), 0, msgLength);
        }

        public byte[] GetPacket()
        {
            return ms.ToArray();
        }
    }
}
