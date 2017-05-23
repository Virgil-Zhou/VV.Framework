using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VV.Framework.Core
{
    /// <summary>
    /// 封装Web响应数据
    /// </summary>
    public class WebResponseData
    {
        /// <summary>
        /// 文本信息
        /// </summary>
        public string ResponseText { get; set; }

        /// <summary>
        /// 二进制值
        /// </summary>
        public byte[] ResponseBinary { get; set; }
    }
}

