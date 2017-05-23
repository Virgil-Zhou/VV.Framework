using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VV.Framework.Core
{
    /// <summary>
    /// 提供WebContext类型上的扩展
    /// </summary>
    public static class WebContextExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public static void ThrowNullException(this WebContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static dynamic GetResult(this WebContext context)
        {
            context.ThrowNullException();

            if (context.ResponseData == null)
                throw new InvalidOperationException();

            if (context.ContentType == ResponseContentType.Text)
            {
                return context.ResponseData.ResponseText;
            }

            return context.ResponseData.ResponseBinary;
        }
    }
}
