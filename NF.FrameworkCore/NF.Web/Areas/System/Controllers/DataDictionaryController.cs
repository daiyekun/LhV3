using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Utility;
using NF.Web.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Filters;

namespace NF.Web.Areas.System.Controllers
{
    [Area("System")]
    [Route("System/[controller]/[action]")]
    public class DataDictionaryController : NfBaseController//Controller
    {
        private IDataDictionaryService _IDataDictionaryService;
        private IMapper _mapper;
        public DataDictionaryController(IDataDictionaryService iDataDictionaryService, IMapper mapper)
        {
            _IDataDictionaryService = iDataDictionaryService;
            _mapper = mapper;
        }
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();

        }
        /// <summary>
        /// 获取字典类型
        /// </summary>
        /// <returns></returns>
        public IActionResult DataDicTypes()
        {

            var list = _IDataDictionaryService.GetListTypes();
            RequstResult requestRel = new RequstResult
            {
                Code = 0,
                Data = DataTypesInfo.GetInfo(list)
            };
            return new CustomResultJson(requestRel);
        }

        /// <summary>
        /// 获取字典
        /// </summary>
        /// <param name="limit">每页显示条数</param>
        /// <param name="page">当前页码</param>
        /// <param name="dType">字典类别</param>
        /// <returns></returns>
        public IActionResult GetList(int? limit, int? page, int? dType)
        {
            var _pageIndex = (page ?? 1) <= 0 ? 1 : (page ?? 1);
            var pageInfo = new PageInfo<DataDictionary>(pageIndex: _pageIndex, pageSize: limit ?? 20);
            var predicateAnd = PredicateBuilder.True<DataDictionary>();
            var predicateOr = PredicateBuilder.False<DataDictionary>();
            var _dType = (dType ?? -1) >= 0 ? dType : -1;
            predicateAnd = predicateAnd.And(p => p.DtypeNumber == _dType);
            predicateAnd = predicateAnd.And(p => p.IsDelete !=1);
            var layPage = _IDataDictionaryService.GetList(pageInfo, predicateAnd);
            return new CustomResultJson(layPage);

          

        }
        /// <summary>
        /// 创建
        /// </summary>
        /// <returns></returns>
        public IActionResult Build()
        {
            return View();
        }
        /// <summary>
        /// 数据字典保存
        /// </summary>
        /// <param name="info">保存数据字典对象</param>
        /// <returns></returns>
        public IActionResult Save(DataDictionary info)
        {

            info.IsDelete = 0;
            if (info.Id > 0)
            {

                _IDataDictionaryService.Update(info);
            }
            else
            {
                _IDataDictionaryService.Add(info);
            }
           // var dicDto=_mapper.Map<DataDictionary, DataDictionaryDTO>(info);
            //SysIniInfoUtility.SetDataDic(dicDto, StaticData.RedisDataKey, (key, Id, n) => {
            //    return $"{key}:{Id}:{n}";
            //});
            _IDataDictionaryService.SetRedis();
            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0



            });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids">要删除的ID集合1,2,3</param>
        /// <returns></returns>
        public IActionResult Delete(string Ids)
        {

            _IDataDictionaryService.Delete(Ids);
            //加入队列
            RedisHelper.ListRightPush(StaticData.RedisDataDelKey, Ids);
            _IDataDictionaryService.SetRedis();
            return new CustomResultJson(new RequstResult()
            {
                Msg = "删除成功",
                Code = 0


            });
        }
        /// <summary>
        /// 根据枚举值查询枚举下所有类别列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetListByDataEnumType(int dataEnum)
        {
            var enumInfo = (DataDictionaryEnum)dataEnum;
            return new CustomResultJson(
                new RequstResult()
                {
                    Msg = "",
                    Code = 0,
                    Data= _IDataDictionaryService.GetListByDataEnumType(enumInfo)


                }
                
                );
        }

        /// <summary>
        /// 显示页面信息-主要用于修改和查看
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public IActionResult ShowView(int Id)
        {
            if (Id == -100)
            {//自己修改基本信息
                Id = SessionCurrUserId;
            }
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = _IDataDictionaryService.ShowView(Id)


            });

        }

        /// <summary>
        /// 选择类别树
        /// </summary>
        /// <returns></returns>
        public IActionResult GetTreeSelectData(int dataType)
        {

            return new CustomResultJson(_IDataDictionaryService.GetTreeSelectData((DataDictionaryEnum)dataType), (v) => { return v.Replace("Checked", "checked"); });



        }
    }
}