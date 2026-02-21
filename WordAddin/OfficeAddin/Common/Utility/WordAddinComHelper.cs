using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Utility
{
    /// <summary>
    /// Word插件帮助类
    /// </summary>
    public class WordAddinComHelper
    {

        /// <summary>
        /// 返回Word插件Keys
        /// </summary>
        /// <returns></returns>
        private static string GetWordAddinKeys(string ProgId)
        {
            return "Software/Microsoft/Office/Word/Addins/" + ProgId;
        }

        /// <summary>
        /// 加载插件
        /// </summary>
        public static int WordAddinLoad(string wordAddInComProgId)
        {
            var keys = GetWordAddinKeys(wordAddInComProgId);
            return RegistryHelper.AddinLoad(keys, Registry.CurrentUser);

        }

        /// <summary>
        /// 加载插件
        /// </summary>
        public static int WordAddinLoading(string wordAddInComProgId)
        {
            var keys = GetWordAddinKeys(wordAddInComProgId);
            return RegistryHelper.AddinLoading(keys, Registry.CurrentUser);

        }

        /// <summary>
        /// 卸载当前指定word载插件
        /// </summary>
        public static int WordAddinUnload(string wordAddInComProgId)
        {
            string keys = GetWordAddinKeys(wordAddInComProgId);
            return RegistryHelper.AddinUnload(keys, Registry.CurrentUser);
        }

        /// <summary>
        /// 加载插件
        /// </summary>
        public static int GetLoadBehaviorValue(string wordAddInComProgId)
        {
            var keys = GetWordAddinKeys(wordAddInComProgId);
            return RegistryHelper.GetLoadBehaviorValue(keys, Registry.CurrentUser);

        }

    }
}
