using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Bpod
{
    public class EncoderEventFrame : EncoderDataFrame
    {
        public EncoderEventFrame(byte[] packet)
            : base(packet)
        {
            Source = (EventSource)packet[1];
            Data = packet[2];
        }

        public EventSource Source { get; private set; }

        public byte Data { get; private set; }
    }
}
