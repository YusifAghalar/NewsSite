using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsEntities.Concrete;
using NewsSiteVersion2.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSiteVersion2.Components
{
    public class NewsModalComponent:ViewComponent
    {
        private HttpContextAccessor _httpContextAccessor;
        public NewsModalComponent(HttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IViewComponentResult Invoke(int categoryId)
        {
            ModalNews newsModal = _httpContextAccessor.HttpContext.Session.GetObject<ModalNews>("ModalNews");

            return View("_NewsModalComponent", newsModal);

        }
    }
}
