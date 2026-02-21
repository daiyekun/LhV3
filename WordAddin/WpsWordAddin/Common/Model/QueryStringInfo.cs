using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Model
{
   /// <summary>
   /// 请求参数实体，承载网页端请求到插件的参数
   /// </summary>
    public class QueryStringInfo
    {
        /// <summary>
        /// 插件版本
        /// </summary>
        public string addinVar { get; set; }
        /// <summary>
        /// 请求网页根路径
        /// </summary>
        public string baseAddr { get; set; }
        /// <summary>
        /// 文件ID
        /// </summary>
        public int fId { get; set; }
        /// <summary>
        /// 比较文件ID-（比较文本时用）
        /// </summary>
        public int sId { get; set; }
        /// <summary>
        /// 当前用户ID
        /// </summary>
        public int uId { get; set; }
        /// <summary>
        /// 文档操作类型
        /// </summary>
        public string wT { get; set; }

       // /// <summary>
       // /// 获取请求数据存储到字典
       // /// </summary>
       // /// <returns></returns>
       // public static Dictionary<string, string> GetQueryDataInfoToDictonary(string requestDataJson)
       //{
       //    Dictionary<string, string> dic = new Dictionary<string, string>();
       //    var temstrquerydata = requestDataJson.TrimStart('{').TrimEnd('}').Split(',');
       //    foreach (var itemdata in temstrquerydata)
       //    {
       //        string[] strdata = itemdata.Split(':');
       //        if (strdata.Length == 2)
       //        {
       //            dic.Add(strdata[0], strdata[1]);
       //        }
       //        else if (strdata.Length == 3)
       //        {
       //            dic.Add(strdata[0], strdata[1] +":"+ strdata[2]);

       //        }
       //    }

       //    return dic;
       
       //}

        /// <summary>
        /// 获取请求数据存储到字典
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetQueryDataInfoToDictonary(string requestDataJson)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            var temstrquerydata = requestDataJson.TrimStart('{').TrimEnd('}').Split(',');
            foreach (var itemdata in temstrquerydata)
            {
                //int pos = itemdata.IndexOf(":");
                //string first = itemdata.Substring(0, pos - 1);
                //string end = itemdata.Substring(pos + 1, itemdata.Length - pos - 1);
                //dic.Add(first, end);
                string[] strdata = itemdata.Split(':');
                if (strdata.Length == 2)
                {
                    dic.Add(strdata[0].Replace("\"", ""), strdata[1].Replace("\"", ""));
                }
                else if (strdata.Length > 2)
                {
                    string value = "";
                    for (int i = 1; i < strdata.Length; i++)
                    {
                        value += strdata[i] + ":";
                    }
                    if (!string.IsNullOrWhiteSpace(value))
                        value = value.Substring(0, value.Length - 1);
                    dic.Add(strdata[0].Replace("\"", ""), value.Replace("\"", ""));

                }
            }

            return dic;

        }
        /// <summary>
        /// 获取请求数据对象
        /// </summary>
        /// <returns>请求数据对象</returns>
        public static QueryStringInfo GetRequstQureyInfo(string requestDataJson)
        {
            QueryStringInfo info = new QueryStringInfo();
            Dictionary<string, string> dicdata= GetQueryDataInfoToDictonary(requestDataJson);
            var Properties = info.GetType().GetProperties();
            foreach (var item in Properties)
            {
               
                if (dicdata.ContainsKey(item.Name))
                {
                    var dicvalue=dicdata[item.Name];
                    int tempval=0;
                    if (int.TryParse(dicvalue, out tempval))
                    {
                        item.SetValue(info, tempval, null);
                    }
                    else
                    {
                        item.SetValue(info, dicdata[item.Name], null);
                    }
                    
                    
                }
              
            
            }

            return info;
        
        
        }

     
        
    }
}
