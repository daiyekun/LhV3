using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Utility
{
    /// <summary>
    /// 注册表帮助类
    /// </summary>
    public class RegistryHelper
    {

        /// <summary>
        /// 获取注册表KEY
        /// </summary>
        /// <param name="keys">keys:key1/key2/</param>
        /// <param name="rootKey">根节点:key</param>
        /// <returns>返回对应的注册表KEY对象</returns>
        public static RegistryKey GetRegKey(string keys, RegistryKey rootKey)
        {

            RegistryKey key = rootKey;
            if (string.IsNullOrEmpty(keys))
                return null;
            else
                return GetKey(keys, key);

        }

        /// <summary>
        ///根据指定key打开并获取
        /// </summary>
        /// <param name="keys">key集合keys:key1/key2</param>
        /// <param name="key">根据指定key获取当前key对象</param>
        /// <returns>返回对应的注册表KEY对象</returns>
        private static RegistryKey GetKey(string keys, RegistryKey key)
        {
            if (keys.IndexOf('/') > 0)
            {
                var keyarray = keys.Split('/').ToArray();
                foreach (var keyitem in keyarray)
                {
                    key = key.OpenSubKey(keyitem, true);

                }


            }
            else
            {
                key = key.OpenSubKey(keys);
            }
            return key;
        }


        /// <summary>
        /// 根据给定key格式读取对应key对象
        /// </summary>
        /// <param name="Keystr">注册表路径：key/key1/key2/key3</param>
        /// <param name="rootKey">根节点KEY</param>
        /// <returns>1:设置成功，0不成功</returns>
        public static int AddinLoad(string Keystr, RegistryKey rootKey)
        {
            try
            {
                RegistryKey key = GetRegKey(Keystr, rootKey);
                if (key != null)
                {
                    key.SetValue("LoadBehavior", 3);
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {

                return 500;
            }

        }
        /// <summary>
        /// 获取LoadBehavior的值
        /// </summary>
        /// <param name="Keystr">注册表路径：key/key1/key2/key3</param>
        /// <param name="rootKey">根节点KEY</param>
        /// <returns>1:设置成功，0不成功</returns>
        public static int GetLoadBehaviorValue(string Keystr, RegistryKey rootKey)
        {
            try
            {
                RegistryKey key = GetRegKey(Keystr, rootKey);
                if (key != null)
                {
                   return  Convert.ToInt32( key.GetValue("LoadBehavior"));
                    
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {

                return 500;
            }

        }
        /// <summary>
        /// 根据给定key格式读取对应key对象
        /// </summary>
        /// <param name="Keystr">注册表路径：key/key1/key2/key3</param>
        /// <param name="rootKey">根节点KEY</param>
        /// <returns>1:设置成功，0不成功</returns>
        public static int AddinLoading(string Keystr, RegistryKey rootKey)
        {
            try
            {
                RegistryKey key = GetRegKey(Keystr, rootKey);
                if (key != null)
                {
                    key.SetValue("LoadBehavior", 8);
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {

                return 500;
            }

        }
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="Keystr">注册表路径：key/key1/key2/key3</param>
        /// <param name="rootKey">根节点KEY</param>
        /// <returns>1：标识修改成功；0：标识未获取Key</returns>
        public static int AddinUnload(string Keystr, RegistryKey rootKey)
        {
            try
            {
                RegistryKey key = GetRegKey(Keystr, rootKey);
                if (key != null)
                {
                    key.SetValue("LoadBehavior", 0);
                    return 1;
                }
                else
                {
                    return 0;

                }
            }
            catch (Exception)
            {

                return 500;
            }
        }
       
    }
}
