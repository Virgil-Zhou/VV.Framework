using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VV.Framework.Core
{
    /// <summary>
    /// 图像格式枚举
    /// </summary>
    public enum ImageFormat
    {
        /// <summary>
        /// 其它文件格式
        /// </summary>
        None = 0,

        /// <summary>
        /// (Bitmap)位图
        /// </summary>
        BMP = 1,

        /// <summary>
        /// JPG图像格式
        /// </summary>
        JPG = 2,

        /// <summary>
        /// GIF动态图像格式
        /// </summary>
        GIF = 3,

        /// <summary>
        /// PNG图像格式
        /// </summary>
        PNG = 4,

        /// <summary>
        /// PSD图像格式
        /// </summary>
        PSD = 5,

        /// <summary>
        /// 标签图像文件格式
        /// </summary>
        TIFF = 6
    }
}
