using Common.Model;
using Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WordAddIn.ClassLib
{
    /// <summary>
    /// 初始化帮助类
    /// </summary>
    public class InIDataHipler
    {
        /// <summary>
        /// 初始化请求数据
        /// </summary>
        public static bool ReadINIToData()
        {

            try
            {
                var inipath = INIHelper.GetOptionIni();
                if (System.IO.File.Exists(inipath))
                {
                    WordShare.BaseUrl = INIHelper.Read("TempOption", "BaseUrl", inipath);
                    var tempuserid = INIHelper.Read("TempOption", "UserId", inipath);
                    WordShare.UserId = string.IsNullOrEmpty(tempuserid) ? 0 : int.Parse(tempuserid);
                    var tempTempId = INIHelper.Read("TempOption", "TempId", inipath);
                    WordShare.TempId = string.IsNullOrEmpty(tempTempId) ? 0 : int.Parse(tempTempId);
                    var optionclass = INIHelper.Read("TempOption", "OpionClass", inipath);
                    WordShare.OptionClass =EnumUtility.GetRequestType(typeof(ContTextOption), Convert.ToInt32(optionclass));
                    WordShare.contTextOption = (ContTextOption)Convert.ToInt32(optionclass);
                    var RequestOptionType = INIHelper.Read("TempOption", "RequestOptionType", inipath);
                    //读取请求word插件执行的文档类型
                    WordShare.requestWordType = (requestAddinWordType)EnumUtility.GetValueByDesc(typeof(requestAddinWordType), RequestOptionType);
                    var comTextId= INIHelper.Read("TempOption", "comTextId", inipath);
                    //请求URL
                    WordShare.requestUrl= INIHelper.Read("TempOption", "addinUrl", inipath);
                    WordShare.CompareTextId = string.IsNullOrEmpty(comTextId) ? 0 : Convert.ToInt32(comTextId);
                    var rType2= INIHelper.Read("TempOption", "RequestOptionType2", inipath);
                    WordShare.requestType2 = string.IsNullOrEmpty(rType2) ? 0 : Convert.ToInt32(rType2);
                    if (System.IO.File.Exists(inipath))
                    {
                        System.IO.File.Delete(inipath);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.WriteLog(typeof(InIDataHipler), ex);
                return false;


            }


        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        public static void WordLogin(int usrId)
        {
            
            
                StringBuilder strb = new StringBuilder();
                //strb.Append("?cmd=/ContractTpl/SignInFromWord");
                string cmdstr = "SignInFromWord";
                strb.Append("?cmd=SignInFromWord&uid=" + usrId);
                var responsestr = HttpRequestUtility.SubmitPostRequest(strb.ToString(), CreateUrlType.DraftBaseUrl, cmdstr);
                var User = JsonHelper.DeserializeJsonToObject<User>(responsestr);
                WordShare.UserName = User.Name;
                WordShare.IsWordToLogin = true;
            
            


        }
    }
}
