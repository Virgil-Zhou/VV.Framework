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
            var content = WebRequestUtility.Get("https://www.baidu.com/", 300, "", true, null, "UTF-8", "UTF-8");

            Assert.Fail();
        }
    }
}