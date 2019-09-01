using NewsEntities.Concrete;
using NewsSiteVersion2.Entities;
using System.Collections.Generic;
using X.PagedList;

namespace NewsSiteVersion2.Models
{
    public class SearchViewModel
    {
        public List<News>  News{ get; set; }
        public string   SearchTerm { get; set; }
    }
}
