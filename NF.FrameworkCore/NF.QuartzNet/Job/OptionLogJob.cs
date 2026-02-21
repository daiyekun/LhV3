
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NF.QuartzNet.Job
{
    /// <summary>
    /// 操作日志Job
    /// </summary>
    public class OptionLogJob : IJob
    {
        private ILogger _logger;
        public OptionLogJob(ILogger logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() => {

                string path = @"D:\\1.txt";
                string value = DateTime.Now.ToString();
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                StreamWriter streamWriter = new StreamWriter(path, true);
                streamWriter.WriteLineAsync(value);
                streamWriter.Flush();
                streamWriter.Close();
            });
           
        }
    }
}
