using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NF.BLL.Common
{
   public class BcCateUtility
    {
        /// <summary>
        /// 查询所属类别路径
        /// </summary>
        /// <returns></returns>
        public static string GetCatePath(int cateId, IList<BusinessCategoryDTO> listCates)
        {
            StringBuilder strb = new StringBuilder();
            if (listCates != null && listCates.Count > 0)
            {
                var info = listCates.Where(a => a.Id == cateId).FirstOrDefault();
                if (info != null)
                {
                    strb.Append($"/{info.Name}");
                    //递归上级
                    GetPName(listCates, info, strb);
                }
                return strb.ToString();
            }
            else
            {
                return "";
            }

        }
        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="listCates"></param>
        /// <param name="categoryDTO"></param>
        /// <param name="strb"></param>
        private static void GetPName(IList<BusinessCategoryDTO> listCates, BusinessCategoryDTO categoryDTO, StringBuilder strb)
        {

            if (categoryDTO != null && categoryDTO.Pid != 0)
            {
                var Pinfo = listCates.Where(a => a.Id == categoryDTO.Pid).FirstOrDefault();
                if (Pinfo != null)
                {

                    strb.Insert(0, $"/{Pinfo.Name}");
                    GetPName(listCates, Pinfo, strb);
                }
            }

        }
    }
}
