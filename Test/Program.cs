using OpenContact.EF;
using OpenContact.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Test
{
    class Program
    {
        static string interfaxUrl = "http://www.interfax.by/news/feed";
        static string habrahabrUrl = "http://habrahabr.ru/rss/ ";
        static string interfacsSource = "Interfacs";
        static string habrnabbrSource = "habrnabbr";
        static int interfacsSourceId = 1;
        static int habrnabbrSourceId = 2;
        

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // Recieved all news sources
            var allNewsSources = GetAllNewsSources();

            foreach (var newsSource in allNewsSources)
            {
                var news = GetNews(newsSource.Url);
                Console.WriteLine(string.Format("Read {0} news from {1}", news.Count, newsSource.Name));

                var latestSavedNewsDate = ChooseLastNewsPostDateFromDateBase(newsSource.Id);

                if (latestSavedNewsDate != null)
                {
                    news = FilterValue(news, (DateTime)latestSavedNewsDate);
                }

                SaveNews(news, newsSource.Name);
            }



            //string interfaxUrl = "http://www.interfax.by/news/feed";
            //string habrahabrUrl = "http://habrahabr.ru/rss/ ";
            //string interfacsSource = "Interfacs";
            //string habrnabbrSource = "habrnabbr";
            //int interfacsSourceId = 1;
            //int habrnabbrSourceId = 2;


            // чтение из HabraHabr
            // сохранить новости

            //var news = GetHabraHabrNews();
            //Console.WriteLine(string.Format("Read {0} news from Habrahabr", news.Count));
            //SaveHabrahabrNews(news);

            
            
            // чтение из Interfax
            // сохранить новости

            //var intefaxNews = GetInterfaxNews();
            //Console.WriteLine(string.Format("Read {0} news from Interfax", intefaxNews.Count));
            //SaveInterfaxNews(intefaxNews);

            //var NewsFromInterfax = GetNews(interfaxUrl);
            //Console.WriteLine(string.Format("Read {0} news from Interfax", NewsFromInterfax.Count));
           
            //ChooseLastNewsPostDateFromDateBase(habrnabbrSourceId);
            //FilterValue(NewsFromInterfax,);
            //SaveNews(NewsFromInterfax, interfacsSource);

             Console.ReadKey();
        }

        #region GetAllNewsSources
        public static List<NewsSource> GetAllNewsSources()
        {
            using (var db = new TestProgramDataBaseEntities())
            {
                var newsSources = db.NewsSources.ToList();
                return newsSources;
            }
        }
        #endregion


        // 1 вариант.
        #region Habr
        static List<NewsPostDTO> GetHabraHabrNews()
        {
            string url = "http://habrahabr.ru/rss/ ";

            XmlReaderSettings readerSettings = new XmlReaderSettings();
            readerSettings.IgnoreComments = true;
            readerSettings.IgnoreWhitespace = true;
            XmlReader reader = XmlReader.Create(url, readerSettings);

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


       
        static void SaveHabrahabrNews(List<NewsPostDTO> news)
        {
            using (TestProgramDataBaseEntities db = new TestProgramDataBaseEntities())
            {
                var newsSource = db.NewsSources.First(n => n.Name == "habrnabbr");
                for (int i = 0; i < news.Count; i++)
                {
                    var row = news[i]; 
                    //Console.WriteLine((i +1)+ " - " + row.NewsName);
                    db.NewsPosts.Add(new NewsPost()
                    {
                        NewsName = row.NewsName,
                        NewsDescription = row.NewsDescription,
                        DateOfPublication = row.DateOfPublication,
                        NewsSource = newsSource
                        
                    });
                    db.SaveChanges();
                }
               

            }
        }
        #endregion Habr
        #region Interfax
        static List<NewsPostDTO> GetInterfaxNews()
        {
            string url = "http://www.interfax.by/news/feed";

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

           // Encoding.UTF8.GetString()

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

        static void SaveInterfaxNews(List<NewsPostDTO> news)
        {
            using (TestProgramDataBaseEntities db = new TestProgramDataBaseEntities())
            {
                var newsSource = db.NewsSources.First(n => n.Name == "Interfacs");
                for (int i = 0; i < news.Count; i++)
                {
                    var row = news[i];
                    //Console.WriteLine((i +1)+ " - " + row.NewsName);
                    db.NewsPosts.Add(new NewsPost()
                    {
                        NewsName = row.NewsName,
                        NewsDescription = row.NewsDescription,
                        DateOfPublication = row.DateOfPublication,
                        NewsSource = newsSource
                    });
                    db.SaveChanges();
                }
            }
        }
        #endregion Interfax
        // 2 вариант.
        #region UniversalGetNewsFunction
        static List<NewsPostDTO> GetNews(string url)
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
            // Encoding.UTF8.GetString()

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
        #endregion UniversalGetNewsFunction
        #region UniversalSaveNewsFunction
        static void SaveNews(List<NewsPostDTO> news, string source)
        {

            using (TestProgramDataBaseEntities db = new TestProgramDataBaseEntities())
            {

                var newsSource = db.NewsSources.First(n => n.Name == source);



                for (int i = 0; i < news.Count; i++)
                {
                    var row = news[i];

                    Console.WriteLine((i + 1) + " - " + row.NewsName);
                    db.NewsPosts.Add(new NewsPost()
                    {
                        NewsName = row.NewsName,
                        NewsDescription = row.NewsDescription,
                        DateOfPublication = row.DateOfPublication,
                        NewsSource = newsSource
                    });


                    db.SaveChanges();
                }
            }
        }
        #endregion UniversalSaveNewsFunction


        #region ChooseLastNewsPostDateFromDateBase
        static DateTime? ChooseLastNewsPostDateFromDateBase(int source)
        {
            DateTime? date = new DateTime();

            using (TestProgramDataBaseEntities db = new TestProgramDataBaseEntities())
            {
                date = db.NewsPosts
                    .Where(d => d.DataSourceId == source)
                    .OrderByDescending(np => np.DateOfPublication)
                    .Select(s => s.DateOfPublication)
                    .FirstOrDefault();

               
                Console.WriteLine(date);

            }
            return date;
        }
        #endregion

        #region FilterValue
        static List<NewsPostDTO> FilterValue(List<NewsPostDTO> news, DateTime lastSavedDate)
        {
            // filter
            List<NewsPostDTO> list = news
                  .Where(n => n.DateOfPublication > lastSavedDate)
                  .ToList();

            return list;
        }
        #endregion


    }// end class
}//end namespace

