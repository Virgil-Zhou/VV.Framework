using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VV.Framework.Core
{
    /// <summary>
    /// 提供常用的服务对象
    /// </summary>
    public static class ServiceProvider
    {
        /// <summary>
        /// JSON序列化服务对象
        /// </summary>
        public static IJsonSerialization JsonSerializer => FrameworkConfig.Services.GetService<IJsonSerialization>();
    }
}
