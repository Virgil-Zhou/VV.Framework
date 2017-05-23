using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VV.Framework.Core
{
    /// <summary>
    /// 封装单次Web请求的所有信息
    /// </summary>
    public class WebContext
    {
        #region Constructors

        public WebContext()
        {
            this.contentType = ResponseContentType.Text;
        }

        public WebContext(ResponseContentType contentType)
        {
            this.contentType = contentType;
        }

        #endregion

        #region fields

        /// <summary>
        /// HttpWebRequest对象
        /// </summary>
        internal HttpWebRequest httpWebRequest;

        /// <summary>
        /// HttpWebResponse对象
        /// </summary>
        internal HttpWebResponse httpWebResponse;

        /// <summary>
        /// 用于Form请求的边界值
        /// </summary>
        internal string boundary;

        /// <summary>
        /// Web响应数据
        /// </summary>
        internal WebResponseData responseData;

        /// <summary>
        /// Web响应(内容)类型，默认为文本
        /// </summary>
        private ResponseContentType contentType;

        #endregion

        #region properties

        /// <summary>
        /// Web请求数据
        /// </summary>
        public WebRequestData RequestData { get; set; }

        /// <summary>
        /// Web响应(内容)类型，默认为文本
        /// </summary>
        public ResponseContentType ContentType => contentType;

        /// <summary>
        /// Web响应数据
        /// </summary>
        public WebResponseData ResponseData => responseData;

        #endregion
    }
}
