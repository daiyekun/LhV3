using NF.Common.Extend;
using NF.Common.Utility;
using NF.IBLL;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models.AdvQuerySearch;
using NF.ViewModel.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using System.Diagnostics;
using System.Globalization;

namespace NF.WebAPI.Utility.Common
{
   
    /// <summary>
    /// 高级查询解析
    /// </summary>
    public class AdvQueryHelper
    {
      //  private IProjectManagerService IProjectManagerService;
        public static IList<AdvQueryInfo> GetAdvQuery(string jsonstr)
        {
            //if (jsonstr.Contains("jsonStr"))
            //{
            //    jsonstr = jsonstr.Substring(jsonstr.IndexOf("["), jsonstr.Length - jsonstr.IndexOf("[") - 2);

            //}
           
            return JsonUtility.DeserializeJsonToList<AdvQueryInfo>(jsonstr);
        }

        public static IList<AdvJbcxQueryInfo> GetAdvJbcxQuery(string jsonstr)
        {
            return JsonUtility.DeserializeJsonToList<AdvJbcxQueryInfo>(jsonstr);
        }
        #region 合同对方高级查询

        /// <summary>
        /// 合同对方高级查询
        /// </summary>
        /// <param name="pageParam">页查询条件</param>
        /// <param name="_IUserInforService">用户查询业务类</param>
        /// <returns>合同对方查询表达式</returns>
        public static  Expression<Func<Model.Models.Company, bool>> GetAdvQueryCompany(
              PageparamInfo pageParam
            , IUserInforService _IUserInforService)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.Company>();
            if (!string.IsNullOrEmpty(pageParam.jsonStr))
            {//高级查询
                var advquerys = AdvQueryHelper.GetAdvQuery(pageParam.jsonStr);
                var predicateAndAdv = PredicateBuilder.True<Model.Models.Company>();
                var predicateOrAdv = PredicateBuilder.False<Model.Models.Company>();
                foreach (var adv in advquerys)
                {
                    #region and
                    if (adv.logicalOperator == "and")
                    {
                        switch (adv.conditionFieldVal)
                        {
                            case "Name"://名称
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Name == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "Code"://编号
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Code == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Code.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Code.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "FirstContact"://首要联系人
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.FirstContact == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.FirstContact.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.FirstContact.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "Reserve1"://备用1
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Reserve1 == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Reserve1.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Reserve1.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "Reserve2"://备用2
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Reserve2 == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Reserve2.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Reserve2.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "CompClassId"://类别
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            int classId = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId);
                                            predicateAndAdv = predicateAndAdv.And(a => a.CompClassId == classId);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.CompClass.Name.Contains(adv.conditionValueVal.text));
                                            break;
                                        default:
                                            int classId0 = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId0);
                                            predicateAndAdv = predicateAndAdv.And(a => a.CompClassId == classId0);
                                            break;
                                    }
                                }
                                break;
                            case "CreateDateTime"://创建日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime >= dt0 && a.CreateDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime <= dt1);

                                            }

                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "Trade"://行业
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Trade == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Trade.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Trade.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "CreateUserId"://创建人
                                {
                                    var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(adv.conditionValueVal.value)).Select(a => a.Id).ToArray();
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于

                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.CreateUserId));
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.CreateUserId));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.CreateUserId));
                                            break;
                                    }
                                }
                                break;
                            case "PrincipalUserId"://负责人
                                {
                                    var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(adv.conditionValueVal.value)).Select(a => a.Id).ToArray();
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于

                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.PrincipalUserId ?? 0));
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.PrincipalUserId ?? 0));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.PrincipalUserId ?? 0));
                                            break;
                                    }
                                }
                                break;
                            case "Cstate"://状态
                                {
                                    byte czt = 10;
                                    byte.TryParse(adv.conditionValueVal.value, out czt);
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cstate == czt);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cstate == czt);
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cstate == czt);
                                            break;
                                    }
                                }
                                break;

                            case "WfCurrNodeName"://当前节点
                                switch (adv.conditionOptionVal)
                                {
                                    
                                    case "like"://包含
                                        predicateAndAdv = predicateAndAdv.And(a => a.WfCurrNodeName !=null && a.WfCurrNodeName.Contains(adv.conditionValueVal.value)&&(a.WfState==1||a.WfState==3));
                                        break;
                                    default:
                                        predicateAndAdv = predicateAndAdv.And(a => a.WfCurrNodeName != null && a.WfCurrNodeName.Contains(adv.conditionValueVal.value) && (a.WfState == 1 || a.WfState == 3));
                                        break;
                                }
                                break;
                        }
                        predicateAnd = predicateAnd.And(predicateAndAdv);
                    }
                    #endregion and
                    #region Or
                    else if(adv.logicalOperator == "or")
                    {
                        switch (adv.conditionFieldVal)
                        {
                            case "Name"://名称
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Name == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Name.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "Code"://编号
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Code == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Code.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Code.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "FirstContact"://首要联系人
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.FirstContact == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.FirstContact.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.FirstContact.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "Reserve1"://备用1
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Reserve1 == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Reserve1.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Reserve1.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "Reserve2"://备用2
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Reserve2 == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Reserve2.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Reserve2.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "CompClassId"://类别
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            int classId = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.CompClassId == classId);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.CompClass.Name.Contains(adv.conditionValueVal.text));
                                            break;
                                        default:
                                            int classId0 = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId0);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.CompClassId == classId0);
                                            break;
                                    }
                                }
                                break;
                            case "CreateDateTime"://创建日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime >= dt0 && a.CreateDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime <= dt1);

                                            }

                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "Trade"://行业
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Trade == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Trade.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Trade.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "CreateUserId"://创建人
                                {
                                    var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(adv.conditionValueVal.value)).Select(a => a.Id).ToArray();
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于

                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.CreateUserId));
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.CreateUserId));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.CreateUserId));
                                            break;
                                    }
                                }
                                break;
                            case "PrincipalUserId"://负责人
                                {
                                    var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(adv.conditionValueVal.value)).Select(a => a.Id).ToArray();
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于

                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.PrincipalUserId ?? 0));
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.PrincipalUserId ?? 0));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.PrincipalUserId ?? 0));
                                            break;
                                    }
                                }
                                break;
                            case "Cstate"://状态
                                {
                                    byte czt = 10;
                                    byte.TryParse(adv.conditionValueVal.value, out czt);
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cstate == czt);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cstate == czt);
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cstate == czt);
                                            break;
                                    }
                                }
                                break;

                            case "WfCurrNodeName"://当前节点
                                switch (adv.conditionOptionVal)
                                {

                                    case "like"://包含
                                        predicateOrAdv = predicateOrAdv.Or(a => a.WfCurrNodeName!=null && a.WfCurrNodeName.Contains(adv.conditionValueVal.value) && (a.WfState == 1 || a.WfState == 3));
                                        break;
                                    default:
                                        predicateOrAdv = predicateOrAdv.Or(a => a.WfCurrNodeName != null && a.WfCurrNodeName.Contains(adv.conditionValueVal.value) && (a.WfState == 1 || a.WfState == 3));
                                        break;
                                }
                                break;





                        }
                        if (pageParam.jsonStr.Contains("and"))
                        {
                            predicateAnd = predicateAnd.Or(predicateOrAdv);
                        }
                        else
                        {
                            predicateAnd = predicateAnd.And(predicateOrAdv);
                        }
                    }

                    #endregion

                }


            }
            return predicateAnd;
        }
        /// <summary>
        /// 客户基本筛选
        /// 
        /// </summary>
        /// <param name="pageParam">页查询条件</param>
        /// <param name="_IUserInforService">用户查询业务类</param>
        /// <returns>合同对方查询表达式</returns>
        public static Expression<Func<Model.Models.Company, bool>> GetAdvJBSXQueryCompany(
              PageparamInfo pageParam
            , IUserInforService _IUserInforService, IProjectManagerService _IProjectManagerService)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.Company>();
            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//基本筛选
                var advJbcxQuery = AdvQueryHelper.GetAdvJbcxQuery(pageParam.filterSos);



                var predicateAndAdv = PredicateBuilder.True<Model.Models.Company>();
                var predicateOrAdv = PredicateBuilder.False<Model.Models.Company>();
                foreach (var adv in advJbcxQuery)
                {
                    #region and
                    if (adv.prefix == "and")
                    {
                        switch (adv.field)
                        {
                            case "Name"://名称
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Name == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Name.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Name != item.value);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Name.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Code"://编号
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Code == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Code.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Code != item.value);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Code.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "FirstContact"://首要联系人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.FirstContact == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.FirstContact.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.FirstContact != item.value);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Reserve1"://备用1
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Reserve1 == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Reserve1.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Reserve1 != item.value);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Reserve2"://备用2
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Reserve2 == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Reserve2.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Reserve2 != item.value);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "PrincipalUserDisplayName": //客户负责人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        string name = (item.value).ToString().Trim().Replace(" ", "");
                                        int s = _IProjectManagerService.Ren((string)name);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.PrincipalUserId == s);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.PrincipalUserId != s);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CompanyTypeClass"://类别
                                {
                                    foreach (var item in adv.children)
                                    {
                                        string name = (item.value).ToString().Trim().Replace(" ", "");
                                        int s = _IProjectManagerService.XMLB((string)name);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CompClassId == s);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CompClassId != s);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CreateDateTime"://创建日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime er = DateTime.Now;
                                        DateTime.TryParse(item.value, out er);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime == er);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime != er);
                                                break;

                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Trade"://行业
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Trade == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Trade.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Trade != item.value);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                           
                           
                            case "CstateDic"://状态
                                foreach (var item in adv.children)
                                {
                                    byte czt = 10;
                                    byte.TryParse(item.value, out czt);
                                    switch (item.type)
                                    {
                                        case "eq"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cstate == czt);
                                            break;
                                        case "ne"://不等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cstate != czt);
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "WfCurrNodeName"://当前节点
                                foreach (var item in adv.children)
                                {
                                    switch (item.type)
                                    {
                                        case "contain"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.WfCurrNodeName != null && a.WfCurrNodeName.Contains(item.value) && a.WfState == 1 || a.WfState == 3);
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.WfCurrNodeName.Contains(item.value) && (a.WfState == 1 || a.WfState == 3));
                                            break;
                                    }
                                }
                                break;
                        }
                        predicateAnd = predicateAnd.And(predicateAndAdv);
                    }
                    #endregion and

                    #region Or
                    if (adv.prefix == "Or")
                    {
                        switch (adv.field)
                        {
                            case "Name"://名称
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Name == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Name.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Name != item.value);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Name.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Code"://编号
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Code == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Code.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Code != item.value);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Code.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "FirstContact"://首要联系人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.FirstContact == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.FirstContact.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.FirstContact != item.value);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Reserve1"://备用1
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Reserve1 == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Reserve1.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Reserve1 != item.value);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Reserve2"://备用2
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Reserve2 == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Reserve2.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Reserve2 != item.value);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "PrincipalUserDisplayName": //客户负责人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        string name = (item.value).ToString().Trim().Replace(" ", "");
                                        int s = _IProjectManagerService.Ren((string)name);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PrincipalUserId == s);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PrincipalUserId != s);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CompanyTypeClass"://类别
                                {
                                    foreach (var item in adv.children)
                                    {
                                        string name = (item.value).ToString().Trim().Replace(" ", "");
                                        int s = _IProjectManagerService.XMLB((string)name);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CompClassId == s);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CompClassId != s);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CreateDateTime"://创建日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime er = DateTime.Now;
                                        DateTime.TryParse(item.value, out er);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime == er);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime != er);
                                                break;

                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Trade"://行业
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Trade == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Trade.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Trade != item.value);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;


                            case "CstateDic"://状态
                                foreach (var item in adv.children)
                                {
                                    byte czt = 10;
                                    byte.TryParse(item.value, out czt);
                                    switch (item.type)
                                    {
                                        case "eq"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cstate == czt);
                                            break;
                                        case "ne"://不等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cstate != czt);
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "WfCurrNodeName"://当前节点
                                foreach (var item in adv.children)
                                {
                                    switch (item.type)
                                    {
                                        case "contain"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.WfCurrNodeName != null && a.WfCurrNodeName.Contains(item.value) && a.WfState == 1 || a.WfState == 3);
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.WfCurrNodeName.Contains(item.value) && (a.WfState == 1 || a.WfState == 3));
                                            break;
                                    }
                                }
                                break;
                        }
                        predicateAnd = predicateAnd.Or(predicateOrAdv);
                    }

                    #endregion

                }


            }
            return predicateAnd;
        }
        #endregion

        #region 项目

        /// <summary>
        /// 项目高级查询
        /// </summary>
        /// <param name="pageParam">页查询条件</param>
        /// <param name="_IUserInforService">用户查询业务类</param>
        /// <returns>合同对方查询表达式</returns>
        public static Expression<Func<Model.Models.ProjectManager, bool>> GetAdvQueryProject(
              PageparamInfo pageParam
            , IUserInforService _IUserInforService)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.ProjectManager>();
            if (!string.IsNullOrEmpty(pageParam.jsonStr))
            {//高级查询
                var advquerys = AdvQueryHelper.GetAdvQuery(pageParam.jsonStr);
                var predicateAndAdv = PredicateBuilder.True<Model.Models.ProjectManager>();
                var predicateOrAdv = PredicateBuilder.False<Model.Models.ProjectManager>();
                foreach (var adv in advquerys)
                {
                    #region and
                    if (adv.logicalOperator == "and")
                    {
                        switch (adv.conditionFieldVal)
                        {
                            case "Name"://名称
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateAndAdv.And(a => a.Name == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Name.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "Code"://编号
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Code == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Code.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Code.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "CategoryId"://类别
                                {
                                   
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            int classId = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId);
                                            predicateAndAdv = predicateAndAdv.And(a => a.CategoryId == classId);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Category.Name.Contains(adv.conditionValueVal.text));
                                            break;
                                        default:
                                            int classId0 = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId0);
                                            predicateAndAdv = predicateAndAdv.And(a => a.CategoryId == classId0);
                                            break;
                                    }
                                }
                                break;
                            case "PlanBeginDateTime"://计划开始日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateAndAdv = predicateAndAdv.And(a => a.PlanBeginDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.PlanBeginDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.PlanBeginDateTime >= dt0 && a.PlanBeginDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.PlanBeginDateTime <= dt1);

                                            }

                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "PlanCompleteDateTime"://计划完成日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateAndAdv = predicateAndAdv.And(a => a.PlanCompleteDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.PlanCompleteDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.PlanCompleteDateTime >= dt0 && a.PlanCompleteDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.PlanCompleteDateTime <= dt1);

                                            }
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "ActualBeginDateTime"://实际开始日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateAndAdv = predicateAndAdv.And(a => a.ActualBeginDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualBeginDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualBeginDateTime >= dt0 && a.ActualBeginDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualBeginDateTime <= dt1);

                                            }
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "ActualCompleteDateTime"://实际完成日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateAndAdv = predicateAndAdv.And(a => a.ActualCompleteDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualCompleteDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualCompleteDateTime >= dt0 && a.ActualCompleteDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualCompleteDateTime <= dt1);

                                            }
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "BugetCollectAmountMoney"://项目预算收款
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            decimal dcmval = 0m;
                                            Decimal.TryParse(adv.conditionValueVal.value, out dcmval);
                                            predicateAndAdv = predicateAndAdv.And(a => a.BugetCollectAmountMoney == dcmval);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            Decimal dt0 = 0;
                                            Decimal dt1 = 0;
                                            Decimal.TryParse(leftdt, out dt0);
                                            Decimal.TryParse(rightdt, out dt1);
                                            
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.BugetCollectAmountMoney >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.BugetCollectAmountMoney >= dt0 && a.BugetCollectAmountMoney <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.BugetCollectAmountMoney <= dt1);

                                            }
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "BudgetPayAmountMoney"://项目预算付款
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            decimal dcmval = 0m;
                                            Decimal.TryParse(adv.conditionValueVal.value, out dcmval);
                                            predicateAndAdv = predicateAndAdv.And(a => a.BudgetPayAmountMoney == dcmval);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            Decimal dt0 = 0;
                                            Decimal dt1 = 0;
                                            Decimal.TryParse(leftdt, out dt0);
                                            Decimal.TryParse(rightdt, out dt1);

                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.BudgetPayAmountMoney >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.BudgetPayAmountMoney >= dt0 && a.BudgetPayAmountMoney <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.BudgetPayAmountMoney <= dt1);

                                            }
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "CreateDateTime"://创建日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime >= dt0 && a.CreateDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime <= dt1);

                                            }

                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => false);
                                            break;
                                    }
                                }
                                break;
                           
                            case "CreateUserId"://创建人
                                {
                                    var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(adv.conditionValueVal.value)).Select(a => a.Id).ToArray();
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于

                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.CreateUserId));
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.CreateUserId));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.CreateUserId));
                                            break;
                                    }
                                }
                                break;
                            case "PrincipalUserId"://负责人
                                {
                                    var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(adv.conditionValueVal.value)).Select(a => a.Id).ToArray();
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于

                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.PrincipalUserId ?? 0));
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.PrincipalUserId ?? 0));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.PrincipalUserId ?? 0));
                                            break;
                                    }
                                }
                                break;
                            case "Pstate"://状态
                                {
                                    byte czt = 10;
                                    byte.TryParse(adv.conditionValueVal.value, out czt);
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Pstate == czt);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Pstate == czt);
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Pstate == czt);
                                            break;
                                    }
                                }
                                break;
                            case "WfCurrNodeName"://当前节点
                                switch (adv.conditionOptionVal)
                                {

                                    case "like"://包含
                                        predicateAndAdv = predicateAndAdv.And(a => a.WfCurrNodeName!=null && a.WfCurrNodeName.Contains(adv.conditionValueVal.value) && a.WfState == 1 || a.WfState == 3);
                                        break;
                                    default:
                                        predicateAndAdv = predicateAndAdv.And(a => a.WfCurrNodeName.Contains(adv.conditionValueVal.value) && (a.WfState == 1 || a.WfState == 3));
                                        break;
                                }
                                break;
                        }

                        predicateAnd = predicateAnd.And(predicateAndAdv);
                    }
                    #endregion and
                    #region Or
                    else if (adv.logicalOperator == "or")
                    {
                        switch (adv.conditionFieldVal)
                        {
                            case "Name"://名称
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Name == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Name.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "Code"://编号
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Code == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Code.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Code.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "CategoryId"://类别
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            int classId = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.CategoryId == classId);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Category.Name.Contains(adv.conditionValueVal.text));
                                            break;
                                        default:
                                            int classId0 = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId0);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.CategoryId == classId0);
                                            break;
                                    }
                                }
                                break;
                            case "PlanBeginDateTime"://计划开始日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.PlanBeginDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PlanBeginDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PlanBeginDateTime >= dt0 && a.PlanBeginDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PlanBeginDateTime <= dt1);

                                            }

                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "PlanCompleteDateTime"://计划完成日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.PlanCompleteDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PlanCompleteDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PlanCompleteDateTime >= dt0 && a.PlanCompleteDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PlanCompleteDateTime <= dt1);

                                            }
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "ActualBeginDateTime"://实际开始日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ActualBeginDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualBeginDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualBeginDateTime >= dt0 && a.ActualBeginDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualBeginDateTime <= dt1);

                                            }
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "ActualCompleteDateTime"://实际完成日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ActualCompleteDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualCompleteDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualCompleteDateTime >= dt0 && a.ActualCompleteDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualCompleteDateTime <= dt1);

                                            }
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "BugetCollectAmountMoney"://项目预算收款
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            decimal dcmval = 0m;
                                            Decimal.TryParse(adv.conditionValueVal.value, out dcmval);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.BugetCollectAmountMoney == dcmval);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            Decimal dt0 = 0;
                                            Decimal dt1 = 0;
                                            Decimal.TryParse(leftdt, out dt0);
                                            Decimal.TryParse(rightdt, out dt1);

                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.BugetCollectAmountMoney >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.BugetCollectAmountMoney >= dt0 && a.BugetCollectAmountMoney <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.BugetCollectAmountMoney <= dt1);

                                            }
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "BudgetPayAmountMoney"://项目预算付款
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            decimal dcmval = 0m;
                                            Decimal.TryParse(adv.conditionValueVal.value, out dcmval);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.BudgetPayAmountMoney == dcmval);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            Decimal dt0 = 0;
                                            Decimal dt1 = 0;
                                            Decimal.TryParse(leftdt, out dt0);
                                            Decimal.TryParse(rightdt, out dt1);

                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.BudgetPayAmountMoney >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.BudgetPayAmountMoney >= dt0 && a.BudgetPayAmountMoney <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.BudgetPayAmountMoney <= dt1);

                                            }
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "CreateDateTime"://创建日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天             
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime >= dt0 && a.CreateDateTime <= dt1);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime <= dt1);

                                            }

                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => false);
                                            break;
                                    }
                                }
                                break;

                            case "CreateUserId"://创建人
                                {
                                    var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(adv.conditionValueVal.value)).Select(a => a.Id).ToArray();
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于

                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.CreateUserId));
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.CreateUserId));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.CreateUserId));
                                            break;
                                    }
                                }
                                break;
                            case "PrincipalUserId"://负责人
                                {
                                    var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(adv.conditionValueVal.value)).Select(a => a.Id).ToArray();
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于

                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.PrincipalUserId ?? 0));
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.PrincipalUserId ?? 0));
                                            break;
                                        default:
                                            predicateAndAdv =predicateOrAdv.Or(a => userIds.Contains(a.PrincipalUserId ?? 0));
                                            break;
                                    }
                                }
                                break;
                            case "Pstate"://状态
                                {
                                    byte czt = 10;
                                    byte.TryParse(adv.conditionValueVal.value, out czt);
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Pstate == czt);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Pstate == czt);
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.Or(a => a.Pstate == czt);
                                            break;
                                    }
                                }
                                break;
                            case "WfCurrNodeName"://当前节点
                                switch (adv.conditionOptionVal)
                                {

                                    case "like"://包含
                                        predicateAndAdv = predicateAndAdv.Or(a=> a.WfCurrNodeName.Contains(adv.conditionValueVal.value) && (a.WfState == 1 || a.WfState == 3));
                                        break;
                                    default:
                                        predicateAndAdv = predicateAndAdv.Or(a => a.WfCurrNodeName.Contains(adv.conditionValueVal.value) && (a.WfState == 1 || a.WfState == 3));
                                        break;
                                }
                                break; 
                        }
                        if (pageParam.jsonStr.Contains("and"))
                        {
                            predicateAnd = predicateAnd.Or(predicateOrAdv);
                        }
                        else
                        {
                            predicateAnd = predicateAnd.And(predicateOrAdv);
                        }
                    }

                    #endregion

                }


            }
            return predicateAnd;

        

        }
        /// <summary>
        /// 项目基本筛选
        /// 
        /// </summary>
        /// <param name="pageParam">页查询条件</param>
        /// <param name="_IUserInforService">用户查询业务类</param>
        /// <returns>合同对方查询表达式</returns>
        public static Expression<Func<Model.Models.ProjectManager, bool>> GetAdvJBSXQueryProjectManager(
              PageparamInfo pageParam
            , IUserInforService _IUserInforService, IProjectManagerService _IProjectManagerService)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.ProjectManager>();
            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//基本筛选
                var advJbcxQuery = AdvQueryHelper.GetAdvJbcxQuery(pageParam.filterSos);
                var predicateAndAdv = PredicateBuilder.True<Model.Models.ProjectManager>();
                var predicateOrAdv = PredicateBuilder.False<Model.Models.ProjectManager>();
                foreach (var adv in advJbcxQuery)
                {
                    #region and
                    if (adv.prefix == "and")
                    {
                        switch (adv.field)
                        {
                            case "Name"://项目名称
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Name == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Name.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Name != item.value);
                                                break;
                                           
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Name.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Code"://项目编号
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Code == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Code.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Code != item.value);
                                                break;

                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Code.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ProjTypeName"://项目类别
                                {
                                    foreach (var item in adv.children)
                                    {
                                        string name = (item.value).ToString().Trim().Replace(" ", "");
                                       int s= _IProjectManagerService.XMLB((string)name);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CategoryId == s);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CategoryId != s);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "PlanBeginDateTime"://计划开始日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime er = Convert.ToDateTime(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.PlanBeginDateTime == er);
                                                break;
                                          
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.PlanBeginDateTime != er);
                                                break;

                                            //default:
                                            //    predicateAndAdv = predicateAndAdv.And(a => a.Code.Contains(item.value));
                                            //    break;
                                        }
                                    }
                                }
                                break;
                            case "PlanCompleteDateTime"://计划完成日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime er = Convert.ToDateTime(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.PlanCompleteDateTime == er);
                                                break;
                                        
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.PlanCompleteDateTime != er);
                                                break;

                                            //default:
                                            //    predicateAndAdv = predicateAndAdv.And(a => a.PlanCompleteDateTime.Contains(item.value));
                                            //    break;
                                        }
                                    }
                                }
                                break;
                            case "ActualBeginDateTime"://实际开始日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime er = Convert.ToDateTime(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualBeginDateTime == er);
                                                break;

                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualBeginDateTime != er);
                                                break;

                                                //default:
                                                //    predicateAndAdv = predicateAndAdv.And(a => a.PlanCompleteDateTime.Contains(item.value));
                                                //    break;
                                        }
                                    }
                                }
                                break;
                            case "ActualCompleteDateTime"://实际完成日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime er = Convert.ToDateTime(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualCompleteDateTime == er);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualCompleteDateTime != er);
                                                break;

                                                //default:
                                                //    predicateAndAdv = predicateAndAdv.And(a => a.PlanCompleteDateTime.Contains(item.value));
                                                //    break;
                                        }
                                    }
                                }
                                break;
                            case "BugetCollectAmountMoneyThod"://项目预收款
                                {
                                    foreach (var item in adv.children)
                                    {
                                        decimal? je = Convert.ToDecimal(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.BugetCollectAmountMoney == je);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.BugetCollectAmountMoney > je);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.BugetCollectAmountMoney != je);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.BugetCollectAmountMoney < je);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.BugetCollectAmountMoney >= je);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.BugetCollectAmountMoney <= je);
                                                break;

                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "BudgetPayAmountMoney"://项目预算付款 BudgetPayAmountMoneyThod
                                {
                                    foreach (var item in adv.children)
                                    {
                                        decimal? je = Convert.ToDecimal(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.BudgetPayAmountMoney == je);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.BudgetPayAmountMoney > je);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.BudgetPayAmountMoney != je);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.BudgetPayAmountMoney < je);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.BudgetPayAmountMoney >= je);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.BudgetPayAmountMoney <= je);
                                                break;

                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CreateDateTime"://创建日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime er = Convert.ToDateTime(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualCompleteDateTime == er);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualCompleteDateTime != er);
                                                break;

                                                //default:
                                                //    predicateAndAdv = predicateAndAdv.And(a => a.PlanCompleteDateTime.Contains(item.value));
                                                //    break;
                                        }
                                    }
                                }
                                break;
                            case "CreateUserName"://创建人 CreateUserName
                                {
                                    foreach (var item in adv.children)
                                    {
                                        string name = (item.value).ToString().Trim().Replace(" ", "");
                                        int s = _IProjectManagerService.Ren((string)name);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateUserId == s);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateUserId != s);
                                                break;
                                                //default:
                                                //    predicateAndAdv = predicateAndAdv.And(a => a.Code.Contains(item.value));
                                                //    break;
                                        }
                                    }
                                }
                               
                                break;
                            case "PriUserName"://负责人 PriUserName
                                {
                                    foreach (var item in adv.children)
                                    {
                                        string name = (item.value).ToString().Trim().Replace(" ", "");
                                        int s = _IProjectManagerService.Ren((string)name);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.PrincipalUserId == s);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.PrincipalUserId != s);
                                                break;
                                                //default:
                                                //    predicateAndAdv = predicateAndAdv.And(a => a.Code.Contains(item.value));
                                                //    break;
                                        }
                                    }
                                }
                                break;
                            case "Pstate"://状态
                                foreach (var item in adv.children)
                                {
                                    byte czt = 10;
                                    byte.TryParse(item.value, out czt);
                                    switch (item.type)
                                    {
                                        case "eq"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Pstate == czt);
                                            break;
                                        case "ne"://不等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Pstate != czt);
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Pstate == czt);
                                            break;
                                    }
                                }
                                break;
                            case "WfCurrNodeName"://当前节点
                                foreach (var item in adv.children)
                                {
                                    switch (item.type)
                                    {

                                        case "contain"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.WfCurrNodeName != null && a.WfCurrNodeName.Contains(item.value) && a.WfState == 1 || a.WfState == 3);
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.WfCurrNodeName.Contains(item.value) && (a.WfState == 1 || a.WfState == 3));
                                            break;
                                    }
                                }
                                break;
                        }


                        predicateAnd = predicateAnd.And(predicateAndAdv);
                    }
                    #endregion and

                    #region Or

                   else 
                    {
                        switch (adv.field)
                        {
                            case "Name"://项目名称
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Name == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Name.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Name != item.value);
                                                break;

                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Name.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Code"://项目编号
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Code == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Code.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Code != item.value);
                                                break;

                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Code.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ProjTypeName"://项目类别
                                {
                                    foreach (var item in adv.children)
                                    {
                                        string name = (item.value).ToString().Trim().Replace(" ", "");
                                        int s = _IProjectManagerService.XMLB((string)name);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CategoryId == s);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CategoryId != s);
                                                break;
                                                //default:
                                                //    predicateAndAdv = predicateAndAdv.And(a => a.Code.Contains(item.value));
                                                //    break;
                                        }
                                    }
                                }
                                break;
                            case "PlanBeginDateTime"://计划开始日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime er = Convert.ToDateTime(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PlanBeginDateTime == er);
                                                break;

                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PlanBeginDateTime != er);
                                                break;

                                                //default:
                                                //    predicateAndAdv = predicateAndAdv.And(a => a.Code.Contains(item.value));
                                                //    break;
                                        }
                                    }
                                }
                                break;
                            case "PlanCompleteDateTime"://计划完成日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime er = Convert.ToDateTime(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PlanCompleteDateTime == er);
                                                break;

                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PlanCompleteDateTime != er);
                                                break;

                                                //default:
                                                //    predicateAndAdv = predicateAndAdv.And(a => a.PlanCompleteDateTime.Contains(item.value));
                                                //    break;
                                        }
                                    }
                                }
                                break;
                            case "ActualBeginDateTime"://实际开始日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime er = Convert.ToDateTime(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualBeginDateTime == er);
                                                break;

                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualBeginDateTime != er);
                                                break;

                                                //default:
                                                //    predicateAndAdv = predicateAndAdv.And(a => a.PlanCompleteDateTime.Contains(item.value));
                                                //    break;
                                        }
                                    }
                                }
                                break;
                            case "ActualCompleteDateTime"://实际完成日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime er = Convert.ToDateTime(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualCompleteDateTime == er);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualCompleteDateTime != er);
                                                break;

                                                //default:
                                                //    predicateAndAdv = predicateAndAdv.And(a => a.PlanCompleteDateTime.Contains(item.value));
                                                //    break;
                                        }
                                    }
                                }
                                break;
                            case "BugetCollectAmountMoneyThod"://项目预收款
                                {
                                    foreach (var item in adv.children)
                                    {
                                        decimal? je = Convert.ToDecimal(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.BugetCollectAmountMoney == je);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.BugetCollectAmountMoney > je);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.BugetCollectAmountMoney != je);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.BugetCollectAmountMoney < je);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.BugetCollectAmountMoney >= je);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.BugetCollectAmountMoney <= je);
                                                break;

                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "BudgetPayAmountMoneyThod"://项目预算付款 BudgetPayAmountMoneyThod
                                {
                                    foreach (var item in adv.children)
                                    {
                                        decimal? je = Convert.ToDecimal(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.BudgetPayAmountMoney == je);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.BudgetPayAmountMoney > je);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.BudgetPayAmountMoney != je);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.BudgetPayAmountMoney < je);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.BudgetPayAmountMoney >= je);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.BudgetPayAmountMoney <= je);
                                                break;

                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CreateDateTime"://创建日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime er = Convert.ToDateTime(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualCompleteDateTime == er);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualCompleteDateTime != er);
                                                break;

                                                //default:
                                                //    predicateAndAdv = predicateAndAdv.And(a => a.PlanCompleteDateTime.Contains(item.value));
                                                //    break;
                                        }
                                    }
                                }
                                break;
                            case "CreateUserName"://创建人 CreateUserName
                                {
                                    foreach (var item in adv.children)
                                    {
                                        string name = (item.value).ToString().Trim().Replace(" ", "");
                                        int s = _IProjectManagerService.Ren((string)name);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateUserId == s);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateUserId != s);
                                                break;
                                                //default:
                                                //    predicateAndAdv = predicateAndAdv.And(a => a.Code.Contains(item.value));
                                                //    break;
                                        }
                                    }
                                }

                                break;
                            case "PriUserName"://负责人 PriUserName
                                {
                                    foreach (var item in adv.children)
                                    {
                                        string name = (item.value).ToString().Trim().Replace(" ", "");
                                        int s = _IProjectManagerService.Ren((string)name);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PrincipalUserId == s);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PrincipalUserId != s);
                                                break;
                                                //default:
                                                //    predicateAndAdv = predicateAndAdv.And(a => a.Code.Contains(item.value));
                                                //    break;
                                        }
                                    }
                                }
                                break;
                            case "Pstate"://状态
                                foreach (var item in adv.children)
                                {
                                    byte czt = 10;
                                    //byte.TryParse(item.value, out czt);
                                  
                                    byte.TryParse(item.value, out czt);
                                    switch (item.type)
                                    {
                                        case "eq"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Pstate == czt);
                                            break;
                                        case "ne"://不等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Pstate != czt);
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Pstate == czt);
                                            break;
                                    }
                                }
                                break;
                            case "WfCurrNodeName"://当前节点
                                foreach (var item in adv.children)
                                {
                                    switch (item.type)
                                    {

                                        case "contain"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.WfCurrNodeName != null && a.WfCurrNodeName.Contains(item.value) && a.WfState == 1 || a.WfState == 3);
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.WfCurrNodeName.Contains(item.value) && (a.WfState == 1 || a.WfState == 3));
                                            break;
                                    }
                                }
                                break;
                        }
                        predicateAnd = predicateAnd.Or(predicateOrAdv);
                    }
                    #endregion
                }
            }
            return predicateAnd;
        }
        #endregion


        #region 合同

        /// <summary>
        /// 合同高级查询
        /// 
        /// </summary>
        /// <param name="pageParam">页查询条件</param>
        /// <param name="_IUserInforService">用户查询业务类</param>
        /// <returns>合同对方查询表达式</returns>
        public static Expression<Func<Model.Models.ContractInfo, bool>> GetAdvQueryContract(
              PageparamInfo pageParam
            , IUserInforService _IUserInforService)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.ContractInfo>();
            if (!string.IsNullOrEmpty(pageParam.jsonStr))
            {//高级查询
                var advquerys = AdvQueryHelper.GetAdvQuery(pageParam.jsonStr);
                var predicateAndAdv = PredicateBuilder.True<Model.Models.ContractInfo>();
                var predicateOrAdv = PredicateBuilder.False<Model.Models.ContractInfo>();
                foreach (var adv in advquerys)
                {
                    #region and
                    if (adv.logicalOperator == "and")
                    {
                        switch (adv.conditionFieldVal)
                        {
                            case "Name"://名称
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Name == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Name.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "Code"://编号
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Code == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Code.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Code.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "ContTypeId"://类别
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            int classId = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId);
                                            predicateAndAdv = predicateAndAdv.And(a => a.ContTypeId == classId);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.ContType.Name.Contains(adv.conditionValueVal.text));
                                            break;
                                        default:
                                            int classId0 = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId0);
                                            predicateAndAdv = predicateAndAdv.And(a => a.ContTypeId == classId0);
                                            break;
                                    }
                                }
                                break;
                            case "AmountMoney"://合同金额
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            decimal dcmval = 0m;
                                            Decimal.TryParse(adv.conditionValueVal.value, out dcmval);
                                            predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney == dcmval);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            Decimal dt0 = 0;
                                            Decimal dt1 = 0;
                                            Decimal.TryParse(leftdt, out dt0);
                                            Decimal.TryParse(rightdt, out dt1);

                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney >= dt0 && a.AmountMoney <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney <= dt1);

                                            }
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "DeptId"://经办机构
                                {
                                    int deptId = 0;
                                    int.TryParse(adv.conditionValueVal.value, out deptId);
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.DeptId == deptId);
                                            break;
                                        
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.DeptId == deptId);
                                            break;

                                    }
                                }
                                break;
                            case "CompId"://合同对方
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Comp.Name == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Comp.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Comp.Name.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "ProjectId"://项目
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Project.Name== adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Project.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Project.Name.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;

                            case "SngnDateTime"://签订日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateAndAdv = predicateAndAdv.And(a => a.SngnDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.SngnDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.SngnDateTime >= dt0 && a.SngnDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.SngnDateTime <= dt1);

                                            }

                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "EffectiveDateTime"://生效日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateAndAdv = predicateAndAdv.And(a => a.EffectiveDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.EffectiveDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.EffectiveDateTime >= dt0 && a.EffectiveDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.EffectiveDateTime <= dt1);

                                            }

                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => false);
                                            break;
                                    }
                                }
                                break;

                            case "PlanCompleteDateTime"://计划完成日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateAndAdv = predicateAndAdv.And(a => a.PlanCompleteDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.PlanCompleteDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.PlanCompleteDateTime >= dt0 && a.PlanCompleteDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.PlanCompleteDateTime <= dt1);

                                            }
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "ActualCompleteDateTime"://实际完成日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateAndAdv = predicateAndAdv.And(a => a.ActualCompleteDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualCompleteDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualCompleteDateTime >= dt0 && a.ActualCompleteDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualCompleteDateTime <= dt1);

                                            }
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "PrincipalUserId"://负责人
                                {
                                    var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(adv.conditionValueVal.value)).Select(a => a.Id).ToArray();
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于

                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.PrincipalUserId ?? 0));
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.PrincipalUserId ?? 0));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.PrincipalUserId ?? 0));
                                            break;
                                    }
                                }
                                break;
                            case "OtherCode"://对方合同编号
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.OtherCode == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.OtherCode.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.OtherCode.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "CreateDateTime"://创建日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime >= dt0 && a.CreateDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime <= dt1);

                                            }

                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "CreateUserId"://创建人
                                {
                                    var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(adv.conditionValueVal.value)).Select(a => a.Id).ToArray();
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于

                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.CreateUserId));
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.CreateUserId));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.CreateUserId));
                                            break;
                                    }
                                }
                                break;
                            case "Reserve1"://备用1
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Reserve1 == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Reserve1.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Reserve1.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "Reserve2"://备用2
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Reserve2 == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Reserve2.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Reserve2.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "MainDeptId"://签约主体
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.MainDept.Name==adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.MainDept.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.MainDept.Name.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                                case "ContDivisionZb"://是否总包
                                {
                                    int zbId0 = 0;
                                    int.TryParse(adv.conditionValueVal.value, out zbId0);
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.ContDivision == zbId0);
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.ContDivision == zbId0);
                                            break;
                                    }
                                }
                                break;
                            case "PerformanceDateTime"://实际履行日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateAndAdv = predicateAndAdv.And(a => a.PerformanceDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.PerformanceDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.PerformanceDateTime >= dt0 && a.PerformanceDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.PerformanceDateTime <= dt1);

                                            }

                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => false);
                                            break;
                                    }
                                }
                                break;

                            case "AdvanceAmount"://预收金额
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            decimal dcmval = 0m;
                                            Decimal.TryParse(adv.conditionValueVal.value, out dcmval);
                                            predicateAndAdv = predicateAndAdv.And(a => a.AdvanceAmount == dcmval);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            Decimal dt0 = 0;
                                            Decimal dt1 = 0;
                                            Decimal.TryParse(leftdt, out dt0);
                                            Decimal.TryParse(rightdt, out dt1);

                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.AdvanceAmount >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.AdvanceAmount >= dt0 && a.AdvanceAmount <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.AdvanceAmount <= dt1);

                                            }
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "EstimateAmount"://预估金额
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            decimal dcmval = 0m;
                                            Decimal.TryParse(adv.conditionValueVal.value, out dcmval);
                                            predicateAndAdv = predicateAndAdv.And(a => a.EstimateAmount == dcmval);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            Decimal dt0 = 0;
                                            Decimal dt1 = 0;
                                            Decimal.TryParse(leftdt, out dt0);
                                            Decimal.TryParse(rightdt, out dt1);

                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.EstimateAmount >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.EstimateAmount >= dt0 && a.EstimateAmount <= dt1);
                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.EstimateAmount <= dt1);

                                            }
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "ContState"://状态
                                {
                                    byte czt = 10;
                                    byte.TryParse(adv.conditionValueVal.value, out czt);
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.ContState == czt);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.ContState == czt);
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.ContState == czt);
                                            break;
                                    }
                                }
                                break;
                            case "ContSourceId"://合同来源
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            int classId = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId);
                                            predicateAndAdv = predicateAndAdv.And(a => a.ContSourceId == classId);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.ContSource.Name.Contains(adv.conditionValueVal.text));
                                            break;
                                        default:
                                            int classId0 = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId0);
                                            predicateAndAdv = predicateAndAdv.And(a => a.ContSourceId == classId0);
                                            break;
                                    }
                                }
                                break;
                            case "WfCurrNodeName"://当前节点
                                switch (adv.conditionOptionVal)
                                {

                                    case "like"://包含
                                        predicateAndAdv = predicateAndAdv.And(a => a.WfCurrNodeName!=null && a.WfCurrNodeName.Contains(adv.conditionValueVal.value) && (a.WfState == 1 || a.WfState == 3));
                                        break;
                                    default:
                                        predicateAndAdv = predicateAndAdv.And(a => a.WfCurrNodeName != null && a.WfCurrNodeName.Contains(adv.conditionValueVal.value) && (a.WfState == 1 || a.WfState == 3));
                                        break;
                                }
                                break;

                        }
                        

                       predicateAnd = predicateAnd.And(predicateAndAdv);
                    }
                    #endregion and

                    #region Or

                    else
                    {

                        switch (adv.conditionFieldVal)
                        {
                            case "Name"://名称
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Name == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Name.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "Code"://编号
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Code == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Code.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Code.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "ContTypeId"://类别
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            int classId = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ContTypeId == classId);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ContType.Name.Contains(adv.conditionValueVal.text));
                                            break;
                                        default:
                                            int classId0 = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId0);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ContTypeId == classId0);
                                            break;
                                    }
                                }
                                break;
                            case "AmountMoney"://合同金额
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            decimal dcmval = 0m;
                                            Decimal.TryParse(adv.conditionValueVal.value, out dcmval);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney == dcmval);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            Decimal dt0 = 0;
                                            Decimal dt1 = 0;
                                            Decimal.TryParse(leftdt, out dt0);
                                            Decimal.TryParse(rightdt, out dt1);

                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney >= dt0 && a.AmountMoney <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney <= dt1);

                                            }
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "DeptId"://经办机构
                                {
                                    int deptId = 0;
                                    int.TryParse(adv.conditionValueVal.value, out deptId);
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.DeptId == deptId);
                                            break;

                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.DeptId == deptId);
                                            break;

                                    }
                                }
                                break;
                            case "CompId"://合同对方
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Comp.Name == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Comp.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Comp.Name.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "ProjectId"://项目
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Project.Name == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Project.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Project.Name.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;

                            case "SngnDateTime"://签订日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.SngnDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.SngnDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.SngnDateTime >= dt0 && a.SngnDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.SngnDateTime <= dt1);

                                            }

                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "EffectiveDateTime"://生效日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.EffectiveDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.EffectiveDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.EffectiveDateTime >= dt0 && a.EffectiveDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.EffectiveDateTime <= dt1);

                                            }

                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => false);
                                            break;
                                    }
                                }
                                break;

                            case "PlanCompleteDateTime"://计划完成日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.PlanCompleteDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PlanCompleteDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PlanCompleteDateTime >= dt0 && a.PlanCompleteDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PlanCompleteDateTime <= dt1);

                                            }
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "ActualCompleteDateTime"://实际完成日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ActualCompleteDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualCompleteDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualCompleteDateTime >= dt0 && a.ActualCompleteDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualCompleteDateTime <= dt1);

                                            }
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "PrincipalUserId"://负责人
                                {
                                    var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(adv.conditionValueVal.value)).Select(a => a.Id).ToArray();
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于

                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.PrincipalUserId ?? 0));
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.PrincipalUserId ?? 0));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.PrincipalUserId ?? 0));
                                            break;
                                    }
                                }
                                break;
                            case "OtherCode"://对方合同编号
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.OtherCode == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.OtherCode.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.OtherCode.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "CreateDateTime"://创建日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime >= dt0 && a.CreateDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime <= dt1);

                                            }

                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "CreateUserId"://创建人
                                {
                                    var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(adv.conditionValueVal.value)).Select(a => a.Id).ToArray();
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于

                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.CreateUserId));
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.CreateUserId));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.CreateUserId));
                                            break;
                                    }
                                }
                                break;
                            case "Reserve1"://备用1
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Reserve1 == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Reserve1.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Reserve1.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "Reserve2"://备用2
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Reserve2 == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Reserve2.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Reserve2.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "MainDeptId"://签约主体
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.MainDept.Name == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.MainDept.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.MainDept.Name.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "ContDivisionZb"://是否总包
                                {
                                    int zbId0 = 0;
                                    int.TryParse(adv.conditionValueVal.value, out zbId0);
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ContDivision == zbId0);
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ContDivision == zbId0);
                                            break;
                                    }
                                }
                                break;
                            case "PerformanceDateTime"://实际履行日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.PerformanceDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PerformanceDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PerformanceDateTime >= dt0 && a.PerformanceDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PerformanceDateTime <= dt1);

                                            }

                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => false);
                                            break;
                                    }
                                }
                                break;

                            case "AdvanceAmount"://预收金额
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            decimal dcmval = 0m;
                                            Decimal.TryParse(adv.conditionValueVal.value, out dcmval);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.AdvanceAmount == dcmval);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            Decimal dt0 = 0;
                                            Decimal dt1 = 0;
                                            Decimal.TryParse(leftdt, out dt0);
                                            Decimal.TryParse(rightdt, out dt1);

                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.AdvanceAmount >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.AdvanceAmount >= dt0 && a.AdvanceAmount <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.AdvanceAmount <= dt1);

                                            }
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "EstimateAmount"://预估金额
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            decimal dcmval = 0m;
                                            Decimal.TryParse(adv.conditionValueVal.value, out dcmval);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.EstimateAmount == dcmval);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            Decimal dt0 = 0;
                                            Decimal dt1 = 0;
                                            Decimal.TryParse(leftdt, out dt0);
                                            Decimal.TryParse(rightdt, out dt1);

                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.EstimateAmount >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.EstimateAmount >= dt0 && a.EstimateAmount <= dt1);
                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.EstimateAmount <= dt1);

                                            }
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "ContState"://状态
                                {
                                    byte czt = 10;
                                    byte.TryParse(adv.conditionValueVal.value, out czt);
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ContState == czt);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ContState == czt);
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ContState == czt);
                                            break;
                                    }
                                }
                                break;
                            case "ContSourceId"://合同来源
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            int classId = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ContSourceId == classId);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ContSource.Name.Contains(adv.conditionValueVal.text));
                                            break;
                                        default:
                                            int classId0 = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId0);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ContSourceId == classId0);
                                            break;
                                    }
                                }
                                break;
                            case "WfCurrNodeName"://当前节点
                                switch (adv.conditionOptionVal)
                                {

                                    case "like"://包含
                                        predicateOrAdv = predicateOrAdv.Or(a => a.WfCurrNodeName!=null && a.WfCurrNodeName.Contains(adv.conditionValueVal.value) && (a.WfState == 1 || a.WfState == 3));
                                        break;
                                    default:
                                        predicateOrAdv = predicateOrAdv.Or(a => a.WfCurrNodeName != null && a.WfCurrNodeName.Contains(adv.conditionValueVal.value) && (a.WfState == 1 || a.WfState == 3));
                                        break;
                                }
                                break;

                        }
                        if (pageParam.jsonStr.Contains("and"))
                        {
                            predicateAnd = predicateAnd.Or(predicateOrAdv);
                        }
                        else
                        {
                            predicateAnd = predicateAnd.And(predicateOrAdv);
                        }
                    }

                    #endregion

                }


            }
            return predicateAnd;
        }

        /// <summary>
        /// 基本筛选
        /// </summary>
        /// <param name="pageParam">页查询条件</param>
        /// <param name="_IUserInforService">用户查询业务类</param>
        /// <returns>合同对方查询表达式</returns>
        public static Expression<Func<Model.Models.ContractInfo, bool>> GetAdvJBSXQueryContract(
              PageparamInfo pageParam
            ,IDataDictionaryService _IDataDictionaryService)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.ContractInfo>();
            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//基本筛选
                var advJbcxQuery = AdvQueryHelper.GetAdvJbcxQuery(pageParam.filterSos);



                var predicateAndAdv = PredicateBuilder.True<Model.Models.ContractInfo>();
                var predicateOrAdv = PredicateBuilder.False<Model.Models.ContractInfo>();
                foreach (var adv in advJbcxQuery)
                {
                    #region and
                    if (adv.prefix == "and")
                    {
                        switch (adv.field)
                        {
                            case "Name"://名称
                                {
                                    foreach (var item in adv.children)
                                    {


                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Name == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Name != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Name.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Name.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Code"://编号
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Code == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Code != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Code.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Code.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ContTypeName"://类别
                                {
                                    foreach (var item in adv.children)
                                    {
                                        int classId = _IDataDictionaryService.GetDataDicID(item.value);
                                        switch (item.type)
                                        {

                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ContTypeId == classId);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ContTypeId != classId);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.ContType.Name.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.ContTypeId == classId);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ContAmThod"://合同金额
                                {
                                    foreach (var item in adv.children)
                                    {
                                        decimal dcmval = 0m;
                                        Decimal.TryParse(item.value, out dcmval);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                               
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney == dcmval);
                                                break;
                                            case "ne"://不等于
                                                Decimal.TryParse(item.value, out dcmval);
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney != dcmval);
                                                break;
                                            case "gt"://大于
                                                Decimal.TryParse(item.value, out dcmval);
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney > dcmval);
                                                break;
                                            case "ge"://大于等于
                                                Decimal.TryParse(item.value, out dcmval);
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney >= dcmval);
                                                break;
                                            case "lt"://小于
                                                Decimal.TryParse(item.value, out dcmval);
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney <= dcmval);
                                                break;
                                            case "le"://小于等于
                                                Decimal.TryParse(item.value, out dcmval);
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney <= dcmval);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CompName"://合同对方
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Comp.Name == item.value);
                                                break;
                                            case "ne"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Comp.Name!= item.value);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Comp.Name.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "ProjName"://项目
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Project.Name == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Project.Name != item.value);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Project.Name.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;

                            case "SngnDateTime"://签订日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.SngnDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.SngnDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.SngnDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.SngnDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.SngnDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.SngnDateTime <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "EffectiveDateTime"://生效日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.EffectiveDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.EffectiveDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.EffectiveDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.EffectiveDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.EffectiveDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.EffectiveDateTime <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;

                            case "PlanCompleteDateTime"://计划完成日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.PlanCompleteDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.PlanCompleteDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.PlanCompleteDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.PlanCompleteDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.PlanCompleteDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.PlanCompleteDateTime <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ActualCompleteDateTime"://实际完成日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualCompleteDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualCompleteDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualCompleteDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualCompleteDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualCompleteDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualCompleteDateTime <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "OtherCode"://对方合同编号
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.OtherCode == item.value);
                                                break;
                                            case "ne"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.OtherCode != item.value);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.OtherCode.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "Reserve1"://备用1
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Reserve1 == item.value);
                                                break;
                                            case "ne"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Reserve1 !=item.value);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Reserve1.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "Reserve2"://备用2
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Reserve2 == item.value);
                                                break;
                                            case "ne"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Reserve2.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Reserve2.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "MainDeptId"://签约主体
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.MainDept.Name == item.value);
                                                break;
                                            case "ne"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.MainDept.Name != item.value);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.MainDept.Name.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "PerformanceDateTime"://实际履行日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.PerformanceDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.PerformanceDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.PerformanceDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.PerformanceDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.PerformanceDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.PerformanceDateTime <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ContState"://状态
                                {
                                    foreach (var item in adv.children)
                                    {
                                        byte czt = 10;
                                        byte.TryParse(item.value, out czt);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ContState == czt);
                                                break;
                                            case "ne"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.ContState != czt);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.ContState == czt);
                                                break;
                                        }
                                    }
                                }
                                break;
                        }


                        predicateAnd = predicateAnd.And(predicateAndAdv);
                    }
                    #endregion and

                    #region Or
                    else
                    {
                        switch (adv.field)
                        {
                            case "Name"://名称
                                {
                                    foreach (var item in adv.children)
                                    {


                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Name == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Name != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Name.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Name.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Code"://编号
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Code == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Code != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Code.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Code.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ContTypeName"://类别
                                {
                                    foreach (var item in adv.children)
                                    {
                                        int classId = _IDataDictionaryService.GetDataDicID(item.value);
                                        switch (item.type)
                                        {

                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ContTypeId == classId);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ContTypeId != classId);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ContType.Name.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ContTypeId == classId);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ContAmThod"://合同金额
                                {
                                    foreach (var item in adv.children)
                                    {
                                        decimal dcmval = 0m;
                                        Decimal.TryParse(item.value, out dcmval);
                                        switch (item.type)
                                        {
                                            case "eq"://等于

                                                predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney == dcmval);
                                                break;
                                            case "ne"://不等于
                                                Decimal.TryParse(item.value, out dcmval);
                                                predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney != dcmval);
                                                break;
                                            case "gt"://大于
                                                Decimal.TryParse(item.value, out dcmval);
                                                predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney > dcmval);
                                                break;
                                            case "ge"://大于等于
                                                Decimal.TryParse(item.value, out dcmval);
                                                predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney >= dcmval);
                                                break;
                                            case "lt"://小于
                                                Decimal.TryParse(item.value, out dcmval);
                                                predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney <= dcmval);
                                                break;
                                            case "le"://小于等于
                                                Decimal.TryParse(item.value, out dcmval);
                                                predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney <= dcmval);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CompName"://合同对方
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Comp.Name == item.value);
                                                break;
                                            case "ne"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Comp.Name != item.value);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Comp.Name.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "ProjName"://项目
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Project.Name == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Project.Name != item.value);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Project.Name.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;

                            case "SngnDateTime"://签订日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.SngnDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.SngnDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.SngnDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.SngnDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.SngnDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.SngnDateTime <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "EffectiveDateTime"://生效日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.EffectiveDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.EffectiveDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.EffectiveDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.EffectiveDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.EffectiveDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.EffectiveDateTime <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;

                            case "PlanCompleteDateTime"://计划完成日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PlanCompleteDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PlanCompleteDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PlanCompleteDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PlanCompleteDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PlanCompleteDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PlanCompleteDateTime <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ActualCompleteDateTime"://实际完成日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualCompleteDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualCompleteDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualCompleteDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualCompleteDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualCompleteDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualCompleteDateTime <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "OtherCode"://对方合同编号
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.OtherCode == item.value);
                                                break;
                                            case "ne"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.OtherCode != item.value);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.OtherCode.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "Reserve1"://备用1
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Reserve1 == item.value);
                                                break;
                                            case "ne"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Reserve1 != item.value);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Reserve1.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "Reserve2"://备用2
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Reserve2 == item.value);
                                                break;
                                            case "ne"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Reserve2.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Reserve2.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "MainDeptId"://签约主体
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.MainDept.Name == item.value);
                                                break;
                                            case "ne"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.MainDept.Name != item.value);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.MainDept.Name.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "PerformanceDateTime"://实际履行日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PerformanceDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PerformanceDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PerformanceDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PerformanceDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PerformanceDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PerformanceDateTime <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ContState"://状态
                                {
                                    foreach (var item in adv.children)
                                    {
                                        byte czt = 10;
                                        byte.TryParse(item.value, out czt);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ContState == czt);
                                                break;
                                            case "ne"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ContState != czt);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ContState == czt);
                                                break;
                                        }
                                    }
                                }
                                break;
                        }
                        if (pageParam.filterSos.Contains("and"))
                        {
                            predicateAnd = predicateAnd.Or(predicateOrAdv);
                        }
                        else
                        {
                            predicateAnd = predicateAnd.And(predicateOrAdv);
                        }
                    }

                    #endregion

                }


                    }
            return predicateAnd;
        }
        #endregion

        #region 计划资金
        public static Expression<Func<Model.Models.ContPlanFinance, bool>> GetAdvJBSXQueryContPlanFinance(
           PageparamInfo pageParam
         , IUserInforService _IUserInforService, IContPlanFinanceService _IContPlanFinanceService)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.ContPlanFinance>();
            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//基本筛选
                var advJbcxQuery = AdvQueryHelper.GetAdvJbcxQuery(pageParam.filterSos);
                var predicateAndAdv = PredicateBuilder.True<Model.Models.ContPlanFinance>();
                var predicateOrAdv = PredicateBuilder.False<Model.Models.ContPlanFinance>();
                foreach (var adv in advJbcxQuery)
                {
                    #region and
                    if (adv.prefix == "and")
                    {
                        switch (adv.field)
                        {
                            case "Name"://名称
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Name == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Name.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Name != item.value);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Name.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ContName"://合同名称
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Name == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Name.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Name != item.value);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Name.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ContCode"://合同编号
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Code == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Code.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Code != item.value);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "AmountMoneyThod"://计划收款金额
                                {
                                    foreach (var item in adv.children)
                                    {
                                     
                                        var je = _IContPlanFinanceService.ParseThousandthString(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney == je);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney != je);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney > je);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney >= je);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney <= je);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney <= je);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CompAmountThod"://已完成金额
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var je = _IContPlanFinanceService.ParseThousandthString(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ConfirmedAmount == je);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ConfirmedAmount != je);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ConfirmedAmount > je);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ConfirmedAmount >= je);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ConfirmedAmount <= je);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ConfirmedAmount <= je);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "BalanceThod": //余额
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var je = _IContPlanFinanceService.ParseThousandthString(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => ((a.AmountMoney ?? 0) - (a.ConfirmedAmount ?? 0)) == je);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => ((a.AmountMoney ?? 0) - (a.ConfirmedAmount ?? 0)) != je);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => ((a.AmountMoney ?? 0) - (a.ConfirmedAmount ?? 0)) > je);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => ((a.AmountMoney ?? 0) - (a.ConfirmedAmount ?? 0)) >= je);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => ((a.AmountMoney ?? 0) - (a.ConfirmedAmount ?? 0)) <= je);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => ((a.AmountMoney ?? 0) - (a.ConfirmedAmount ?? 0)) <= je);
                                                break;
                                        }
                                    }
                                }
                                break;
                        }
                        predicateAnd = predicateAnd.And(predicateAndAdv);
                    }
                    #endregion and

                    #region Or
                    if (adv.prefix == "Or")
                    {
                        switch (adv.field)
                        {
                            case "Name"://名称
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Name == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Name.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Name != item.value);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Name.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ContName"://合同名称
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Name == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Name.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Name != item.value);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Name.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ContCode"://合同编号
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Code == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Code.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Code != item.value);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "AmountMoneyThod"://计划收款金额
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var je = _IContPlanFinanceService.ParseThousandthString(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney == je);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney != je);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney > je);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney >= je);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney <= je);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney <= je);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CompAmountThod"://已完成金额
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var je = _IContPlanFinanceService.ParseThousandthString(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmedAmount == je);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmedAmount != je);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmedAmount > je);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmedAmount >= je);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmedAmount <= je);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmedAmount <= je);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "BalanceThod": //余额
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var je = _IContPlanFinanceService.ParseThousandthString(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => ((a.AmountMoney ?? 0) - (a.ConfirmedAmount ?? 0)) == je);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => ((a.AmountMoney ?? 0) - (a.ConfirmedAmount ?? 0)) != je);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => ((a.AmountMoney ?? 0) - (a.ConfirmedAmount ?? 0)) > je);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => ((a.AmountMoney ?? 0) - (a.ConfirmedAmount ?? 0)) >= je);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => ((a.AmountMoney ?? 0) - (a.ConfirmedAmount ?? 0)) <= je);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => ((a.AmountMoney ?? 0) - (a.ConfirmedAmount ?? 0)) <= je);
                                                break;
                                        }
                                    }
                                }
                                break;
                        }
                        predicateAnd = predicateAnd.Or(predicateOrAdv);
                    }
                    #endregion
                }
            }
            return predicateAnd;
        }
        #endregion
      
        #region 实际资金
        /// <summary>
        /// 实际资金高级查询
        /// </summary>
        /// <param name="pageParam">页查询条件</param>
        /// <param name="_IUserInforService">用户查询业务类</param>
        /// <returns>实际资金高级查询查询表达式</returns>
        public static Expression<Func<Model.Models.ContActualFinance, bool>> GetAdvQueryActFince(
              PageparamInfo pageParam
            , IUserInforService _IUserInforService)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.ContActualFinance>();
            if (!string.IsNullOrEmpty(pageParam.jsonStr))
            {//高级查询
                var advquerys = AdvQueryHelper.GetAdvQuery(pageParam.jsonStr);
                var predicateAndAdv = PredicateBuilder.True<Model.Models.ContActualFinance>();
                var predicateOrAdv = PredicateBuilder.False<Model.Models.ContActualFinance>();
                foreach (var adv in advquerys)
                {
                    #region and
                    if (adv.logicalOperator == "and")
                    {
                        switch (adv.conditionFieldVal)
                        {
                            case "AmountMoney"://实际金额
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            decimal dcmval = 0m;
                                            Decimal.TryParse(adv.conditionValueVal.value, out dcmval);
                                            predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney == dcmval);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            Decimal dt0 = 0;
                                            Decimal dt1 = 0;
                                            Decimal.TryParse(leftdt, out dt0);
                                            Decimal.TryParse(rightdt, out dt1);

                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney >= dt0 && a.AmountMoney <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney <= dt1);

                                            }
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => false);
                                            break;
                                    }
                                }
                                break;

                            case "Name"://合同名称
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cont.Name == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cont.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cont.Name.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "Code"://合同编号
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cont.Code == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cont.Code.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cont.Code.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "ContTypeId"://合同类别
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            int classId = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId);
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cont.ContTypeId == classId);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cont.ContType.Name.Contains(adv.conditionValueVal.text));
                                            break;
                                        default:
                                            int classId0 = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId0);
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cont.ContTypeId == classId0);
                                            break;
                                    }
                                }
                                break;
                            
                            case "DeptId"://经办机构
                                {
                                    int deptId = 0;
                                    int.TryParse(adv.conditionValueVal.value, out deptId);
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cont.DeptId == deptId);
                                            break;

                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cont.DeptId == deptId);
                                            break;

                                    }
                                }
                                break;
                            case "CompName"://合同对方
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cont.Comp.Name == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cont.Comp.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cont.Comp.Name.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "ActualSettlementDate"://结算日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateAndAdv = predicateAndAdv.And(a => a.ActualSettlementDate == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualSettlementDate >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualSettlementDate >= dt0 && a.ActualSettlementDate <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualSettlementDate <= dt1);

                                            }

                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "VoucherNo"://发票编号
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.VoucherNo == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.VoucherNo.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.VoucherNo.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "Astate"://状态
                                {
                                    byte czt = 10;
                                    byte.TryParse(adv.conditionValueVal.value, out czt);
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Astate == czt);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Astate == czt);
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Astate == czt);
                                            break;
                                    }
                                }
                                break;
                            case "SettlementMethod"://结算方式
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            int classId = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId);
                                            predicateAndAdv = predicateAndAdv.And(a => a.SettlementMethod == classId);
                                            break;
                                        
                                        default:
                                            int classId0 = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId0);
                                            predicateAndAdv = predicateAndAdv.And(a => a.SettlementMethod == classId0);
                                            break;
                                    }
                                }
                                break;
                            case "ConfirmUserId"://确认人
                                {
                                    var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(adv.conditionValueVal.value)).Select(a => a.Id).ToArray();
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于

                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.ConfirmUserId ?? 0));
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.ConfirmUserId ?? 0));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.ConfirmUserId ?? 0));
                                            break;
                                    }
                                }
                                break;
                            case "ConfirmDateTime"://确认时间
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateAndAdv = predicateAndAdv.And(a => a.ConfirmDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.ConfirmDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.ConfirmDateTime >= dt0 && a.ConfirmDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.ConfirmDateTime <= dt1);

                                            }

                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "CreateDateTime"://创建日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime >= dt0 && a.CreateDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime <= dt1);

                                            }

                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "CreateUserId"://创建人
                                {
                                    var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(adv.conditionValueVal.value)).Select(a => a.Id).ToArray();
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于

                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.CreateUserId));
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.CreateUserId));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.CreateUserId));
                                            break;
                                    }
                                }
                                break;
                            
                            case "Remark"://备注
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Remark == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Remark.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Remark.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            
                            case "Reserve1"://备用1
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Reserve1 == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Reserve1.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Reserve1.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "Reserve2"://备用2
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Reserve2 == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Reserve2.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Reserve2.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "WfCurrNodeName"://当前节点
                                switch (adv.conditionOptionVal)
                                {

                                    case "like"://包含
                                        predicateAndAdv = predicateAndAdv.And(a => a.WfCurrNodeName!=null && a.WfCurrNodeName.Contains(adv.conditionValueVal.value) && (a.WfState == 1 || a.WfState == 3));
                                        break;
                                    default:
                                        predicateAndAdv = predicateAndAdv.And(a => a.WfCurrNodeName != null && a.WfCurrNodeName.Contains(adv.conditionValueVal.value) && (a.WfState == 1 || a.WfState == 3));
                                        break;
                                }
                                break;
                        }


                        predicateAnd = predicateAnd.And(predicateAndAdv);
                    }
                    #endregion and

                    #region Or

                    else if (adv.logicalOperator == "or")
                    {
                        switch (adv.conditionFieldVal)
                        {
                            case "AmountMoney"://实际金额
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            decimal dcmval = 0m;
                                            Decimal.TryParse(adv.conditionValueVal.value, out dcmval);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney == dcmval);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            Decimal dt0 = 0;
                                            Decimal dt1 = 0;
                                            Decimal.TryParse(leftdt, out dt0);
                                            Decimal.TryParse(rightdt, out dt1);

                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney >= dt0 && a.AmountMoney <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney <= dt1);

                                            }
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => false);
                                            break;
                                    }
                                }
                                break;

                            case "Name"://合同名称
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Name == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Name.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "Code"://合同编号
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Code == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Code.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Code.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "ContTypeId"://合同类别
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            int classId = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cont.ContTypeId == classId);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cont.ContType.Name.Contains(adv.conditionValueVal.text));
                                            break;
                                        default:
                                            int classId0 = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId0);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cont.ContTypeId == classId0);
                                            break;
                                    }
                                }
                                break;

                            case "DeptId"://经办机构
                                {
                                    int deptId = 0;
                                    int.TryParse(adv.conditionValueVal.value, out deptId);
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cont.DeptId == deptId);
                                            break;

                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cont.DeptId == deptId);
                                            break;

                                    }
                                }
                                break;
                            case "CompName"://合同对方
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Comp.Name == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Comp.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Comp.Name.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "ActualSettlementDate"://结算日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ActualSettlementDate == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualSettlementDate >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualSettlementDate >= dt0 && a.ActualSettlementDate <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualSettlementDate <= dt1);

                                            }

                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "VoucherNo"://发票编号
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.VoucherNo == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.VoucherNo.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.VoucherNo.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "Astate"://状态
                                {
                                    byte czt = 10;
                                    byte.TryParse(adv.conditionValueVal.value, out czt);
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Astate == czt);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Astate == czt);
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Astate == czt);
                                            break;
                                    }
                                }
                                break;
                            case "SettlementMethod"://结算方式
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            int classId = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.SettlementMethod == classId);
                                            break;

                                        default:
                                            int classId0 = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId0);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.SettlementMethod == classId0);
                                            break;
                                    }
                                }
                                break;
                            case "ConfirmUserId"://确认人
                                {
                                    var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(adv.conditionValueVal.value)).Select(a => a.Id).ToArray();
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于

                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.ConfirmUserId ?? 0));
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.ConfirmUserId ?? 0));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.ConfirmUserId ?? 0));
                                            break;
                                    }
                                }
                                break;
                            case "ConfirmDateTime"://确认时间
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmDateTime >= dt0 && a.ConfirmDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a=> a.ConfirmDateTime <= dt1);

                                            }

                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "CreateDateTime"://创建日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime >= dt0 && a.CreateDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime <= dt1);

                                            }

                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "CreateUserId"://创建人
                                {
                                    var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(adv.conditionValueVal.value)).Select(a => a.Id).ToArray();
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于

                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.CreateUserId));
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.CreateUserId));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.CreateUserId));
                                            break;
                                    }
                                }
                                break;

                            case "Remark"://备注
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Remark == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Remark.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Remark.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;

                            case "Reserve1"://备用1
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Reserve1 == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Reserve1.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Reserve1.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "Reserve2"://备用2
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Reserve2 == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Reserve2.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Reserve2.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "WfCurrNodeName"://当前节点
                                switch (adv.conditionOptionVal)
                                {

                                    case "like"://包含
                                        predicateOrAdv = predicateOrAdv.Or(a => a.WfCurrNodeName!=null && a.WfCurrNodeName.Contains(adv.conditionValueVal.value) && (a.WfState == 1 || a.WfState == 3));
                                        break;
                                    default:
                                        predicateOrAdv = predicateOrAdv.Or(a => a.WfCurrNodeName != null && a.WfCurrNodeName.Contains(adv.conditionValueVal.value) && (a.WfState == 1 || a.WfState == 3));
                                        break;
                                }
                                break;
                        }
                        if (pageParam.jsonStr.Contains("and"))
                        {
                            predicateAnd = predicateAnd.Or(predicateOrAdv);
                        }
                        else
                        {
                            predicateAnd = predicateAnd.And(predicateOrAdv);
                        }
                    }

                    #endregion

                }


            }
            return predicateAnd;
        }
        /// <summary>
        ///实际资金基本查询
        /// </summary>
        /// <param name="pageParam"></param>
        /// <param name="_IUserInforService"></param>
        /// <param name="_IContPlanFinanceService"></param>
        /// <returns></returns>
        public static Expression<Func<Model.Models.ContActualFinance, bool>> GetAdvJBSXQueryContActualFinance(
     PageparamInfo pageParam
   , IUserInforService _IUserInforService,IProjectManagerService _IProjectManagerService, IContPlanFinanceService _IContPlanFinanceService)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.ContActualFinance>();
            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//基本筛选
                var advJbcxQuery = AdvQueryHelper.GetAdvJbcxQuery(pageParam.filterSos);
                var predicateAndAdv = PredicateBuilder.True<Model.Models.ContActualFinance>();
                var predicateOrAdv = PredicateBuilder.False<Model.Models.ContActualFinance>();
                foreach (var adv in advJbcxQuery)
                {
                    #region and
                    if (adv.prefix == "and")
                    {
                        switch (adv.field)
                        {
                            case "AmountMoneyThod"://实际金额
                                {
                                    foreach (var item in adv.children)
                                    {
                                        ///千分位转数字
                                        var je = _IContPlanFinanceService.ParseThousandthString(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney == je);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney != je);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney > je);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney >= je);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney <= je);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney <= je);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ContName"://合同名称
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Name == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Name.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Name != item.value);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Name.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ContCode"://合同编号
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Code == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Code.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Code != item.value);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ContCategoryName"://合同类别
                                foreach (var item in adv.children)
                                {
                                    string name = (item.value).ToString().Trim().Replace(" ", "");
                                    int? s = _IProjectManagerService.XMLB((string)name);
                                    switch (item.type)
                                    {
                                        case "eq"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => (a.Cont == null ? -1 : a.Cont.ContTypeId) == s);
                                            break;
                                        case "ne"://不等于
                                            predicateAndAdv = predicateAndAdv.And(a => (a.Cont == null ? -1 : a.Cont.ContTypeId) != s);
                                            break;
                                    }
                                }
                                break;
                            case "DeptName"://经办机构
                                foreach (var item in adv.children)
                                {
                                    string name = (item.value).ToString().Trim().Replace(" ", "");
                                    int? s = _IContPlanFinanceService.DepartmentID((string)name);
                                    switch (item.type)
                                    {
                                        case "eq"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => (a.Cont == null ? -1 : a.Cont.DeptId) == s);
                                            break;
                                        case "ne"://不等于
                                            predicateAndAdv = predicateAndAdv.And(a => (a.Cont == null ? -1 : a.Cont.DeptId) != s);
                                            break;
                                    }
                                }
                                break;
                            case "CompName"://合同对方
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => ((a.Cont != null && a.Cont.Comp != null) ? a.Cont.Comp.Name : "") == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => ((a.Cont != null && a.Cont.Comp != null) ? a.Cont.Comp.Name : "").Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => ((a.Cont != null && a.Cont.Comp != null) ? a.Cont.Comp.Name : "") != item.value);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Name.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ActualSettlementDate"://结算日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualSettlementDate == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualSettlementDate != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualSettlementDate > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualSettlementDate >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualSettlementDate < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualSettlementDate <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                              
                                break;
                            case "VoucherNo"://发票编号
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.VoucherNo == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.VoucherNo.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.VoucherNo != item.value);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "AstateDic"://状态
                                {
                                    foreach (var item in adv.children)
                                    {
                                        string name = (item.value).ToString().Trim().Replace(" ", "");
                                        var state = -1;
                                        if (name == "未提交")
                                        {
                                            state = 0;
                                        }
                                        else if (name == "已提交")
                                        {
                                            state = 1;
                                        }
                                        else if (name == "已确认")
                                        {
                                            state = 2;
                                        }
                                        else if (name == "被打回")
                                        {
                                            state = 3;
                                        }
                                        else if (name == "审批通过")
                                        {
                                            state = 6;
                                        }
                                       
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Astate == state);
                                                break;
                                            //case "contain"://包含
                                            //    predicateAndAdv = predicateAndAdv.And(a => a.VoucherNo.Contains(state));
                                            //    break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Astate != state);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "SettlementMethodDic"://结算方式
                                foreach (var item in adv.children)
                                {
                                    string name = (item.value).ToString().Trim().Replace(" ", "");
                                    int? s = _IProjectManagerService.XMLB((string)name);
                                    switch (item.type)
                                    {
                                        case "eq"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.SettlementMethod == s);
                                            break;
                                        case "ne"://不等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.SettlementMethod != s);
                                            break;
                                    }
                                }
                                break;
                            case "ConfirmUserName"://确认人     
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name == item.value).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ConfirmUserId == userIds);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ConfirmUserId != userIds);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ConfirmDateTime"://确认时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ConfirmDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ConfirmDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ConfirmDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ConfirmDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ConfirmDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ConfirmDateTime <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CreateDateTime"://创建日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CreateUserName"://创建人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name == item.value).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateUserId == userIds);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateUserId != userIds);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Remark"://备注
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Remark == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Remark.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Remark != item.value);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Reserve1"://备用1
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Reserve1 == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Reserve1.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Reserve1 != item.value);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Reserve2"://备用2
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Remark == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Remark.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Remark != item.value);
                                                break;
                                        }
                                    }
                                }
                                break;
                        }
                        predicateAnd = predicateAnd.And(predicateAndAdv);
                    }
                    #endregion and

                    #region Or
                    if (adv.prefix == "Or")
                    {
                        switch (adv.field)
                        {
                            case "AmountMoneyThod"://实际金额
                                {
                                    foreach (var item in adv.children)
                                    {
                                        ///千分位转数字
                                        var je = _IContPlanFinanceService.ParseThousandthString(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney == je);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney != je);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney > je);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney >= je);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney <= je);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney <= je);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ContName"://合同名称
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Name == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Name.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Name != item.value);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Name.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ContCode"://合同编号
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Code == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Code.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Code != item.value);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ContCategoryName"://合同类别
                                foreach (var item in adv.children)
                                {
                                    string name = (item.value).ToString().Trim().Replace(" ", "");
                                    int? s = _IProjectManagerService.XMLB((string)name);
                                    switch (item.type)
                                    {
                                        case "eq"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => (a.Cont == null ? -1 : a.Cont.ContTypeId) == s);
                                            break;
                                        case "ne"://不等于
                                            predicateOrAdv = predicateOrAdv.Or(a => (a.Cont == null ? -1 : a.Cont.ContTypeId) != s);
                                            break;
                                    }
                                }
                                break;
                            case "DeptName"://经办机构
                                foreach (var item in adv.children)
                                {
                                    string name = (item.value).ToString().Trim().Replace(" ", "");
                                    int? s = _IContPlanFinanceService.DepartmentID((string)name);
                                    switch (item.type)
                                    {
                                        case "eq"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => (a.Cont == null ? -1 : a.Cont.DeptId) == s);
                                            break;
                                        case "ne"://不等于
                                            predicateOrAdv = predicateOrAdv.Or(a => (a.Cont == null ? -1 : a.Cont.DeptId) != s);
                                            break;
                                    }
                                }
                                break;
                            case "CompName"://合同对方
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => ((a.Cont != null && a.Cont.Comp != null) ? a.Cont.Comp.Name : "") == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => ((a.Cont != null && a.Cont.Comp != null) ? a.Cont.Comp.Name : "").Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => ((a.Cont != null && a.Cont.Comp != null) ? a.Cont.Comp.Name : "") != item.value);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Name.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ActualSettlementDate"://结算日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualSettlementDate == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualSettlementDate != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualSettlementDate > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualSettlementDate >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualSettlementDate < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualSettlementDate <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }

                                break;
                            case "VoucherNo"://发票编号
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.VoucherNo == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.VoucherNo.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.VoucherNo != item.value);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "AstateDic"://状态
                                {
                                    foreach (var item in adv.children)
                                    {
                                        string name = (item.value).ToString().Trim().Replace(" ", "");
                                        var state = -1;
                                        if (name == "未提交")
                                        {
                                            state = 0;
                                        }
                                        else if (name == "已提交")
                                        {
                                            state = 1;
                                        }
                                        else if (name == "已确认")
                                        {
                                            state = 2;
                                        }
                                        else if (name == "被打回")
                                        {
                                            state = 3;
                                        }
                                        else if (name == "审批通过")
                                        {
                                            state = 6;
                                        }

                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Astate == state);
                                                break;
                                            //case "contain"://包含
                                            //    predicateAndAdv = predicateAndAdv.And(a => a.VoucherNo.Contains(state));
                                            //    break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Astate != state);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "SettlementMethodDic"://结算方式
                                foreach (var item in adv.children)
                                {
                                    string name = (item.value).ToString().Trim().Replace(" ", "");
                                    int? s = _IProjectManagerService.XMLB((string)name);
                                    switch (item.type)
                                    {
                                        case "eq"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.SettlementMethod == s);
                                            break;
                                        case "ne"://不等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.SettlementMethod != s);
                                            break;
                                    }
                                }
                                break;
                            case "ConfirmUserName"://确认人     
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name == item.value).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmUserId == userIds);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmUserId != userIds);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ConfirmDateTime"://确认时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmDateTime <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CreateDateTime"://创建日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmDateTime <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CreateUserName"://创建人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name == item.value).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateUserId == userIds);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateUserId != userIds);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Remark"://备注
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Remark == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Remark.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Remark != item.value);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Reserve1"://备用1
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Reserve1 == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Reserve1.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Reserve1 != item.value);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Reserve2"://备用2
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Remark == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Remark.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Remark != item.value);
                                                break;
                                        }
                                    }
                                }
                                break;
                        }
                        predicateAnd = predicateAnd.Or(predicateOrAdv);
                    }
                    #endregion
                }
            }
            return predicateAnd;
        }

        #endregion

        #region 发票
        /// <summary>
        /// 发票高级查询
        /// </summary>
        /// <param name="pageParam">页查询条件</param>
        /// <param name="_IUserInforService">用户查询业务类</param>
        /// <returns>发票高级查询查询表达式</returns>
        public static Expression<Func<Model.Models.ContInvoice, bool>> GetAdvQueryInvoice(
              PageparamInfo pageParam
            , IUserInforService _IUserInforService)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.ContInvoice>();
            if (!string.IsNullOrEmpty(pageParam.jsonStr))
            {//高级查询
                var advquerys = AdvQueryHelper.GetAdvQuery(pageParam.jsonStr);
                var predicateAndAdv = PredicateBuilder.True<Model.Models.ContInvoice>();
                var predicateOrAdv = PredicateBuilder.False<Model.Models.ContInvoice>();
                foreach (var adv in advquerys)
                {
                    #region and
                    if (adv.logicalOperator == "and")
                    {
                        switch (adv.conditionFieldVal)
                        {
                            case "InType"://发票类型
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            int classId = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId);
                                            predicateAndAdv = predicateAndAdv.And(a => a.InType == classId);
                                            break;
                                        
                                        default:
                                            int classId0 = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId0);
                                            predicateAndAdv = predicateAndAdv.And(a => a.InType == classId0);
                                            break;
                                    }
                                }
                                break;
                            case "Name"://合同名称
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cont.Name == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cont.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cont.Name.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "Code"://合同编号
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cont.Code == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cont.Code.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cont.Code.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "ContTypeId"://合同类别
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            int classId = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId);
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cont.ContTypeId == classId);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cont.ContType.Name.Contains(adv.conditionValueVal.text));
                                            break;
                                        default:
                                            int classId0 = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId0);
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cont.ContTypeId == classId0);
                                            break;
                                    }
                                }
                                break;

                            case "DeptId"://经办机构
                                {
                                    int deptId = 0;
                                    int.TryParse(adv.conditionValueVal.value, out deptId);
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cont.DeptId == deptId);
                                            break;

                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cont.DeptId == deptId);
                                            break;

                                    }
                                }
                                break;
                            case "InCode"://发票编号
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.InCode == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.InCode.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.InCode.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "InTitle"://发票抬头
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.InTitle == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.InTitle.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.InTitle.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "CompName"://合同对方
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cont.Comp.Name == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cont.Comp.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Cont.Comp.Name.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "AmountMoney"://金额
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            decimal dcmval = 0m;
                                            Decimal.TryParse(adv.conditionValueVal.value, out dcmval);
                                            predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney == dcmval);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            Decimal dt0 = 0;
                                            Decimal dt1 = 0;
                                            Decimal.TryParse(leftdt, out dt0);
                                            Decimal.TryParse(rightdt, out dt1);

                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney >= dt0 && a.AmountMoney <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney <= dt1);

                                            }
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "ConfirmUserId"://确认人
                                {
                                    var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(adv.conditionValueVal.value)).Select(a => a.Id).ToArray();
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于

                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.ConfirmUserId ?? 0));
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.ConfirmUserId ?? 0));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.ConfirmUserId ?? 0));
                                            break;
                                    }
                                }
                                break;
                            case "ConfirmDateTime"://确认时间
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateAndAdv = predicateAndAdv.And(a => a.ConfirmDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.ConfirmDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.ConfirmDateTime >= dt0 && a.ConfirmDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.ConfirmDateTime <= dt1);

                                            }

                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "CreateDateTime"://创建日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime >= dt0 && a.CreateDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime <= dt1);

                                            }

                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "CreateUserId"://创建人
                                {
                                    var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(adv.conditionValueVal.value)).Select(a => a.Id).ToArray();
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于

                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.CreateUserId));
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.CreateUserId));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.CreateUserId));
                                            break;
                                    }
                                }
                                break;
                            case "InState"://状态
                                {
                                    byte czt = 10;
                                    byte.TryParse(adv.conditionValueVal.value, out czt);
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.InState == czt);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.InState == czt);
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.InState == czt);
                                            break;
                                    }
                                }
                                break;
                            case "Remark"://备注
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Remark == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Remark.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Remark.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;

                            case "Reserve1"://备用1
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Reserve1 == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Reserve1.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Reserve1.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "Reserve2"://备用2
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Reserve2 == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Reserve2.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Reserve2.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "WfCurrNodeName"://当前节点
                                switch (adv.conditionOptionVal)
                                {

                                    case "like"://包含
                                        predicateAndAdv = predicateAndAdv.And(a => a.WfCurrNodeName!=null && a.WfCurrNodeName.Contains(adv.conditionValueVal.value) && (a.WfState == 1 || a.WfState == 3));
                                        break;
                                    default:
                                        predicateAndAdv = predicateAndAdv.And(a => a.WfCurrNodeName != null && a.WfCurrNodeName.Contains(adv.conditionValueVal.value) && (a.WfState == 1 || a.WfState == 3));
                                        break;
                                }
                                break;
                        }


                        predicateAnd = predicateAnd.And(predicateAndAdv);
                    }
                    #endregion and

                    #region Or

                    else if (adv.logicalOperator == "or")
                    {
                        switch (adv.conditionFieldVal)
                        {
                            case "InType"://发票类型
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            int classId = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.InType == classId);
                                            break;

                                        default:
                                            int classId0 = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId0);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.InType == classId0);
                                            break;
                                    }
                                }
                                break;
                            case "Name"://合同名称
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Name == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Name.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "Code"://合同编号
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Code == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Code.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Code.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "ContTypeId"://合同类别
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            int classId = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cont.ContTypeId == classId);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cont.ContType.Name.Contains(adv.conditionValueVal.text));
                                            break;
                                        default:
                                            int classId0 = 0;
                                            int.TryParse(adv.conditionValueVal.value, out classId0);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cont.ContTypeId == classId0);
                                            break;
                                    }
                                }
                                break;

                            case "DeptId"://经办机构
                                {
                                    int deptId = 0;
                                    int.TryParse(adv.conditionValueVal.value, out deptId);
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cont.DeptId == deptId);
                                            break;

                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cont.DeptId == deptId);
                                            break;

                                    }
                                }
                                break;
                            case "InCode"://发票编号
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.InCode == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.InCode.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.InCode.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "InTitle"://发票抬头
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.InTitle == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.InTitle.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.InTitle.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "CompName"://合同对方
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Comp.Name == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Comp.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Comp.Name.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "AmountMoney"://金额
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            decimal dcmval = 0m;
                                            Decimal.TryParse(adv.conditionValueVal.value, out dcmval);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney == dcmval);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            Decimal dt0 = 0;
                                            Decimal dt1 = 0;
                                            Decimal.TryParse(leftdt, out dt0);
                                            Decimal.TryParse(rightdt, out dt1);

                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney >= dt0 && a.AmountMoney <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney <= dt1);

                                            }
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "ConfirmUserId"://确认人
                                {
                                    var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(adv.conditionValueVal.value)).Select(a => a.Id).ToArray();
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于

                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.ConfirmUserId ?? 0));
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.ConfirmUserId ?? 0));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.ConfirmUserId ?? 0));
                                            break;
                                    }
                                }
                                break;
                            case "ConfirmDateTime"://确认时间
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmDateTime >= dt0 && a.ConfirmDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmDateTime <= dt1);

                                            }

                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "CreateDateTime"://创建日期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            //if (!string.IsNullOrEmpty(rightdt))
                                            //{
                                            //    dt1 = dt1.AddDays(1);//将当前时间加一天
                                            //}
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime >= dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime >= dt0 && a.CreateDateTime <= dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime <= dt1);

                                            }

                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "CreateUserId"://创建人
                                {
                                    var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(adv.conditionValueVal.value)).Select(a => a.Id).ToArray();
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于

                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.CreateUserId));
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.CreateUserId));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.CreateUserId));
                                            break;
                                    }
                                }
                                break;


                            case "InState"://状态
                                {
                                    byte czt = 10;
                                    byte.TryParse(adv.conditionValueVal.value, out czt);
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.InState == czt);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.InState == czt);
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.InState == czt);
                                            break;
                                    }
                                }
                                break;
                            case "Remark"://备注
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Remark == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Remark.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Remark.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;

                            case "Reserve1"://备用1
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Reserve1 == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Reserve1.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Reserve1.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "Reserve2"://备用2
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Reserve2 == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Reserve2.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Reserve2.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "WfCurrNodeName"://当前节点
                                switch (adv.conditionOptionVal)
                                {

                                    case "like"://包含
                                        predicateOrAdv = predicateOrAdv.Or(a => a.WfCurrNodeName != null && a.WfCurrNodeName.Contains(adv.conditionValueVal.value) && (a.WfState == 1 || a.WfState == 3));
                                        break;
                                    default:
                                        predicateOrAdv = predicateOrAdv.Or(a => a.WfCurrNodeName != null && a.WfCurrNodeName.Contains(adv.conditionValueVal.value) && (a.WfState == 1 || a.WfState == 3));
                                        break;
                                }
                                break;
                        }
                        if (pageParam.jsonStr.Contains("and"))
                        {
                            predicateAnd = predicateAnd.Or(predicateOrAdv);
                        }
                        else
                        {
                            predicateAnd = predicateAnd.And(predicateOrAdv);
                        }
                        
                    }

                    #endregion

                }


            }
            return predicateAnd;
        }

        /// <summary>
        /// 基本筛选
        /// </summary>
        /// <param name="pageParam">页查询条件</param>
        /// <param name="_IUserInforService">用户查询业务类</param>
        /// <returns>合同对方查询表达式</returns>
        public static Expression<Func<Model.Models.ContInvoice, bool>> GetAdvJBSXQueryContInvoice(
              PageparamInfo pageParam
             , IUserInforService _IUserInforService
            , IProjectManagerService  _IProjectManagerService
            , IContPlanFinanceService _IContPlanFinanceService)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.ContInvoice>();
            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//基本筛选
                var advJbcxQuery = AdvQueryHelper.GetAdvJbcxQuery(pageParam.filterSos);
                var predicateAndAdv = PredicateBuilder.True<Model.Models.ContInvoice>();
                var predicateOrAdv = PredicateBuilder.False<Model.Models.ContInvoice>();
                foreach (var adv in advJbcxQuery)
                {
                    #region and
                    if (adv.prefix == "and")
                    {
                        switch (adv.field)
                        {
                            case "InTypeName":////发票类型
                                foreach (var item in adv.children)
                                {
                                    string name = (item.value).ToString().Trim().Replace(" ", "");
                                    int? s = _IProjectManagerService.XMLB(name);
                                    switch (item.type)
                                    {
                                        case "eq"://等于
                                            predicateAndAdv = predicateAndAdv.And(a =>a.InType  == s);
                                            break;
                                        case "ne"://不等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.InType != s);
                                            break;
                                    }
                                }
                                break;
                            case "ContName"://合同名称
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Name == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Name != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Name.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ContCode"://合同编号
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Code == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Code != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Code.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ContCategoryName"://合同类别
                                foreach (var item in adv.children)
                                {
                                    string name = (item.value).ToString().Trim().Replace(" ", "");
                                    int? s = _IProjectManagerService.XMLB((string)name);
                                    switch (item.type)
                                    {
                                        case "eq"://等于
                                            predicateAndAdv = predicateAndAdv.And(a =>(a.Cont != null ? a.Cont.ContTypeId : -1) == s);
                                            break;
                                        case "ne"://不等于
                                            predicateAndAdv = predicateAndAdv.And(a => (a.Cont != null ? a.Cont.ContTypeId : -1) != s);
                                            break;
                                    }
                                }
                                break;
                            case "DeptName"://经办机构
                                foreach (var item in adv.children)
                                {
                                    string name = (item.value).ToString().Trim().Replace(" ", "");
                                    int? s = _IContPlanFinanceService.DepartmentID((string)name);
                                    switch (item.type)
                                    {
                                        case "eq"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => (a.Cont == null ? -1 : a.Cont.DeptId) == s);
                                            break;
                                        case "ne"://不等于
                                            predicateAndAdv = predicateAndAdv.And(a => (a.Cont == null ? -1 : a.Cont.DeptId) != s);
                                            break;
                                    }
                                }
                                break;
                            case "InCode"://发票编号
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.InCode == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.InCode.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.InCode != item.value);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CompName"://合同对方
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => ((a.Cont != null && a.Cont.Comp != null) ? a.Cont.Comp.Name : "") == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => ((a.Cont != null && a.Cont.Comp != null) ? a.Cont.Comp.Name : "").Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => ((a.Cont != null && a.Cont.Comp != null) ? a.Cont.Comp.Name : "") != item.value);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Name.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "AmountMoneyThod"://发票金额
                                {
                                    foreach (var item in adv.children)
                                    {
                                        ///千分位转数字
                                        var je = _IContPlanFinanceService.ParseThousandthString(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney == je);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney != je);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney > je);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney >= je);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney <= je);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.AmountMoney <= je);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ConfirmUserName"://确认人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name == item.value).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ConfirmUserId == userIds);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ConfirmUserId != userIds);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ConfirmDateTime"://确认时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ConfirmDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ConfirmDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ConfirmDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ConfirmDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ConfirmDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ConfirmDateTime <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CreateDateTime"://创建日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CreateUserName"://创建人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name == item.value).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateUserId == userIds);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateUserId != userIds);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "InStateDic"://发票状态
                                {
                                    foreach (var item in adv.children)
                                    {
                                        string name = (item.value).ToString().Trim().Replace(" ", "");
                                        var state = -1;
                                        if (name == "未提交")
                                        {
                                            state = 0;
                                        }
                                        else if (name == "已提交")
                                        {
                                            state = 1;
                                        }
                                        else if (name == "已开出")
                                        {
                                            state = 2;
                                        }
                                        else if (name == "已收到")
                                        {
                                            state = 3;
                                        }
                                        else if (name == "被打回")
                                        {
                                            state = 4;
                                        }

                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.InState == state);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.InState != state);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Remark"://备注
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Remark == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Remark.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Remark != item.value);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Reserve1"://备用1
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a =>a.Reserve1  == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a =>a.Reserve1.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a =>a.Reserve1 != item.value);
                                                break;
                                           
                                        }
                                    }
                                }
                                break;
                            case "Reserve2"://备用2
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a =>a.Reserve2  == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Reserve2.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a =>a.Reserve2 != item.value);
                                                break;
                                        }
                                    }
                                }
                                break;
                        }
                        predicateAnd = predicateAnd.And(predicateAndAdv);
                    }
                    #endregion and

                    #region Or
                    if (adv.prefix == "Or")
                    {
                        switch (adv.field)
                        {
                            case "InTypeName":////发票类型
                                foreach (var item in adv.children)
                                {
                                    string name = (item.value).ToString().Trim().Replace(" ", "");
                                    int? s = _IProjectManagerService.XMLB((string)name);
                                    switch (item.type)
                                    {
                                        case "eq"://等于
                                             predicateOrAdv = predicateOrAdv.Or(a => a.InType == s);
                                            break;
                                        case "ne"://不等于
                                             predicateOrAdv = predicateOrAdv.Or(a => a.InType != s);
                                            break;
                                    }
                                }
                                break;
                            case "ContName"://合同名称
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                 predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Name == item.value);
                                                break;
                                            case "ne"://不等于
                                                 predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Name != item.value);
                                                break;
                                            case "contain"://包含
                                                 predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Name.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ContCode"://合同编号
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                 predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Code == item.value);
                                                break;
                                            case "ne"://不等于
                                                 predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Code != item.value);
                                                break;
                                            case "contain"://包含
                                                 predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Code.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ContCategoryName"://合同类别
                                foreach (var item in adv.children)
                                {
                                    string name = (item.value).ToString().Trim().Replace(" ", "");
                                    int? s = _IProjectManagerService.XMLB((string)name);
                                    switch (item.type)
                                    {
                                        case "eq"://等于
                                             predicateOrAdv = predicateOrAdv.Or(a => (a.Cont != null ? a.Cont.ContTypeId : -1) == s);
                                            break;
                                        case "ne"://不等于
                                             predicateOrAdv = predicateOrAdv.Or(a => (a.Cont != null ? a.Cont.ContTypeId : -1) != s);
                                            break;
                                    }
                                }
                                break;
                            case "DeptName"://经办机构
                                foreach (var item in adv.children)
                                {
                                    string name = (item.value).ToString().Trim().Replace(" ", "");
                                    int? s = _IContPlanFinanceService.DepartmentID((string)name);
                                    switch (item.type)
                                    {
                                        case "eq"://等于
                                             predicateOrAdv = predicateOrAdv.Or(a => (a.Cont == null ? -1 : a.Cont.DeptId) == s);
                                            break;
                                        case "ne"://不等于
                                             predicateOrAdv = predicateOrAdv.Or(a => (a.Cont == null ? -1 : a.Cont.DeptId) != s);
                                            break;
                                    }
                                }
                                break;
                            case "InCode"://发票编号
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                 predicateOrAdv = predicateOrAdv.Or(a => a.InCode == item.value);
                                                break;
                                            case "contain"://包含
                                                 predicateOrAdv = predicateOrAdv.Or(a => a.InCode.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                 predicateOrAdv = predicateOrAdv.Or(a => a.InCode != item.value);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CompName"://合同对方
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                 predicateOrAdv = predicateOrAdv.Or(a => ((a.Cont != null && a.Cont.Comp != null) ? a.Cont.Comp.Name : "") == item.value);
                                                break;
                                            case "contain"://包含
                                                 predicateOrAdv = predicateOrAdv.Or(a => ((a.Cont != null && a.Cont.Comp != null) ? a.Cont.Comp.Name : "").Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                 predicateOrAdv = predicateOrAdv.Or(a => ((a.Cont != null && a.Cont.Comp != null) ? a.Cont.Comp.Name : "") != item.value);
                                                break;
                                            default:
                                                 predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Name.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "AmountMoneyThod"://发票金额
                                {
                                    foreach (var item in adv.children)
                                    {
                                        ///千分位转数字
                                        var je = _IContPlanFinanceService.ParseThousandthString(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                 predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney == je);
                                                break;
                                            case "ne"://不等于
                                                 predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney != je);
                                                break;
                                            case "gt"://大于
                                                 predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney > je);
                                                break;
                                            case "ge"://大于等于
                                                 predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney >= je);
                                                break;
                                            case "lt"://小于
                                                 predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney <= je);
                                                break;
                                            case "le"://小于等于
                                                 predicateOrAdv = predicateOrAdv.Or(a => a.AmountMoney <= je);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ConfirmUserName"://确认人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name == item.value).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                 predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmUserId == userIds);
                                                break;
                                            case "ne"://不等于
                                                 predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmUserId != userIds);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ConfirmDateTime"://确认时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                 predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                 predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                 predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                 predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                 predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                 predicateOrAdv = predicateOrAdv.Or(a => a.ConfirmDateTime <= ctime);
                                                break;
                                            default:
                                                 predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CreateDateTime"://创建日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CreateUserName"://创建人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name == item.value).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateUserId == userIds);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateUserId != userIds);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "InStateDic"://发票状态
                                {
                                    foreach (var item in adv.children)
                                    {
                                        string name = (item.value).ToString().Trim().Replace(" ", "");
                                        var state = -1;
                                        if (name == "未提交")
                                        {
                                            state = 0;
                                        }
                                        else if (name == "已提交")
                                        {
                                            state = 1;
                                        }
                                        else if (name == "已开出")
                                        {
                                            state = 2;
                                        }
                                        else if (name == "已收到")
                                        {
                                            state = 3;
                                        }
                                        else if (name == "被打回")
                                        {
                                            state = 4;
                                        }

                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.InState == state);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.InState != state);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Remark"://备注
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Remark == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Remark.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Remark != item.value);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Reserve1"://备用1
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Reserve1 == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Reserve1.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Reserve1 != item.value);
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "Reserve2"://备用2
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Reserve2 == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Reserve2.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Reserve2 != item.value);
                                                break;
                                        }
                                    }
                                }
                                break;
                        }
                        predicateAnd = predicateAnd.Or(predicateOrAdv);
                    }
                    #endregion


                }


            }
            return predicateAnd;
        }
        #endregion


        #region 询价
        public static Expression<Func<Model.Models.Inquiry, bool>> GetAdvQueryInquiry(
          PageparamInfo pageParam
        , IUserInforService _IUserInforService)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.Inquiry>();
            if (!string.IsNullOrEmpty(pageParam.jsonStr))
            {//高级查询
                var advquerys = AdvQueryHelper.GetAdvQuery(pageParam.jsonStr);
                var predicateAndAdv = PredicateBuilder.True<Model.Models.Inquiry>();
                var predicateOrAdv = PredicateBuilder.False<Model.Models.Inquiry>();
                foreach (var adv in advquerys)
                {
                    #region and
                    if (adv.logicalOperator == "and")
                    {
                        switch (adv.conditionFieldVal)
                        {
                            case "ProjectName"://项目名称
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.ProjectNameNavigation.Name == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.ProjectNameNavigation.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.ProjectNameNavigation.Name.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "ProjectNumber"://项目编号
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.ProjectNumber == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.ProjectNumber.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.ProjectNumber.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "Sites"://地点
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Sites == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Sites.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Sites.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "Times"://时间
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateAndAdv = predicateAndAdv.And(a => a.Times == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            if (!string.IsNullOrEmpty(rightdt))
                                            {
                                                dt1 = dt1.AddDays(1);//将当前时间加一天
                                            }
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.Times > dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.Times > dt0 && a.Times < dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.Times < dt1);

                                            }

                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "MainDeptId"://签约主体
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.ContractExecuteBranchNavigation.Name == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.ContractExecuteBranchNavigation.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.ContractExecuteBranchNavigation.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "TheWinningConditions"://中标条件
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.TheWinningConditions == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.TheWinningConditions.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.TheWinningConditions.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "Recorder"://记录人
                                {
                                    var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(adv.conditionValueVal.value)).Select(a => a.Id).ToArray();
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.Recorder ?? 0));
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.Recorder ?? 0));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.Recorder ?? 0));
                                            break;
                                    }
                                }
                                break;
                            case "UsefulLife"://有效期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateAndAdv = predicateAndAdv.And(a => a.UsefulLife == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            if (!string.IsNullOrEmpty(rightdt))
                                            {
                                                dt1 = dt1.AddDays(1);//将当前时间加一天
                                            }
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.UsefulLife > dt0);
                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.UsefulLife > dt0 && a.UsefulLife < dt1);
                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.UsefulLife < dt1);
                                            }
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "InState"://状态
                                {
                                    byte czt = 10;
                                    byte.TryParse(adv.conditionValueVal.value, out czt);
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.InState == czt);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.InState == czt);
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.InState == czt);
                                            break;
                                    }
                                }
                                break;
                        }
                        predicateAnd = predicateAnd.And(predicateAndAdv);
                    }
                    #endregion and
                    #region Or
                    else if (adv.logicalOperator == "or")
                    {
                        switch (adv.conditionFieldVal)
                        {
                            case "ProjectName"://项目名称
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNameNavigation.Name == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNameNavigation.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNameNavigation.Name.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "ProjectNumber"://项目编号
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNumber == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNumber.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNumber.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "Sites"://地点
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Sites == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Sites.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Sites.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "Times"://时间
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Times == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            if (!string.IsNullOrEmpty(rightdt))
                                            {
                                                dt1 = dt1.AddDays(1);//将当前时间加一天
                                            }
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Times > dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Times > dt0 && a.Times < dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Times < dt1);

                                            }

                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "MainDeptId"://签约主体
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ContractExecuteBranchNavigation.Name == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ContractExecuteBranchNavigation.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ContractExecuteBranchNavigation.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "TheWinningConditions"://中标条件
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.TheWinningConditions == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.TheWinningConditions.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.TheWinningConditions.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "Recorder"://记录人
                                {
                                    var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(adv.conditionValueVal.value)).Select(a => a.Id).ToArray();
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.Recorder ?? 0));
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.Recorder ?? 0));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.Recorder ?? 0));
                                            break;
                                    }
                                }
                                break;
                            case "UsefulLife"://有效期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.UsefulLife == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            if (!string.IsNullOrEmpty(rightdt))
                                            {
                                                dt1 = dt1.AddDays(1);//将当前时间加一天
                                            }
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.UsefulLife > dt0);
                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.UsefulLife > dt0 && a.UsefulLife < dt1);
                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.UsefulLife < dt1);
                                            }
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "InState"://状态
                                {
                                    byte czt = 10;
                                    byte.TryParse(adv.conditionValueVal.value, out czt);
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.InState == czt);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.InState == czt);
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.InState == czt);
                                            break;
                                    }
                                }
                                break;
                        }
                        predicateAnd = predicateAnd.Or(predicateOrAdv);
                    }
                    #endregion
                }
            }
            return predicateAnd;
        }

        //询价基本查询
        public static Expression<Func<Model.Models.Inquiry, bool>> GetJBAdvQueryInquiry(
          PageparamInfo pageParam
        , IUserInforService _IUserInforService)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.Inquiry>();
            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//高级查询
                var advquerys = AdvQueryHelper.GetAdvJbcxQuery(pageParam.filterSos);
                var predicateAndAdv = PredicateBuilder.True<Model.Models.Inquiry>();
                var predicateOrAdv = PredicateBuilder.False<Model.Models.Inquiry>();
                foreach (var adv in advquerys)
                {
                    #region and
                    if (adv.prefix == "and")
                    {
                        switch (adv.field)
                        {
                            case "ProjectName"://项目名称
                                {

                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ProjectNameNavigation.Name == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ProjectNameNavigation.Name != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.ProjectNameNavigation.Name.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.ProjectNameNavigation.Name.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "ProjectNumber"://项目编号
                                {

                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ProjectNumber == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ProjectNumber != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.ProjectNumber.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.ProjectNumber.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Sites"://地点
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sites == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sites != item.value);
                                                break;
                                            case "contain"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sites.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sites.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Times"://时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Times == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Times != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Times > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Times >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Times < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Times <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "MdeptName"://合同执行部门
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ContractExecuteBranchNavigation.Name == item.value);
                                                break;
                                            case "ne"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.ContractExecuteBranchNavigation.Name != item.value);
                                                break;
                                            case "contain"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.ContractExecuteBranchNavigation.Name.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.ContractExecuteBranchNavigation.Name.Contains(item.value));
                                                break;
                                        }

                                    }
                                }
                                break;
                            case "TheWinningConditions"://中标条件
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.TheWinningConditions == item.value);
                                                break;
                                            case "ne"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.TheWinningConditions.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.TheWinningConditions.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "RecorderName"://记录人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(item.value)).Select(a => a.Id).ToArray();
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => userIds == userIds);
                                                break;
                                            case "ne"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => userIds != userIds);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "UsefulLife"://有效期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.UsefulLife == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.UsefulLife != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.UsefulLife > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.UsefulLife >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.UsefulLife < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.UsefulLife <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "InState"://状态
                                {
                                    foreach (var item in adv.children)
                                    {
                                        byte czt = 10;
                                        byte.TryParse(item.value, out czt);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.InState == czt);
                                                break;
                                            case "ne"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.InState != czt);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }

                                }
                                break;
                        }
                        predicateAnd = predicateAnd.And(predicateAndAdv);
                    }
                    #endregion and
                    #region Or
                    else if (adv.prefix == "or")
                    {
                        switch (adv.field)
                        {
                            case "ProjectName"://项目名称
                                {

                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNameNavigation.Name == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNameNavigation.Name != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNameNavigation.Name.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNameNavigation.Name.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "ProjectNumber"://项目编号
                                {

                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNumber == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNumber != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNumber.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNumber.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Sites"://地点
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sites == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sites != item.value);
                                                break;
                                            case "contain"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sites.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sites.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Times"://时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Times == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Times != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Times > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Times >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Times < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Times <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "MdeptName"://合同执行部门
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ContractExecuteBranchNavigation.Name == item.value);
                                                break;
                                            case "ne"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ContractExecuteBranchNavigation.Name != item.value);
                                                break;
                                            case "contain"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ContractExecuteBranchNavigation.Name.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ContractExecuteBranchNavigation.Name.Contains(item.value));
                                                break;
                                        }

                                    }
                                }
                                break;
                            case "TheWinningConditions"://中标条件
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.TheWinningConditions == item.value);
                                                break;
                                            case "ne"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.TheWinningConditions.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.TheWinningConditions.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "RecorderName"://记录人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(item.value)).Select(a => a.Id).ToArray();
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => userIds == userIds);
                                                break;
                                            case "ne"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => userIds != userIds);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "UsefulLife"://有效期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.UsefulLife == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.UsefulLife != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.UsefulLife > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.UsefulLife >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.UsefulLife < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.UsefulLife <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "InState"://状态
                                {
                                    foreach (var item in adv.children)
                                    {
                                        byte czt = 10;
                                        byte.TryParse(item.value, out czt);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.InState == czt);
                                                break;
                                            case "ne"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.InState != czt);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }

                                }
                                break;
                        }
                        if (pageParam.filterSos.Contains("and"))
                        {
                            predicateAndAdv = predicateAndAdv.Or(predicateOrAdv);
                        }
                        else
                        {
                            predicateOrAdv = predicateOrAdv.And(predicateAndAdv);
                        }
                    }
                    #endregion
                }
            }
            return predicateAnd;
        }
        #endregion

        #region 约谈
        public static Expression<Func<Model.Models.Questioning, bool>> GetAdvQueryQuestioning(
          PageparamInfo pageParam
        , IUserInforService _IUserInforService)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.Questioning>();
            if (!string.IsNullOrEmpty(pageParam.jsonStr))
            {//高级查询
                var advquerys = AdvQueryHelper.GetAdvQuery(pageParam.jsonStr);
                var predicateAndAdv = PredicateBuilder.True<Model.Models.Questioning>();
                var predicateOrAdv = PredicateBuilder.False<Model.Models.Questioning>();
                foreach (var adv in advquerys)
                {
                    #region and
                    if (adv.logicalOperator == "and")
                    {
                        switch (adv.conditionFieldVal)
                        {
                            case "ProjectName"://项目名称
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.ProjectNameNavigation.Name == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.ProjectNameNavigation.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.ProjectNameNavigation.Name.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "ProjectNumber"://项目编号
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.ProjectNumber == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.ProjectNumber.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.ProjectNumber.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "Sites"://地点
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.Sites == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.Sites.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.Sites.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "Times"://时间
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateAndAdv = predicateAndAdv.And(a => a.Times == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            if (!string.IsNullOrEmpty(rightdt))
                                            {
                                                dt1 = dt1.AddDays(1);//将当前时间加一天
                                            }
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.Times > dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.Times > dt0 && a.Times < dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.Times < dt1);

                                            }

                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "MainDeptId"://签约主体
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.ContractExecuteBranchNavigation.Name == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.ContractExecuteBranchNavigation.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.ContractExecuteBranchNavigation.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "TheWinningConditions"://中标条件
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.TheWinningConditions == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.TheWinningConditions.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.TheWinningConditions.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "Recorder"://记录人
                                {
                                    var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(adv.conditionValueVal.value)).Select(a => a.Id).ToArray();
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.Recorder ?? 0));
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.Recorder ?? 0));
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => userIds.Contains(a.Recorder ?? 0));
                                            break;
                                    }
                                }
                                break;
                            case "UsefulLife"://有效期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateAndAdv = predicateAndAdv.And(a => a.UsefulLife == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            if (!string.IsNullOrEmpty(rightdt))
                                            {
                                                dt1 = dt1.AddDays(1);//将当前时间加一天
                                            }
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.UsefulLife > dt0);
                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.UsefulLife > dt0 && a.UsefulLife < dt1);
                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateAndAdv = predicateAndAdv.And(a => a.UsefulLife < dt1);
                                            }
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "InState"://状态
                                {
                                    byte czt = 10;
                                    byte.TryParse(adv.conditionValueVal.value, out czt);
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateAndAdv = predicateAndAdv.And(a => a.InState == czt);
                                            break;
                                        case "like"://包含
                                            predicateAndAdv = predicateAndAdv.And(a => a.InState == czt);
                                            break;
                                        default:
                                            predicateAndAdv = predicateAndAdv.And(a => a.InState == czt);
                                            break;
                                    }
                                }
                                break;
                        }
                        predicateAnd = predicateAnd.And(predicateAndAdv);
                    }
                    #endregion and
                    #region Or
                    else if (adv.logicalOperator == "or")
                    {
                        switch (adv.conditionFieldVal)
                        {
                            case "ProjectName"://项目名称
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNameNavigation.Name == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNameNavigation.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNameNavigation.Name.Contains(adv.conditionValueVal.value));
                                            break;

                                    }
                                }
                                break;
                            case "ProjectNumber"://项目编号
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNumber == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNumber.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNumber.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "Sites"://地点
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Sites == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Sites.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Sites.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "Times"://时间
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.Times == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            if (!string.IsNullOrEmpty(rightdt))
                                            {
                                                dt1 = dt1.AddDays(1);//将当前时间加一天
                                            }
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Times > dt0);

                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Times > dt0 && a.Times < dt1);


                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Times < dt1);

                                            }

                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "MainDeptId"://签约主体
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ContractExecuteBranchNavigation.Name == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ContractExecuteBranchNavigation.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.ContractExecuteBranchNavigation.Name.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "TheWinningConditions"://中标条件
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.TheWinningConditions == adv.conditionValueVal.value);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.TheWinningConditions.Contains(adv.conditionValueVal.value));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.TheWinningConditions.Contains(adv.conditionValueVal.value));
                                            break;
                                    }
                                }
                                break;
                            case "Recorder"://记录人
                                {
                                    var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(adv.conditionValueVal.value)).Select(a => a.Id).ToArray();
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.Recorder ?? 0));
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.Recorder ?? 0));
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => userIds.Contains(a.Recorder ?? 0));
                                            break;
                                    }
                                }
                                break;
                            case "UsefulLife"://有效期
                                {
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            DateTime ctime = DateTime.Now;
                                            DateTime.TryParse(adv.conditionValueVal.value, out ctime);
                                            predicateOrAdv = predicateOrAdv.Or(a => a.UsefulLife == ctime);
                                            break;
                                        case "between"://范围
                                            var leftdt = adv.conditionValueLeftVal.value;
                                            var rightdt = adv.conditionValueRightVal.value;
                                            DateTime dt0 = DateTime.Now;
                                            DateTime dt1 = DateTime.Now;
                                            DateTime.TryParse(leftdt, out dt0);
                                            DateTime.TryParse(rightdt, out dt1);
                                            if (!string.IsNullOrEmpty(rightdt))
                                            {
                                                dt1 = dt1.AddDays(1);//将当前时间加一天
                                            }
                                            if (string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.UsefulLife > dt0);
                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && !string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.UsefulLife > dt0 && a.UsefulLife < dt1);
                                            }
                                            else if (!string.IsNullOrEmpty(rightdt) && string.IsNullOrEmpty(leftdt))
                                            {
                                                predicateOrAdv = predicateOrAdv.Or(a => a.UsefulLife < dt1);
                                            }
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => false);
                                            break;
                                    }
                                }
                                break;
                            case "InState"://状态
                                {
                                    byte czt = 10;
                                    byte.TryParse(adv.conditionValueVal.value, out czt);
                                    switch (adv.conditionOptionVal)
                                    {
                                        case "equal"://等于
                                            predicateOrAdv = predicateOrAdv.Or(a => a.InState == czt);
                                            break;
                                        case "like"://包含
                                            predicateOrAdv = predicateOrAdv.Or(a => a.InState == czt);
                                            break;
                                        default:
                                            predicateOrAdv = predicateOrAdv.Or(a => a.InState == czt);
                                            break;
                                    }
                                }
                                break;
                        }
                        predicateAnd = predicateAnd.Or(predicateOrAdv);
                    }
                    #endregion
                }
            }
            return predicateAnd;
        }

        public static Expression<Func<Model.Models.Questioning, bool>> GetytAdvQueryQuestioning(
          PageparamInfo pageParam
        , IUserInforService _IUserInforService)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.Questioning>();
            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//高级查询
                var advquerys = AdvQueryHelper.GetAdvJbcxQuery(pageParam.filterSos);
                var predicateAndAdv = PredicateBuilder.True<Model.Models.Questioning>();
                var predicateOrAdv = PredicateBuilder.False<Model.Models.Questioning>();
                foreach (var adv in advquerys)
                {
                    #region and
                    if (adv.prefix == "and")
                    {
                        switch (adv.field)
                        {
                            case "ProjectName"://项目名称
                                {

                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ProjectNameNavigation.Name == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ProjectNameNavigation.Name != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.ProjectNameNavigation.Name.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.ProjectNameNavigation.Name.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "ProjectNumber"://项目编号
                                {

                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ProjectNumber == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ProjectNumber != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.ProjectNumber.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.ProjectNumber.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Sites"://地点
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sites == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sites != item.value);
                                                break;
                                            case "contain"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sites.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sites.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Times"://时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Times == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Times != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Times > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Times >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Times < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Times <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "MdeptName"://合同执行部门
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ContractExecuteBranchNavigation.Name == item.value);
                                                break;
                                            case "ne"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.ContractExecuteBranchNavigation.Name != item.value);
                                                break;
                                            case "contain"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.ContractExecuteBranchNavigation.Name.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.ContractExecuteBranchNavigation.Name.Contains(item.value));
                                                break;
                                        }

                                    }
                                }
                                break;
                            case "TheWinningConditions"://中标条件
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.TheWinningConditions == item.value);
                                                break;
                                            case "ne"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.TheWinningConditions.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.TheWinningConditions.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "RecorderName"://记录人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(item.value)).Select(a => a.Id).ToArray();
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => userIds == userIds);
                                                break;
                                            case "ne"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => userIds != userIds);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "UsefulLife"://有效期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.UsefulLife == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.UsefulLife != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.UsefulLife > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.UsefulLife >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.UsefulLife < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.UsefulLife <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "InState"://状态
                                {
                                    foreach (var item in adv.children)
                                    {
                                        byte czt = 10;
                                        byte.TryParse(item.value, out czt);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.InState == czt);
                                                break;
                                            case "ne"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.InState != czt);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }

                                }
                                break;
                        }
                        predicateAnd = predicateAnd.And(predicateAndAdv);
                    }
                    #endregion and
                    #region Or
                    else if (adv.prefix == "or")
                    {
                        switch (adv.field)
                        {
                            case "ProjectName"://项目名称
                                {

                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNameNavigation.Name == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNameNavigation.Name != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNameNavigation.Name.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNameNavigation.Name.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "ProjectNumber"://项目编号
                                {

                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNumber == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNumber != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNumber.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNumber.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Sites"://地点
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sites == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sites != item.value);
                                                break;
                                            case "contain"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sites.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sites.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Times"://时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Times == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Times != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Times > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Times >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Times < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Times <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "MdeptName"://合同执行部门
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ContractExecuteBranchNavigation.Name == item.value);
                                                break;
                                            case "ne"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ContractExecuteBranchNavigation.Name != item.value);
                                                break;
                                            case "contain"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ContractExecuteBranchNavigation.Name.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ContractExecuteBranchNavigation.Name.Contains(item.value));
                                                break;
                                        }

                                    }
                                }
                                break;
                            case "TheWinningConditions"://中标条件
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.TheWinningConditions == item.value);
                                                break;
                                            case "ne"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.TheWinningConditions.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.TheWinningConditions.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "RecorderName"://记录人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(item.value)).Select(a => a.Id).ToArray();
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => userIds == userIds);
                                                break;
                                            case "ne"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => userIds != userIds);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "UsefulLife"://有效期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.UsefulLife == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.UsefulLife != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.UsefulLife > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.UsefulLife >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.UsefulLife < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.UsefulLife <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "InState"://状态
                                {
                                    foreach (var item in adv.children)
                                    {
                                        byte czt = 10;
                                        byte.TryParse(item.value, out czt);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.InState == czt);
                                                break;
                                            case "ne"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.InState != czt);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }

                                }
                                break;
                        }
                        if (pageParam.filterSos.Contains("and"))
                        {
                            predicateAndAdv = predicateAndAdv.Or(predicateOrAdv);
                        }
                        else
                        {
                            predicateOrAdv = predicateOrAdv.And(predicateAndAdv);
                        }
                    }
                    #endregion
                }
            }
            return predicateAnd;
        }

        #endregion

        #region 招标
        public static Expression<Func<Model.Models.TenderInfor, bool>> GetytAdvQueryTenderInfor(
          PageparamInfo pageParam
        , IUserInforService _IUserInforService)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.TenderInfor>();
            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//高级查询
                var advquerys = AdvQueryHelper.GetAdvJbcxQuery(pageParam.filterSos);
                var predicateAndAdv = PredicateBuilder.True<Model.Models.TenderInfor>();
                var predicateOrAdv = PredicateBuilder.False<Model.Models.TenderInfor>();
                foreach (var adv in advquerys)
                {
                    #region and
                    if (adv.prefix == "and")
                    {
                        switch (adv.field)
                        {
                            case "ProjectName"://项目名称
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Project.Name == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Project.Name != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Project.Name.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Project.Name.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "ProjectNO"://项目编号
                                {

                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ProjectNo == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ProjectNo != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.ProjectNo.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.ProjectNo.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Iocation"://地点
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Iocation == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Iocation != item.value);
                                                break;
                                            case "contain"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.Iocation.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Iocation.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "TenderDate"://时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.TenderDate == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.TenderDate != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.TenderDate > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.TenderDate >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.TenderDate < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.TenderDate <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            //case "ContractEnforcementDepName"://合同执行部门
                            //    {
                            //        foreach (var item in adv.children)
                            //        {
                            //            switch (item.type)
                            //            {
                            //                case "eq"://等于
                            //                    predicateAndAdv = predicateAndAdv.And(a => a.ContractEnforcementDepId.Name == item.value);
                            //                    break;
                            //                case "ne"://
                            //                    predicateAndAdv = predicateAndAdv.And(a => a.ContractExecuteBranchNavigation.Name != item.value);
                            //                    break;
                            //                case "contain"://
                            //                    predicateAndAdv = predicateAndAdv.And(a => a.ContractExecuteBranchNavigation.Name.Contains(item.value));
                            //                    break;
                            //                default:
                            //                    predicateAndAdv = predicateAndAdv.And(a => a.ContractExecuteBranchNavigation.Name.Contains(item.value));
                            //                    break;
                            //            }

                            //        }
                            //    }
                            //    break;
                            case "TheWinningConditions"://中标单位
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ZbdwNavigation.Name == item.value);
                                                break;
                                            case "ne"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.ZbdwNavigation.Name.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.ZbdwNavigation.Name.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "RecorderName"://记录人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(item.value)).Select(a => a.Id).ToArray();
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => userIds == userIds);
                                                break;
                                            case "ne"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => userIds != userIds);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "TenderExpirationDate"://有效期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.TenderExpirationDate == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.TenderExpirationDate != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.TenderExpirationDate > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.TenderExpirationDate >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.TenderExpirationDate < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.TenderExpirationDate <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "TenderStatus"://状态
                                {
                                    foreach (var item in adv.children)
                                    {
                                        byte czt = 10;
                                        byte.TryParse(item.value, out czt);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.TenderStatus == czt);
                                                break;
                                            case "ne"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.TenderStatus != czt);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }

                                }
                                break;
                        }
                        predicateAnd = predicateAnd.And(predicateAndAdv);
                    }
                    #endregion and
                    #region Or
                    else if (adv.prefix == "or")
                    {
                        switch (adv.field)
                        {
                            case "ProjectName"://项目名称
                                {

                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Project.Name == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Project.Name != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Project.Name.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Project.Name.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "ProjectNO"://项目编号
                                {

                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNo == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNo != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNo.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ProjectNo.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Iocation"://地点
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Iocation == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Iocation != item.value);
                                                break;
                                            case "contain"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Iocation.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Iocation.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "TenderDate"://时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.TenderDate == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.TenderDate != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.TenderDate > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.TenderDate >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.TenderDate < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.TenderDate <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            //case "ContractEnforcementDepName"://合同执行部门
                            //    {
                            //        foreach (var item in adv.children)
                            //        {
                            //            switch (item.type)
                            //            {
                            //                case "eq"://等于
                            //                    predicateOrAdv=predicateOrAdv.Or(a => a.ContractEnforcementDepId.Name == item.value);
                            //                    break;
                            //                case "ne"://
                            //                    predicateOrAdv=predicateOrAdv.Or(a => a.ContractExecuteBranchNavigation.Name != item.value);
                            //                    break;
                            //                case "contain"://
                            //                    predicateOrAdv=predicateOrAdv.Or(a => a.ContractExecuteBranchNavigation.Name.Contains(item.value));
                            //                    break;
                            //                default:
                            //                    predicateOrAdv=predicateOrAdv.Or(a => a.ContractExecuteBranchNavigation.Name.Contains(item.value));
                            //                    break;
                            //            }

                            //        }
                            //    }
                            //    break;
                            case "TheWinningConditions"://中标单位
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ZbdwNavigation.Name == item.value);
                                                break;
                                            case "ne"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ZbdwNavigation.Name.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ZbdwNavigation.Name.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "RecorderName"://记录人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(item.value)).Select(a => a.Id).ToArray();
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => userIds == userIds);
                                                break;
                                            case "ne"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => userIds != userIds);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "TenderExpirationDate"://有效期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.TenderExpirationDate == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.TenderExpirationDate != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.TenderExpirationDate > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.TenderExpirationDate >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.TenderExpirationDate < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.TenderExpirationDate <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "TenderStatus"://状态
                                {
                                    foreach (var item in adv.children)
                                    {
                                        byte czt = 10;
                                        byte.TryParse(item.value, out czt);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.TenderStatus == czt);
                                                break;
                                            case "ne"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.TenderStatus != czt);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }

                                }
                                break;
                        }

                        if (pageParam.filterSos.Contains("and"))
                        {
                            predicateAndAdv = predicateAndAdv.Or(predicateOrAdv);
                        }
                        else
                        {
                            predicateOrAdv = predicateOrAdv.And(predicateAndAdv);
                        }
                    }
                    #endregion
                }
            }
            return predicateAnd;
        }
        #endregion

        #region 合同标的
        public static Expression<Func<Model.Models.ContSubjectMatter, bool>> GetAdvJBSXQueryHtBD(
           PageparamInfo pageParam,
          IContPlanFinanceService _IContPlanFinanceService)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.ContSubjectMatter>();
            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//基本筛选
                var advJbcxQuery = AdvQueryHelper.GetAdvJbcxQuery(pageParam.filterSos);
                var predicateAndAdv = PredicateBuilder.True<Model.Models.ContSubjectMatter>();
                var predicateOrAdv = PredicateBuilder.False<Model.Models.ContSubjectMatter>();
                foreach (var adv in advJbcxQuery)
                {
                    #region and
                    if (adv.prefix == "and")
                    {
                        switch (adv.field)
                        {
                            case "SubName"://标的名称
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Name == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Name.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Name != item.value);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Name.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ContName"://合同名称
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Name == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Name.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Name != item.value);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Name.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ContNo"://合同编号
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Code == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Code.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Code != item.value);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "PriceThod"://单价
                                {
                                    foreach (var item in adv.children)
                                    {

                                        var je = _IContPlanFinanceService.ParseThousandthString(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Price == je);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Price != je);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Price > je);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Price >= je);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Price <= je);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Price <= je);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "TotalThod"://小计
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var je = _IContPlanFinanceService.ParseThousandthString(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => ((a.Price ?? 0) * (a.Amount ?? 0)) == je);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => ((a.Price ?? 0) * (a.Amount ?? 0)) != je);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => ((a.Price ?? 0) * (a.Amount ?? 0)) > je);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => ((a.Price ?? 0) * (a.Amount ?? 0)) >= je);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => ((a.Price ?? 0) * (a.Amount ?? 0)) <= je);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => ((a.Price ?? 0) * (a.Amount ?? 0)) <= je);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CompName": //合同对方
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var je = item.value;
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Comp.Name == je);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Cont.Comp.Name != je);
                                                break;

                                        }
                                    }
                                }
                                break;
                        }
                        predicateAnd = predicateAnd.And(predicateAndAdv);
                    }
                    #endregion and

                    #region Or
                    if (adv.prefix == "Or")
                    {
                        switch (adv.field)
                        {
                            case "SubName"://标的名称
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Name == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Name.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Name != item.value);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Name.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ContName"://合同名称
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Name == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Name.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Name != item.value);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Name.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ContNo"://合同编号
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Code == item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Code.Contains(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Code != item.value);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "PriceThod"://单价
                                {
                                    foreach (var item in adv.children)
                                    {

                                        var je = _IContPlanFinanceService.ParseThousandthString(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Price == je);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Price != je);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Price > je);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Price >= je);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Price <= je);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Price <= je);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "TotalThod"://小计
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var je = _IContPlanFinanceService.ParseThousandthString(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => ((a.Price ?? 0) * (a.Amount ?? 0)) == je);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => ((a.Price ?? 0) * (a.Amount ?? 0)) != je);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => ((a.Price ?? 0) * (a.Amount ?? 0)) > je);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => ((a.Price ?? 0) * (a.Amount ?? 0)) >= je);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => ((a.Price ?? 0) * (a.Amount ?? 0)) <= je);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => ((a.Price ?? 0) * (a.Amount ?? 0)) <= je);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CompName": //合同对方
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var je = item.value;
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Comp.Name == je);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Cont.Comp.Name != je);
                                                break;

                                        }
                                    }
                                }
                                break;
                        }
                        predicateAnd = predicateAnd.Or(predicateOrAdv);
                    }
                    #endregion
                }
            }
            return predicateAnd;
        }
        #endregion

        #region 标的交付明细
        public static Expression<Func<Model.Models.ContSubDe, bool>> GetytAdvQueryContSubDe(
          PageparamInfo pageParam
        , IUserInforService _IUserInforService)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.ContSubDe>();
            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//高级查询
                var advquerys = AdvQueryHelper.GetAdvJbcxQuery(pageParam.filterSos);
                var predicateAndAdv = PredicateBuilder.True<Model.Models.ContSubDe>();
                var predicateOrAdv = PredicateBuilder.False<Model.Models.ContSubDe>();
                foreach (var adv in advquerys)
                {
                    #region and
                    if (adv.prefix == "and")
                    {
                        switch (adv.field)
                        {
                            case "SubName"://标的名称
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sub.Name == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sub.Name != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sub.Name.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sub.Name.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "ContName"://合同名称
                                {

                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sub.Cont.Name == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sub.Cont.Name != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sub.Cont.Name.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sub.Cont.Name.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ContCode"://合同编号
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sub.Cont.Code == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sub.Cont.Code != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sub.Cont.Code.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sub.Cont.Code.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ActDate"://交付日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ActualDateTime <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "DevNumber"://交付数量
                                {
                                    foreach (var item in adv.children)
                                    {
                                        decimal ctime = 0;
                                        decimal.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CurrDevNumber == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CurrDevNumber != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CurrDevNumber > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CurrDevNumber >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CurrDevNumber < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CurrDevNumber <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "DevDz"://交付地点
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.DeliverLocation == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.DeliverLocation != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.DeliverLocation.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.DeliverLocation.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "DevFs"://交付方式
                                {
                                    foreach (var item in adv.children)
                                    {
                                        byte czt = 10;
                                        byte.TryParse(item.value, out czt);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.DeliverType == czt);
                                                break;
                                            case "ne"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.DeliverType != czt);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }

                                }
                                break;
                            case "CompName"://合同对方
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sub.Cont.Comp.Name == item.value);
                                                break;
                                            case "ne"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sub.Cont.Comp.Name != item.value);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sub.Cont.Comp.Name.Contains(item.value));
                                                break;
                                        }
                                    }

                                }
                                break;
                            case "Unit"://标的单位
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.Unit == item.value);
                                                break;
                                            case "ne"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.Unit != item.value);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.Unit.Contains(item.value));
                                                break;
                                        }
                                    }

                                }
                                break;
                            case "DjThod"://标的单价
                                {
                                    foreach (var item in adv.children)
                                    {
                                        decimal ctime = 0;
                                        decimal.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sub.Price == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sub.Price != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sub.Price > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sub.Price >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sub.Price < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sub.Price <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Bz1"://标的单位
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Field1 == item.value);
                                                break;
                                            case "ne"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Field1 != item.value);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Field1.Contains(item.value));
                                                break;
                                        }
                                    }

                                }
                                break;
                            case "Bz2"://标的单位
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Field2 == item.value);
                                                break;
                                            case "ne"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Field2 != item.value);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Field2.Contains(item.value));
                                                break;
                                        }
                                    }

                                }
                                break;
                            case "PlanDate"://计划日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sub.PlanDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sub.PlanDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sub.PlanDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sub.PlanDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sub.PlanDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sub.PlanDateTime <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                        }
                        predicateAnd = predicateAnd.And(predicateAndAdv);
                    }
                    #endregion and
                    #region Or
                    else if (adv.prefix == "or")
                    {
                        switch (adv.field)
                        {
                            case "SubName"://标的名称
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.Name == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.Name != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.Name.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.Name.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "ContName"://合同名称
                                {

                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.Cont.Name == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.Cont.Name != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.Cont.Name.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.Cont.Name.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ContCode"://合同编号
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.Cont.Code == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.Cont.Code != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.Cont.Code.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.Cont.Code.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ActDate"://交付日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ActualDateTime <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "DevNumber"://交付数量
                                {
                                    foreach (var item in adv.children)
                                    {
                                        decimal ctime = 0;
                                        decimal.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CurrDevNumber == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CurrDevNumber != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CurrDevNumber > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CurrDevNumber >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CurrDevNumber < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CurrDevNumber <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "DevDz"://交付地点
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.DeliverLocation == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.DeliverLocation != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.DeliverLocation.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.DeliverLocation.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "DevFs"://交付方式
                                {
                                    foreach (var item in adv.children)
                                    {
                                        byte czt = 10;
                                        byte.TryParse(item.value, out czt);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.DeliverType == czt);
                                                break;
                                            case "ne"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.DeliverType != czt);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }

                                }
                                break;
                            case "CompName"://合同对方
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.Cont.Comp.Name == item.value);
                                                break;
                                            case "ne"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.Cont.Comp.Name != item.value);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.Cont.Comp.Name.Contains(item.value));
                                                break;
                                        }
                                    }

                                }
                                break;
                            case "Unit"://标的单位
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.Unit == item.value);
                                                break;
                                            case "ne"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.Unit != item.value);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.Unit.Contains(item.value));
                                                break;
                                        }
                                    }

                                }
                                break;
                            case "DjThod"://标的单价
                                {
                                    foreach (var item in adv.children)
                                    {
                                        decimal ctime = 0;
                                        decimal.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.Price == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.Price != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.Price > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.Price >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.Price < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.Price <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Bz1"://标的单位
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Field1 == item.value);
                                                break;
                                            case "ne"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Field1 != item.value);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Field1.Contains(item.value));
                                                break;
                                        }
                                    }

                                }
                                break;
                            case "Bz2"://标的单位
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Field2 == item.value);
                                                break;
                                            case "ne"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Field2 != item.value);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Field2.Contains(item.value));
                                                break;
                                        }
                                    }

                                }
                                break;
                            case "PlanDate"://计划日期
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.PlanDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.PlanDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.PlanDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.PlanDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.PlanDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sub.PlanDateTime <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                        }

                        if (pageParam.filterSos.Contains("and"))
                        {
                            predicateAndAdv = predicateAndAdv.Or(predicateOrAdv);
                        }
                        else
                        {
                            predicateOrAdv = predicateOrAdv.And(predicateAndAdv);
                        }
                    }
                    #endregion
                }
            }
            return predicateAnd;
        }
        #endregion

        #region 进度管理

        //询价基本查询
        public static Expression<Func<Model.Models.ScheduleManagement, bool>> GetJBAdvQueryScheduleM(
          PageparamInfo pageParam
        , IUserInforService _IUserInforService, IDataDictionaryService _IDataDictionaryService)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.ScheduleManagement>();
            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//询价基本查询
                var advquerys = AdvQueryHelper.GetAdvJbcxQuery(pageParam.filterSos);
                var predicateAndAdv = PredicateBuilder.True<Model.Models.ScheduleManagement>();
                var predicateOrAdv = PredicateBuilder.False<Model.Models.ScheduleManagement>();
                foreach (var adv in advquerys)
                {
                    #region and
                    if (adv.prefix == "and")
                    {
                        switch (adv.field)
                        {
                            case "ScheduleName"://进度名称
                                {

                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ScheduleName == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ScheduleName != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.ScheduleName.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.ScheduleName.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "ScheduleSer"://任务关键字
                                {

                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ScheduleSer == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ScheduleSer != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.ScheduleSer.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.ScheduleSer.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "PriorityDic"://优先级
                                {

                                    foreach (var item in adv.children)
                                    {
                                        int DataId = _IDataDictionaryService.GetDataDicID(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Priority == DataId);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Priority != DataId);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ScheduleAttributionDic"://任务归属
                                {
                                    foreach (var item in adv.children)
                                    {
                                        int DataId = _IDataDictionaryService.GetDataDicID(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ScheduleAttribution == DataId);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ScheduleAttribution != DataId);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            //case "ScheduleDuixiangName"://任务对象
                            //    {
                            //        foreach (var item in adv.children)
                            //        {
                            //            DateTime ctime = DateTime.Now;
                            //            DateTime.TryParse(item.value, out ctime);
                            //            switch (item.type)
                            //            {
                            //                case "eq"://等于
                            //                    predicateAndAdv = predicateAndAdv.And(a => a.Times == ctime);
                            //                    break;
                            //                case "ne"://不等于
                            //                    predicateAndAdv = predicateAndAdv.And(a => a.Times != ctime);
                            //                    break;
                            //                case "gt"://大于
                            //                    predicateAndAdv = predicateAndAdv.And(a => a.Times > ctime);
                            //                    break;
                            //                case "ge"://大于等于
                            //                    predicateAndAdv = predicateAndAdv.And(a => a.Times >= ctime);
                            //                    break;
                            //                case "lt"://小于
                            //                    predicateAndAdv = predicateAndAdv.And(a => a.Times < ctime);
                            //                    break;
                            //                case "le"://小于等于
                            //                    predicateAndAdv = predicateAndAdv.And(a => a.Times <= ctime);
                            //                    break;
                            //                default:
                            //                    predicateAndAdv = predicateAndAdv.And(a => false);
                            //                    break;
                            //            }
                            //        }
                            //    }
                            //    break;
                            case "Description"://描述
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Description == item.value);
                                                break;
                                            case "ne"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.Description != item.value);
                                                break;
                                            case "contain"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.Description.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Description.Contains(item.value));
                                                break;
                                        }

                                    }
                                }
                                break;
                            case "DesigneeName"://指派给
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(item.value)).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq":
                                                predicateAndAdv = predicateAndAdv.And(a => a.Designee == userIds);
                                                break;
                                            case "ne"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.Designee!= userIds);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "StalkerName"://记录人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(item.value)).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Stalker == userIds);
                                                break;
                                            case "ne"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Stalker != userIds);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CreateName"://创建人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(item.value)).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateUserId == userIds);
                                                break;
                                            case "ne"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateUserId != userIds);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ModifyName"://记录人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(item.value)).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ModifyUserId == userIds);
                                                break;
                                            case "ne"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.ModifyUserId != userIds);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Wancheng"://完成
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Wancheng ==Convert.ToInt32(item.value));
                                                break;
                                            case "ne":
                                                predicateAndAdv = predicateAndAdv.And(a => a.Wancheng != Convert.ToInt32(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "JhCreateDateTime"://计划开始时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.JhCreateDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.JhCreateDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.JhCreateDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.JhCreateDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.JhCreateDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.JhCreateDateTime <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "JhCompleteDateTime"://计划完成时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.JhCompleteDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.JhCompleteDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.JhCompleteDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.JhCompleteDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.JhCompleteDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.JhCompleteDateTime <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "SjCreateDateTime"://计划开始时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.SjCreateDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.SjCreateDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.SjCreateDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.SjCreateDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.SjCreateDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.SjCreateDateTime <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "SjCompleteDateTime"://计划开始时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.SjCompleteDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.SjCompleteDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.SjCompleteDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.SjCompleteDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.SjCompleteDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.SjCompleteDateTime <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CreateDateTime"://计划开始时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateDateTime <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ModifyDateTime"://计划开始时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ModifyDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ModifyDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ModifyDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ModifyDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ModifyDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ModifyDateTime <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "State"://状态
                                {
                                    foreach (var item in adv.children)
                                    {
                                        int st = 0;
                                        switch (item.value)
                                        {
                                            case "未完成":
                                                st = 0;
                                                break;
                                            case "已完成":
                                                st = 1;
                                                break;
                                            default:
                                                break;
                                        }
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.State == st);
                                                break;
                                            case "ne"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.State != st);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }

                                }
                                break;
                        }
                        predicateAnd = predicateAnd.And(predicateAndAdv);
                    }
                    #endregion and
                    #region Or
                    else if (adv.prefix == "or")
                    {
                        switch (adv.field)
                        {
                            case "ScheduleName"://进度名称
                                {

                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ScheduleName == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ScheduleName != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ScheduleName.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ScheduleName.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "PriorityDic"://优先级
                                {

                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ScheduleSer == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ScheduleSer != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ScheduleSer.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ScheduleSer.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ScheduleAttributionDic"://任务归属
                                {
                                    foreach (var item in adv.children)
                                    {
                                        int DataId = _IDataDictionaryService.GetDataDicID(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ScheduleAttribution == DataId);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ScheduleAttribution != DataId);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            //case "ScheduleDuixiangName"://任务对象
                            //    {
                            //        foreach (var item in adv.children)
                            //        {
                            //            DateTime ctime = DateTime.Now;
                            //            DateTime.TryParse(item.value, out ctime);
                            //            switch (item.type)
                            //            {
                            //                case "eq"://等于
                            //                    predicateOrAdv = predicateOrAdv.Or(a => a.Times == ctime);
                            //                    break;
                            //                case "ne"://不等于
                            //                    predicateOrAdv = predicateOrAdv.Or(a => a.Times != ctime);
                            //                    break;
                            //                case "gt"://大于
                            //                    predicateOrAdv = predicateOrAdv.Or(a => a.Times > ctime);
                            //                    break;
                            //                case "ge"://大于等于
                            //                    predicateOrAdv = predicateOrAdv.Or(a => a.Times >= ctime);
                            //                    break;
                            //                case "lt"://小于
                            //                    predicateOrAdv = predicateOrAdv.Or(a => a.Times < ctime);
                            //                    break;
                            //                case "le"://小于等于
                            //                    predicateOrAdv = predicateOrAdv.Or(a => a.Times <= ctime);
                            //                    break;
                            //                default:
                            //                    predicateOrAdv = predicateOrAdv.Or(a => false);
                            //                    break;
                            //            }
                            //        }
                            //    }
                            //    break;
                            case "Description"://描述
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Description == item.value);
                                                break;
                                            case "ne"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Description != item.value);
                                                break;
                                            case "contain"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Description.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Description.Contains(item.value));
                                                break;
                                        }

                                    }
                                }
                                break;
                            case "DesigneeName"://指派给
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(item.value)).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq":
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Designee == userIds);
                                                break;
                                            case "ne"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Designee != userIds);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "StalkerName"://记录人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(item.value)).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Stalker == userIds);
                                                break;
                                            case "ne"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Stalker != userIds);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CreateName"://创建人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(item.value)).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateUserId == userIds);
                                                break;
                                            case "ne"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateUserId != userIds);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ModifyName"://记录人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(item.value)).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyUserId == userIds);
                                                break;
                                            case "ne"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyUserId != userIds);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Wancheng"://完成
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Wancheng ==Convert.ToInt32(item.value));
                                                break;
                                            case "ne":
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Wancheng != Convert.ToInt32(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "JhCreateDateTime"://计划开始时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.JhCreateDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.JhCreateDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.JhCreateDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.JhCreateDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.JhCreateDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.JhCreateDateTime <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "JhCompleteDateTime"://计划完成时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.JhCompleteDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.JhCompleteDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.JhCompleteDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.JhCompleteDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.JhCompleteDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.JhCompleteDateTime <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "SjCreateDateTime"://计划开始时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.SjCreateDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.SjCreateDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.SjCreateDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.SjCreateDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.SjCreateDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.SjCreateDateTime <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "SjCompleteDateTime"://计划开始时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.SjCompleteDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.SjCompleteDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.SjCompleteDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.SjCompleteDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.SjCompleteDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.SjCompleteDateTime <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CreateDateTime"://计划开始时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ModifyDateTime"://计划开始时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyDateTime <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "State"://状态
                                {
                                    foreach (var item in adv.children)
                                    {
                                        int st = 0;
                                        switch (item.value)
                                        {
                                            case"未完成":
                                                st = 0;
                                                break;
                                            case "已完成":
                                                st = 1;
                                                break;
                                            default:
                                                break;
                                        }
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.State == st);
                                                break;
                                            case "ne"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.State != st);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }

                                }
                                break;
        
                        }
                        if (pageParam.filterSos.Contains("and"))
                        {
                            predicateAnd = predicateAnd.And(predicateOrAdv);
                        }
                        else
                        {
                            predicateAnd = predicateAnd.Or(predicateOrAdv);
                        }
                    }
                    #endregion
                }
            }
            return predicateAnd;
        }



        public static Expression<Func<Model.Models.ScheduleDetail, bool>> GetJBAdvQueryScheduleD(
         PageparamInfo pageParam
       , IUserInforService _IUserInforService, IDataDictionaryService _IDataDictionaryService)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.ScheduleDetail>(); 
            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//询价基本查询
                var advquerys = AdvQueryHelper.GetAdvJbcxQuery(pageParam.filterSos);
                var predicateAndAdv = PredicateBuilder.True<Model.Models.ScheduleDetail>();
                var predicateOrAdv = PredicateBuilder.False<Model.Models.ScheduleDetail>();
                foreach (var adv in advquerys)
                {
                    #region and
                    if (adv.prefix == "and")
                    {
                        switch (adv.field)
                        {
                            case "ScheduleName"://进度名称
                                {

                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ScheduleName == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ScheduleName != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.ScheduleName.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.ScheduleName.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "ScheduleSerName"://进度名称
                                {

                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ScheduleSerNavigation.ScheduleName == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ScheduleSerNavigation.ScheduleName != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.ScheduleSerNavigation.ScheduleName.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.ScheduleSerNavigation.ScheduleName.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "Description"://描述
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Description == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Description != item.value);
                                                break;
                                            case "contain"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.Description.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Description.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Pdescription"://评定描述
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Pdescription == item.value);
                                                break;
                                            case "ne"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.Pdescription != item.value);
                                                break;
                                            case "contain"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.Pdescription.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Pdescription.Contains(item.value));
                                                break;
                                        }

                                    }
                                }
                                break;
                            case "CreateName"://指派给
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(item.value)).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq":
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateUserId == userIds);
                                                break;
                                            case "ne"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.CreateUserId != userIds);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ModifyName"://记录人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(item.value)).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ModifyUserId == userIds);
                                                break;
                                            case "ne"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.ModifyUserId != userIds);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Wancheng"://完成
                                {
                                    foreach (var item in adv.children)
                                    {
                                        int ctime = 0;
                                        int.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Wancheng == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Wancheng != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Wancheng > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Wancheng >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Wancheng < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Wancheng <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "PddateTime"://判定时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.PddateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.PddateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.PddateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.PddateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.PddateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.PddateTime <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ModifyDateTime"://计划完成时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ModifyDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ModifyDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ModifyDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ModifyDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ModifyDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ModifyDateTime <= ctime);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                        
                            case "State"://状态
                                {
                                    foreach (var item in adv.children)
                                    {
                                        int st = 0;
                                        switch (item.value)
                                        {
                                            case "未完成":
                                                st = 0;
                                                break;
                                            case "已完成":
                                                st = 1;
                                                break;
                                            default:
                                                break;
                                        }
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.State == st);
                                                break;
                                            case "ne"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.State != st);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }

                                }
                                break;
                        }
                        predicateAnd = predicateAnd.And(predicateAndAdv);
                    }
                    #endregion and
                    #region Or
                    else if (adv.prefix == "or")
                    {
                        switch (adv.field)
                        {
                            case "ScheduleName"://进度名称
                                {

                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ScheduleName == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ScheduleName != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ScheduleName.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ScheduleName.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "ScheduleSerName"://进度名称
                                {

                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ScheduleSerNavigation.ScheduleName == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ScheduleSerNavigation.ScheduleName != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ScheduleSerNavigation.ScheduleName.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ScheduleSerNavigation.ScheduleName.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "Description"://描述
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Description == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Description != item.value);
                                                break;
                                            case "contain"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Description.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Description.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Pdescription"://评定描述
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Pdescription == item.value);
                                                break;
                                            case "ne"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Pdescription != item.value);
                                                break;
                                            case "contain"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Pdescription.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Pdescription.Contains(item.value));
                                                break;
                                        }

                                    }
                                }
                                break;
                            case "CreateName"://指派给
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(item.value)).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq":
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateUserId == userIds);
                                                break;
                                            case "ne"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateUserId != userIds);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ModifyName"://记录人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(item.value)).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyUserId == userIds);
                                                break;
                                            case "ne"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyUserId != userIds);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Wancheng"://完成
                                {
                                    foreach (var item in adv.children)
                                    {
                                        int ctime = 0;
                                        int.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Wancheng == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Wancheng != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Wancheng > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Wancheng >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Wancheng < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Wancheng <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "PddateTime"://判定时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PddateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PddateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PddateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PddateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PddateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.PddateTime <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ModifyDateTime"://计划完成时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyDateTime <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;

                            case "State"://状态
                                {
                                    foreach (var item in adv.children)
                                    {
                                        int st = 0;
                                        switch (item.value)
                                        {
                                            case "未完成":
                                                st = 0;
                                                break;
                                            case "已完成":
                                                st = 1;
                                                break;
                                            default:
                                                break;
                                        }
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.State == st);
                                                break;
                                            case "ne"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.State != st);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }

                                }
                                break;
                        }
                        if (pageParam.filterSos.Contains("and"))
                        {
                            predicateAnd = predicateAnd.And(predicateOrAdv);
                        }
                        else
                        {
                            predicateAnd = predicateAnd.Or(predicateOrAdv);
                        }
                    }
                    #endregion
                }
            }
            return predicateAnd;
        }
        #endregion

        #region 我的工作台
        public static Expression<Func<Model.Models.ScheduleList, bool>> GetytAdvQueryWdgzt(
        PageparamInfo pageParam
      , IUserInforService _IUserInforService, IDataDictionaryService _IDataDictionaryService)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.ScheduleList>();
            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//高级查询
                var advquerys = AdvQueryHelper.GetAdvJbcxQuery(pageParam.filterSos);
                var predicateAndAdv = PredicateBuilder.True<Model.Models.ScheduleList>();
                var predicateOrAdv = PredicateBuilder.False<Model.Models.ScheduleList>();
                foreach (var adv in advquerys)
                {
                    #region and
                    if (adv.prefix == "and")
                    {
                        switch (adv.field)
                        {
                            case "Jdname"://进度名称
                                {

                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Schedule.ScheduleName == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Schedule.ScheduleName != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Schedule.ScheduleName.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Schedule.ScheduleName.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "ScheduleName"://所属机构
                                {

                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ScheduleName == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ScheduleName != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.ScheduleName.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.ScheduleName.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ScheduleAttributionDic"://任务归属
                                {
                                    foreach (var item in adv.children)
                                    {
                                        int DataId = _IDataDictionaryService.GetDataDicID(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ScheduleAttribution == DataId);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.ScheduleAttribution != DataId);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                      }
                                }
                                break;
                            case "Description"://描述
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Description == item.value);
                                                break;
                                            case "ne"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.Description != item.value);
                                                break;
                                            case "contain"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.Description.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Description.Contains(item.value));
                                                break;
                                        }

                                    }
                                }
                                break;
                            case "Descriptionms"://描述
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Descriptionms == item.value);
                                                break;
                                            case "ne"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.Descriptionms != item.value);
                                                break;
                                            case "contain"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.Descriptionms.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Descriptionms.Contains(item.value));
                                                break;
                                        }

                                    }
                                }
                                break;
                            case "TixingName"://提醒人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(item.value)).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Tixing == userIds);
                                                break;
                                            case "ne"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Tixing != userIds);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "DesigneeName"://执行人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(item.value)).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Designee == userIds);
                                                break;
                                            case "ne"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Designee != userIds);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "StalkerName"://跟踪者
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(item.value)).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Stalker == userIds);
                                                break;
                                            case "ne"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Stalker != userIds);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CreateUserName"://创建人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(item.value)).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateUserId == userIds);
                                                break;
                                            case "ne"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateUserId != userIds);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ModifyUserName"://修改人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(item.value)).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyUserId == userIds);
                                                break;
                                            case "ne"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyUserId != userIds);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "JdtataTime"://进度时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Myjdtime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Myjdtime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Myjdtime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Myjdtime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Myjdtime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Myjdtime <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ModifyDateTime"://计划完成时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyDateTime <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CreateDateTime"://创建时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                        }
                        predicateAnd = predicateAnd.And(predicateAndAdv);
                    }
                    #endregion and
                    #region Or
                    else if (adv.prefix == "or")
                    {
                        switch (adv.field)
                        {
                            case "Jdname"://进度名称
                                {

                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Schedule.ScheduleName == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Schedule.ScheduleName != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Schedule.ScheduleName.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Schedule.ScheduleName.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "ScheduleName"://所属机构
                                {

                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ScheduleName == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ScheduleName != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ScheduleName.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ScheduleName.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ScheduleAttributionDic"://任务归属
                                {
                                    foreach (var item in adv.children)
                                    {
                                        int DataId = _IDataDictionaryService.GetDataDicID(item.value);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ScheduleAttribution == DataId);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ScheduleAttribution != DataId);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Description"://描述
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Description == item.value);
                                                break;
                                            case "ne"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Description != item.value);
                                                break;
                                            case "contain"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Description.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Description.Contains(item.value));
                                                break;
                                        }

                                    }
                                }
                                break;
                            case "Descriptionms"://描述
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Descriptionms == item.value);
                                                break;
                                            case "ne"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Descriptionms != item.value);
                                                break;
                                            case "contain"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Descriptionms.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Descriptionms.Contains(item.value));
                                                break;
                                        }

                                    }
                                }
                                break;
                            case "TixingName"://提醒人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(item.value)).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Tixing == userIds);
                                                break;
                                            case "ne"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Tixing != userIds);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "DesigneeName"://执行人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(item.value)).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Designee == userIds);
                                                break;
                                            case "ne"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Designee != userIds);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "StalkerName"://跟踪者
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(item.value)).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Stalker == userIds);
                                                break;
                                            case "ne"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Stalker != userIds);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CreateUserName"://创建人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(item.value)).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateUserId == userIds);
                                                break;
                                            case "ne"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateUserId != userIds);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ModifyUserName"://修改人
                                {
                                    foreach (var item in adv.children)
                                    {
                                        var userIds = _IUserInforService.GetRedisUsers(a => a.Name.Contains(item.value)).FirstOrDefault().Id;
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyUserId == userIds);
                                                break;
                                            case "ne"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyUserId != userIds);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "JdtataTime"://进度时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Myjdtime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Myjdtime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Myjdtime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Myjdtime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Myjdtime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Myjdtime <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "ModifyDateTime"://计划完成时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.ModifyDateTime <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "CreateDateTime"://创建时间
                                {
                                    foreach (var item in adv.children)
                                    {
                                        DateTime ctime = DateTime.Now;
                                        DateTime.TryParse(item.value, out ctime);
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime == ctime);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime != ctime);
                                                break;
                                            case "gt"://大于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime > ctime);
                                                break;
                                            case "ge"://大于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime >= ctime);
                                                break;
                                            case "lt"://小于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime < ctime);
                                                break;
                                            case "le"://小于等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.CreateDateTime <= ctime);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                        }
                        if (pageParam.filterSos.Contains("and"))
                        {
                            predicateAndAdv = predicateAndAdv.Or(predicateOrAdv);
                        }
                        else
                        {
                            predicateOrAdv = predicateOrAdv.And(predicateAndAdv);
                        }
                    }
                    #endregion
                }
            }
            return predicateAnd;
        }
        #endregion

        #region 用户

        public static Expression<Func<Model.Models.UserInfor, bool>> GetytAdvQueryUser(
        string filterSos
      , IUserInforService _IUserInforService)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.UserInfor>();
            if (!string.IsNullOrEmpty(filterSos))
            {//高级查询
                var advquerys = AdvQueryHelper.GetAdvJbcxQuery(filterSos);
                var predicateAndAdv = PredicateBuilder.True<Model.Models.UserInfor>();
                var predicateOrAdv = PredicateBuilder.False<Model.Models.UserInfor>();
                foreach (var adv in advquerys)
                {
                    #region and
                    if (adv.prefix == "and")
                    {
                        switch (adv.field)
                        {
                            case "Name"://用户名称
                                {

                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Name == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Name != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Name.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Name.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "DeptName"://所属机构
                                {

                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Department.Name == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Department.Name != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateAndAdv = predicateAndAdv.And(a => a.Department.Name.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Department.Name.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "DisplyName"://显示名称
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.DisplyName == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.DisplyName != item.value);
                                                break;
                                            case "contain"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.DisplyName.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.DisplyName.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "SexDic"://显示名称
                                {
                                    foreach (var item in adv.children)
                                    {
                                        int czt = 0;
                                        if (item.value == "男")
                                        {
                                            czt = 1;
                                        }
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sex == czt);
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Sex != czt);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a =>false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Age":
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Age ==Convert.ToInt32(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Age != Convert.ToInt32(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Tel"://显示名称
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Tel == item.value);
                                                break;
                                            case "ne"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.Tel != item.value);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Mobile"://手机
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Mobile == item.value);
                                                break;
                                            case "ne"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.Mobile != item.value);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }

                                }
                                break;
                            case "Email"://邮件
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.Email == item.value);
                                                break;
                                            case "ne"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.Email != item.value);
                                                break;
                                            case "contain"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.Email.Contains(item.value));
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => a.Email.Contains(item.value));
                                                break;
                                        }
                                    }

                                }
                                break;
                            case "Ustart"://状态
                                {
                                    foreach (var item in adv.children)
                                    {
                                        int czt = 0;
                                        if (item.value=="启用")
                                        {
                                            czt = 1;
                                        }

                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateAndAdv = predicateAndAdv.And(a => a.State == czt);
                                                break;
                                            case "ne"://
                                                predicateAndAdv = predicateAndAdv.And(a => a.State != czt);
                                                break;
                                            default:
                                                predicateAndAdv = predicateAndAdv.And(a => false);
                                                break;
                                        }
                                    }

                                }
                                break;
                        }
                        predicateAnd = predicateAnd.And(predicateAndAdv);
                    }
                    #endregion and
                    #region Or
                    else if (adv.prefix == "or")
                    {
                        switch (adv.field)
                        {
                            case "Name"://用户名称
                                {

                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Name == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Name != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Name.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Name.Contains(item.value));
                                                break;

                                        }
                                    }
                                }
                                break;
                            case "DeptName"://所属机构
                                {

                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Department.Name == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Department.Name != item.value);
                                                break;
                                            case "contain"://包含
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Department.Name.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Department.Name.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "DisplyName"://显示名称
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.DisplyName == item.value);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.DisplyName != item.value);
                                                break;
                                            case "contain"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.DisplyName.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.DisplyName.Contains(item.value));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "SexDic"://显示名称
                                {
                                    foreach (var item in adv.children)
                                    {
                                        int czt = 0;
                                        if (item.value == "男")
                                        {
                                            czt = 1;
                                        }
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sex == czt);
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Sex != czt);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Age":
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Age == Convert.ToInt32(item.value));
                                                break;
                                            case "ne"://不等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Age != Convert.ToInt32(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Tel"://显示名称
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Tel == item.value);
                                                break;
                                            case "ne"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Tel != item.value);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "Mobile"://手机
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Mobile == item.value);
                                                break;
                                            case "ne"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Mobile != item.value);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }

                                }
                                break;
                            case "Email"://邮件
                                {
                                    foreach (var item in adv.children)
                                    {
                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Email == item.value);
                                                break;
                                            case "ne"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Email != item.value);
                                                break;
                                            case "contain"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Email.Contains(item.value));
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => a.Email.Contains(item.value));
                                                break;
                                        }
                                    }

                                }
                                break;
                            case "Ustart"://状态
                                {
                                    foreach (var item in adv.children)
                                    {
                                        int czt = 0;
                                        if (item.value == "启用")
                                        {
                                            czt = 1;
                                        }

                                        switch (item.type)
                                        {
                                            case "eq"://等于
                                                predicateOrAdv = predicateOrAdv.Or(a => a.State == czt);
                                                break;
                                            case "ne"://
                                                predicateOrAdv = predicateOrAdv.Or(a => a.State != czt);
                                                break;
                                            default:
                                                predicateOrAdv = predicateOrAdv.Or(a => false);
                                                break;
                                        }
                                    }

                                }
                                break;
                        }
                        if (filterSos.Contains("and"))
                        {
                            predicateAndAdv = predicateAndAdv.Or(predicateOrAdv);
                        }
                        else
                        {
                            predicateOrAdv = predicateOrAdv.And(predicateAndAdv);
                        }
                    }
                    #endregion
                }
            }
            return predicateAnd;
        }

        #endregion
    }

}
