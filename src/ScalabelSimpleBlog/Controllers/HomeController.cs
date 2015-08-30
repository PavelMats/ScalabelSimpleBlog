using ScalabelSimpleBlog.Business.Dto.HomeCotrollerDto;
using ScalabelSimpleBlog.Business.Services.Contracts;
using ScalabelSimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScalabelSimpleBlog.Controllers
{
    public class HomeController : Controller
    {
        protected IBlogService Blogservice { get; set; }

        public HomeController(IBlogService blogService)
        {
            this.Blogservice = blogService;
        }

        public ActionResult Index()
        {
            var model = new HomeIndexModel();

            model.LatestArticles  = this.Blogservice.GetLatest<LatestArticlesDto>(3);

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}