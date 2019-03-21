using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenContact.BLL.Interfaces;
using OpenContact.Models;
using System.Data.SqlClient;
using OpenContact.EF;

namespace OpenContact.BLL.Implementations
{
    public class NewsPostsRepository : INewsPostsRepository
    {
        public DateTime? GetLatestNewsPostByNewsSourceId(int newsSourceId)
        {
            DateTime? date = new DateTime();

            using (TestProgramDataBaseEntities db = new TestProgramDataBaseEntities())
            {
                date = db.NewsPosts
                    .Where(d => d.NewsSourceId == newsSourceId)
                    .OrderByDescending(np => np.DateOfPublication)
                    .Select(s => s.DateOfPublication)
                    .FirstOrDefault();
            }

            return date;
        }

        public void SaveNewsPosts(List<NewsPost> newsPosts)
        {
            using (TestProgramDataBaseEntities db = new TestProgramDataBaseEntities())
            {
                db.NewsPosts.AddRange(newsPosts);
                db.SaveChanges();
            }
        }
    }
}

