using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VV.Framework.Core
{
    /// <summary>
    /// 提供IDictionary类型上的扩展
    /// </summary>
    public static class IDictionaryExtension
    {
        /// <summary>
        /// 获取GET请求的键值对
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string KeyValuePair(this IDictionary<string, string> dic)
        {
            if (dic == null) return "";

            var sb = new StringBuilder(200);
            for (int i = 0; i < dic.Keys.Count; i++)
            {
                string key = dic.Keys.ElementAt(i),
                     value = dic[key];

                if (i == 0)
                    sb.AppendFormat("{0}={1}", key, value);
                else
                    sb.AppendFormat("&{0}={1}", key, value);
            }

            return sb.ToString();
        }
    }
}
