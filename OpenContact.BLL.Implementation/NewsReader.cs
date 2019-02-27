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
        public static void Reader()
        {
            string url = "http://habrahabr.ru/rss/ ";
            XmlReaderSettings readerSettings = new XmlReaderSettings();
            readerSettings.IgnoreComments = true;
            readerSettings.IgnoreWhitespace = true;
            XmlReader reader = XmlReader.Create(url, readerSettings);

            //XmlDocument xml = new XmlDocument();
            //xml.Load(reader);

            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            foreach (SyndicationItem item in feed.Items)
            {
                String subject = item.Title.Text;
                String summary = item.Summary.Text;
            }
        }

        public List<NewsPost> Read(string url)
        {
            throw new NotImplementedException();
        }
    }
}
