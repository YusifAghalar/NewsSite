using NewsSiteVersion2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSiteVersion2.Models
{
    public class AdminIndexViewModel
    {
        public List<NewsWithAuthor> News { get; set; }
        public string searchTerm { get; set; }
    }
}
