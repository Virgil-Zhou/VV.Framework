using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VV.Framework.Core
{
    /// <summary>
    /// 提供常用的 Web Request 成员
    /// </summary>
    public static partial class WebRequestUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="timeout"></param>
        /// <param name="contentType"></param>
        /// <param name="keepAlive"></param>
        /// <param name="headers"></param>
        /// <param name="requestUseEncodeName"></param>
        /// <param name="responseUseEncodeName"></param>
        /// <returns></returns>
        public static string Get(string url, int timeout, string contentType, bool keepAlive, IDictionary<string, string> headers, string requestUseEncodeName, string responseUseEncodeName)
        {
            var context = new WebContext();

            context.RequestData = new WebRequestData
            {
                Url = url,
                Method = WebRequestMethods.Http.Get,
                Timeout = timeout,
                ContentType = contentType,
                KeepAlive = keepAlive,
                Headers = headers,
                RequestUseEncodeName = requestUseEncodeName,
                ResponseUseEncodeName = responseUseEncodeName
            };

            return SendRequest(context).GetResult();
        }
    }
}
