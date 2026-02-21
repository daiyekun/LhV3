using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Models.ContTxtTemplate;
using NF.Web.Utility;

namespace NF.Web.Areas.ContractDraft.Controllers
{
    /// <summary>
    /// 标的字段
    /// </summary>
    [Area("ContractDraft")]
    [Route("ContractDraft/[controller]/[action]")]

    public class ContTxtTempAndSubFieldController : Controller
    {
        private IContTxtTempAndSubFieldService _IContTxtTempAndSubFieldService;
        public ContTxtTempAndSubFieldController(IContTxtTempAndSubFieldService IContTxtTempAndSubFieldService)
        {
            _IContTxtTempAndSubFieldService = IContTxtTempAndSubFieldService;
        }
        /// <summary>
        /// 获取选择Checkbox数据源
        /// </summary>
        /// <returns></returns>
        public IActionResult GetSubChkFields(int tempId, int fieldType, int bcId)
        {
            var userId = HttpContext.Session.GetInt32(StaticData.NFUserId) ?? 0;
            if (tempId <= 0)
            {
                tempId = -userId;
            }
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = _IContTxtTempAndSubFieldService.GetSubChkFields(tempId, fieldType, bcId)


            });
        }
        /// <summary>
        /// 获取字段列表
        /// </summary>
        /// <param name="bcId">业务品类</param>
        /// <param name="$fdType">显示格式：0统一格式，1：按业务品类不同格式</param>
        /// <returns>返回当前模板下字段名称</returns>
        public IActionResult GetFields(int tempId,int fdType, int bcId)
        {
            var userId = HttpContext.Session.GetInt32(StaticData.NFUserId) ?? 0;
            var pageInfo = new NoPageInfo<ContTxtTempAndSubField>();
            var predicateAnd = PredicateBuilder.True<ContTxtTempAndSubField>();
            //var predicateOr = PredicateBuilder.False<ContTxtTempAndSubField>();
            predicateAnd = predicateAnd.And(a=>a.TempHistId== tempId||a.TempHistId==-userId);
            predicateAnd = predicateAnd.And(a => a.FieldType == fdType&&(a.BcId??0)== bcId);
            var layPage = _IContTxtTempAndSubFieldService.GetList(pageInfo, predicateAnd, a => a.SortFd,true, fdType, bcId);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 保存标的模板字段
        /// </summary>
        /// <returns></returns>
        public IActionResult AddSubTmpField(IList<SelFields> selFields,int fieldType,int bcId,int tempHisId)
        {

            var userId=HttpContext.Session.GetInt32(StaticData.NFUserId) ?? 0;
            if (tempHisId <= 0)
            {
                tempHisId = -userId;
            }
            var delsql = $"delete ContTxtTempAndSubField where TempHistId={tempHisId} and BcId={bcId}";
            _IContTxtTempAndSubFieldService.ExecuteSqlCommand(delsql);
            IList<ContTxtTempAndSubField> fields = new List<ContTxtTempAndSubField>();
            foreach (var item in selFields)
            {
                var field = new ContTxtTempAndSubField();
                field.FieldType = fieldType;
                if (fieldType != 0)
                {
                    field.BcId = bcId;
                }
                field.TempHistId = tempHisId;
                field.SortFd = item.Id;
                field.Sval = item.Name;
                field.IsTotal = 0;//是否统计
                field.SubFieldId = item.Id;
                fields.Add(field);
                


            }
            _IContTxtTempAndSubFieldService.Add(fields);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "success",
                Code = 0,
            });

        }
        /// <summary>
        /// 排序（上移/下移）
        /// </summary>
        /// <returns></returns>
        public IActionResult SortField(int Id, int tempId,int fieldType,int bcId,bool up)
        {
            StringBuilder builder = new StringBuilder();
            var userId = HttpContext.Session.GetInt32(StaticData.NFUserId) ?? 0;
            if (tempId <= 0)
            {
                tempId = -userId;
            }
            
             var query=_IContTxtTempAndSubFieldService.GetQueryable(a => a.TempHistId == tempId && a.FieldType == fieldType && (a.BcId ?? 0) == bcId).ToList();
            var sortint = query.FirstOrDefault(a => a.Id == Id);
            if (up)
            {//上移
               
                if (sortint != null)
                {
                    var list = query.Where(a => a.SortFd < sortint.SortFd).OrderByDescending(a=>a.SortFd).ToList();
                    if (list.Count > 0) {
                     var sinfo = list.FirstOrDefault();
                        builder.Append($"update ContTxtTempAndSubField set SortFd={sinfo.SortFd} where Id={sortint.Id};");
                        builder.Append($"update ContTxtTempAndSubField set SortFd={sortint.SortFd} where Id={sinfo.Id};");
                    }

                }
            }
            else
            {
                if (sortint != null)
                {
                    var list = query.Where(a => a.SortFd > sortint.SortFd).OrderBy(a => a.SortFd).ToList();
                    if (list.Count > 0)
                    {
                        var sinfo = list.FirstOrDefault();
                        builder.Append($"update ContTxtTempAndSubField set SortFd={sinfo.SortFd} where Id={sortint.Id};");
                        builder.Append($"update ContTxtTempAndSubField set SortFd={sortint.SortFd} where Id={sinfo.Id};");
                    }

                }

            }
            if (!string.IsNullOrEmpty(builder.ToString()))
            {
                _IContTxtTempAndSubFieldService.ExecuteSqlCommand(builder.ToString());
            }

            return new CustomResultJson(new RequstResult()
            {
                Msg = "success",
                Code = 0,
            });
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public IActionResult Delete(string Ids)
        {
            var listIds = StringHelper.String2ArrayInt(Ids);
            var info = _IContTxtTempAndSubFieldService.GetQueryable(a => listIds.Contains(a.Id)).FirstOrDefault();
            if (info != null)
            {
                _IContTxtTempAndSubFieldService.Delete(info);
            }
            return new CustomResultJson(new RequstResult()
            {
                Msg = "操作成功",
                Code = 0,
            });
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public IActionResult SaveData(IList<ContTempSubFiled> fields)
        {
            // var list = new List<ContTxtTempAndSubField>();
            StringBuilder builder = new StringBuilder();
     
            foreach(var item in fields)
            {
                builder.Append($"update ContTxtTempAndSubField set Sval='{item.Sval}'  where Id={item.Id};");
            }
            if (!string.IsNullOrEmpty(builder.ToString()))
            {
                _IContTxtTempAndSubFieldService.ExecuteSqlCommand(builder.ToString());
            }
            
            return new CustomResultJson(new RequstResult()
            {
                Msg = "操作成功",
                Code = 0,
            });
        }

    }
}
