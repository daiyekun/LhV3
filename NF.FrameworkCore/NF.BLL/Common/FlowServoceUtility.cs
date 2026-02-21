using Microsoft.EntityFrameworkCore;
using NF.Model;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NF.BLL.Common
{
    /// <summary>
    /// 流程公共类
    /// </summary>
    public class FlowServoceUtility
    {
        




        #region 提交带金额节点判断

        /// <summary>
        /// 获取满足条件的节点集合IDs
        /// </summary>
        /// <param name="submitWfRes"></param>
        /// <returns></returns>
        public  static IList<string>  GetNodeStrIds(SubmitWfResParam submitWfRes, DbContext Db)
        {
            IList<string> nodeIds = new List<string>();
            var listnodeInfos = Db.Set<FlowTempNodeInfo>().AsNoTracking().Where(a => a.TempId == submitWfRes.TempId).ToList();
            foreach (var item in listnodeInfos)
            {
                if (item.Min != null && item.Max == null)
                {//最小值不为空，最大值为空
                    if ((item.IsMin ?? 0) == 0 && submitWfRes.Amount > (item.Min ?? 0))
                    {
                        nodeIds.Add(item.NodeStrId);
                    }
                    if ((item.IsMin ?? 0) == 1 && submitWfRes.Amount >= (item.Min ?? 0))
                    {
                        nodeIds.Add(item.NodeStrId);
                    }

                }
                else if (item.Min == null && item.Max != null)
                {//最大值不为空，最小值为空
                    if ((item.IsMax ?? 0) == 0 && submitWfRes.Amount < (item.Max ?? 0))
                    {
                        nodeIds.Add(item.NodeStrId);
                    }
                    if ((item.IsMax ?? 0) == 1 && submitWfRes.Amount <= (item.Max ?? 0))
                    {
                        nodeIds.Add(item.NodeStrId);
                    }
                }
                else if (item.Min != null && item.Max != null)
                {
                    if (((item.IsMin ?? 0) == 0 && (item.IsMax ?? 0) == 0)
                        && (submitWfRes.Amount > (item.Min ?? 0) && submitWfRes.Amount < (item.Max ?? 0)))
                    {
                        nodeIds.Add(item.NodeStrId);
                    }
                    else if (((item.IsMin ?? 0) == 1 && (item.IsMax ?? 0) == 0)
                        && (submitWfRes.Amount >= (item.Min ?? 0) && submitWfRes.Amount < (item.Max ?? 0))
                       )
                    {
                        nodeIds.Add(item.NodeStrId);
                    }
                    else if (((item.IsMin ?? 0) == 0 && (item.IsMax ?? 0) == 1)
                         && (submitWfRes.Amount > (item.Min ?? 0) && submitWfRes.Amount <= (item.Max ?? 0))
                        )
                    {
                        nodeIds.Add(item.NodeStrId);
                    }
                    else if (((item.IsMin ?? 0) == 1 && (item.IsMax ?? 0) == 1)
                         && (submitWfRes.Amount >= (item.Min ?? 0) && submitWfRes.Amount <= (item.Max ?? 0))
                        )
                    {
                        nodeIds.Add(item.NodeStrId);
                    }

                }




            }
            return nodeIds.Distinct().ToList();
        }
        #endregion
    }
}
