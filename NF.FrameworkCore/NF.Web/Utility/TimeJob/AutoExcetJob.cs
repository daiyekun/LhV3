using Microsoft.Extensions.DependencyInjection;
using NF.IBLL;
using Pomelo.AspNetCore.TimedJob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NF.Common.Extend;
using NF.Common.Utility;
using NF.Model.Models;
using NF.BLL;
using Microsoft.EntityFrameworkCore;
using NF.Common.Models;
using Microsoft.AspNetCore.Builder;
using NF.Web.Utility.DI;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Utility;

namespace NF.Web.Utility.TimeJob
{
    public class AutoExcetJob: Job
    {
       
        // Begin 起始时间（2016-11-29 22:10）；Interval执行时间间隔，单位是毫秒，建议使用以下格式，此处为3（ 1000 * 3600 * 3）小时；
        //SkipWhileExecuting是否等待上一个执行完成，true为等待；
        [Invoke(Begin = "2018-01-01 01:10", Interval =1000*10, SkipWhileExecuting = true)]
        public void Run()
        {
            TimeJobOptionUtility.GetQueueWriteDb();
            //TimJobOptionBusiness.CreateContHistoryTabData();


        }
       

        private static IOptionLogService GetService()
        {
            //var services = new ServiceCollection();
            //var connection = ConfigurationManager.GetAppSettings<ConnectionStrings>("ConnectionStrings");
            //services.AddDbContext<NFDBContext>(options => options.UseSqlServer(connection.SqlConnection));
            //services.AddScoped<DbContext>(p => p.GetService<NFDBContext>());
            //services.AddTransient<IOptionLogService, OptionLogService>();
            //var provider = services.BuildServiceProvider();
            //IOptionLogService optionlogService = provider.GetService<IOptionLogService>();
            //return optionlogService;
            return ServicesDIUtility.GetService<IOptionLogService, OptionLogService, OptionLog>();
        }

        



    }
   
}
