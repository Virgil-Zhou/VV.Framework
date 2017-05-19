using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VV.Framework.Core
{
    /// <summary>
    /// 封装单次Web请求的所有信息
    /// </summary>
    public class WebContext
    {
        /// <summary>
        /// Web请求数据
        /// </summary>
        public WebRequest Request { get; set; }

        /// <summary>
        /// Web响应数据
        /// </summary>
        public WebResponse Response { get; set; }


    }
}
