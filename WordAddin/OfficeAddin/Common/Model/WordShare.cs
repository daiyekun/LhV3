using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Common.Model
{   
    /// <summary>
    /// 零时存储数据类
    /// </summary>
    public class WordShare
    {
        
        //获取应用程序
        public static Microsoft.Office.Interop.Word.Application WordApp;
        /// <summary>
        /// 存放地址URL
        /// </summary>
        public static string BaseUrl;
        /// <summary>
        /// 登录ID
        /// </summary>
        public static int UserId = 0;
        /// <summary>
        /// 登录名称
        /// </summary>
        public static string UserName=string.Empty;

        /// <summary>
        /// 模板ID
        /// </summary>
        public static int TempId;
        /// <summary>
        /// 插件描述不符关键字
        /// </summary>
        public static string AddInCom_ProgId = "Lh.LhWordAddin";
        /// <summary>
        /// 操作类别
        /// </summary>
        public static string OptionClass;
        /// <summary>
        /// 是否从word登录
        /// </summary>
        public static bool IsWordToLogin = false;
        /// <summary>
        /// 系统变量ListBox
        /// </summary>
        public static ListBox lisBoxSysVal;
        /// <summary>
        /// 自定义变量ListBox
        /// </summary>
        public static ListBox lisBoxCustomVal;

        /// <summary>
        /// 文档密码
        /// </summary>
        public static string WordPassword = "95B13B93E52C7C1FD2D2A1F341844C71QAZ";
        /// <summary>
        /// 请求插件执行的Word类型
        /// </summary>
        public static requestAddinWordType requestWordType;
        /// <summary>
        /// Word操作枚举
        /// </summary>
        public static ContTextOption contTextOption;
        /// <summary>
        /// 比较合同文本ID
        /// </summary>
        public static int CompareTextId;
        /// <summary>
        /// 请求URL
        /// </summary>
        public static string requestUrl;
        /// <summary>
        /// 请求类型
        /// </summary>
        public static int requestType2;
    }
}
