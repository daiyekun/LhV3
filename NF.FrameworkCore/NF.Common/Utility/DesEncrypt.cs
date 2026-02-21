using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NF.Common.Utility
{
    /// <summary>
    /// DES AES Blowfish
    ///  对称加密算法的优点是速度快，
    ///  缺点是密钥管理不方便，要求共享密钥。
    /// 可逆对称加密  密钥长度8
    /// </summary>
    public class DesEncrypt
    {
        
        

        /// <summary>
        /// DES 加密
        /// </summary>
        /// <param name="text">需要加密的值</param>
        /// <param name="key">加密Key</param>
        /// <returns>加密后的结果</returns>
        public static string Encrypt(string text,string key)
        {
             UnicodeEncoding byteuncode = new UnicodeEncoding();
             byte[] _rgbKey = byteuncode.GetBytes(key);
             byte[] _rgbIV = byteuncode.GetBytes(key);
        DESCryptoServiceProvider dsp = new DESCryptoServiceProvider();
            using (MemoryStream memStream = new MemoryStream())
            {
                CryptoStream crypStream = new CryptoStream(memStream, dsp.CreateEncryptor(_rgbKey, _rgbIV), CryptoStreamMode.Write);
                StreamWriter sWriter = new StreamWriter(crypStream);
                sWriter.Write(text);
                sWriter.Flush();
                crypStream.FlushFinalBlock();
                memStream.Flush();
                return Convert.ToBase64String(memStream.GetBuffer(), 0, (int)memStream.Length);
            }
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="encryptText">解密字符串</param>
        /// <param name="key">解密key</param>
        /// <returns>解密后的结果</returns>
        public static string Decrypt(string encryptText, string key)
        {
            UnicodeEncoding byteuncode = new UnicodeEncoding();
            byte[] _rgbKey = byteuncode.GetBytes(key);
            byte[] _rgbIV = byteuncode.GetBytes(key);
            DESCryptoServiceProvider dsp = new DESCryptoServiceProvider();
            byte[] buffer = Convert.FromBase64String(encryptText);

            using (MemoryStream memStream = new MemoryStream())
            {
                CryptoStream crypStream = new CryptoStream(memStream, dsp.CreateDecryptor(_rgbKey, _rgbIV), CryptoStreamMode.Write);
                crypStream.Write(buffer, 0, buffer.Length);
                crypStream.FlushFinalBlock();
                return ASCIIEncoding.UTF8.GetString(memStream.ToArray());
            }
        }
    }
}
