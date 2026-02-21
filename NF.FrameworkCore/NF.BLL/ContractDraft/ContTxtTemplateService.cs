using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NF.AutoMapper;
using NF.BLL.Extend;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NF.BLL
{
    /// <summary>
    /// 合同模板
    /// </summary>
    public partial class ContTxtTemplateService
    {
        private IMapper _IMapper;
        /// <summary>
        /// 大列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">where条件表达式</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <param name="deptId">经办机构ID，选择模板时用</param>
        /// <returns></returns>
        public LayPageInfo<ContTxtTemplateListDTO> GetList<s>(PageInfo<ContTxtTemplate> pageInfo, Expression<Func<ContTxtTemplate, bool>> whereLambda,
           Expression<Func<ContTxtTemplate, s>> orderbyLambda, bool isAsc,int deptId=0,int htLb=0)
        {
            IList<DepartmentDTO> list = RedisHelper.StringGetToList<DepartmentDTO>("Nf-DeptListAll");
            IList<DataDictionary> listdate = this.Db.Set<DataDictionary>().Where(a => a.DtypeNumber == (int)DataDictionaryEnum.contractType).ToList(); //RedisHelper.StringGetToList<DepartmentDTO>("Nf-DeptListAll");
            var tempquery = _ContTxtTemplateSet.AsTracking().Where<ContTxtTemplate>(whereLambda.Compile()).AsQueryable();
            if (deptId > 0&& htLb>0)
            {
                var strdept = deptId.ToString();
                var strLb = htLb.ToString();
                tempquery = tempquery
                                .Where(a => !string.IsNullOrEmpty(a.DeptIds) && !string.IsNullOrEmpty(a.TepTypes) && (a.DeptIds == strdept
                                    || a.DeptIds.StartsWith(strdept + ",")
                                    || a.DeptIds.EndsWith("," + strdept)
                                    || a.DeptIds.Contains("," + strdept + ","))
                                    &&( a.TepTypes == strLb
                                    || a.TepTypes.StartsWith(strLb + ",")
                                    || a.TepTypes.EndsWith("," + strLb)
                                    || a.TepTypes.Contains("," + strLb + ",")));
            }
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<ContTxtTemplate>))
            { //分页
                tempquery = tempquery.Skip<ContTxtTemplate>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<ContTxtTemplate>(pageInfo.PageSize);
            }

            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            TepType=a.TepType,//模板类别（合同类别）
                            TextType=a.TextType,//文本类别
                            DeptIds=a.DeptIds,//部门
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            ModifyDateTime=a.ModifyDateTime,//修改时间
                            Vesion=a.Vesion,//版本
                            Tstate=a.Tstate,//状态（启用、禁用）
                            WordEdit=a.WordEdit,//是否允许编辑
                            TepTypes=a.TepTypes,//合同类别，多个

                        };
            var local = from a in query.AsEnumerable()
                        select new ContTxtTemplateListDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            TepType = a.TepType,//模板类别（合同类别）
                            //TepTypeDic =DataDicUtility.GetDicValueToRedis(a.TepType, DataDictionaryEnum.contractType),//合同类别
                            TextType = a.TextType,//文本类别
                            TextTypeDic= this.Db.GetRedisDataDictionaryValue(a.TextType ?? 0, DataDictionaryEnum.ContTxtType),
                            //DataDicUtility.GetDicValueToRedis(a.TextType, DataDictionaryEnum.ContTxtType),//合同文本类别
                            DeptIds = a.DeptIds,//部门
                            DeptNames = list == null ? "" :( StringHelper.ArrayString2String( list.Where(p => StringHelper.String2ArrayInt(a.DeptIds).Contains(p.Id)).Select(p => p.Name).ToList())),
                            CreateUserId = a.CreateUserId,
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.CreateUserId??0),
                            CreateDateTime = a.CreateDateTime,
                            ModifyDateTime = a.ModifyDateTime,//修改时间
                            Vesion = a.Vesion??0,//版本
                            Tstate = a.Tstate??0,//状态（启用、禁用）
                            WordEdit = a.WordEdit??0,//是否允许编辑
                            UseHistId=Db.Set<ContTxtTemplateHist>().Where(p=>p.TempId==a.Id&&p.UseVersion==1).Any()?
                            Db.Set<ContTxtTemplateHist>().Where(p => p.TempId == a.Id && p.UseVersion == 1).FirstOrDefault().Id:0,
                            TepTypeDic= StringHelper.ArrayString2String(listdate.Where(p => StringHelper.String2ArrayInt(a.TepTypes).Contains(p.Id)).Select(p => p.Name).ToList()),
                        };
            return new LayPageInfo<ContTxtTemplateListDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }
        /// <summary>
        ///校验对象值
        /// </summary>
        /// <param name="templateDTO">模板对象</param>
        /// <returns>RequestMsg：返回消息对象</returns>
       public  RequestMsg CheckInputValExist(ContTxtTemplateDTO templateDTO)
        {
            var reqMsg= new RequestMsg()
            {
                Code = 0,
                Msg="success"

            };
            var IsUnqName = _ContTxtTemplateSet.AsNoTracking().Where(a => a.Name == templateDTO.Name && a.Id != templateDTO.Id).Any();
            if (IsUnqName)
            {
                reqMsg.Code = 1;
                reqMsg.Msg = "模板名称已经存在";
               
            }
            //else 
            //{
            //    var list = _ContTxtTemplateSet.AsNoTracking().Where(a => a.Id != templateDTO.Id && a.TepType == templateDTO.TepType
            //      && a.TextType == templateDTO.TextType).ToList();
            //    var depts = StringHelper.String2ArrayInt(templateDTO.DeptIds);
            //    foreach (var temp in list)
            //    {
            //        var tempdepts = StringHelper.String2ArrayInt(temp.DeptIds);
            //        var indepts = depts.Intersect(tempdepts);
            //        if (indepts.Count() > 0)
            //        {
            //            reqMsg.Code = 2;
            //            reqMsg.Msg = $"与【{temp.Name}】重复";
            //            break;
            //        }


            //    }
            //}

            return reqMsg;

        }
        
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="template">当前对象</param>
        /// <returns></returns>
        public CurrTempInfo AddSave(ContTxtTemplate template,IMapper _IMapper)
        {

            SetPath(template);//设置路径
            var info=Add(template);
            CurrTempInfo currTemp = new CurrTempInfo();
            currTemp.TempHistId= CreateTempHist(_IMapper,template).Id;
            currTemp.TempId = info.Id;
            //修改子项零时数据
            UpdateItem(info, currTemp.TempHistId);
            return currTemp;



        }
        /// <summary>
        /// 修改子项
        /// </summary>
        private void UpdateItem(ContTxtTemplate template,int tempHistId)
        {
            string sqlstr =$"update ContTxtTempAndSubField set TempHistId={tempHistId} where TempHistId=-{template.CreateUserId}";
            ExecuteSqlCommand(sqlstr);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="template">当前修改对象</param>
        /// <returns></returns>
       public CurrTempInfo UpdateSave(ContTxtTemplate template,IMapper _IMapper)
        {
            //查询当前使用的模板
            var lastHist = Db.Set<ContTxtTemplateHist>()
                 .Where(a => a.TempId == template.Id && a.UseVersion == 1).FirstOrDefault();
            CurrTempInfo currTemp = new CurrTempInfo();
            var newhist= CreateTempHist(_IMapper, template, true);
            currTemp.TempHistId = newhist.Id;
            currTemp.TempId = template.Id;
            template.Path = newhist.Path;
            CopyTempVarAndFile(lastHist, newhist,template);
            Db.Entry(template).State = EntityState.Modified;
            //复制标的字段
            CopySubFields(template, lastHist, newhist.Id, _IMapper);
            UpdateItem(template, newhist.Id);
            this.SaveChanges();
            return currTemp;

        }
        private void CopySubFields(ContTxtTemplate template, ContTxtTemplateHist userHist, int HistId, IMapper _IMapper)
        {
            //var userHist = Db.Set<ContTxtTemplateHist>()
            //     .Where(a => a.TempId == template.Id && a.UseVersion == 1).AsNoTracking().FirstOrDefault();
            IList<ContTxtTempAndSubField> listfds = new List<ContTxtTempAndSubField>();
            if (userHist != null)
            {
                var SubFields = Db.Set<ContTxtTempAndSubField>().Where(a => a.TempHistId == userHist.Id).AsNoTracking().ToList();
                foreach (var item  in SubFields)
                {
                   
                   var model = _IMapper.Map<ContTxtTempAndSubField, ContTxtTempAndSubField>(item); //item.ToModel<ContTxtTempAndSubField, ContTxtTempAndSubField>();
                    model.TempHistId = HistId;
                    listfds.Add(model);
                }

                foreach (var t in listfds)
                {
                    
                    this.Db.Entry<ContTxtTempAndSubField>(t).State = EntityState.Modified;
                }



            }

        }
        /// <summary>
        /// 修改时复制变量到新历史模板
        /// </summary>
        private  void CopyTempVarAndFile(ContTxtTemplateHist lastHist, ContTxtTemplateHist newHist, ContTxtTemplate template)
        {
           
            //查询当前使用模板的变量（主要是自定义变量）
            IList<ContTxtTempVarStore> listVars = Db.Set<ContTxtTempVarStore>()
                .Where(a => a.TempHistId == lastHist.Id).ToList();
            //查询当前使用的历史模板使用的变量
            IList<int> vars = Db.Set<ContTxtTempAndVarStoreRela>()
                  .Where(a => a.TempHistId == lastHist.Id).Select(a => a.VarId ?? 0).ToList();
            IList<ContTxtTempVarStore> listvarstores = new List<ContTxtTempVarStore>();
            //变量添加进入新的历史模板
            if (vars != null && vars.Count > 0)
            {
                foreach (var item in listVars)
                {
                    var val = new ContTxtTempVarStore()
                    {
                        Name = item.Name,
                        Code=item.Code,
                        IsCustomer=item.IsCustomer,
                        Isdelete=item.Isdelete,
                        TempHistId= newHist.Id,
                        StoreType=item.StoreType
                        
                    };
                    if (item.OriginalId.HasValue && item.OriginalId.Value > 0)
                    {
                        val.OriginalId = item.OriginalId;
                    }
                    else
                    {
                        val.OriginalId = item.Id;
                    }

                    listvarstores.Add(val);
                }
                
            }

            //添加新的模板变量映射
            IList<ContTxtTempAndVarStoreRela> listvarrelas = new List<ContTxtTempAndVarStoreRela>();
            if (vars!=null&& vars.Count() > 0)
            {
                foreach (var vId in vars)
                {
                    var varral = new ContTxtTempAndVarStoreRela()
                    {
                        VarId = vId,
                        TempHistId = newHist.Id

                    };
                    listvarrelas.Add(varral);
                }

                this.Db.Set<ContTxtTempAndVarStoreRela>().AddRange(listvarrelas);

            }
            //设置路径
            SetHistPath(newHist);
            template.Path = newHist.Path;
            if (lastHist != null)
            {
                FileUtility.CopyFile(lastHist.Path, newHist.Path);
            }
            else
            {
                FileUtility.CopyFile(template.Path, newHist.Path);
            }

            this.Db.Set<ContTxtTempVarStore>().AddRange(listvarstores);


            this.Db.SaveChanges();
           



        }
        

        /// <summary>
        /// 创建模板历史
        /// </summary>
        /// <returns></returns>
        public ContTxtTemplateHist CreateTempHist(IMapper _IMapper, ContTxtTemplate template,bool newPath=false)
        {
             string sqlstr=$"update ContTxtTemplateHist set UseVersion=0 where TempId={template.Id}";
             ExecuteSqlCommand(sqlstr);
            var temphist = _IMapper.Map<ContTxtTemplate, ContTxtTemplateHist>(template); //template.ToModel<ContTxtTemplate, ContTxtTemplateHist>();
            temphist.TempId = template.Id;
            temphist.UseVersion = 1;
            temphist.CreateUserId = temphist.ModifyUserId;
            temphist.CreateDateTime = temphist.ModifyDateTime;
            if (newPath)
            {
                SetHistPath(temphist);
            }
            Db.Set<ContTxtTemplateHist>().Add(temphist);
            this.SaveChanges();
            return temphist;
        }

        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="info">修改的字段对象</param>
        /// <returns>返回受影响行数</returns>
        public int UpdateField(UpdateFieldInfo info)
        {
            string sqlstr = "";
            switch (info.FieldName)
            {
                case "Tstate"://状态
                    var state = Convert.ToInt32(info.FieldValue);
                    sqlstr = $"update  ContTxtTemplate set Tstate={state} where Id={info.Id}";
                    break;

                default:
                    break;
            }
            if (!string.IsNullOrEmpty(sqlstr))
                return ExecuteSqlCommand(sqlstr);
            return 0;

        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Ids">需要删除的Ids集合</param>
        /// <returns>受影响行数</returns>
        public int Delete(string Ids)
        {
            StringBuilder strb = new StringBuilder();
            strb.Append("update ContTxtTemplate set IsDelete=1 where Id in(" + Ids + ");");
            strb.Append("update ContTxtTemplateHist set IsDelete=1 where TempId in(" + Ids + ")");
            return ExecuteSqlCommand(strb.ToString());
        }
        /// <summary>
        /// 查看信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public ContTxtTemplateViewDTO ShowView(int Id)
        {
            IList<DepartmentDTO> list = RedisHelper.StringGetToList<DepartmentDTO>("Nf-DeptListAll");
            IList<DataDictionary> listdate = this.Db.Set<DataDictionary>().Where(a => a.DtypeNumber == (int)DataDictionaryEnum.contractType).ToList(); 
            var query = from a in _ContTxtTemplateSet.AsNoTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            TepType = a.TepType,//模板类别（合同类别）
                            TextType = a.TextType,//文本类别
                            DeptIds = a.DeptIds,//部门
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            ModifyDateTime = a.ModifyDateTime,//修改时间
                            Vesion = a.Vesion,//版本
                            Tstate = a.Tstate,//状态（启用、禁用）
                            WordEdit = a.WordEdit,//是否允许编辑
                            FieldType=a.FieldType,
                            ShowSub=a.ShowSub,//是否显示标的
                            MingXiTitle=a.MingXiTitle,//明细标题
                            TepTypes = a.TepTypes,
                        };
            var local = from a in query.AsEnumerable()
                        select new ContTxtTemplateViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            TepType = a.TepType,//模板类别（合同类别）
                            TextType = a.TextType,//文本类别
                            DeptIds = a.DeptIds,//部门
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            ModifyDateTime = a.ModifyDateTime,//修改时间
                            Vesion = a.Vesion,//版本
                            Tstate = a.Tstate,//状态（启用、禁用）
                            TstateDic = a.Tstate == 1 ? "启用" : "禁用",
                            WordEdit = a.WordEdit,//是否允许编辑
                            FieldType = a.FieldType,
                            ShowSub = a.ShowSub ?? 0,//是否显示标的
                            MingXiTitle = a.MingXiTitle,//明细标题
                            FieldTypeVal = a.FieldType??0,
                            ShowSubMatter = a.ShowSub == 1 ? true:false,
                            TepTypeDic = this.Db.GetRedisDataDictionaryValue(a.TepType ?? 0, DataDictionaryEnum.contractType),
                            //DataDicUtility.GetDicValueToRedis(a.TepType, DataDictionaryEnum.contractType),//合同类别
                            TextTypeDic = this.Db.GetRedisDataDictionaryValue(a.TextType ?? 0, DataDictionaryEnum.ContTxtType),
                            //DataDicUtility.GetDicValueToRedis(a.TextType, DataDictionaryEnum.ContTxtType),//合同文本类别
                            DeptNames = list==null?"":( StringHelper.ArrayString2String(list.Where(p => StringHelper.String2ArrayInt(a.DeptIds).Contains(p.Id)).Select(p => p.Name).ToList())),
                            DeptIdsArray = StringHelper.String2ArrayInt(a.DeptIds),
                            HistId= GetTempHist(a.Id)==null?0: GetTempHist(a.Id).Id,
                            TepTypes=a.TepTypes,
                            TepTypesArray = StringHelper.String2ArrayInt(a.TepTypes),

                        };
            return local.FirstOrDefault();



        }
        /// <summary>
        /// 获取模板对应历史模板
        /// </summary>
        /// <param name="tempId">模板ID</param>
        /// <returns></returns>
        private ContTxtTemplateHist GetTempHist(int tempId)
        {
            return Db.Set<ContTxtTemplateHist>().AsNoTracking()
                 .Where(p => p.TempId == tempId && p.IsDelete != 1 && p.UseVersion == 1)
                 .FirstOrDefault();
        }

       
        /// <summary>
        /// 设置PATH
        /// </summary>
        /// <param name="info"></param>
        public void SetPath(ContTxtTemplate info)
        {
            string templateName = info.Name;
            string templateVersion = info.Vesion.ToString();
            string docName = DateTime.Now.Ticks + "-" + templateName + "-" + templateVersion;
            info.Path = String.Format("~/Uploads/ContractTemplates/{0}.docx", docName);
        }

        /// <summary>
        /// 设置历史PATH
        /// </summary>
        /// <param name="info"></param>
        public void SetHistPath(ContTxtTemplateHist info)
        {
            string templateName = info.Name;
            string templateVersion = info.Vesion.ToString();
            string docName = DateTime.Now.Ticks + "-" + templateName + "-" + templateVersion;
            info.Path = String.Format("~/Uploads/ContractTemplates/{0}.docx", docName);
        }
        /// <summary>
        /// 自定义变量
        /// </summary>
        /// <param name="cttextid">合同文本ID</param>
        /// <returns></returns>
        public IList<ContractVariable> GetCustomVariables(Int32 cttextid)
        {
            //if (_dataContext == null)
            //    _dataContext = DataEntity.Initializes();
          
                var _contractTextObj = Db.Set<ContText>().Where(p => p.Id == cttextid).FirstOrDefault();
            if (_contractTextObj == null)
                return null;
            IList<ContTxtTempVarStore> varsDef =Db.Set< ContTxtTempVarStore >() 
                .Where(p => p.TempHistId == _contractTextObj.TemplateId && p.IsCustomer == 1 && p.Isdelete != 1)
                .ToList();

            ContTxtTempVarStore @var = null;
            if (varsDef != null && varsDef.Count > 0)
            {
                IList<ContractVariable> cuVars = new List<ContractVariable>();
                for (var i = 0; i < varsDef.Count; i++)
                {
                    ContractVariable cuvar = new ContractVariable();
                    @var = varsDef[i];
                    cuvar.VarLabel = @var.Name == null ? String.Empty : @var.Name;
                    cuvar.VarName = @var.OriginalId == null ? @var.Id.ToString() : @var.OriginalId.Value.ToString();
                    cuvar.VarValue = null;
                    cuVars.Add(cuvar);
                }
                return cuVars;
            }
            return null;

        }

    }
}
