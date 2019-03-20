using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenContact.BLL.Implementations;
using OpenContact.BLL.Interfaces;

namespace OpenContact.Synchronizator.Console
{
    class Program
    {
        static void Main(string[] args)
        {

            ISyncManager syncManager = new SyncManager();
            syncManager.ProcessAllSources();
           
            INewsReader newsReader = new NewsReader();
            newsReader.Read("http://habrahabr.ru/rss/");
        }
    }
}
