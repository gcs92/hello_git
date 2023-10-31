using Google.Protobuf.WellKnownTypes;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading;
using System.Windows.Forms.Design;
using System.IO;

namespace vlcplay
{
    public partial class Flow : Form
    {
        private List<double> data = new List<double>();
        long num_max = 0;
        long num_min = 100;
        long dt_last = 0;
        private static Service service;
        private bool flagWatch = false;
        private delegate void RefreshPortStateDelegate();
        private delegate void PrintMsgDelegate(byte printflag, string type, byte[] command);
        static Inihelper ini_data = new Inihelper(Directory.GetCurrentDirectory() + "/config.ini");
        public Flow()
        {
            InitializeComponent();
            SetChartStyle();
            InitService();//初始化串口
            DefaultConfig();//串口默认值
            StartReadThread();//开启读线程
            timer_chart.Enabled = true;
        }
        /// <summary>
        /// char初始化，用于简单配置，结合控件属性一起使用(这里使用的值可以在控件属性里面去更改，写在这里便于观看)
        /// </summary>
        public void SetChartStyle() // 图表初始化设置
        {
            chart1.Titles.Add("Real-time Chart Demo");
            chart1.ChartAreas[0].AxisX.Title = "Time";
            chart1.ChartAreas[0].AxisY.Title = "Value";

            ////缩放功能开启
            chart1.ChartAreas[0].CursorX.Interval = 0;
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;//仅对X轴放大
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;//仅对X轴放大
            chart1.MouseWheel += new System.Windows.Forms.MouseEventHandler(chart_MouseWheel);
            chart1.ChartAreas[0].AxisX.ScrollBar.Size = 5;//设置滚动条宽度


            chart1.ChartAreas[0].AxisX.ScaleView.Size = 2000;
            chart1.ChartAreas[0].AxisX.ScaleView.MinSize = 0;//
            chart1.ChartAreas[0].AxisY.ScaleView.Size = 40;
            chart1.ChartAreas[0].AxisY.ScaleView.MinSize = 0;//

            // chart1.Series.Add("Series");
            // chart1.Series["Series"].ChartType = SeriesChartType.Line;
            data.Add(0);
            chart1.Series["Series"].Points.DataBindY(data);
            chart1.Refresh();

        }
        /// <summary>
        /// 鼠标滑轮事件，用于放大缩小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chart_MouseWheel(object sender, MouseEventArgs e)
        {
            Chart chart1 = sender as Chart;

            // 实验发现鼠标滚轮滚动一圈时e.Delta = 120，正反转对应正负120
            if (chart1.ChartAreas[0].AxisX.ScaleView.Size + (((-e.Delta) / 120) * 100) > 0) // 防止越过左边界
            {
                if (chart1.ChartAreas[0].AxisX.ScaleView.Size + (((-e.Delta) / 120) * 100) <= chart1.ChartAreas[0].AxisX.Maximum)
                {
                    chart1.ChartAreas[0].AxisX.ScaleView.Size += (((-e.Delta) / 120) * 100); // 每次缩放1
                }
                if (chart1.ChartAreas[0].AxisX.ScaleView.Size < 100)
                {
                    chart1.ChartAreas[0].AxisX.ScaleView.Size = 100;
                }
            }
            else if ((-e.Delta) > 0)
            {
                chart1.ChartAreas[0].AxisX.ScaleView.Size += (((-e.Delta) / 120) * 100); // 每次缩放1
            }
            textBox_max.Text = "范围："+chart1.ChartAreas[0].AxisX.ScaleView.Size.ToString();
            textBox_min.Text = "增加："+(-e.Delta).ToString();
            /*//右键恢复事件
            if (e.Button == MouseButtons.Right)
            {
                chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset(0);
            }*/
        }
        /// <summary>
        /// 定时器，自动生成数值，用于调试画图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_chart_Tick(object sender, EventArgs e)
        {
            try
            {
                long value = (long)new Random().NextDouble() * 4 + 18;
                if (num_max < value)
                {
                    num_max = value;
                    textBox_max.Text = num_max.ToString();
                }
                if (num_min > value)
                {
                    num_min = value;
                    textBox_min.Text = num_min.ToString();
                }
                textBox_now.Text = value.ToString();
                // 添加到数据源中
                data.Add(value);

                // 如果数据源超过了一定长度，就删除最前面的数据
                if (data.Count > chart1.ChartAreas[0].AxisX.Maximum)
                {
                    data.RemoveAt(0);
                }
                // 绑定数据并刷新Chart控件
                chart1.Series["Series"].Points.DataBindY(data);
                chart1.Refresh();
            }
            catch
            {

            }
        }
        /// <summary>
        /// 画点函数
        /// </summary>
        /// <param name="ex"></param>
        private void Drow_tool(long dt_delay)
        {
            try
            {
                if (num_max < dt_delay)
                {
                    num_max = dt_delay;
                    textBox_max.Text = num_max.ToString();
                }
                if (num_min > dt_delay)
                {
                    num_min = dt_delay;
                    textBox_min.Text = num_min.ToString();
                }
                textBox_now.Text = dt_delay.ToString();
                // 添加到数据源中
                data.Add(dt_delay);

                // 如果数据源超过了一定长度，就删除最前面的数据
                if (data.Count > chart1.ChartAreas[0].AxisX.Maximum)
                {
                    data.RemoveAt(0);
                }
                // 绑定数据并刷新Chart控件
                chart1.Series["Series"].Points.DataBindY(data);
                chart1.Refresh();
            }
            catch
            {

            }
        }
        private void ShowException(Exception ex)
        {
            string msg = string.Format("{0}\r\n{1}", ex.Message, ex.StackTrace);
            MessageBox.Show(msg);
        }
        /// <summary>
        /// 加载串口名称事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxPortName_Click(object sender, EventArgs e)
        {
            try
            {
                string[] portNameArr = service.GetPortName();
                this.comboBoxPortName.Items.Clear();
                this.comboBoxPortName.Items.AddRange(portNameArr);
                // this.comboBoxPortName.Items.AddRange(new object[] {
                // "COM1",
                // "COM2",
                // "COM3",
                // "COM4"});
                if (portNameArr.Length > 0)
                {
                    this.comboBoxPortName.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        /// <summary>
        ///更新串口状态 
        /// </summary>
        private void RefreshPortState()
        {
            if (base.InvokeRequired)
            {
                base.Invoke(new RefreshPortStateDelegate(this.RefreshPortState));
            }
            else
            {
                this.btn_open.Text = "打开串口";
                this.btnPortState.BackColor = Color.FromArgb(0, 192, 0);
                ComboBoxPortName_Click(null, null);
            }
        }
        /// <summary>
        /// 监视串口状态
        /// </summary>
        private void WatchPortState()
        {
            flagWatch = true;
            while (flagWatch)
            {
                if (service.GetPortName().Length == 0 || !service.GetPortState())
                {
                    RefreshPortState();
                    flagWatch = false;
                }
                Thread.Sleep(1);

            }
        }

        /// <summary>
        /// 初始化服务
        /// </summary>
        private void InitService()
        {
            if (service == null)
            {
                service = new Service();
            }
        }
        /// <summary>
        /// 初始化串口默认配置
        /// </summary>
        private void DefaultConfig()
        {
            ComboBoxPortName_Click(null, null);
            string stemp = ini_data.ReadValue("PORT", "BuadRate");
            if (stemp.Length <= 0)
            {
                this.comboBoxBaudRate.SelectedIndex = 12;
            }
            else {
                this.comboBoxBaudRate.Text = stemp;
            }
            stemp = ini_data.ReadValue("PORT", "DataBits");
            if (stemp.Length <= 0)
            {
                this.comboBoxDataBits.SelectedIndex = 3;
            }
            else
            {
                this.comboBoxDataBits.Text = stemp;
            }
            stemp = ini_data.ReadValue("PORT", "StopBits");
            if (stemp.Length <= 0)
            {
                this.comboBoxStopBits.SelectedIndex = 1;
            }
            else
            {
                this.comboBoxStopBits.Text = stemp;
            }
            stemp = ini_data.ReadValue("PORT", "Parity");
            if (stemp.Length <= 0)
            {
                this.comboBoxParity.SelectedIndex = 0;
            }
            else
            {
                this.comboBoxParity.Text = stemp;
            }
        }
        /// <summary>
        /// 字节转字符串 中间用空格隔开
        /// </summary>
        /// <returns></returns>
        private string ByteToString(byte[] command)
        {
            StringBuilder commandStr = new StringBuilder();
            foreach (byte item in command)
            {
                commandStr.Append(item.ToString("x2"));
                commandStr.Append(" ");
            }
            return commandStr.ToString();
        }
        /// <summary>
        /// 打印报文
        /// </summary>
        /// <param name="command"></param>
        private void PrintMsg(byte printflag, string type, byte[] command = null)
        {
            if (base.InvokeRequired)
            {
                base.Invoke(new PrintMsgDelegate(this.PrintMsg), new object[] { printflag, type, command });
            }
            else
            {
                if (printflag == 1)
                {
                    if (command == null)
                    {
                        if (type == "\r\n")
                        {
                            if (this.richTextBox_show.Text.Length != 0)
                            {
                                this.richTextBox_show.AppendText("\r\n");
                            }
                        }
                        else
                        {
                            this.richTextBox_show.AppendText(string.Format("{0} {1}\r\n", DateTime.Now, type));
                        }
                    }
                    else
                    {
                        if (type == "接收")
                        {
                            this.richTextBox_show.SelectionColor = Color.Blue;
                            long dt_now = DateTime.Now.ToFileTime();
                            long dt_delay = (dt_now - dt_last)/10000;
                            if (dt_delay < 1000)
                            {
                                Drow_tool(dt_delay);
                            }
                            this.richTextBox_show.AppendText(string.Format("{0} {1}：{2}：{3}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), type, dt_delay, ByteToString(command)));
                            dt_last = dt_now;
                        }
                        else if (type == "发送")
                        {
                            this.richTextBox_show.SelectionColor = Color.Green;
                            this.richTextBox_show.AppendText(string.Format("{0} {1}：{2}\r\n", DateTime.Now, type, ByteToString(command)));
                        }
                        else
                        {
                            this.richTextBox_show.AppendText(string.Format("{0} {1}：{2}\r\n", DateTime.Now, type, ByteToString(command)));
                        }
                        this.richTextBox_show.SelectionColor = Color.Black;
                    }
                }
            }
        }
        /// <summary>
        /// 处理报文
        /// </summary>
        /// <param name="buffer"></param>
        private void dealReadMsg(byte[] buffer)
        {
            PrintMsg(1, "接收", buffer);//打印信息
            if (buffer.Length < 8)
            {
                return;
            }
        }
            /// <summary>
            /// 读取报文
            /// </summary>
        private void Read()
        {
            while (true)
            {
                byte[] buffer = service.ReadFromQueue();
                
                if (buffer.Length > 0)
                {
                    dealReadMsg(buffer);//处理报文
                }
                Thread.Sleep(1);
            }
        }
        /// <summary>
        /// 开启读取线程
        /// </summary>
        private void StartReadThread()
        {
            Thread readThread = new Thread(new ThreadStart(Read))
            {
                IsBackground = true
            };
            readThread.Start();
        }
        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_open_Click(object sender, EventArgs e)
        {
            try
            {
                if (!service.GetPortState())
                {
                    if (this.comboBoxPortName.SelectedItem == null
                        || this.comboBoxBaudRate.SelectedItem == null
                        || this.comboBoxDataBits.SelectedItem == null
                        || this.comboBoxStopBits.SelectedItem == null
                        || this.comboBoxParity.SelectedItem == null)
                    {
                        MessageBox.Show("请选择串口配置");
                        return;
                    }
                    Dictionary<string, string> portConfig = new Dictionary<string, string>();
                    portConfig.Add("PortName", this.comboBoxPortName.Text);
                    portConfig.Add("BaudRate", this.comboBoxBaudRate.Text);
                    portConfig.Add("DataBits", this.comboBoxDataBits.Text);
                    portConfig.Add("StopBits", this.comboBoxStopBits.Text);
                    portConfig.Add("Parity", this.comboBoxParity.Text);
                    service.OpenService(portConfig);
                    this.btn_open.Text = "关闭串口";
                    this.btnPortState.BackColor = Color.Red;
                    Thread watchThread = new Thread(new ThreadStart(WatchPortState))
                    {
                        IsBackground = true
                    };
                    watchThread.Start();

                }
                else
                {
                    service.CloseService();
                    flagWatch = false;
                    this.btn_open.Text = "打开串口";
                    this.btnPortState.BackColor = Color.FromArgb(0, 192, 0);
                }
            }
            catch (Exception ex)
            {
                string[] portNameArr = service.GetPortName();
                for (int i = 0; i < portNameArr.Length; i++)
                {
                    if (this.comboBoxPortName.Text == portNameArr[i])
                    {
                        MessageBox.Show("串口正在被占用");
                        return;
                    }
                }
                MessageBox.Show("串口不存在");

                //ShowException(ex); 

                return;

            }
        }
        /// <summary>
        /// 关闭前保存参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Flow_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 写入ini
            ini_data.WriteValue("PORT", "BuadRate", this.comboBoxBaudRate.Text);
            ini_data.WriteValue("PORT", "DataBits", this.comboBoxDataBits.Text);
            ini_data.WriteValue("PORT", "StopBits", this.comboBoxStopBits.Text);
            ini_data.WriteValue("PORT", "Parity", this.comboBoxParity.Text);
        }
    }
}
