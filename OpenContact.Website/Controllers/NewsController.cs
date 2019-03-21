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
        public JsonResult GetNews(string sourceId)
        {
           // _newsPostsRepository

            using (TestProgramDataBaseEntities db = new TestProgramDataBaseEntities())
            {
                var newsPosts = db.NewsPosts
                    .Include("NewsSource")
                    .ToList();

                return Json(newsPosts.Select(s => new
                {
                    s.NewsName,
                    s.NewsDescription,
                    DataSource = s.NewsSource.Name,
                    DateOfPublication = s.DateOfPublication.ToString()
                }), JsonRequestBehavior.AllowGet);
            }
        }
    }
}