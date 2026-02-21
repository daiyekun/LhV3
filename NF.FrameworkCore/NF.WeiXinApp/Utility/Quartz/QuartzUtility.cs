using Microsoft.Extensions.Configuration;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.WeiXinApp.Utility.Quartz
{
    public class QuartzUtility
    {
        /// <summary>
        /// Quarzt初始化
        /// </summary>
        public static void QuarztInit(string WxCron)
        {
            IScheduler scheduler = new StdSchedulerFactory().GetScheduler().Result;
            IJobDetail testJob = JobBuilder.Create<WxMsgSubmitJob>()
                     .WithIdentity("hetong", "group1")
                     .WithDescription("this is hetongtimejob")
                     .StoreDurably()
                     .Build();

            ITrigger trigger =
                        TriggerBuilder.Create()
                             .StartAt(DateTime.Now)//什么时候开始执行
                             .WithCronSchedule(WxCron) //https://cron.qqe2.com/
                             .Build();
            //每个三秒执行一次：0/3 * * * * ?
            // 每天执行一次0 0 0 1/1 * ?
            scheduler.ScheduleJob(testJob, trigger);
            scheduler.Start();
        }

        /// <summary>
        /// 到期提醒
        /// </summary>
        public static void QuarzDaoqitInit(string WxCron)
        {
            IScheduler scheduler = new StdSchedulerFactory().GetScheduler().Result;
            IJobDetail testJob = JobBuilder.Create<WxDqoQiMsgJob>()
                     .WithIdentity("hetongdq", "groupdq2")
                     .WithDescription("this is hetongtimejobdq")
                     .StoreDurably()
                     .Build();

            ITrigger trigger =
                        TriggerBuilder.Create()
                             .StartAt(DateTime.Now)//什么时候开始执行
                             .WithCronSchedule(WxCron) //https://cron.qqe2.com/
                             .Build();
            //每个三秒执行一次：0/3 * * * * ?
            // 每天执行一次0 0 0 1/1 * ?
            scheduler.ScheduleJob(testJob, trigger);
            scheduler.Start();
        }

        /// <summary>
        /// 计划每周5提醒一次待处理审批条数
        /// </summary>
        public static void QuarzAppRowMsg(string WxCron)
        {
            IScheduler scheduler = new StdSchedulerFactory().GetScheduler().Result;
            IJobDetail testJob = JobBuilder.Create<WxAppRowsJob>()
                     .WithIdentity("hetongdqrow", "groupdq2row")
                     .WithDescription("this is hetongtimejobdqrow")
                     .StoreDurably()
                     .Build();

            ITrigger trigger =
                        TriggerBuilder.Create()
                             .StartAt(DateTime.Now)//什么时候开始执行
                             .WithCronSchedule(WxCron) //https://cron.qqe2.com/
                             .Build();
            //每个三秒执行一次：0/3 * * * * ?
            // 每天执行一次0 0 0 1/1 * ?
            scheduler.ScheduleJob(testJob, trigger);
            scheduler.Start();
        }
    }
}
