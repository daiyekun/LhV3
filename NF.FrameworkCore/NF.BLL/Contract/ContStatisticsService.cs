using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NF.Model.Models;
using NF.ViewModel.Models;
using System.Linq.Expressions;
using NF.Common.Utility;

namespace NF.BLL
{
    /// <summary>
    /// 统计表
    /// </summary>
    public partial  class ContStatisticService
    {

        /// <summary>
        /// 设置统计表字段
        /// </summary>
        /// <param name="No"></param>
        public void SetContractTongJi(string No="")
        {
            if (!string.IsNullOrEmpty(No))
            {
                SetContTjByHtNo(No);
            }
            else
            {
                SetContTj();

            }

        }


        /// <summary>
        /// 统计全部
        /// </summary>
        public void SetContTj()
        {
            var conttjs = this.Db.Set<ContStatistic>().ToList();
            var conts = this.Db.Set<ContractInfo>().Where(a => a.IsDelete != 1).ToList();
            //发票金额
            var invoices = this.Db.Set<ContInvoice>().Where(a => a.IsDelete != 1).ToList();
            //实际资金
            var actfinces = this.Db.Set<ContActualFinance>().Where(a => a.IsDelete != 1).ToList();
            StringBuilder strb = new StringBuilder();
            IList<ContStatistic> liststatic = new List<ContStatistic>();
            foreach (var ht in conts)
            {
                //发票建立金额
                var fpjlje = invoices.Where(a => a.ContId == ht.Id).Sum(a => (decimal?)a.AmountMoney ?? 0);
                //发票确认金额
                var fpqrje = invoices.Where(a => a.ContId == ht.Id&&(a.InState==2|| a.InState==3))
                    .Sum(a => (decimal?)a.AmountMoney ?? 0);
                //实际资金建立
                var jejl= actfinces.Where(a => a.ContId == ht.Id).Sum(a => (decimal?)a.AmountMoney ?? 0);
                //实际资金确定
                var jeqr = actfinces.Where(a => a.ContId == ht.Id && a.Astate == 2 )
                    .Sum(a => (decimal?)a.AmountMoney ?? 0);
                //统计金额对象
                ContStatistic statistic = new ContStatistic();
                statistic.ContId = ht.Id;
                statistic.InvoiceAmount = fpjlje;//发票建立金额
                statistic.CompInAm = fpqrje;
                statistic.ActualAmount = jejl;
                statistic.CompActAm = jeqr;
                if ((statistic.CompActAm ?? 0) == 0 || (ht.AmountMoney ?? 0) == 0)
                {
                    statistic.CompRatio = 0;
                }
                else
                {
                    var tempval = PublicMethod.DivisionTWoDec((statistic.CompActAm ?? 0), (ht.AmountMoney ?? 0));//保留四舍五入两位小数
                    statistic.CompRatio = tempval > 1 ? 1 : tempval;//最高封顶100%
                }
                statistic.BalaTick = fpqrje - jeqr;

                if (conttjs.Any(a => a.ContId == ht.Id))
                {
                    var tjinfo = this.Db.Set<ContStatistic>().Where(a=>a.ContId==ht.Id).FirstOrDefault();

                    tjinfo.InvoiceAmount = statistic.InvoiceAmount;
                    tjinfo.CompInAm = statistic.CompInAm;
                    tjinfo.ActualAmount = statistic.ActualAmount;
                    tjinfo.CompRatio = statistic.CompRatio;
                    tjinfo.CompActAm = statistic.CompActAm;
                    tjinfo.BalaTick = statistic.BalaTick;
                    tjinfo.ModifyUserId = 1;
                    tjinfo.ModifyDateTime = DateTime.Now;
                    this.Update(tjinfo);
                    strb.Append($"update ContractInfo set ContStaticId={tjinfo.Id} where Id={ht.Id};");
                }
                else
                {
                    statistic.ModifyUserId = 1;
                    statistic.ModifyDateTime = DateTime.Now;
                    var info= this.Add(statistic);
                    strb.Append($"update ContractInfo set ContStaticId={info.Id} where Id={ht.Id};");
                }

            }

            var stringsql = strb.ToString();
            if (!string.IsNullOrEmpty(stringsql))
            {
                this.ExecuteSqlCommand(stringsql);
            }


        }
        /// <summary>
        /// 统计个别合同统计
        /// </summary>
        public void SetContTjByHtNo(string No)
        {
            StringBuilder strb = new StringBuilder();
            var ht = this.Db.Set<ContractInfo>().Where(a => a.Code == No).FirstOrDefault();
            if (ht != null)
            {
                
                //发票建立金额
                var fpjlje = this.Db.Set<ContInvoice>().Where(a => a.ContId == ht.Id).Sum(a => (decimal?)a.AmountMoney ?? 0);
                //发票确认金额
                var fpqrje = this.Db.Set<ContInvoice>().Where(a => a.ContId == ht.Id && (a.InState == 2 || a.InState == 3))
                    .Sum(a => (decimal?)a.AmountMoney ?? 0);
                //实际资金建立
                var jejl = this.Db.Set<ContActualFinance>().Where(a => a.ContId == ht.Id).Sum(a => (decimal?)a.AmountMoney ?? 0);
                //实际资金确定
                var jeqr = this.Db.Set<ContActualFinance>().Where(a => a.ContId == ht.Id && a.Astate == 2)
                    .Sum(a => (decimal?)a.AmountMoney ?? 0);
                var httjinfo = this.Db.Set<ContStatistic>().Where(a => a.ContId == ht.Id).FirstOrDefault();
                if (httjinfo == null)
                {
                    var tjinfo = new ContStatistic();
                    tjinfo.InvoiceAmount = fpjlje;
                    tjinfo.CompInAm = fpqrje;
                    tjinfo.ActualAmount = jejl;
                    tjinfo.CompActAm = jeqr;
                    tjinfo.ContId = ht.Id;
                    if ((tjinfo.CompActAm ?? 0) == 0 || (ht.AmountMoney ?? 0) == 0)
                    {
                        tjinfo.CompRatio = 0;
                    }
                    else
                    {
                        var tempval = PublicMethod.DivisionTWoDec((tjinfo.CompActAm ?? 0), (ht.AmountMoney ?? 0));//保留四舍五入两位小数
                        tjinfo.CompRatio = tempval > 1 ? 1 : tempval;//最高封顶100%
                    }
                    tjinfo.BalaTick = fpqrje - jeqr;
                    tjinfo.ModifyUserId = 1;
                    tjinfo.ModifyDateTime = DateTime.Now;
                    var info=this.Add(tjinfo);
                    strb.Append($"update ContractInfo set ContStaticId={info.Id} where Id={ht.Id};");

                }
                else
                {
                   
                    httjinfo.InvoiceAmount = fpjlje;
                    httjinfo.CompInAm = fpqrje;
                    httjinfo.ActualAmount = jejl;
                    httjinfo.CompActAm = jeqr;
                    if ((httjinfo.CompActAm ?? 0) == 0 || (ht.AmountMoney ?? 0) == 0)
                    {
                        httjinfo.CompRatio = 0;
                    }
                    else
                    {
                        var tempval = PublicMethod.DivisionTWoDec((httjinfo.CompActAm ?? 0), (ht.AmountMoney ?? 0));//保留四舍五入两位小数
                        httjinfo.CompRatio = tempval > 1 ? 1 : tempval;//最高封顶100%
                    }
                    httjinfo.BalaTick = fpqrje - jeqr;
                    httjinfo.ContId = ht.Id;
                    httjinfo.ModifyUserId = 1;
                    httjinfo.ModifyDateTime = DateTime.Now;
                    this.Db.Update(httjinfo);
                    strb.Append($"update ContractInfo set ContStaticId={httjinfo.Id} where Id={ht.Id};");

                }

                var stringsql = strb.ToString();
                if (!string.IsNullOrEmpty(stringsql))
                {
                    this.ExecuteSqlCommand(stringsql);
                }

            }

        }
    }
}
