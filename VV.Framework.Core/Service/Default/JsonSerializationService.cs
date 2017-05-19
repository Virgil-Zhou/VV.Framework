using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VV.Framework.Core
{
    /// <summary>
    /// 提供Json序列化服务
    /// </summary>
    [DefaultService]
    public class JsonSerializationService : IJsonSerialization
    {
        public T Deserialize<T>(string jsonStr) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public string Serialize(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
