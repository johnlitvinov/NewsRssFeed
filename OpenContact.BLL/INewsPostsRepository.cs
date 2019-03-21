using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenContact.EF;
using OpenContact.Models;
namespace OpenContact.BLL.Interfaces
{
    public interface INewsPostsRepository
    {
        List<NewsPost> GetNewsPosts();
        List<NewsPost> GetNewsPostsBySourceId(int sourceId);
        DateTime? GetLatestNewsPostByNewsSourceId(int sourceId);
        void SaveNewsPosts(List<NewsPost> newsPosts);
    }
}
