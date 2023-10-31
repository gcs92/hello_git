using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vlcplay
{
    public partial class SQLite : Form
    {
        SQLiteClass sql = new SQLiteClass();
        SQLiteParameter[] sp =
        {
            new SQLiteParameter("@id",DbType.Int32)
        };
        
        public SQLite()
        {
            InitializeComponent();
            sp[0].Value = 1;
            //SQLiteHelper.SetConnectionString("D:\\用户目录\\我的文档\\Visual Studio 2015\\Projects\\WindowsFormsApplication2\\WindowsFormsApplication2\\midtrans.db",null,3);
            string cd = System.Environment.CurrentDirectory;
            string pd = Directory.GetParent(Directory.GetParent(cd).FullName).FullName;
            pd += "\\midtrans.db";
            //SQLiteClass.CreateDB(pd);//手动创建表即可，如果使用这个会覆盖里面的表数据，则需要重新创建
            sql.SetConnectionString(pd, null, 3);
        }

        private void button_test_Click(object sender, EventArgs e)
        {
            DataTable dt = sql.ExecuteQuery("select * from gcstest", sp);//查询
            dataGridView1.DataSource = dt;
            string str =" ";
            str += "\r\n" + System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            //str += "\r\n" + System.Environment.CurrentDirectory;
            //str += "\r\n" + System.IO.Directory.GetCurrentDirectory();
            //str += "\r\n" + System.AppDomain.CurrentDomain.BaseDirectory;
            //str += "\r\n" + System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            //str += "\r\n" + System.Windows.Forms.Application.StartupPath;
            //str += "\r\n" + System.Windows.Forms.Application.ExecutablePath;
            Console.Write(str);
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            DataTable dt = sql.ExecuteQuery("select * from gcstest", sp);//查询
            dataGridView1.DataSource = dt;
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            int i = sql.ExecuteNonQuery("insert into gcstest values(5,\"小五\",8,\"小学\",\"4镇\",now())", sp);//增删改 命令执行
        }

        private void button_del_Click(object sender, EventArgs e)
        {
            int i = sql.ExecuteNonQuery("delete from gcstest where id = 1", sp);//增删改 命令执行
        }

        private void button_change_Click(object sender, EventArgs e)
        {
            int i = sql.ExecuteNonQuery("update gcstest set city = \"四镇\" where city = \"4镇\"", sp);//增删改 命令执行
        }
    }
}
