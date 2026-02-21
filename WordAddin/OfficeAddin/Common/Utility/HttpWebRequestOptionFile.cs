using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utility
{
   /// <summary>
   /// 文件下载和文件上传
   /// </summary>
   public class HttpWebRequestOptionFile
    {
       /// <summary>
       /// 上传文件
       /// </summary>
       /// <param name="strFileToUpload">本地上传文件的全路径名称：C:\123.txt</param>
       /// <param name="strUrl">处理客户端请求的URL地址:http://localhost:8088/default.aspx</param>
       /// <returns></returns>
       public static string MyUploader(string strFileToUpload, string strUrl)
       {
           string strFileFormName = "file";
           Uri oUri = new Uri(strUrl);
           string strBoundary = "----------" + DateTime.Now.Ticks.ToString("x");
           // The trailing boundary string
           byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + strBoundary + "\r\n");
           //拼接请求头文件
           StringBuilder sb = new StringBuilder();
           sb.Append("--");
           sb.Append(strBoundary);
           sb.Append("\r\n");
           sb.Append("Content-Disposition: form-data; name=\"");
           sb.Append(strFileFormName);
           sb.Append("\"; filename=\"");
           sb.Append(Path.GetFileName(strFileToUpload));
           sb.Append("\"");
           sb.Append("\r\n");
           sb.Append("Content-Type: ");
           sb.Append("application/octet-stream");
           sb.Append("\r\n");
           sb.Append("\r\n");
           string strPostHeader = sb.ToString();
           byte[] postHeaderBytes = Encoding.UTF8.GetBytes(strPostHeader);
           //根据uri创建HttpWebRequest对象 
           HttpWebRequest oWebrequest = (HttpWebRequest)WebRequest.Create(oUri);
           oWebrequest.ContentType = "multipart/form-data; boundary=" + strBoundary;
           oWebrequest.Method = "POST";
          
           //对发送的数据不使用缓存【重要、关键】 
           oWebrequest.AllowWriteStreamBuffering = false;
           //设置获得响应的超时时间（300秒） 
           oWebrequest.Timeout = 300000;
           // Get a FileStream and set the final properties of the WebRequest//FileShare.ReadWrite共享读写，不然提示被其他进程使用
           FileStream oFileStream = new FileStream(strFileToUpload, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
           long length = postHeaderBytes.Length + oFileStream.Length + boundaryBytes.Length;
           oWebrequest.ContentLength = length;
           Stream oRequestStream = oWebrequest.GetRequestStream();
           // Write the post header
           oRequestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
           // Stream the file contents in small pieces (4096 bytes, max).
           byte[] buffer = new Byte[checked((uint)Math.Min(4096, (int)oFileStream.Length))];
           int bytesRead = 0;
           while ((bytesRead = oFileStream.Read(buffer, 0, buffer.Length)) != 0)
           oRequestStream.Write(buffer, 0, bytesRead);
           oFileStream.Close();
           // Add the trailing boundary
           oRequestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
           WebResponse oWResponse = oWebrequest.GetResponse();
           Stream s = oWResponse.GetResponseStream();
           StreamReader sr = new StreamReader(s);
           String sReturnString = sr.ReadToEnd();
           // Clean up
           oFileStream.Close();
           oRequestStream.Close();
           s.Close();
           sr.Close();
           return sReturnString;

       }
      /// <summary>
      /// 从服务器下载文件到本地保存
      /// </summary>
      /// <param name="downloadUrl">下载的文件路径:http://localhost:8088/123.txt</param>
      /// <param name="loadpath">本地保存的文件全路径名称:C:\123.txt</param>
       public static void Download(string downloadUrl,string loadpath) 
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
                       fs.Write(bytes, 0, n);　//将指定字节的流信息写入文件流中
                   }
               }
           }
          
       }

     
    

    }
}
