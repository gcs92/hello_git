using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using System.Windows.Forms;

namespace vlcplay
{
    class Service
    {
        private static SerialPort serialPort = new SerialPort();
        private static readonly ConcurrentQueue<byte[]> readQueue = new ConcurrentQueue<byte[]>();
        private static readonly ConcurrentQueue<byte[]> writeQueue = new ConcurrentQueue<byte[]>();
        private List<byte> readBuffer = new List<byte>();
        private bool flagRead = false;
        private bool isRead = false;
        private bool flagWrite = false;

        /// <summary>
        /// 开启服务
        /// </summary>
        public void OpenService(Dictionary<string, string> config)
        {
            serialPort.PortName = GetConfig("PortName", config["PortName"]);
            serialPort.BaudRate = GetConfig("BaudRate", config["BaudRate"]);
            serialPort.DataBits = GetConfig("DataBits", config["DataBits"]);
            serialPort.StopBits = GetConfig("StopBits", config["StopBits"]);
            serialPort.Parity = GetConfig("Parity", config["Parity"]);
            serialPort.Handshake = Handshake.None;
            serialPort.ReadTimeout = 10;
            serialPort.WriteTimeout = 10;
            serialPort.Open();
            Thread readThread = new Thread(new ThreadStart(Read))
            {
                IsBackground = true
            };
            readThread.Start();
            Thread writeThread = new Thread(new ThreadStart(Write))
            {
                IsBackground = true
            };
            writeThread.Start();
        }
        /// <summary>
        /// 关闭服务
        /// </summary>
        public void CloseService()
        {
            serialPort.Close();
            flagRead = false;
            flagWrite = false;
        }
        /// <summary>
        /// 获取串口数据
        /// </summary>
        /// <returns></returns>
        public string[] GetPortName()
        {
            return SerialPort.GetPortNames();
        }
        /// <summary>
        /// 获取串口状态
        /// </summary>
        /// <returns></returns>
        public bool GetPortState()
        {
            return serialPort.IsOpen;
        }
        /// <summary>
        /// 写入发送队列
        /// </summary>
        public void WriteToQueue(byte[] buffer)
        {
            writeQueue.Enqueue(buffer);
        }
        /// <summary>
        /// 从队列读取
        /// </summary>
        public byte[] ReadFromQueue()
        {
            byte[] buffer = new byte[1024];
            if (readQueue.Count > 0)
            {
                if (readQueue.TryDequeue(out buffer) == true)
                {
                    return buffer;
                }
            }
            return ClearZero(buffer);
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private dynamic GetConfig(string key, string value)
        {
            dynamic result = null;
            switch (key)
            {
                case "PortName":
                    result = value;
                    break;
                case "BaudRate":
                    result = int.Parse(value);
                    break;
                case "Parity":
                    switch (value)
                    {
                        case "None":
                            result = Parity.None;
                            break;
                        case "Odd":
                            result = Parity.Odd;
                            break;
                        case "Even":
                            result = Parity.Even;
                            break;
                        case "Mark":
                            result = Parity.Mark;
                            break;
                        case "Space":
                            result = Parity.Space;
                            break;
                        default:
                            break;
                    }
                    break;
                case "DataBits":
                    result = int.Parse(value);
                    break;
                case "StopBits":
                    switch (value)
                    {
                        case "None":
                            result = StopBits.None;
                            break;
                        case "One":
                            result = StopBits.One;
                            break;
                        case "Two":
                            result = StopBits.Two;
                            break;
                        case "OnePointFive":
                            result = StopBits.OnePointFive;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            return result;
        }
        /// <summary>
        /// 读取数据并存入栈
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Read()
        {
            flagRead = true;
            while (flagRead)
            {

                //接收数据
                try
                {
                    byte bt = (byte)serialPort.ReadByte();
                    if (bt == 0x68)
                    {
                        isRead = true;
                    }
                    if (isRead)
                    {
                        readBuffer.Add(bt);
                        
                        if (readBuffer.Count >= 9)
                        {
                            //MessageBox.Show(readBuffer[1].ToString() + ":" + readBuffer.Count.ToString());
                            if (true)//(readBuffer[1]+3 == readBuffer.Count)
                            {
                                isRead = false;
                                readQueue.Enqueue(readBuffer.ToArray());
                                readBuffer.Clear();
                            }
                        }

                    }
                }
                catch (InvalidOperationException ex)
                {
                    flagRead = false;
                }
                catch (Exception)
                {
                }
            }
        }
        /// <summary>
        /// 从栈获取数据并发送
        /// </summary>
        private void Write()
        {
            flagWrite = true;

            while (flagWrite)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    writeQueue.TryDequeue(out buffer);
                    buffer = ClearZero(buffer);
                    serialPort.Write(buffer, 0, buffer.Length);

                }
                catch (InvalidOperationException ex)
                {
                    flagWrite = false;
                }
                catch (Exception)
                {

                }
                Thread.Sleep(1);
            }
        }
        /// <summary>
        /// 清除多余0x00
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private byte[] ClearZero(byte[] buffer)
        {
            if (buffer != null)
            {
                int Length = 0;
                for (int i = buffer.Length - 1; i >= 0; i--)
                {
                    if (buffer[i] == 0x16)
                    {
                        Length = i + 1;
                        break;
                    }
                }
                byte[] buffer2 = new byte[Length];
                Array.Copy(buffer, buffer2, Length);
                return buffer2;
            }
            return new byte[0];
        }
    }
}
