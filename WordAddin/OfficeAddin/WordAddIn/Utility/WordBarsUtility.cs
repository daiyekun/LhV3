using Common.Model;
using Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordAddIn.Utility
{
    /// <summary>
    /// word Bars相关操作
    /// </summary>
   public  class WordBarsUtility
    {
        #region

        /// <summary>
        /// 执行CommbandBar
        /// </summary>
        public static void ExcuteCommbandBarMso(string commandBarMso, bool CurrentBarState = true)
        {
            try
            {
                if (CurrentBarState)
                    ExecuteMsoByCurrentStateTure(commandBarMso);
                else
                    ExecuteMsoByCurrentStateFalse(commandBarMso);
            }
            catch (Exception ex)
            {
                LogUtility.WriteLog(typeof(WordBarsUtility), ex);
               
            }
        }
        /// <summary>
        /// 当前按钮状态已经被点击
        /// </summary>
        /// <param name="commandBarMso"></param>
        private static void ExecuteMsoByCurrentStateTure(string commandBarMso)
        {
            if (WordShare.WordApp.CommandBars.GetPressedMso(commandBarMso))
            {

                WordShare.WordApp.CommandBars.ExecuteMso(commandBarMso);
            }
        }
        /// <summary>
        /// 当前Bar没有被点击
        /// </summary>
        /// <param name="commandBarMso"></param>
        private static void ExecuteMsoByCurrentStateFalse(string commandBarMso)
        {
            if (!WordShare.WordApp.CommandBars.GetPressedMso(commandBarMso))
            {

                WordShare.WordApp.CommandBars.ExecuteMso(commandBarMso);
            }
        }

        #endregion
    }
}
