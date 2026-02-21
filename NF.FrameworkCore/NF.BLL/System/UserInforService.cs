using NF.IBLL;
using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Linq;
using NF.Model.Models;
using NF.Common.Models;
using NF.ViewModel.Models;
using NF.ViewModel.Extend.Enums;
using NF.Common.Extend;
using NF.ViewModel.Models.Utility;

namespace NF.BLL
{
    public partial class UserInforService : BaseService<UserInfor>, IUserInforService
    {
        /// <summary>
        /// 登陆校验
        /// </summary>
        /// <param name="LoginName">登陆名称</param>
        /// <param name="LoginPwd">登陆密码</param>
        /// <returns>返回结果集</returns>
        public RequstResult CheckLogin(string LoginName, string LoginPwd)
        {

            RequstResult resultData = new RequstResult { Code = 0 };
          
            bool exist = false;
            var query = _UserInforSet.Where(a => a.Name == LoginName&&a.IsDelete!=1);
            try
            {
                exist = query.Any();
            }
            catch (Exception ex)
            {
                resultData.Tag = 4;
                resultData.Msg = ex.Message;
                return resultData;


            }
            if (!exist)
            {
                resultData.Tag = -1;
                resultData.Msg = "此用户不存在！";

            }
            else
            {
                string md5pwd = MD5Encrypt.Encrypt(LoginPwd);
                var info = query.Where(a => a.Pwd == md5pwd).FirstOrDefault();
                if (info == null)
                {
                    resultData.Tag = 0;
                    resultData.Msg = "密码错误！";

                }
                else if (info.State != 1)
                {
                    resultData.Tag = 3;
                    resultData.Msg = "此用户未启用，请联系管理员！";
                }
                else
                {
                    resultData.Tag = 1;
                    resultData.RetValue = query.FirstOrDefault();


                }



            }

            return resultData;

        }
        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <param name="pageInfo">用户对象</param>
        /// <param name="whereLambda">Where条件</param>
        /// <returns>用户列表</returns>
        public LayPageInfo<UserInforDTO> GetList(PageInfo<UserInfor> pageInfo, Expression<Func<UserInfor, bool>> whereLambda)
        {
            var tempquery = Db.Set<UserInfor>().AsNoTracking().Include(a=>a.Department).Where<UserInfor>(whereLambda.Compile());
            pageInfo.TotalCount = tempquery.Count();
            tempquery = tempquery.OrderByDescending(a => a.Id);
            tempquery = tempquery.Skip<UserInfor>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<UserInfor>(pageInfo.PageSize);
            var query = from a in tempquery
                        select new
                        {
                            Id=a.Id,
                            Name=a.Name,
                            LastName=a.LastName,
                            FirstName=a.FirstName,
                            DisplyName=a.DisplyName,
                            Sex=a.Sex,
                            Age=a.Age,
                            Tel=a.Tel,
                            Mobile=a.Mobile,
                            Email= a.Email,
                            EntryDatetime=a.EntryDatetime,
                            Ustart = a.Ustart,
                            DeptName=a.Department.Name,
                            State=a.State,
                            WxCode = a.WxCode

                        };
            var local = from a in query.AsEnumerable()
                        select new UserInforDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            LastName = a.LastName,
                            FirstName = a.FirstName,
                            DisplyName = a.DisplyName,
                            Sex = a.Sex,
                            Age = a.Age,
                            Tel = a.Tel,
                            Mobile = a.Mobile,
                            Email = a.Email,
                            EntryDatetime = a.EntryDatetime,
                            Ustart = a.Ustart,
                            DeptName=a.DeptName,
                            SexDic= EmunUtility.GetDesc(typeof(SexEnum), a.Sex),
                            //IsMainDic = EmunUtility.GetDesc(typeof(OtherDataState), a.IsMain ?? 0)
                            State = a.State??0,
                            StateDic = EmunUtility.GetDesc(typeof(UserState), a.State ?? 0),
                            WxCode = a.WxCode

                        };
            return new LayPageInfo<UserInforDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };

        }
        public LayPageInfo<UserInforDTO> GetList1(PageInfo<UserInfor> pageInfo, Expression<Func<UserInfor, bool>> whereLambda)
        {
            var tempquery = Db.Set<UserInfor>().AsNoTracking().Include(a => a.Department).Where<UserInfor>(whereLambda.Compile());
            //var Roles = Db.Set<Role>().Where(a => a.Name == "项目负责人").FirstOrDefault().Id;
            //var UserInforRoles = Db.Set<UserRole>().Where(a => a.RoleId == Roles).Select(s => s.UserId);
            // tempquery = tempquery.Where(a => UserInforRoles.Contains(a.Id));
            pageInfo.TotalCount = tempquery.Count();
            tempquery = tempquery.OrderByDescending(a => a.Id);
            tempquery = tempquery.Skip<UserInfor>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<UserInfor>(pageInfo.PageSize);
          
           // var set = UserInforRoles;

            var query = from a in tempquery
                       // where UserInforRoles.Contains(a.Id)
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            LastName = a.LastName,
                            FirstName = a.FirstName,
                            DisplyName = a.DisplyName,
                            Sex = a.Sex,
                            Age = a.Age,
                            Tel = a.Tel,
                            Mobile = a.Mobile,
                            Email = a.Email,
                            EntryDatetime = a.EntryDatetime,
                            Ustart = a.Ustart,
                            DeptName = a.Department.Name,
                            State = a.State,

                        };
            var local = from a in query.AsEnumerable()
                        select new UserInforDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            LastName = a.LastName,
                            FirstName = a.FirstName,
                            DisplyName = a.DisplyName,
                            Sex = a.Sex,
                            Age = a.Age,
                            Tel = a.Tel,
                            Mobile = a.Mobile,
                            Email = a.Email,
                            EntryDatetime = a.EntryDatetime,
                            Ustart = a.Ustart,
                            DeptName = a.DeptName,
                            SexDic = EmunUtility.GetDesc(typeof(SexEnum), a.Sex),
                            //IsMainDic = EmunUtility.GetDesc(typeof(OtherDataState), a.IsMain ?? 0)
                            State = a.State ?? 0,
                            StateDic = EmunUtility.GetDesc(typeof(UserState), a.State ?? 0)

                        };
            return new LayPageInfo<UserInforDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };

        }
        /// <summary>
        /// 选择用户(招标/询价/约谈)记录人默认为“季凌燕”
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public LayPageInfo<UserInforDTO> GetList2(PageInfo<UserInfor> pageInfo, Expression<Func<UserInfor, bool>> whereLambda ,string name)
        {
            var tempquery = Db.Set<UserInfor>().AsNoTracking().Include(a => a.Department).Where<UserInfor>(whereLambda.Compile());
            //var Roles = Db.Set<Role>().Where(a => a.Name == "招标询价约谈").FirstOrDefault().Id;
            //var UserInforRoles = Db.Set<UserRole>().Where(a => a.RoleId == Roles).Select(s => s.UserId).ToList();
            //var sname= tempquery.Where(a=>UserInforRoles.Contains(a.Id));
            pageInfo.TotalCount = tempquery.Count();
            tempquery = tempquery.OrderByDescending(a => a.Id);
            tempquery = tempquery.Skip<UserInfor>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<UserInfor>(pageInfo.PageSize);
          
            //var set = UserInforRoles;
            var query = from a in tempquery
                            // where UserInforRoles.Contains(a.Id)
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            LastName = a.LastName,
                            FirstName = a.FirstName,
                            DisplyName = a.DisplyName,
                            Sex = a.Sex,
                            Age = a.Age,
                            Tel = a.Tel,
                            Mobile = a.Mobile,
                            Email = a.Email,
                            EntryDatetime = a.EntryDatetime,
                            Ustart = a.Ustart,
                            DeptName = a.Department.Name,
                            State = a.State,

                        };
            var local = from a in query.AsEnumerable()
                        select new UserInforDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            LastName = a.LastName,
                            FirstName = a.FirstName,
                            DisplyName = a.DisplyName,
                            Sex = a.Sex,
                            Age = a.Age,
                            Tel = a.Tel,
                            Mobile = a.Mobile,
                            Email = a.Email,
                            EntryDatetime = a.EntryDatetime,
                            Ustart = a.Ustart,
                            DeptName = a.DeptName,
                            SexDic = EmunUtility.GetDesc(typeof(SexEnum), a.Sex),
                            //IsMainDic = EmunUtility.GetDesc(typeof(OtherDataState), a.IsMain ?? 0)
                            State = a.State ?? 0,
                            StateDic = EmunUtility.GetDesc(typeof(UserState), a.State ?? 0)

                        };
            return new LayPageInfo<UserInforDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };

        }


        /// <summary>
        /// 修改字段值
        /// </summary>
        /// <param name="info">修改字段新</param>
        /// <returns>返回受影响行数</returns>
        public int UpdateField(UpdateFieldInfo info)
        {
            string sqlstr = "";
            switch (info.FieldName)
            {
                case "Ustart"://手机使用状态
                    {
                        int state = 0;
                        int.TryParse(info.FieldValue, out state);
                        sqlstr = "update UserInfor set UStart=" + state + " where Id=" + info.Id;
                    }
                    break;
                case "State"://用户状态
                    {
                        int state = 0;
                        int.TryParse(info.FieldValue, out state);
                        sqlstr = "update UserInfor set State=" + state + " where Id=" + info.Id;
                    }
                    break;


            }
           
            if (!string.IsNullOrEmpty(sqlstr))
                return ExecuteSqlCommand(sqlstr);
            return 0;

        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Ids">修改集合</param>
        /// <returns></returns>
        public int Delete(string Ids)
        {
            string sqlstr = "update UserInfor set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="userInfo">当前的用户实体</param>
        /// <returns>返回当前保存对象</returns>
        public UserInfor SaveInfo(UserInfor userInfo)
        {
            UserInfor resul = null;
            if (userInfo.Id > 0)
            {//修改
                var _firest = _UserInforSet.AsNoTracking().Where(a => a.Id == userInfo.Id).FirstOrDefault();
                if (_firest.Pwd != userInfo.Pwd)
                {//表示密码修改
                    userInfo.Pwd = MD5Encrypt.Encrypt(MD5Encrypt.Encrypt(userInfo.Pwd));
                }
                resul = UpdateSave(userInfo);

            }
            else
            {
                resul = AddSave(userInfo);
            }
            return resul;



        }
        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="userInfo">修改对象</param>
        /// <returns></returns>
        private UserInfor UpdateSave(UserInfor userInfo)
        {
            UserInfor resul;
            var tempinfo = _UserInforSet.FirstOrDefault(a => a.Id == userInfo.Id);
            tempinfo.Name = userInfo.Name;
            tempinfo.Pwd = userInfo.Pwd;
            tempinfo.LastName = userInfo.LastName;
            tempinfo.FirstName = userInfo.FirstName;
            tempinfo.DisplyName = userInfo.DisplyName;
            tempinfo.Sex = userInfo.Sex;
            tempinfo.Age = userInfo.Age;
            tempinfo.Tel = userInfo.Tel;
            tempinfo.Mobile = userInfo.Mobile;
            tempinfo.EntryDatetime = userInfo.EntryDatetime;
            tempinfo.IdNo = userInfo.IdNo;
            tempinfo.Address = userInfo.Address;
            tempinfo.DepartmentId = userInfo.DepartmentId;
            tempinfo.ModifyUserId = userInfo.ModifyUserId;
            tempinfo.ModifyDatetime = userInfo.ModifyDatetime;
            tempinfo.Pwd = userInfo.Pwd;
            tempinfo.UserEsTy = userInfo.UserEsTy;
            tempinfo.WxCode = userInfo.WxCode;
            tempinfo.Email = userInfo.Email;
            tempinfo.Remark = userInfo.Remark;
            resul = tempinfo;
            Db.SaveChanges();
            return resul;
        }
        /// <summary>
        /// 新增保存
        /// </summary>
        /// <param name="userInfo">新增对象</param>
        /// <returns>当前保存对象</returns>
        private UserInfor AddSave(UserInfor userInfo)
        {
            userInfo.Pwd=MD5Encrypt.Encrypt(MD5Encrypt.Encrypt(userInfo.Pwd));
            userInfo.CreateDatetime = DateTime.Now;
            userInfo.CreateUserId = userInfo.CreateUserId;
            userInfo.IsDelete = 0;
            userInfo.Ustart = 0;
            userInfo.State = 0;
            return Add(userInfo);
         
        }

        /// <summary>
        /// 显示查看基本信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public UserInforDTO ShowView(int Id)
        {

            var tempInfo = _UserInforSet.Where(a => a.Id == Id).Include(a=>a.Department).FirstOrDefault();
            var info = new UserInforDTO
            {
                Id = tempInfo.Id,
                Name = tempInfo.Name,
                LastName = tempInfo.LastName,
                FirstName = tempInfo.FirstName,
                DisplyName = tempInfo.DisplyName,
                Sex = tempInfo.Sex,
                Age = tempInfo.Age,
                Remark = tempInfo.Remark,
                Tel = tempInfo.Tel,
                Mobile = tempInfo.Mobile,
                Email = tempInfo.Email,
                EntryDatetime = tempInfo.EntryDatetime.ConvertDate().StringToDate(),
                IdNo = tempInfo.IdNo,
                Address = tempInfo.Address,
                DepartmentId = tempInfo.DepartmentId,
                IsDelete = tempInfo.IsDelete,
                CreateUserId = tempInfo.CreateUserId,
                CreateDatetime = tempInfo.CreateDatetime.ConvertDate().StringToDate(),
                Ustart = tempInfo.Ustart,
                DeptName = tempInfo.Department.Name,
                Pwd = tempInfo.Pwd,
                Repass = tempInfo.Pwd,
                State = tempInfo.State,
                UserEsTy = tempInfo.UserEsTy,
                WxCode=tempInfo.WxCode

            };
            info.SexDic = EmunUtility.GetDesc(typeof(SexEnum), tempInfo.Sex);
            info.UstartDic = EmunUtility.GetDesc(typeof(DataState), tempInfo.Ustart);
            info.StateDic= EmunUtility.GetDesc(typeof(UserState), tempInfo.State??0);
            return info;


        }
        /// <summary>
        /// 判断某一字段值是否唯一
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="inputValue">输入值</param>
        /// <param name="Id">修改时的ID</param>
        /// <returns>True表示存在，否则不存在</returns>
        public bool CheckFieldValExist(UpdateFieldInfo updateField)
        {
            bool result = false;
            switch (updateField.FieldName) {
                case "Name":
                    result = _UserInforSet.AsNoTracking().Where(a => a.Name == updateField.FieldValue && a.IsDelete != 1&&a.Id != updateField.Id).Any();
                    break;

            }
            return result;

        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oldPwd">旧密码</param>
        /// <param name="pwd">新密码</param>
        /// <param name="Id">当前ID</param>
        /// <returns>true:成功</returns>
        public RequstResult UpdatePwd(string oldPwd, string pwd, int Id)
        {
            var firstInfo = _UserInforSet.Where(a => a.Id == Id).FirstOrDefault();
            var tempwd= MD5Encrypt.Encrypt(MD5Encrypt.Encrypt(oldPwd));
            RequstResult resultData = new RequstResult { Code = 0 };
            if (firstInfo.Pwd != tempwd)
            {
                resultData.Tag = -1;
                resultData.Msg = "旧密码输入错误";
            }
            else {
               
                firstInfo.Pwd = MD5Encrypt.Encrypt(MD5Encrypt.Encrypt(pwd)); ;
                SaveChanges();
                resultData.Tag = 1;
            }
            SetRedis();
            return resultData;

           
        }
        /// <summary>
        /// 设置用户基本信息
        /// </summary>
        /// <param name="info">当前用户对象</param>
        /// <returns>返回用户基本信息</returns>
        public UserInfor SetUserInfo(UserInfor userInfo)
        {
            var tempinfo = _UserInforSet.FirstOrDefault(a => a.Id == userInfo.Id);
            tempinfo.Name = userInfo.Name;
            tempinfo.LastName = userInfo.LastName;
            tempinfo.FirstName = userInfo.FirstName;
            tempinfo.DisplyName = userInfo.DisplyName;
            tempinfo.Sex = userInfo.Sex;
            tempinfo.Tel = userInfo.Tel;
            tempinfo.Mobile = userInfo.Mobile;
            tempinfo.IdNo = userInfo.IdNo;
            tempinfo.Address = userInfo.Address;
            tempinfo.Email = userInfo.Email;
            tempinfo.ModifyUserId = userInfo.ModifyUserId;
            tempinfo.ModifyDatetime = userInfo.ModifyDatetime;
            tempinfo.Remark = userInfo.Remark;
            Db.SaveChanges();
            SetRedis();
            return tempinfo;
        }
        /// <summary>
        /// 根据条件查询数据库返回指定Redis用户对象
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>Redis存储对象</returns>
        public IList<RedisUser> GetRedisUsers(Expression<Func<UserInfor, bool>> whereLambda)
        {
            var query0 = _UserInforSet.Where(whereLambda).Include(a=>a.Department);
            var query = from a in query0
                        select new {
                            Id=a.Id,
                            Name=a.Name,
                            DisplyName=a.DisplyName,
                            Email=a.Email,
                            DepartmentId=a.DepartmentId,
                            DeptName=a.Department.Name,
                            State=a.State,
                            UserEs=a.UserEs,
                            UserEsTy=a.UserEsTy
                        };
            var local = from a in query
                        select new RedisUser
                        {
                            Id = a.Id,
                            Name = a.Name,
                            DisplyName = a.DisplyName,
                            Email = a.Email,
                            DepartmentId = a.DepartmentId,
                            DeptName = a.DeptName,
                            State = a.State,
                            UserEs = a.UserEs,
                            UserEsTy = a.UserEsTy
                        };
            return local.ToList();


        }
        /// <summary>
        /// 存储Redis
        /// </summary>
       public  void SetRedis()
        {
            var listuser = GetRedisUsers(a => a.IsDelete == 0);
            foreach (var item in listuser)
            {
                SysIniInfoUtility.SetRedisHash(item, StaticData.RedisUserKey, (a, c) =>
                {
                    return $"{a}:{c}";
                });


            }
        }
        /// <summary>
        /// word登录插件
        /// </summary>
        /// <param name="uid">当前用户</param>
        /// <returns></returns>
        public SignInFromWordUser SignInFromWord(int uid)
        {
             return _UserInforSet.Where(a => a.Id == uid).Select(a => new SignInFromWordUser
            {
                Uid = a.Id,
                Name = a.Name,
                ReadOnly = "false"

            }).FirstOrDefault();
        }
        /// <summary>
        /// 获取用户是当前系统中第几个活动用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>-1 用户不存在</returns>
        public  int GetUserTurn(string userName)
        {
            //try
            //{

            //    var query = _UserInforSet.Where(m => m.Name == userName && (m.State == 1))
            //        .OrderBy(m => m.Id).Select(m => _UserInforSet.Count(y => y.Id < m.Id && (y.State == 1)));
            //    return query.First();
            //}
            //catch
            //{
            //    return -1;
            //}
            try
            {

                return _UserInforSet.Where(a => a.State == 1).Count();
                
            }
            catch
            {
                return -1;
            }
        }
        /// <summary>
        /// 获取移动用户是当前系统中第几个活动用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>-1 用户不存在</returns>
        public int GetMobileUserTurn(string userName)
        {
            //try
            //{

            //    var query = _UserInforSet.Where(m => m.Name == userName && (m.Ustart == 1))
            //        .OrderBy(m => m.Id).Select(m => _UserInforSet.Count(y => y.Id < m.Id && (y.Ustart == 1)));
            //    return query.First();
            //}
            //catch
            //{
            //    return -1;
            //}
            try
            {

                return _UserInforSet.Where(a => a.Ustart == 1).Count();

            }
            catch
            {
                return -1;
            }

        }
        /// <summary>
        /// 获取系统并发用户数
        /// </summary>
        /// <returns></returns>
        public int GetBinFaYongFuSHu()
        {
            var StartDate = DateTime.Now.AddDays(-1);
            var endDate= DateTime.Now.AddDays(1);
            //var number = Db.Set<LoginLog>().AsNoTracking()
            //    .Where(a => a.CreateDatetime > StartDate && a.CreateDatetime < endDate)
            //    .GroupBy(a => a.LoginUserId).Select(a => a.Key).Count();//查询当天登录用户数
            //按在线平均4小时，一天工作时长为8小时计算平均并发量
           // var UserNumber = number * 4 / 8;
            var UserNumber = 20 * 4 / 8;
            return UserNumber;




        }

        public int ADDDzqz(int id) 
        {
            string sqlstr = "update UserInfor set UserEs=1,UserEsTy=1 where Id in(" + id + ")";
            return ExecuteSqlCommand(sqlstr);


        }
        public int? DZQZ(int id) {
            int? isdz = 0;
            int? isd = 0;
            try
            {
             isd = Db.Set<UserInfor>().Where(a => a.Id == id).FirstOrDefault().UserEs;
            }
            catch (Exception)
            {

                return isd == null ? isdz : isd;
            }
          
            return isd == null ? isdz : isd;
        
        }

        /// <summary>
        /// 查询国家
        /// </summary>
        /// <returns></returns>
       public IList<Country> Gj() {
            var Glist = Db.Set<Country>().ToList();
            return Glist;
        }
        public IList<Province> Sf()
        {
            var Glist = Db.Set<Province>().ToList();
            return Glist;
        }
        public IList<City> sj()
        {
            var Glist = Db.Set<City>().ToList();
            return Glist;
        }

    }
}
