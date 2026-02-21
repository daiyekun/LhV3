using Microsoft.EntityFrameworkCore;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NF.BLL
{
    public partial class NoHipler : BaseService<SysPermissionModel>, INoHipler
    {

        private DbSet<Department> _DepartmentModelSet = null;
        private DbSet<DataDictionary> _DataDictionaryModelSet = null;

        public NoHipler(DbContext dbContext)
           : base(dbContext)
        {
            _DepartmentModelSet = base.Db.Set<Department>();
            _DataDictionaryModelSet = base.Db.Set<DataDictionary>();
        }
        #region 旧的合同编号-作废
        /// <summary>
        /// 合同编号生成
        ///编号规则：公司+部门+合同类别+日期+编号
        ///<param name="DeptId">经办机构-公司代码</param>
        ///<param name="ContTypeId">合同类别</param>
        ///<param name="MainDeptShortName">部门代码</param>
        /// </summary>
        /// <returns></returns>
        public string ContractNo2(int DeptId, int ContTypeId, string MainDeptShortName)
        {
            //var GSNOinfo = _DepartmentModelSet.FirstOrDefault(a => a.Id == DeptId);//经办机构Department
            //var HTLBinfo = _DataDictionaryModelSet.FirstOrDefault(a => a.Id == ContTypeId);//合同类别
            //string HTLB = GSNOinfo == null? string.Empty : GSNOinfo.ShortName;//
            //string GSNO = HTLBinfo == null? string.Empty : HTLBinfo.ShortName;//
            //DateTime data = DateTime.Now;
            //string YMDH = data.ToString("yyyyMMddHH");
            StringBuilder strNo = new StringBuilder();
            //strNo.Append(GSNO);
            strNo.Append(MainDeptShortName+"-");
            //strNo.Append(HTLB);
            //strNo.Append(YMDH);
            string no = strNo.ToString();
            //按编号格式取得编号
            var nolist = base.Db.Set<ContractInfo>().OrderByDescending(c => c.Id).Select(c => c.Code).ToList();
            //倒序遍历编号，看是否符合自动编号规则
            return GetNumber(no, nolist);
        }

        /// <summary>
        /// 生成流水号
        /// </summary>
        /// <param name="no">流水号前的一段字符</param>
        /// <param name="nolist">当前满足条件的编号</param>
        /// <returns></returns>
        private  string GetNumber(string no, List<string> nolist)
        {
            DateTime data = DateTime.Now;
            string YMDH = data.ToString("yyyyMMdd");
            for (int i = 0; i < nolist.Count; i++)
            {
                //没有'-'不符合
                if (nolist[i].IndexOf("-") < 0)
                    continue;
                //取得流水号
                string numstring = nolist[i].Substring(nolist[i].LastIndexOf("-") + 1);
                //流水号不是3位长度，不符合
                if (numstring.Length != 3)
                    continue;
                int num = 0;
                //流水号不是数字格式，不符合
                if (!int.TryParse(numstring, out num))
                    continue;
                //按规则拼接合同编号
                string temstr = (num + 1).ToString().PadLeft(3, '0');
                //每年1月1日自动重置编号
                if (data.Month==1&& data.Day==1)
                    temstr= "001";
                return no + "-" + temstr;
            }
            return no + "-" + "001";
        }
        #endregion
        /// <summary>
        /// 项目编号
        /// </summary>
        /// <returns></returns>
        public string ProjectNo()
        {
            var count = this.Db.Set<ProjectManager>().Count();
            string temstr = (count + 1).ToString().PadLeft(3, '0');
            return temstr;
        }
        /// <summary>
        /// 客户-供应商-其他对方 编号自动生成
        /// </summary>
        /// <param name="gyslb">供应商类别</param>
        /// <returns></returns>
        public string CustrNo(int gyslb=0, int compType = 1)
        {
            string temstr = "Error No";
            if (gyslb>0)
            {
                var gyslbinfo=this.Db.Set<DataDictionary>().Where(a => a.Id == gyslb).FirstOrDefault();
                if (gyslbinfo != null)
                {
                    var count = this.Db.Set<Company>()
                        .Where(a=>a.CompClassId== gyslb&&a.IsDelete!=1&&a.Ctype== compType).Count();
                    temstr = $"{gyslbinfo.Remark}-{(count + 1).ToString().PadLeft(4, '0')}";

                }
                else
                {
                    return "没有供应商类别";
                }
                
            }
            else
            {
                DateTime data = DateTime.Now;
                string YMDH = data.ToString("yyyyMMdd");
                var count = this.Db.Set<Company>().Count();
                 temstr = $"{YMDH}{(count + 1).ToString().PadLeft(4, '0')}";
               
            }
            return temstr;


        }

        #region 合同编号自动生成

        /// <summary>
        /// 设计变更编号生成
        ///编号规则： GDLY-（分包合同编号）-SJBGD-001（流水号）
        ///<param name="contId">分包合同编号</param>
        /// </summary>
        /// <returns></returns>
        public  string ContractNo(int DeptId, int ContTypeId, string MainDeptShortName)
        {

            //查询分包合同
            //var contInfo = wd.WOO_CONTRACT.Where(a => a.ID == contId).FirstOrDefault();
            //string contNo = contInfo == null ? string.Empty : contInfo.NO;
            var date = DateTime.Now.ToString("yyyy");
            var deptshortno = this.Db.Set<Department>().FirstOrDefault(a => a.Id == DeptId).ShortName;
            StringBuilder strNo = new StringBuilder();
            strNo.Append($"{deptshortno}-{date}-");
            
            //strNo.Append("-SJBGD");
            string no = strNo.ToString();
            //按编号格式取得编号
            var nolist = this.Db.Set<ContractInfo>().Where(c => c.Code.Contains(no)).OrderByDescending(c => c.Code).Select(c => c.Code).ToList();
            //倒序遍历编号，看是否符合自动编号规则
            return GetNumber1(no, nolist);

        }

        
       
       
        /// <summary>
        /// 生成流水号
        /// </summary>
        /// <param name="no">流水号前的一段字符</param>
        /// <param name="nolist">当前满足条件的编号</param>
        /// <returns></returns>
        private static string GetNumber1(string no, List<string> nolist)
        {
            for (int i = 0; i < nolist.Count; i++)
            {
                //没有'-'不符合
                if (nolist[i].IndexOf("-") < 0)
                    continue;
                //取得流水号
                string numstring = nolist[i].Substring(nolist[i].LastIndexOf("-") + 1);
                //流水号不是3位长度，不符合
                if (numstring.Length != 3)
                    continue;
                int num = 0;
                //流水号不是数字格式，不符合
                if (!int.TryParse(numstring, out num))
                    continue;
                //按规则拼接合同编号
                string temstr = (num + 1).ToString().PadLeft(3, '0');

                return no + temstr;
            }
            return no + "001";
        }
        #endregion
    }
}
