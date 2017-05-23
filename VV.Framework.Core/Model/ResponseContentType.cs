using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VV.Framework.Core
{
    /// <summary>
    /// Web响应(内容)类型
    /// </summary>
    public enum ResponseContentType : int
    {
        /// <summary>
        /// 文本（String）
        /// </summary>
        Text = 0,

        /// <summary>
        /// 二进制值（byte[]）
        /// </summary>
        Binary = 1
    }
}
