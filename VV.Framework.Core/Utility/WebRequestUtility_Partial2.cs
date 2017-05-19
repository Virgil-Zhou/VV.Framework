using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
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
        /// 发送Web请求
        /// </summary>
        /// <param name="context">Web请求上下文</param>
        public static void SendRequest(WebContext context)
        {

        }


        /// <summary>
        /// 发送Web请求
        /// </summary>
        /// <param name="context">Web请求上下文</param>
        public async static void SendRequestAsync(WebContext context)
        {

        }


        /// <summary>
        /// 创建HttpWebRequest对象
        /// </summary>
        /// <param name="context">Web请求上下文</param>
        /// <returns>返回HttpWebRequest对象</returns>
        private static HttpWebRequest CreateWebRequest(WebContext context)
        {
            var url = context.Request.Url.Trim();
            if (url.StartsWith("https", StringComparison.CurrentCultureIgnoreCase))
            {
                if (context.Request.RemoteCertificateValidation == null)
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                        new RemoteCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors) => true);
                }
                else
                {
                    ServicePointManager.ServerCertificateValidationCallback = context.Request.RemoteCertificateValidation;
                }
            }

            return (HttpWebRequest)System.Net.WebRequest.Create(url);
        }
    }
}
