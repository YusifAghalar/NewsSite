using NewsEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSiteVersion2.Models
{
    public class IndexViewModel
    {
        public List<News> News { get; set; }
        public List<News> HeaderNews { get; set; }
    }
}
