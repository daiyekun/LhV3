using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Common.Utility
{
    /// <summary>
    /// 消息处理工具类
    /// </summary>
    public class MessageUtility
    {
        public delegate void Adelegate();
        /// <summary>
        /// 保存消息提示
        /// </summary>
        /// <param name="result">提交返回的消息</param>
        public static void SaveMsg(string result)
        {

            if (result.Equals("SUC"))
            {
                MessageBox.Show("保存成功！");

            }
            else
            {
                MessageBox.Show("保存失败！");
            }

        }
        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="result"></param>
        public static void ShowMsg(string msg)
        {
            
            MessageBox.Show(msg);
           

        }

        public static void ShowMsg2(string msg,object o)
        {
            Adelegate dgt = new Adelegate(() => {
                var reslt=MessageBox.Show("qq");
               
            });
            dgt.Invoke();
            
        }


        public static void CheckProcess(string WinTitle)
          {
            string processName = "WINWORD";//"WINWORD";
            Process[] process = Process.GetProcessesByName(processName);
            
                foreach (Process p in process)
                {
                    if (p.MainWindowTitle.Contains(WinTitle))
                    {
                        p.WaitForExit(10000);

                    }
                }
            
        }
        /// <summary>
        /// 使用进程打开文件
        /// </summary>
        /// <param name="WinTitle"></param>
        /// <param name="fileName"></param>
        public static void ProcessOpenWord(string WinTitle,string fileName)
        {
            string processName = "WINWORD";//"WINWORD";
            Process[] process = Process.GetProcessesByName(processName);

            foreach (Process p in process)
            {
                if (p.MainWindowTitle.Contains(WinTitle))
                {
                   
                    p.StartInfo.FileName = fileName;
                    p.StartInfo.Arguments = "";
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                    p.Start();



                }
            }

        }








    }
}
