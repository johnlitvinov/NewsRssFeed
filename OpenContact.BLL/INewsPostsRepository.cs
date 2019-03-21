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
        DateTime? GetLatestNewsPostByNewsSourceId(int newsSourceId);
        void SaveNewsPosts(List<NewsPost> newsPosts);
    }
}
