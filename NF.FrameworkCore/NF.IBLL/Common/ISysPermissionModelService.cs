using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
    /// <summary>
    /// 自定义系统权限类接口
    /// </summary>
   public partial interface ISysPermissionModelService: IBaseService<SysPermissionModel>
    {
        /// <summary>
        /// 客户列表权限表达式
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <param name="deptId">当前用户所属部门ID</param>
        /// <param name="funCode">功能状态码</param>
        /// <returns>客户权限表达式</returns>
        Expression<Func<Company, bool>> GetCmpListPermissionExpression(string funCode,int userId, int deptId = 0);
        /// <summary>
        /// 判断当前用户是否有新建客户的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <returns>True：有权限新建，False：没权限</returns>
        bool GetCmpAddPermission(string funCode,int userId);
        /// <summary>
        /// 判断当前用户是否有修改客户的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="deptId">当前部门ID</param>
        /// <param name="updateObjId">当前修改的ID</param>
        /// <param name="funCode">功能码</param>
        /// <returns>True：有权限新建，False：没权限</returns>
        PermissionDicEnum GetCmpUpdatePermission(string funCode,int userId, int deptId, int updateObjId);


        #region 状态修改
        /// <summary>
        /// 客户状态修改
        /// </summary>
        /// <param name="funCode"></param>
        /// <param name="userId"></param>
        /// <param name="deptId"></param>
        /// <param name="detailObjId"></param>
        /// <returns></returns>
        PermissionDicEnum UpdateCmp(string funCode, int userId, int deptId, int detailObjId);

        /// <summary>
        /// 项目状态修改
        /// </summary>
        /// <param name="funCode"></param>
        /// <param name="userId"></param>
        /// <param name="deptId"></param>
        /// <param name="detailObjId"></param>
        /// <returns></returns>
        PermissionDicEnum UpdateProj(string funCode, int userId, int deptId, int detailObjId);
        /// <summary>
        /// 合同状态修改
        /// </summary>
        /// <param name="funCode"></param>
        /// <param name="userId"></param>
        /// <param name="deptId"></param>
        /// <param name="detailObjId"></param>
        /// <returns></returns>
        PermissionDicEnum UpdateContract(string funCode, int userId, int deptId, int detailObjId);
        /// <summary>
        /// 招标状态修改
        /// </summary>
        /// <param name="funCode"></param>
        /// <param name="userId"></param>
        /// <param name="deptId"></param>
        /// <param name="detailObjId"></param>
        /// <returns></returns>
        PermissionDicEnum UpdateZhao(string funCode, int userId, int deptId, int detailObjId);
        /// <summary>
        /// 询价状态修改
        /// </summary>
        /// <param name="funCode"></param>
        /// <param name="userId"></param>
        /// <param name="deptId"></param>
        /// <param name="detailObjId"></param>
        /// <returns></returns>
        PermissionDicEnum UpdateXunjia(string funCode, int userId, int deptId, int detailObjId);
        /// <summary>
        /// 洽谈状态修改
        /// </summary>
        /// <param name="funCode"></param>
        /// <param name="userId"></param>
        /// <param name="deptId"></param>
        /// <param name="detailObjId"></param>
        /// <returns></returns>
        PermissionDicEnum UpdateQia(string funCode, int userId, int deptId, int detailObjId);
        #endregion

        /// <summary>
        /// 查看客户详情权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="deptId">用户部门ID</param>
        /// <param name="detailObjId">数据对象ID</param>
        /// <param name="funCode">功能码</param>
        /// <returns>True:有权限，否则无权限</returns>
        bool GetCmpDetailPermission(string funCode,int userId, int deptId, int detailObjId);
  
        /// <summary>
        /// 获取删除客户权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="deptId">用户所属机构ID</param>
        /// <param name="listdelIds">删除数据ID</param>
        /// <param name="funCode">功能状态码</param>
        /// <returns>权限对象</returns>
        PermissionDataInfo GetCmpDeletePermission(string funCode,int userId, int deptId, IList<int> listdelIds);
        /// <summary>
        /// 询价删除
        /// </summary>
        /// <param name="funCode"></param>
        /// <param name="userId"></param>
        /// <param name="deptId"></param>
        /// <param name="listdelIds"></param>
        /// <returns> 权限对象</returns>
        PermissionDataInfo GetInquiryDeletePermission(string funCode, int userId, int deptId, IList<int> listdelIds);
        /// <summary>
        /// 约谈删除
        /// </summary>
        /// <param name="funCode"></param>
        /// <param name="userId"></param>
        /// <param name="deptId"></param>
        /// <param name="listdelIds"></param>
        /// <returns></returns>
        PermissionDataInfo GetQuestioningDeletePermission(string funCode, int userId, int deptId, IList<int> listdelIds);
        /// <summary>
        /// 修改客户次要字段权限
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <param name="deptId">当前用户部门ID</param>
        /// <param name="updateObjId">当前数据ID</param>
        /// <param name="funCode">功能码</param>
        /// <returns>PermissionDicEnum</returns>
        PermissionDicEnum GetCmpSecFieldUpdatePermission(string funCode,int userId, int deptId, int updateObjId);
        /// <summary>
        /// 查看页面按钮权限，判断是否显示隐藏
        /// </summary>
        /// <returns>{Change:1,Update:1,Delete:0}=>1：标识允许，0：标识不允许</returns>
        BtnPremissionInfo GetCmpBtnPermission(int Id);
        /// <summary>
        /// 查看页面按钮权限，判断是否显示隐藏
        /// </summary>
        /// <returns>{Change:1,Update:1,Delete:0}=>1：标识允许，0：标识不允许</returns>
        BtnPremissionInfo GetscheBtnPermission(int Id);
        /// <summary>
        /// 修改项目次要字段
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <param name="deptId">当前用户部门ID</param>
        /// <param name="updateObjId">当前数据ID</param>
        /// <param name="funCode">功能码</param>
        /// <returns>PermissionDicEnum</returns>
        PermissionDicEnum GetProjSecFieldUpdatePermission(string funCode, int userId, int deptId, int updateObjId);
        /// <summary>
        /// 项目列表权限表达式
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <param name="deptId">当前用户所属部门ID</param>
        /// <param name="funCode">功能状态码</param>
        /// <returns>项目权限表达式</returns>
        Expression<Func<ProjectManager, bool>> GetProjectListPermissionExpression(string funCode, int userId, int deptId = 0);
        /// <summary>
        /// 判断当前用户是否有新建项目的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <returns>True：有权限新建，False：没权限</returns>
        bool GetProjectAddPermission(string funCode, int userId);
        /// <summary>
        /// 判断当前用户是否有修改项目权限的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="deptId">当前部门ID</param>
        /// <param name="updateObjId">当前修改的ID</param>
        /// <param name="funCode">功能码</param>
        /// <returns>True：有权限新建，False：没权限</returns>
        PermissionDicEnum GetProjectUpdatePermission(string funCode, int userId, int deptId, int updateObjId);

        /// <summary>
        /// 查看项目详情权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="deptId">用户部门ID</param>
        /// <param name="detailObjId">数据对象ID</param>
        /// <param name="funCode">功能码</param>
        /// <returns>True:有权限，否则无权限</returns>
        bool GetProjectDetailPermission(string funCode, int userId, int deptId, int detailObjId);
        /// <summary>
        /// 获取删除项目权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="deptId">用户所属机构ID</param>
        /// <param name="listdelIds">删除数据ID</param>
        /// <param name="funCode">功能状态码</param>
        /// <returns>权限对象</returns>
        PermissionDataInfo GetProjectDeletePermission(string funCode, int userId, int deptId, IList<int> listdelIds);
        /// <summary>
        /// 查看页面按钮权限，判断是否显示隐藏
        /// </summary>
        /// <returns>{Change:1,Update:1,Delete:0}=>1：标识允许，0：标识不允许</returns>
        BtnPremissionInfo GetProjBtnPermission(int Id);
        /// <summary>
        /// 合同列表权限表达式
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <param name="deptId">当前用户所属部门ID</param>
        /// <param name="funCode">功能状态码</param>
        /// <returns>合同权限表达式</returns>
        Expression<Func<ContractInfo, bool>> GetContractListPermissionExpression(string funCode, int userId, int deptId = 0);
        /// <summary>
        /// 招标列表权限表达式
        /// </summary>
        /// <param name="funCode"></param>
        /// <param name="userId"></param>
        /// <param name="deptId"></param>
        /// <returns></returns>
        //Expression<Func<TenderInfor, bool>> GetTenderInListPermissionExpression(string funCode, int userId, int deptId = 0);
        /// <summary>
        /// 获取删除删除权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="deptId">用户所属机构ID</param>
        /// <param name="listdelIds">删除数据ID</param>
        /// <param name="funCode">功能状态码</param>
        /// <returns>权限对象</returns>
        PermissionDataInfo GetContractDeletePermission(string funCode, int userId, int deptId, IList<int> listdelIds);
        /// <summary>
        /// 修改合同权限
        /// </summary>
        /// <param name="funcCode">功能标识</param>
        /// <param name="userId">当前用户ID</param>
        /// <param name="deptId">部门ID</param>
        /// <param name="updateObjId">修改数据ID</param>
        /// <returns></returns>
        PermissionDicEnum GetContractUpdatePermission(string funcCode, int userId, int deptId, int updateObjId);


        /// <summary>
        /// 判断当前用户是否有修改合同次要字段的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="updateObjId">修改数据的ID</param>
        /// <returns>PermissionDicEnum</returns>
        PermissionDicEnum GetContractSecFieldUpdatePermission(string funcCode, int userId, int deptId, int updateObjId);
        /// <summary>
        /// 查看页面按钮权限，判断是否显示隐藏
        /// </summary>
        /// <returns>{Change:1,Update:1,Delete:0}=>1：标识允许，0：标识不允许</returns>
        BtnPremissionInfo GetContractBtnPermission(int Id);
        /// <summary>
        /// 判断当前用户是否有新建项目的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <returns>True：有权限新建，False：没权限</returns>
        bool GetContractAddPermission(string funCode, int userId);
        /// <summary>
        /// 查看合同详情权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="deptId">用户部门ID</param>
        /// <param name="detailObjId">数据对象ID</param>
        /// <param name="funCode">功能码</param>
        /// <returns>True:有权限，否则无权限</returns>
        bool GetContractDetailPermission(string funCode, int userId, int deptId, int detailObjId);
        /// <summary>
        /// 计划资金权限
        /// 计划资金权限是根据合同查看权限
        /// </summary>
        /// <param name="funCode">功能码</param>
        /// <param name="userId">当前登录用户ID</param>
        /// <param name="deptId">当前所属部门ID</param>
        /// <returns>返回计划资金权限表达式</returns>
        Expression<Func<ContPlanFinance, bool>> GetFinanceListPermissionExpression(string funCode, int userId, int deptId);
        /// <summary>
        /// 合同文本大列表
        /// </summary>
        /// <param name="funCode">功能标识</param>
        /// <param name="userId">用户ID</param>
        /// <param name="deptId">用户所属部门ID</param>
        /// <returns>合同文本权限表达式</returns>
        Expression<Func<ContText, bool>> GetContractTextListPermissionExpression(string funCode, int userId, int deptId);
        /// <summary>
        /// 合同文本盖章权限
        /// </summary>
        /// <param name="funCode">权限标识</param>
        /// <param name="userId">用户ID</param>
        /// <returns>bool:true/false</returns>
        bool ContTextSealPermission(string funCode, int userId);
        /// <summary>
        /// 归档权限
        /// </summary>
        /// <param name="funCode">权限标识</param>
        /// <param name="userId">用户ID</param>
        /// <returns>bool:true/false</returns>
        bool ContTextArchivePermission(string funCode, int userId);

        /// <summary>
        /// 发票权限
        /// 发票权限是根据合同查看权限
        /// </summary>
        /// <param name="funCode">功能码</param>
        /// <param name="userId">当前登录用户ID</param>
        /// <param name="deptId">当前所属部门ID</param>
        /// <returns>返回发票权限表达式</returns>
        Expression<Func<ContInvoice, bool>> GetInvoiceListPermissionExpression(string funCode, int userId, int deptId);

        /// <summary>
        /// 建立发票权限
        /// </summary>
        /// <param name="funCode">功能点</param>
        /// <param name="userId">当前用户</param>
        /// <param name="contId">合同ID</param>
        /// <param name="deptId">用户当前部门ID</param>
        /// <returns></returns>
        bool GetAddUpdateInvoicePermission(string funCode, int userId,int deptId,int contId);
        /// <summary>
        /// 删除发票权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="deptId">用户所属机构ID</param>
        /// <param name="listdelIds">删除数据ID</param>
        /// <param name="funCode">功能状态码</param>
        /// <returns>权限对象</returns>
        PermissionDataInfo GetDeleteInvoicePermission(string funCode, int userId, int deptId, IList<int> listdelIds);
        /// <summary>
        /// 确认和打回发票权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="deptId">用户所属机构ID</param>
        /// <param name="listdelIds">删除数据ID</param>
        /// <param name="funCode">功能状态码</param>
        /// <returns>True/False</returns>
        bool GetConfirmBackInvoicePermission(string funcCode, int userId, int deptId, int contId);
        /// <summary>
        /// 实际资金权限
        /// 实际资金权限是根据合同查看权限
        /// </summary>
        /// <param name="funCode">功能码</param>
        /// <param name="userId">当前登录用户ID</param>
        /// <param name="deptId">当前所属部门ID</param>
        /// <returns>返回实际资金权限表达式</returns>
        Expression<Func<ContActualFinance, bool>> GetActFinanceListPermissionExpression(string funCode, int userId, int deptId);
        /// <summary>
        /// 建立/修改实际资金权限
        /// </summary>
        /// <param name="funCode">功能点</param>
        /// <param name="userId">当前用户</param>
        /// <param name="contId">合同ID</param>
        /// <param name="deptId">用户当前部门ID</param>
        /// <param name="actId">实际资金ID</param>
        /// <returns>建立/修改实际资金权限</returns>
        bool GetAddUpdateActFinancePermission(string funCode, int userId, int deptId, int contId,int actId);

        /// <summary>
        /// 删除发票权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="deptId">用户所属机构ID</param>
        /// <param name="listdelIds">删除数据ID</param>
        /// <param name="funCode">功能状态码</param>
        /// <returns>实际资金权限对象</returns>
        PermissionDataInfo GetDeleteActFinancePermission(string funCode, int userId, int deptId, IList<int> listdelIds);
        /// <summary>
        /// 确认和打回实际资金权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="deptId">用户所属机构ID</param>
        /// <param name="listdelIds">删除数据ID</param>
        /// <param name="funCode">功能状态码</param>
        /// <returns>True/False</returns>
        bool GetConfirmBackActFinancePermission(string funcCode, int userId, int deptId, int contId);

        /// <summary>
        /// 判断用户是否有新建和修改单品的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <returns>True：有权限新建，False：没权限</returns>
        bool GetBcInstanceAddOrUpdatePermission(string funCode, int userId);
        /// <summary>
        /// 单品管理列表权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <returns>True：有权限新建，False：没权限</returns>
        bool GetBcInstanceListPermission(string funCode, int userId);
        /// <summary>
        /// 单品管理查看详情权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <returns>True：有权限新建，False：没权限</returns>
        bool GetBcInstanceDetailPermission(string funCode, int userId);

        /// <summary>
        /// 标的权限
        /// </summary>
        /// <param name="funCode">功能码</param>
        /// <param name="userId">当前登录用户ID</param>
        /// <param name="deptId">当前所属部门ID</param>
        /// <returns>标的权限表达式</returns>
        Expression<Func<ContSubjectMatter, bool>> GetListSubjectMatterPermissionExpression(string funCode, int userId, int deptId);
        /// <summary>
        /// 标的明细权限
        /// </summary>
        /// <param name="funCode">功能码</param>
        /// <param name="userId">当前登录用户ID</param>
        /// <param name="deptId">当前所属部门ID</param>
        /// <returns>标的明细权限表达式</returns>
        Expression<Func<ContSubDe, bool>> GetListSubDesPermissionExpression(string funCode, int userId, int deptId);

        /// <summary>
        /// 询价列表查询
        /// </summary>
        /// <param name="funCode"></param>
        /// <param name="userId"></param>
        /// <param name="deptId"></param>
        /// <returns></returns>
   //    Expression<Func<Inquiry, bool>> GetInquiryListPermissionExpression(string funCode, int userId, int deptId = 0);
        /// <summary>
        /// 询价删除权限
        /// </summary>
        /// <param name="funCode"></param>
        /// <param name="userId"></param>
        /// <param name="deptId"></param>
        /// <param name="listdelIds"></param>
        /// <returns></returns>
      //  PermissionDataInfo GetInquiryDeletePermission(string funCode, int userId, int deptId, IList<int> listdelIds);

            /// <summary>
            /// 询价修改
            /// </summary>
            /// <param name="funcCode"></param>
            /// <param name="userId"></param>
            /// <param name="deptId"></param>
            /// <param name="updateObjId"></param>
            /// <returns></returns>
        PermissionDicEnum GetInquiryUpdatePermission(string funcCode, int userId, int deptId, int updateObjId);
        /// <summary>
        /// 约谈修改
        /// </summary>
        /// <param name="funcCode"></param>
        /// <param name="userId"></param>
        /// <param name="deptId"></param>
        /// <param name="updateObjId"></param>
        /// <returns></returns>
        PermissionDicEnum GetQuestioningUpdatePermission(string funcCode, int userId, int deptId, int updateObjId);

        #region  招标权限
        bool GetaddTenderAddPermission(string funCode, int userId);
        #endregion
        #region  询价权限
        bool GetInquiryAddPermission(string funCode, int userId);
        #endregion
        #region  洽谈权限
        bool GetQuestioningAddPermission(string funCode, int userId);
        #endregion

        bool GetJfsbAddPermission(string funcCode, int userId);
        bool GetAjglAddPermission(string funcCode, int userId);
        PermissionDicEnum UpdateAjgl(string funcCode, int userId, int deptId, int updateObjId);
        PermissionDicEnum GetAjglUpdatePermission(string funcCode, int userId, int deptId, int updateObjId);
        PermissionDicEnum GetJfsbUpdatePermission(string funcCode, int userId, int deptId, int updateObjId);
        PermissionDataInfo GetAjglDeletePermission(string funcCode, int userId, int deptId, IList<int> listdelIds);
        Expression<Func<CaseManager, bool>> GetAjglListPermissionExpression(string funcCode, int userId, int deptId = 0);
        PermissionDataInfo GetJfsbDeletePermission(string funcCode, int userId, int deptId, IList<int> listdelIds);
        Expression<Func<DisputeManager, bool>> GetJfsbListPermissionExpression(string funcCode, int userId, int deptId = 0);
    }
}
