using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using NF.Model.Models;

namespace NF.BLL
{
    /// <summary>
    /// 国家
    /// </summary>
    public partial class CountryService
    {

        /// <summary>
        /// 返回3级联动数据集合(国家\省\市)
        /// </summary>
        /// <returns></returns>
        public IList<AddressDTO> GetAddress()
        {
            IList<AddressDTO> listAddress = new List<AddressDTO>();
            //国家
            var listCountry = GetQueryable(a => 1 == 1).ToList();
            //省
            var listProvince = Db.Set<Province>().ToList();
            //市
            var listCity = Db.Set<City>().ToList();
            foreach (var country in listCountry)
            {
                var ct = new AddressDTO
                {
                    Code = country.Id.ToString(),
                    Name = country.DisplayName,
                };
                ct.Childs = AddProvince(listProvince, listCity, country.Id);
                listAddress.Add(ct);
            }

            return listAddress;


        }
        /// <summary>
        /// 添加省
        /// </summary>
        /// <param name="listlistProvince">省数据</param>
        /// <param name="listcity">市数据</param>
        /// <param name="countryId">国家ID</param>
        private IList<AddressDTO> AddProvince(IList<Province> listlistProvince,IList<City> listcity, int countryId)
        {
            var listprvs = listlistProvince.Where(a => a.CountryId == countryId).ToList();
            IList<AddressDTO> listProvs = new List<AddressDTO>();
            foreach (var prv in listprvs)
            {
                var add_pv = new AddressDTO {

                    Code = prv.Id.ToString(),
                    Name = prv.DisplayName,
                };
                add_pv.Childs = AddCity(listcity, prv.Id);
                listProvs.Add(add_pv);
            }

            return listProvs;




        }

        /// <summary>
        /// 添加省
        /// </summary>
        /// <param name="listcity">市数据集合</param>
        /// <param name="ProvinceId">省ID</param>
        private IList<AddressDTO> AddCity(IList<City> listcity, int ProvinceId)
        {
            var listcitys = listcity.Where(a => a.ProvinceId == ProvinceId).ToList();
            IList<AddressDTO> listCitys = new List<AddressDTO>();
            foreach (var prv in listcitys)
            {
                var add_city = new AddressDTO
                {

                    Code = prv.Id.ToString(),
                    Name = prv.DisplayName,
                };
                listCitys.Add(add_city);

            }

            return listCitys;




        }
    }
}
