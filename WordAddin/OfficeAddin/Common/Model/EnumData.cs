using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Model
{
    /// <summary>
    /// Word操作枚举
    /// </summary>
    [EnumClass(Min = 0, Max = 4)]
    public enum ContTextOption
    {
        /// <summary>
        /// 模板操作
        /// </summary>
        [EnumContTextOptionAttribute(Desc = "模板操作", RequestType = "lhaddin://contractTpl/", Value = 0, ClassType = null)]
        contractTpl = 0,
        /// <summary>
        /// 文本起草
        /// </summary>
        [EnumContTextOptionAttribute(Desc = "文本起草", RequestType = "lhaddin://contractText/", Value = 1, ClassType = null)]
        contractText = 1,
        /// <summary>
        /// 文本审阅
        /// </summary>
        [EnumContTextOptionAttribute(Desc = "文本审阅", RequestType = "lhaddin://contractReview/", Value = 2, ClassType = null)]
        contractReview = 2,
        /// <summary>
        /// 文本下载
        /// </summary>
        [EnumContTextOptionAttribute(Desc = "文本下载", RequestType = "lhaddin://contractFinalProcess/", Value = 3, ClassType = null)]
        contractFinalProcess = 3,
        /// <summary>
        /// 文本对比
        /// </summary>
        [EnumContTextOptionAttribute(Desc = "文本对比", RequestType = "lhaddin://contractTextCmp/", Value = 4, ClassType = null)]
        contractTextCmp = 4

    }
    /// <summary>
    /// 创建URL枚举
    /// </summary>
     [EnumClass(Min = 0, Max = 7)]
    public enum CreateUrlType 
    {   
         /// <summary>
         /// 模板操作
         /// </summary>
         [EnumItem(Value = 0, Desc = "模板操作")]
         Temp=0,
         /// <summary>
         /// 文本起草
         /// </summary>
         [EnumItem(Value = 1, Desc = "文本起草")]
         TextDraft=1,
         /// <summary>
         /// 文本审阅
         /// </summary>
           [EnumItem(Value = 2, Desc = "文本审阅")]
         TextReview=2,
           /// <summary>
           /// 文本下载
           /// </summary>
           [EnumItem(Value = 3, Desc = "文本下载")]
           TextDownload = 3,
           /// <summary>
           /// 文本水印
           /// </summary>
           [EnumItem(Value = 4, Desc = "文本水印")]
           WaterMark = 4,
           /// <summary>
           /// 文本对比
           /// </summary>
           [EnumItem(Value = 5, Desc = "文本对比")]
           TextCmp = 5,
          
           /// <summary>
           /// 根目录请求-通过Cmd参数配置具体请求
           /// </summary>
           [EnumItem(Value = 6, Desc = "根目录请求")]
           DraftBaseUrl = 6,

    
    }
    /// <summary>
    /// 请求插件执行Word状态
    /// </summary>
    [EnumClass(Min = -1, Max = 20)]
     public enum requestAddinWordType 
     {
        /// <summary>
        /// 无
        /// </summary>
        [EnumItem(Value = -1, Desc = "None")]
        None = -1,
        /// <summary>
        /// 模板建立
        /// </summary>
        [EnumItem(Value = 0, Desc = "TplonreadOrwrite")]
         TplonreadOrwrite=0,
        /// <summary>
        /// 文本起草
        /// </summary>
        [EnumItem(Value = 1, Desc = "TextDraft")]
        TextDraft = 1,
         /// <summary>
         /// 模板预览
         /// </summary>
         /// 
        [EnumItem(Value = 2, Desc = "Tplonreadonly")]
        Tplonreadonly = 2,
        /// <summary>
        /// 起草预览
        /// </summary>
        [EnumItem(Value=3,Desc="conttext_readonly")]
        conttext_readonly=3,
        /// <summary>
        ///历史版本预览
        /// </summary>
        [EnumItem(Value = 4, Desc = "history_readonly")]
        history_readonly = 4,
        /// <summary>
        ///审阅-允许修改文本
        /// </summary>
        [EnumItem(Value = 5, Desc = "editable")]
        editable = 5,
        /// <summary>
        ///审阅-不允许修改文本
        /// </summary>
        [EnumItem(Value = 6, Desc = "contractReview")]
        contractReview = 6,
        /// <summary>
        ///生成Word文件
        /// </summary>
        [EnumItem(Value = 7, Desc = "saveWord")]
        saveWord = 7,
        /// <summary>
        ///生成PDF文件
        /// </summary>
        [EnumItem(Value = 8, Desc = "savePdf")]
        savePdf = 8,
        /// <summary>
        ///文本比对
        /// </summary>
        [EnumItem(Value = 9, Desc = "wdCompare")]
        wdCompare = 9,
        /// <summary>
        ///打开对比结果文件
        /// </summary>
        [EnumItem(Value = 10, Desc = "wdOpenCompare")]
        wdOpenCompare = 10,




    }

    
}
