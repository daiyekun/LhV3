using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel
{
   public class OpeningSituationInforDTO: OpeningSituationInfor
    {
        //public int Id { get; set; }
       // public int TenderId { get; set; }
      //  public string OpenSituationName { get; set; }
     //   public int Unit { get; set; }
       public string UnitName { get; set; }
      //  public decimal TotalPrice { get; set; }
        public string TotalPricethis { get; set; }
      //  public decimal Uitprice { get; set; }
        public string Uitpricethis { get; set; }
     //   public int UserId { get; set; }
        public string UserName { get; set; }
       //public int IsDelete { get; set; }
    }
    public class OpeningSituationInforViewDTO
    {
        public int Id { get; set; }
        public string OpenSituationName { get; set; }
        public int Unit { get; set; }
        public string UnitName { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Uitprice { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
