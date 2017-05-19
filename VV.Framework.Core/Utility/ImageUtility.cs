using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace VV.Framework.Core
{
    /// <summary>
    /// 提供图像操作常用成员
    /// </summary>
    public static class ImageUtility
    {
        /// <summary>
        /// 图像格式识别，path为空或null会引发ArgumentNullException异常
        /// </summary>
        /// <param name="path">文件的绝对路径</param>
        /// <returns>返回图像格式枚举</returns>
        public static ImageFormat FormatRecognition(string path)
        {
            if (path.IsNullOrWhiteSpace())
                throw new ArgumentNullException(nameof(path), "图像路径不能为空");

            var buffer = new Byte[4];
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                fs.Read(buffer, 0, buffer.Length);
            }

            var hexStr = buffer.FileHeaderHex();
            return ImageFormatCollection.Get(hexStr);
        }


        /// <summary>
        /// 图像格式识别，arr为null会引发ArgumentNullException异常
        /// </summary>
        /// <param name="arr">图像的字节数组</param>
        /// <returns>返回图像格式枚举</returns>
        public static ImageFormat FormatRecognition(byte[] arr)
        {
            if (arr == null)
                throw new ArgumentNullException(nameof(arr), "图像字节数组不能为空");

            var hexStr = arr.FileHeaderHex();
            return ImageFormatCollection.Get(hexStr);
        }


        /// <summary>
        /// 图像格式识别，postedFile为null会引发ArgumentNullException异常
        /// </summary>
        /// <param name="postedFile">上载文件对象</param>
        /// <returns>返回图像格式枚举</returns>
        public static ImageFormat FormatRecognition(HttpPostedFileBase postedFile)
        {
            if (postedFile == null)
                throw new ArgumentNullException(nameof(postedFile), "上载文件对象不能为空");

            byte[] buffer;
            using (var br = new BinaryReader(postedFile.InputStream))
            {
                buffer = br.ReadBytes(4);
            }

            var hexStr = buffer.FileHeaderHex();
            return ImageFormatCollection.Get(hexStr);
        }
    }
}
