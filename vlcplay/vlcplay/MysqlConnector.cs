using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

/// <summary>
/// 数据库操作类
/// </summary>
namespace vlcplay
{
    public class MysqlConnector
    {
        string server = null;
        string userid = null;
        string password = null;
        string database = null;
        string port = "3306";
        string charset = "utf-8";

        public MysqlConnector() { }
        public MysqlConnector SetServer(string server)
        {
            this.server = server;
            return this;
        }

        public MysqlConnector SetUserID(string userid)
        {
            this.userid = userid;
            return this;
        }

        public MysqlConnector SetDataBase(string database)
        {
            this.database = database;
            return this;
        }

        public MysqlConnector SetPassword(string password)
        {
            this.password = password;
            return this;
        }
        public MysqlConnector SetPort(string port)
        {
            this.port = port;
            return this;
        }
        public MysqlConnector SetCharset(string charset)
        {
            this.charset = charset;
            return this;
        }



        #region  建立MySql数据库连接
        /// <summary>
        /// 建立数据库连接.
        /// </summary>
        /// <returns>返回MySqlConnection对象</returns>
        private MySqlConnection GetMysqlConnection()
        {
            string M_str_sqlcon = string.Format("server={0};user id={1};password={2};database={3};port={4};Charset={5}", server, userid, password, database, port, charset);
            MySqlConnection myCon = new MySqlConnection(M_str_sqlcon);
            return myCon;
        }
        #endregion

        #region  执行MySqlCommand命令
        /// <summary>
        /// 执行MySqlCommand
        /// </summary>
        /// <param name="M_str_sqlstr">SQL语句</param>
        public void ExeUpdate(string M_str_sqlstr)
        {
            try
            {
                MySqlConnection mysqlcon = this.GetMysqlConnection();
                mysqlcon.Open();
                MySqlCommand mysqlcom = new MySqlCommand(M_str_sqlstr, mysqlcon);
                mysqlcom.ExecuteNonQuery();
                mysqlcom.Dispose();
                mysqlcon.Close();
                mysqlcon.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接失败：" + ex.ToString());
            }
            
        }
        #endregion

        #region  创建MySqlDataReader对象
        /// <summary>
        /// 第一步：使用SqlConnection对象连接数据库
        /// 第二步：建立SqlCommand对象，执行SQL语句
        /// 第三步：对SQL语句执行后的结果进行操作
        /// 创建一个MySqlDataReader对象
        /// </summary>
        /// <param name="M_str_sqlstr">SQL语句</param>
        /// <returns>返回MySqlDataReader对象</returns>
        public MySqlDataReader ExeQuery(string M_str_sqlstr)
        {
            try 
            {
                Console.WriteLine(M_str_sqlstr);
                MySqlConnection mysqlcon = this.GetMysqlConnection();
                MySqlCommand mysqlcom = new MySqlCommand(M_str_sqlstr, mysqlcon);
                mysqlcon.Open();
                MySqlDataReader mysqlread = mysqlcom.ExecuteReader(CommandBehavior.CloseConnection);
                return mysqlread;
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取数据失败：" + ex.ToString());
                return null;
            }
        }
        #endregion
    }
}
