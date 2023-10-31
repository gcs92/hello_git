using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Eagle._Tests.Default;

namespace vlcplay
{
    public class SQLiteClass
    {
        private static string connectionString = string.Empty;
        /// <summary>
        /// 根据数据源、密码、版本号设置连接字符串。
        /// </summary>
        /// <param name="datasource">数据源。</param>
        /// <param name="password">密码。</param>
        /// <param name="version">版本号（缺省为3）。</param>
        public void SetConnectionString(string datasource, string password, int version = 3)
        {
            //connectionString = string.Format("Data Source={0};Version={1};password={2}",//加密扩展所需要的访问格式
            // datasource, version, password);
            connectionString = string.Format("Data Source={0}",//非加密扩展所需要的访问格式
                datasource, version, password);

        }

        /// <summary>
        /// 创建一个数据库文件。如果存在同名数据库文件，则会覆盖。
        /// </summary>
        /// <param name="dbName">数据库文件名。为null或空串时不创建。</param>
        /// <param name="password">（可选）数据库密码，默认为空。</param>
        /// <exception cref="Exception"></exception>
        public void CreateDB(string dbName)
        {
            if (!string.IsNullOrEmpty(dbName))
            {
                try { SQLiteConnection.CreateFile(dbName); }
                catch (Exception) { throw; }
            }
        }

        /// <summary> 
        /// 对SQLite数据库执行增删改操作，返回受影响的行数。 
        /// </summary> 
        /// <param name="sql">要执行的增删改的SQL语句。</param> 
        /// <param name="parameters">执行增删改语句所需要的参数，参数必须以它们在SQL语句中的顺序为准。</param> 
        /// <returns></returns> 
        /// <exception cref="Exception"></exception>
        public int ExecuteNonQuery(string sql, params SQLiteParameter[] parameters)
        {
            int affectedRows = 0;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    try
                    {
                        connection.Open();
                        command.CommandText = sql;
                        if (parameters.Length != 0)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        affectedRows = command.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (Exception) { throw; }
                }
            }
            return affectedRows;
        }
        /// <summary> 
        /// 执行一个查询语句，返回一个包含查询结果的DataTable。 
        /// </summary> 
        /// <param name="sql">要执行的查询语句。</param> 
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准。</param> 
        /// <returns></returns> 
        /// <exception cref="Exception"></exception>
        public DataTable ExecuteQuery(string sql, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    connection.Open();
                    if (parameters != null && parameters.Length != 0)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    DataTable data = new DataTable();
                    try { adapter.Fill(data); }
                    catch (Exception) {
                        throw; 
                    }
                    connection.Close();
                    return data;
                }
                connection.Close();
            }
        }
        /// <summary> 
        /// 查询数据库中的所有数据类型信息。暂时没什么作用
        /// </summary> 
        /// <returns></returns> 
        /// <exception cref="Exception"></exception>
        public DataTable GetSchema()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return connection.GetSchema("TABLES");
                }
                catch (Exception) { throw; }
            }

        }
        public static void Test(string sql, params SQLiteParameter[] parameters) 
        {
            string connectionString = "Data Source=database.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // 创建表格
                string createTableQuery = "CREATE TABLE IF NOT EXISTS MyTable (Id INTEGER PRIMARY KEY, Name TEXT)";
                using (SQLiteCommand createTableCommand = new SQLiteCommand(createTableQuery, connection))
                {
                    createTableCommand.ExecuteNonQuery();
                }

                // 插入数据
                string insertDataQuery = "INSERT INTO MyTable (Name) VALUES (@name)";
                using (SQLiteCommand insertDataCommand = new SQLiteCommand(insertDataQuery, connection))
                {
                    insertDataCommand.Parameters.AddWithValue("@name", "John");
                    insertDataCommand.ExecuteNonQuery();
                }

                // 查询数据
                string selectDataQuery = "SELECT * FROM MyTable";
                using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(selectDataQuery, connection))
                {
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // 输出查询结果
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Console.WriteLine("Id: {0}, Name: {1}", row["Id"], row["Name"]);
                    }
                }

                // 更新数据
                string updateDataQuery = "UPDATE MyTable SET Name = @newName WHERE Id = @id";
                using (SQLiteCommand updateDataCommand = new SQLiteCommand(updateDataQuery, connection))
                {
                    updateDataCommand.Parameters.AddWithValue("@newName", "Jane");
                    updateDataCommand.Parameters.AddWithValue("@id", 1);
                    updateDataCommand.ExecuteNonQuery();
                }

                // 关闭连接
                connection.Close();
            }
        }
    }
}
