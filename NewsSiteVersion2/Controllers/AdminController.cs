using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsBLL.Abstract;
using NewsEntities.Concrete;
using NewsSiteVersion2.Entities;
using NewsSiteVersion2.Models;
using X.PagedList;

namespace NewsSiteVersion2.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private INewsService _newsService;
        private ICategoryService _categoryService;
        private UserManager<ApplicationUser> _userManager;

        public AdminController(INewsService newsService,ICategoryService categoryService, UserManager<ApplicationUser> userManager)
        {
            _newsService = newsService;
            _categoryService = categoryService;
            _userManager = userManager;

        }
        [Route("Admin/News/Search/")]
        [HttpGet,HttpPost]
        [ActionName("Search")]
        public  IActionResult Search(string searchTerm, string categoryName ,int pagenumber=1)
        {
           
            var news = _newsService.SearchByCategoryandSearchTerm(searchTerm, categoryName);

            var newsList = new List<NewsWithAuthor>();
            foreach (var item in news)
            {
                newsList.Add(new NewsWithAuthor
                {
                    News = item,
                    
                    AuthorName = item.AuthorId == null
                                      ? "Undefined"
                                      : _userManager.FindByIdAsync(item.AuthorId).Result.UserName ?? "Undefined"
                });
            }
            var adminSearchViewModel = new AdminSearchViewModel()
            {
                News=newsList.ToPagedList(pagenumber,10),
                SearchTerm = searchTerm,
                CategoryName=categoryName
            };

            return View(adminSearchViewModel);
        }  

        // GET: News
        public IActionResult Index(string categoryName,int pageNumber=1,int pageSize=10)
        {
            Expression<Func<News, bool>> expression=null;
            if (!String.IsNullOrEmpty(categoryName))
            {
                expression = x => x.Category.Name == categoryName;
            }
            
            var news = _newsService.GetAllWithCategories(expression)
                                                   .OrderByDescending(x => x.CreatedOn)
                                                   .ToPagedList(pageNumber, pageSize);
           

            List<NewsWithAuthor> newsWithAuthors = new List<NewsWithAuthor>();
            
            foreach (var item in news)
            {
                newsWithAuthors.Add(new NewsWithAuthor
                {
                    News = item,
                    AuthorName = item.AuthorId==null
                                       ?"Undefined"
                                       : _userManager.FindByIdAsync(item.AuthorId).Result.UserName ?? "Undefined" 
                });
            };

            ViewBag.news = news;
            
            
           
            return View(newsWithAuthors);  
        }

        // GET: News/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = _newsService.Get((int)id);
                                         
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: News/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "Id","Name");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrUpdateViewModel createViewModel)
        {

            if (createViewModel.image != null)
            {
                var fileName = Path.GetFileName(createViewModel.image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\", fileName);
                var dbFilePath = Path.Combine("\\images\\", fileName);
                var fileStream = new FileStream(filePath, FileMode.Create);
                using (fileStream)
                {
                    await createViewModel.image.CopyToAsync(fileStream);
                }
                createViewModel.news.Imagepath = dbFilePath;
            }
                
                createViewModel.news.CreatedOn = DateTime.Now;
                createViewModel.news.AuthorId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                
                _newsService.Add(createViewModel.news);
               
                return RedirectToAction(nameof(Index));
        }

        // GET: News/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = _newsService.Get(id??1);
            if (result == null)
            {
                return NotFound();
            }
            var editViewModel = new CreateOrUpdateViewModel()
            {
                news = result,
                imagePath=result.Imagepath
                
            };
            ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "Id", "Name", result.CategoryId);
            return View(editViewModel);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateOrUpdateViewModel createViewModel)
        {
            if (id != createViewModel.news.Id)
            {
                return NotFound();
            }
            if (createViewModel.image != null)
            {
                string fileName = Path.GetFileName(createViewModel.image.FileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\", fileName);
                string dbFilePath = Path.Combine("\\images\\", fileName);
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                var pathToDelete = Path.Combine(Environment.CurrentDirectory, "wwwroot\\", "@" + createViewModel.imagePath);
                using (fileStream)
                {
                    //Deleting existing file
                    if (System.IO.File.Exists(pathToDelete))
                        System.IO.File.Delete(pathToDelete);
                    //Creating new file
                    await createViewModel.image.CopyToAsync(fileStream);
                }
                createViewModel.news.Imagepath = dbFilePath;
                
            }
            createViewModel.news.CreatedOn = DateTime.Now;
            createViewModel.news.AuthorId = User.FindFirst(ClaimTypes.NameIdentifier).Value;



            if (ModelState.IsValid)
            {
                try
                {
                    _newsService.Update(createViewModel.news);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(createViewModel.news.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "Id", "Name", createViewModel.news.CategoryId);
            return View(createViewModel);
        }

       
        [HttpGet]
        [Route("Admin/News/{categoryname}")]
        [ActionName("News")]
        public IActionResult GetByCategory(int id,int pageNumber)
        {
            return View("GetByCategory",_newsService.GetByCategory(id).ToPagedList(pageNumber,10));

        }

        [HttpGet, ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var news = _newsService.Get((int)id);
            _newsService.Delete(news);
            
            return RedirectToAction(nameof(Index));
        }
        [HttpGet, ActionName("DeleteCategory")]
        public IActionResult DeleteCategory(int? id)
        {
            _categoryService.Delete(new Category { Id = (int)id });
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ActionName("AddCategory")]
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        [ActionName("AddCategory")]
        public IActionResult CreateCategory(Category category)
        {
            _categoryService.Add(category);
            return RedirectToAction("Index");
        }







        private bool NewsExists(int id)
        {
            return _newsService.Get(id) == null;
        }

        

    }
}
