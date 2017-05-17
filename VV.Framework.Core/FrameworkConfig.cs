using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VV.Framework.Core
{
    /// <summary>
    /// 框架配置
    /// </summary>
    public static class FrameworkConfig
    {
        #region 框架初始化

        readonly static ServiceContainer services;

        static FrameworkConfig()
        {
            services = new ServiceContainer();
            InitDefaultService();
        }

        #endregion


        /// <summary>
        /// 服务容器实例
        /// </summary>
        public static ServiceContainer Services => services;


        /// <summary>
        /// 初始化默认服务
        /// </summary>
        private static void InitDefaultService()
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var typeArray = currentAssembly.GetTypes();
            var isvcType = typeof(IService);
            var defaultSvcAttrType = typeof(DefaultServiceAttribute);

            for (int i = 0; i < typeArray.Length; i++)
            {
                var type = typeArray[i];
                if (type.IsDefined(defaultSvcAttrType, false))
                {
                    var specificISvcType = type.GetInterfaces().FirstOrDefault(c => c.Name != isvcType.Name);
                    if (specificISvcType == null) continue;
                    services.AddService(specificISvcType, Activator.CreateInstance(type));
                }
            }
        }
    }
}
