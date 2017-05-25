using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace VV.Framework.Core
{
    /// <summary>
    /// 提供常用的 Web Request 成员
    /// </summary>
    public static partial class WebRequestUtility
    {
        /// <summary>
        /// 发送GET请求，超时时间：300 秒，默认编码：UTF-8
        /// </summary>
        /// <param name="url">网络资源地址</param>
        /// <returns>返回响应文本信息</returns>
        public static string Get(string url)
        {
            return Get(url, 300, WebRequestContentType.FormUrlEncoded, true, null, Constant.UTF8, Constant.UTF8);
        }


        /// <summary>
        /// 发送GET请求，超时时间：300 秒，默认编码：UTF-8
        /// </summary>
        /// <typeparam name="T">返回(对象)类型</typeparam>
        /// <param name="url">网络资源地址<</param>
        /// <returns>返回T对象</returns>
        public static T Get<T>(string url) where T : class, new()
        {
            var content = Get(url);

            return ServiceProvider.JsonSerializer.Deserialize<T>(content);
        }


        /// <summary>
        /// 发送GET请求
        /// </summary>
        /// <param name="url">网络资源地址</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="contentType">指定Content Type的编码方式</param>
        /// <param name="keepAlive">是否建立持久连接</param>
        /// <param name="headers">请求头信息（K/V），若无则为 null</param>
        /// <param name="requestUseEncodeName">请求使用的编码名称</param>
        /// <param name="responseUseEncodeName">响应使用的编码名称</param>
        /// <returns>返回响应文本信息</returns>
        public static string Get(string url, int timeout, string contentType, bool keepAlive, IDictionary<string, string> headers, string requestUseEncodeName, string responseUseEncodeName)
        {
            var context = new WebContext();

            context.RequestData = new WebRequestData
            {
                Url = url,
                Method = WebRequestMethods.Http.Get,
                Timeout = timeout,
                ContentType = contentType,
                KeepAlive = keepAlive,
                Headers = headers,
                RequestUseEncodeName = requestUseEncodeName,
                ResponseUseEncodeName = responseUseEncodeName
            };

            return SendRequest(context).GetResult();
        }


        /// <summary>
        /// 发送GET请求
        /// </summary>
        /// <typeparam name="T">返回(对象)类型</typeparam>
        /// <param name="url">网络资源地址</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="contentType">指定Content Type的编码方式</param>
        /// <param name="keepAlive">是否建立持久连接</param>
        /// <param name="headers">请求头信息（K/V），若无则为 null</param>
        /// <param name="requestUseEncodeName">请求使用的编码名称</param>
        /// <param name="responseUseEncodeName">响应使用的编码名称</param>
        /// <returns>返回T对象</returns>
        public static T Get<T>(string url, int timeout, string contentType, bool keepAlive, IDictionary<string, string> headers, string requestUseEncodeName, string responseUseEncodeName) where T : class, new()
        {
            var content = Get(url, timeout, contentType, keepAlive, headers, requestUseEncodeName, responseUseEncodeName);

            return ServiceProvider.JsonSerializer.Deserialize<T>(content);
        }


        /// <summary>
        /// 发送GET请求，超时时间：300 秒，默认编码：UTF-8
        /// </summary>
        /// <param name="url">网络资源地址</param>
        /// <returns>返回响应文本信息</returns>
        public async static Task<string> GetAsync(string url)
        {
            return await GetAsync(url, 300, WebRequestContentType.FormUrlEncoded, true, null, Constant.UTF8, Constant.UTF8);
        }


        /// <summary>
        /// 发送GET请求，超时时间：300 秒，默认编码：UTF-8
        /// </summary>
        /// <typeparam name="T">返回(对象)类型</typeparam>
        /// <param name="url">网络资源地址</param>
        /// <returns>返回T对象</returns>
        public async static Task<T> GetAsync<T>(string url) where T : class, new()
        {
            var content = await GetAsync(url);

            return ServiceProvider.JsonSerializer.Deserialize<T>(content);
        }


        /// <summary>
        /// 发送GET请求
        /// </summary>
        /// <param name="url">网络资源地址</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="contentType">指定Content Type的编码方式</param>
        /// <param name="keepAlive">是否建立持久连接</param>
        /// <param name="headers">请求头信息（K/V），若无则为 null</param>
        /// <param name="requestUseEncodeName">请求使用的编码名称</param>
        /// <param name="responseUseEncodeName">响应使用的编码名称</param>
        /// <returns>返回响应文本信息</returns>
        public async static Task<string> GetAsync(string url, int timeout, string contentType, bool keepAlive, IDictionary<string, string> headers, string requestUseEncodeName, string responseUseEncodeName)
        {
            var context = new WebContext();

            context.RequestData = new WebRequestData
            {
                Url = url,
                Method = WebRequestMethods.Http.Get,
                Timeout = timeout,
                ContentType = contentType,
                KeepAlive = keepAlive,
                Headers = headers,
                RequestUseEncodeName = requestUseEncodeName,
                ResponseUseEncodeName = responseUseEncodeName
            };

            return (await SendRequestAsync(context)).GetResult();
        }


        /// <summary>
        /// 发送GET请求
        /// </summary>
        /// <typeparam name="T">返回(对象)类型</typeparam>
        /// <param name="url">网络资源地址</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="contentType">指定Content Type的编码方式</param>
        /// <param name="keepAlive">是否建立持久连接</param>
        /// <param name="headers">请求头信息（K/V），若无则为 null</param>
        /// <param name="requestUseEncodeName">请求使用的编码名称</param>
        /// <param name="responseUseEncodeName">响应使用的编码名称</param>
        /// <returns>返回T对象</returns>
        public async static Task<T> GetAsync<T>(string url, int timeout, string contentType, bool keepAlive, IDictionary<string, string> headers, string requestUseEncodeName, string responseUseEncodeName) where T : class, new()
        {
            var content = await GetAsync(url, timeout, contentType, keepAlive, headers, requestUseEncodeName, responseUseEncodeName);

            return ServiceProvider.JsonSerializer.Deserialize<T>(content);
        }


        /// <summary>
        /// 发送POST请求
        /// </summary>
        /// <param name="url">网络资源地址</param>
        /// <param name="parameters">请求参数（K/V）集合</param>
        /// <returns>返回响应文本信息</returns>
        public static string Post(string url, IDictionary<string, string> parameters)
        {
            return Post(url, 300, WebRequestContentType.FormUrlEncoded, true, null, parameters, Constant.UTF8, Constant.UTF8);
        }


        /// <summary>
        /// 发送POST请求
        /// </summary>
        /// <typeparam name="T">返回(对象)类型</typeparam>
        /// <param name="url">网络资源地址</param>
        /// <param name="parameters">请求参数（K/V）集合</param>
        /// <returns>返回T对象</returns>
        public static T Post<T>(string url, IDictionary<string, string> parameters) where T : class, new()
        {
            var content = Post(url, parameters);

            return ServiceProvider.JsonSerializer.Deserialize<T>(content);
        }


        /// <summary>
        /// 发送POST请求
        /// </summary>
        /// <param name="url">网络资源地址</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="contentType">指定Content Type的编码方式</param>
        /// <param name="keepAlive">是否建立持久连接</param>
        /// <param name="headers">请求头信息（K/V），若无则为 null</param>
        /// <param name="parameters">请求参数（K/V）集合</param>
        /// <param name="requestUseEncodeName">请求使用的编码名称</param>
        /// <param name="responseUseEncodeName">响应使用的编码名称</param>
        /// <returns>返回响应文本信息</returns>
        public static string Post(string url, int timeout, string contentType, bool keepAlive, IDictionary<string, string> headers, IDictionary<string, string> parameters, string requestUseEncodeName, string responseUseEncodeName)
        {
            var context = new WebContext();

            context.RequestData = new WebRequestData
            {
                Url = url,
                Method = WebRequestMethods.Http.Post,
                Timeout = timeout,
                ContentType = contentType,
                KeepAlive = keepAlive,
                Headers = headers,
                Parameter = parameters,
                RequestUseEncodeName = requestUseEncodeName,
                ResponseUseEncodeName = responseUseEncodeName
            };

            return SendRequest(context).GetResult();
        }


        /// <summary>
        /// 发送POST请求
        /// </summary>
        /// <typeparam name="T">返回(对象)类型</typeparam>
        /// <param name="url">网络资源地址</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="contentType">指定Content Type的编码方式</param>
        /// <param name="keepAlive">是否建立持久连接</param>
        /// <param name="headers">请求头信息（K/V），若无则为 null</param>
        /// <param name="parameters">请求参数（K/V）集合</param>
        /// <param name="requestUseEncodeName">请求使用的编码名称</param>
        /// <param name="responseUseEncodeName">响应使用的编码名称</param>
        /// <returns>返回T对象</returns>
        public static T Post<T>(string url, int timeout, string contentType, bool keepAlive, IDictionary<string, string> headers, IDictionary<string, string> parameters, string requestUseEncodeName, string responseUseEncodeName) where T : class, new()
        {
            var content = Post(url, timeout, contentType, keepAlive, headers, parameters, requestUseEncodeName, responseUseEncodeName);

            return ServiceProvider.JsonSerializer.Deserialize<T>(content);
        }


        /// <summary>
        /// 发送POST请求
        /// </summary>
        /// <param name="url">网络资源地址</param>
        /// <param name="parameters">请求参数（K/V）集合</param>
        /// <returns>返回响应文本信息</returns>
        public async static Task<string> PostAsync(string url, IDictionary<string, string> parameters)
        {
            return await PostAsync(url, 300, WebRequestContentType.FormUrlEncoded, true, null, parameters, Constant.UTF8, Constant.UTF8);
        }


        /// <summary>
        /// 发送POST请求
        /// </summary>
        /// <typeparam name="T">返回(对象)类型</typeparam>
        /// <param name="url">网络资源地址</param>
        /// <param name="parameters">请求参数（K/V）集合</param>
        /// <returns>返回T对象</returns>
        public async static Task<T> PostAsync<T>(string url, IDictionary<string, string> parameters) where T : class, new()
        {
            var content = await PostAsync(url, parameters);

            return ServiceProvider.JsonSerializer.Deserialize<T>(content);
        }


        /// <summary>
        /// 发送POST请求
        /// </summary>
        /// <param name="url">网络资源地址</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="contentType">指定Content Type的编码方式</param>
        /// <param name="keepAlive">是否建立持久连接</param>
        /// <param name="headers">请求头信息（K/V），若无则为 null</param>
        /// <param name="parameters">请求参数（K/V）集合</param>
        /// <param name="requestUseEncodeName">请求使用的编码名称</param>
        /// <param name="responseUseEncodeName">响应使用的编码名称</param>
        /// <returns>返回响应文本信息</returns>
        public async static Task<string> PostAsync(string url, int timeout, string contentType, bool keepAlive, IDictionary<string, string> headers, IDictionary<string, string> parameters, string requestUseEncodeName, string responseUseEncodeName)
        {
            var context = new WebContext();

            context.RequestData = new WebRequestData
            {
                Url = url,
                Method = WebRequestMethods.Http.Post,
                Timeout = timeout,
                ContentType = contentType,
                KeepAlive = keepAlive,
                Headers = headers,
                Parameter = parameters,
                RequestUseEncodeName = requestUseEncodeName,
                ResponseUseEncodeName = responseUseEncodeName
            };

            return (await SendRequestAsync(context)).GetResult();
        }


        /// <summary>
        /// 发送POST请求
        /// </summary>
        /// <typeparam name="T">返回(对象)类型</typeparam>
        /// <param name="url">网络资源地址</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="contentType">指定Content Type的编码方式</param>
        /// <param name="keepAlive">是否建立持久连接</param>
        /// <param name="headers">请求头信息（K/V），若无则为 null</param>
        /// <param name="parameters">请求参数（K/V）集合</param>
        /// <param name="requestUseEncodeName">请求使用的编码名称</param>
        /// <param name="responseUseEncodeName">响应使用的编码名称</param>
        /// <returns>返回T对象</returns>
        public async static Task<T> PostAsync<T>(string url, int timeout, string contentType, bool keepAlive, IDictionary<string, string> headers, IDictionary<string, string> parameters, string requestUseEncodeName, string responseUseEncodeName) where T : class, new()
        {
            var content = await PostAsync(url, timeout, contentType, keepAlive, headers, parameters, requestUseEncodeName, responseUseEncodeName);

            return ServiceProvider.JsonSerializer.Deserialize<T>(content);
        }


        /// <summary>
        /// 发送表单请求，超时时间：300 秒，默认编码：UTF-8
        /// </summary>
        /// <param name="url">网络资源地址</param>
        /// <param name="parameters">请求参数（K/V）集合</param>
        /// <param name="uploadFiles">上载文件集合</param>
        /// <returns>返回响应文本信息</returns>
        public static string FormRequest(string url, IDictionary<string, string> parameters, params PostedFileData[] uploadFiles)
        {
            return FormRequest(url, 300, true, null, parameters, Constant.UTF8, Constant.UTF8, uploadFiles);
        }


        /// <summary>
        /// 发送表单请求，超时时间：300 秒，默认编码：UTF-8
        /// </summary>
        /// <typeparam name="T">返回(对象)类型</typeparam>
        /// <param name="url">网络资源地址</param>
        /// <param name="parameters">请求参数（K/V）集合</param>
        /// <param name="uploadFiles">上载文件集合</param>
        /// <returns>返回T对象</returns>
        public static T FormRequest<T>(string url, IDictionary<string, string> parameters, params PostedFileData[] uploadFiles) where T : class, new()
        {
            var content = FormRequest(url, parameters, uploadFiles);

            return ServiceProvider.JsonSerializer.Deserialize<T>(content);
        }


        /// <summary>
        /// 发送表单请求，超时时间：300 秒，默认编码：UTF-8
        /// </summary>
        /// <param name="url">网络资源地址</param>
        /// <param name="parameters">请求参数（K/V）集合</param>
        /// <param name="name">上载文件对应表单name属性名称</param>
        /// <param name="filePath">文件的绝对路径</param>
        /// <returns>返回响应文本信息</returns>
        public static string FormRequest(string url, IDictionary<string, string> parameters, string name, string filePath)
        {
            if (name.IsNullOrWhiteSpace())
                throw new ArgumentNullException(nameof(name));

            if (filePath.IsNullOrWhiteSpace())
                throw new ArgumentNullException(nameof(filePath));

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"在路径：{filePath} 下未找到指定文件");

            var fileName = Path.GetFileName(filePath);
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);

                return FormRequest(url, parameters, new PostedFileData
                {
                    Name = name,
                    FileFullName = fileName,
                    ContentType = MimeMapping.GetMimeMapping(fileName),
                    FileBinary = buffer
                });
            }
        }


        /// <summary>
        /// 发送表单请求，超时时间：300 秒，默认编码：UTF-8
        /// </summary>
        /// <typeparam name="T">返回(对象)类型</typeparam>
        /// <param name="url">网络资源地址</param>
        /// <param name="parameters">请求参数（K/V）集合</param>
        /// <param name="name">上载文件对应表单name属性名称</param>
        /// <param name="filePath">文件的绝对路径</param>
        /// <returns>返回T对象</returns>
        public static T FormRequest<T>(string url, IDictionary<string, string> parameters, string name, string filePath) where T : class, new()
        {
            var content = FormRequest(url, parameters, name, filePath);

            return ServiceProvider.JsonSerializer.Deserialize<T>(content);
        }


        /// <summary>
        /// 发送表单请求，超时时间：300 秒，默认编码：UTF-8
        /// </summary>
        /// <param name="url">网络资源地址</param>
        /// <param name="parameters">请求参数（K/V）集合</param>
        /// <param name="fileDic">上载文件字典集合，key为表单对应name属性名称，value为该文件的绝对路径</param>
        /// <returns>返回响应文本信息</returns>
        public static string FormRequest(string url, IDictionary<string, string> parameters, IDictionary<string, string> fileDic)
        {
            List<PostedFileData> fileList = null;
            for (int i = 0; fileDic != null && i < fileDic.Keys.Count; i++)
            {
                if (fileList == null)
                    fileList = new List<PostedFileData>();

                string name = fileDic.Keys.ElementAt(i),
                       path = fileDic[name];

                if (name.IsNullOrWhiteSpace())
                    throw new ArgumentNullException(nameof(name));

                if (path.IsNullOrWhiteSpace())
                    throw new ArgumentNullException(nameof(path));

                if (!File.Exists(path))
                    throw new FileNotFoundException($"在路径：{path} 下未找到指定文件");

                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);

                    var fileName = Path.GetFileName(path);
                    fileList.Add(new PostedFileData
                    {
                        Name = name,
                        FileFullName = fileName,
                        ContentType = MimeMapping.GetMimeMapping(fileName),
                        FileBinary = buffer
                    });
                }
            }

            return FormRequest(url, parameters, fileList == null ? null : fileList.ToArray());
        }


        /// <summary>
        /// 发送表单请求，超时时间：300 秒，默认编码：UTF-8
        /// </summary>
        /// <typeparam name="T">返回(对象)类型</typeparam>
        /// <param name="url">网络资源地址</param>
        /// <param name="parameters">请求参数（K/V）集合</param>
        /// <param name="fileDic">上载文件字典集合，key为表单对应name属性名称，value为该文件的绝对路径</param>
        /// <returns>返回T对象</returns>
        public static T FormRequest<T>(string url, IDictionary<string, string> parameters, IDictionary<string, string> fileDic) where T : class, new()
        {
            var content = FormRequest(url, parameters, fileDic);

            return ServiceProvider.JsonSerializer.Deserialize<T>(content);
        }


        /// <summary>
        /// 发送表单请求
        /// </summary>
        /// <param name="url">网络资源地址</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="keepAlive">是否建立持久连接</param>
        /// <param name="headers">请求头信息（K/V），若无则为 null</param>
        /// <param name="parameters">请求参数（K/V）集合</param>
        /// <param name="requestUseEncodeName">请求使用的编码名称</param>
        /// <param name="responseUseEncodeName">响应使用的编码名称</param>
        /// <param name="uploadFiles">上载文件集合</param>
        /// <returns>返回响应文本信息</returns>
        public static string FormRequest(string url, int timeout, bool keepAlive, IDictionary<string, string> headers, IDictionary<string, string> parameters, string requestUseEncodeName, string responseUseEncodeName, params PostedFileData[] uploadFiles)
        {
            var context = new WebContext();

            context.RequestData = new WebRequestData
            {
                Url = url,
                Method = WebRequestMethods.Http.Post,
                Timeout = timeout,
                ContentType = WebRequestContentType.MultipartForm,
                KeepAlive = keepAlive,
                Headers = headers,
                Parameter = parameters,
                RequestUseEncodeName = requestUseEncodeName,
                ResponseUseEncodeName = responseUseEncodeName,
                UploadFiles = uploadFiles
            };

            return SendRequest(context).GetResult();
        }


        /// <summary>
        /// 发送表单请求
        /// </summary>
        /// <typeparam name="T">返回(对象)类型</typeparam>
        /// <param name="url">网络资源地址</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="keepAlive">是否建立持久连接</param>
        /// <param name="headers">请求头信息（K/V），若无则为 null</param>
        /// <param name="parameters">请求参数（K/V）集合</param>
        /// <param name="requestUseEncodeName">请求使用的编码名称</param>
        /// <param name="responseUseEncodeName">响应使用的编码名称</param>
        /// <param name="uploadFiles">上载文件集合</param>
        /// <returns>返回T对象</returns>
        public static T FormRequest<T>(string url, int timeout, bool keepAlive, IDictionary<string, string> headers, IDictionary<string, string> parameters, string requestUseEncodeName, string responseUseEncodeName, params PostedFileData[] uploadFiles) where T : class, new()
        {
            var content = FormRequest(url, timeout, keepAlive, headers, parameters, requestUseEncodeName, responseUseEncodeName, uploadFiles);

            return ServiceProvider.JsonSerializer.Deserialize<T>(content);
        }





    }
}
