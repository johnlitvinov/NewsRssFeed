using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenContact.Models
{
    public class NewsPost
    {
        public string ResourceId { get; set; }
        public string DataSource { get; set; }
        public string NewsName { get; set; }
        public string NewsDescription { get; set; }
        public DateTime DateOfPublication { get; set; }
    }
}
