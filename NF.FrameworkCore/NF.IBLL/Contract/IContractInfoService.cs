using AutoMapper;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
   /// <summary>
   /// 合同接口
   /// </summary>
   public partial  interface IContractInfoService
    {
        /// <summary>
        /// 校验某一字段值是否已经存在
        /// </summary>
        /// <param name="fieldInfo">字段相关信息</param>
        /// <returns>True:存在/False不存在</returns>
        bool CheckInputValExist(UniqueFieldInfo fieldInfo);
        /// <summary>
        /// 保存合同
        /// </summary>
        /// <param name="contractInfo">合同信息</param>
        /// <param name="contractInfoHistory">合同历史表信息（拷贝信息）</param>
        /// <returns>Id:\Hid:字典</returns>
        Dictionary<string, int> AddSave(ContractInfo contractInfo, ContractInfoHistory contractInfoHistory, IMapper _IMapper);
        /// <summary>
        /// 合同新建页提交流程
        /// </summary>
        /// <param name="contractInfo"></param>
        /// <param name="contractInfoHistory"></param>
        /// <returns></returns>
        List<CounLc> AddSave02(ContractInfo contractInfo, ContractInfoHistory contractInfoHistory, IMapper _IMapper);
        /// <summary>
        /// 修改合同信息
        /// </summary>
        /// <param name="contractInfo">合同修改信息对象</param>
        /// <param name="contractInfoHistory">合同修改信息拷贝对象（历史）</param>
        /// <returns>Id:\Hid:字典</returns>
        Dictionary<string, int> UpdateSave(ContractInfo contractInfo, ContractInfoHistory contractInfoHistory, IMapper _IMapper);
        /// <summary>
        /// 修改页返回消息
        /// </summary>
        /// <param name="contractInfo"></param>
        /// <param name="contractInfoHistory"></param>
        /// <returns></returns>
        List<CounLc> UpdateSave01(ContractInfo contractInfo, ContractInfoHistory contractInfoHistory, IMapper _IMapper);

        /// <summary>
        /// 变更
        /// </summary>
        /// <param name="contractInfo">合同变更信息对象</param>
        /// <param name="contractInfoHistory">合同变更信息拷贝对象（历史）</param>
        /// <returns>Id:\Hid:字典</returns>
        Dictionary<string, int> ChangeSave(ContractInfo contractInfo, ContractInfoHistory contractInfoHistory, IMapper _IMapper);
        /// <summary>
        /// 查询信息列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<ContractInfoListViewDTO> GetList<s>(PageInfo<ContractInfo> pageInfo, Expression<Func<ContractInfo, bool>> whereLambda, Expression<Func<ContractInfo, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 删除信息-软删除
        /// </summary>
        /// <param name="Ids">删除数据Ids</param>
        /// <returns>受影响行数</returns>
        int Delete(string Ids,int i);

        DELETElist GetIsFpt(string Ids);
        DELETElist GetIsSjzj(string Ids);
        /// <summary>
        /// 显示查看基本信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        ContractInfoViewDTO ShowView(int Id);
        /// <summary>
        /// 复制合同信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        ContractInfoViewDTO CopShowView(int Id);
        /// <summary>
        /// 清除标签垃圾数据
        /// </summary>
        /// <param name="currUserId">当前用户ID</param>
        /// <returns></returns>
        int ClearJunkItemData(int currUserId);
        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="info">修改字段对象</param>
        /// <returns>受影响行数</returns>
        int UpdateField(UpdateFieldInfo info);

        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="fields">修改字段集合</param>
        /// <returns>受影响行数</returns>
        int UpdateField(IList<UpdateFieldInfo> fields);

        /// <summary>
        /// 查询选择合同信息列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<SelectContractInfoDTO> GetSelectList<s>(PageInfo<ContractInfo> pageInfo, Expression<Func<ContractInfo, bool>> whereLambda, Expression<Func<ContractInfo, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 根据合同对方ID查询合同列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        LayPageInfo<CompanyContract> GetContsByCompId<s>(PageInfo<ContractInfo> pageInfo, Expression<Func<ContractInfo, bool>> whereLambda, Expression<Func<ContractInfo, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 资金统计
        /// </summary>
        /// <param name="ContId">合同ID</param>
        /// <returns></returns>
        ContractStatic GetContractStatic(int ContId);

        /// <summary>
        /// 首页合同列表显示
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">where表达式</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序排序</param>
        /// <returns></returns>
        LayPageInfo<ConsoleContractInfoDTO> GetListConsoleContracts<s>(PageInfo<ContractInfo> pageInfo, Expression<Func<ContractInfo, bool>> whereLambda, Expression<Func<ContractInfo, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 合同相关进度--用于首页
        /// </summary>
        /// <returns></returns>
        ProgressInfoDTO GetProgress();
        /// <summary>
        /// 查询分包合同
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        LayPageInfo<ProjContract> GetContsByZbContId<s>(PageInfo<ContractInfo> pageInfo, Expression<Func<ContractInfo, bool>> whereLambda, Expression<Func<ContractInfo, s>> orderbyLambda, bool isAsc);


        /// <summary>
        /// 系统添加字段
        /// </summary>
        /// <param name="ContId">合同ID</param>
        /// <returns></returns>
        LayPageInfo<SysFieldDTO> ContractXtZdyzd();

      int  CyzdAdd(int id, string vals);
        int CyzdUpd(int id, string vals);

  
        Thzt TjNewtimezt();
        TjHtjenum TjNewHt();
        HtTypeTj Htlbjt();
        HtjeTj HtjeTj(int time);
        /// <summary>
        /// 合同资金往来
        /// </summary>
        /// <param name="time">年份</param>
        /// <returns></returns>
        Htwlzjtj Htwlzjtj(int time);
        HtLyTj HtLyTj();

        int Datadh(int Id);
    }
}
