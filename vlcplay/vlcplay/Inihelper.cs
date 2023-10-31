﻿using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mysqlx.Expect.Open.Types.Condition.Types;
using static System.Collections.Specialized.BitVector32;
using System.IO;
/// <summary>
/// ini文件(配置文件)使用类
/// </summary>
namespace vlcplay
{
    public partial class Inihelper
    {
        // 声明INI文件的写操作函数 WritePrivateProfileString()
         [System.Runtime.InteropServices.DllImport("kernel32")]
         private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
 
         // 声明INI文件的读操作函数 GetPrivateProfileString()
         [System.Runtime.InteropServices.DllImport("kernel32")]
         private static extern int GetPrivateProfileString(string section, string key, string def, System.Text.StringBuilder retVal, int size, string filePath);
 
         private string sPath = null;
         public Inihelper(string path)
         {
             this.sPath = path;
         }

         public void WriteValue(string section, string key, string value)
         {
           // section=配置节，key=键名，value=键值，path=路径
             WritePrivateProfileString(section, key, value, sPath);
        }

         public string ReadValue(string section, string key)
        {
            // 每次从ini中读取多少字节
            System.Text.StringBuilder temp = new System.Text.StringBuilder(255);
            // section=配置节，key=键名，temp=上面，path=路径
            GetPrivateProfileString(section, key, "", temp, 255, sPath);
            return temp.ToString();
        }
    }
}
