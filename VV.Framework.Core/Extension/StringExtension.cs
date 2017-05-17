using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.String;

namespace VV.Framework.Core
{
    /// <summary>
    /// 提供String类型上的扩展
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="args">参数数组</param>
        /// <returns>格式化完的字符串</returns>
        public static string Fmt(this string str, params object[] args) => Format(str, args);

        /// <summary>
        /// 指示指定的字符串是 null 还是 System.String.Empty 字符串。
        /// </summary>
        /// <param name="str">要测试的字符串</param>
        /// <returns>如果 value 参数为 null 或空字符串 ("")，则为 true；否则为 false。</returns>
        public static bool IsNullOrEmpty(this string str) => IsNullOrEmpty(str);

        /// <summary>
        /// 指示指定的字符串是 null、空还是仅由空白字符组成。
        /// </summary>
        /// <param name="str"> 要测试的字符串。</param>
        /// <returns>如果 value 参数为 null 或 System.String.Empty，或者如果 value 仅由空白字符组成，则为 true。</returns>
        public static bool IsNullOrWhiteSpace(this string str) => IsNullOrWhiteSpace(str);

        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="str">待编码字符串</param>
        /// <param name="encodingName">字符编码名称</param>
        /// <returns>编码后的字符串</returns>
        public static string EncodeBase64String(this string str, string encodingName = "UTF-8")
        {
            var bytes = Encoding.GetEncoding(encodingName).GetBytes(str);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Base64解码
        /// </summary>
        /// <param name="str">待解码的字符串</param>
        /// <param name="encodingName">字符编码名称</param>
        /// <returns>解码后的字符串</returns>
        public static string DecodeBase64String(this string str, string encodingName = "UTF-8")
        {
            var bytes = Convert.FromBase64String(str);
            return Encoding.GetEncoding(encodingName).GetString(bytes);
        }

        /// <summary>
        /// Base64解码，一个指示解码是否成功的返回值
        /// </summary>
        /// <param name="str">待解码的字符串</param>
        /// <param name="value">当此方法返回时，如果解码成功则为str解码成功后的字符串，若解码失败则返回null。</param>
        /// <param name="encodingName">字符编码名称</param>
        /// <returns>如果成功解码了str，则为 true；否则为 false。</returns>
        public static bool TryDecodeBase64String(this string str, out string value, string encodingName = "UTF-8")
        {
            try
            {
                value = str.DecodeBase64String(encodingName);
                return true;
            }
            catch
            {
                value = null;
                return false;
            }
        }

        /// <summary>
        /// 获取AppSettings节点下指定Key的Value值
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>返回AppSettings节点下指定Key的Value值，若Key不存在或为null则返回null</returns>
        public static string ValueOfAppSetting(this string key) => ConfigurationManager.AppSettings[key];

        /// <summary>
        /// 获取ConnectionStrings节点下指定Key的Value值
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>返回ConnectionStrings节点下指定Key的Value值，Key为空或null则会抛出 ArgumentNullException，若Key不存在则会抛出 KeyNotFoundException</returns>
        public static string ValueOfConnectionString(this string key)
        {
            if (key.IsNullOrWhiteSpace())
                throw new ArgumentNullException(nameof(key), "数据库连接字符串的Key不能为空");

            var conn = ConfigurationManager.ConnectionStrings[key];
            if (conn == null)
                throw new KeyNotFoundException($"Key：{key} 不存在或未配置");

            return conn.ConnectionString;
        }

        /// <summary>
        /// 将数字的字符串表现形式转换为它的等效32位带符号整数，str为null或格式不正确会引发异常
        /// </summary>
        /// <param name="str">数字字符串</param>
        /// <returns>返回转换后的Int32整数</returns>
        public static int AsInt(this string str) => int.Parse(str);

        /// <summary>
        /// 从源字符串索引为0开始截取指定长度的字符串，length小于等于0时返回String.Empty
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="length">需要截取的长度</param>
        /// <returns>返回处理后的字符串</returns>
        public static string SubstringIf(this string str, int length)
        {
            if (str == null) return null;
            if (length <= 0) return "";
            if (str.Length > length)
            {
                return str.Substring(0, length);
            }

            return str;
        }

    }
}
