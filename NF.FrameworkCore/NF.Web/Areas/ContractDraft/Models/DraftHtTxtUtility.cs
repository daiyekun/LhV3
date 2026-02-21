using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NF.Web.Areas.ContractDraft.Models
{
    public class DraftHtTxtUtility
    {
        ///// <summary>
        ///// 如果文件长度为0，则写入一个空文件（长度不为0）        
        ///// </summary>
        ///// <param name="FileName"></param>
        public static string  WriteEmptyWord(string FileName, string urlfilename)
        {
            var file = new FileInfo(FileName);
            if (!file.Exists || file.Length == 0)
            {
               
                urlfilename = "/wwwroot/Uploads/HtTemp/ContTextEmpty.docx";
            }
            
            return urlfilename;
        }

        
    }
}
