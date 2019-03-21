using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OpenContact.EF;
using OpenContact.Models;
using System.Data.Entity;
using OpenContact.BLL.Implementations;
using OpenContact.BLL.Interfaces;

namespace OpenContact.Website.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsSourcesRepository _newsSourcesRepository;
        private readonly INewsPostsRepository _newsPostsRepository;

        public NewsController()
        {
            _newsSourcesRepository = new NewsSourcesRepository();
            _newsPostsRepository = new NewsPostsRepository();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var sources = _newsSourcesRepository.GetAllNewsSources();
            ViewBag.NewsSources = sources;
            return View();
        }
       
        [HttpGet]
        public JsonResult GetNews(string sourceId, string sortBy)
        {
            List<NewsPost> newsPosts = null;

            if (sourceId == "all")
            {
                newsPosts = _newsPostsRepository.GetNewsPosts();
            }
            else
            {
                newsPosts = _newsPostsRepository.GetNewsPostsBySourceId(int.Parse(sourceId));
            }

            switch (sortBy)
            {
                case "date":
                    newsPosts = newsPosts.OrderByDescending(n => n.DateOfPublication).ToList();
                    break;
                case "source":
                    newsPosts = newsPosts.OrderBy(n => n.NewsSource.Name).ToList();
                    break;
            }
            
            return Json(newsPosts.Select(s => new
            {
                s.NewsName,
                s.ResourceID,
                s.NewsDescription,
                DataSource = s.NewsSource.Name,
                DateOfPublication = s.DateOfPublication.ToString()
            }), JsonRequestBehavior.AllowGet);
        }
    }
}