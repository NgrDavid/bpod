using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Bpod
{
    public class EncoderPositionFrame : EncoderDataFrame
    {
        public EncoderPositionFrame(byte[] packet)
            : base(packet)
        {
            Position = BitConverter.ToInt16(packet, 1);
        }

        public short Position { get; private set; }
    }
}
