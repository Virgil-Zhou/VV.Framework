using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VV.Framework.Core
{
    /// <summary>
    /// Json序列化API
    /// </summary>
    public interface IJsonSerialization : IService
    {
        /// <summary>
        /// 将一个对象转换为Json字符串
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <returns>返回序列化后的Json字符串</returns>
        string Serialize(Object obj);


        /// <summary>
        /// 将指定的Json字符串转换为一个T类型对象
        /// </summary>
        /// <typeparam name="T">生成的对象类型</typeparam>
        /// <param name="jsonStr">要反序列化的Json字符串</param>
        /// <returns>返回反序列化后的T对象</returns>
        T Deserialize<T>(string jsonStr) where T : class, new();
    }
}
