using NF.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace NF.WeiXin.Lib.Utility
{
    /// <summary>
    /// 后台请求工具类
    /// </summary>
    public  class RequestUtility
    {
        /// <summary>
        /// GET 方法
        /// </summary>
        /// <param name="Url">请求地址</param>
        /// <param name="postDataStr">请求数据</param>
        /// <returns>返回字符串</returns>
        public static string HttpGet(string Url, string postDataStr = "")
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
        /// post请求这个正常使用
        /// </summary>
        /// <param name="URL"></param>
        /// <param name="postdata"></param>
        /// <returns></returns>
        public static string HttpPost4(string URL, string postdata)
        {
            // Create a request using a URL that can receive a post. 
            WebRequest request = WebRequest.Create(URL);
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
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            //Console.WriteLine(responseFromServer);
            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;

        }


        /// <summary>
        /// 从服务器下载文件到本地保存
        /// </summary>
        /// <param name="downloadUrl">下载的文件路径:http://localhost:8088/123.txt</param>
        /// <param name="loadpath">本地保存的文件全路径名称:C:\123.txt</param>
        public static void Download(string downloadUrl, string loadpath)
        {

            try
            {
                Uri url = new Uri(downloadUrl);
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

                using (Stream stream = request.GetResponse().GetResponseStream())
                {
                    //文件流，流信息读到文件流中，读完关闭//@"download.jpg"
                    using (FileStream fs = File.Create(loadpath))
                    {
                        //建立字节组，并设置它的大小是多少字节
                        int length = 1024;//缓冲，1kb，如果设置的过大，而要下载的文件大小小于这个值，就会出现乱码。
                        byte[] bytes = new byte[length];
                        int n = 1;
                        while (n > 0)
                        {
                            //一次从流中读多少字节，并把值赋给Ｎ，当读完后，Ｎ为０,并退出循环
                            n = stream.Read(bytes, 0, length);
                            fs.Write(bytes, 0, n); //将指定字节的流信息写入文件流中
                        }

                    }
                }
            }
            catch (Exception ex)
            {

                Log4netHelper.Error(ex.Message);
                throw new Exception("文件不存在！");
            }
            
            
        }



    }
}
