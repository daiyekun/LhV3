using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using Tool = Microsoft.Office.Tools;
using Common.Model;

namespace WordAddIn.ClassLib.WordAddinClass
{
    /// <summary>
    /// 插件承载实体对象
    /// </summary>
   public class AddInDataInfo
    {
       /// <summary>
       /// 当前任务窗口集合
       /// </summary>
       public Tool.CustomTaskPaneCollection taskPaneCollection { get; set; }
       /// <summary>
       /// 系统变量
       /// </summary>
       public IList<ContractVariable> SysVariable { get; set; }
       /// <summary>
       /// 自定义变量
       /// </summary>
       public IList<ContractVariable> CustomVariable { get; set; }
    }

    /// <summary>
    /// 承载合同模板变量
    /// </summary>
   public class VariableData 
   {
       /// <summary>
       /// 系统变量
       /// </summary>
       public IList<ContractVariable> SysVariable { get; set; }
       /// <summary>
       /// 自定义变量
       /// </summary>
       public IList<ContractVariable> CustomVariable { get; set; }
   
   
   }
}
