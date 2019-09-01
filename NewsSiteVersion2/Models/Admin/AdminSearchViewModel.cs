using NewsSiteVersion2.Entities;
using X.PagedList;

namespace NewsSiteVersion2.Models
{
    public class AdminSearchViewModel
    {
        public IPagedList<NewsWithAuthor> News { get; set; }
        public string SearchTerm { get; set; }
        public string CategoryName { get;  set; }
    }
}
