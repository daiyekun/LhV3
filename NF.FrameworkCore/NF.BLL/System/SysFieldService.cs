using Microsoft.EntityFrameworkCore;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NF.BLL
{
  public partial  class SysFieldService
    {
        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <param name="pageInfo">用户对象</param>
        /// <param name="whereLambda">Where条件</param>
        /// <returns>用户列表</returns>
        public LayPageInfo<SysFieldDTO> GetList(PageInfo<SysField> pageInfo, Expression<Func<SysField, bool>> whereLambda)
        {
            var tempquery = Db.Set<SysField>().AsNoTracking().Where<SysField>(whereLambda.Compile());
            pageInfo.TotalCount = tempquery.Count();
            //tempquery = tempquery.OrderByDescending(a => a.Zbpx);
            tempquery = tempquery.OrderBy(a => a.Zbpx);
            //tempquery = tempquery.OrderByO(a=>a.)
            tempquery = tempquery.Skip<SysField>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<SysField>(pageInfo.PageSize);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Fname = a.Fname,
                            Lable = a.Lable,//字段标题
                            FieldType = a.FieldType,//字段类型
                            Required = a.Required,//必填
                            IsList = a.IsList,//显示列表
                            Tag = a.Tag,//用于
                            SelData = a.SelData,//选择框内容
                            Isqy = a.Isqy,
                           Zbpx = a.Zbpx
                         };
            var local = from a in query.AsEnumerable()
                        select new SysFieldDTO
                        {
                            Id = a.Id,
                            Lable = a.Lable,//字段标题
                            FieldTypeName = ZType(a.FieldType??0),//字段类型
                            RequiredName = (a.Required ?? 0)== 0 ? "否" : "是",//必填
                            IsListName =(a.IsList ?? 0)== 0 ? "否" : "是",//显示列表
                            TagName = Slist(a.Tag ?? 0),//用于
                            SelData =a.SelData,//选择框内容
                            Isqy = a.Isqy,//自定义排序列
                            Zbpx = a.Zbpx,//是否启用
                            Fname = a.Fname,
                        };
            return new LayPageInfo<SysFieldDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0
            };
        }

        public string ZType(int t) {
            var name = "";
            if (t==0)
            {
                name = "文本框";

            }
            else if (t==1)
            {
                name = "选择框";
            }
            else if (t == 2)
            {
                name = "时间框";
            }

            return name;
        }
        public string Slist(int t)
        {
            var name = "";
            if (t == 0)
            {
                name = "合同";

            }
            //else if (t == 1)
            //{
            //    name = "选择框";
            //}
            //else if (t == 2)
            //{
            //    name = "时间框";
            //}

            return name;
        }
        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="userInfo">当前的用户实体</param>
        /// <returns>返回当前保存对象</returns>
        public SysField SaveInfo(SysField sysField)
        {
            SysField resul = null;
            if (sysField.Id > 0)
            {//修改
                var _firest = _SysFieldSet.AsNoTracking().Where(a => a.Id == sysField.Id).FirstOrDefault();
              
                resul = UpdateSave(sysField);

            }
            else
            {
                resul = AddSave(sysField);
            }
            return resul;



        }

        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="userInfo">修改对象</param>
        /// <returns></returns>
        private SysField UpdateSave(SysField sysField)
        {
            SysField resul;
            var tempinfo = _SysFieldSet.FirstOrDefault(a => a.Id == sysField.Id);
            tempinfo.Lable = sysField.Lable;//字段标题
            tempinfo.FieldType = sysField.FieldType;//字段类型
            tempinfo.Required = sysField.Required;//必填
            tempinfo.IsList = sysField.IsList;//显示列表
         //   tempinfo.Tag = sysField.Tag;//用于
            tempinfo.SelData = sysField.SelData;//选择框内容
            tempinfo.Isqy = sysField.Isqy;//自定义排序列
            tempinfo.Zbpx = sysField.Zbpx;//是否启用
            tempinfo.Fname = sysField.Fname;
            resul = tempinfo;
            Db.SaveChanges();
            return resul;
        }
        /// <summary>
        /// 新增保存
        /// </summary>
        /// <param name="userInfo">新增对象</param>
        /// <returns>当前保存对象</returns>
        private SysField AddSave(SysField sysField)
        {

            //sysField.Lable = DateTime.Now;
            //sysField.FieldType = userInfo.CreateUserId;
            //sysField.Required = 0;
            //sysField.IsList = 0;
            sysField.Tag = 0;
            //sysField.SelData=
            return Add(sysField);

        }


        /// <summary>
        /// 显示查看基本信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public SysField ShowView(int Id)
        {
            var a = _SysFieldSet.Where(a => a.Id == Id).FirstOrDefault();
            var info = new SysField
            {
                Id = a.Id,
                Fname = a.Fname,
                Lable = a.Lable,//字段标题
                FieldType = a.FieldType,//字段类型
                Required = a.Required,//必填
                IsList = a.IsList,//显示列表
                Tag = a.Tag,//用于
                SelData = a.SelData,//选择框内容
                Isqy = a.Isqy,
                Zbpx = a.Zbpx
            };
            return info;
        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Ids">修改集合</param>
        /// <returns></returns>
        public int Delete(string Ids)
        {
            string sqlstr = "update SysField set ISqy=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
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
                case "ISqy"://手机使用状态
                    {
                        int state = 0;
                        int.TryParse(info.FieldValue, out state);
                        sqlstr = "update SysField set ISqy=" + state + " where Id=" + info.Id;
                    }
                    break;
                //case "State"://用户状态
                //    {
                //        int state = 0;
                //        int.TryParse(info.FieldValue, out state);
                //        sqlstr = "update SysField set State=" + state + " where Id=" + info.Id;
                //    }
                //    break;


            }

            if (!string.IsNullOrEmpty(sqlstr))
                return ExecuteSqlCommand(sqlstr);
            return 0;

        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <param name="field">当前字段</param>
        /// <param name="fdv">当前值</param>
        /// <returns></returns>
        public bool UpdateDesc(int Id, string field, string fdv)
        {
            var info = Find(Id);

            switch (field)
            {
                case "Zbpx":
                    info.Zbpx = Convert.ToInt32(fdv);
                    break;
              

            }

            return Update(info);

        }
    }
}
