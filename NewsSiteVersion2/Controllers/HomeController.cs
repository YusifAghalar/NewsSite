using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsBLL.Abstract;
using NewsEntities.Concrete;
using NewsSiteVersion2.Extensions;
using NewsSiteVersion2.Models;
using X.PagedList;

namespace NewsSiteVersion2.Controllers
{
    public class HomeController : Controller
    {
        private INewsService NewsService;
        public HomeController(INewsService newsService)
        {
            this.NewsService = newsService;
        }
        [HttpGet]
        [Route("")]
        [ActionName("Index")]
        public IActionResult Index()
        {
            var indexViewModel = new IndexViewModel()
            {
                News = NewsService.GetAllWithCategories()
                                                 .OrderByDescending(x=>x.CreatedOn)
                                                 .Take(15).ToList(),
                HeaderNews = NewsService.GetHeaderNews(3)
            };
            
           
            return View(indexViewModel);
        }
        [HttpPost]
        [Route("Home/News/Search")]
        [ActionName("Search")]
        public IActionResult Search(string searchTerm)
        {
            var searchViewModel = new SearchViewModel()
            {
                News = NewsService.GetAllWithCategories(x => x.Title.Contains(searchTerm))
                                                            .OrderByDescending(x => x.CreatedOn).Take(15)
                                                            .ToList(),
                SearchTerm = searchTerm
            };
            return View(searchViewModel);
        }

        
        [HttpGet]
        [Route("Home/News/Detail/{id}")]
        [ActionName("Detail")]
        public IActionResult Detail(int id)
        {
            var detailViewModel = new DetailViewModel()
            {
                News = NewsService.Get(id)
            };

            return View(detailViewModel);
        }
        [HttpGet]
        [Route("/Home/News/")]
        [ActionName("News")]
        public IActionResult GetByCategory(int? page, int pagesize=5,string categoryname = "World")
        {

            var news = NewsService.GetAllWithCategories(x=>x.Category.Name==categoryname)
                                  .OrderByDescending(x=>x.CreatedOn).ToPagedList(page??1,pagesize);

            return View("GetByCategory",news);
        }


        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult AddToNewsModal(int id )
        {
            if (User.Identity.IsAuthenticated)
            {
                var modalnews = HttpContext.Session.GetObject<ModalNews>("ModalNews");
                if (modalnews == null)
                {
                    modalnews = new ModalNews();
                    modalnews.News = new List<News>();
                    modalnews.News.Add(NewsService.Get(id));
                    HttpContext.Session.SetObject("ModalNews", modalnews);

                    return Redirect(Request.Headers["Referer"].ToString());
                }
                else
                {
                    modalnews.News.Add(NewsService.Get(id));
                    HttpContext.Session.SetObject("ModalNews", modalnews);
                    return Redirect(Request.Headers["Referer"].ToString());

                }
            }
           
            return Redirect(Request.Headers["Referer"].ToString());
           
        }
        public IActionResult RemoveFromNewsModal(int? id)
        {
           
            if (id == null)
            { 
                HttpContext.Session.Clear();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            var modalnews = HttpContext.Session.GetObject<ModalNews>("ModalNews");
            var newsToBeDeleted = modalnews.News.FirstOrDefault(x => x.Id == id);
            modalnews.News.Remove(newsToBeDeleted);
            HttpContext.Session.SetObject("ModalNews", modalnews);
            return Redirect(Request.Headers["Referer"].ToString());

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
