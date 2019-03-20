using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenContact.BLL.Interfaces;
using OpenContact.Models;
using System.Data.SqlClient;

namespace OpenContact.BLL.Implementations
{
    public class NewsRepository : INewsRepository
    {
       public void AddNewsPost(NewsPostDTO newsPost)
        {
            throw new NotImplementedException();
        }

       public DateTime? GetNearestNewsDate()
        {
            throw new NotImplementedException();
        }
    }
}

