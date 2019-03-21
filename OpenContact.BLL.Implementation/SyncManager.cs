using OpenContact.BLL.Interfaces;
using OpenContact.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenContact.EF;
using System.Xml;
using System.ServiceModel.Syndication;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace OpenContact.BLL.Implementations
{
    public class SyncManager : ISyncManager
    {
        private readonly INewsPostsRepository _newsPostsRepository;
        private readonly INewsSourcesRepository _newsSourcesRepository;
        private readonly INewsReader _newsReader;


        public SyncManager()
        {
            _newsSourcesRepository = new NewsSourcesRepository();
            _newsPostsRepository = new NewsPostsRepository();
            _newsReader = new NewsReader();
        }

        public void ProcessAllSources()
        {
            // Recieve all news sources
            var newsSources = _newsSourcesRepository.GetAllNewsSources();

             foreach (var newsSource in newsSources)
             {
                Process(newsSource);
             }
        }

         
        private void Process(NewsSource newsSource)
        {
            var news = _newsReader.Read(newsSource.Url);
            Console.WriteLine(string.Format("Прочитано {0} новостей из {1}", news.Count, newsSource.Name));

            var latestSavedNewsPostDate = _newsPostsRepository.GetLatestNewsPostByNewsSourceId(newsSource.Id);
            if (latestSavedNewsPostDate != null)
            {
                news = GetFreshNewsByDate(news, (DateTime)latestSavedNewsPostDate);
            }

            var filteredNewsEntities = news
                .Select(s => new NewsPost()
                {
                    NewsName = s.NewsName,
                    NewsDescription = s.NewsDescription,
                    DateOfPublication = s.DateOfPublication,
                    NewsSourceId = newsSource.Id
                })
                .ToList();

            _newsPostsRepository.SaveNewsPosts(filteredNewsEntities);
            Console.ReadKey();
        }

        #region Assistants

        static List<NewsPostDTO> GetFreshNewsByDate(List<NewsPostDTO> news, DateTime lastSavedDate)
        {
            // filter
            List<NewsPostDTO> list = news
                .Where(n => n.DateOfPublication > lastSavedDate)
                .ToList();
            return list;
            
        }
        #endregion Assistants
    }
}
