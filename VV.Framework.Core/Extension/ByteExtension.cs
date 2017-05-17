using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VV.Framework.Core
{
    /// <summary>
    /// 提供Byte类型上的扩展
    /// </summary>
    public static class ByteExtension
    {
        /// <summary>
        /// 获取文件头信息（前面4字节的十六进制）, arr为null或arr.Length小于4则会引发ArgumentException异常
        /// </summary>
        /// <param name="arr">字节数组</param>
        /// <returns>返回文件头信息（十六进制）</returns>
        public static string FileHeaderHex(this byte[] arr)
        {
            if (arr == null)
                throw new ArgumentNullException(nameof(arr), "文件的字节数据不能为空");

            if (arr.Length < 4)
                throw new ArgumentException("文件的字节数组长度不能小于4");

            var sb = new StringBuilder(50);
            for (int i = 0; i < 4; i++)
            {
                sb.Append(arr[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
