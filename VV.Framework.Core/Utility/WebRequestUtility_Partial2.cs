using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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
        #region 初始化成员

        private static event WebRequestHandler handler;
        private static event WebRequestHandler handlerSync;
        private static event WebRequestHandlerAsync handlerAsync;

        static WebRequestUtility()
        {
            handler += CreateWebRequest;
            handler += InitRequestHeaders;

            handlerSync += InitRequestPayload;
            handlerSync += GetResponseResult;

            handlerAsync += InitRequestPayloadAsync;
            handlerAsync += GetResponseResultAsync;
        }

        #endregion


        /// <summary>
        /// 发送Web请求
        /// </summary>
        /// <param name="context">Web请求上下文</param>
        /// <returns>返回WebContext对象</returns>
        public static WebContext SendRequest(WebContext context)
        {
            context.ThrowNullException();

            handler(context);
            handlerSync(context);

            return context;
        }


        /// <summary>
        /// 发送Web请求
        /// </summary>
        /// <param name="context">Web请求上下文</param>
        /// <returns>返回WebContext对象</returns>
        public async static Task<WebContext> SendRequestAsync(WebContext context)
        {
            context.ThrowNullException();

            handler(context);
            await handlerAsync(context);

            return context;
        }


        /// <summary>
        /// 创建HttpWebRequest对象
        /// </summary>
        /// <param name="context">Web请求上下文</param>
        private static void CreateWebRequest(WebContext context)
        {
            context.ThrowNullException();

            var url = context.RequestData.Url.Trim();
            if (url.StartsWith("https", StringComparison.CurrentCultureIgnoreCase))
            {
                if (context.RequestData.RemoteCertificateValidation == null)
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                        new RemoteCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors) => true);
                }
                else
                {
                    ServicePointManager.ServerCertificateValidationCallback = context.RequestData.RemoteCertificateValidation;
                }
            }

            context.httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
        }


        /// <summary>
        /// 初始化请求头
        /// </summary>
        /// <param name="context">Web请求上下文</param>
        private static void InitRequestHeaders(WebContext context)
        {
            context.ThrowNullException();

            var requestData = context.RequestData;
            var webRequest = context.httpWebRequest;

            if (requestData.Method.IsNullOrEmpty())
                requestData.Method = WebRequestMethods.Http.Get;

            if (requestData.ContentType.IsNullOrEmpty())
            {
                requestData.ContentType = WebRequestContentType.FormUrlEncoded;
            }
            else if (string.Equals(requestData.ContentType, WebRequestContentType.MultipartForm, StringComparison.CurrentCultureIgnoreCase))
            {
                context.boundary = GenerateBoundary();
                requestData.ContentType = $"multipart/form-data; boundary={context.boundary}";
            }

            webRequest.Accept = "*/*";
            webRequest.Headers["Accept-Language"] = "zh-CN,zh;q=0.8,en-US;q=0.5,en;q=0.3";
            webRequest.Headers["Accept-Encoding"] = "gzip, deflate";
            webRequest.Headers["Cache-Control"] = "no-cache";

            webRequest.KeepAlive = requestData.KeepAlive;
            webRequest.Method = requestData.Method;
            webRequest.ContentType = requestData.ContentType;
            webRequest.UserAgent = Constant.UserAgent;
            webRequest.Timeout = (requestData.Timeout > 0 ? requestData.Timeout : 300) * 1000;
            webRequest.ProtocolVersion = HttpVersion.Version11;

            for (int i = 0; requestData.Headers != null && i < requestData.Headers.Count; i++)
            {
                string key = requestData.Headers.Keys.ElementAt(i),
                     value = requestData.Headers[key];

                webRequest.Headers.Add(key, value);
            }

            if (requestData.Certificate != null)
            {
                webRequest.ClientCertificates.Add(requestData.Certificate);
            }

            if (requestData.Cookies != null)
            {
                webRequest.CookieContainer = new CookieContainer();
                webRequest.CookieContainer.Add(requestData.Cookies);
            }
        }


        /// <summary>
        /// 初始化请求体
        /// </summary>
        /// <param name="context">Web请求上下文</param>
        private static void InitRequestPayload(WebContext context)
        {
            context.ThrowNullException();

            var requestData = context.RequestData;
            var webRequest = context.httpWebRequest;

            // GET 请求参数跟 Url 一起传，但框架会提供扩展方法供其使用
            if (string.Equals(requestData.Method, WebRequestMethods.Http.Get, StringComparison.CurrentCultureIgnoreCase)) return;

            byte[] buffer;
            if (string.Equals(webRequest.ContentType, WebRequestContentType.MultipartForm, StringComparison.CurrentCultureIgnoreCase))
            {
                buffer = BuildMultipartPostData(context);
            }
            else
            {
                var kvStr = requestData.Parameter.KeyValuePair();
                buffer = Encoding.GetEncoding(requestData.RequestUseEncodeName).GetBytes(kvStr);
            }

            webRequest.ContentLength = buffer.Length;
            using (var requestStream = webRequest.GetRequestStream())
            {
                requestStream.Write(buffer, 0, buffer.Length);
            }
        }


        /// <summary>
        /// 初始化请求报文体
        /// </summary>
        /// <param name="context">Web请求上下文</param>
        private async static Task InitRequestPayloadAsync(WebContext context)
        {
            context.ThrowNullException();

            var requestData = context.RequestData;
            var webRequest = context.httpWebRequest;

            // GET 请求参数跟 Url 一起传，但框架会提供扩展方法供其使用
            if (string.Equals(requestData.Method, WebRequestMethods.Http.Get, StringComparison.CurrentCultureIgnoreCase)) return;

            byte[] buffer;
            if (string.Equals(webRequest.ContentType, WebRequestContentType.MultipartForm, StringComparison.CurrentCultureIgnoreCase))
            {
                buffer = BuildMultipartPostData(context);
            }
            else
            {
                var kvStr = requestData.Parameter.KeyValuePair();
                buffer = Encoding.GetEncoding(requestData.RequestUseEncodeName).GetBytes(kvStr);
            }

            webRequest.ContentLength = buffer.Length;
            using (var requestStream = await webRequest.GetRequestStreamAsync())
            {
                await requestStream.WriteAsync(buffer, 0, buffer.Length);
            }
        }


        /// <summary>
        /// 构建多表单Post数据
        /// </summary>
        /// <param name="context">Web请求上下文</param>
        /// <returns>返回二进制的表单数据</returns>
        private static byte[] BuildMultipartPostData(WebContext context)
        {
            context.ThrowNullException();

            var payload = new StringBuilder(500);
            var requestData = context.RequestData;
            var encoding = Encoding.GetEncoding(requestData.RequestUseEncodeName);

            for (int i = 0; requestData.Parameter != null && i < requestData.Parameter.Count; i++)
            {
                string key = requestData.Parameter.Keys.ElementAt(i),
                     value = requestData.Parameter[key];

                payload.AppendFormat("--{0}{1}", context.boundary, Environment.NewLine);
                payload.AppendFormat("Content-Disposition: form-data; name=\"{0}\"{1}", key, Environment.NewLine);
                payload.Append(Environment.NewLine);
                payload.AppendLine(value);
            }

            if (payload.Length > 0 || (requestData.UploadFiles != null && requestData.UploadFiles.Length > 0))
            {
                using (var ms = new MemoryStream())
                {
                    using (var bw = new BinaryWriter(ms, encoding, true))
                    {
                        if (payload.Length > 0)
                        {
                            bw.Write(encoding.GetBytes(payload.ToString()));
                        }

                        for (int i = 0; requestData.UploadFiles != null && i < requestData.UploadFiles.Length; i++)
                        {
                            var fileData = requestData.UploadFiles[i];
                            payload.Clear();
                            payload.AppendFormat("--{0}{1}", context.boundary, Environment.NewLine);
                            payload.AppendFormat("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"{2}", fileData.Name, fileData.FileFullName, Environment.NewLine);
                            payload.AppendFormat("Content-Type: {0}{1}", fileData.ContentType, Environment.NewLine);
                            payload.Append(Environment.NewLine);
                            bw.Write(encoding.GetBytes(payload.ToString()));
                            bw.Write(fileData.FileBinary);
                            bw.Write(encoding.GetBytes(Environment.NewLine));
                        }

                        bw.Write(encoding.GetBytes("--{0}--".Fmt(context.boundary)));
                    }

                    ms.Flush();
                    ms.Position = 0;
                    return ms.ToArray();
                }
            }

            return new byte[0];
        }


        /// <summary>
        /// 生成边界值
        /// </summary>
        /// <returns></returns>
        private static string GenerateBoundary()
        {
            var guid = Guid.NewGuid().ToString("N");
            var md5Val = AlgorithmUtility.MD5Encryption(guid, true, 16);
            return $"----WebKitFormBoundary{md5Val}";
        }


        /// <summary>
        /// 获取响应结果
        /// </summary>
        /// <param name="context">Web请求上下文</param>
        private static void GetResponseResult(WebContext context)
        {
            context.ThrowNullException();

            try
            {
                context.httpWebResponse = (HttpWebResponse)context.httpWebRequest.GetResponse();
                var stream = GetResponseStream(context);
                var encoding = Encoding.GetEncoding(context.RequestData.ResponseUseEncodeName);

                if (context.ContentType == ResponseContentType.Text)
                {
                    context.responseData.ResponseText = new StreamReader(stream, encoding).ReadToEnd();
                }
                else
                {
                    var buffer = new List<byte>();
                    do
                    {
                        var val = stream.ReadByte();
                        if (val == -1) break;
                        buffer.Add((byte)val);

                    } while (true);

                    context.responseData.ResponseBinary = buffer.ToArray();
                }
            }
            finally
            {
                if (context.httpWebResponse != null)
                    context.httpWebResponse.Close();
            }
        }


        /// <summary>
        /// 获取响应结果
        /// </summary>
        /// <param name="context">Web请求上下文</param>
        private async static Task GetResponseResultAsync(WebContext context)
        {
            context.ThrowNullException();

            try
            {
                context.httpWebResponse = (HttpWebResponse)await context.httpWebRequest.GetResponseAsync();
                var stream = GetResponseStream(context);
                var encoding = Encoding.GetEncoding(context.RequestData.ResponseUseEncodeName);

                if (context.ContentType == ResponseContentType.Text)
                {
                    context.responseData.ResponseText = await new StreamReader(stream, encoding).ReadToEndAsync();
                }
                else
                {
                    var buffer = new List<byte>();
                    do
                    {
                        var val = stream.ReadByte(); // 有待改进为异步
                        if (val == -1) break;
                        buffer.Add((byte)val);

                    } while (true);

                    context.responseData.ResponseBinary = buffer.ToArray();
                }
            }
            finally
            {
                if (context.httpWebResponse != null)
                    context.httpWebResponse.Close();
            }
        }


        /// <summary>
        /// 获取Web响应流
        /// </summary>
        /// <param name="context">Web请求上下文</param>
        /// <returns>返回响应流</returns>
        private static Stream GetResponseStream(WebContext context)
        {
            context.ThrowNullException();

            Stream stream = null;
            var webResponse = context.httpWebResponse;
            var contentEncoding = webResponse.ContentEncoding.ToLower();

            if (contentEncoding.Contains("gzip"))
            {
                stream = new GZipStream(webResponse.GetResponseStream(), CompressionMode.Decompress, false);
            }
            else if (contentEncoding.Contains("deflate"))
            {
                stream = new DeflateStream(webResponse.GetResponseStream(), CompressionMode.Decompress, false);
            }
            else
            {
                stream = webResponse.GetResponseStream();
            }

            return stream;
        }

    }
}
