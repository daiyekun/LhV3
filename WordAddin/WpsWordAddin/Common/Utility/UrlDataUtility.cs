using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Utility
{
    /// <summary>
    /// URL路径数据创建类
    /// </summary>
    public class UrlDataUtility
    {
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public UrlDataUtility() { 
        
        }
        ///// <summary>
        ///// 构造函数
        ///// </summary>
        ///// <param name="createUrlType">创建类型枚举</param>
        //public UrlDataUtility(CreateUrlType createUrlType) 
        //{
        //    _CreateUrlType = createUrlType;
        //}

        /// <summary>
        /// 获取操作模板的Url
        /// </summary>
        /// <returns></returns>
        public static  string GetOptionUrl(CreateUrlType createUrlType)
        {
            string tempbaseurl = string.IsNullOrEmpty(StoreData.RequestBaseUrl) ? WordShare.BaseUrl : StoreData.RequestBaseUrl;
            switch (createUrlType)
            {

                case CreateUrlType.Temp://建立模板
                    return tempbaseurl + "ContractDraft/ContractTpl/";
                case CreateUrlType.TextDraft://模板起草
                    return tempbaseurl + "ContractDraft/ContTextDraft/";
                case CreateUrlType.TextReview://文本审阅
                    //return tempbaseurl + "ContractDraft/ContractReview/GetContractText?cmd=/ContractReview/getContractText";
                    return tempbaseurl + "ContractDraft/ContractReview/";
                case CreateUrlType.TextDownload://合同文本下载
                    //return tempbaseurl + "/BLL/ModuleBusiness/Draft/Operation.aspx?cmd=/ContractAuthoring/getContractTextTemplate";
                    return tempbaseurl + "ContractDraft/ContTextDraft/";
                case CreateUrlType.WaterMark://获取水印
                    return tempbaseurl + "/BLL/ModuleBusiness/Draft/Operation.aspx?cmd=/ContractAuthoring/getWordWaterMark";
                case CreateUrlType.TextCmp://合同文本对比时下载
                    //return tempbaseurl + "/BLL/ModuleBusiness/Draft/Operation.aspx?cmd=/ContractTextCmp/GetContractTextVersionedDoc";
                    return tempbaseurl + "ContractDraft/ContractTextCmp/";
                case CreateUrlType.DraftBaseUrl://起草请求根目录
                    return tempbaseurl + "ContractDraft/DraftComm/";
                   
                //case CreateUrlType.SysVal://系统变量请求
                //    return tempbaseurl +"/BLL/ModuleBusiness/Draft/Operation.aspx?cmd=/ContractAuthoring/getCustomVariables";
                //case CreateUrlType.CustomVal://自定义变量请求
                //    return tempbaseurl + "/BLL/ModuleBusiness/Draft/Operation.aspx?cmd=/ContractAuthoring/getCustomVariables";
                   
                default:
                    return "";
            }
        }
    }
}
