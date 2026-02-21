using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpsCommon
{
    public class WpsWordUtility
    {
        /// <summary>
        /// 新建建Word应用程序实例
        /// </summary>
        /// <param name="appVisible">是否显示窗体</param>
        /// <param name="caption">标题</param>
        public static Word.Application CreateWordApp(bool appVisible = true, string caption = "")
        {
            Word.Application wordApp = null;
            try
            {

                wordApp = new Word.Application();

                if (!string.IsNullOrEmpty(caption))
                {
                    wordApp.Caption = caption;

                }
                else
                {
                    wordApp.Caption = "WordAddin";
                }
                wordApp.Visible = appVisible;
            }
            catch (Exception ex)
            {

                if (wordApp != null)
                {
                    wordApp.Quit();
                }
                //LogUtility.WriteLog(typeof(WordUtility), ex);

            }

            return wordApp;
        }
    }
}
