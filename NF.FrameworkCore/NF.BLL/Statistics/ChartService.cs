using Microsoft.EntityFrameworkCore;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NF.BLL
{
    /// <summary>
    /// 图表
    /// </summary>
    public partial class ChartService : BaseService<ContractInfo>, IChartService
    {
        #region 基础初始化

        private DbSet<ContractInfo> _ContractInfoSet = null;
        private DbSet<ContActualFinance> _ContActualFinanceSet = null;
        public ChartService(DbContext dbContext)
           : base(dbContext)
        {
            _ContractInfoSet = base.Db.Set<ContractInfo>();
            _ContActualFinanceSet = base.Db.Set<ContActualFinance>();
        }

        #endregion 
        /// <summary>
        /// 统计图
        /// </summary>
        /// <param name="Year"></param>
        /// <returns></returns>
        public HtZhiXingTongji GetHtZxTj(int Year = 0)
        {
            //var tongji = new HtZhiXingTongji();
            var temyear = Year == 0 ? DateTime.Now.Year : Year;
            List<decimal> listskhtje = new List<decimal>();
            List<decimal> listfkhtje = new List<decimal>();
            List<decimal> listsjskje = new List<decimal>();
            List<decimal> listsjfkje = new List<decimal>();
            IList<long> listhtcount = new List<long>();
            //合同列表
            var listht = _ContractInfoSet.Where(a => a.CreateDateTime.Year == temyear)
                .Select(s => new
                {
                    // Id = s.Id,
                    Mth = s.CreateDateTime.Month,
                    Je = s.AmountMoney,
                    Ftype = s.FinanceType
                }).ToList();
            //实际资金
            var listsjzj = _ContActualFinanceSet.Where(a => a.CreateDateTime.Year == temyear)
                .Select(s => new
                {
                    Mth = s.CreateDateTime.Month,
                    Je = s.AmountMoney,
                    Ftype = s.FinceType
                }).ToList();
            var lisskhts = listht.Where(a => a.Ftype == 0).ToList();
            var lisfkhts = listht.Where(a => a.Ftype == 1).ToList();
            var lissksjzjs = listsjzj.Where(a => a.Ftype == 0).ToList();
            var lisfksjzjs = listsjzj.Where(a => a.Ftype == 1).ToList();
            for (var i = 1; i <= 12; i++)
            {
                listskhtje.Add(lisskhts.Where(a=>a.Mth==i).Sum(a=>a.Je??0));
                listfkhtje.Add(lisfkhts.Where(a => a.Mth == i).Sum(a => a.Je ?? 0));
                listsjskje.Add(lissksjzjs.Where(a => a.Mth == i).Sum(a => a.Je ?? 0));
                listsjfkje.Add(lisfksjzjs.Where(a => a.Mth == i).Sum(a => a.Je ?? 0));
                listhtcount.Add(listht.Where(a=>a.Mth==i).Count());
            }

            
            return new HtZhiXingTongji()
            {
                SkhtJe = listskhtje ,
                FkHtJe = listfkhtje,
                SjSkJe = listsjskje,
                SjFkJe = listsjfkje,
                HtCount=listhtcount
            };

        }
    /// <summary>
    /// 合同列表饼图
    /// </summary>
    /// <returns></returns>

      public HtLbTjInfo GetHtLbPie()
        {
            HtLbTjInfo htLbTjInfo = new HtLbTjInfo();
            IList<HtLbJeInfo> lbJeInfos = new List<HtLbJeInfo>();
            var listht = _ContractInfoSet.Select(a => new { a.ContTypeId, a.AmountMoney }).ToList();
            var lbs = Db.Set<DataDictionary>().Where(a=>a.DtypeNumber==1)
                .Take(5).Select(a=>new {a.Id,a.Name}).ToList();

           var listlbs= lbs.Select(a => a.Name).ToList();
            

            
            foreach (var item in lbs) {
                HtLbJeInfo htLbJe = new HtLbJeInfo();
                htLbJe.name = item.Name;
                htLbJe.value = listht.Where(a => a.ContTypeId == item.Id).Sum(a => a.AmountMoney ?? 0);
                lbJeInfos.Add(htLbJe);
            }
            //如果字典设置类别大于就用其他代替
            if (lbs.Count > 5)
            {
                listlbs.Add("其他");
                var top5lb = lbs.Select(a => (int?)a.Id).ToArray();
                HtLbJeInfo htqtLbJe = new HtLbJeInfo();
                htqtLbJe.name = "其他";
                htqtLbJe.value = listht.Where(a => !top5lb.Contains(a.ContTypeId)).Sum(a => a.AmountMoney ?? 0);
                lbJeInfos.Add(htqtLbJe);
            }
            htLbTjInfo.HtLbs = listlbs;
            htLbTjInfo.HtLbJes = lbJeInfos;
            return htLbTjInfo;

        }
    }
}
