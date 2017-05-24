using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VV.Framework.Core
{
    /* === 提供框架使用的委托 Start === */


    /// <summary>
    /// 封装Web请求方法，该方法具有一个WebContext参数且不具有返回值
    /// </summary>
    /// <param name="context"></param>
    internal delegate void WebRequestHandler(WebContext context);


    /// <summary>
    /// 封装Web请求方法，该方法具有一个WebContext参数且返回Task
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    internal delegate Task WebRequestHandlerAsync(WebContext context);


    /* === 提供框架使用的委托 End === */
}
