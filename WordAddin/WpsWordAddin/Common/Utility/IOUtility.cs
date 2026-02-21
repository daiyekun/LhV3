using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Common.Utility
{
    /// <summary>
    /// 文件流操作
    /// </summary>
    public class IOUtility
    {


        /// <summary>
        /// 复制大文件
        /// </summary>
        /// <param name="fromPath">源文件的路径</param>
        /// <param name="toPath">文件保存的路径</param>
        /// <param name="eachReadLength">每次读取的长度(1024*1024*5)</param>
        /// <returns>是否复制成功</returns>
        public static bool CopyFile(string fromPath, string toPath, int eachReadLength)
        {
            //将源文件 读取成文件流
            FileStream fromFile = new FileStream(fromPath, FileMode.Open, FileAccess.Read);
            //已追加的方式 写入文件流
            FileStream toFile = new FileStream(toPath, FileMode.Append, FileAccess.Write);
            //实际读取的文件长度
            int toCopyLength = 0;
            //如果每次读取的长度小于 源文件的长度 分段读取
            if (eachReadLength < fromFile.Length)
            {
                byte[] buffer = new byte[eachReadLength];
                long copied = 0;
                while (copied <= fromFile.Length - eachReadLength)
                {
                    toCopyLength = fromFile.Read(buffer, 0, eachReadLength);
                    fromFile.Flush();
                    toFile.Write(buffer, 0, eachReadLength);
                    toFile.Flush();
                    //流的当前位置
                    toFile.Position = fromFile.Position;
                    copied += toCopyLength;

                }
                int left = (int)(fromFile.Length - copied);
                toCopyLength = fromFile.Read(buffer, 0, left);
                fromFile.Flush();
                toFile.Write(buffer, 0, left);
                toFile.Flush();

            }
            else
            {
                //如果每次拷贝的文件长度大于源文件的长度 则将实际文件长度直接拷贝
                byte[] buffer = new byte[fromFile.Length];
                fromFile.Read(buffer, 0, buffer.Length);
                fromFile.Flush();
                toFile.Write(buffer, 0, buffer.Length);
                toFile.Flush();

            }
            fromFile.Close();
            toFile.Close();
            return true;
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="source">原路径</param>
        /// <param name="dest">目标路径</param>
        /// <param name="overwrite">覆盖</param>
        /// <returns>复制成功</returns>
        public static bool CopyFile(string source, string dest, bool overwrite = true)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(dest))
                return false;

            //source = TransFilePath(source);
            //dest = TransFilePath(dest);

            if (!File.Exists(source))
                return false;

            string strDir = Path.GetDirectoryName(dest);
            if (!Directory.Exists(strDir))
            {
                Directory.CreateDirectory(strDir);
            }
            File.Copy(source, dest, overwrite);
            return true;
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filename">文件路径</param>
        /// <returns>删除成功：true</returns>
        public static bool DeleteFile(string filename)
        {
            try
            {
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                    return true;

                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }

        }
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="Path">文件夹路径</param>
        /// <returns>成功创建返回：true 否则False</returns>
        public static bool CreateFolder(string FolderPath)
        {

            try
            {
                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return true;
            }

        }
        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="FolderPath">文件夹路径</param>
        /// <returns>成功返回：true</returns>
        public static bool DeleteFolder(string FolderPath)
        {
            try
            {
                if (Directory.Exists(FolderPath))
                {
                    Directory.Delete(FolderPath);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return true;
            }
        }
        /// <summary>
        /// 判断文件不存在就创建
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
       public static bool CreateFile(string filename)
        {
            try
            {
                if (!File.Exists(filename))
                {
                    File.Create(filename);
                    return true;

                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.WriteLog(typeof(IOUtility),ex);
                return false;
            }
        }

             

        /// <summary>
        /// 删除文件夹下所有文件
        /// </summary>
        /// <param name="FolderPath">文件路径</param>
        /// <returns>删除成功返回：true</returns>
        public static bool DeleteAllFile(string FolderPath)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(FolderPath);
                FileInfo[] files = dirInfo.GetFiles();
                if (files.Count() > 0)
                {
                    foreach (var item in files)
                    {
                        try
                        {
                            File.Delete(item.FullName);
                        }
                        catch (Exception)
                        {

                            continue;
                        }

                    }

                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        /// <summary>
        /// 打开Word
        /// </summary>
        /// <param name="WordPath"></param>
        public static void OpenWord(string WordPath, ProcessWindowStyle winStyle = ProcessWindowStyle.Normal)
        {


           
            string winwordPath = "";
            Process[] wordProcesses = Process.GetProcessesByName("WINWORD");
            foreach (Process process in wordProcesses)
            {
                // Debug.WriteLine(process.MainWindowTitle);
                // 如果有的话获得 Winword.exe 的完全限定名称。
                winwordPath = process.MainModule.FileName;
                break;
            }

            Process wordProcess = new Process();

            if (winwordPath.Length > 0)    // 如果有 Word 实例在运行，使用 /w 参数来强制启动新实例，并将文件名作为参数传递。 
            {
                wordProcess.StartInfo.FileName = winwordPath;
                wordProcess.StartInfo.UseShellExecute = false;
                wordProcess.StartInfo.Arguments = WordPath + " /w";
                wordProcess.StartInfo.RedirectStandardOutput = true;
            }
            else
            { // 如果没有 Word 实例在运行，还是 
                wordProcess.StartInfo.FileName = WordPath;
                wordProcess.StartInfo.UseShellExecute = true;

            }
            //显示方式
           // wordProcess.StartInfo.WindowStyle = winStyle;

            // 当前进程一直在等待，直到该 Word 实例退出。 
            wordProcess.WaitForExit();
            wordProcess.Close();

        }

        /// <summary>
        /// 根据窗体标题删掉相关进程，
        /// Word标题在Caption属性指定
        ///   var wordApp = new Application();
        /// wordApp.Caption = "CompareWordApp";
        /// </summary>
        /// <param name="WinTitle">标题包含</param>
        public static void KillWordProcess(string WinTitle)
        {
            string processName = "WINWORD";//"WINWORD";
            Process[] process = Process.GetProcessesByName(processName);
            try
            {
                foreach (Process p in process)
                {
                    if (p.MainWindowTitle.Contains(WinTitle))
                    {
                        p.Kill();

                    }
                }
            }
            catch (Exception ex)
            {

                LogUtility.WriteLog(typeof(IOUtility), ex);
               
            }
        }
        /// <summary>
        /// 获取路径Path
        /// </summary>
        /// <returns></returns>
        public static string GetFilePath(string prefix)
        {
            var tnk = System.DateTime.Now.Ticks.ToString();
            string tempPath = System.Environment.GetEnvironmentVariable("TEMP");
            string tempfile = string.Format("{0}_{1}.docx", prefix,tnk);
            string temppath = Path.Combine(tempPath, tempfile);
            return temppath;
        }




    }
}
