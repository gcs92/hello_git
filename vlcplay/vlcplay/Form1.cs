using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace vlcplay
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        private Thread th;

        private static void VLCMainForm()
        {
            FormPlay form = new FormPlay();
            Application.Run(form);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ///不关闭窗体
            //FormPlay form = new FormPlay();
            //form.ShowDialog();

            ///关闭此窗体
            th = new Thread(new ThreadStart(VLCMainForm));
            th.Start();
            this.Close();

        }
        private static void MysqlMainForm()
        {
            Mysql form = new Mysql();
            Application.Run(form);
        }
        private static void SQLiteMainForm()
        {
            SQLite form = new SQLite();
            Application.Run(form);
        }
        private static void FlowMainForm()
        {
            Flow form = new Flow();
            Application.Run(form);
        }
        private void button_Mysql_Click(object sender, EventArgs e)
        {
            ///关闭此窗体
            th = new Thread(new ThreadStart(MysqlMainForm));
            th.Start();
            this.Close();
        }

        private void button_flow_Click(object sender, EventArgs e)
        {
            ///关闭此窗体
            th = new Thread(new ThreadStart(FlowMainForm));
            th.Start();
            this.Close();
        }

        private void button_SQLite_Click(object sender, EventArgs e)
        {
            ///关闭此窗体
            th = new Thread(new ThreadStart(SQLiteMainForm));
            th.Start();
            this.Close();
        }
    }
}
