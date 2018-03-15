using Bonsai;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;

namespace Bonsai.Bpod
{
    public class Encoder : Source<EncoderDataFrame>
    {
        const int PacketLength = 7;

        [TypeConverter(typeof(SerialPortNameConverter))]
        public string PortName { get; set; }

        public override IObservable<EncoderDataFrame> Generate()
        {
            return Observable.Using(
                () =>
                {
                    var port = new SerialPort(PortName);
                    port.BaudRate = 115200;
                    port.DtrEnable = true;
                    port.Open();
                    port.ReadExisting();
                    port.Write("C");
                    port.ReadByte();
                    port.Write("S1");
                    return port;
                },
                serialPort => Observable.Create<EncoderDataFrame>(observer =>
                {
                    var bufferedStream = new BufferedStream(serialPort.BaseStream, serialPort.ReadBufferSize);
                    var packet = new byte[PacketLength];

                    SerialDataReceivedEventHandler dataReceivedHandler;
                    dataReceivedHandler = (sender, e) =>
                    {
                        var bytesToRead = serialPort.BytesToRead;
                        bufferedStream.PushBytes(bytesToRead);

                        while (bytesToRead >= PacketLength)
                        {
                            bytesToRead -= bufferedStream.Read(packet, 0, packet.Length);
                            switch (packet[0])
                            {
                                case (byte)OpCode.Position:
                                    observer.OnNext(new EncoderPositionFrame(packet));
                                    break;
                                case (byte)OpCode.Event:
                                    observer.OnNext(new EncoderEventFrame(packet));
                                    break;
                            }
                        }
                    };

                    serialPort.DataReceived += dataReceivedHandler;
                    return Disposable.Create(() =>
                    {
                        serialPort.DataReceived -= dataReceivedHandler;
                    });
                }));
        }
    }
}
