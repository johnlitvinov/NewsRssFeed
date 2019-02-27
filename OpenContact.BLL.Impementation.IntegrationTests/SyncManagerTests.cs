using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenContact.BLL.Implementations;
using OpenContact.BLL.Interfaces;
using OpenContact.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace OpenContact.BLL.Impementation.IntegrationTests
{
    [TestClass]
    class SyncManagerTests
    {
            private ISyncManager _syncManager;

            [TestInitialize]
            public void SetUp()
            {

                _syncManager = new SyncManager();
            }


            [TestMethod]
            public void Method()
            {
               
            }
    }
}
