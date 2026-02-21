using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordAddIn.ClassLib.WordAddinClass;
using System.Threading;
using System.Windows.Forms;
using Common.Utility;

namespace WordAddIn.ClassLib
{
    /// <summary>
    /// 类型创建工厂类
    /// </summary>
    public class WordHandleFactory
    {
        /// <summary>
        /// 创建文档操作类型实例
        /// </summary>
        /// <param name="contTextOption">请求插件类型</param>
        /// <param name="docType">文档状态</param>
        /// <returns></returns>
        public static  WordAddinBase CreateWordExeInstance()
        {
            //MessageUtility.ShowMsg(WordShare.contTextOption.ToString());
            switch (WordShare.contTextOption)
            {
               
                case ContTextOption.contractTpl:
                    return new ContTextTemplate();
                case ContTextOption.contractText:
                     return new ContractTextDraft();
                case ContTextOption.contractReview:
                     return new ContractTextReview();
                case ContTextOption.contractFinalProcess:
                     return new ContractFinalProcess();
                case ContTextOption.contractTextCmp:
                    return new ContTractTextComp(); //ContractTextCmp();


                default:
                    return default(WordAddinBase);


            }
        
        }

        /// <summary>
        /// 创建文档操作类型实例
        /// </summary>
        /// <param name="contTextOption">请求插件类型</param>
        /// <param name="docType">文档状态</param>
        /// <returns></returns>
        public static IDocOption CreateIOptionInstance()
        {
           
            switch (WordShare.contTextOption)
            {
                case ContTextOption.contractTpl:
                    return new ContTextTemplate();
                case ContTextOption.contractText:
                    {
                        
                        return new ContractTextDraft();
                    }
                
                default:
                    return default(IDocOption);


            }

        }
       

        /// <summary>
        /// 创建泛型实例
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <returns>泛型实体对象</returns>
        public static T CreateInstance<T>() where T : WordAddinBase,new()
        {
            return new T();
        }

    }
}
