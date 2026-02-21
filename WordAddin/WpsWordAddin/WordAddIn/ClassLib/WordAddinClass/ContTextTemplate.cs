using Common.Model;
using Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordAddIn.Utility;

namespace WordAddIn.ClassLib.WordAddinClass
{
    /// <summary>
    /// 合同模板类
    /// 处理：lhwcms://contractTpl/
    /// </summary>
    public class ContTextTemplate : WordAddinBase, IDocOption
    {
        /// <summary>
        /// 写入异常
        /// </summary>
        /// <param name="ex"></param>
        private static void WriteLog(Exception ex)
        {
            LogUtility.WriteLog(typeof(ContTextTemplate), ex);

        }
        /// <summary>
        /// 写入异常在返回
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private static string LogToResult(Exception ex, string msg = "")
        {
            WriteLog(ex);
            return string.IsNullOrEmpty(msg) ? "ERR" : msg;
        }



        /// <summary>
        /// 保存Word
        /// </summary>
        /// <param name="wordFullName">当前Word全路径名称</param>
        /// <returns>返回服务器响应数据</returns>
        public  string WordSave(string wordFullName="")
        {
            try
            {

                //请求路径
                string wordPath = wordFullName;
                var HttprequestUrl = UrlDataUtility.GetOptionUrl(CreateUrlType.Temp);
                StringBuilder strb = new StringBuilder(HttprequestUrl);
                strb.Append("Savetemplate");
                strb.Append("?cmd=savetemplate");
                strb.Append("&uid=" + WordShare.UserId);//登录人ID
                strb.Append("&tplid=" + WordShare.TempId);//模板历史ID

                if (string.IsNullOrEmpty(wordFullName))
                {
                    var activeDocument = WordShare.WordApp.ActiveDocument;
                    activeDocument.Save();
                    wordPath = activeDocument.FullName;
                }
                return HttpWebRequestOptionFile.MyUploader(wordPath, strb.ToString());

            }
            catch (Exception ex)
            {

               return LogToResult(ex);
            }
           

        }
        /// <summary>
        /// 初始化Word状态
        /// </summary>
        public override void InitWordStyle(AddInDataInfo addInDataInfo)
        {
            try
            {
                //MessageUtility.ShowMsg("ContTextTemplate");
                base.WordAddinOpenDocument();
                if (WordShare.requestWordType == requestAddinWordType.TplonreadOrwrite)
                {
                    base.AddTaskPane(addInDataInfo.taskPaneCollection);

                    WordBarsUtility.ExcuteCommbandBarMso("DesignMode", false);
                }
                else if (WordShare.requestWordType == requestAddinWordType.Tplonreadonly)
                {
                    WordDocumentHelper.DocProtectOrUnProtect(0);

                }
            }
            catch (Exception ex)
            {

                WriteLog(ex);
            }
        }
    }
}
