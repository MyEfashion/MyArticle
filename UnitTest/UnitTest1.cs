using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyArticle;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        public void TestMethod1()
        {
            int pageSize = 10;
            int pageIndex = 0;
            int portalId = 0;


           List<MyArticleItem> articles =  MyArticleManager.GetArticlesByLastModifiedOnDate(pageSize, pageIndex, portalId, ResultSortType.ASC, DateTime.Now, DateTime.Now);
        }
    }
}
