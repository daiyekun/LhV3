using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 字典视图类
    /// </summary>
   public class DataDictionaryDTO
    {
        public int Id { get; set; }
        public int? Pid { get; set; }
        public string Name { get; set; }
        public int? Sort { get; set; }
        public string Dtype { get; set; }
        public string Remark { get; set; }
        public byte? FundsNature { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDatetime { get; set; }
        public int? ModifyUserId { get; set; }
        public DateTime? ModifyDatetime { get; set; }
        public int? DtypeNumber { get; set; }
        public string ShortName { get; set; }

        /// <summary>
        /// 资金性质
        /// </summary>
        public string FundDic { get; set; }
    




    }
    /// <summary>
    /// Redis存储信息
    /// </summary>
    public class RedisData : IEntityDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 字典类型表示
        /// </summary>
        public int? DtypeNumber { get; set; }


    }


    /// <summary>
    /// 数据字典类型信息
    /// </summary>
    public class DataTypesInfo
    {

        public int id { get; set; }
        public string title { get; set; }

        public static IList<DataTypesInfo> GetInfo(IList<EnumItemAttribute> listEnum)
        {
            IList<DataTypesInfo> _list = new List<DataTypesInfo>();
            foreach (var info in listEnum)
            {
                DataTypesInfo datainfo = new DataTypesInfo
                {
                    title = info.Desc,
                    id = info.Value
                };
                _list.Add(datainfo);
            }

            return _list;

        }
    }
}
