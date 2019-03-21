using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenContact.BLL.Interfaces;
using OpenContact.EF;

namespace OpenContact.BLL.Implementations
{
    public class NewsSourcesRepository : INewsSourcesRepository
    {
        public List<NewsSource> GetAllNewsSources()
        {
            using (var db = new TestProgramDataBaseEntities())
            {
                var newsSources = db.NewsSources.ToList();
                return newsSources;
            }
        }
    }
}
