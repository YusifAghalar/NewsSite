using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsBLL.Abstract;
using NewsSiteVersion2.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSiteVersion2.Components
{
    public class MenuComponent : ViewComponent
    {
        private ICategoryService _db;
        public MenuComponent(ICategoryService db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            var menuModel = new MenuModel()
            {
                Categories = _db.GetAll()
            };

            return View("_MenuComponent", menuModel);
        }
    }
}
