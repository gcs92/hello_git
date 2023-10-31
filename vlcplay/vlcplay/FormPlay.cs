using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;//DllImport 支持 文件包
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace vlcplay
{
    public partial class FormPlay : Form
    {
        /// <summary>
        /// windowapi 找到指定窗体的句柄函数
        /// </summary>
        /// <param name="lpClassName">窗体类名</param>
        /// <param name="lpWindowName">窗体标题名</param>
        /// <returns>返回窗体句柄（IntPtr）</returns>
        ///[DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        ///public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        VLCPlayer vlcplayer = new VLCPlayer();
        ProgressBarText ProgressBarT = new ProgressBarText();
        double all_num;

        //定义回调
        private delegate void SetTextCallBack(string strValue);
        //声明
        private SetTextCallBack setCallBack;

        //定义接收服务端发送消息的回调
        private delegate void ReceiveMsgCallBack(string strMsg);
        //声明
        private ReceiveMsgCallBack receiveCallBack;

        //创建连接的Socket
        Socket socketSend;
        //创建接收客户端发送消息的线程
        Thread threadReceive;

        public FormPlay()
        {
            InitializeComponent();
            ProgressBarT.Font = Font;
            ProgressBarT.Text = "0%";
            ProgressBarT.ForeColor = Color.Blue;
            ProgressBarT.Control = progressBar1;
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            this.Play();
            this.button_start.Enabled = false;
            comboBox_url.Enabled = false;
            button_stop.Enabled = true;

            //this.Play();
            /*vlcplayer.Stop();
            vlcplayer.release();
            this.Close();
            Formdisplay form = new Formdisplay();
            form.ShowDialog();
            */
        }

        private void Play()
        {
            bool fd;
            string str_name = comboBox_url.Text;
            //string str_name = "rtsp://hy_syma:ls155633@192.168.100.1:19200/HYSymaH265VideoSMS";
            //string str_name = "G:\\test.mp4";
            fd = vlcplayer.playUrl(str_name, this.panel2.Handle);// 这里的这个panel是winform窗体的一个panel的控件
            MessageBox.Show(fd.ToString());
            //fd = vlcplayer.playLocalVideo("G:\\test.mp4", this.panel2.Handle);
            all_num = vlcplayer.Durations(str_name);
            
            if (all_num != 0)
            {
                this.progressBar1.Value = 0;
                this.progressBar1.Minimum = 0;
                this.progressBar1.Maximum = (int)(all_num / 1000);
                timer1.Enabled = true;
            }
            //vlcplayer.Aspect("4:3");//这个是设置屏幕比的`
        }

        /// <summary>
        /// 根据屏幕自适应
        /// </summary>
        /// <param name="tags">屏幕占比</param>
        private void FullScreenChange(string tags)
        {
            Action action = () => {
                vlcplayer.Aspect(tags);
            };
            action.Invoke();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string text_name;
            double new_num = 0;
            if (all_num != 0)
            {
                if (this.progressBar1.Value < this.progressBar1.Maximum)
                {
                    new_num = vlcplayer.GetTime();
                    this.progressBar1.Value = (int)(new_num / 1000);
                    text_name = (new_num / 1000).ToString() + "/" + (all_num / 1000).ToString();
                    this.ProgressBarT.Text = text_name;

                }
                else
                {
                    timer1.Enabled = false;
                    comboBox_url.Enabled = true;
                    MessageBox.Show("播放完成！！！");
                    this.button_start.Enabled = true;
                    vlcplayer.release();
                }
            }
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            comboBox_url.Enabled = true;
            this.button_start.Enabled = true;
            button_stop.Enabled = false;
            vlcplayer.release();
        }
        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_connection_Click(object sender, EventArgs e)
        {
            try
            {
                socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Parse(this.textBox_ip.Text.Trim());
                socketSend.Connect(ip, Convert.ToInt32(this.textBox_port.Text.Trim()));
                //实例化回调
                setCallBack = new SetTextCallBack(SetValue);
                receiveCallBack = new ReceiveMsgCallBack(SetValue);
                this.richTextBox_log.Invoke(setCallBack, "连接成功");

                //开启一个新的线程不停的接收服务器发送消息的线程
                threadReceive = new Thread(new ThreadStart(Receive));
                //设置为后台线程
                threadReceive.IsBackground = true;
                threadReceive.Start();

                button_connection.Enabled = false;
                button_disconnection.Enabled = true;
                textBox_ip.Enabled = false;
                textBox_port.Enabled = false;
            }
            catch (Exception ex)
            {
                if (ex.ToString() != null)
                {
                    MessageBox.Show("连接服务端失败！请确认IP/端口是否正常");
                }
            }
        }

        /// <summary>
        /// 接口服务器发送的消息
        /// </summary>
        private void Receive()
        {
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[2048];
                    //实际接收到的字节数
                    int count = socketSend.Receive(buffer);
                    if (count == 0)
                    {
                        return;
                    }
                    else
                    {
                        //判断发送的数据的类型
                        if (true)// (buffer[0] == 0)//表示发送的是文字消息
                        { 
                            string str = Encoding.Default.GetString(buffer, 0, count).Replace("\0","");//没有.Replace("\0","") 后面加的字符串将显示不了
                            string output_log = "接收：" + str;
                            this.richTextBox_log.Invoke(receiveCallBack, output_log);
                            //this.richTextBox_log.Invoke(receiveCallBack, "接收远程服务器:" + socketSend.RemoteEndPoint + "发送的消息:" + str+ "\r\n");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("接收服务端发送的消息出错:" + ex.ToString());
            }
        }


        private void SetValue(string strValue)
        {
            this.richTextBox_log.AppendText(strValue + "\r\n");
        }


        private void FrmClient_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_disconnection_Click(object sender, EventArgs e)
        {
            //关闭socket
            socketSend.Close();
            //终止线程
            threadReceive.Abort();
            button_disconnection.Enabled = false;
            button_connection.Enabled = true;

        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_send_Click(object sender, EventArgs e)
        {
            try
            {
                string strMsg = this.richTextBox_msg.Text.Trim();
                byte[] buffer = new byte[2048];
                buffer = Encoding.Default.GetBytes(strMsg);
                if (button_connection.Enabled == false)
                {
                    this.richTextBox_log.Invoke(receiveCallBack, "发送：" + strMsg);
                    int receive = socketSend.Send(buffer);
                }
                else
                {
                    MessageBox.Show("请先连接服务器！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("发送消息出错:" + ex.Message);
            }
        }

        private void button_clean_Click(object sender, EventArgs e)
        {
            this.richTextBox_log.Clear();
        }

        private void button_flow_camera_Click(object sender, EventArgs e)
        {
            try
            {
                string strMsg = "{\"CMD\":75,\"PARAM\":{\"csi_num\":1}}";
                byte[] buffer = new byte[2048];
                buffer = Encoding.Default.GetBytes(strMsg);
                if (button_connection.Enabled == false)
                {
                    this.richTextBox_log.Invoke(receiveCallBack, "发送：" + strMsg);
                    int receive = socketSend.Send(buffer);
                }
                else {
                    MessageBox.Show("请先连接服务器！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("发送消息出错:" + ex.Message);
            }
        }

        private void button_main_camera_Click(object sender, EventArgs e)
        {
            try
            {
                string strMsg = "{\"CMD\":75,\"PARAM\":{\"csi_num\":0}}";
                byte[] buffer = new byte[2048];
                buffer = Encoding.Default.GetBytes(strMsg);
                if (button_connection.Enabled == false)
                {
                    this.richTextBox_log.Invoke(receiveCallBack, "发送：" + strMsg);
                    int receive = socketSend.Send(buffer);
                }
                else
                {
                    MessageBox.Show("请先连接服务器！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("发送消息出错:" + ex.Message);
            }
        }

        private void richTextBox_log_TextChanged(object sender, EventArgs e)//自动跳到最后一行
        {
            richTextBox_log.SelectionStart = richTextBox_log.Text.Length;
            richTextBox_log.ScrollToCaret();
        }
    }
}
