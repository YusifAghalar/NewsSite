using Microsoft.AspNetCore.Mvc;
using NewsBLL.Abstract;
using NewsSiteVersion2.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSiteVersion2.Components
{
    public class AdminCategoryComponent:ViewComponent
    {
        private ICategoryService _categoryService;
        public AdminCategoryComponent(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }
        public IViewComponentResult  Invoke(int categoryId)
        {
            var adminSidebarModel = new AdminSidebarModel()
            {
                Categories = _categoryService.GetAll()


            };



            return View("_AdminCategoryViewComponent", adminSidebarModel);

        }
    }

}


