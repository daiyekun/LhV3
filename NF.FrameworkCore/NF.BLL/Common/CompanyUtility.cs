using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.BLL.Common
{
   public class CompanyUtility
    {
        /// <summary>
        /// 类别
        /// </summary>
        /// <param name="classId">类别ID</param>
        /// <param name="comptype">0：客户，1：供应商，2：其他对方</param>
        /// <returns></returns>
        public static string CompanyTypeClass(int? classId, int comptype)
        {
            if (classId == null)
            {
                return "";
            }
            else
            {
                DataDictionaryEnum typeEnum = DataDictionaryEnum.customerType;
                switch (comptype)
                {
                    case 0:
                        typeEnum = DataDictionaryEnum.customerType;
                        break;
                    case 1:
                        typeEnum = DataDictionaryEnum.suppliersType;
                        break;
                    case 2:
                        typeEnum = DataDictionaryEnum.otherType;
                        break;

                }

                return DataDicUtility.GetDicValueToRedis(classId, typeEnum);
            }


        }
    }
}
