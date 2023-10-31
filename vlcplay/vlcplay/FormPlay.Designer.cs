
namespace vlcplay
{
    partial class FormPlay
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button_start = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_main_camera = new System.Windows.Forms.Button();
            this.button_clean = new System.Windows.Forms.Button();
            this.button_send = new System.Windows.Forms.Button();
            this.richTextBox_msg = new System.Windows.Forms.RichTextBox();
            this.button_flow_camera = new System.Windows.Forms.Button();
            this.button_stop = new System.Windows.Forms.Button();
            this.button_disconnection = new System.Windows.Forms.Button();
            this.button_connection = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.richTextBox_log = new System.Windows.Forms.RichTextBox();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.comboBox_url = new System.Windows.Forms.ComboBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_start
            // 
            this.button_start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_start.Location = new System.Drawing.Point(699, 36);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(75, 23);
            this.button_start.TabIndex = 0;
            this.button_start.Text = "播放";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.button_main_camera);
            this.panel1.Controls.Add(this.button_clean);
            this.panel1.Controls.Add(this.button_send);
            this.panel1.Controls.Add(this.richTextBox_msg);
            this.panel1.Controls.Add(this.button_flow_camera);
            this.panel1.Controls.Add(this.button_stop);
            this.panel1.Controls.Add(this.button_start);
            this.panel1.Location = new System.Drawing.Point(0, 521);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(850, 58);
            this.panel1.TabIndex = 1;
            // 
            // button_main_camera
            // 
            this.button_main_camera.Location = new System.Drawing.Point(606, 36);
            this.button_main_camera.Name = "button_main_camera";
            this.button_main_camera.Size = new System.Drawing.Size(68, 23);
            this.button_main_camera.TabIndex = 12;
            this.button_main_camera.Text = "切换主摄";
            this.button_main_camera.UseVisualStyleBackColor = true;
            this.button_main_camera.Click += new System.EventHandler(this.button_main_camera_Click);
            // 
            // button_clean
            // 
            this.button_clean.Location = new System.Drawing.Point(84, 32);
            this.button_clean.Name = "button_clean";
            this.button_clean.Size = new System.Drawing.Size(60, 23);
            this.button_clean.TabIndex = 10;
            this.button_clean.Text = "清屏";
            this.button_clean.UseVisualStyleBackColor = true;
            this.button_clean.Click += new System.EventHandler(this.button_clean_Click);
            // 
            // button_send
            // 
            this.button_send.Location = new System.Drawing.Point(5, 32);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(75, 23);
            this.button_send.TabIndex = 11;
            this.button_send.Text = "发送";
            this.button_send.UseVisualStyleBackColor = true;
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // richTextBox_msg
            // 
            this.richTextBox_msg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_msg.Location = new System.Drawing.Point(3, 3);
            this.richTextBox_msg.Name = "richTextBox_msg";
            this.richTextBox_msg.Size = new System.Drawing.Size(844, 23);
            this.richTextBox_msg.TabIndex = 9;
            this.richTextBox_msg.Text = "";
            // 
            // button_flow_camera
            // 
            this.button_flow_camera.Location = new System.Drawing.Point(511, 36);
            this.button_flow_camera.Name = "button_flow_camera";
            this.button_flow_camera.Size = new System.Drawing.Size(89, 23);
            this.button_flow_camera.TabIndex = 6;
            this.button_flow_camera.Text = "切换光流镜头";
            this.button_flow_camera.UseVisualStyleBackColor = true;
            this.button_flow_camera.Click += new System.EventHandler(this.button_flow_camera_Click);
            // 
            // button_stop
            // 
            this.button_stop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_stop.Enabled = false;
            this.button_stop.Location = new System.Drawing.Point(781, 36);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(75, 23);
            this.button_stop.TabIndex = 1;
            this.button_stop.Text = "停止";
            this.button_stop.UseVisualStyleBackColor = true;
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // button_disconnection
            // 
            this.button_disconnection.Location = new System.Drawing.Point(86, 79);
            this.button_disconnection.Name = "button_disconnection";
            this.button_disconnection.Size = new System.Drawing.Size(64, 23);
            this.button_disconnection.TabIndex = 8;
            this.button_disconnection.Text = "断开连接";
            this.button_disconnection.UseVisualStyleBackColor = true;
            this.button_disconnection.Click += new System.EventHandler(this.button_disconnection_Click);
            // 
            // button_connection
            // 
            this.button_connection.Location = new System.Drawing.Point(12, 79);
            this.button_connection.Name = "button_connection";
            this.button_connection.Size = new System.Drawing.Size(63, 23);
            this.button_connection.TabIndex = 7;
            this.button_connection.Text = "连接";
            this.button_connection.UseVisualStyleBackColor = true;
            this.button_connection.Click += new System.EventHandler(this.button_connection_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel2.Location = new System.Drawing.Point(0, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(850, 382);
            this.panel2.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.richTextBox_log);
            this.panel3.Controls.Add(this.textBox_port);
            this.panel3.Controls.Add(this.textBox3);
            this.panel3.Controls.Add(this.button_disconnection);
            this.panel3.Controls.Add(this.comboBox_url);
            this.panel3.Controls.Add(this.textBox2);
            this.panel3.Controls.Add(this.button_connection);
            this.panel3.Controls.Add(this.textBox_ip);
            this.panel3.Controls.Add(this.progressBar1);
            this.panel3.Location = new System.Drawing.Point(-7, 391);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(863, 124);
            this.panel3.TabIndex = 4;
            // 
            // richTextBox_log
            // 
            this.richTextBox_log.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_log.Location = new System.Drawing.Point(156, 25);
            this.richTextBox_log.Name = "richTextBox_log";
            this.richTextBox_log.ReadOnly = true;
            this.richTextBox_log.Size = new System.Drawing.Size(701, 77);
            this.richTextBox_log.TabIndex = 6;
            this.richTextBox_log.Text = "";
            this.richTextBox_log.TextChanged += new System.EventHandler(this.richTextBox_log_TextChanged);
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(50, 52);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(100, 21);
            this.textBox_port.TabIndex = 5;
            this.textBox_port.Text = "4646";
            // 
            // textBox3
            // 
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Location = new System.Drawing.Point(12, 56);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(32, 14);
            this.textBox3.TabIndex = 4;
            this.textBox3.Text = "Port:";
            // 
            // comboBox_url
            // 
            this.comboBox_url.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_url.FormattingEnabled = true;
            this.comboBox_url.Items.AddRange(new object[] {
            "rtsp://hy_syma:ls155633@192.168.100.1:19200/HYSymaH265VideoSMS",
            "rtsp://hy_syma:ls155633@192.168.100.1:19200/HYSymaH264VideoSMS",
            "rtsp://hy_kwt:kwt123456@192.168.16.25:19200/HYKwtH265VideoSMS",
            "rtsp://hy_kwt:kwt123456@192.168.16.25:19200/HYKwtH264VideoSMS"});
            this.comboBox_url.Location = new System.Drawing.Point(0, 6);
            this.comboBox_url.Name = "comboBox_url";
            this.comboBox_url.Size = new System.Drawing.Size(860, 20);
            this.comboBox_url.TabIndex = 1;
            this.comboBox_url.Text = "rtsp://hy_syma:ls155633@192.168.100.1:19200/HYSymaH265VideoSMS";
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(26, 32);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(18, 14);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "IP:";
            // 
            // textBox_ip
            // 
            this.textBox_ip.Location = new System.Drawing.Point(50, 29);
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.Size = new System.Drawing.Size(100, 21);
            this.textBox_ip.TabIndex = 2;
            this.textBox_ip.Text = "192.168.100.1";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(3, 108);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(857, 13);
            this.progressBar1.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormPlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 579);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FormPlay";
            this.Text = "播放窗体";
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox comboBox_url;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox_ip;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.Button button_connection;
        private System.Windows.Forms.Button button_flow_camera;
        private System.Windows.Forms.Button button_disconnection;
        private System.Windows.Forms.RichTextBox richTextBox_log;
        private System.Windows.Forms.RichTextBox richTextBox_msg;
        private System.Windows.Forms.Button button_clean;
        private System.Windows.Forms.Button button_send;
        private System.Windows.Forms.Button button_main_camera;
    }
}