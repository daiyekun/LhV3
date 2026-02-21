using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Model
{
  public class ContractObjectTable
    {
      //[{"FIELD_TYPE":0,"BC_ID":0,"ExcelSubDocument":null,"BC_DESC":"","ExcelSubDocSheet":"","DETAIL_TITLE":"........20171219","CONFINE_NUM":-1}]
     
      public int FIELD_TYPE { get; set; }
      public int BC_ID { get; set; }
      public string ExcelSubDocument { get; set; }
      public string BC_DESC { get; set; }
      public string ExcelSubDocSheet { get; set; }
      public string DETAIL_TITLE { get; set; }
      public int CONFINE_NUM { get; set; }

    }
}
