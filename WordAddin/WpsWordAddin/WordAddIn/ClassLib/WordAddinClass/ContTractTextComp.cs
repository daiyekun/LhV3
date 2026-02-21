using Common.Model;
using Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordAddIn.Utility;
using WordAddIn2010.Unttitily;

namespace WordAddIn.ClassLib.WordAddinClass
{
    /// <summary>
    /// 比对
    /// </summary>
    public class ContTractTextComp : WordAddinBase
    {
        public override void InitWordStyle(AddInDataInfo addInDataInfo)
        {

            CompareWord();



        }
       

        private void CompareWord()
        {
            
            try
            {
                WordDownloadUtility opwordUtility = new WordDownloadUtility();
                var firstFileurl = WordShare.BaseUrl + opwordUtility.GetCurrentWordUrl(WordShare.TempId, WordShare.UserId, ContTextOption.contractTextCmp);
                var secondFileurl = WordShare.BaseUrl + opwordUtility.GetCurrentWordUrl(WordShare.CompareTextId, WordShare.UserId, ContTextOption.contractTextCmp);
                var firstFilePath = IOUtility.GetFilePath("firstword");
                var scondFilePath = IOUtility.GetFilePath("scondword");
                var compareReultWord = IOUtility.GetFilePath("comparereultword");
                //下载两文件到本地
                HttpWebRequestOptionFile.Download(firstFileurl, firstFilePath);
                HttpWebRequestOptionFile.Download(secondFileurl, scondFilePath);
                // MessageUtility.ShowMsg("11");
                WordCmp.CompWord(WordShare.WordApp, firstFilePath, scondFilePath);
                //CompareWordFile(firstFilePath, scondFilePath, compareReultWord);
                //LogUtility.WriteDebug(typeof(ContractTextCmp),compareReultWord);
                //return compareReultWord;
            }
            catch (Exception ex)
            {

                LogUtility.WriteLog(typeof(ContTractTextComp), ex);
            }

        }
    }
}
