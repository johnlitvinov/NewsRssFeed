using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenContact.BLL.Implementations;
using OpenContact.BLL.Interfaces;
using OpenContact.Models;

namespace OpenContact.BLL.Impementation.IntegrationTests
{
    [TestClass]
    public class NewsReaderTests
    {
        private INewsReader _newsReader;

        [TestInitialize]
        public void SetUp()
        {
          
           _newsReader = new NewsReader();
        }


        [TestMethod]
        public void Read()
        {
            //string url = "http://www.interfax.by/news/feed";
            string url = "http://habrahabr.ru/rss/";
            
            var news = _newsReader.Read(url);

            Assert.IsNotNull(news);
        }
    }
}


