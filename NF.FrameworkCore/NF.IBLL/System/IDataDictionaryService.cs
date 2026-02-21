using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
    /// <summary>
    /// 数据字典
    /// </summary>
   public partial interface IDataDictionaryService
    {
        /// <summary>
        /// 获取字典类别
        /// </summary>
        /// <returns></returns>
        IList<EnumItemAttribute> GetListTypes();
        /// <summary>
        /// 根据数据枚举返回类别
        /// </summary>
        /// <param name="dataEnum">数据字典枚举</param>
        /// <returns>当前枚举下所有类别</returns>
        IList<DataDictionary> GetListByDataEnumType(DataDictionaryEnum dataEnum);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids">删除ID：1,2,3...</param>
        /// <returns></returns>
        int Delete(string Ids);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        LayPageInfo<DataDictionaryDTO> GetList(PageInfo<DataDictionary> pageInfo, Expression<Func<DataDictionary, bool>> whereLambda);
        /// <summary>
        /// 查看修改
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns></returns>
        DataDictionaryDTO ShowView(int Id);
        /// <summary>
        /// 存储Redis
        /// </summary>
        void SetRedis();
        /// <summary>
        /// 根据类型和关键字查下计划对象
        /// </summary>
        /// <returns></returns>
        IList<int> GetDicKes(string keyWord, DataDictionaryEnum dictionaryEnum);
        /// <summary>
        /// 根据类型和关键字查部门
        /// </summary>
        /// <returns></returns>
        IList<int> GetDepDicKes(string keyWord);
        /// <summary>
        /// 数据字典返回LayUI Tree需要数据格式
        /// </summary>
        /// <returns></returns>
        IList<TreeSelectInfo> GetTreeSelectData(DataDictionaryEnum dictionaryEnum);
        /// <summary>
        /// 根据类型获取所有数据字典
        /// </summary>
        /// <param name="dictionaryEnum">数据字典类型</param>
        /// <returns></returns>
        IList<DataDictionary> GetListAll(DataDictionaryEnum dictionaryEnum);

        int GetDataDicID(string name);
        int BzSelect(string name);
        int GetZzjg(string name);
        int GetYh(string name);
        int GetQyzt(string name);
        int GetHtlyID(string name);
    }
}
