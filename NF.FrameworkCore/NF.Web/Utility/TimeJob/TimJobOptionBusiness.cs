using AutoMapper;
using NF.BLL;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Models;
using NF.Web.Utility.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace NF.Web.Utility.TimeJob
{
 
    public  class TimJobOptionBusiness
    {
        private IMapper _IMapper;
        //private IMapper _IMapper;
        //public TimJobOptionBusiness(IMapper IMappr)
        //{
        //    _IMapper = IMappr;
        //}


        /// <summary>
        /// 创建合同标签历史信息（计划资金、标的、）
        /// </summary>
        //public static  void CreateContHistoryTabData()
        //{   
       
        //    if (RedisUtility._redisData.KeyExists(StaticData.AddContHistory))
        //    {
        //        IContPlanFinanceService planfinanceService = ServicesDIUtility.GetService<IContPlanFinanceService, ContPlanFinanceService, ContPlanFinance>();
        //        MappContToHistory mph = RedisHelper.ListLeftPopToObj<MappContToHistory>(StaticData.AddContHistory);

        //        var planFinances = planfinanceService.GetQueryable(a => a.ContId == mph.ContId).ToList();
        //        IList<ContPlanFinanceHistory> listHisplanfinance = new List<ContPlanFinanceHistory>();
        //        foreach (var finceinfo in planFinances)
        //        {
        //            var sd= _IMapper.Map<ContPlanFinance, ContPlanFinanceHistory>(finceinfo);

        //            listHisplanfinance.Add(sd);//finceinfo.ToModel<ContPlanFinance, ContPlanFinanceHistory>());
        //        }
        //        IContPlanFinanceHistoryService planfinanceHisService = ServicesDIUtility.GetService<IContPlanFinanceHistoryService, ContPlanFinanceHistoryService, ContPlanFinanceHistory>();
        //        planfinanceHisService.Add(listHisplanfinance);
        //    }
        //}
    }
}
