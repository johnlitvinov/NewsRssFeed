using OpenContact.BLL.Interfaces;
using OpenContact.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenContact.EF;

namespace OpenContact.BLL.Implementations
{
    public class SyncManager : ISyncManager

    {
        public void ProcessAllSources()
        {
            using (var db = new TestProgramDataBaseEntities())
            {
                 var datasource = db.NewsSources.ToList();
            }
        }

        public void ProcessByName(string name)
        {
            using (var db = new TestProgramDataBaseEntities())
            {
                var datasource = db.NewsSources.Where(n => n.Name == name).First();
                Process(datasource,db);
            }
        }

        private void Process(NewsSource source, TestProgramDataBaseEntities db)
        {

        }
    }
}
