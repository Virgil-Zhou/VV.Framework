using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace VV.Framework.Core
{
    /// <summary>
    /// 封装Web请求数据
    /// </summary>
    public class WebRequestData
    {
        /// <summary>
        /// 请求Url
        /// </summary>
        [RequiredThrow]
        public string Url { get; set; }

        /// <summary>
        /// 请求方法
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 请求超时时间（单位：秒）
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// 内容的编码类型，请使用 WebRequestContentType 的枚举成员
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// 是否建立持久连接
        /// </summary>
        public bool KeepAlive { get; set; }

        /// <summary>
        /// HTTP请求头（K/V）集合
        /// </summary>
        public IDictionary<string, string> Headers { get; set; }

        /// <summary>
        /// HTTP请求参数（K/V）集合
        /// </summary>
        public IDictionary<string, string> Parameter { get; set; }

        /// <summary>
        /// 请求附带的Cookie信息
        /// </summary>
        public CookieCollection Cookies { get; set; }

        /// <summary>
        /// X509 证书
        /// </summary>
        public X509Certificate Certificate { get; set; }

        /// <summary>
        /// 写入请求流使用的编码名称
        /// </summary>
        public string RequestUseEncodeName { get; set; }

        /// <summary>
        /// 读取响应流使用的编码名称
        /// </summary>
        public string ResponseUseEncodeName { get; set; }

        /// <summary>
        /// 验证服务器证书的回调委托，参数为null时则表示证书总是被接受(视为安全证书)
        /// </summary>
        public RemoteCertificateValidationCallback RemoteCertificateValidation { get; set; }

        /// <summary>
        /// 上载文件数据集，GET请求会忽略此参数
        /// </summary>
        public PostedFileData[] UploadFiles { get; set; }
    }
}
