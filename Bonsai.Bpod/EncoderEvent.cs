using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Bpod
{
    public class EncoderEvent : Combinator<EncoderDataFrame, EncoderEventFrame>
    {
        public override IObservable<EncoderEventFrame> Process(IObservable<EncoderDataFrame> source)
        {
            return source.Where(frame => frame.OpCode == OpCode.Event).Select(frame => (EncoderEventFrame)frame);
        }
    }
}
