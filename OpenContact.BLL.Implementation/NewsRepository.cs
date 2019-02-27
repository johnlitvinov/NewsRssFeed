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
        void INewsRepository.AddNewsPost(NewsPost newsPost)
        {
            throw new NotImplementedException();
        }

        DateTime? INewsRepository.GetNearestNewsDate(int DataSourceId)
        {
            throw new NotImplementedException();
        }
    }
}

