using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenContact.BLL.Interfaces;
using System.Xml;
using System.ServiceModel.Syndication;
using OpenContact.Models;

namespace OpenContact.BLL.Implementations
{
   public class NewsReader: INewsReader
    {

        public List<NewsPostDTO> Read(string url)
        {
            //string url = "http://habrahabr.ru/rss/ ";

            XmlReaderSettings readerSettings = new XmlReaderSettings();
            readerSettings.IgnoreComments = true;
            readerSettings.IgnoreWhitespace = true;
            XmlReader reader = XmlReader.Create(url, readerSettings);

            //XmlDocument xml = new XmlDocument();
            //xml.Load(reader);

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
