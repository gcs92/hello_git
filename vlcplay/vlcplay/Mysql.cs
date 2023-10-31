using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using Mysqlx.Crud;

namespace vlcplay
{
    public partial class Mysql : Form
    {
        MysqlConnector mc = new MysqlConnector();
        /// <summary>
        /// Directory.GetCurrentDirectory();//获取当前根目录;
        /// </summary>
        static Inihelper ini_data = new Inihelper(Directory.GetCurrentDirectory() + "/config.ini");
        public Mysql()
        {
            InitializeComponent();

            mc.SetServer("127.0.0.1")
              .SetDataBase("mysql")
              .SetUserID("root")
              .SetPassword("12345678")
              .SetPort("3306")
              .SetCharset("utf8");

            // 读取ini
            string stemp = ini_data.ReadValue("Parameter", "cmd_search");
            if (stemp.Length <= 0)
            {
                MessageBox.Show("查询数值为空");
            }
            else {
                textBox_cmd.Text = stemp;
            }
            stemp = ini_data.ReadValue("Parameter", "cmd_add");
            if (stemp.Length <= 0)
            {
                MessageBox.Show("增加数值为空");
            }
            else
            {
                textBox_add.Text = stemp;
            }
            stemp = ini_data.ReadValue("Parameter", "cmd_del");
            if (stemp.Length <= 0)
            {
                MessageBox.Show("删除数值为空");
            }
            else
            {
                textBox_del.Text = stemp;
            }
            stemp = ini_data.ReadValue("Parameter", "cmd_change");
            if (stemp.Length <= 0)
            {
                MessageBox.Show("改变数值为空");
            }
            else
            {
                textBox_change.Text = stemp;
            }

        }

        private void button_search_Click(object sender, EventArgs e)
        {
            string sql = textBox_cmd.Text;
            string result = "";
            int head_flag = 0;
            //执行查询
            MySqlDataReader reader = mc.ExeQuery(sql);
            if (reader == null)
            {
                return;
            }
            while (reader.Read())
            {
                ListViewItem item = new ListViewItem();
                if (head_flag == 0)
                {
                    head_flag = 1;
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        result += reader.GetName(i) + "\t";
                    }
                    result += "\r\n";
                }
                item.Text = listView1.Items.Count.ToString();
                //item.Name = metter;
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    result += reader.GetValue(i) + "\t";
                    item.SubItems.Add(reader.GetValue(i).ToString());
                }
                result += "\r\n";
                listView1.BeginUpdate();
                listView1.Items.Add(item);
                if (listView1.Items.Count > 0)
                {
                    listView1.Items[listView1.Items.Count - 1].EnsureVisible();
                }
                listView1.EndUpdate();
            }
            textBox_show.AppendText(result + "\r\n");
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            string sql = textBox_add.Text;
            mc.ExeUpdate(sql);
            MessageBox.Show("添加成功！");
        }

        private void button_del_Click(object sender, EventArgs e)
        {
            string sql = textBox_del.Text;
            mc.ExeUpdate(sql);
            MessageBox.Show("删除成功！");
        }

        private void button_change_Click(object sender, EventArgs e)
        {
            string sql = textBox_change.Text;
            mc.ExeUpdate(sql);
            MessageBox.Show("更改成功！");
        }
        private void Mysql_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 写入ini
            ini_data.WriteValue("Setting", "key1", "hello word!");
            ini_data.WriteValue("Setting", "key2", "hello ini!");
            ini_data.WriteValue("SettingImg", "Path", "IMG.Path");
            ini_data.WriteValue("Parameter", "cmd_search", textBox_cmd.Text);
            ini_data.WriteValue("Parameter", "cmd_add", textBox_add.Text);
            ini_data.WriteValue("Parameter", "cmd_del", textBox_del.Text);
            ini_data.WriteValue("Parameter", "cmd_change", textBox_change.Text);
        }
    }
}
