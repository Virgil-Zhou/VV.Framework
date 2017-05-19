using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VV.Framework.Core
{
    /// <summary>
    /// 服务容器
    /// </summary>
    public class ServiceContainer
    {
        readonly static ServiceCollection<Object> services = new ServiceCollection<object>();

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <typeparam name="T">服务类型</typeparam>
        /// <returns>返回指定服务的实例</returns>
        public T GetService<T>() where T : class
        {
            return services[typeof(T)] as T;
        }


        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="serviceInstance">服务实例</param>
        public void AddService(Type serviceType, object serviceInstance)
        {
            if (serviceType == null)
                throw new ArgumentNullException(nameof(serviceType));

            if (serviceInstance == null)
                throw new ArgumentNullException(nameof(serviceInstance));

            if (!(serviceType is IService))
                throw new ArgumentException($"类型：{serviceType.FullName} 未被标识为服务类型");

            var svcInstanceType = serviceInstance.GetType();
            if (!serviceType.IsAssignableFrom(svcInstanceType))
                throw new InvalidOperationException($"类型：{svcInstanceType.FullName} 必须是 {serviceType.FullName} 的派生类或者实现类");

            if (services.ContainsKey(serviceType))
                throw new ArgumentException($"服务：{serviceType.FullName} 已存在");

            services.Add(serviceType, serviceInstance);
        }


        /// <summary>
        /// 删除服务
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns>删除成功则返回 true 否则为 false</returns>
        public bool RemoveService(Type serviceType)
        {
            return services.Remove(serviceType);
        }


        /// <summary>
        /// 清除所有服务
        /// </summary>
        public void Clear()
        {
            services.Clear();
        }


        /// <summary>
        /// 服务替换，常用于替换框架的默认服务
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="serviceInstance">服务实例</param>
        public void Replace(Type serviceType, object serviceInstance)
        {
            if (serviceType == null)
                throw new ArgumentNullException(nameof(serviceType));

            if (!services.Keys.Contains(serviceType))
                throw new ArgumentException($"服务：{serviceType.FullName} 不存在");

            var svcInstanceType = serviceInstance.GetType();
            if (!serviceType.IsAssignableFrom(svcInstanceType))
                throw new InvalidOperationException($"类型：{svcInstanceType.FullName} 必须是 {serviceType.FullName} 的派生类或者实现类");

            services[serviceType] = serviceInstance;
        }
    }
}
