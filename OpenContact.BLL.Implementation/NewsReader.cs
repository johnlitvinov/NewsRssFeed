using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenContact.BLL.Interfaces;
using System.Xml;
using System.ServiceModel.Syndication;
using OpenContact.Models;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace OpenContact.BLL.Implementations
{
   public class NewsReader: INewsReader
   {
        public List<NewsPostDTO> Read(string url)
        {
            string cleanedXML = string.Empty;
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                string rawXML = client.DownloadString(url);
                rawXML = rawXML.Replace("\n", "");
                rawXML = rawXML.Replace("\r", "");
                Regex regex = new Regex(@">\s*<");
                cleanedXML = regex.Replace(rawXML, "><");
            }

            XmlReader reader = XmlReader.Create(new StringReader(cleanedXML));
            List<NewsPostDTO> list = new List<NewsPostDTO>();

            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            foreach (SyndicationItem item in feed.Items)
            {
                var newPost = new NewsPostDTO();
                newPost.ResourceId = item.Id;
                newPost.NewsName = item.Title.Text;
                newPost.DataSource = url;
                newPost.NewsDescription = item.Summary.Text;
                newPost.DateOfPublication = item.PublishDate.DateTime;

                list.Add(newPost);
            }
            return list;
        }
    }
}
