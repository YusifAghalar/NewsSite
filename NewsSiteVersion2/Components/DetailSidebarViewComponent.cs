using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using NewsBLL.Abstract;
using NewsSiteVersion2.Models.Components;

namespace NewsSite.ViewComponets
{
    public class DetailSidebarComponent:ViewComponent
    {
        private INewsService _db;
        public DetailSidebarComponent(INewsService db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync(int categoryId)
        {
            var detailSidebarModel = new DetailSidebarModel()
            {
                News = await _db.GetAllWithCategories(x => x.CategoryId == categoryId)
                                                                     .OrderByDescending(x => x.CreatedOn)
                                                                     .Take(5).ToListAsync()

            };
            


            return View("_DetailSidebarComponent", detailSidebarModel);  
          
        }
    }
}
