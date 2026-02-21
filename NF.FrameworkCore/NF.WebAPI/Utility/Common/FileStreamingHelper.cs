using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using NF.Common.Models;
using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.WebAPI.Utility.Common
{
    public static class FileStreamingHelper
    {
        private static readonly FormOptions _defaultFormOptions = new FormOptions();

        public static async Task<FormValueProvider> StreamFiles(this HttpRequest request, string targetDirectory, UploadFileInfo uploadFileInfo)
        {
            if (!MultipartRequestHelper.IsMultipartContentType(request.ContentType))
            {
                throw new Exception($"Expected a multipart request, but got {request.ContentType}");
            }

            // Used to accumulate all the form url encoded key value pairs in the 
            // request.
            var formAccumulator = new KeyValueAccumulator();

            var boundary = MultipartRequestHelper.GetBoundary(
                MediaTypeHeaderValue.Parse(request.ContentType),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, request.Body);
            var filepath = targetDirectory + "\\" + uploadFileInfo.SourceFileName;
            if (uploadFileInfo.RemGuidName)
            {//模板起草插件保存文件
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
            }
                
            var section = await reader.ReadNextSectionAsync();//用于读取Http请求中的第一个section数据
            while (section != null)
            {
                Microsoft.Net.Http.Headers.ContentDispositionHeaderValue contentDisposition;
                var hasContentDispositionHeader = Microsoft.Net.Http.Headers.ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition);

                if (hasContentDispositionHeader)
                {
                    
                    if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                    {
                        if (!Directory.Exists(targetDirectory))
                        {
                            Directory.CreateDirectory(targetDirectory);
                        }

                       
                        var loadBufferBytes = 1024;
                        if (uploadFileInfo.RemGuidName)
                        {
                            var fileName = MultipartRequestHelper.GetFileName(contentDisposition);
                            //这个是每一次从Http请求的section中读出文件数据的大小，单位是Byte即字节，这里设置为1024的意思是，每次从Http请求的section数据流中读取出1024字节的数据到服务器内存中，然后写入下面targetFileStream的文件流中，可以根据服务器的内存大小调整这个值。这样就避免了一次加载所有上传文件的数据到服务器内存中，导致服务器崩溃。
                            string sourceFilePath = targetDirectory + "\\" + fileName;//原始文件名拼接文件路径
                            var extension = Path.GetExtension(sourceFilePath);//文件扩展
                            var notextenFileName = Path.GetFileNameWithoutExtension(sourceFilePath);//没有扩展的文件名称
                            var tmpfileName = Guid.NewGuid().ToString();
                            //存储于实体，用于后台绑定数据库
                            uploadFileInfo.SourceFileName = fileName;
                            uploadFileInfo.GuidFileName = tmpfileName + extension;
                            uploadFileInfo.NotExtenFileName = notextenFileName;
                            uploadFileInfo.Extension = extension;
                            using (var targetFileStream = File.Create(targetDirectory + "\\" + tmpfileName + extension))
                            {
                                //section.Body是System.IO.Stream类型，表示的是Http请求中一个section的数据流，从该数据流中可以读出每一个section的全部数据，所以我们下面也可以不用section.Body.CopyToAsync方法，而是在一个循环中用section.Body.Read方法自己读出数据，再将数据写入到targetFileStream
                                await section.Body.CopyToAsync(targetFileStream, loadBufferBytes);
                            }
                        }
                        else
                        {
                            
                            using (var targetFileStream = File.Create(filepath))
                            {
                                //section.Body是System.IO.Stream类型，表示的是Http请求中一个section的数据流，从该数据流中可以读出每一个section的全部数据，所以我们下面也可以不用section.Body.CopyToAsync方法，而是在一个循环中用section.Body.Read方法自己读出数据，再将数据写入到targetFileStream
                                 await section.Body.CopyToAsync(targetFileStream, loadBufferBytes);
                                
                            }
                        }


                    }
                   
                    else if (MultipartRequestHelper.HasFormDataContentDisposition(contentDisposition))
                    {
                      
                        var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name);
                        var encoding = GetEncoding(section);
                        using (var streamReader = new StreamReader(
                            section.Body,
                            encoding,
                            detectEncodingFromByteOrderMarks: true,
                            bufferSize: 1024,
                            leaveOpen: true))
                        {
                            // The value length limit is enforced by MultipartBodyLengthLimit
                            var value = await streamReader.ReadToEndAsync();
                            if (String.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
                            {
                                value = String.Empty;
                            }
                            formAccumulator.Append(key.Value, value); // For .NET Core <2.0 remove ".Value" from key

                            if (formAccumulator.ValueCount > _defaultFormOptions.ValueCountLimit)
                            {
                                throw new InvalidDataException($"Form key count limit {_defaultFormOptions.ValueCountLimit} exceeded.");
                            }
                        }
                    }
                }



                try
                {
                    section = await reader.ReadNextSectionAsync();//用于读取Http请求中的下一个section数据
                }
                catch (Exception ex)
                {


                }
                finally
                {
                    if(section==null|| section.Headers.Count <= 0 || string.IsNullOrEmpty(section.ContentDisposition))
                    {
                        section = null;//不然死循环
                    }

                }
                
            }

            // Bind form data to a model
            var formValueProvider = new FormValueProvider(
                BindingSource.Form,
                new FormCollection(formAccumulator.GetResults()),
                CultureInfo.CurrentCulture);

            return formValueProvider;
        }

        public static async Task<FormValueProvider> StreamFiles_Dz(this HttpRequest request, string targetDirectory, UploadFileInfo uploadFileInfo ,int idf ,int userid)
        {
            if (!MultipartRequestHelper.IsMultipartContentType(request.ContentType))
            {
                throw new Exception($"Expected a multipart request, but got {request.ContentType}");
            }

            // Used to accumulate all the form url encoded key value pairs in the 
            // request.
            var formAccumulator = new KeyValueAccumulator();

            var boundary = MultipartRequestHelper.GetBoundary(
                MediaTypeHeaderValue.Parse(request.ContentType),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, request.Body);
            var filepath = targetDirectory + "\\" + uploadFileInfo.SourceFileName;
            if (uploadFileInfo.RemGuidName)
            {//模板起草插件保存文件
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
            }

            var section = await reader.ReadNextSectionAsync();//用于读取Http请求中的第一个section数据
            while (section != null)
            {
                Microsoft.Net.Http.Headers.ContentDispositionHeaderValue contentDisposition;
                var hasContentDispositionHeader = Microsoft.Net.Http.Headers.ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition);

                if (hasContentDispositionHeader)
                {

                    if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                    {
                        if (!Directory.Exists(targetDirectory))
                        {
                            Directory.CreateDirectory(targetDirectory);
                        }


                        var loadBufferBytes = 1024;
                        if (uploadFileInfo.RemGuidName)
                        {
                            if (idf==15)
                            {
                            var fileName =userid.ToString();
                                var fileNa = MultipartRequestHelper.GetFileName(contentDisposition);
                                //这个是每一次从Http请求的section中读出文件数据的大小，单位是Byte即字节，这里设置为1024的意思是，每次从Http请求的section数据流中读取出1024字节的数据到服务器内存中，然后写入下面targetFileStream的文件流中，可以根据服务器的内存大小调整这个值。这样就避免了一次加载所有上传文件的数据到服务器内存中，导致服务器崩溃。
                                string sourceFilePath = targetDirectory + "\\" + fileNa;//原始文件名拼接文件路径
                            var extension = Path.GetExtension(sourceFilePath);//文件扩展
                            var notextenFileName = userid.ToString();//没有扩展的文件名称
                            var tmpfileName = userid.ToString();
                            //存储于实体，用于后台绑定数据库
                            uploadFileInfo.SourceFileName = fileName+extension;
                            uploadFileInfo.GuidFileName = tmpfileName + extension;
                            uploadFileInfo.NotExtenFileName = notextenFileName;
                            uploadFileInfo.Extension = extension;
                       
                            using (var targetFileStream = File.Create(targetDirectory + "\\" + tmpfileName + extension))
                            {
                                //section.Body是System.IO.Stream类型，表示的是Http请求中一个section的数据流，从该数据流中可以读出每一个section的全部数据，所以我们下面也可以不用section.Body.CopyToAsync方法，而是在一个循环中用section.Body.Read方法自己读出数据，再将数据写入到targetFileStream
                                await section.Body.CopyToAsync(targetFileStream, loadBufferBytes);
                            }
                            }
                            else
                            {
                                var fileName = MultipartRequestHelper.GetFileName(contentDisposition);
                                //这个是每一次从Http请求的section中读出文件数据的大小，单位是Byte即字节，这里设置为1024的意思是，每次从Http请求的section数据流中读取出1024字节的数据到服务器内存中，然后写入下面targetFileStream的文件流中，可以根据服务器的内存大小调整这个值。这样就避免了一次加载所有上传文件的数据到服务器内存中，导致服务器崩溃。
                                string sourceFilePath = targetDirectory + "\\" + fileName;//原始文件名拼接文件路径
                                var extension = Path.GetExtension(sourceFilePath);//文件扩展
                                var notextenFileName = Path.GetFileNameWithoutExtension(sourceFilePath);//没有扩展的文件名称
                                var tmpfileName = Guid.NewGuid().ToString();
                                //存储于实体，用于后台绑定数据库
                                uploadFileInfo.SourceFileName = fileName;
                                uploadFileInfo.GuidFileName = tmpfileName + extension;
                                uploadFileInfo.NotExtenFileName = notextenFileName;
                                uploadFileInfo.Extension = extension;

                                using (var targetFileStream = File.Create(targetDirectory + "\\" + tmpfileName + extension))
                                {
                                    //section.Body是System.IO.Stream类型，表示的是Http请求中一个section的数据流，从该数据流中可以读出每一个section的全部数据，所以我们下面也可以不用section.Body.CopyToAsync方法，而是在一个循环中用section.Body.Read方法自己读出数据，再将数据写入到targetFileStream
                                    await section.Body.CopyToAsync(targetFileStream, loadBufferBytes);
                                }
                            }
                        }
                        else
                        {

                            using (var targetFileStream = File.Create(filepath))
                            {
                                //section.Body是System.IO.Stream类型，表示的是Http请求中一个section的数据流，从该数据流中可以读出每一个section的全部数据，所以我们下面也可以不用section.Body.CopyToAsync方法，而是在一个循环中用section.Body.Read方法自己读出数据，再将数据写入到targetFileStream
                                await section.Body.CopyToAsync(targetFileStream, loadBufferBytes);

                            }
                        }


                    }

                    else if (MultipartRequestHelper.HasFormDataContentDisposition(contentDisposition))
                    {

                        var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name);
                        var encoding = GetEncoding(section);
                        using (var streamReader = new StreamReader(
                            section.Body,
                            encoding,
                            detectEncodingFromByteOrderMarks: true,
                            bufferSize: 1024,
                            leaveOpen: true))
                        {
                            // The value length limit is enforced by MultipartBodyLengthLimit
                            var value = await streamReader.ReadToEndAsync();
                            if (String.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
                            {
                                value = String.Empty;
                            }
                            formAccumulator.Append(key.Value, value); // For .NET Core <2.0 remove ".Value" from key

                            if (formAccumulator.ValueCount > _defaultFormOptions.ValueCountLimit)
                            {
                                throw new InvalidDataException($"Form key count limit {_defaultFormOptions.ValueCountLimit} exceeded.");
                            }
                        }
                    }
                }



                try
                {
                    section = await reader.ReadNextSectionAsync();//用于读取Http请求中的下一个section数据
                }
                catch (Exception ex)
                {


                }
                finally
                {
                    if (section == null || section.Headers.Count <= 0 || string.IsNullOrEmpty(section.ContentDisposition))
                    {
                        section = null;//不然死循环
                    }

                }

            }

            // Bind form data to a model
            var formValueProvider = new FormValueProvider(
                BindingSource.Form,
                new FormCollection(formAccumulator.GetResults()),
                CultureInfo.CurrentCulture);

            return formValueProvider;
        }
        private static Encoding GetEncoding(MultipartSection section)
        {
            Microsoft.Net.Http.Headers.MediaTypeHeaderValue mediaType;
            var hasMediaTypeHeader = Microsoft.Net.Http.Headers.MediaTypeHeaderValue.TryParse(section.ContentType, out mediaType);
            // UTF-7 is insecure and should not be honored. UTF-8 will succeed in 
            // most cases.
            if (!hasMediaTypeHeader || Encoding.UTF7.Equals(mediaType.Encoding))
            {
                return Encoding.UTF8;
            }
            return mediaType.Encoding;
        }

        #region 下载使用
        public static DownLoadInfo Download(string fileName)
        {
            var addrUrl = fileName;
            var stream = System.IO.File.OpenRead(addrUrl);
            string exten = Path.GetExtension(fileName);
            string fileExt = exten.Substring(exten.IndexOf('.'));//需要不含.
            //获取文件的ContentType
            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            var memi = provider.Mappings[fileExt];
            //return File(stream, memi, Path.GetFileName(addrUrl));
            return new DownLoadInfo
            {
                NfFileStream = stream,
                Memi = memi,
                FileName = Path.GetFileName(addrUrl)
            };
        }
        #endregion

    }
}
