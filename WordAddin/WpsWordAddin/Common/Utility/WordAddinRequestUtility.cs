using Common.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Common.Utility
{
    /// <summary>
    /// word插件请求工具类
    /// </summary>
    public class WordAddinRequestUtility
    {
        /// <summary>
        /// 处理Word插件请求信息
        /// </summary>
        /// <param name="requestUrlInfo">插件请求路径信息</param>
        /// <param name="requestAddinWord">请求类型</param>
        public bool  WordAddinRequestHandle(string requestUrlInfo )
        {
            if (requestUrlInfo.EndsWith("json"))
            {

                RequstHandle(requestUrlInfo);
                  
                return true;
            }
            else
            {
                return false;
            }
            
        }

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="requestUrlInfo">请求信息</param>
        ///// <param name="requestAddinWord">请求类型</param>
        public void RequstHandle(string requestUrlInfo) 
        {
            try
            {
                var tempUrlArray = StringUtility.Strint2ArrayString(requestUrlInfo, "&");
                int enumValue = EnumUtility.GetValueByRequestType(typeof(ContTextOption), tempUrlArray[0]);
                StoreData.contTextOption = (ContTextOption)enumValue;
                //StoreData.requestType2 = (int)requestAddinWord;
                string paramstr = tempUrlArray[1];//.TrimStart('\"').TrimEnd('\"');//请求参数列表
                QueryStringInfo Info = QueryStringInfo.GetRequstQureyInfo(paramstr); //JsonHelper.DeserializeJsonToObject<QueryStringInfo>(paramstr);由于前段不同浏览器传递过来的值双引号可能丢失
                if (Info.addinVar.Contains(StoreData.PlinVisNo))
                {
                    StoreData.RequestBaseUrl = Info.baseAddr;
                    StoreData.requestTempId = Info.fId;
                    StoreData.requestUserId = Info.uId;
                    StoreData.RequestOptionType = Info.wT;
                    StoreData.contTextOptionValue = enumValue;
                    StoreData.comTextId = Info.sId;
                    WriteINIFile(requestUrlInfo);
                    //加载插件
                    WordAddinComHelper.WordAddinLoad(WordShare.AddInCom_ProgId);
                    var appvisable = GetAppVisble(Info.wT);
                    WordUtility.CreateWordApp(appvisable);
                }
                else
                {

                    MessageBox.Show("您所使用的插件版本不匹配！");
                }
            }
            catch (Exception ex)
            {

                LogUtility.WriteLog(typeof(WordAddinRequestUtility),ex);
            }
           
        
        }
        private bool GetAppVisble(string wT)
        {
            bool visble = true;
            switch (wT)
            {
                case "saveWord":
                case "savePdf":
                    visble = false;
                    break;
                default:
                    break;
               
            }
            return visble;

        }

        /// <summary>
        /// 将请求信息写入INI文件
        /// </summary>
        /// <param name="opionclass">请求类别</param>
        /// <param name="urlarray">请求URL</param>
        private void WriteINIFile(string requestUrlInfo)
        {
            
            var iniPath = INIHelper.GetOptionIni();//.InIPath;
            //IOUtility.CreateFile(iniPath);

            INIHelper.Write("TempOption", "BaseUrl", StoreData.RequestBaseUrl, iniPath);
            INIHelper.Write("TempOption", "OpionClass", StoreData.contTextOptionValue.ToString(), iniPath);
            INIHelper.Write("TempOption", "UserId", StoreData.requestUserId.ToString(), iniPath);
            INIHelper.Write("TempOption", "TempId", StoreData.requestTempId.ToString(), iniPath);//文本ID
            INIHelper.Write("TempOption", "RequestOptionType", StoreData.RequestOptionType.ToString(), iniPath);
            INIHelper.Write("TempOption", "comTextId", StoreData.comTextId.ToString(), iniPath);
            INIHelper.Write("TempOption", "addinUrl", requestUrlInfo, iniPath);
            INIHelper.Write("TempOption", "RequestOptionType2", StoreData.requestType2.ToString(), iniPath);
            
        }
    }
}
