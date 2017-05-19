using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace VV.Framework.Core
{
    /// <summary>
    /// 提供常用的算法成员
    /// </summary>
    public static class AlgorithmUtility
    {
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="text">待加密文本</param>
        /// <param name="key">8位秘钥</param>
        /// <param name="encodingName">字符编码名称</param>
        /// <returns>加密后的字符串</returns>
        public static string DESEncryption(string text, string key, string encodingName = "UTF-8")
        {
            var encoding = Encoding.GetEncoding(encodingName);
            var byteArray = encoding.GetBytes(text);

            using (var desSvc = new DESCryptoServiceProvider())
            {
                desSvc.Mode = CipherMode.CBC;
                desSvc.Padding = PaddingMode.PKCS7;
                desSvc.Key = encoding.GetBytes(key);
                desSvc.IV = encoding.GetBytes(key);

                using (var ms = new MemoryStream())
                {
                    var cryStream = new CryptoStream(ms, desSvc.CreateEncryptor(), CryptoStreamMode.Write);
                    cryStream.Write(byteArray, 0, byteArray.Length);
                    cryStream.FlushFinalBlock();
                    cryStream.Close();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }


        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="text">待解密文本</param>
        /// <param name="key">8位秘钥</param>
        /// <param name="encodingName">字符编码名称</param>
        /// <returns>解密后的字符串</returns>
        public static string DESDecryption(string text, string key, string encodingName = "UTF-8")
        {
            var encoding = Encoding.GetEncoding(encodingName);
            var byteArray = Convert.FromBase64String(text);

            using (var desSvc = new DESCryptoServiceProvider())
            {
                desSvc.Mode = CipherMode.CBC;
                desSvc.Padding = PaddingMode.PKCS7;
                desSvc.Key = encoding.GetBytes(key);
                desSvc.IV = encoding.GetBytes(key);

                using (var ms = new MemoryStream())
                {
                    var cryStream = new CryptoStream(ms, desSvc.CreateDecryptor(), CryptoStreamMode.Write);
                    cryStream.Write(byteArray, 0, byteArray.Length);
                    cryStream.FlushFinalBlock();
                    cryStream.Close();
                    return encoding.GetString(ms.ToArray());
                }
            }
        }


        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="text">待加密文本</param>
        /// <param name="capital">加密后的文本是否大写，true：大写 false：小写</param>
        /// <param name="digit">加密字符串的位数，默认：32位，否则：16位</param>
        /// <param name="encodingName">字符编码名称</param>
        /// <returns>加密后的字符串</returns>
        public static string MD5Encryption(string text, bool capital = true, int digit = 32, string encodingName = "UTF-8")
        {
            var encoding = Encoding.GetEncoding(encodingName);
            using (var md5Svc = new MD5CryptoServiceProvider())
            {
                byte[] hashVals = md5Svc.ComputeHash(encoding.GetBytes(text));

                var sb = new StringBuilder(32);
                for (int i = 0; i < hashVals.Length; i++)
                {
                    if (capital)
                        sb.Append(hashVals[i].ToString("X2"));
                    else
                        sb.Append(hashVals[i].ToString("x2"));
                }

                return digit == 32 ? sb.ToString() : sb.ToString().Substring(8, 16);
            }
        }
    }
}
