using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.Web.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Filters;
using NF.Model.Models;
using NF.ViewModel.Models.Common;
using NF.ViewModel.Models.Utility;

namespace NF.Web.Areas.System.Controllers
{
    /// <summary>
    /// 币种管理
    /// </summary>
    [Area("System")]
    [Route("System/[controller]/[action]")]
    public class CurrencyManagerController : NfBaseController
    {
       
        private IMapper _IMapper { get; set; }
        private ICurrencyManagerService _ICurrencyManagerService;
        public CurrencyManagerController(ICurrencyManagerService ICurrencyManagerService, IMapper IMapper)
        {
            _ICurrencyManagerService = ICurrencyManagerService;
            _IMapper = IMapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 列表-
        /// </summary>
        /// <param name="pageParam">请求参数</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam)
        {
            var pageInfo = new PageInfo<CurrencyManager>(pageIndex: pageParam.page, pageSize: pageParam.limit);
           
            LayPageInfo<CurrencyManagerViewDTO> layPage = GetListData(pageInfo, pageParam.keyWord);
            return new CustomResultJson(layPage);

        }

        private LayPageInfo<CurrencyManagerViewDTO> GetListData(PageInfo<CurrencyManager> pageInfo,string keyWord)
        {
            var predicateAnd = PredicateBuilder.True<CurrencyManager>();
            var predicateOr = PredicateBuilder.False<CurrencyManager>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0);
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.ShortName.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            var layPage = _ICurrencyManagerService.GetList(pageInfo, predicateAnd, a => a.Id, false);
            return layPage;
        }
        /// <summary>
        /// 下拉币种数据源
        /// </summary>
        /// <returns></returns>
        public IActionResult GetSelectData()
        {
           var  pageInfo = new NoPageInfo<CurrencyManager>();
            var predicateAnd = PredicateBuilder.True<CurrencyManager>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0);
            var layPage = _ICurrencyManagerService.GetSelectList(pageInfo, predicateAnd, a => a.Id, true);
           
            return new CustomResultJson(
               new RequstResult()
               {
                   Msg = "",
                   Code = 0,
                   Data = layPage.data


               }

               );
        }

        /// <summary>
        /// 判断字段填写内容是否存在
        /// </summary>
        /// <param name="fieldInfo"></param>
        /// <returns></returns>
        public IActionResult CheckInputValExist(UniqueFieldInfo fieldInfo)
        {
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                RetValue = _ICurrencyManagerService.CheckInputValExist(fieldInfo)

            });
        }
        /// <summary>
        /// 新建页
        /// </summary>
        /// <returns></returns>
        public IActionResult Build()
        {
            
            return View();
        }

        [NfCustomActionFilter("新增币种", OptionLogTypeEnum.Add, "新增币种", true)]
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="info">保存信息对象</param>
        /// <returns>当前实体信息</returns>
        public IActionResult Save(CurrencyManagerDTO info)
        {


            var saveInfo = _IMapper.Map<CurrencyManager>(info);
            saveInfo.CreateDateTime = DateTime.Now;
            saveInfo.ModifyDateTime = DateTime.Now;
            saveInfo.CreateUserId = this.SessionCurrUserId;
            saveInfo.ModifyUserId = this.SessionCurrUserId;
            saveInfo.IsDelete = 0;
            _ICurrencyManagerService.Add(saveInfo);
            SetRedisInfo(info);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,


            });
        }
        /// <summary>
        /// 设置到Redis
        /// </summary>
        /// <param name="info">当前对象</param>
        private void SetRedisInfo(CurrencyManagerDTO info)
        {
            var redisinfo = _IMapper.Map<CurrencyManagerDTO, RedisCurrency>(info);
            SysIniInfoUtility.SetRedisHash(redisinfo, StaticData.RedisCurrencyKey, (key, Id) =>
            {
                return $"{key}:{Id}";
            });
        }

        [NfCustomActionFilter("修改币种", OptionLogTypeEnum.Update, "修改币种", true)]
        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="info">保存信息对象</param>
        /// <returns>当前实体信息</returns>
        public IActionResult UpdateSave(CurrencyManagerDTO info)
        {

            if (info.Id > 0)
            {
                var updateinfo = _ICurrencyManagerService.Find(info.Id);
                var updatedata = _IMapper.Map(info, updateinfo);
                updatedata.ModifyUserId = this.SessionCurrUserId;
                updatedata.ModifyDateTime = DateTime.Now;
                _ICurrencyManagerService.Update(updatedata);
            }
            SetRedisInfo(info);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,


            });
        }

        [NfCustomActionFilter("删除币种", OptionLogTypeEnum.Del, "删除币种", false)]
        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        public IActionResult Delete(string Ids)
        {
            var listIds = StringHelper.String2ArrayInt(Ids);
            var resinfo = new RequstResult(){ Msg = "删除成功！", Code = 0};
            _ICurrencyManagerService.Delete(Ids);
            _ICurrencyManagerService.SetRedis();
            RedisHelper.ListRightPush(StaticData.RedisDelCurrencyKey, Ids);
            return new CustomResultJson(resinfo);
        }
        /// <summary>
        /// 查看页面和修改页面赋值
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public IActionResult ShowView(int Id)
        {
            var info = _ICurrencyManagerService.ShowView(Id);
           return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = info


            });
        }
    }
}