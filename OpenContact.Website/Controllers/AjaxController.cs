using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OpenContact.EF;
using OpenContact.Models;
using System.Data.Entity;

namespace OpenContact.Website.Controllers
{
    public class AjaxController : Controller
    {
        public ActionResult Ajax()
        {
            return View();
        }
       
        [HttpGet]
        public JsonResult Result()
        {
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