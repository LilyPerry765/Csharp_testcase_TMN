using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TMN
{
    class SerialPortEmulator
    {
        private static bool isAccessed;

        private Thread dataReceivingThread;

        public SerialPortEmulator()
        {
            dataReceivingThread = new Thread(((param) =>
            {
                foreach (var item in data)
                {
                    if (DateTime.Now.Second != 0) // <- Noise or failoure simulation
                        DataReceived(this, null);
                }

            }))
            {
                Name = "DataReceiving"
            };
        }

        internal void Dispose()
        {
            isAccessed = false;
        }

        public bool IsOpen
        {
            get;
            set;
        }

        internal void Close()
        {
            isAccessed = false;
            IsOpen = false;
        }

        internal void Open()
        {
            if (isAccessed)
            {
                throw new AccessViolationException();
            }
            isAccessed = true;
            IsOpen = true;
        }

        byte currentModule;
        internal void Write(byte[] request, int p, int p_2)
        {
            currentModule = request[1];
            index = 0;
            if (DataReceived != null)
            {
                //while (dataReceivingThread.ThreadState == ThreadState.Running)
                //{
                //    Thread.Sleep(100);
                //}

                dataReceivingThread.Start();
            }
        }


        static int index = 0;
        byte?[] data = { 0xAA, null, 50, 50, 0x55 };
        internal byte ReadByte()
        {
            if (index >= data.Length)
            {
                index = 0;
            }
            return data[index++] ?? currentModule;
        }

        public int BaudRate
        {
            get;
            set;
        }

        public System.IO.Ports.Parity Parity
        {
            get;
            set;
        }

        public int DataBits
        {
            get;
            set;
        }

        public string PortName
        {
            get;
            set;
        }

        public System.IO.Ports.StopBits StopBits
        {
            get;
            set;
        }

        public System.IO.Ports.SerialDataReceivedEventHandler DataReceived
        {
            get;
            set;
        }

        public System.IO.Ports.SerialErrorReceivedEventHandler ErrorReceived
        {
            get;
            set;
        }
    }
}
