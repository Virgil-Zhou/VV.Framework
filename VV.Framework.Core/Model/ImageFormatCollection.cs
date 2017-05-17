using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VV.Framework.Core.Model
{
    /// <summary>
    /// 图像格式集合
    /// </summary>
    internal static class ImageFormatCollection
    {
        /// <summary>
        /// 文件头（十六进制）与文件格式字典
        /// </summary>
        readonly static SortedDictionary<string, ImageFormat> byteMapFormat;


        static ImageFormatCollection()
        {
            byteMapFormat = new SortedDictionary<string, ImageFormat>()
            {
                { "424D"     , ImageFormat.BMP },
                { "FFD8FF"   , ImageFormat.JPG },
                { "47494638" , ImageFormat.GIF },
                { "89504E47" , ImageFormat.PNG },
                { "38425053" , ImageFormat.PSD },
                { "49492A00" , ImageFormat.TIFF }
            };
        }


        /// <summary>
        /// 根据文件头（十六进制）获取图像格式，hexStr为空或null会引发ArgumentNullException异常
        /// </summary>
        /// <param name="hexStr">文件头十六进制字符串</param>
        /// <returns>返回图像格式枚举</returns>
        public static ImageFormat Get(string hexStr)
        {
            if (hexStr.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(hexStr), "文件头十六进制字符串不能为空");

            var key = byteMapFormat.Keys.FirstOrDefault(c => hexStr.Contains(c));
            if (key == null)
                return ImageFormat.None;

            return byteMapFormat[key];
        }

    }
}
