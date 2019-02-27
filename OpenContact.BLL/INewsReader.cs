using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenContact.BLL.Interfaces;
using OpenContact.Models;

namespace OpenContact.BLL.Interfaces
{
    public interface INewsReader
    {
        List<NewsPost> Read(string url);
    }
}
