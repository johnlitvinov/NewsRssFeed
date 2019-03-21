using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenContact.BLL.Implementations;
using OpenContact.BLL.Interfaces;
using OpenContact.EF;
using OpenContact.Models;
using System.Data.Entity.Core.Common.EntitySql;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;
using System.ServiceModel.Syndication;


namespace OpenContact.Synchronizator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            ISyncManager syncManager = new SyncManager();
            syncManager.ProcessAllSources();

            Console.ReadKey();
        }
    }
}
