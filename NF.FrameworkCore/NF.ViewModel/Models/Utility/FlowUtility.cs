using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NF.ViewModel.Models
{
   public class FlowUtility
    {
        /// <summary>
        /// 获取审批事项
        /// </summary>
        /// <returns></returns>
        public static string GetMessionDic(int mession, int objTypeEnum)
        {
            var itemObjType = EmunUtility.GetEnumItemExAttribute(typeof(FlowObjEnums), objTypeEnum);
            var list = EmunUtility.GetAttr(itemObjType.TypeValue);
            //var listids = StringHelper.String2ArrayInt(Ids);
            var firstMs = list.Where(a => a.Value == mession).FirstOrDefault();
            return firstMs?.Desc;

        }
    }
}
