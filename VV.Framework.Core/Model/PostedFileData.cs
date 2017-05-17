using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VV.Framework.Core.Model
{
    /// <summary>
    /// 上载文件数据信息
    /// </summary>
    public class PostedFileData
    {
        /// <summary>
        /// 构造表单的 Name 属性名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 文件的全名称（文件名+扩展名）
        /// </summary>
        public string FileFullName { get; set; }

        /// <summary>
        /// 文件的MIME类型
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// 文件的字节表现形式
        /// </summary>
        public byte[] FileBinary { get; set; }
    }
}
