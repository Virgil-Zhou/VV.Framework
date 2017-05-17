using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VV.Framework.Core.Service
{
    /// <summary>
    /// 服务集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class ServiceCollection<T> : Dictionary<Type, T>
    {
        static ServiceTypeComparer serviceTypeComparer = new ServiceTypeComparer();

        public ServiceCollection() : base(serviceTypeComparer)
        {

        }

        class ServiceTypeComparer : IEqualityComparer<Type>
        {
            public bool Equals(Type x, Type y)
            {
                return x.IsEquivalentTo(y);
            }

            public int GetHashCode(Type obj)
            {
                return obj.FullName.GetHashCode();
            }
        }
    }
}
