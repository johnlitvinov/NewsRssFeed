using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenContact.Models;

namespace OpenContact.BLL.Interfaces
{
    public interface INewsReader
    {
        List<NewsPostDTO> Read(string url);
    }
}
