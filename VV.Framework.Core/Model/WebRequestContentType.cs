using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VV.Framework.Core
{
    /// <summary>
    /// WebRequest的ContentType编码类型
    /// </summary>
    public static class WebRequestContentType
    {
        /// <summary>
        /// 将所有字符编码（空格转换为 "+" 加号，特殊符号转换为 ASCII HEX 值）并得到键值对的形式
        /// </summary>
        public const string FormUrlEncoded = "application/x-www-form-urlencoded";

        /// <summary>
        /// 不对字符编码，在包含文件上传的表单中必须使用该值
        /// </summary>
        public const string MultipartForm = "multipart/form-data";

        /// <summary>
        /// 空格转为"+" 加号，但不对特殊字符编码
        /// </summary>
        public const string TextPlain = "text/plain";
    }
}
