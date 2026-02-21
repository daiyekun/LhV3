using Common.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace Common.Utility
{
    /// <summary>
    /// http请求操作类
    /// </summary>
   public class HttpRequestUtility
    {
        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="URL">请求URLhttp://localhost/xx.aspx</param>
        /// <param name="postdata">请求参数字符串</param>
        /// <returns></returns>
        public static string HttpPost(string URL, string postdata)
        {
            // Create a request using a URL that can receive a post. 
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
            // Set the Method property of the request to POST.
            request.Method = "POST";
            // Create POST data and convert it to a byte array.
            string postData = postdata;//"This is a test that posts this string to a Web server.";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            var ce = response.ContentEncoding;
            if(ce.ToLower()=="gzip"){//判断解决乱码
                dataStream = new GZipStream(dataStream,CompressionMode.Decompress);
            
            }
            var encoding = response.CharacterSet;//判断解决乱码
            StreamReader reader = null;
            switch (encoding)
            {
                case "utf-8":
                    reader = new StreamReader(dataStream, Encoding.UTF8);
                    break;
                case"gb2312":
                    reader = new StreamReader(dataStream, Encoding.GetEncoding("gb2312"));
                    break;
                default:
                    reader = new StreamReader(dataStream, Encoding.UTF8);
                    break;
            
            }
          
            string responseFromServer = reader.ReadToEnd();
          
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }

        
       /// <summary>
       /// GET请求
       /// </summary>
        /// <param name="Url">请求地址http://localhost/xx.aspx</param>
       /// <param name="postDataStr">请求数据</param>
       /// <returns>返回字符串</returns>
        public static string HttpGet(string Url, string postDataStr="")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
       /// <summary>
       /// 提交Post请求
       /// </summary>
       /// <param name="postdata">请求数据</param>
       /// <param name="iurlType">URL地址类型</param>
       /// <param name="cmdStr">请求后太方法,如果有?cmd=xxx/ddd必须传入</param>
       /// <returns>返回字符串</returns>
        public static string SubmitPostRequest(string postdata,CreateUrlType iurlType,string cmdStr="")
        {
            var HttprequestUrl = UrlDataUtility.GetOptionUrl(iurlType) + cmdStr;
            //MessageBox.Show("url路径：", HttprequestUrl);
            return HttpRequestUtility.HttpPost(HttprequestUrl, postdata);

        }
        /// <summary>
        /// 提交Post请求
        /// </summary>
        /// <param name="postdata">请求数据</param>
        /// <param name="iurlType">URL地址类型</param>
        /// <param name="cmdStr">请求后太方法,如果有?cmd=xxx/ddd必须传入</param>
        /// <param name="func">mvc 具体方法</param>
        /// <returns>返回字符串</returns>
        public static string SubmitPostRequestCore(string postdata, CreateUrlType iurlType,string func, string cmdStr = "")
        {
            var HttprequestUrl = UrlDataUtility.GetOptionUrl(iurlType)+ func + cmdStr;
           
            return HttpRequestUtility.HttpPost(HttprequestUrl, postdata);

        }




    }
}
