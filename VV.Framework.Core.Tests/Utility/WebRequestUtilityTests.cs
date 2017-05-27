using Microsoft.VisualStudio.TestTools.UnitTesting;
using VV.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VV.Framework.Core.Tests
{
    [TestClass()]
    public class WebRequestUtilityTests
    {
        [TestMethod()]
        public void GetTest()
        {
            var content = WebRequestUtility.Get("https://www.baidu.com/");

            Assert.IsNotNull(content);
        }

        [TestMethod()]
        public void PostTest()
        {
            var content = WebRequestUtility.Post("https://www.baidu.com/", null);

            Assert.IsNotNull(content);
        }

        [TestMethod()]
        public void FormRequestTest()
        {
            var url = "http://localhost:13050/api/file/upload";

            var parameters = new Dictionary<string, string>
            {
                ["Phone"] = "18620706258",
                ["Name"] = "Virgil"
            };

            var fileDic = new Dictionary<string, string>
            {
                ["JDB-1"] = @"F:\迅雷下载\stupid-girl.zip\stupid girl\最新全套\1河南—朱江雪\1.jpg",
                ["JDB-2"] = @"F:\迅雷下载\stupid-girl.zip\stupid girl\最新全套\1河南—朱江雪\2.jpg",
                ["JDB-3"] = @"F:\迅雷下载\stupid-girl.zip\stupid girl\最新全套\1河南—朱江雪\3.jpg",
            };

            var fileCount = WebRequestUtility.FormRequest(url, parameters, fileDic);

            Assert.IsTrue(fileCount == "3");
        }
    }
}