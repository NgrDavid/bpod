using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Bpod
{
    public class EncoderPosition : Combinator<EncoderDataFrame, EncoderPositionFrame>
    {
        public override IObservable<EncoderPositionFrame> Process(IObservable<EncoderDataFrame> source)
        {
            return source.Where(frame => frame.OpCode == OpCode.Position).Select(frame => (EncoderPositionFrame)frame);
        }
    }
}
