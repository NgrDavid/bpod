using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Bpod
{
    public class EncoderDataFrame
    {
        public EncoderDataFrame(byte[] packet)
        {
            OpCode = (OpCode)packet[0];
            Timestamp = BitConverter.ToUInt32(packet, 3);
        }

        public OpCode OpCode { get; private set; }

        public uint Timestamp { get; private set; }
    }
}
