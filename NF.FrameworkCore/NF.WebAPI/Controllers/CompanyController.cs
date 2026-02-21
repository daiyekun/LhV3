using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Utility;
using NF.IBLL;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.WebAPI.Utility;
using NF.WebAPI.Utility.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NF.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class CompanyController : ControllerBase
    {
        private IProjectManagerService _IProjectManagerService;
        private ICompanyService _ICompanyService;
        private IUserInforService _IUserInforService;
        public CompanyController(ICompanyService ICompanyService
       , IUserInforService IUserInforService
       , IProjectManagerService IProjectManagerService)
        {
            _ICompanyService = ICompanyService;
            _IUserInforService = IUserInforService;
            _IProjectManagerService = IProjectManagerService;
        }

        // GET: api/<CompanyController>
        [HttpGet("ComList")]
        public IEnumerable<NF.ViewModel.Models.CompanyViewDTO> Get()
        {
            PageparamInfo pageParam = new PageparamInfo();
            var pageInfo = new PageInfo<Model.Models.Company>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<Model.Models.Company>();
           // predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, pageParam));
            if (pageParam.selitem)
            {//选择框
                predicateAnd = predicateAnd.And(a => a.Cstate == (int)CompStateEnum.Audited);
            }
            if (!string.IsNullOrEmpty(pageParam.jsonStr))
            {//高级查询
                predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvQueryCompany(pageParam, _IUserInforService));
            }
            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//基本筛选
                predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvJBSXQueryCompany(pageParam, _IUserInforService, _IProjectManagerService));
            }
            Expression<Func<Model.Models.Company, object>> orderbyLambda = null;
            bool IsAsc = false;
            switch (pageParam.orderField)
            {
                case "Name":
                    orderbyLambda = a => a.Name;

                    break;
                case "Code":
                    orderbyLambda = a => a.Code;
                    break;
                default:
                    orderbyLambda = a => a.Id;
                    break;

            }
            if (pageParam.orderType == "asc")
                IsAsc = true;
            var layPage = _ICompanyService.GetList(pageInfo, predicateAnd, orderbyLambda, IsAsc);
            var sd = layPage.data.ToList();
            return layPage.data.ToList();

           // return new string[] { "value1", "value2" };
        }
       
        // GET api/<CompanyController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CompanyController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CompanyController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CompanyController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
