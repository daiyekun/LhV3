using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace NF.Common.Utility
{
    /// <summary>
    /// 文件操作
    /// </summary>
    public class FileUtility
    {
        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public static bool Exists(string path)
        {
            return System.IO.File.Exists(path);
        }
        public static bool Exists(string path, bool IsMppth = false)
        {
            var temppath = path;
            if (IsMppth)
            {
                temppath = GetMapPath(path);
            }

            return File.Exists(temppath);
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

            source = GetMapPath(source);
            dest = GetMapPath(dest);

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
        /// 获取路径
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="folder">文件夹名称</param>
        /// <returns></returns>
        public static string GetPath(string folder, string fileName)
        {
            //var path = Path.Combine(
            //         Directory.GetCurrentDirectory(), "wwwroot", "Uploads", folder,
            //         fileName);
            var path = Path.Combine(
                     Directory.GetCurrentDirectory(), "Uploads", folder,
                     fileName);

            return path;
        }
        /// <summary>
        /// 根据相对路径得到绝对路径
        /// </summary>
        /// <returns></returns>
        public static string GetMapPath(string filePath)
        {
            if (filePath.StartsWith("~"))
            {
                filePath = filePath.TrimStart('~').TrimStart('/');
            }
            var arraypath = StringHelper.Strint2ArrayString(filePath, "/").ToArray();
            var path0 = Path.Combine(arraypath);
            //var path = Path.Combine(
            //         Directory.GetCurrentDirectory(), "wwwroot", path0);
            var path = Path.Combine(
                     Directory.GetCurrentDirectory(),  path0);
            return path;
        }
        /// <summary>
        /// 根据相对路径得到文件夹，不包含文件名称
        /// </summary>
        /// <returns></returns>
        public static string GetMapDicPath(string filePath)
        {
            if (filePath.StartsWith("~"))
            {
                filePath = filePath.TrimStart('~').TrimStart('/');
            }
            var arraypath = StringHelper.Strint2ArrayString(filePath, "/").ToArray();
            IList<string> lists = new List<string>();
            for (var i = 0; i < arraypath.Length - 1; i++)
            {
                lists.Add(arraypath[i]);
            }

            var path0 = Path.Combine(lists.ToArray());
            //var path = Path.Combine(
            //         Directory.GetCurrentDirectory(), "wwwroot", path0);
            var path = Path.Combine(
                     Directory.GetCurrentDirectory(),  path0);
            return path;
        }
        /// <summary>
        /// 获取文件名称
        /// </summary>
        /// <param name="filePath">根据给定路径获取名称</param>
        /// <returns></returns>
        public static string GetFileName(string filePath)
        {

            int index = filePath.LastIndexOf("/");
            string s = filePath.Substring(index + 1);
            //StringHelper.Strint2ArrayString(filePath,"/").Where(a=>a.StartsWith(".docx"))
            return s;



        }
        /// <summary>
        /// 转换路径中的~
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="isDic">是否只是需要文件路径</param>
        /// <returns></returns>
        public static string TransFilePath(string FilePath, bool isDic = false)
        {
            if (string.IsNullOrEmpty(FilePath))
                return "";

            if (FilePath.Length > 2 && FilePath.StartsWith("~/"))
            {
                if (isDic)
                {
                    FilePath =GetMapDicPath(FilePath);
                }
                else
                {

                    FilePath = GetMapPath(FilePath);
                    var dir = Path.GetDirectoryName(FilePath);
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                }

            }
            else
            {
                FilePath = GetMapPath(FilePath);
               
            }

            //else
            //{
            //    //upload
            //    if (!FilePath.Contains(":"))
            //    {
            //        FilePath = System.Web.HttpContext.Current.Server.MapPath("~/" + FilePath);
            //    }
            //}


            return FilePath;
        }


    }
}
