using NewsEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSiteVersion2.Models.Components
{
    public class DetailSidebarModel
    {
        public List<News> News { get; set; }
        public string CategoryName { get; set; }

    }
}
