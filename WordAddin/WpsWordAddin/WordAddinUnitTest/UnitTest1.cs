using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.Model;
using Common.Utility;

namespace WordAddinUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestEnumMethod()
        {
            EnumUtility.GetValueByDesc(typeof(ContTextOption), "文本审阅");

            var result=EnumUtility.GetValueByRequestType(typeof(ContTextOption), "lhwcms://contractReview/");
            Assert.AreEqual(result,2);
            StoreData.contTextOption = (ContTextOption)EnumUtility.GetValueByRequestType(typeof(ContTextOption), "lhwcms://contractReview/");
            
        }


    }
}
