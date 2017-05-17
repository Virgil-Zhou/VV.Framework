using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VV.Framework.Core
{
    /// <summary>
    /// 提供Double类型上的扩展
    /// </summary>
    public static class DoubleExtension
    {
        /// <summary>
        /// 将当前数值保留指定位小数
        /// </summary>
        /// <param name="number">源数据</param>
        /// <param name="digits">位数</param>
        /// <returns>返回指定小数位的double数据</returns>
        public static double Retain(this double number, int digits)
        {
            var divisor = (int)Math.Pow(10, digits);
            return Math.Truncate(number * divisor) / divisor;
        }
    }
}
