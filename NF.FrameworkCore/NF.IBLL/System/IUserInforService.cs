using NF.Common.Models;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
   public partial interface IUserInforService : IBaseService<UserInfor>
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="LoginName">登陆名称</param>
        /// <param name="LoginPwd">登陆密码</param>
        /// <returns></returns>
        RequstResult CheckLogin(string LoginName, string LoginPwd);
        /// <summary>
        /// 查询用户信息列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<UserInforDTO> GetList(PageInfo<UserInfor> pageInfo, Expression<Func<UserInfor, bool>> whereLambda);
        LayPageInfo<UserInforDTO> GetList1(PageInfo<UserInfor> pageInfo, Expression<Func<UserInfor, bool>> whereLambda);
        LayPageInfo<UserInforDTO> GetList2(PageInfo<UserInfor> pageInfo, Expression<Func<UserInfor, bool>> whereLambda,string  name);
      
        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="info">修改字段信息</param>
        /// <returns>受影响行数</returns>
        int UpdateField(UpdateFieldInfo info);
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="Ids">修改数据Ids</param>
        /// <returns>受影响行数</returns>
        int Delete(string Ids);

        /// <summary>
        /// 保存系统用户
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns>返回主信息</returns>
        UserInfor SaveInfo(UserInfor userInfo);
        /// <summary>
        /// 显示查看基本信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        UserInforDTO ShowView(int Id);
        /// <summary>
        /// 判断输入值是否存在
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="inputValue">输入值</param>
        /// <param name="Id">修改时的ID</param>
        /// <returns></returns>
         bool CheckFieldValExist(UpdateFieldInfo updateField);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oldPwd">旧密码</param>
        /// <param name="pwd">新密码</param>
        /// <param name="Id">当前ID</param>
        /// <returns>true:成功</returns>
        RequstResult UpdatePwd(string oldPwd, string pwd, int Id);
        /// <summary>
        /// 设置用户基本信息
        /// </summary>
        /// <param name="info">用户基本信息</param>
        /// <returns>当前用户对象</returns>
        UserInfor SetUserInfo(UserInfor info);
        /// <summary>
        /// 根据条件查询数据库返回指定Redis用户对象
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>Redis存储对象</returns>
        IList<RedisUser> GetRedisUsers(Expression<Func<UserInfor, bool>> whereLambda);
        /// <summary>
        /// 存储Redis
        /// </summary>
        void SetRedis();
        /// <summary>
        /// word登录插件
        /// </summary>
        /// <param name="uid">当前用户</param>
        /// <returns></returns>
        SignInFromWordUser SignInFromWord(int uid);
        /// <summary>
        /// 获取用户是当前系统中第几个活动用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        int GetUserTurn(string userName);
        /// <summary>
        /// 获取移动用户是当前系统中第几个活动用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        int GetMobileUserTurn(string userName);

        /// <summary>
        /// 获取平均并发用户数
        /// </summary>
        /// <returns></returns>
        int GetBinFaYongFuSHu();



        int ADDDzqz(int id);
        int? DZQZ(int id);

       IList<Country> Gj();
        IList<Province> Sf();
        IList<City> sj();


    }
}
