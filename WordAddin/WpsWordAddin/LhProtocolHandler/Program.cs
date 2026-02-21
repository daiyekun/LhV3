using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Model;
using System.Windows.Forms;
using Common.Utility;

namespace LhProtocolHandler
{
    /// <summary>
    /// 外界调用插件入口
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            
            try
            {
                MessageUtility.ShowMsg("测试");
                if (args.Length > 0)
                {
                    MessageUtility.ShowMsg("Main");
                    WordAddinRequestUtility requestUtility = new WordAddinRequestUtility();
                    var httprequestdata = System.Web.HttpUtility.UrlDecode(args[0], UTF8Encoding.Default);
                    requestUtility.WordAddinRequestHandle(httprequestdata);

                }
                else
                {
                    MessageBox.Show("请求插件信息错误", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Word插件加载失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                WordAddinComHelper.WordAddinUnload(WordShare.AddInCom_ProgId);
                LogUtility.WriteLog(typeof(Program), ex);

            }
        }
    }
}
